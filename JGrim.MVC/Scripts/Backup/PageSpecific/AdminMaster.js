
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




function LoadDefaults(){
    $('button, input:submit, a.button').button();
}

$(document).ready(function () {
    LoadDefaults();
});


function LoadPopupFromButton(event, clickedButton, title, popupControlID) {
    LoadPopupFromButton(event, clickedButton, title, popupControlID, true); 
}

function LoadPopupFromButton(event, clickedButton, title, popupControlID, blockUI) {
    event.preventDefault();
    
    var $clickedButton = $(clickedButton);

    if(blockUI) $clickedButton.block({ message: null });

    $href = $clickedButton.attr("href") ? $clickedButton.attr("href") : $clickedButton.find('a').attr("href");

    if ($href) {
        $('#' + popupControlID).load($href,
                         function () {
                             debugger;
                             if (typeof (LoadDefaults) == "function") LoadDefaults();
                             if (typeof (LoadPopupEvents) == "function") LoadPopupEvents();
                             if (typeof (RegisterEvents) == "function") RegisterEvents();
                             $clickedButton.unblock();
                             $(this).height($(this).height() > $(this).scrollHeight ? $(this).height() : $(this).scrollHeight);
                             $('#' + popupControlID).dialog("close");
                             $('ui-dialog-title').text(title);

                             $('#' + popupControlID).dialog("open", "option", "title", title);
                         }
                    );
    };
};