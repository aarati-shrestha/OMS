$(document).ready(function () {

    $("#btnLogin").click(function ()
    {
        if(ViewBag.ErrorMessage!= null)
        {
            alert("Username/password is incorrect");
        }
    });
});