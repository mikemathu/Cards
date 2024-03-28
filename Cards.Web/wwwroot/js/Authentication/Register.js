    $(document).ready(function () {

        $("#RegisterForm").submit(function (event) {
            event.preventDefault();

            function validateUser() {
                var userData = {
                    UserName: $("#userName").val(),
                    Password: $("#userPassword").val()
                };

                document.getElementById("loader").style.display = "block";
                $("#btnLogin").prop("disabled", true);

                let requestData = userData;
                let token = $("#RegisterForm input[name=__RequestVerificationToken]").val();

                GetOrPostAsync("POST", "/Security/Register/", requestData, token)
                    .then(response => {
                        if (response.message === "Success") {
                            window.location.href = "/Accounts/GeneralLedgerAccounts";
                        }
                    })
                    .catch(error => {
                        document.getElementById("loader").style.display = "none";
                        $("#btnLogin").prop("disabled", false);
                        Notify(false, error);
                    });
            }

            validateUser();
        });


        function GetOrPostAsync(method, url, requestData, token) {
            return new Promise((resolve, reject) => {
                var retryCount = 5;

                (function makeRequest() {
                    $.ajax({
                        url: url,
                        type: method,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        cache: true,
                        data: JSON.stringify(requestData),
                        headers: {
                            RequestVerificationToken: token
                        },
                        success: (data) => {
                            resolve(data);
                        },
                        error: (xhr, status) => {
                            if (xhr.responseText === undefined || xhr.responseText === null) {
                                reject("Could not connect to the server. Please try again.");
                            } else if (status === "timeout") {
                                retryCount--;
                                if (retryCount > 0) {
                                    makeRequest();
                                } else {
                                    reject("Connection timed out. Please try again.");
                                }
                            } else if (xhr.statusText === "parsererror") {
                                reject("Something went wrong");
                            } else {
                                if (xhr.status === 401) {
                                    window.location.href = `/Security/LockScreen/?Username=${User}&BranchID=${CompanyBranchID}&ReturnUrl=${encodeURI(window.location.pathname)}`;
                                }
                                reject(xhr.responseText);
                            }
                        }
                    });
                })();
            });
        }
});