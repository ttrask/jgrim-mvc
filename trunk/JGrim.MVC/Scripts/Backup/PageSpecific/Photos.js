



$(document).ready(function () {


    //loads Classig Galleria theme
    
    
    Galleria.loadTheme(galleriaThemeUrl);

    $('div#breadCrumbs').show();
    $('#PhotoGallery').galleria();
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