$(document).ready(function () {

    function ReloadPage() {
        LoadAllPeriods()
    }

    function LoadAllPeriods() {
        AjaxServerCallAsync("GET", "/Accounts/GetAllFiscalPeriods/", "", "", function (response) {
            var fiscalPeriods = response.response;

            if (response.status && !$.isEmptyObject(fiscalPeriods)) {
                fiscalPeriodsTable.clear().draw();

                var tableRows = "";

                for (var i = 0; i < fiscalPeriods.length; i++) {
                    var fiscalPeriod = fiscalPeriods[i];

                    var isActiveText = fiscalPeriod.isActive === 1 ? "Yes" : "No";
                    var isOpenText = fiscalPeriod.isOpen === 1 ? "Yes" : "No";

                    tableRows += '<tr id="' + fiscalPeriod.fiscalPeriodID + '">';
                    tableRows += '<td data-title="Fiscal Period No">' + fiscalPeriod.fiscalPeriodID + '</td>';
                    tableRows += '<td data-title="Open Date">' + formatDate(fiscalPeriod.openDate) + '</td>';
                    tableRows += '<td data-title="Close Date">' + formatDate(fiscalPeriod.closeDate) + '</td>';
                    tableRows += '<td data-title="Is Active">' + isActiveText + '</td>';
                    tableRows += '<td data-title="Is Open">' + isOpenText + '</td>';
                    tableRows += '</tr>';
                }

                fiscalPeriodsTable.rows.add($(tableRows)).draw(false);
            }
        });
    }

    function LoadAllActivePeriods() {
        AjaxServerCallAsync("GET", "/Accounts/GetAllActiveFiscalPeriods/", "", "", function (response) {
            var fiscalPeriods = response.response;

            if (response.status && !$.isEmptyObject(fiscalPeriods)) {
                fiscalPeriodsTable.clear().draw();

                var tableRows = "";

                for (var i = 0; i < fiscalPeriods.length; i++) {
                    var fiscalPeriod = fiscalPeriods[i];

                    var isActiveText = fiscalPeriod.isActive === 1 ? "Yes" : "No";
                    var isOpenText = fiscalPeriod.isOpen === 1 ? "Yes" : "No";

                    tableRows += '<tr id="' + fiscalPeriod.fiscalPeriodID + '">';
                    tableRows += '<td data-title="Fiscal Period No">' + fiscalPeriod.fiscalPeriodNo + '</td>';
                    tableRows += '<td data-title="Open Date">' + formatDate(fiscalPeriod.openDate) + '</td>';
                    tableRows += '<td data-title="Close Date">' + formatDate(fiscalPeriod.closeDate) + '</td>';
                    tableRows += '<td data-title="Is Active">' + isActiveText + '</td>';
                    tableRows += '<td data-title="Is Open">' + isOpenText + '</td>';
                    tableRows += '</tr>';
                }

                fiscalPeriodsTable.rows.add($(tableRows)).draw(false);
            }
        });
    }

    function LoadAllInactivePeriods() {
        AjaxServerCallAsync("GET", "/Accounts/GetAllInactiveFiscalPeriods/", "", "", function (response) {
            var inactiveFiscalPeriods = response.response;

            if (response.status && !$.isEmptyObject(inactiveFiscalPeriods)) {
                fiscalPeriodsTable.clear().draw();

                var tableRows = "";

                for (var i = 0; i < inactiveFiscalPeriods.length; i++) {
                    var fiscalPeriod = inactiveFiscalPeriods[i];

                    var isActiveText = fiscalPeriod.isActive === 1 ? "Yes" : "No";
                    var isOpenText = fiscalPeriod.isOpen === 1 ? "Yes" : "No";

                    tableRows += '<tr id="' + fiscalPeriod.fiscalPeriodID + '">';
                    tableRows += '<td data-title="Fiscal Period No">' + fiscalPeriod.fiscalPeriodNo + '</td>';
                    tableRows += '<td data-title="Open Date">' + formatDate(fiscalPeriod.openDate) + '</td>';
                    tableRows += '<td data-title="Close Date">' + formatDate(fiscalPeriod.closeDate) + '</td>';
                    tableRows += '<td data-title="Is Active">' + isActiveText + '</td>';
                    tableRows += '<td data-title="Is Open">' + isOpenText + '</td>';
                    tableRows += '</tr>';
                }

                fiscalPeriodsTable.rows.add($(tableRows)).draw(false);
            }
        });
    }

    function GetPeriodDetails(periodID) {
        var requestData = periodID;
        var requestVerificationToken = $("#FiscalPeriodForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/GetFiscalPeriodDetails/", requestData, requestVerificationToken, function (response) {
            var periodDetails = response.response;

            if (response.status) {
                var fiscalPeriodID = periodDetails.fiscalPeriodID;
                var openDate = new Date(periodDetails.openDate);
                var closeDate = new Date(periodDetails.closeDate);
                var isActive = periodDetails.isActive;
                var isOpen = periodDetails.isOpen;

                $("#FiscalPeriodID").val(fiscalPeriodID);
                $("#FiscalPeriodNo").val(periodDetails.fiscalPeriodNo);
                $("#OpenDate").val(openDate.toLocalISOString().substr(0, 10));
                $("#CloseDate").val(closeDate.toLocalISOString().substr(0, 10));

                $("#IsActive").prop("checked", isActive === 1);
                $("#IsOpen").prop("checked", isOpen === 1);

                $("#FiscalHash").val(periodDetails.encrypted);

                $("#addperiod").hide();
                $("#update").show();
            }
        });
    }

    function DeleteFiscalPeriod(periodId) {
        var requestData = periodId;
        var requestVerificationToken = $("#FiscalPeriodForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/DeleteFiscalPeriod/", requestData, requestVerificationToken, function (response) {
            var deleteResponse = response.response;

            if (response.status) {
                var deletedElement = document.getElementById(periodId);

                if (deletedElement) {
                    deletedElement.parentNode.removeChild(deletedElement);
                    Notify(!0, "Fiscal Period Deleted Successfully");
                } else {
                    Notify(!1, "Error: Could not find the element to delete.");
                }
            } else {
                Notify(!1, deleteResponse);
            }
        });
    }

    const fiscalPeriodsTable = $("#fiscalperiodstable").DataTable({
        drawCallback: function () {
            $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
            $.contextMenu({
                selector: "#fiscalperiodstable tbody tr td",
                trigger: "right",
                delay: 500,
                autoHide: !0,
                callback: function (n, t) {
                    var i = t.$trigger[0].parentElement.id, r;
                    switch (n) {
                        case "select":
                            GetPeriodDetails(i);
                            break;
                        case "delete":
                            r = confirm("Are You Sure You Want To Delete The Selected Fiscal Period?");
                            r === !0 && DeleteFiscalPeriod(i)
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
        scrollY: "30vh",
        sScrollX: "100%",
        scrollX: !0
    });

    fiscalPeriodsTable.on("select", function (event, dataTable, type, indexes) {
        if (type === "row") {
            let selectedRowsData = fiscalPeriodsTable.rows(indexes).data().toArray();
            let rowId = selectedRowsData[0].DT_RowId;
            if (rowId !== undefined) {
                GetPeriodDetails(rowId);
            }
        }
    });

    fiscalPeriodsTable.clear().draw();
    ReloadPage();
    $("#FiscalPeriodID").val("0");
    $("#FiscalPeriodNo").val("0")

    $("#FiscalPeriodForm").submit(function (event) {
        event.preventDefault();

        var fiscalPeriodID = $("#FiscalPeriodID").val();
        var laddaButton = Ladda.create(document.querySelector(fiscalPeriodID > 0 ? "#btnupdateperiod" : "#btnaddperiod"));

        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var requestData = {
            FiscalPeriodID: $("#FiscalPeriodID").val(),
            FiscalPeriodNo: $("#FiscalPeriodNo").val(),
            OpenDate: $("#OpenDate").val(),
            CloseDate: $("#CloseDate").val(),
            IsActive: $("#IsActive").is(":checked") ? 1 : 0,
            IsOpen: $("#IsOpen").is(":checked") ? 1 : 0
        };

        var requestVerificationToken = $("#FiscalPeriodForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/CreateUpdateFiscalPeriod/", requestData, requestVerificationToken, function (response) {
            var fiscalPeriod = response.response;

            if (response.status) {
                var isNewPeriod = fiscalPeriodID <= 0;
                var formattedOpenDate = formatDate(fiscalPeriod.openDate);
                var formattedCloseDate = formatDate(fiscalPeriod.closeDate);
                var isActiveText = fiscalPeriod.isActive === 1 ? "Yes" : "No";
                var isOpenText = fiscalPeriod.isOpen === 1 ? "Yes" : "No";

                var rowHtml = '<tr id="' + fiscalPeriod.fiscalPeriodID + '"><td data-title="Fiscal Period No">' + fiscalPeriod.fiscalPeriodNo + '</td><td data-title="Open Date">' + formattedOpenDate + '</td><td data-title="Close Date">' + formattedCloseDate + '</td><td data-title="Is Active">' + isActiveText + '</td><td data-title="Is Open">' + isOpenText + '</td></tr>';

                if (isNewPeriod) {
                    fiscalPeriodsTable.row.add($(rowHtml)).draw(false);
                    Notify(true, "Fiscal Period Created Successfully");
                    $("#btnCreateNew").click();
                } else {
                    $("#" + fiscalPeriodID).replaceWith(rowHtml);
                    Notify(true, "Fiscal Period Updated Successfully");
                }

                laddaButton.stop();
            } else {
                laddaButton.stop();
                Notify(false, response.response);
            }
        });
    });

    $("#btnCreateNew").click(function () {
        $("#FiscalPeriodForm")[0].reset();
        $("#update").hide();
        $("#addperiod").show();
        $("#FiscalPeriodID").val("0");
        $("#FiscalPeriodNo").val("0");
        $("#FiscalHash").val("")
    })

    $("#btncloseperiod").on("click", () => {
        var fiscalPeriodID = Number($("#FiscalPeriodID").val());

        if (isNaN(fiscalPeriodID) || fiscalPeriodID === 0) {
            Notify(!1, "First select a fiscal period.");
            return;
        }

        var requestData = fiscalPeriodID;
        var requestVerificationToken = "CfDJ8D8r6CAn5wtBg7mv7kHl3cT0-21p93qtV_UStt_7HtuL_4ZH3xkjAzhFUV0Wu4fr4vUkSA89sqJeKG0z6RtK9MWbWP4bo4YuBOws4KYXPjqhE3xycfGoyU3J1rZeeYRH4hCIoLT4KDt6TyAo1BCFJCPA7iUt-X0zrCg351nPiGeyyAqlarwJFh17wlFbQRK23w";

        AjaxServerCallAsync("POST", "/Accounts/CloseFiscalPeriod/", requestData, requestVerificationToken, function (response) {
            var closedPeriodDetails = response.response;

            if (response.status) {
                var fiscalPeriodNo = closedPeriodDetails.fiscalPeriodNo;
                var openDate = closedPeriodDetails.openDate;
                var closeDate = closedPeriodDetails.closeDate;
                var isActive = closedPeriodDetails.isActive === 1 ? "Yes" : "No";
                var isOpen = closedPeriodDetails.isOpen === 1 ? "Yes" : "No";

                let newRow = '<tr id="' + fiscalPeriodNo + '"><td data-title="Fiscal Period No">' + fiscalPeriodNo + '</td><td data-title="Open Date">' + formatDate(openDate) + '</td><td data-title="Close Date">' + formatDate(closeDate) + '</td> <td data-title="Is Active">' + isActive + '</td><td data-title="Is Open">' + isOpen + '</td></tr>';

                $("#" + fiscalPeriodID).replaceWith(newRow);
                Notify(!0, "Fiscal Period Closed Successfully");
            } else {
                Notify(!1, closedPeriodDetails);
            }
        });
    });

    $("input:radio[name=optPeriods]").change(function () {
        this.value == "1" ? LoadAllActivePeriods() : this.value == "2" ? LoadAllInactivePeriods() : this.value == "3" && LoadAllPeriods()
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