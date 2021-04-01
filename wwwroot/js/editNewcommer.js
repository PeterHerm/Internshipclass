$(document).ready(function () {

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var index = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("memberIndex", index);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        console.log('submit changes to server');

        var newName = $('#classmateName').val();
        var index = $('#editClassmate').attr("memberIndex");

        console.log(`/Home/UpdateMember?index=${index}&name=${newName}`);

        $.ajax({
            method: "PUT",
            url: `/Home/UpdateMember?index=${index}&name=${newName}`,
            success: function (data) {
                $('.name').eq(index).replaceWith(newName);
            },
            error: function (data) {
                alert(`Failed to update`);
            },
        });
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })

});
