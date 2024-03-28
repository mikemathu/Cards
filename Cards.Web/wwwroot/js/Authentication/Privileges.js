
    $(document).ready(function () {

        function ReloadPage() {
            LoadAllPrivileges();
            GetRoles();
        }

        function updateDataTableSelectAllCtrl(dataTable) {
            var dataTableNode = dataTable.table().node();
            var allCheckboxes = $('tbody input[type="checkbox"]', dataTableNode);
            var checkedCheckboxes = $('tbody input[type="checkbox"]:checked', dataTableNode);
            var selectAllCheckbox = $('thead input[name="select_all"]', dataTableNode).get(0);

            if (checkedCheckboxes.length === 0) {
                selectAllCheckbox.checked = false;
                if ("indeterminate" in selectAllCheckbox) {
                    $(".selectAll").prop("indeterminate", false);
                    $(".selectAll").prop("checked", false);
                }
            } else if (checkedCheckboxes.length === allCheckboxes.length) {
                selectAllCheckbox.checked = true;
                if ("indeterminate" in selectAllCheckbox) {
                    $(".selectAll").prop("indeterminate", false);
                    $(".selectAll").prop("checked", true);
                }
            } else {
                selectAllCheckbox.checked = true;
                if ("indeterminate" in selectAllCheckbox) {
                    $(".selectAll").prop("indeterminate", true);
                }
            }
        }

        function updateDataTable2SelectAllCtrl(dataTable) {
            var tableNode = dataTable.table().node();
            var allCheckboxes = $('tbody input[type="checkbox"]', tableNode);
            var checkedCheckboxes = $('tbody input[type="checkbox"]:checked', tableNode);
            var selectAllCheckbox = $('thead input[name="select_all"]', tableNode).get(0);

            if (checkedCheckboxes.length === 0) {
                selectAllCheckbox.checked = false;
                if ("indeterminate" in selectAllCheckbox) {
                    $(".selectAll2").prop("indeterminate", false);
                    $(".selectAll2").prop("checked", false);
                }
            } else if (checkedCheckboxes.length === allCheckboxes.length) {
                selectAllCheckbox.checked = true;
                if ("indeterminate" in selectAllCheckbox) {
                    $(".selectAll2").prop("indeterminate", false);
                    $(".selectAll2").prop("checked", true);
                }
            } else {
                selectAllCheckbox.checked = true;
                if ("indeterminate" in selectAllCheckbox) {
                    $(".selectAll2").prop("indeterminate", true);
                }
            }
        }

        function GetRoles() {
                AjaxServerCallAsync("GET", "/Security/GetAllRoles/", "", "", function (response) {
                    var rolesResponse = response.response;
                    var selectRoleElement, optionElement, fromRoleElement, toRoleElement;

                   

                        if (response.status && !$.isEmptyObject(rolesResponse)) {
                            selectRoleElement = $(".select-role");
                            selectRoleElement.empty();
                            selectRoleElement.append('<option value="0">--Select a role here--</option>');

                            for (var i = 0; i < rolesResponse.length; i++) {
                                if (rolesResponse[i].roleName !== "Super Admininstrator") {
                                    optionElement = new Option(rolesResponse[i].roleName, rolesResponse[i].roleID);
                                    selectRoleElement.append(optionElement);
                                }
                            }

                            let selectOptions = $(".select-role option");
                            selectOptions.clone().appendTo("#fromRole");
                            selectOptions.clone().appendTo("#toRole");
                        }

                    
                });
        }

        function LoadAllPrivileges() {
            AjaxServerCallAsync("GET", "/Security/GetAllPrivileges/", "", "", function (response) {
                var privileges = response.response;

                if (table.clear().draw(), response.status && !$.isEmptyObject(privileges)) {
                    var privilegeRows = "";
                    for (var i = 0; i < privileges.length; i++) {
                        privilegeRows += '<tr role="row" class="odd" id="' + privileges[i].privilegeID + '"><td class="dt-body-center"><input type="checkbox" id="' + privileges[i].privilegeID + '"/></td><td data-title="ID">' + privileges[i].privilegeID + '</td><td data-title="Name">' + privileges[i].privilegeName + "</td></tr>";
                    }
                    table.rows.add($(privilegeRows)).draw(false);

                    if (FetchingRolePrivileges) {
                        FetchRolesPrivileges(roleID);
                    }
                }
            });
        }

        function GetRolePrivilege(elementId) {
            var targetElement = document.getElementById(elementId);
            var tableCells = targetElement.getElementsByTagName("td");
            var privilegeValue = tableCells[2].innerText;
            var windowWidth;

            $("#Name").val(privilegeValue);
            $("#btnCreateNew").show();
            $("#editprivilge").show();
            $("#PrivilegeID").val(elementId);
            $("#btnaddprivilege").hide();

            windowWidth = $(window).width();
            if (windowWidth <= 991) {
                goToByScroll("btn-horizontal");
            }
        }


        function DeleteRolePrivilege(privilegeId) {
            var token = $("#PrivilegeForm input[name=__RequestVerificationToken]").val();

            AjaxServerCallAsync("POST", "/Security/DeleteRolePrivilege/", privilegeId, token, function (response) {
                var responseData = response.response;
                if (response.status) {
                    var element = document.getElementById(privilegeId);
                    element.parentNode.removeChild(element);
                    Notify(!0, "Privilege Deleted Successfully");
                } else {
                    Notify(!1, responseData);
                }
            });
        }

        function FetchRolesPrivileges(roleId) {
            var role = roleId;
            AjaxServerCallAsync("POST", "/Security/GetRolePrivileges/", role, "", function (response) {
                var privileges = response.response;

                if (table2.clear().draw(), response.status && !$.isEmptyObject(privileges)) {
                    var selectedPrivileges = "";
                    var privilegeRows = "";

                    for (var i = 0; i < privileges.length; i++) {
                        selectedPrivileges += (i === privileges.length - 1) ? "#allprivilegestable #" + privileges[i].privilegeID : "#allprivilegestable #" + privileges[i].privilegeID + ",";

                        privilegeRows += '<tr role="row" class="odd" id="' + privileges[i].privilegeID + '"><td class="dt-body-center"><input type="checkbox"  id="' + privileges[i].privilegeID + '"/></td><td data-title="ID">' + privileges[i].privilegeID + '</td><td data-title="Name">' + privileges[i].privilegeName + "</td></tr>";
                    }

                    table2.rows.add($(privilegeRows)).draw(false);
                    table.rows($(selectedPrivileges)).remove().draw();
                }

                $("#loader").hide();
                FetchingRolePrivileges = false;
                roleID = 0;
            });
        }

        var table, table2, FetchingRolePrivileges, roleID;   

        $(document).ready(function () {
            table.clear().draw();
            table2.clear().draw();
            ReloadPage();
            $("#btnCreateNew").hide();
            $("#editprivilge").hide();
            $("#PrivilegeID").val("0")
        });

        table = $("#allprivilegestable").DataTable({
            drawCallback: function () {
                $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
                $.contextMenu({
                    selector: "#allprivilegestable tbody tr td",
                    trigger: "right",
                    delay: 500,
                    autoHide: !0,
                    callback: function (n, t) {
                        var i = t.$trigger[0].parentElement.id, r;
                        switch (n) {
                            case "edit":
                                GetRolePrivilege(i);
                                break;
                            case "delete":
                                r = confirm("Are You Sure You Want To Delete The Selected Privilege?");
                                r === !0 && DeleteRolePrivilege(i)
                        }
                    },
                    items: {
                     /*   edit: {
                            name: "Edit"
                        },*/
                        "delete": {
                            name: "Delete"
                        }
                    }
                })
            },
            columnDefs: [{
                targets: 0,
                searchable: !1,
                orderable: !1,
                width: "1%",
                className: "dt-body-center",
                render: function () {
                    return '<input type="checkbox">'
                }
            }],
            lengthChange: !1,
            buttons: ["excel", "csv", "pdf", "print"],
            paging: !1,
            searching: !0,
            ordering: !0,
            bInfo: !1,
            scrollY: "45vh",
            sScrollX: "100%",
            scrollX: !0
        });

        $("#allprivilegestable tbody").on("click", 'input[type="checkbox"]', function (event) {
            var clickedCheckbox = $(this);
            var closestTableRow = clickedCheckbox.closest("tr");

            if (this.checked) {
                closestTableRow.addClass("selected");
            } else {
                closestTableRow.removeClass("selected");
            }

            updateDataTableSelectAllCtrl(table);
            event.stopPropagation();
        });


        $("#allprivilegestable").on("click", "tbody td, thead th:first-child", function () {
            var clickedElement = $(this);
            var parentElement = clickedElement.parent();
            var checkboxInputs = parentElement.find('input[type="checkbox"]');

            checkboxInputs.trigger("click");
        });

        $('thead input[name="select_all"]', table.table().container()).on("click", function (event) {
            var selectAllCheckbox = this;
            var uncheckedCheckboxes = $('#allprivilegestable tbody input[type="checkbox"]:not(:checked)');
            var checkedCheckboxes = $('#allprivilegestable tbody input[type="checkbox"]:checked');

            if (selectAllCheckbox.checked) {
                uncheckedCheckboxes.trigger("click");
            } else {
                checkedCheckboxes.trigger("click");
            }

            event.stopPropagation();
        });

        table.on("draw", function () {
            updateDataTableSelectAllCtrl(table)
        });

        table2 = $("#roleprivilegesitems").DataTable({
            drawCallback: function () {
                $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
                $.contextMenu({
                    selector: "#roleprivilegesitems tbody tr td",
                    trigger: "right",
                    delay: 500,
                    autoHide: !0,
                    callback: function (n, t) {
                        var i = t.$trigger[0].parentElement.id;
                        switch (n) {
                            case "edit":
                                GetRolePrivilege(i)
                        }
                    },
                    items: {
                        edit: {
                            //name: "Edit"
                        }
                    }
                })
            },
            columnDefs: [{
                targets: 0,
                searchable: !1,
                orderable: !1,
                width: "1%",
                className: "dt-body-center",
                render: function () {
                    return '<input type="checkbox">'
                }
            }],
            lengthChange: !1,
            buttons: ["excel", "csv", "pdf", "print"],
            paging: !1,
            searching: !0,
            ordering: !0,
            bInfo: !1,
            scrollY: "45vh",
            sScrollX: "100%",
            scrollX: !0
        });

        $("#roleprivilegesitems tbody").on("click", 'input[type="checkbox"]', function (event) {
            var clickedCheckbox = $(this);
            var closestTableRow = clickedCheckbox.closest("tr");

            if (this.checked) {
                closestTableRow.addClass("selected");
            } else {
                closestTableRow.removeClass("selected");
            }

            updateDataTable2SelectAllCtrl(table2);
            event.stopPropagation();
        });

       
        $("#roleprivilegesitems").on("click", "tbody td, thead th:first-child", function () {
                var clickedElement = $(this);
                var parentElement = clickedElement.parent();
                var checkboxInputs = parentElement.find('input[type="checkbox"]');

                checkboxInputs.trigger("click");
         
        });



        $('thead input[name="select_all"]', table2.table().container()).on("click", function (event) {
            var selectAllCheckbox = this;
            var uncheckedCheckboxes = $('#roleprivilegesitems tbody input[type="checkbox"]:not(:checked)');
            var checkedCheckboxes = $('#roleprivilegesitems tbody input[type="checkbox"]:checked');

            if (selectAllCheckbox.checked) {
                uncheckedCheckboxes.trigger("click");
            } else {
                checkedCheckboxes.trigger("click");
            }

            event.stopPropagation();
        });

        $(window).resize(function () {
            var windowWidth = $(window).width();

            if (windowWidth <= 991) {
                $("#btn-horizontal").show();
                $("#btn-vertical").hide();
            } else {
                $("#btn-vertical").show();
                $("#btn-horizontal").hide();
            }
        });

        $("#PrivilegeForm").submit(function (event) {
            event.preventDefault();

            var privilegeID = $("#PrivilegeID").val();
            var buttonSelector = privilegeID > 0 ? "#editprivilge" : "#btnaddprivilege";
            var laddaButton = Ladda.create(document.querySelector(buttonSelector));

            laddaButton.start();
            laddaButton.isLoading();
            laddaButton.setProgress(-1);

            var privilegeData = {
                PrivilegeID: $("#PrivilegeID").val(),
                PrivilegeName: $("#Name").val()
            };

            var token = $("#PrivilegeForm input[name=__RequestVerificationToken]").val();

            AjaxServerCallAsync("POST", "/Security/CreateUpdatePrivilege/", privilegeData, token, function (response) {
                var responseData = response.response;

                if (response.status) {
                    if (privilegeID > 0) {
                        var elements = document.querySelectorAll("[id='" + privilegeID + "']");
                        for (var i = 0; i < elements.length; i++) {
                            var element = elements[i];
                            var tableData = element.getElementsByTagName("td");
                            tableData[2].innerText = responseData.privilegeName;
                        }
                        Notify(true, "Privilege updated Successfully");
                    } else {
                        var newRow = '<tr role="row" class="odd" id="' + responseData.privilegeID + '"><td class="dt-body-center"><input type="checkbox" id="' + responseData.privilegeID + '"/></td><td data-title="ID">' + responseData.privilegeID + '</td><td data-title="Name">' + responseData.privilegeName + "</td></tr>";
                        table.row.add($(newRow)).draw(false);
                        Notify(true, "Privilege added Successfully");
                        $("#btnCreateNew").click();
                    }
                    laddaButton.stop();
                } else {
                    laddaButton.stop();
                    Notify(false, responseData);
                }
            });
        });

        $("#btnCreateNew").click(function () {
            $("#PrivilegeID").val("0");
            $("#Name").val("");
            $("#btnCreateNew").hide();
            $("#editprivilge").hide();
            $("#btnaddprivilege").show()
        });

        FetchingRolePrivileges = false;
        roleID = 0;

        $(".select-role").change(function () {
            let selectedRoleValue = $(".select-role").val();
            table2.clear().draw();

            if (selectedRoleValue > 0) {
                $("#loader").show();
                FetchingRolePrivileges = true;
                roleID = selectedRoleValue;
                LoadAllPrivileges();
            } else {
                $("#loader").hide();
            }
        });


        $(".movetorole").click(function () {
                var laddaButton = Ladda.create(document.querySelector(".movetorole"));
                laddaButton.start();
                laddaButton.isLoading();
                laddaButton.setProgress(-1);

                var selectedPrivileges = [];
                $("#allprivilegestable input[type=checkbox]:checked").each(function () {
                    let privilegeId = $(this).closest("tr").attr("id");
                    if (privilegeId !== "") {
                        selectedPrivileges.push(privilegeId);
                    }
                });

                var roleId = $(".select-role").val();

                if (roleId === "" || selectedPrivileges.length <= 0) {
                    laddaButton.stop();
                    Notify(!1, "First select a role and/or permission(s)!!!");
                } else {
                    selectedPrivileges = selectedPrivileges.filter(function (id) {
                        return id !== undefined;
                    });

                    var data = {
                        SelectedPrivilegesIDs: selectedPrivileges,
                        RoleID: roleId
                    };

                    AjaxServerCallAsync("POST", "/Security/AddPrivilegesToRole/", data, "", function (response) {
                        var privileges = response.response;

                        if (response.status) {
                            if (!$.isEmptyObject(privileges)) {
                                var privilegeRows = "";
                                var selectedPrivilegeIds = "";

                                for (var i = 0; i < privileges.length; i++) {
                                    selectedPrivilegeIds += (i === privileges.length - 1) ? "#allprivilegestable #" + privileges[i].privilegeID : "#allprivilegestable #" + privileges[i].privilegeID + ",";

                                    privilegeRows += '<tr role="row" class="odd" id="' + privileges[i].privilegeID + '"><td class="dt-body-center"><input type="checkbox"  id="' + privileges[i].privilegeID + '"/></td><td data-title="ID">' + privileges[i].privilegeID + '</td><td data-title="Name">' + privileges[i].privilegeName + "</td></tr>";
                                }

                                if (privileges.length === 1) {
                                    table2.row.add($(privilegeRows)).draw(!1);
                                } else {
                                    table2.rows.add($(privilegeRows)).draw(!1);
                                }

                                table.rows($(selectedPrivilegeIds)).remove().draw();
                                laddaButton.stop();
                            }
                        } else {
                            laddaButton.stop();
                            Notify(!1, privileges);
                        }
                    });
                }
            });

        $(".movefromrole").click(function () {
            var laddaButton = Ladda.create(document.querySelector(".movefromrole"));
            var selectedPrivilegeIds = [];
            var roleID, response;

            laddaButton.start();
            laddaButton.isLoading();
            laddaButton.setProgress(-1);

            $("#roleprivilegesitems input[type=checkbox]:checked").each(function () {
                let privilegeId = $(this).closest("tr").attr("id");
                if (privilegeId !== "") {
                    selectedPrivilegeIds.push(privilegeId);
                }
            });

            roleID = $(".select-role").val();

            if (roleID === "" || selectedPrivilegeIds.length <= 0) {
                laddaButton.stop();
                Notify(!1, "First select a role and/or permission(s)!!!");
            } else {
                selectedPrivilegeIds = selectedPrivilegeIds.filter(function (id) {
                    return id !== undefined;
                });

                var requestData = {
                    SelectedPrivilegesIds: selectedPrivilegeIds,
                    RoleID: roleID
                };

                AjaxServerCallAsync("POST", "/Security/DeletePrivilegesFromRole/", requestData, "", function (response) {
                    var privilegesResponse = response.response;

                    if (response.status) {
                        if (!$.isEmptyObject(privilegesResponse)) {
                            var deletedPrivilegeIDs = "";
                            var removedRowsSelector = "";

                            for (var index = 0; index < privilegesResponse.length; index++) {
                                removedRowsSelector += (index === privilegesResponse.length - 1) ?
                                    "#roleprivilegesitems #" + privilegesResponse[index].privilegeID :
                                    "#roleprivilegesitems #" + privilegesResponse[index].privilegeID + ",";

                                deletedPrivilegeIDs += '<tr role="row" class="odd" id="' + privilegesResponse[index].privilegeID + '"><td class="dt-body-center"><input type="checkbox"  id="' + privilegesResponse[index].privilegeID + '"/></td><td data-title="ID">' + privilegesResponse[index].privilegeID + '</td><td data-title="Name">' + privilegesResponse[index].privilegeName + "</td></tr>";
                            }

                            if (privilegesResponse.length === 1) {
                                table.row.add($(deletedPrivilegeIDs)).draw(false);
                            } else {
                                table.rows.add($(deletedPrivilegeIDs)).draw(false);
                            }

                            table2.rows($(removedRowsSelector)).remove().draw();
                            laddaButton.stop();
                        }
                    } else {
                        laddaButton.stop();
                        Notify(!1, privilegesResponse);
                    }
                });
            }
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