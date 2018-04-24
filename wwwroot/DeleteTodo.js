debugger;
$(document).ready(function () {

    $('#todoId').text(localStorage.todoId); // use "val" instead of "text"
    $('#name').text(localStorage.todoName);
    $('#iscomplete').text(localStorage.getItem("todoIsComplete"));
});

$("#deleteTodo").click(function (e) {

    // var todoId = document.getElementById("todoId").value;
    var todoId = localStorage.todoId;
    $.ajax({
        contentType: 'application/json',
        type: "DELETE",
        url: "api/todo/" + todoId,
        // data: JSON.stringify({
        //    Id: id,
        //    name: document.getElementById("name").value,
        //    isComplete: document.getElementById("iscomplete").value
        // }),
        success: function (data, textStatus, jqXHR) {
            $("#showDeleteResult").text(jqXHR.statusText);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#showDeleteResult").text(jqXHR.statusText);
        }
    });
});