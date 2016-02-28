(function() {
    'use strict';
    
    var serviceId = 'entityManagerFactory';
    angular.module('app')
           .factory(serviceId, ['config', 'model', emFactory]);

    function emFactory(config, model) {
        
        var serviceName = config.remoteServiceName;
        var metadataStore = createMetadataStore();

        var provider = {
            metadataStore: metadataStore,
            newManager: newManager
        };
        
        return provider;

        function createMetadataStore() {
            var store = new breeze.MetadataStore();
            model.configureMetadataStore(store);
            if (model.useManualMetadata) {
                store.addDataService(new breeze.DataService({ serviceName: serviceName }));
            }
            return store;
        }

        function newManager() {
            var mgr = new breeze.EntityManager({
                serviceName: serviceName,
                metadataStore: metadataStore
            });
            return mgr;
        }
    }
})();