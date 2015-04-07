irl.core.namespace("admin");

irl.admin = function () {
    var that = {};

    var constructor = function () {
        $("#admin-tabs").tabs();

        $("#dtUser").dataTableCrud(
        {
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",
            "bProcessing": true,
            //"bServerSide": true,
            "sAjaxSource": "/Admin/UserListJson",
            "sAjaxDataProp": "DataList",
            "aoColumns": [
                { "sTitle": "Id", "mDataProp": "Id", "bVisible": false },
                { "sTitle": "AD Name", "mDataProp": "ADName" },
                { "sTitle": "Display Name", "mDataProp": "DisplayName"}]
        },
        {
            sAddFormURL: "/Admin/AddUserPartial",
            sAddFormId: "frmAddUser",
            sAddNewRowButtonId: "btnAddUser",
            sAddNewRowOkButtonId: "btnAddUserFormSubmit",
            sAddNewRowCancelButtonId: "btnAddUserFormCancel",
            fnOnAdded: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while adding a new user.");
                    return;
                }

                irl.system.logger.log("User added with success.");
            },
            sDeleteURL: "/Admin/DeleteUserProccess",
            sDeleteRowButtonId: "btnRemoveUser",
            fnOnDeleted: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while deleting the user.");
                    return;
                }

                irl.system.logger.log("User deleted with success.");
            },
            sUpdateFormURL: "/Admin/UpdateUserPartial",
            sUpdateFormId: "frmUpdateUser",
            sUpdateRowButtonId: "btnEditUser",
            sUpdateRowOkButtonId: "btnUpdateUserFormSubmit",
            sUpdateRowCancelButtonId: "btnUpdateUserFormCancel",
            fnOnUpdated: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while updating the user.");
                    return;
                }

                irl.system.logger.log("User updated with success.");
            },
            oKeyTable: "Id"
        });

        $("#dtRole").dataTableCrud(
        {
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",
            "bProcessing": true,
            //"bServerSide": true,
            "sAjaxSource": "/Admin/RoleListJson",
            "sAjaxDataProp": "DataList",
            "aoColumns": [
                { "sTitle": "Id", "mDataProp": "Id", "bVisible": false },
                { "sTitle": "Name", "mDataProp": "Name"}]
        },
        {
            sAddFormURL: "/Admin/AddRolePartial",
            sAddFormId: "frmAddRole",
            sAddNewRowButtonId: "btnAddRole",
            sAddNewRowOkButtonId: "btnAddRoleFormSubmit",
            sAddNewRowCancelButtonId: "btnAddRoleFormCancel",
            fnOnAdded: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while adding a new role.");
                    return;
                }

                irl.system.logger.log("Role added with success.");
            },
            sDeleteURL: "/Admin/DeleteRoleProccess",
            sDeleteRowButtonId: "btnRemoveRole",
            fnOnDeleted: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while deleting the role.");
                    return;
                }

                irl.system.logger.log("Role deleted with success.");
            },
            sUpdateFormURL: "/Admin/UpdateRolePartial",
            sUpdateFormId: "frmUpdateRole",
            sUpdateRowButtonId: "btnEditRole",
            sUpdateRowOkButtonId: "btnUpdateRoleFormSubmit",
            sUpdateRowCancelButtonId: "btnUpdateRoleFormCancel",
            fnOnUpdated: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while updating the role.");
                    return;
                }

                irl.system.logger.log("Role updated with success.");
            },
            oKeyTable: "Id"
        });

        $("#dtUserRole").dataTableCrud(
        {
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",
            "bProcessing": true,
            //"bServerSide": true,
            "sAjaxSource": "/Admin/UserRoleListJson",
            "sAjaxDataProp": "DataList",
            "aoColumns": [
                { "sTitle": "Id", "mDataProp": "Id", "bVisible": false },
                { "sTitle": "User", "mDataProp": "User.ADName" },
                { "sTitle": "Role", "mDataProp": "Role.Name"}]
        },
        {
            sAddFormURL: "/Admin/AddUserRolePartial",
            sAddFormId: "frmAddUserRole",
            sAddNewRowButtonId: "btnAddUserRole",
            sAddNewRowOkButtonId: "btnAddUserRoleFormSubmit",
            sAddNewRowCancelButtonId: "btnAddUserRoleFormCancel",
            fnOnAdded: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while adding a new user role.");
                    return;
                }

                irl.system.logger.log("User role added with success.");
            },
            sDeleteURL: "/Admin/DeleteUserRoleProccess",
            sDeleteRowButtonId: "btnRemoveUserRole",
            fnOnDeleted: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while deleting the user role.");
                    return;
                }

                irl.system.logger.log("User role deleted with success.");
            },
            sUpdateFormURL: "/Admin/UpdateUserRolePartial",
            sUpdateFormId: "frmUpdateUserRole",
            sUpdateRowButtonId: "btnEditUserRole",
            sUpdateRowOkButtonId: "btnUpdateUserRoleFormSubmit",
            sUpdateRowCancelButtonId: "btnUpdateUserRoleFormCancel",
            fnOnUpdated: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while updating the user role.");
                    return;
                }

                irl.system.logger.log("User role updated with success.");
            },
            oKeyTable: "Id"
        });

        $("#dtWebsitePermission").dataTableCrud(
        {
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",
            "bProcessing": true,
            //"bServerSide": true,
            "sAjaxSource": "/Admin/WebsitePermissionListJson",
            "sAjaxDataProp": "DataList",
            "aoColumns": [
                { "sTitle": "Id", "mDataProp": "Id", "bVisible": false },
                { "sTitle": "Role", "mDataProp": "Role.Name" },
                { "sTitle": "Controller", "mDataProp": "Controller" },
                { "sTitle": "Action", "mDataProp": "Action" },
                { "sTitle": "Enabled", "mDataProp": "Enabled" },
                { "sTitle": "Comments", "mDataProp": "Comments"}]
        },
        {
            sAddFormURL: "/Admin/AddWebsitePermissionPartial",
            sAddFormId: "frmAddWebsitePermission",
            sAddNewRowButtonId: "btnAddWebsitePermission",
            sAddNewRowOkButtonId: "btnAddWebsitePermissionFormSubmit",
            sAddNewRowCancelButtonId: "btnAddWebsitePermissionFormCancel",
            fnOnAdded: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while adding a new website permission.");
                    return;
                }

                irl.system.logger.log("Website permission added with success.");
            },
            fnAfterShowAddModal: function () {
                initActionResultDdl();
            },
            sDeleteURL: "/Admin/DeleteWebsitePermissionProccess",
            sDeleteRowButtonId: "btnRemoveWebsitePermission",
            fnOnDeleted: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while deleting the website permission.");
                    return;
                }

                irl.system.logger.log("Website permission deleted with success.");
            },
            sUpdateFormURL: "/Admin/UpdateWebsitePermissionPartial",
            sUpdateFormId: "frmUpdateWebsitePermission",
            sUpdateRowButtonId: "btnEditWebsitePermission",
            sUpdateRowOkButtonId: "btnUpdateWebsitePermissionFormSubmit",
            sUpdateRowCancelButtonId: "btnUpdateWebsitePermissionFormCancel",
            fnOnUpdated: function (result) {
                if (result === "error") {
                    irl.system.logger.logError("An error occured while updating the website permission.");
                    return;
                }

                irl.system.logger.log("Website permission updated with success.");
            },
            fnAfterShowUpdateModal: function () {
                initActionResultDdl(function () {
                    $(".cbActionResultSelector").val($(".cbActionResultValue").text());
                });
            },
            oKeyTable: "Id"
        });

        $(document.body).on("change", ".cbControllerSelector", function () {
            var $this = $(this);

            emptyDropDownList(".cbActionResultSelector");
            populateActionResultDdl($this.val());
        });
    };

    var initActionResultDdl = function (callback) {
        emptyDropDownList(".cbActionResultSelector");
        populateActionResultDdl($(".cbControllerSelector").val(), callback);
    };

    var emptyDropDownList = function (selectorDdl) {
        $(selectorDdl).empty();
    };

    var populateActionResultDdl = function (val, callback) {
        var $destDdl = $(".cbActionResultSelector");
        $.getJSON("/Admin/ActionResultListJson", { ctl: val }, function (json) {
            $.each(json.DataList, function (i, el) {
                $destDdl.append(
                        $('<option></option>').val(el).html(el)
                    );
            });

            if ($.isFunction(callback)) {
                callback();
            }
        });
    };

    constructor();

    return that;
};

$(function () {
    irl.ui.admin = irl.admin();
});