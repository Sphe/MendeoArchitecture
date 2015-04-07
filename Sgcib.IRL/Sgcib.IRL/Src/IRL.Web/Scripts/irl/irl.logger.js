irl.core.namespace("logger");

irl.logger = function () {
    var that = {};

    var constructor = function () {
        toastr.options.positionClass = 'toast-bottom-right';
        toastr.options.backgroundpositionClass = 'toast-bottom-right';
    };

    var log = function (message) {
        logIt(message, 'info');
    };

    var logError = function (message) {
        logIt(message, 'error');
    };

    var logIt = function (message, toastType) {
        if (toastType === 'error') {
            toastr.error(message);
        } else {
            toastr.info(message);
        }
    };

    constructor();

    that.log = log;
    that.logError = logError;

    return that;
};

$(function () {
    irl.system.logger = irl.logger();
});