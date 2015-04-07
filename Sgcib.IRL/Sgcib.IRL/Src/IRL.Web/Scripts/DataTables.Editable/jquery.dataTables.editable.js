/**
* PLUGIN_NAME
* Version: VERSION
* URL: URL
* Description: DESCRIPTION
* Requires: JQUERY_VERSION, OTHER_PLUGIN(S), ETC.
* Author: AUTHOR (AUTHOR_URL)
* Copyright: Copyright 2013 YOUR_NAME
* License: LICENSE_INFO
*/

// Plugin closure wrapper
// Uses dollar, but calls jQuery to prevent conflicts with other libraries
// Semicolon to prevent breakage with concatenation
// Pass in window as local variable for efficiency (could do same for document)
// Pass in undefined to prevent mutation in ES3
;(function($, document, window, undefined) {
    // Optional, but considered best practice by some
    "use strict";

    // Name the plugin so it's only in one place
    var pluginName = 'dataTableCrud';

    // Plugin constructor
    // This is the boilerplate to set up the plugin to keep our actual logic in one place
    var coreplugin = function (element, dtOptions, crudOptions) {
        
        var that = {};
        var elmt;
        var options;
        var name;
        var oTable;
        var oSettings;
        var nSelectedRow, nSelectedCell;
        var oKeyTablePosition;
        var modalController;
        var oAddNewRowButton, oUpdateRowButton, oDeleteRowButton;
        var iDisplayStart = 0;

        // Private methods

        var _fnGetRowID = function (cell) {
            var position = oTable.fnGetPosition($(cell.parentNode)[0]);
            var Id = oTable.fnGetData(position)[options.oKeyTable];
            return Id;
        };

        var fnDisableUpdateButton = function () {
            oUpdateRowButton.attr("disabled", "true");
        };

        var fnEnableUpdateButton = function () {
            oUpdateRowButton.removeAttr("disabled");
        };

        var fnDisableDeleteButton = function () {
            oDeleteRowButton.attr("disabled", "true");
        };

        var fnEnableDeleteButton = function () {
            oDeleteRowButton.removeAttr("disabled");
        };

        var _fnOnRowDelete = function (event) {
            event.preventDefault();
            event.stopPropagation();

            iDisplayStart = fnGetDisplayStart();

            nSelectedRow = null;
            nSelectedCell = $('tr.' + options.sSelectedRowClass + ' td', oTable)[0];

            if (nSelectedCell == null) {
                fnDisableDeleteButton();
                return;
            }

            var id = options.fnGetRowID(nSelectedCell);
            var jSelectedRow = $(nSelectedCell).parent("tr");
            nSelectedRow = jSelectedRow[0];
            if (options.fnOnDeleting(jSelectedRow, id, fnDeleteRow)) {
                fnDeleteRow(id);
            }
        };

        var _fnOnDeleting = function (tr, id, fnDeleteRow) {
            return confirm("Are you sure that you want to delete this record?"); ;
        };

        var fnDeleteRow = function (id, sDeleteURL) {           
            var sURL = sDeleteURL;
            if (sDeleteURL == null) {
                sURL = options.sDeleteURL;
            }

            options.fnStartProcessingMode();
            var data = $.extend(options.oDeleteParameters, { "id": id });
            $.ajax({
                url: sURL,
                type: options.sDeleteHttpMethod,
                data: data,
                success: fnOnRowDeleted,
                dataType: options.sDeleteDataType,
                cache: false,
                error: function (response) {
                    options.fnEndProcessingMode();
                    options.fnShowError(response.responseText, "delete");
                    options.fnOnDeleted("error");
                }
            });
        };

        var fnOnRowDeleted = function (response) {
            options.fnEndProcessingMode();
            var oTRSelected = nSelectedRow;

            if (response.Response == options.sSuccessResponse || response == "") {
                oTable.fnReloadAjax();
                fnDisableDeleteButton();
                fnDisableUpdateButton();
                options.fnOnDeleted("success");
            }
            else {
                options.fnShowError(response, "delete");
                options.fnOnDeleted("error");
            }
        };

        var fnGetDisplayStart = function () {
            return oSettings._iDisplayStart;
        };

        var fnSetDisplayStart = function () {
            oSettings._iDisplayStart = iDisplayStart;
            oSettings.oApi._fnCalculateEnd(oSettings);
            oSettings.oApi._fnDraw(oSettings);
        };

        var _fnOnDeleted = function (result) {
        };

        var _fnOnAdded = function (result) {
        };

        var _fnOnUpdated = function (result) {
        };

        var _fnStartProcessingMode = function () {
            if (oTable.fnSettings().oFeatures.bProcessing) {
                $(".dataTables_processing").css('visibility', 'visible');
            }
        };

        var _fnEndProcessingMode = function () {
            if (oTable.fnSettings().oFeatures.bProcessing) {
                $(".dataTables_processing").css('visibility', 'hidden');
            }
        };

        var _fnShowError = function (errorText, action) {
            alert(errorText);
        };

        var fnAfterShowAddModal = function() {
        };

        var fnAfterShowUpdateModal = function() {
        };

        // Default options

        var _defaults = {
            sUpdateURL: "UpdateData",
            sUpdateFormURL: "UpdateForm",
            sUpdateFormId: "UpdateFormId",
            sUpdateRowButtonId: "btnUpdateRow",
            sUpdateRowOkButtonId: "btnUpdateRowOk",
            sUpdateRowCancelButtonId: "btnUpdateRowCancel",
            fnOnUpdated: _fnOnUpdated,
            fnAfterShowUpdateModal: fnAfterShowUpdateModal,

            sAddURL: "AddData",
            sAddFormURL: "AddForm",
            sAddFormId: "AddFormId",
            sAddNewRowButtonId: "btnAddNewRow",
            sAddNewRowOkButtonId: "btnAddNewRowOk",
            sAddNewRowCancelButtonId: "btnAddNewRowCancel",
            fnOnAdded: _fnOnAdded,
            fnAfterShowAddModal: fnAfterShowAddModal,

            sDeleteURL: "DeleteData",
            sDeleteRowButtonId: "btnDeleteRow",
            fnOnDeleting: _fnOnDeleting,
            fnOnDeleted: _fnOnDeleted,
            sDeleteHttpMethod: "POST",
            oDeleteParameters: {},

            sSelectedRowClass: "row_selected",
            fnGetRowID: _fnGetRowID,

            fnStartProcessingMode: _fnStartProcessingMode,
            fnEndProcessingMode: _fnEndProcessingMode,
            fnShowError: _fnShowError,

            sSuccessResponse: "success",

            oKeyTable: null
        };

        // Init method

        var init = function() {

            oDeleteRowButton = $('#' + options.sDeleteRowButtonId);
            if (oDeleteRowButton.length != 0) {
                if (oDeleteRowButton.data("delete-event-attached") != "true") {
                    oDeleteRowButton.click(_fnOnRowDelete);
                    oDeleteRowButton.data("delete-event-attached", "true");
                }
            }
            else {
                oDeleteRowButton = null;
            }

            // Handling Create Process

            oAddNewRowButton = $("#" + options.sAddNewRowButtonId);
            if (oAddNewRowButton.length != 0) {

                if (oAddNewRowButton.data("add-event-attached") != "true") {
                    oAddNewRowButton.click(function (e) {
                        e.preventDefault();
                        e.stopPropagation();

                        var request = $.ajax({
                            url: options.sAddFormURL,
                            type: "GET",
                            dataType: "html",
                            cache: false,
                            success: function (html) {
                                modalController.show(html);
                                $.validator.unobtrusive.parse($("#" + options.sAddFormId));
                                options.fnAfterShowAddModal();
                            },
                            error: options.fnShowError
                        });
                    });
                    oAddNewRowButton.data("add-event-attached", "true");
                }

                $(document.body).on("click", "#" + options.sAddNewRowOkButtonId, function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    var opt = {
                        beforeSubmit: function (arr, $form, opts) {
                            options.fnStartProcessingMode();
                        },
                        success: function (result) {
                            modalController.hide();
                            options.fnEndProcessingMode();
                            oTable.fnReloadAjax();
                            fnDisableDeleteButton();
                            fnDisableUpdateButton();
                            options.fnOnAdded("success");
                        },
                        error: function (data) {
                            modalController.hide();
                            options.fnEndProcessingMode();
                            options.fnShowError(data.responseText);
                            options.fnOnAdded("error");
                        }
                    };

                    var $addForm = $("#" + options.sAddFormId);

                    if ($addForm.valid()) {
                        $addForm.ajaxSubmit(opt);
                    }
                });

                $(document.body).on("click", "#" + options.sAddNewRowCancelButtonId, function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    modalController.hide();
                });

            } else {
                oAddNewRowButton = null;
            }

            // Handling Update Process

            oUpdateRowButton = $("#" + options.sUpdateRowButtonId);
            if (oUpdateRowButton.length != 0) {

                if (oUpdateRowButton.data("add-event-attached") != "true") {
                    oUpdateRowButton.click(function (e) {
                        e.preventDefault();
                        e.stopPropagation();

                        nSelectedRow = null;
                        nSelectedCell = $('tr.' + options.sSelectedRowClass + ' td', oTable)[0];

                        if (nSelectedCell == null) {
                            fnDisableUpdateButton();
                            return;
                        }

                        var id = options.fnGetRowID(nSelectedCell);

                        var request = $.ajax({
                            url: options.sUpdateFormURL,
                            type: "GET",
                            data: { "id": id },
                            dataType: "html",
                            cache: false,
                            success: function (html) {
                                modalController.show(html);
                                $.validator.unobtrusive.parse($("#" + options.sUpdateFormId));
                                options.fnAfterShowUpdateModal();
                            },
                            error: options.fnShowError
                        });
                    });
                    oUpdateRowButton.data("add-event-attached", "true");
                }

                $(document.body).on("click", "#" + options.sUpdateRowOkButtonId, function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    var opt = {
                        beforeSubmit: function (arr, $form, opts) {
                            options.fnStartProcessingMode();
                        },
                        success: function (result) {
                            modalController.hide();
                            options.fnEndProcessingMode();
                            oTable.fnReloadAjax(); 
                            fnDisableDeleteButton();
                            fnDisableUpdateButton();
                            options.fnOnUpdated("success");
                        },
                        error: function (data) {
                            modalController.hide();
                            options.fnEndProcessingMode();
                            options.fnShowError(data.responseText);
                            options.fnOnUpdated("error");
                        }
                    };

                    var $updateForm = $("#" + options.sUpdateFormId);

                    if ($updateForm.valid()) {
                        $updateForm.ajaxSubmit(opt);
                    }
                });

                $(document.body).on("click", "#" + options.sUpdateRowCancelButtonId, function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    modalController.hide();
                });

            } else {
                oUpdateRowButton = null;
            }

            // Handling Selected Row With Disabling/Enabling Delete Button

            $("tbody", oTable).click(function (event) {
                if ($(event.target.parentNode).hasClass(options.sSelectedRowClass)) {
                    $(event.target.parentNode).removeClass(options.sSelectedRowClass);
                    if (oDeleteRowButton != null) {
                        fnDisableDeleteButton();
                    }

                    if (oUpdateRowButton != null) {
                        fnDisableUpdateButton();
                    }
                } else {
                    $(oTable.fnSettings().aoData).each(function () {
                        $(this.nTr).removeClass(options.sSelectedRowClass);
                    });
                    $(event.target.parentNode).addClass(options.sSelectedRowClass);
                    if (oDeleteRowButton != null) {
                        fnEnableDeleteButton();
                    }

                    if (oUpdateRowButton != null) {
                        fnEnableUpdateButton();
                    }
                }
            });

            fnDisableDeleteButton();
            fnDisableUpdateButton();
        };

        // Constructor

        var constructor = function() {
            elmt = element;

            options = $.extend( {}, _defaults, crudOptions );

            name = pluginName;

            oTable = $(element).dataTable(dtOptions);
            oSettings = oTable.fnSettings();
            options.bUseKeyTable = (options.oKeyTable != null);

            modalController = modal();

            init();      
        };

        constructor();

        return that;
    };

    $.fn[pluginName] = function(dtOptions, options) {
        // Iterate through each DOM element and return it
        return this.each(function() {
            // prevent multiple instantiations
            if (!$.data(this, 'plugin_' + pluginName)) {
                $.data(this, 'plugin_' + pluginName, coreplugin(this, dtOptions, options));
            }
        });
    };

    var modal = function() {
        var that = {};
        var _modal = undefined;

        that.show = function (html) {
            _modal = $.modal(html);
        };

        that.hide = function () {
            if (_modal !== undefined) {
                _modal.close();
            }
        };

        return that;
    };

})(jQuery, document, window);
