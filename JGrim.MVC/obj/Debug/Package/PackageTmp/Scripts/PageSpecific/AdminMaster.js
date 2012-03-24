
var urlParams = {};
(function () {
    var e,
                    a = /\+/g,  // Regex for replacing addition symbol with a space
                    r = /([^&;=]+)=?([^&;]*)/g,
                    d = function (s) { return decodeURIComponent(s.replace(a, " ")); },
                    q = window.location.search.substring(1);
    while (e = r.exec(q))
        urlParams[d(e[1])] = d(e[2]);
});




function LoadDefaults() {
    $('button, input:submit, a.button').button();
}

$(document).ready(function () {
    LoadDefaults();


    $('.jqButton').each(function () {
        $(this).button();
    });
});


function LoadPopupFromButton(event, clickedButton, title, popupControlID) {
    LoadPopupFromButton(event, clickedButton, title, popupControlID, true);
}

function LoadPopupFromButton(event, clickedButton, title, popupControlID, blockUI) {
    event.preventDefault();

    var $clickedButton = $(clickedButton);
    var $availableIDs;

    if (blockUI) $clickedButton.block({ message: null });

    $href = $clickedButton.attr("href") ? $clickedButton.attr("href") : $clickedButton.find('a').attr("href");

    if ($href) {
        var $btnID = $(clickedButton).attr("ID");

        var $dialog = $('#' + popupControlID);

        if (($dialog).find('div#' + $btnID).length == 0) {
            var $dlgContent = $('#tmpPopup');

            if($dlgContent.length==0)
            {
                $dialog.parent().append('<div id="tmpPopup" style="display:none;" />');
            }
            $dlgContent = $('#tmpPopup');

            $dlgContent.load($href,
                         function () {
                             $dialog.find('div').hide();
                             $dialog.append($dlgContent.html());
                             if (typeof (LoadDefaults) == "function") LoadDefaults();
                             if (typeof (RegisterEvents) == "function") RegisterEvents($dialog);
                             if (typeof (LoadPopupEvents) == "function") LoadPopupEvents();


                             //$(this).height($(this).height() > $(this).scrollHeight ? $(this).height() : $(this).scrollHeight);

                             $dialog.dialog("open", "option", "position", "center");
                             $dialog.find('img').load(function () {
                                 $dialog.parent().css("top", ($(window).outerHeight() - $dialog.offsetHeight) / 2 + $(window).scrollTop() + "px");
                             });
                             $('.ui-dialog-title', $dialog.parent()).text(title);

                             $clickedButton.unblock();
                         }
                    );
        }
        else {

            $dialog.find('div').each(function (index, div) {
                if ($(div).parent().get(0) == $dialog.get(0)) {
                    $(div).hide();
                }
            });

        
            $dialog.find('div#' + $btnID + ':gt(0)').remove();
            //$dialog.find('div#' + $btnID)[0].find("div").show().css("display", "");
            $dialog.find('div#' + $btnID).each(function () {
                $(this).show();
                $(this).css("display", "inline-table");
                $(this).find("div").css('display', '');
            });


            $dialog.dialog("open", "option", "position", "center");
            $dialog.find('img').load(function () {
                $dialog.parent().css("top", ($(window).outerHeight() - $dialog.offsetHeight) / 2 + $(window).scrollTop() + "px");
            });
            $('.ui-dialog-title', $dialog.parent()).text(title);
        }
    };
};