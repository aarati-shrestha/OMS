var userIdList = [];
var sDate, eDate,uId;
function UpdateUser() {
    $.ajax({
        type: "POST",
        url: '/User/UpdateUser',
        data: $("#userDetail").serialize(),
        //dataType: "json",

        success: function (data) {
            if (data == 0)
                alert("Successful");
            else
                alert("Unsucessful");
        },
        error: function () {
            alert("Failed! Please try again.");
        }
    });
}

function ChangePassword() {
    if ($('#currentPassword').val() != $('#Password').val()) {
        alert("Password incorrect");
    }
    else {
        $.ajax({
            type: "POST",
            url: '/User/UpdatePassword',
            data: { userId: parseInt($('#UserId').val()), password: $('#newPassword').val() },

            success: function (data) {
                alert("Password change:" + data);
            },
            error: function () {
                alert("Error");
            }


        });
    }
}

function GetUser() {
    $.ajax({
        type: "POST",
        url: '/User/GetUserByUsername',
        data: { searchString: $('#txtSearch').val() },
        success: function (data) {
            $("#userListDiv").html(data);
        },

        error: function () {
            alert("Failed! Please try again.");
        }

    });


    
}



function ShowLeaveRecord() {
    uId = $("#userId").val();
    sDate =$("#txtStartDate").val();
    eDate = $("#txtEndDate").val();
    $.ajax({
        type: "POST",
        url: '/User/GetLeaveReport',
        data: { userId: $("#userId").val(), startDate: $("#txtStartDate").val(), endDate: $("#txtEndDate").val() },
        success:function(data)
        {
            $("#divLeaveList").html(data);
        },
        error: function()
        {

        }

    });
}

function DownloadPdf() {
    $.ajax({
        type: "POST",
        url: '/User/GetPdf',
        data: { userId: uId, startDate: sDate, endDate: eDate },
        success: function (data) {
            alert("Downloaded");
        },
        error: function () {

        }

    });
}




function CheckAll() {
    if($("#chkAction").is(':checked'))
    {
        $('#btnDelete').show();
        $("input[type=checkbox]").each(function ()
        {
            $(this).attr("checked", true);
        });
    }

   
    else
    {
        $('#btnDelete').hide();
        $("input[type=checkbox]").each(function () {
            $(this).attr("checked", false);
        });
    }
    GetCheckedItemsUserId();
}



function GetCheckedItemsUserId()
{
    userIdList=[];
    $("#userList tr").each(function () {
        if($(this).find("input").is(":checked"))
        {
            $('#btnDelete').show();
            var userId = $(this).find("td:first").text();
            userIdList.push(userId);
        }
    });

}

function DeleteCheckedUser()
{
    if (userIdList.length == 0)
    {
        alert("You have not selected any data");
    }
    else
    {
        $.ajax({
            type: "POST",
            url: '/User/DeleteUserById',
            data: JSON.stringify({ userId: userIdList }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert(data);
                GetUser();
            },
            error: function () {
                alert("Failed! Try again later.")
            }
        });
    }
   
}