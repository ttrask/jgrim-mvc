
function showAddPage() {


    $('#pageName').removeAttr('disabled');
    $('#pageName').val("");
    $('#pageUrl').val("");
    $('#pageContent').val("");

    $('#btnSavePage').attr('value', 'Save');
    $('#dvPageEdit').dialog(
                {
                    modal: true,
                    width: 600,

                    title: 'Add Page'
                });
}

function showEditPage($pageGridItem) {


    $('#pageName').attr('disabled', 'disabled');
    $('#pageName').val($pageGridItem.find('.pageName').text());
    $('#pageUrl').val($pageGridItem.find('.pageUrl').text());
    $('#pageContent').val($pageGridItem.find('.pageContent').text());

    $('#btnSavePage').attr('value', 'Update');

    $('#dvPageEdit').dialog(
                {
                    modal: true,
                    width: 600,

                    title: 'Edit Page: ' + $pageGridItem.find('.pageName').html()
                });

}


$('.pageContent').css('display', "none");

$('th').each(function () {
    if ($.trim($(this).text()) == "") $(this).css('display', 'none');
});

$('.pageName').click(function () {
    showEditPage($(this).parent());
});



$('#btnSavePage').click(function () {


    if ($(this).attr('value') == "Save") {
        JGrim.admin.AjaxService.SavePageContent($('#pageName').val(), $('#pageUrl').val(), $('#pageContent').val(),
                    function () {
                        location.reload();
                    },

                    function () {
                        $('#failure').html("<div > Something went wrong with saving your page!</div>").dialog({ title: "Oh Noes!" });
                    }
                );
    }
    else {
        JGrim.admin.AjaxService.UpdatePageContent($('#pageName').val(), $('#pageUrl').val(), $('#pageContent').val(),
                function () {
                    location.reload();
                },

                function () {
                    $('#failure').html("<div > Something went wrong with saving your page!</div>").dialog({ title: "Oh Noes!" });
                }
            );
    }
});