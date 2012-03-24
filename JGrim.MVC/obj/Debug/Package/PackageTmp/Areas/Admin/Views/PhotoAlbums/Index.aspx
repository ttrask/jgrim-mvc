<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<JGrim.MVC.Models.PhotoAlbum>>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src='<%=ResolveUrl( "~/Scripts/jquery.fileinput.js") %>' type="text/javascript"></script>
    <script type="text/javascript">

        var photoAlbumObj;
        var eventsRegistered = false;

        function LoadImagesIntoDB() {

            var url = '<%=ResolveUrl("~/Services/AjaxService.SVC/LoadImagesToDB") %>';
            $('#btnLoadImages').disabled = true;
            
            ops.LoadImagesToDB();
        }

        function onMethodCompleted(results){
            alert(results);
        }

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
                    LoadPopupFromButton(event, this, "Edit Album", "Popup");
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

        $(document).ajaxStop($.unblockUI);

        $(document).ready(function () {

            $('#enable-dialog').dialog({
                autoOpen: false,
                width: 400,
                resizable: false,
                modal: true, //Dialog options
                buttons: {
                    "Enable": function () {
                        $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                            window.location.reload();
                        });
                        $(this).dialog("close");
                    },
                     "Delete": function () {
                        $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                            window.location.reload();
                        });
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                }
            });

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
                        $.post(deleteLinkObj[0].href.replace("Delete", "Disable"), function (data) {  //Post to action
                            window.location.reload();
                        });
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                }
            });

            $('div.photoAlbumButton').hover(
                function () {
                    $(this).find('#Delete, #Enable').show();

                },
                function () {
                    $(this).find('#Delete, #Enable').hide();
                }
            );

            $('#Popup').dialog({
                resizable: false,
                position: "center",
                autoOpen: false,
                modal: true,
                width: "auto",
                dialogClass: "fixed",
                width: 480,
                close: function () {
                    $(this).html("");
                    $(this).unblock();
                }
            });


            $('div#Delete a, div#Enable a').click(function (event) {

                event.preventDefault();

                deleteLinkObj = $(this);

                switch($(this).attr("ID")){
                    case "Enable":
                        $('#enable-dialog').dialog("open");
                        break;
                    case "Delete":
                        $('#delete-dialog').dialog("open");
                        break;        
                };
                
                return false;
            });


            $('#Details, span#Create').click(function (event) {
                switch ($(this).attr('id')) {
                    case "Create":
                        LoadPopupFromButton(event, this, "New Album", "Popup");
                        break;
                    case "Details":
                        LoadPopupFromButton(event, this, "Album Details", "Popup");
                        break;
                }

            });

            $('#btnUploadFromDisk').click(function (event) {

                $('div#mainContent').block({
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#777',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .9,
                        color: '#fff'
                    },
                    overlayCSS: {
                        backgroundColor: '#444',
                        padding: '-10px',
                        opacity: .2
                    },
                    message:
                        "loading from disk"
                });

            });


        });

    </script>
  
    <link href='<%=ResolveUrl("~/Content/fileinput.css") %>' rel="stylesheet" type="text/css" />
      <style type="text/css">
        div.Details, div.Edit
        {
            width:100%; height:100%;
            
        }
        .disabled
        {
         
            background-color:#FFAAB0;
        }
        #Popup
        {
            margin: 10px;
            padding: 0;
            position: relative;
        }
        
        .photoAlbumButton
        {
            
            display: inline;
        }
        
        .photoAlbumButton table
        {
            width: auto;
            display: inline-block;
            margin: 5px;
            text-align: center;
            border-radius: 3px;
            border: 1px solid #aaa;
        }
        
        .photoAlbumButton table td
        {
            text-align: center;
            padding: 5px;
        }
        
        .photoAlbumButton table td.button
        {
            font-weight:bold;
            background-color: #888;
            color: White;
        }
        
        span.addAlbum
        {
            background-color:#B5FFB7;
            color:Black;
            font-weight:bold;
        }
        
        .photoAlbumButton table td.button:hover
        {
            background-color: #FF5D30;
            cursor: pointer;
        }
        
        span.addAlbum:hover
        {
            background-color: #00820D;
            cursor: pointer;
        }
        
        span.addAlbum
        {
            width: auto;
            display: inline-block;
            text-align: center;
            border-radius: 3px;
            border: 1px solid #aaa;
            text-align: center;
            vertical-align: top;
            margin: 5px;
        }
        
        a#Create
        {
            text-decoration: none;
            color: inherit;
        }
        a#Details
        {
            color: inherit;
            text-decoration: none;
        }
        
        .fixed
        {
            position: fixed;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Photo Album Admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="mainContent" style="width: 100%; height: 100%">
        <div style="display: inline-block; margin: 5px;">
            <%=Html.ActionLink("Reload Albums From Disk", "UpdateAlbumsFromFiles", new {}, new {@class="button", @id="btnUploadFromDisk"})%>
        </div>
        <div style="display: block; width: 100%;">
            <% foreach (var album in Model)
           { 
            %><div class="photoAlbumButton">
                <table>
                    <tr>
                        <td colspan="2" style="vertical-align: top; white-space: nowrap; padding: 0; margin: 0;"
                            <%if(!album.IsActive){%> class="disabled" <%} %>>
                            <div style="position: relative; width: 100%;">
                                <span style="z-index: 0;">
                                    <%=album.Name%></span>
                                <div id="Delete">
                                    <a href='<%=Url.Action("Delete", new{ ID=album.PhotoAlbumID, @albumName = album.Name})  %>' id="Delete">
                                        <img src='<%=ResolveUrl("~/images/delete.png") %>' class="Delete" alt="delete" style='display: <% Response.Write(album.IsActive ? "inherit" : "none");
                                            %>' />
                                    </a>
                                </div>
                                <div id="Enable">
                                    <a href='<%=Url.Action("Enable", new{ ID=album.PhotoAlbumID, @albumName = album.Name})  %>' id="Enable">
                                        <img src='<%=ResolveUrl("~/images/enable.png") %>' class="Enable" alt="Re-enable" style='display: <% Response.Write(album.IsActive? "none" : "inherit");%>' />
                                    </a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td class="button" style="border-right: 1px solid white;" onclick='window.location="<%=ResolveUrl("~/Admin/Photos.aspx/Albums/"+album.Name) %>"'>
                            Photos
                        </td>
                        <td class="button" id="Details">
                            <%=Html.ActionLink("Details", "Details", new { id = album.PhotoAlbumID }, new { @id = "Details", @albumName = album.Name})%>
                        </td>
                    </tr>
                </table>
            </div>
            <%
           } 
           
            %>
            <span style="padding: 0; line-height: 3.2em; width: 7em;" class="addAlbum" id="Create">
                <%= Html.ActionLink("New", "Create", null, new { @id = "Create", @class="addAlbum" })%>
            </span>
        </div>
        <div id="Popup" style="overflow: hidden;">
        </div>
        <div id="delete-dialog" title="Delete Album?">
            <p>
                Are you sure you want to delete this Photo album? Not only will the album's information
                be lost but any data associated with the photos in this album will be lost as well.
            </p>
        </div>
        
        <div id="enable-dialog" title="Re-enable Album?">
            <p>
                Re-enabling Photo albums makes them visible to regular users to the site.  Make sure that your Photo Album looks the way you want before re-enabling it.
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Subtitle" runat="server">
    Albums
</asp:Content>
