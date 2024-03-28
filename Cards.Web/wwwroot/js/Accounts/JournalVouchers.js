$(document).ready(function () {

    function ReloadPage() { 
        LoadAllPeriods();
        LoadAllAccounts();
    }

    function LoadAllPeriods() {
        GetOrPostAsync("GET", "/Accounts/GetAllFiscalPeriods/", "", "").then(response => {
                if (!$.isEmptyObject(response)) {
                    var selectElement = $("#FiscalPeriodID");
                    selectElement.empty();

                    for (let i = 0; i < response.length; i++) {
                        let option = '<option value="' + response[i].fiscalPeriodID + '"';
                        if (response[i].isActive === 1) {
                            option += ' selected>';
                        } else {
                            option += '>';
                        }
                        option += response[i].fiscalPeriodID + '</option>';

                        selectElement.append(option);
                    }
                    selectElement.trigger("change");
                }
            })
            .catch(error => Notify(false, error));
    }

    function FilterJournalVouchers() {
        var filterData = {
            DateFrom: $("#dateFrom").val(),
            DateTo: $("#dateTo").val(),
            Flag: $("#flag").val(),
            FiscalPeriodID: $("#ForAllPeriods").is(":checked") ? 0 : $("#FiscalPeriodID").val()
        };

        var requestVerificationToken = $("#FilterJvsForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/FilterJournalVouchers/", filterData, requestVerificationToken)
            .then(journalVouchers => {
                if (jvsTable.clear().draw(), !$.isEmptyObject(journalVouchers)) {
                    var tableRows = "";

                    for (let i = 0; i < journalVouchers.length; i++) {
                        let textColor = "black";
                        journalVouchers[i].isPosted === 1 && (textColor = "green");

                        tableRows += '<tr style="color: ' + textColor + ';" id="' + journalVouchers[i].journalVoucherID + '">'
                            + '<td data-title="JV ID">' + journalVouchers[i].journalVoucherID + '</td>'
                            + '<td data-title="Fiscal Period">' + journalVouchers[i].fiscalPeriodID + '</td>'
                            + '<td data-title="Transaction DateTime">' + formatDateTime(journalVouchers[i].transactionDateTime) + '</td>'
                            + '<td data-title="Source Ref">' + journalVouchers[i].sourceReference + '</td>'
                            + '<td data-title="Description">' + journalVouchers[i].description + '</td>'
                            + '<td data-title="Amount" class="currency">' + formatCurrency(journalVouchers[i].amount) + '</td>'
                            + '</tr>';
                    }
                    jvsTable.rows.add($(tableRows)).draw(false);
                }
            })
            .catch(error => Notify(false, error));
    }

    function DeleteJournalVoucher(journalVoucherID) {
        var requestData = journalVoucherID;
        var requestVerificationToken = $("#JournalVoucherForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/DeleteJournalVoucher/", requestData, requestVerificationToken)
            .then(() => {
                Notify(true, "Journal Voucher Cancelled Successfully");
                FilterJournalVouchers();
            })
            .catch(error => Notify(false, error));
    }

    function GetJournalVoucherDetails(journalVoucherID) {
        var requestData = journalVoucherID;
        var requestVerificationToken = $("#JournalVoucherForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/GetJournalVoucherDetails/", requestData, requestVerificationToken).then(details => {
                $("#JournalVoucherID").val(details.journalVoucherID);
                $("#DepartmentID").val(details.departmentID);
                $("#SourceReference").val(details.sourceReference);
                $("#Description").val(details.description);
                $("#Amount").val(details.amount);
                $("#TransactionDateTime").val(new Date(details.transactionDateTime).toLocalISOString().slice(0, 16));
                $(".txtJvId").text(details.journalVoucherID);
                $("#txtJvPostedBy").text(details.postedBy);
                $("#txtJvStatus").text(details.isPosted === 1 ? "Posted" : "Not Posted");

                GetJournalVoucherEntries(details.journalVoucherID);

                $("#addjv").hide();
                $("#updatejv").show();
                $("#FiscalPeriodID").val(details.fiscalPeriodID);
        })
        .catch(error => Notify(false, error));
    }

    function GetJournalVoucherEntries(n) {
        var t = n
          , i = $("#JournalVoucherForm input[name=__RequestVerificationToken]").val();
        GetOrPostAsync("POST", "/Accounts/GetJournalVoucherEntries/", t, i).then(n=>{
            entriesTable.clear().draw();
            let i = 0
              , r = 0;
            if (!$.isEmptyObject(n)) {
                var t = "";
                for (let u = 0; u < n.length; u++)
                            t += '<tr id="' + n[u].journalVoucherEntryID + '"><td data-title="Entry No">' + n[u].journalVoucherEntryID + '<\/td><td data-title="Sub Account">' + n[u].debitSubAccount.subAccountName + '<\/td><td data-title="Debit" class="currency">' + formatCurrency(n[u].creditAmount) + '<\/td><td><\/td><td><a href="Javascript:DeleteJournalVoucherEntry(' + n[u].journalVoucherEntryID + ');"><span class=""><\/span><\/a><\/td><\/tr>',
                    t += '<tr id="' + n[u].journalVoucherEntryID + '"><td style="border-top: none;"><\/td><td style="border-top: none;" data-title="Sub Account">' + n[u].creditSubAccount.subAccountName + '<\/td><td style="border-top: none;"><\/td><td data-title="Credit" class="currency" style="border-top: none;">' + formatCurrency(n[u].creditAmount) + '<\/td><td style="border-top: none;"><\/td><\/tr>',
                    i += n[u].creditAmount,
                    r += n[u].creditAmount;
                entriesTable.rows.add($(t)).draw(!1)
            }
            $(entriesTable.column(2).footer()).html(`<strong>${formatCurrency(i)}</strong>`);
            $(entriesTable.column(3).footer()).html(`<strong>${formatCurrency(r)}</strong>`)
        }
        ).catch(n=>Notify(!1, n))
    }
    function LoadAllAccounts() {
        var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();
        var debitAccountSelect = $("#DebitAccountID");
        var creditAccountSelect = $("#CreditAccountID");

        debitAccountSelect.empty().append('<option value="0"></option>');
        creditAccountSelect.empty();

        GetOrPostAsync("GET", "/Accounts/GetAllAccounts/", "", requestVerificationToken)
            .then(accounts => {
                if (!$.isEmptyObject(accounts)) {
                    for (let i = 0; i < accounts.length; i++) {
                        let option = new Option(accounts[i].accountName, accounts[i].accountID);
                        debitAccountSelect.append(option);
                    }

                    var clonedOptions = $("#DebitAccountID > option").clone();
                    creditAccountSelect.append(clonedOptions);

                    debitAccountSelect.select2().trigger("change");
                    creditAccountSelect.select2().trigger("change");
                }
            })
            .catch(error => Notify(false, error));
    }

    function GetJournalVoucherEntryDetails(entryID) {
        var requestData = entryID;
        var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/GetJournalVoucherEntryDetails/", requestData, requestVerificationToken)
            .then(entryDetails => {
                $("#CreditSubAccountID").empty();
                $("#DebitSubAccountID").empty();

                $("#DebitAccountID").val(entryDetails.debitAccountID).select2().trigger("change");
                $("#CreditAccountID").val(entryDetails.creditAccountID).select2().trigger("change");
                $("#AccountEntryID").val(entryDetails.journalVoucherEntryID);
                $("#EntryAmount").val(entryDetails.creditAmount);

                var debitSubAccountInterval = setInterval(function () {
                    if ($("#DebitSubAccountID option").length > 1) {
                        $("#DebitSubAccountID").val(entryDetails.debitSubAccountID).select2();
                        clearInterval(debitSubAccountInterval);
                    }
                }, 20);

                var creditSubAccountInterval = setInterval(function () {
                    if ($("#CreditSubAccountID option").length > 1) {
                        $("#CreditSubAccountID").val(entryDetails.creditSubAccountID).select2();
                        clearInterval(creditSubAccountInterval);
                    }
                }, 20);

                $("#addentry").hide();
                $("#updateentry").show();
        })
        .catch(error => Notify(false, error));
    }

    function DeleteJournalVoucherEntry(entryID) {
        var requestData = entryID;
        var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();
        var journalVoucherID = $("#JournalVoucherID").val();

        GetOrPostAsync("POST", "/Accounts/DeleteEntry/", requestData, requestVerificationToken)
            .then(() => {
                Notify(true, "Account Entry Deleted Successfully");
                GetJournalVoucherEntries(journalVoucherID);
            })
            .catch(error => Notify(false, error));
    }

    $(document).ready(function() {
        jvsTable.clear().draw();
        entriesTable.clear().draw();
        $("#dateFrom").val((new Date).toLocalISOString().slice(0, 10));
        $("#dateTo").val((new Date).toLocalISOString().slice(0, 10));
        $("#TransactionDateTime").val((new Date).toLocalISOString().slice(0, 16));
        $("#JournalVoucherID").val("0");
        $("#JournalVoucherEntryID").val("0");
        ReloadPage()
    });

    const jvsTable = $("#jvstable").DataTable({
        drawCallback: function() {
            $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
            $.contextMenu({
                selector: "#jvstable tbody tr td",
                trigger: "right",
                delay: 500,
                autoHide: !0,
                callback: function(n, t) {
                    var i = t.$trigger[0].parentElement.id;
                    switch (n) {
                    case "select":
                        GetJournalVoucherDetails(i);
                        break;
                    case "cancel":
                        confirm("Are you sure you want to cancel the selected journal voucher?") && DeleteJournalVoucher(i)
                    }
                },
                items: {
                    select: {
                        name: "Select"
                    },
                    cancel: {
                        name: "Cancel Journal"
                    }
                }
            })
        },
        lengthChange: !1,
        buttons: ["copy", "csv", "excel", "pdf", "print"],
        paging: !1,
        searching: !0,
        ordering: !0,
        bInfo: !0,
        select: !0,
        scrollY: "40vh",
        sScrollX: "100%",
        scrollX: !0
    });

    jvsTable.on("select", function (event, datatable, type, indexes) {
        if (type === "row") {
            let selectedRowsData = jvsTable.rows(indexes).data().toArray();
            let rowId = selectedRowsData[0].DT_RowId;
            if (rowId !== undefined) {
                GetJournalVoucherDetails(rowId);
            }
        }
    });

    jvsTable.on("dblclick", function (event) {
        let targetParentId = $(event.target.parentElement).prop("id");
        GetJournalVoucherDetails(targetParentId);
        $(".jv-posting-modal").modal("toggle");
    });

    var entriesTable = $("#postingstable").DataTable({
        drawCallback: function() {
            $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
            $.contextMenu({
                selector: "#postingstable tbody tr td",
                trigger: "right",
                delay: 500,
                autoHide: !0,
                callback: function(n, t) {
                    var i = t.$trigger[0].parentElement.id;
                    switch (n) {
                    case "select":
                        GetJournalVoucherEntryDetails(i);
                        break;
                    case "delete":
                        confirm("Are you sure you want to delete the selected entry?") && DeleteJournalVoucherEntry(i)
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
        searching: !1,
        ordering: !1,
        bInfo: !1,
        scrollY: "35vh",
        sScrollX: "100%",
        scrollX: !0
    });

    $("#FiscalPeriodID").on("change", ()=>{
        var n = Number($("#FiscalPeriodID").val());
        isNaN(n) || n <= 0 || (GetPeriodDetails(n),
        FilterJournalVouchers())
    }
    );

    $("#FilterJvsForm").submit(function(n) {
        n.preventDefault();
        FilterJournalVouchers()
    });
    $("#flag").on("change", ()=>{
        FilterJournalVouchers()
    }
    );
    $("#JournalVoucherForm").submit(function (event) {
        event.preventDefault();

        var journalVoucherID = $("#JournalVoucherID").val();
        var laddaButton = Ladda.create(document.querySelector("#btnSaveJournalVoucher"));

        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var formData = {
            JournalVoucherID: journalVoucherID,
            SourceReference: $("#SourceReference").val(),
            Description: $("#Description").val(),
            FiscalPeriodID: $("#FiscalPeriodID").val(),
            TransactionDateTime: $("#TransactionDateTime").val()
        };

        var requestVerificationToken = $("#JournalVoucherForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/CreateUpdateJournalVoucher/", formData, requestVerificationToken)
            .then(response => {
                if (Number(journalVoucherID) > 0) {
                    Notify(true, "Journal voucher Updated Successfully.");
                } else {
                    Notify(true, "Journal voucher Created Successfully.");
                    $("#JournalVoucherID").val(response.journalVoucherID);
                    $(".txtJvId").text(response.journalVoucherID);
                    $("#txtJvPostedBy").text("Not Posted");
                    $("#txtJvStatus").text("Not Posted");
                }

                FilterJournalVouchers();
                laddaButton.stop();
            })
            .catch(error => {
                laddaButton.stop();
                Notify(false, error);
            });
    });

    $("#btnCreateNewJv").click(function() {
        $("#JournalVoucherForm")[0].reset();
        $("#TransactionDateTime").val((new Date).toLocalISOString().slice(0, 16));
        $("#JournalVoucherID").val("0");
        $("#txtJvPostedBy").text("");
        $(".txtJvId").text("");
        $("#txtJvStatus").text("");
        $("#updatejv").hide();
        $("#addjv").show();
        $("#btnCreateNewEntry").click();
        entriesTable.clear().draw()
    });
    $("#btnEntries").on("click", ()=>{
        var n = Number($("#JournalVoucherID").val());
        if (n <= 0) {
            Notify(!1, "First select a journal voucher");
            return
        }
        $(".jv-posting-modal").modal("toggle")
    }
    );
    $("#DebitAccountID").on("change", function () {
        var $debitSubAccountID = $("#DebitSubAccountID");
        $debitSubAccountID.empty().append("<option></option>");

        var selectedValue = Number($(this).val() || 0);
        if (!(selectedValue <= 0)) {
            var requestData = selectedValue;
            var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();

            GetOrPostAsync("POST", "/Accounts/GetAllSubAccountsByAccountID/", requestData, requestVerificationToken)
                .then(response => {
                    if (!$.isEmptyObject(response)) {
                        for (let i = 0; i < response.length; i++) {
                            let option = new Option(response[i].subAccountName, response[i].subAccountID);
                            $debitSubAccountID.append(option);
                        }
                        $debitSubAccountID.select2();
                    }
                })
                .catch(error => Notify(false, error));
        }
    });

    $("#CreditAccountID").on("change", function () {
        var creditSubAccount = $("#CreditSubAccountID");
        creditSubAccount.empty().append("<option></option>");

        var selectedValue = $(this).val();
        if (!(Number(selectedValue) <= 0)) {
            var accountId = selectedValue;
            var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();

            GetOrPostAsync("POST", "/Accounts/GetAllSubAccountsByAccountID/", accountId, requestVerificationToken)
                .then(subAccounts => {
                    if (!$.isEmptyObject(subAccounts)) {
                        for (let i = 0; i < subAccounts.length; i++) {
                            let option = new Option(subAccounts[i].subAccountName, subAccounts[i].subAccountID);
                            creditSubAccount.append(option);
                        }
                        creditSubAccount.select2();
                    }
                })
                .catch(error => Notify(false, error));
        }
    });

    $("#JournalVoucherEntryForm").submit(function(event) {
        event.preventDefault();

        var entryID = Number($("#AccountEntryID").val());
        var voucherID = $("#JournalVoucherID").val();
        var laddaButton = Ladda.create(document.querySelector("#btnaddentry"));

        if (entryID > 0) {
            laddaButton = Ladda.create(document.querySelector("#btnupdateentry"));
        }

        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var entryData = {
            JournalVoucherID: voucherID,
            JournalVoucherEntryID: entryID,
            DebitAmount: $("#EntryAmount").val(),
            CreditAmount: $("#EntryAmount").val(),
            DebitSubAccountID: $("#DebitSubAccountID").val(),
            CreditSubAccountID: $("#CreditSubAccountID").val()
        };

        var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();

        GetOrPostAsync("POST", "/Accounts/CreateUpdateEntry/", entryData, requestVerificationToken)
            .then(() => {
                if (entryID > 0) {
                    Notify(true, "Account Entry Updated Successfully.");
                } else {
                    Notify(true, "Account Entry Added Successfully.");
                }

                GetJournalVoucherEntries(voucherID);
                laddaButton.stop();
                $("#btnCreateNewEntry").click();
            })
            .catch(error => {
                laddaButton.stop();
                Notify(false, error);
            });
    });

    $("#btnCreateNewEntry").click(function() {
        $("#JournalVoucherEntryForm")[0].reset();
        $("#JournalVoucherEntryID").val("0");
        $("#DebitAccountID").val($("#DebitAccountID option:first").val()).select2().trigger("change");
        $("#CreditAccountID").val($("#CreditAccountID option:first").val()).select2().trigger("change");
        $("#CreditSubAccountID").empty();
        $("#DebitSubAccountID").empty();
        $("#addentry").show();
        $("#updateentry").hide()
    });
    $("#btnPostJournal").on("click", () => {
        var journalVoucherID = Number($("#JournalVoucherID").val());
        var laddaButton;

        if (journalVoucherID <= 0) {
            Notify(false, "First select a journal voucher");
            return;
        }

        if (confirm("You are about to post a journal voucher. Are you sure you want to continue?")) {
            laddaButton = Ladda.create(document.querySelector("#btnPostJournal"));
            laddaButton.start();
            laddaButton.isLoading();
            laddaButton.setProgress(-1);

            var requestData = journalVoucherID;
            var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();

            GetOrPostAsync("POST", "/Accounts/PostJournalVoucher/", requestData, requestVerificationToken)
                .then(() => {
                    GetJournalVoucherDetails(journalVoucherID);
                    FilterJournalVouchers();
                    Notify(true, "Journal Voucher Posted Successfully");
                    laddaButton.stop();
                    $(".jv-posting-modal").modal("toggle");
                })
                .catch(error => {
                    Notify(false, error);
                    laddaButton.stop();
                });
        }
    });

    $("#btnunpostJournal").on("click", () => {
        var journalVoucherID = Number($("#JournalVoucherID").val());
        var laddaButton;

        if (journalVoucherID <= 0) {
            Notify(false, "First select a journal voucher");
            return;
        }

        if (confirm("You are about to unpost a journal voucher. Are you sure you wish to continue?")) {
            laddaButton = Ladda.create(document.querySelector("#btnunpostJournal"));
            laddaButton.start();
            laddaButton.isLoading();
            laddaButton.setProgress(-1);

            var requestData = journalVoucherID;
            var requestVerificationToken = $("#JournalVoucherEntryForm input[name=__RequestVerificationToken]").val();

            GetOrPostAsync("POST", "/Accounts/UnPostJournalVoucher/", requestData, requestVerificationToken)
                .then(() => {
                    GetJournalVoucherDetails(journalVoucherID);
                    FilterJournalVouchers();
                    Notify(true, "Journal Voucher unposted Successfully");
                    laddaButton.stop();
                })
                .catch(error => {
                    Notify(false, error);
                    laddaButton.stop();
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