(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId,
        ['$injector', '$rootScope', 'common', 'config', 'entityManagerFactory', 'model', 'zStorage', 'zStorageWip', datacontext]);

    function datacontext($injector, $rootScope, common, config, emFactory, model, zStorage, zStorageWip) {
        var events = config.events;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(serviceId);
        var logError = getLogFn(serviceId, 'error');
        var logSuccess = getLogFn(serviceId, 'success');
        var manager = emFactory.newManager();
        var primePromise;
        var repoNames = ['attendee', 'lookup', 'session', 'speaker'];
        var $q = common.$q;

        var service = {
            // functions
            cancel: cancel,
            markDeleted: markDeleted,
            prime: prime,
            save: save,
            // sub-services
            zStorage: zStorage,
            zStorageWip: zStorageWip
            // Repositories to be added by defineLazyLoadedRepos
            //      attendees
            //      lookups
            //      sessions
            //      speakers
        };
       
        init();

        return service;

        function init() {
            zStorage.init(manager);
            zStorageWip.init(manager);
            defineLazyLoadedRepos();
            setupEventForHasChangesChanged();
            setupEventForEntitiesChanged();
            listenForStorageEvents();
        }

        function cancel() {
            if (manager.hasChanges()) {
                manager.rejectChanges();
                logSuccess('Canceled changes', null, true);
            }
        }

        // Add ES5 property to datacontext for each named repo
        function defineLazyLoadedRepos() {                     
            repoNames.forEach(function (name) {
                Object.defineProperty(service, name, {
                    configurable: true, // will redefine this property once
                    get: function () {
                        // The 1st time the repo is request via this property, 
                        // we ask the repositories for it (which will inject it).
                        var repo = getRepo(name);
                        // Rewrite this property to always return this repo;
                        // no longer redefinable
                        Object.defineProperty(service, name, {
                            value: repo,
                            configurable: false,
                            enumerable: true
                        });
                        return repo;
                    }
                });
            });
        }

        // Get named Repository Ctor (by injection), new it, and initialize it
        function getRepo(repoName) {
            var fullRepoName = 'repository.' + repoName.toLowerCase();
            var factory = $injector.get(fullRepoName);
            return factory.create(manager);
        }

        function listenForStorageEvents() {
            $rootScope.$on(config.events.storage.storeChanged, function (event, data) {
                log('Updated local storage', data, true);
            });
            $rootScope.$on(config.events.storage.wipChanged, function (event, data) {
                log('Updated WIP', data, true);
            });
            $rootScope.$on(config.events.storage.error, function (event, data) {
                logError('Error with local storage. ' + data.activity, data, true);
            });
        }

        function markDeleted(entity) {
            return entity.entityAspect.setDeleted();
        }

        function prime() {
            // There are many paths through here, all must return a promise.

            // This function can only be called once.
            if (primePromise) return primePromise; 

            // look in local storage, if data is here, 
            // grab it. otherwise get from 'resources'
            var storageEnabledAndHasData = zStorage.load(manager);
            var promise = storageEnabledAndHasData ?
                $q.when(log('Loading entities and metadata from local storage')) :
                loadLookupsFromRemote();

            primePromise = promise.then(success);
            return primePromise;

            function loadLookupsFromRemote() {
                // get lookups and speakers from remote data source, in parallel
                var promise = $q.all([service.lookup.getAll(), service.speaker.getPartials(true)]);
                if (!model.useManualMetadata) {
                    // got metadata from remote service; now extend it
                    promise = promise.then(function () {
                        model.extendMetadata(manager.metadataStore);
                    });
                }
                return promise.then(function() { zStorage.save(); });
            }


            function success() {
                log('Primed data', service.lookup.cachedData);
            }
        }

        function save() {
            // Learning Point:
            // Must convert all q promises to $q.
            // Breeze uses q, angular uses $q.
            return manager.saveChanges().then(saveSucceeded).catch(saveFailed);

            function saveSucceeded(result) {
                logSuccess('Saved data', result, true);
                zStorage.save();
            }

            function saveFailed(error) {
                var msg = config.appErrorPrefix + 'Save failed: ' +
                    breeze.saveErrorMessageService.getErrorMessage(error);
                error.message = msg;
                logError(msg, error);
                throw error;
            }
        }

        //#region Internal methods   
        
        function setupEventForEntitiesChanged() {
            // We use this for detecting changes of any kind so we can save them to local storage
            manager.entityChanged.subscribe(function (changeArgs) {
                if (changeArgs.entityAction === breeze.EntityAction.PropertyChange) {
                    interceptPropertyChange(changeArgs);
                    common.$broadcast(events.entitiesChanged, changeArgs);
                }
            });
        }

        function setupEventForHasChangesChanged() {
            manager.hasChangesChanged.subscribe(function(eventArgs) {
                var data = { hasChanges: eventArgs.hasChanges };
                common.$broadcast(events.hasChangesChanged, data);
            });
        }

        // Forget certain changes by removing them from the entity's originalValues
        // This function becomes unnecessary if Breeze decides that
        // unmapped properties are not recorded in originalValues
        //
        // We do this so we can remove the isSpeaker and isPartial properties from
        // the originalValues of an entity. Otherwise, when the object's changes
        // are canceled these values will also reset: isPartial will go
        // from false to true, and force the controller to refetch the
        // entity from the server.
        // Ultimately, we do not want to track changes to these properties, 
        // so we remove them.        
        function interceptPropertyChange(changeArgs) {
            var changedProp = changeArgs.args.propertyName;
            if (changedProp === 'isPartial' || changedProp === 'isSpeaker') {
               delete changeArgs.entity.entityAspect.originalValues[changedProp];
            }
        }
        //#endregion
    }
})();