irl.core.namespace("tdb");

irl.tdb = function () {
    var that = {};

    var constructor = function () {
        $(".tbDataPicker").datepicker();

        $(".tbSpinner").spinner();
    };

    constructor();

    return that;
};

$(function () {
    irl.ui.tdb = irl.tdb();
});
