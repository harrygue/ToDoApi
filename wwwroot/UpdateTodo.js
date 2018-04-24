// get Todo by Id
// constructor function
// var Id =3; // comes from GetTodos

$(document).ready(function () {

    $('#id').val(localStorage.todoId); // use "val" instead of "text"
    $('#name').val(localStorage.todoName);
    $('#iscomplete').val(localStorage.getItem("todoIsComplete"));
});

$("#updateTodo").click(function (e) {
   
    var id = document.getElementById("id").value;
    var _name = document.getElementById("name").value;
    var _isComplete = document.getElementById("iscomplete").value;
    // var id = localStorage.todoId;
  
    $.ajax({
        contentType: 'application/json',
        type: "PUT",
        url: "api/todo" + id,
        data: JSON.stringify({
           Id: id,
           name: _name, // document.getElementById("name").value,
           isComplete: _isComplete // document.getElementById("iscomplete").value
        }),
        success: function (data, textStatus, jqXHR) {
            // $("#showUpdateResult").text("Updated Todo: " + jqXHR.responseText);
            $("#showUpdateResult").text("Updated Todo:" & data.id & " / " + data.name & " / " & data.isComplete);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#showUpdateResult").text(jqXHR.statusText);
        }
    });
});