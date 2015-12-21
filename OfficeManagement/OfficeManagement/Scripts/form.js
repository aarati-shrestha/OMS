$(document).ready(function () {
    var d=new Date();
    $('#MeetingDate').attr({
        min: d.getDate()
    });
    //$('#chkAction').change(function() {
    //    if($('#chkAction').is(':checked')) {
    //        alert("checked");
    //    }
    //});

});

var formlist = [];
function CheckAll()
{
   
    if ($('#chkAction').is(':checked'))
    {
        $('#btnDelete').show();
        $("input[type=checkbox]").each(function () {
            $(this).attr("checked", true);
        });
    }
    else {
        $('#btnDelete').hide();
        $("input[type=checkbox]").each(function () {
            $(this).attr("checked", false);
        });
    }

    GetCheckedItemsFormId();

}

function GetCheckedItemsFormId()
{
    $('#btnDelete').show();
    formlist.length = 0;
    $('#manageUsers tr').each(function () {
        
        if ($(this).find("input").is(":checked"))
        {
            var id = $(this).find("td:first").text();
            formlist.push(id);
        }
    });
    
}

function DeleteForm()
{
    if (formlist.length == 0)
    {
        alert("You have not selected any data");
    }
    else
    {
        $.ajax
       ({
           type: "POST",
           url: '/Home/DeleteFormByFormId',
           data: JSON.stringify({ formId: formlist }),
           contentType: "application/json; charset=utf-8",
           success: function (data) {
               data = data.toLowerCase()
               if (data == "true") {
                   alert("Successfully Deleted");
               }
               else {
                   alert("You have not selected any data");
               }
               window.location.href = '/Form';
           },
           error: function () {

           }

       });
    }
   
}
function UpdateForm() {
    $.ajax({
        type: "POST",
        url: '/Form/UpdateForm',
        data: $("#formDetail").serialize(),
        //dataType: "json",

        success: function (data) {
            alert("Successful");
        },
        error: function () {
            alert("Failed! Please try again.");
        }
    });//


}



function InsertFormStatus(isApproved) {
    var status = isApproved;
    $.ajax({
        type: "POST",
        url: '/Form/AddFormStatus',
        data: { formId: parseInt($('#formId').val()), isApproved: parseInt(isApproved), statusDescription: $('#commentField').val() },
        //dataType: "json",

        success: function (data) {
            if (data == "True")
            {
                    $("#lastComment").show();
                    $("#comment").text($('#commentField').val());
                   // $("#commentField").val() = "";
                    if(status==1)
                    {
                        $("#isApproved").text("Yes");
                    }
                    else {
                        $("#isApproved").text("No");
                    }              
            }
            else
            {
                alert("Error occur while saving form");
            }
        },
        error: function () {
            alert("Failed! Please try again.");
        }
    });//


}

function GetReceivedForm()
{
    $.ajax({
        type: "POST",
        url: '/Home/GetReceivedFrom',
        data: { searchString: $('#txtReceivedSearch').val() },
        success: function(data)
        {
            $("#receivedFormDiv").html(data);
        },
        error: function()
        {
            alert("Failed! Try again later");

        }
    });
}
function GetSentForm() {
    $.ajax({
        type: "POST",
        url: '/Home/GetSentForm',
        data: { searchString: $('#txtSentSearch').val() },
        success: function (data) {
            $("#sentFormDiv").html(data);
        },
        error: function () {
            alert("Failed! Try again later");

        }
    });
}

