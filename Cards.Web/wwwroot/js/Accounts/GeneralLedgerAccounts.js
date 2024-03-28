$(document).ready(function () {

    function ReloadPage() {
        FetchAccountClasses();
        LoadAllAccounts();
        LoadAllSubAccounts();
        LoadAllCashFlowCategories()
    } 

    function LoadAllAccounts() {
        var configuredAccountDropdown = $("#ConfiguredAccountID");
        var transferDestAccountDropdown = $("#TransferDestAccountID");

        configuredAccountDropdown.empty().append('<option value="0"></option>');
        transferDestAccountDropdown.empty().append("<option></option>");

        AjaxServerCallAsync("GET", "/Accounts/GetAllAccounts/", "", "", function (response) {
            var accounts = response.response;
            if (response.status && !$.isEmptyObject(accounts)) {
                var tableRows = "";
                for (let i = 0; i < accounts.length; i++) {
                    configuredAccountDropdown.append(new Option(accounts[i].accountName, accounts[i].accountID));
                    transferDestAccountDropdown.append(new Option(accounts[i].accountName, accounts[i].accountID));

                    tableRows += '<tr id="' + accounts[i].accountID + '"><td data-title="Account No">' + accounts[i].accountNo + '</td><td data-title="Account Name">' + accounts[i].accountName + '</td><td data-title="Account Class">' + accounts[i].accountClass.className + "</td></tr>";
                 }

                accountsTable.clear().rows.add($(tableRows)).draw(false);
                configuredAccountDropdown.select2();
                transferDestAccountDropdown.select2();
                }
         });
    }

    function GetAccountDetails(accountId) {
        var requestData = accountId;
        var requestVerificationToken = $("#AccountForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/GetAccountDetails/", requestData, requestVerificationToken, function(response) {
            var accountDetails = response.response;

            if (response.status) {
                var accountId = accountDetails.accountID;
                var accountNo = accountDetails.accountNo;
                var accountName = accountDetails.accountName;
                var accountClassID = accountDetails.accountClassID;

                $("#Account_AccountID").val(accountId);
                $("#Account_AccountNo").val(accountNo);
                $("#Account_Name").val(accountName);
                $("#Account_AccountClassID").val(accountClassID);
                $("#Account_CashFlowCategoryID").val(accountDetails.cashFlowCategoryID);

                var option = new Option(accountName, accountId, true, true);
                $("#SubAccount_AccountID").append(option);

                retrySub = 3;
                LoadAllSubAccountsByAccountID(accountId);

                $("#ForAcc").text(accountName.toUpperCase());
                $("#btnCreateNewSubAccount").click();
                $("#addaccount").hide();
                $("#updateaccount").show();
            }
        });
    }

    function DeleteAccount(accountId) {
        var requestData = accountId;
        var requestVerificationToken = $("#AccountForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/DeleteAccount/", requestData, requestVerificationToken, function (response) {
            var responseData = response.response;

            if (response.status) {
                $("#accountstable #" + accountId).remove();
                Notify(true, "Account Deleted Successfully");
            } else {
                Notify(false, responseData);
              }
         });
    }

    function LoadAllSubAccountsByAccountID(accountID) {
            var requestData = accountID;

            GetOrPostAsync("POST", "/Accounts/GetAllSubAccountsByAccountID/", requestData, "").then(subAccounts => {
                if (subAccountsTable.clear().draw(), !$.isEmptyObject(subAccounts)) {
                    var tableRows = "";

                    for (let i = 0; i < subAccounts.length; i++) {
                        let color, toggleIcon;

                        if (subAccounts[i].isActive === 1) {
                            color = "black";
                            toggleIcon = '<a title="Deactivate Sub Account" href="Javascript:DeactivateSubAccount(' + subAccounts[i].subAccountID + ')"><i class=""></i></a>';
                        } else {
                            color = "gray";
                            toggleIcon = '<a title="Activate Sub Account" href="Javascript:ActivateSubAccount(' + subAccounts[i].subAccountID + ')"><i class="fas fa-toggle-off fa-lg pull-right"></i></a>';
                        }
                        tableRows += '<tr style="color: ' + color + '" id="' + subAccounts[i].subAccountID + '"><td data-title="No">' + subAccounts[i].subAccountID + '</td><td data-title="Name">' + subAccounts[i].subAccountName + '</td><td>' + toggleIcon + '</td></tr>';
                    }
                    subAccountsTable.rows.add($(tableRows)).draw(false);
                 }
            })
            .catch(error => Notify(false, error));
    }

    function GetSubAccountDetails(subAccountId) {
        var requestData = subAccountId;
        var requestVerificationToken = $("#SubAccountForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/GetSubAccountDetails/", requestData, requestVerificationToken, function (response) {
            var subAccountDetails = response.response;

            if (response.status) {
                var subAccountId = subAccountDetails.subAccountID;
                var subAccountName = subAccountDetails.subAccountName;
                var currentBalance = subAccountDetails.currentBalance;

                $("#SubAccount_SubAccountID").val(subAccountId);
                $("#SubAccount_Name").val(subAccountName);
                $("#SubAccount_CurrentBalance").val(currentBalance);
                $("#txtSrcSubAcc").text(subAccountName);
                $("#addsubacc").hide();
                $("#updatesubacc").show();
            }
        });
    }

    function DeleteSubAccount(subAccountId) {
        var requestData = subAccountId;
        var requestVerificationToken = $("#SubAccountForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/DeleteSubAccount/", requestData, requestVerificationToken).then(() => {
            var accountID = $("#SubAccount_AccountID").val();
            LoadAllSubAccountsByAccountID(accountID);
            Notify(true, "Sub Account Deleted Successfully");
        })
        .catch(error => Notify(false, error));
    }

    function LoadAllSubAccounts() {

    }
    function LoadAllBranches() {

    }

    function LoadAllCashFlowCategories() {
        var requestVerificationToken = $("#CashFlowCategoryForm input[name=__RequestVerificationToken]").val();
        var cashFlowCategoryDropdown = $("#Account_CashFlowCategoryID");
        var selectedCategoryId = Number(cashFlowCategoryDropdown.val() || 0);

        cashFlowCategoryDropdown.empty();
        cashFlowCategoryDropdown.append('<option value="0"></option>');

        GetOrPostAsync("GET", "/Accounts/GetActiveCashFlowCategories/", "", requestVerificationToken).then(categories => {
            if (cashflowcatsTable.clear().draw(), !$.isEmptyObject(categories)) {
                var tableRows = "";

                for (let i = 0; i < categories.length; i++) {
                    cashFlowCategoryDropdown.append(selectedCategoryId === categories[i].cashFlowCategoryID
                        ? new Option(categories[i].cashFlowCategoryName, categories[i].cashFlowCategoryID, true, true)
                        : new Option(categories[i].cashFlowCategoryName, categories[i].cashFlowCategoryID));

                    tableRows += '<tr id="' + categories[i].cashFlowCategoryID + '">';
                    tableRows += '<td data-title="No">' + categories[i].cashFlowCategoryID + '</td>';
                    tableRows += '<td data-title="Name">' + categories[i].cashFlowCategoryName + '</td>';
                    tableRows += '<td data-title="Type">' + categories[i].cashFlowCategoryType.cashFlowCategoryTypeName + '</td>';
                    tableRows += '</tr>';
                }

                cashflowcatsTable.rows.add($(tableRows)).draw(false);
            }
        })
        .catch(error => Notify(false, error));
    }

    function GetCashFlowCategoryDetails(categoryId) {
        var requestData = categoryId;
        var requestVerificationToken = $("#CashFlowCategoryForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/GetCashFlowCategoryDetails/", requestData, requestVerificationToken).then(categoryDetails => {
            // Update form fields with received data
            $("#CashFlowCategoryCashFlowCategoryID").val(categoryDetails.cashFlowCategoryID);
            $("#CashFlowCategoryName").val(categoryDetails.cashFlowCategoryName);
            $("#CashFlowCategoryType").val(categoryDetails.cashFlowCategoryType.cashFlowCategoryTypeID);

            // Toggle visibility of form buttons
            $("#addcfcat").hide();
            $("#updatecfcat").show();
        })
        .catch(error => Notify(false, error));
    }

    function DeleteCashFlowCategory(categoryId) {
        var requestData = categoryId;
        var requestVerificationToken = $("#CashFlowCategoryForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/DeleteCashFlowCategory/", requestData, requestVerificationToken).then(() => {
            LoadAllCashFlowCategories();
            Notify(true, "Cash Flow Category Deleted Successfully.");
        })
        .catch(error => Notify(false, error));
    }

    const accountsTable = $("#accountstable").DataTable({
        drawCallback: function() {
            $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
            $.contextMenu({
                selector: "#accountstable tbody tr td",
                trigger: "right",
                delay: 500,
                autoHide: !0,
                callback: function(n, t) {
                    var i = t.$trigger[0].parentElement.id, r;
                    switch (n) {
                        case "select":
                            GetAccountDetails(i);
                        break;
                        case "delete":
                            r = confirm("Are You Sure You Want To Delete The Selected Account?");
                            r === !0 && DeleteAccount(i)
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
        lengthChange: !1,
        buttons: ["excel", "csv", "pdf", "print"],
        paging: !1,
        searching: !0,
        ordering: !0,
        bInfo: !1,
        select: !0,
        scrollY: "36vh",
        sScrollX: "100%",
        scrollX: !0
    });

        accountsTable.on("select", function(event, dt, type, indexes) {
            if (type === "row") {
                let selectedRowsData = accountsTable.rows(indexes).data().toArray();
                let rowId = selectedRowsData[0].DT_RowId;
                if (rowId !== undefined) {
                    GetAccountDetails(rowId);
                }
            }
        });

    const subAccountsTable = $("#subaccountstable").DataTable({
        drawCallback: function() {
            $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
            $.contextMenu({
                selector: "#subaccountstable tbody tr td",
                trigger: "right",
                delay: 500,
                autoHide: !0,
                callback: function(n, t) {
                                        var i = t.$trigger[0].parentElement.id, r;
                    switch (n) {
                                        case "select":
                    GetSubAccountDetails(i);
                    break;
                    case "delete":
                    r = confirm("Are You Sure You Want To Delete The Selected Sub Account?");
                    r === !0 && DeleteSubAccount(i)
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
        lengthChange: !1,
        buttons: ["excel", "csv", "pdf", "print"],
        paging: !1,
        searching: !0,
        ordering: !0,
        bInfo: !1,
        select: !0,
        scrollY: "41vh",
        sScrollX: "100%",
        scrollX: !0
    });

    subAccountsTable.on("select", function(event, dt, type, indexes) {
        if (type === "row") {
            let selectedRowsData = subAccountsTable.rows(indexes).data().toArray();
            let rowId = selectedRowsData[0].DT_RowId;
            if (rowId !== undefined) {
                GetSubAccountDetails(rowId);
            }
        }
    });

    const cashflowcatsTable = $("#cashflowcatstable").DataTable({
        drawCallback: function() {
            $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
            $.contextMenu({
                selector: "#cashflowcatstable tbody tr td",
                trigger: "right",
                delay: 500,
                autoHide: !0,
                callback: function(n, t) {
                                    var i = t.$trigger[0].parentElement.id, r;
                    switch (n) {
                        case "select":
                        GetCashFlowCategoryDetails(i);
                        break;
                        case "delete":
                        r = confirm("Are You Sure You Want To Delete The Selected Category?");
                        r === !0 && DeleteCashFlowCategory(i)
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
        lengthChange: !1,
        paging: !1,
        searching: !0,
        ordering: !0,
        bInfo: !1,
        select: !0,
        scrollY: "45vh",
        sScrollX: "100%",
        scrollX: !0
    });

    cashflowcatsTable.on("select", function(event, dataTable, type, indexes) {
        if (type === "row") {
            let selectedRowsData = cashflowcatsTable.rows(indexes).data().toArray();
            let rowId = selectedRowsData[0].DT_RowId;
            if (rowId !== undefined) {
                GetCashFlowCategoryDetails(rowId);
            }
        }
    });

    accountsTable.clear().draw();
    subAccountsTable.clear().draw();
    cashflowcatsTable.clear().draw();
    $("#Account_AccountID").val("0");
    $("#Account_AccountNo").val("0");
    $("#SubAccount_SubAccountID").val("0");
    $("#SubAccount_CurrentBalance").val("0");
    $("#CashFlowCategoryCashFlowCategoryID").val("0");
    ReloadPage()

    $("#AccountForm").submit(function (event) {
        event.preventDefault();

        var accountId = $("#Account_AccountID").val();
        var buttonSelector = accountId > 0 ? "#btnupdateaccount" : "#btnaddaccount";
        var laddaButton = Ladda.create(document.querySelector(buttonSelector));

        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var accountData = {
            AccountID: $("#Account_AccountID").val(),
            AccountNo: $("#Account_AccountNo").val(),
            AccountName: $("#Account_Name").val(),
            AccountClassID: $("#Account_AccountClassID").val(),
            CashFlowCategoryID: $("#Account_CashFlowCategoryID").val()
        };

        var requestVerificationToken = $("#AccountForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/CreateUpdateAccount/", accountData, requestVerificationToken, function (response) {
            var responseData = response.response;

            if (response.status) {
                var newRow = '<tr id="' + responseData.accountID + '"><td data-title="Account No">' + responseData.accountNo + '</td><td data-title="Account Name">' + responseData.accountName + '</td><td data-title="Account Class">' + responseData.accountClass.className + '</td></tr>';

                if (accountId > 0) {
                    $("#accountstable #" + accountId).replaceWith(newRow);
                    Notify(true, "Account Updated Successfully");
                } else {
                    accountsTable.row.add($(newRow)).draw(false);
                    Notify(true, "Account Created Successfully");
                    $("#btnCreateNewAccount").click();
                }
                laddaButton.stop();
            } else {
                laddaButton.stop();
                Notify(false, responseData);
            }
        });
    });

    $("#btnCreateNewAccount").click(function() {
        $("#AccountForm")[0].reset();
        $("#updateaccount").hide();
        $("#addaccount").show();
        $("#Account_AccountID").val("0");
        $("#Account_AccountNo").val("0");
        subAccountsTable.clear().draw();
        $("#btnCreateNewSubAccount").click();
        $("#ForAcc").text("");
        $("#SubAccount_AccountID").empty()
    });

    $("#SubAccountForm").submit(function (event) {
        event.preventDefault();

        var subAccountId = Number($("#SubAccount_SubAccountID").val() || 0);
        var laddaButton = Ladda.create(document.querySelector("#btnaddsubaccount"));

        if (subAccountId > 0) {
            laddaButton = Ladda.create(document.querySelector("#btnupdatesubaccount"));
        }

        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var subAccountData = {
            SubAccountID: $("#SubAccount_SubAccountID").val(),
            SubAccountName: $("#SubAccount_Name").val(),
            AccountID: $("#SubAccount_AccountID").val(),
            CurrentBalance: $("#SubAccount_CurrentBalance").val()
        };

        var requestVerificationToken = $("#SubAccountForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/CreateUpdateSubAccount/", subAccountData, requestVerificationToken).then(() => {
            LoadAllSubAccountsByAccountID(subAccountData.AccountID);
            laddaButton.stop();

            if (subAccountId > 0) {
                Notify(true, "Sub Account Updated Successfully");
            } else {
                Notify(true, "Sub Account Created Successfully");
                $("#btnCreateNewSubAccount").click();
            }
        })
        .catch(error => {
            laddaButton.stop();
            Notify(false, error);
        });
    });

    $("#btnCreateNewSubAccount").click(function() {
        $("#SubAccountForm")[0].reset();
        $("#updatesubacc").hide();
        $("#addsubacc").show();
        $("#SubAccount_SubAccountID").val("0");
        $("#SubAccount_CurrentBalance").val("0");
        $("#txtSrcSubAcc").text("")
    });

    $("#CashFlowCategoryForm").submit(function(event) {
        event.preventDefault();

        var cashFlowCategoryId = Number($("#CashFlowCategoryCashFlowCategoryID").val() || 0);
        var button = Ladda.create(document.querySelector(cashFlowCategoryId > 0 ? "#btnupdatecfcat" : "#btnaddcfcat"));

        button.start();
        button.isLoading();
        button.setProgress(-1);

        var cashFlowCategoryData = {
            CashFlowCategoryID: cashFlowCategoryId,
            CashFlowCategoryName: $("#CashFlowCategoryName").val(),
            CashFlowCategoryTypeID: $("#CashFlowCategoryType").val()
        };

        var requestVerificationToken = $("#CashFlowCategoryForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/CreateUpdateCashFlowCategory/", cashFlowCategoryData, requestVerificationToken).then(() => {
            LoadAllCashFlowCategories();

            if (cashFlowCategoryId > 0) {
                Notify(true, "Cash Flow Category Updated Successfully");
            } else {
                Notify(true, "Cash Flow Category Created Successfully");
                $("#btnCreateNewCfcat").click();
            }

            button.stop();
        })
        .catch(error => {
            button.stop();
            Notify(false, error);
        });
    });

    $("#btnCreateNewCfcat").click(function () {
        $("#CashFlowCategoryForm")[0].reset();
        $("#CashFlowCategoryCashFlowCategoryID").val("0");
        $("#updatecfcat").hide();
        $("#addcfcat").show()
    });

    $("#AddAccountClassForm").submit(function (event) {
        var laddaButton, className, accountType;
        event.preventDefault();

        laddaButton = Ladda.create(document.querySelector("#addAccountClass"));
        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        className = $("#AccountClassName").val();
        accountType = $("#AccountType").val();
        className = className.substr(0, 1).toUpperCase() + className.substr(1);

        var requestVerificationToken = $("#AddAccountClassForm input[name=__RequestVerificationToken]").val();
        var requestData = { AccountClassID: 0, ClassName: className, AccountTypeID: accountType };

        GetOrPostAsync("POST", "/Accounts/CreateAccountClass/", requestData, requestVerificationToken)
            .then((response) => {
                    var newOption = new Option(response.className, response.accountClassID, true, true);
                    $(".select-account-class").append(newOption);
                    $("#AddAccountClassForm")[0].reset();
                    laddaButton.stop();
                    $(".add-accountclass-modal").modal("toggle");
                    Notify(true, "Account Class Created Successfully");
                    laddaButton.stop();           
            })
            .catch((error) => {
                Notify(false, error);
                laddaButton.stop();
            });
    });


    $("#TransferSubAccountForm").submit(function (event) {
        var laddaButton;
        event.preventDefault();

        laddaButton = Ladda.create(document.querySelector("#btnTransferSubAccountBalance"));
        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var transferData = {
            SourceSubAccountID: $("#SubAccount_SubAccountID").val(),
            DestSubAccountID: $("#TransferDestSubAccountID").val()
        };

        var requestVerificationToken = $("#TransferSubAccountForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/TransferSubAccountBalance/", transferData, requestVerificationToken)
            .then(() => {
                laddaButton.stop();
                Notify(true, "Balance Transferred Successfully.");
                $(".transfer-balance-modal").modal("toggle");
            })
            .catch(error => {
                laddaButton.stop();
                Notify(false, error);
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


function TransferSubAccountBalance() {
    let sourceSubAccountId = Number($("#SubAccount_SubAccountID").val() || 0);

    if (sourceSubAccountId === 0) {
        Notify(false, "First select the source sub account.");
        return;
    }

    $("#TransferSubAccountForm")[0].reset();
    $("#TransferDestAccountID").select2();
    $("#TransferDestSubAccountID").empty().select2();
    $(".transfer-balance-modal").modal("toggle");

    $("#TransferDestAccountID").on("change", function () {
        let selectedAccountId = $(this).val();
        var destinationSubAccountDropdown = $("#TransferDestSubAccountID");
        destinationSubAccountDropdown.empty().append("<option></option>");

        GetOrPostAsync("POST", "/Accounts/GetAllSubAccountsByAccountID/", selectedAccountId, "")
            .then(subAccounts => {
                if (!$.isEmptyObject(subAccounts)) {
                    for (let i = 0; i < subAccounts.length; i++) {
                        destinationSubAccountDropdown.append(new Option(subAccounts[i].subAccountName, subAccounts[i].subAccountID));
                    }
                }
            })
            .catch(error => console.log(error));
    });
}
