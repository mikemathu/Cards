function Notify(n, t) {
    t = t.substring(0, 100);
    var i = n ? new Noty({
        text: t,
        type: "success",
        theme: "metroui",
        closeWith: ["click", "button"]
    }).show() : new Noty({
        text: t,
        type: "error",
        theme: "metroui",
        closeWith: ["click", "button"]
    }).show()
}

"Notification" in window && Notification.requestPermission(function () {
    return
});
$("#showpswd").on("click", function () {
    $(this).is(":checked") ? $("#userPassword").attr("type", "text") : $("#userPassword").attr("type", "password")
});