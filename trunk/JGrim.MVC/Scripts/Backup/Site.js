var urlParams = {};
(function () {
    var e,
                    a = /\+/g,  // Regex for replacing addition symbol with a space
                    r = /([^&;=]+)=?([^&;]*)/g,
                    d = function (s) { return decodeURIComponent(s.replace(a, " ")); },
                    q = window.location.search.substring(1);
    while (e = r.exec(q))
        urlParams[d(e[1])] = d(e[2]);
})();

var pageHeight = "520px";

jQuery(document).ready(function () {
    $("#NavigationMenu a").each(function () {
        $(this).addClass("current");
    });

    
    $("td.main").each(function () {

        $(this).height(pageHeight);
    });

    $('.menu li').hover(
                function () {

                    $(this).find('div').addClass('MenuHover');
                },
                function () {

                    $(this).find('div').removeClass('MenuHover');
                }
            );

});