
$(document).ready(function () {

    $(".delete").click(function () {

        var newcomerId = $(this).attr('memberId');
        console.log($(".member"));

        $.ajax({
            url: `/Home/RemoveMember/${newcomerId}`,
            type: 'DELETE',
            success: function (data) {
                $(".member").eq(newcomerId).remove();
            },
            error: function (data) {
                alert(`Failed to remove ${newcomerId}`);          
            },
        });
    })    
    
});
