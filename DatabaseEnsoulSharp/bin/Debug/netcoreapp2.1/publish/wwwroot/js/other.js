var clicked = false;

function ClickLogin() {
    clicked = true;
}

function onSignIn(googleUser) {
    if (clicked) {
        var profile = googleUser.getBasicProfile();

        var model = {
            FirstName: profile.getName(),
            LastName: profile.getName(),
            Email: profile.getEmail()
        };
        Site.ajax("/Home/Login", JSON.stringify(model), function () {
            location.reload();
        });
    }
}

function signOut() {
    $("#logout").click();
}

$("#logout").click(function () {
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        Site.ajax("/Home/Logout", null, function () {
            location.reload();
        });
    });
});