var ajaxDefaults = {};

ajaxDefaults.base = {
    type: "POST",

    dataFilter: function (data) {
        //see http://encosia.com/2009/06/29/never-worry-about-asp-net-ajaxs-d-again/ 
        data = JSON.parse(data); //use the JSON2 library if you aren’t using FF3+, IE8, Safari 3/Google Chrome 
        return data.hasOwnProperty("d") ? data.d : data;
    },

    error: function (xhr) {
        //see 
        if (!xhr) return;
        if (xhr.responseText) {
            var response = JSON.parse(xhr.responseText);
            //console.log works in FF + Firebug only, replace this code 
            if (response) console.log(response);
            else console.log("Unknown server error");
        }
    }
};

ajaxDefaults.json = $.extend(ajaxDefaults.base, {
    //see http://encosia.com/2008/03/27/using-jquery-to-consume-aspnet-json-web-services/ 
    contentType: "application/json; charset=utf-8",
    dataType: "json"
});

var ops = {
    baseUrl: "/Services/AjaxService.svc/",


    LoadImagesToDB: function () {
        var ajaxOpts = $.extend(ajaxDefaults.json, {

            url: ops.baseUrl + "LoadImagesToDB",
            success: function (msg) {
                alert("Successfully uploaded " + msg+" pictures.");
             },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("failure:" + jqXHR.statusText);
            }

        });

        $.ajax(ajaxOpts);
        return false;
    }
}

