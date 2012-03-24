



$(document).ready(function () {


    //loads Classig Galleria theme
    var descriptionHoverFade = 0.7;

    Galleria.loadTheme(galleriaThemeUrl);

    $('div#breadCrumbs').show();
    $('#PhotoGallery').galleria({
        showTip: true,
        dataConfig: function (img) {
            
            var imageSource = $(img).attr('src').split("?")[0];
            
            return {
                description: $(img).attr('alt'),
                thumb: $(img).attr('src'),
                image: imageSource
            };
        },
        extend: function (options) {

            $('.galleria-image>img', '#PhotoGallery').click(function () {

            });

            $('.galleria-info', '#PhotoGallery').each(
            function (img) {
                $(this).css({ opacity: 0.3 });
            })
            .hover(
                function () {
                    $(this).fadeTo("fast", descriptionHoverFade);
                },
                function () {
                    $(this).fadeTo("fast", 0.3);
                }
            );
        }

    });





    $('#PhotoGallery').show();


    $('#breadCrumbs').each(function () {

        var zIndex = $(this).find('.breadCrumbLink').length * 4 + 4;

        $(this).find('.breadCrumbLink').each(function () {
            zIndex--;
            $(this).find('.arrow').css('z-index', zIndex);
            zIndex--;
            $(this).find('.arrow-border').css('z-index', zIndex);
            zIndex--;
            $(this).find('.arrow-bg').css('z-index', zIndex);
            zIndex--;
            $(this).css('z-index', zIndex);
        });


        $(this).find('.breadCrumbLink:last').each(function () {

            $(this).css('background-color', '#888').css('left', $(this).position.left);
        });

        $(this).find('.arrow:last').css('border-color', 'transparent transparent transparent #888');
    });
});