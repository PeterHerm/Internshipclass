$(document).ready(function () {

    $("#list").on("click", ".remove", function () {

        var $li = $(this).closest('li');
        var index = $li.index();

        console.log(`index=${index}`);
        console.log(`$li=${$li}`);

        $.ajax({
            method: "DELETE",
            url: `/Home/RemoveMember?index=${index}`,
            success: function (data) {

                $li.remove();

            },
            error: function (data) {
                alert(`Failed to remove`);
            },
        });
    })

});
