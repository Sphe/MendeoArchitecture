irl.core.namespace("consultation");

irl.consultation = function () {
    var that = {};
    var consultationTabs;

    var constructor = function () {
        $('.btnCheckAll').on('click', function () {
            selectAllMetadata();

            return false;
        });

        $('.btnRemoveAll').on('click', function () {
            removeAllMetadata();

            return false;
        });

        $(".lstCbMetadata").find(':checkbox').on('change', function () {
            renderSelectedMetadata();
        });

        consultationTabs = $("#consultation-tabs").tabs({
            beforeActivate: function (event, ui) {
                var returnValue = false;
                if (ui.newPanel.prop("id") === "tab-result") {

                    var dLinqFilter = $(".sortable").querybuilder('renderQueryToDynamicLinq');
                    $.ajax({
                        type: "POST",
                        url: "/Consultation/ConsultationResultPartial",
                        data: {
                            metadataToShow: retrieveSelectedMetadataArray(),
                            filterString: dLinqFilter.queryString,
                            filterArgumentValues: explodeInStringArray(dLinqFilter.argumentList, "dataValue"),
                            filterArgumentDataTypes: explodeInStringArray(dLinqFilter.argumentList, "dataType")
                        },
                        traditional: true,
                        async: false,
                        cache: false,
                        success: function (html) {
                            ui.newPanel.find(".containerConsultationGrid").html(html);
                            irl.system.logger.log("Request executed with success.");
                            returnValue = true;
                        },
                        error: function (err) {
                            irl.system.logger.logError("An error occured while proccessing the request.");
                            returnValue = false;
                        }
                    });
                } else {
                    returnValue = true;
                }

                return returnValue;
            },
            activate: function (event, ui) {
                if (typeof gvConsultationResultMain !== 'undefined') {
                    gvConsultationResultMain.AdjustControl();
                }
            }
        });

        $(".sortable").querybuilder({
            placeholder: 'ui-placeholder',
            forcePlaceholderSize: true,
            handle: 'div',
            helper: 'clone',
            items: 'li',
            //tolerance: 'pointer',
            revert: 250,
            tabSize: 25,

            urlMetadataAccessor: "/Consultation/MetadataAccessor",
            textAreaRenderFilters: ".txtRenderFilterQuery"
        });

        $(".btnQueryBuilderAddAnd").on('click', function () {
            $(".sortable").querybuilder('addLogicalOperator', {
                logicalOperator: "AND"
            });

            return false;
        });

        $(".btnQueryBuilderAddOr").on('click', function () {
            $(".sortable").querybuilder('addLogicalOperator', {
                logicalOperator: "OR"
            });

            return false;
        });

        $(".btnQueryBuilderAddExpression").on('click', function () {
            $(".sortable").querybuilder('addFilterExpression');

            return false;
        });

        $(".btnQueryBuilderVisualize").on('click', function () {
            $(".sortable").querybuilder('renderQueryToTextArea');

            return false;
        });

        $(".txtRenderFilterQuery").focus(function () {
            $(".sortable").querybuilder('renderQueryToTextArea');

            return false;
        });
    };

    var explodeInStringArray = function (arliteral, prop) {
        var data = [];

        $.each(arliteral, function (i, el) {
            data.push(el[prop]);
        });

        return data;
    };

    var selectAllMetadata = function () {
        $(".lstCbMetadata").find(':checkbox').prop('checked', true);
        renderSelectedMetadata();
    };

    var removeAllMetadata = function () {
        $(".lstCbMetadata").find(':checkbox').prop('checked', false);
        renderSelectedMetadata();
    };

    var renderSelectedMetadata = function () {
        $lstSelectedMetadata = $(".lstSelectedMetadata");
        $lstSelectedMetadata.html("");

        $(".lstCbMetadata").find(':checkbox').each(function () {
            var $this = $(this);
            if ($this.prop('checked') === true) {
                $lstSelectedMetadata.append($("<li>" + $this.data("name") + "</li>"));
            }
        });
    };

    var retrieveSelectedMetadataArray = function () {
        var selectedMetadataArray = [];

        $(".lstCbMetadata").find(':checkbox').each(function () {
            var $this = $(this);
            if ($this.prop('checked') === true) {
                selectedMetadataArray.push($this.data("name"));
            }
        });

        return selectedMetadataArray;
    };

    constructor();

    return that;
};

$(function () {
    irl.ui.consultation = irl.consultation();
});