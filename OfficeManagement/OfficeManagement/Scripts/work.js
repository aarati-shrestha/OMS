


var workIdList = [];



function UpdateWork()
{   
    $.ajax({
        type: "POST",
        url: '/Work/UpdateWork',
        data: $("#workDetail").serialize(),
        success: function (data) {
            if (data == 0)
            {
                alert("Problem in saving data");
            }
            else
            {
                alert("Successful");
            }           
        },
        error: function () {
            alert("Failed! Please try again.");
        }
    });
}

function InsertWorkUserStatusAndUpdateWork()
{
    $.ajax(
        {
            type: "POST",
            url: '/Work/InsertWorkUserStatusUpdateWork',
            data: $("#WorkUserStatus").serialize(),
            success:function(data)
            {
                $("#lastFormUserStatus").show();
                $("#assignedUser").text($("#WorkUserStatus_AssignedUserId").find('option:selected').text());
                $("#remark").text($("#WorkUserStatus_Remark").val());
            },
            error: function()
            {

            }
        });
}

function GetCheckedItemsWorkId() {
    workIdList = [];
    $("#workList tr").each(function () {
        if ($(this).find("input").is(":checked")) {
            $('#btnDelete').show();
            var workId = $(this).find("td:first").text();
            workIdList.push(workId);
        }
    });

}
function DeleteCheckedWork() {

    $.ajax({
        type: "POST",
        url: '/Work/DeleteWorkAndWorkUserStatus',
        data: JSON.stringify({ workIds: workIdList }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            data=data.toLowerCase()
            if (data == "true")
            {
                alert("Successfully Deleted");
            }
            else
            {
                alert("You have not selected any data");
            }
            getWork(1);
        },
        error: function () {
            alert("Failed! Try again later.")
        }
    });
}

function getWork(currentPage)
{
    $.ajax({
        type: "POST",
        url: '/Work/GetWorkByTitle',
        data: { search: $("#txtSearch").val(), assignedOrCreated: $("#assignedOrCreatedDDL").val(), currentPage: currentPage },
        success: function (data)
        {
            $("#divWorkList").html(data);
        },
        error: function()
        {
            alert("error");
        }
    });
}

function CheckAll() {
    if ($("#chkAction").is(':checked')) {
        $('#btnDelete').show();
        $("input[type=checkbox]").each(function () {
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
    GetCheckedItemsWorkId();
}