    $(document).ready(function () {

        function ReloadPage() {
            LoadAllUsers();
            FetchRoles();
        } 

        function FetchRoles() {
            AjaxServerCallAsync("GET", "/Security/GetAllRoles/", "", "", function (response) {
                var rolesResponse = response.response;
                var selectRolesElement, optionElement;

                if (response.status && !$.isEmptyObject(rolesResponse)) {
                    selectRolesElement = $(".select-roles");
                    selectRolesElement.empty();
                    selectRolesElement.append('<option value="0"></option>');

                    for (var i = 0; i < rolesResponse.length; i++) {
                        optionElement = new Option(rolesResponse[i].roleName, rolesResponse[i].roleID);
                        selectRolesElement.append(optionElement);
                    }
                }
            });
        }

        function LoadAllUsers() {
            AjaxServerCallAsync("GET", "/Security/GetAllUsers/", "", "", function (response) {
                var userData = response.response;
                if (usersTable.clear().draw() && !$.isEmptyObject(userData)) {
                    var tableRows = "";
                    for (var i = 0; i < userData.length; i++) {
                        if (userData[i].userName !== "Admin" && userData[i].userName !== "Super Admin" ) {
                            var userId = userData[i].sysUserID;
                            var userName = userData[i].userName;
                            var surName = userData[i].surName === null ? "" : userData[i].surName;
                            var otherNames = userData[i].otherNames === null ? "" : userData[i].otherNames;                            
                            var createdDate = formatDate(userData[i].dateTimeCreated);
                            tableRows += '<tr data-uname="' + userName + '" id="' + userId + '">';
                            tableRows += '<td data-title="No">' + userId + '</td>';
                            tableRows += '<td data-title="UserName">' + userName + '</td>';
                            tableRows += '<td data-title="SurName">' + surName + '</td>';
                            tableRows += '<td data-title="OtherNames">' + otherNames + '</td>';
                            tableRows += '<td data-title="Created On">' + createdDate + '</td>';
                            tableRows += '</tr>';
                        }
                    }
                    usersTable.rows.add($(tableRows)).draw(false);
                }
            });
        }

         function GetUserDetails(userId) {
            var userDetails = userId;
            var requestVerificationToken = $("#SystemUserForm input[name=__RequestVerificationToken]").val();

             GetOrPostAsync("POST", "/Security/GetUserDetails/", userDetails, requestVerificationToken).then(response => {
                    $("#Password").attr("readonly", "readonly");
                    $("#ConfirmUserPassword").attr("readonly", "readonly");

                    var userID = response.sysUserID;
                    var surName = response.surName;
                    var otherNames = response.otherNames;
                    var userName = response.userName;
                    var userRolesIDs = response.userRolesIDs;
                    var dateTimeCreated = response.dateTimeCreated;
                    var password = response.password;

                    $("#SystemUserID").val(userID);
                    $("#Surname").val(surName);
                    $("#Othernames").val(otherNames);
                    $("#Username").val(userName);
                    $("#UserRolesIds").val(userRolesIDs);
                    $("#Password").val(password);
                    $("#ConfirmUserPassword").val(password);
                    $("#DateTimeCreated").val(dateTimeCreated);                  

                    $(".select-roles").select2();

                    $("#adduser").hide();
                    $("#update").show();
                })
                .catch(error => Notify(false, error));
        }

     
        $(".select-roles").select2({
            placeholder: "Select Roles"
        });

        
        const usersTable = $("#userstable").DataTable({
            drawCallback: function () {
                $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
                $.contextMenu({
                    selector: "#userstable tbody tr td",
                    trigger: "right",
                    delay: 500,
                    autoHide: !0,
                    callback: function (n, t) {
                        var i = t.$trigger[0].parentElement.id, r;
                        switch (n) {
                            case "select":
                                GetUserDetails(i);
                                break;
                            case "reset":
                                r = confirm("You are about to reset the password for user " + i + " The password will be reset to '123123'. Are you sure you want to continue?");
                                r === !0 && ResetUserPassword(i);
                                break;
                            case "lockout":
                                $("#ScheduleLockoutForm #SysUID").val(i);
                                $(".schedule-lockout-modal").modal("toggle");
                                break;
                            case "cancellockout":
                                CancelScheduledLockout(i);
                                break;
                            case "code":
                                $("#txtUName").text($("#userstable #" + i).data("uname"));
                                $("#AccessCodeForm #CodeSystemUID").val(i);
                                $(".access-code-modal").modal("toggle")
                        }
                    },
                    items: {
                        select: {
                            name: "Select"
                        }
                    }
                })
            },
            lengthChange: !1,
            buttons: ["excel", "csv", "pdf", "print"],
            paging: !1,
            searching: !0,
            ordering: !0,
            bInfo: !0,
            select: !0,
            scrollY: "30vh",
            sScrollX: "100%",
            scrollX: !0
        });

        usersTable.on("select", function (event, dataTable, type, indexes) {
            if (type === "row") {
                let selectedRowsData = usersTable.rows(indexes).data().toArray();
                let selectedRowId = selectedRowsData[0].DT_RowId;
                if (selectedRowId !== undefined) {
                    GetUserDetails(selectedRowId);
                }
            }
        });

        usersTable.clear().draw();
        $("#SystemUserID").val("0");
        ReloadPage();


        $('#SystemUserForm').submit(function (event) {
            event.preventDefault();

            var systemUserID = $('#SystemUserID').val();
            var addButton = $('#btnadduser');
            var laddaButton = Ladda.create(document.querySelector('#btnadduser'));

            if (systemUserID > 0) {
                addButton = $('#btnupdateuser');
                laddaButton = Ladda.create(document.querySelector('#btnupdateuser'));
            }

            laddaButton.start();
            laddaButton.isLoading();
            laddaButton.setProgress(-1);

            var password = $('#Password').val();
            var confirmPassword = $('#ConfirmUserPassword').val();

            if (password === confirmPassword) {
                var selectedUserRoleOptions = document.querySelectorAll('#UserRolesIds option:checked');
                var selectedUserRoleValues = Array.from(selectedUserRoleOptions).map(option => option.value);

                var userDetails = {
                    UserRolesIds: selectedUserRoleValues,
                    SysUserID: $('#SystemUserID').val(),
                    Surname: $('#Surname').val(),
                    Othernames: $('#Othernames').val(),
                    Username: $('#Username').val(),
                    Password: $('#Password').val()                    
                };

                var requestVerificationToken = $('#SystemUserForm input[name=__RequestVerificationToken]').val();

                GetOrPostAsync('POST', '/Security/CreateUpdateSystemUser/', userDetails, requestVerificationToken).then(() => {
                        laddaButton.stop();
                        LoadAllUsers();
                        if (systemUserID > 0) {
                            Notify(true, 'System User Updated Successfully');
                        } else {
                            Notify(true, 'System User Created Successfully');
                            $('#btnCreateNew').click();
                        }
                    })
                    .catch(error => {
                        laddaButton.stop();
                        Notify(false, error);
                    });
            } else {
                laddaButton.stop();
                Notify(false, 'Make sure the passwords match');
            }
        });

        $("#btnCreateNew").click(function () {
            $("#SystemUserForm")[0].reset();
            $("#SystemUserID").val("0");
            $(".select-roles").select2({
                placeholder: "Select Roles"
            });
          
            $("#update").hide();
            $("#adduser").show();
            $("#Password").removeAttr("readonly");
            $("#ConfirmUserPassword").removeAttr("readonly")
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
