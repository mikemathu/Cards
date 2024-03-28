    $(document).ready(function () {
        function ReloadPage() {
            LoadAllVATTypes();
            LoadAllOtherTaxes();
            LoadAllLiabSubAccounts()
        }     

        function LoadAllVATTypes() {
            AjaxServerCallAsync("GET", "/Accounts/GetAllVATTypes/", "", "", function (response) {
                var vatTypes = response.response;
                if (response.status && !$.isEmptyObject(vatTypes)) {
                    vatTable.clear().draw();
                    var rowsToAdd = "";
                    for (var i = 0; i < vatTypes.length; i++) {
                        rowsToAdd += '<tr id="' + vatTypes[i].vatTypeID + '"><td data-title="VATType ID">' + vatTypes[i].vatTypeID + '</td><td data-title="Name">' + vatTypes[i].vatTypeName + '</td><td data-title="Per Rate (%)">' + vatTypes[i].perRate + '</td></tr>';
                    }
                    vatTable.rows.add($(rowsToAdd)).draw(false);
                }
            });
        }

    function GetVATTypeDetails(vatType) {
        var token = $("#VATTypeForm input[name=__RequestVerificationToken]").val();
        AjaxServerCallAsync("POST", "/Accounts/GetVATTypeDetails/", vatType, token, function (response) {
            var vatTypeDetails = response.response;
            if (response.status) {
                $("#VATType_VATTypeID").val(vatTypeDetails.vatTypeID);
                $("#VATType_Name").val(vatTypeDetails.vatTypeName);
                $("#VATType_VATLiabSubAccountID").val(vatTypeDetails.vatLiabSubAccountID);
                $("#VATType_PerRate").val(vatTypeDetails.perRate);
                $("#addvattype").hide();
                $("#updatevattype").show();
            }
        });
    }

    function DeleteVATType(vatTypeIDToDelete) {
        var token = $("#VATTypeForm input[name=__RequestVerificationToken]").val();
        AjaxServerCallAsync("POST", "/Accounts/DeleteVATType/", vatTypeIDToDelete, token, function (response) {
            var responseData = response.response;
            if (response.status) {
                $("#vattypesstable #" + vatTypeIDToDelete).remove();
                Notify(true, "VAT Type Deleted Successfully");
            } else {
                Notify(false, responseData);
            }
        });
    }

    function LoadAllOtherTaxes() {
        AjaxServerCallAsync("GET", "/Accounts/GetAllOtherTaxes/", "", "", function (response) {
            var taxData = response.response;
            if (response.status && !$.isEmptyObject(taxData)) {
                taxTable.clear().draw();
                var rowsToAdd = "";

                for (var i = 0; i < taxData.length; i++) {
                    var tax = taxData[i];
                    rowsToAdd += '<tr id="' + tax.otherTaxID + '"><td data-title="OtherTax ID">' + tax.otherTaxID + '</td><td data-title="Name">' + tax.otherTaxName + '</td><td data-title="Per Rate (%)">' + tax.perRate + '</td></tr>';
                }

                taxTable.rows.add($(rowsToAdd)).draw(false);
            }
        });
    }

    function GetOtherTaxDetails(otherTaxData) {
        var token = $("#OtherTaxForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/GetOtherTaxDetails/", otherTaxData, token, function (response) {
            var otherTaxDetails = response.response;

            if (response.status) {
                $("#OtherTax_OtherTaxID").val(otherTaxDetails.otherTaxID);
                $("#OtherTax_Name").val(otherTaxDetails.otherTaxName);
                $("#OtherTax_VATLiabSubAccountID").val(otherTaxDetails.vatLiabSubAccountID);
                $("#OtherTax_PerRate").val(otherTaxDetails.perRate);
                $("#addothertax").hide();
                $("#updateothertax").show();
            }
        });
    }

    function DeleteOtherTax(otherTaxID) {
            var token = $("#OtherTaxForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/DeleteOtherTax/", otherTaxID, token, function (response) {
            var responseData = response.response;

            if (response.status) {
                $("#othertaxesstable #" + otherTaxID).remove();
                Notify(true, "Other Tax Deleted Successfully");
            } else {
                Notify(false, responseData);
            }
        });
    }

    function LoadAllLiabSubAccounts() {
        AjaxServerCallAsync("GET", "/Accounts/GetAllLiabilitySubAccounts/", "", "", function (n) {
            var i = n.response, r, t;
            if (n.status && !$.isEmptyObject(i)) {
                for (r = $("#VATType_VATLiabSubAccountID"),
                    r.empty(),
                    r.append("<option><\/option>"),
                    t = 0; t < i.length; t++) {
                    let n = new Option(i[t].subAccountName, i[t].subAccountID);
                    r.append(n)
                }
                let n = $("#VATType_VATLiabSubAccountID option");
                n.clone().appendTo("#OtherTax_VATLiabSubAccountID")
            }
        })
    }
    const vatTable = $("#vattypesstable").DataTable({
        drawCallback: function () {
            $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
            $.contextMenu({
                selector: "#vattypesstable tbody tr td",
                trigger: "right",
                delay: 500,
                autoHide: !0,
                callback: function (n, t) {
                    var i = t.$trigger[0].parentElement.id, r;
                    switch (n) {
                        case "select":
                            GetVATTypeDetails(i);
                            break;
                        case "delete":
                            r = confirm("Are You Sure You Want To Delete The Selected VAT Type?");
                            r === !0 && DeleteVATType(i)
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
        searching: !1,
        ordering: !1,
        bInfo: !1,
        scrollY: "150px",
        sScrollX: "100%",
        scrollX: !0
    });
    vatTable.on("select", function (event, dt, type, indexes) {
        if (type === "row") {
            let selectedRowsData = vatTable.rows(indexes).data().toArray();
            let rowId = selectedRowsData[0].DT_RowId;
            if (rowId !== undefined) {
                GetVATTypeDetails(rowId);
            }
        }
    });
    const taxTable = $("#othertaxesstable").DataTable({
            drawCallback: function() {
                $.contextMenu("destroy", `#${$(this).prop("id")} tbody tr td`);
                $.contextMenu({
                    selector: "#othertaxesstable tbody tr td",
                    trigger: "right",
                    delay: 500,
                    autoHide: !0,
                    callback: function(n, t) {
                        var i = t.$trigger[0].parentElement.id, r;
                        switch (n) {
                            case "select":
                                GetOtherTaxDetails(i);
                            break;
                            case "delete":
                                r = confirm("Are You Sure You Want To Delete The Selected Tax?");
                                r === !0 && DeleteOtherTax(i)
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
            searching: !1,
            ordering: !1,
            bInfo: !1,
            scrollY: "150px",
            sScrollX: "100%",
            scrollX: !0
    });

    taxTable.on("select", function (event, dt, type, indexes) {
        if (type === "row") {
            let selectedRowsData = taxTable.rows(indexes).data().toArray();
            let rowId = selectedRowsData[0].DT_RowId;
            if (rowId !== undefined) {
                GetVATTypeDetails(rowId);
            }
        }
    });
    

    vatTable.clear().draw();
    taxTable.clear().draw();
    ReloadPage();
    $("#VATType_VATTypeID").val("0");
    $("#OtherTax_OtherTaxID").val("0");

    $("#VATTypeForm").submit(function (event) {
        event.preventDefault();

        var VATTypeID = $("#VATType_VATTypeID").val();
        var button = Ladda.create(document.querySelector("#btnaddvattype"));
        if (VATTypeID > 0) {
            button = Ladda.create(document.querySelector("#btnupdatevattype"));
        }

        button.start();
        button.isLoading();
        button.setProgress(-1);

        var data = {
            VATTypeID: $("#VATType_VATTypeID").val(),
            VATTypeName: $("#VATType_Name").val(),
            VATLiabSubAccountID: $("#VATType_VATLiabSubAccountID").val(),
            PerRate: $("#VATType_PerRate").val()
        };

        var requestVerificationToken = $("#VATTypeForm input[name=__RequestVerificationToken]").val();

        AjaxServerCallAsync("POST", "/Accounts/CreateUpdateVATType/", data, requestVerificationToken, function (response) {
            if (response.status) {
                var vatTypeRow = '<tr id="' + response.response.vatTypeID + '"><td data-title="VATType ID">' + response.response.vatTypeID + '<\/td><td data-title="Name">' + response.response.vatTypeName + '<\/td><td data-title="Per Rate (%)">' + response.response.perRate + "<\/td><\/tr>";

                if (VATTypeID > 0) {
                    $("#vattypesstable #" + VATTypeID).replaceWith(vatTypeRow);
                    Notify(true, "VAT Type Updated Successfully");
                } else {
                    $("#vattypesstable").append(vatTypeRow);
                    Notify(true, "VAT Type Created Successfully");
                    $("#btnCreateNewVATType").click();
                }
                button.stop();
            } else {
                button.stop();
                Notify(false, response.response);
            }
        });
    });

    $("#btnCreateNewVATType").click(function () {
        $("#VATTypeForm")[0].reset();
        $("#VATType_VATTypeID").val("0");
        $("#updatevattype").hide();
        $("#addvattype").show()
    })

    $("#OtherTaxForm").submit(function (event) {
        event.preventDefault();

        var otherTaxID = $("#OtherTax_OtherTaxID").val();
        var laddaButton = otherTaxID > 0 ?
        Ladda.create(document.querySelector("#btnupdateothertax")) :
        Ladda.create(document.querySelector("#btnaddothertax"));

        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var requestData = {
            OtherTaxID: $("#OtherTax_OtherTaxID").val(),
            OtherTaxName: $("#OtherTax_Name").val(),
            VATLiabSubAccountID: $("#OtherTax_VATLiabSubAccountID").val(),
            PerRate: $("#OtherTax_PerRate").val()
        };

        var token = $("#OtherTaxForm input[name=__RequestVerificationToken]").val();
        AjaxServerCallAsync("POST", "/Accounts/CreateUpdateOtherTax/", requestData, token, function (response) {
            var responseData = response.response;

            if (response.status) {
                var newRow = '<tr id="' + responseData.otherTaxID + '"><td data-title="OtherTax ID">' + responseData.otherTaxID + '</td><td data-title="Name">' + responseData.otherTaxName + '</td><td data-title="Per Rate (%)">' + responseData.perRate + '</td></tr>';

            if (otherTaxID > 0) {
                $("#othertaxesstable #" + otherTaxID).replaceWith(newRow);
                Notify(true, "Other Tax Updated Successfully");
            } else {
                $("#othertaxesstable").append(newRow);
                Notify(true, "Other Tax Created Successfully");
                $("#btnCreateNewOtherTax").click();
            }
                laddaButton.stop();
            } else {
                laddaButton.stop();
                Notify(false, responseData);
            }
        });
    });

    $("#btnCreateNewOtherTax").click(function () {
        $("#OtherTaxForm")[0].reset();
        $("#OtherTax_OtherTaxID").val("0");
        $("#updateothertax").hide();
        $("#addothertax").show()
    })

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