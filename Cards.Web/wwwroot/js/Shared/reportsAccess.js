$(document).ready(function () {

    $("#EnterAccessCodeForm").submit(function (event) {
        var laddaButton;
        event.preventDefault();

        laddaButton = Ladda.create(document.querySelector("#btnenteraccesscode"));
        laddaButton.start();
        laddaButton.isLoading();
        laddaButton.setProgress(-1);

        var verificationToken = $("#EnterAccessCodeForm input[name=__RequestVerificationToken]").val();
        var accessCodeValue = $("#EnterAccessCode").val();

        GetOrPostAsync("POST", "/Security/AuthenticateAccessPassword/", accessCodeValue, verificationToken)
            .then(() => {
                laddaButton.stop();
                window.location.href = "/Dashboard/Accounts/";
            })
            .catch((error) => {
                Notify(!1, error);
                laddaButton.stop();
            });
    });


});