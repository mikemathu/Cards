    $(document).ready(function () {

        function ReloadPage() {
            LoadRoles()
        }

        $("#createnewrole").click(function () {
            $("#UserRoleForm")[0].reset();
            $("#btnaddrole").show();
            $("#updaterole").hide();
        });

        function LoadRoles() {
            AjaxServerCallAsync("GET", "/Security/GetAllRoles/", "", "", function (response) {
                var rolesData = response.response;

                if (rolesTable) {
                    rolesTable.clear().draw();

                    if (response.status && !$.isEmptyObject(rolesData)) {
                        var rolesHTML = "";

                        for (var i = 0; i < rolesData.length; i++) {
                            if (rolesData[i].roleName !== "Super Admininstrator") {
                                var roleDescription = rolesData[i].description === null ? "" : rolesData[i].description;
                                rolesHTML += '<tr id="' + rolesData[i].roleID + '"><td data-title="RoleID">' + rolesData[i].roleID + '<\/td><td data-title="Name">' + rolesData[i].roleName + '<\/td> <td data-title="Description">' + roleDescription + "<\/td><\/tr>";
                            }
                        }

                        rolesTable.rows.add($(rolesHTML)).draw(false);
                    }
                }
            });
        }

        function DeleteRole(roleId) {
            Noty.closeAll();
            var token = $("#UserRoleForm input[name=__RequestVerificationToken]").val();

            AjaxServerCallAsync("POST", "/Security/DeleteRole/", roleId, token, function (response) {
                var responseData = response.response;

                if (response.status) {
                    var roleElement = document.getElementById(roleId);
                    roleElement.parentNode.removeChild(roleElement);
                    Notify(true, "Role Deleted Successfully");
                } else {
                    Notify(false, responseData);
                }
            });
        }

        function GetRoleDetails(roleId) {
            var token = $("#InventoryItemForm input[name=__RequestVerificationToken]").val();

            AjaxServerCallAsync("POST", "/Security/GetRoleDetails/", roleId, token, function (response) {
                var roleDetails = response.response;

                if (response.status) {
                    $("#btnCreateNewProduct").click();
                    $("#RoleID").val(roleDetails.roleID);
                    $("#Description").val(roleDetails.description);
                    $("#Name").val(roleDetails.roleName);
                    $("#btnaddrole").hide();
                    $("#updaterole").show();
                } else {
                    Notify(false, roleDetails);
                }
            });
        }

        var rolesTable = $("#rolestable").DataTable({
            drawCallback: function () {
                $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
                $.contextMenu({
                    selector: "#rolestable tbody tr td",
                    trigger: "right",
                    delay: 500,
                    autoHide: !0,
                    callback: function (n, t) {
                        var i = t.$trigger[0].parentElement.id, r;
                        switch (n) {
                            case "select":
                                GetRoleDetails(i);
                                break;
                            case "delete":
                                r = confirm("Are You Sure You Want To Delete The Selected Role?");
                                r === !0 && DeleteRole(i)
                        }
                    },
                    items: {
                        select: {
                            name: "Select"
                        },
                        "delete": {
                            name: "Delete"
                        }
                    }
                })
            },
            paging: !1,
            searching: !0,
            ordering: !0,
            bInfo: !1,
            scrollY: "250px",
            sScrollX: "100%",
            scrollX: !0
        });

        rolesTable.clear().draw();
        ReloadPage()

        $("#UserRoleForm").submit(function (event) {
            event.preventDefault();

            var roleId = Number($("#RoleID").val());
            var buttonId = roleId > 0 ? "#btnupdaterole" : "#btnaddrole";
            var laddaButton = Ladda.create(document.querySelector(buttonId));

            laddaButton.start();
            laddaButton.isLoading();
            laddaButton.setProgress(-1);

            var roleData = {
                RoleID: $("#RoleID").val() || 0,
                RoleName: $("#Name").val(),
                Description: $("#Description").val(),
            };
            var token = $("#UserRoleForm input[name=__RequestVerificationToken]").val();

            AjaxServerCallAsync("POST", "/Security/CreateUpdateRole/", roleData, token, function (response) {
                var roleResponse = response.response;
                var roleDescription = roleResponse.description === null ? "" : roleResponse.description;

                if (response.status) {
                    if (roleId > 0) {
                        var updatedRoleHTML = '<tr id="' + roleResponse.roleID + '"><td data-title="RoleID">' + roleResponse.roleID + '<\/td><td data-title="Name">' + roleResponse.roleName + '<\/td><td data-title="Description">' + roleDescription + "<\/td><\/tr>";
                        $("#" + roleId).replaceWith(updatedRoleHTML);
                        $("#UserRoleForm")[0].reset();
                        Notify(true, "Role Updated Successfully");
                    } else {
                        var newRoleHTML = '<tr id="' + roleResponse.roleID + '"><td data-title="RoleID">' + roleResponse.roleID + '<\/td><td data-title="Name">' + roleResponse.roleName + '<\/td><td data-title="Description">' + roleDescription + "<\/td><\/tr>";
                        rolesTable.row.add($(newRoleHTML)).draw(false);
                        $("#UserRoleForm")[0].reset();
                        Notify(true, "Role Created Successfully");
                    }
                    laddaButton.stop();
                } else {
                    Notify(false, roleResponse);
                    laddaButton.stop();
                }
            });
        });


        $(function () {
            $.contextMenu({
                selector: '#body',
                trigger: 'right',
                autoHide: true,
                zIndex: 9999,
                reposition: false,
                callback: function (key, options) {
                    switch (key) {
                        case 'refresh':
                            if (typeof ReloadPage === "function") {
                                ReloadPage();
                            }
                            break;
                    }
                },
                items: {
                    "refresh": {
                        name: "Refresh",
                        icon: "fas fa-sync-alt"
                    }
                }
            });
        });


});