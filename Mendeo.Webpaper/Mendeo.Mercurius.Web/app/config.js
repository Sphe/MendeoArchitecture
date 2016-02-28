﻿(function () {
    'use strict';

    var app = angular.module('app');

    // Configure Toastr
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = 'toast-bottom-right';
    toastr.options.showMethod = 'slideDown';
    toastr.options.hideMethod = 'slideUp';

    var keyCodes = {
        backspace: 8,
        tab: 9,
        enter: 13,
        esc: 27,
        space: 32,
        pageup: 33,
        pagedown: 34,
        end: 35,
        home: 36,
        left: 37,
        up: 38,
        right: 39,
        down: 40,
        insert: 45,
        del: 46
    };

    var imageSettings = {
        imageBasePath: '../content/images/photos/',
        unknownPersonImageSource: 'unknown_person.jpg'
    };

    var remoteServiceName = 'breeze/Breeze';

    var events = {
        controllerActivateSuccess: 'controller.activateSuccess',
        entitiesChanged: 'datacontext.entitiesChanged',
        entitiesImported: 'datacontext.entitiesImported',
        hasChangesChanged: 'datacontext.hasChangesChanged',
        spinnerToggle: 'spinner.toggle',
        storage: {
            error: 'store.error',
            storeChanged: 'store.changed',
            wipChanged: 'wip.changed'
        }
    };

    var config = {
        appErrorPrefix: '[CC Error] ', //Configure the exceptionHandler decorator
        busyIndicator: 'overlay', // 2 options: spinner or overlay
        docTitle: 'SoukAnnonce',
        events: events,
        imageSettings: imageSettings,
        keyCodes: keyCodes,
        remoteServiceName: remoteServiceName,
        version: '1.1.0'
    };

    app.value('config', config);
    app.value('appSettingsService', {
                brand: 'Mendeo',
                title: 'SoukAnnonce',
                siteUrl: ''
            });

    app.config(['$logProvider', function ($logProvider) {
        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
    }]);
    
    //#region Configure the common services via commonConfig
    app.config(['commonConfigProvider', function (cfg) {
        cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
        cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
    }]);
    //#endregion

    app.config(['blockUIConfig', function (blockUIConfig) {

        blockUIConfig.autoBlock = false;
        blockUIConfig.blockBrowserNavigation = false;

    }]);

})();