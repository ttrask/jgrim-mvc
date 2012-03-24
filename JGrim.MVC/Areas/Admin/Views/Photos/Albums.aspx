<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<JGrim.MVC.Models.PhotoAlbum>" %>

<%@ Import Namespace="JGrim.MVC.Models" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="content5" ContentPlaceHolderID="HeadContent" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/PageSpecific/AdminMaster.js") %>' type="text/javascript"></script>
    <script type="text/javascript" src="http://gettopup.com/releases/latest/top_up-min.js"></script>
    <script src='<%= ResolveUrl("~/Scripts/jquery-loadmask/jquery.loadmask.min.js")%>'
        type="text/javascript"></script>
    <script type="text/javascript">

        var popupHeight = 480;
        var popupWidth = 640;
        var eventsRegistered = false;
        var disabledImgContainer;


        $(function () {


            $('div#ImageContainer').hover(
                function () {
                    $(this).find('div#Delete').show();
                },
                function () {

                    $(this).find('div#Delete').hide();
                }
            );


            /* Delete Image Event*/
            $('div#Delete a').click(function (event) {

                debugger;
                event.preventDefault();

                deleteLinkObj = $(this);

                $(this).closest("div#ImageContainer").block({ message: null });

                switch ($(this).attr("ID")) {
                    case "Enable":
                        $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                            window.location.reload();
                        });
                        break;
                    case "Delete":
                        $('#delete-dialog').dialog("open");
                        break;
                    default :
                        $(this).closest("div#ImageContainer").unblock();
                        break;
                }

                return false;
            });


            /*Delete Dialog Box*/
            $('#delete-dialog').dialog({
                autoOpen: false,
                width: 400,
                resizable: false,
                modal: true, //Dialog options
                buttons: {
                    "Delete": function () {
                        $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                            window.location.reload();
                        });
                        $(this).dialog("close");
                    },
                    "Disable": function () {
                        debugger;
                        $.post(deleteLinkObj[0].href.replace("Delete", "Disable"), function (data) {  //Post to action
                            window.location.reload();
                        });
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    $("div#ImageContainer").unblock();
                }
            });

            $('#Upload').click(function (event) {
                event.preventDefault();
                $('#dvUpload').dialog("open");
            });

            $('#Popup').dialog({
                resizable: false,
                position: "center",
                autoOpen: false,
                modal: true,
                width: "auto",
                dialogClass: "fixed",
                width: "auto",
                height: "auto",
                close: function () {
                    $(this).html("");
                    $(this).unblock();
                }
            });



            var maxPhotoDimension = 360;

            $('#Edit, #Details, #Upload').click(function (event) {
                $(this).block({ message: null });
                event.preventDefault();

                switch ($(this).attr("id")) {
                    case "Edit":
                        LoadPopupFromButton(event, this, "Edit Photo", "Popup", false);
                        break;
                    case "Details":
                        LoadPopupFromButton(event, this, "Photo Information", "Popup", false);
                        break;
                    case "Upload":
                        LoadPopupFromButton(event, this, "Upload", "Popup", false);
                        break;
                }
            });


        });

        function RegisterEvents(context) {

            var $close = $('button#btnClose, a#btnClose', context);
            if ($close.length > 0) {
                $close.unbind('click');
                $close.live('click', function (event) {
                    event.preventDefault();
                    $('div#Popup').dialog("close");
                });
            }

            var $edit = $('a#Edit', context);
            if ($edit.length > 0) {
                $edit.unbind("click");

                $edit.bind('click', function (event) {
                    LoadPopupFromButton(event, this, "Edit Photo", "Popup");
                });
            }

            var $details = $('a#Details', context);
            if ($details.length > 0) {
                $details.unbind("click");
                $details.bind('click', function (event) {
                    LoadPopupFromButton(event, this, "Photo Details", "Popup");
                });
            }
        }

        /*Fancy Popup for Uploading Images*/
        TopUp.addPresets({
            "a.tu_iframe_800x600": {
                type: "html",
                layout: "flatlook",
                modal: 1,
                overlayClose: 1,
                effect: "show",
                shaded: 0,
                onclose: function (event) {
                    window.location = window.location;
                }
            }
        });

    </script>
    <style type="text/css">
        div#Delete
        {
            height: auto;
        }
        
        div#ImageContainer
        {
            display: inline-block;
            width: 120px;
            height: 120px;
            min-height: 120px;
            min-width: 120px;
            vertical-align: middle;
            text-align: center;
            position: relative;
            padding: 5px;
            margin: 5px;
            border: 1px solid grey;
        }
        
        div#ImageContainer:hover
        {
            background-color: #eee;
        }
        
        
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="TitleContent" runat="server" ID="title">
    JGrim Admin >> Photo Album >>
    <%=Model.Name %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Subtitle" runat="server" ID="Content1">
    Album >>
    <%=Model.Name %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-top: 5px;">
        <%=Html.ActionLink("<<","index", "PhotoAlbums", null, new{ @class="button"}) %>
        <a href='<%=ResolveUrl("~/Scripts/jqFileUpload/upload.aspx?albumName="+Model.Name) %>'
            class="tu_iframe_800x600 button" id="element_0">Add Photos</a>
        <div>
            <% PagedList<Photo> photos = Model.GetPagedPhotos(Request.QueryString["page"]);  %>
            <%  Html.RenderPartial("Pager", photos); %>
            <div id='list'>
                <% foreach (JGrim.MVC.Models.Photo photo in photos)
                   { %>
                <div id="ImageContainer" <%if(!photo.IsActive){ Response.Write("class='disabled'");} %>>
                    <div style="margin-top: auto; margin-bottom: auto; width: 100%; height: 100%" id="Image">
                        <a id="Details" href='<% =ResolveUrl("~/admin/photos/details/"+photo.PhotoID)%>'
                            style="z-index: 0; width: 100%; height: 100%;">
                            <img alt="" src='<%=ResolveUrl("~/photos/ScaledImage?albumName="+Model.Name+"&imageName=" +photo.FileName +"&width=120&height=120")%>'
                                style="max-height: 120px; max-width: 120px;" />
                        </a>
                        <%=Html.HiddenFor(pa=>pa.Name) %>
                    </div>
                    <div id="Delete" class=' <% Response.Write(!photo.IsActive? "Enable" : "Delete"); %>'>
                        <a href='<%=Url.Action("Delete", new{ ID=photo.PhotoID})  %>' id="Delete">
                            <img src='<%=ResolveUrl("~/images/delete.png") %>' class="Delete" alt="delete" style='display: <% Response.Write(photo.IsActive? "inherit" : "none");
                                %>' />
                        </a><a href='<%=Url.Action("Enable", new{ ID=photo.PhotoID})  %>' id="Enable">
                            <img src='<%=ResolveUrl("~/images/enable.png") %>' class="Enable" alt="Re-enable"
                                style='display: <% Response.Write(photo.IsActive? "none" : "inherit"); %>' />
                        </a>
                    </div>
                </div>
                <% }%>
            </div>
        </div>
        <div id="delete-dialog" title="Delete Photo?">
            <p>
                Are you sure you want to delete this photo? Not only will the photo be deleted but
                any extra information you've added to the photo will also be removed. Alternatively,
                Disabling the image will prevent users from viewing it in this album but will keep
                the image on disk.
            </p>
        </div>
    </div>
    <div id="Popup">
    </div>
    <div id="tmpPopup" style="display: none; visibility: hidden;">
    </div>
</asp:Content>
