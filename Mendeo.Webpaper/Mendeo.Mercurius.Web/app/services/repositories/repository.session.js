(function () {
    'use strict';

    var serviceId = 'repository.session';
    angular.module('app').factory(serviceId,
        ['config', 'model', 'repository.abstract', RepositorySession]);

    function RepositorySession(config, model, AbstractRepository) {
        var entityName = model.entityNames.session;
        var localSessionSort = config.localSessionSort || function () { /*noop for Mongo*/ }; 
        var orderBy = 'timeSlot.start, speaker.firstName, title';
        var sessionsQuery = breeze.EntityQuery.from('Sessions');

        return {           
            create: createRepo // factory function to create the repository
        };

        /* Implementation */
        function createRepo(manager) {
            var base = new AbstractRepository(manager, entityName, serviceId);
            var count = undefined, sessionsPerTrack = undefined;
            var repo = {
                create: create,
                getById: base.getById,
                getCount: getCount,
                getEntityByIdOrFromWip: base.getEntityByIdOrFromWip,
                getPartials: getPartials,
                getSessionsPerTrack: getSessionsPerTrack
            };

            return repo;

            function create() { return manager.createEntity(entityName); }

            function getCount() {
                if (base.zStorage.areItemsLoaded('sessions')) {
                    return base.$q.when(base.getLocalEntityCount(entityName));
                }
                if (count !== undefined) { return base.$q.when(count); }
                // Sessions aren't loaded and don't have a count yet;
                // ask the server for a count and remember it
                return sessionsQuery.take(0).inlineCount()
                    .using(manager).execute()
                    .then(function(data) {
                         return count = data.inlineCount;
                    });
            }

            function getPartials(forceRemote) {
                var sessions;
                if (base.zStorage.areItemsLoaded('sessions') && !forceRemote) {
                    sessions = base.getAllLocal(entityName, orderBy);
                    return base.$q.when(sessions);
                }

                return sessionsQuery
                    .select('id, title, code, speakerId, trackId, timeSlotId, roomId, level, tags')
                    .orderBy(orderBy)
                    .toType(entityName)
                    .using(manager).execute()
                    .then(success).catch(base.queryFailed);

                function success(response) {
                    sessions = base.setIsPartialTrue(response.results);
                    localSessionSort(sessions); // for Mongo
                    base.zStorage.areItemsLoaded('sessions', true);
                    base.log('Retrieved [Session Partials] from remote data source', sessions.length, true);
                    base.zStorage.save();
                    return sessions;
                }

            }

            function getSessionsPerTrack() {
                // select trackIds from all sessions
                var query = sessionsQuery.select('trackId');
                if (base.zStorage.areItemsLoaded('sessions')) {
                    // query the cache if sessions are in cache
                    query = query.using(breeze.FetchStrategy.FromLocalCache);
                } else {
                    // use cached count, if we have it, rather than go to server
                    if (sessionsPerTrack) {
                        return base.$q.when(sessionsPerTrack);
                    }
                }
                // proceed with query to get trackIds and build 'sessionsPerTrack'
                return manager.executeQuery(query)
                    .then(success).catch(base.queryFailed);

                function success(response) {
                    var trackIds = response.results;
                    var tidCounts = {}; // {trackid: count} hash

                    trackIds.forEach(function (t) {
                        // increment or create-and-set
                        tidCounts[t.trackId] = tidCounts[t.trackId] ?
                            tidCounts[t.trackId] += 1 : 1;
                    });

                    sessionsPerTrack =
                        base.getAllLocal(model.entityNames.track, 'name', base.predicates.isNotNullo)
                            .map(function(t) {
                                return { id: t.id, track: t.name, count: tidCounts[t.id] || 0 };
                            });

                    return sessionsPerTrack;
                }
            }
        }
    }
})();