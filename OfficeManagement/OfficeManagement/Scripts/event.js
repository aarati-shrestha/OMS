function SaveEvent() {
    $.ajax({
        type: "POST",
        url: '/Event/CreateEvent',
        data: $("#event").serialize(),
        success: function (data) {
            alert(data);
        },
        error: function () {
            alert("failed!! try again later");
        }
    });
}