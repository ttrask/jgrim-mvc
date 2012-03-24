<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JGrim.MVC.Models.PhotoAlbum>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Jason Grim Photography:
    <%= Model.Name %>
</asp:Content>
<asp:Content ID="content5" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/custom-theme/jquery-ui-1.8.9.custom.css" rel="stylesheet"
        type="text/css" />
    <link href="~/Content/custom-theme/jquery-ui-1.8.9.custom.css" rel="stylesheet" type="text/css" />
    <link href='<%=ResolveUrl("~/galleria/galleria.classic.css")%>' rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        #PhotoGallery
        {
            width: 600px;
            height: 480px;
        }
        a#AboutLink
        {
            text-decoration: none;
            border-style: none;
        }
    </style>
    <script type="text/javascript">
        var galleriaThemeUrl = '../../galleria/galleria.classic.js';
    </script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/jquery-ui-1.8.9.custom.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/galleria/galleria.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/PageSpecific/Photos.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/jquery.blockUI.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            var centralDivName = 'td.main';
            var $centralDiv = $(centralDivName)[0];

            $('div#AboutPage').dialog({
                position: "center",
                autoOpen: true,
                modal: false,
                width: "500px",
                resizable: false,
                open: function () {
                    $parent = $(this).parent();
                    $parent.position({ of: centralDivName });
                    $footerHtml = $('div#footer').html();
                    $('div#footer').remove();
                    $parent.append($footerHtml);
                    $(centralDivName).block({ message: null });

                },
                title: 'About &quot;<%=Model.Name %>&quot;',
                close: function () {
                    $(centralDivName).unblock();
                }
            });



            $('a#AboutLink').click(function (event) {

                event.preventDefault();
                $('div#AboutPage').dialog("open");
                $('div#AboutPage>div#footer').hide();
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative;" id="Content">
        <div style="position: absolute; bottom: 0; right: 0;">
            <a id="AboutLink" href="#">
                <img src='<%=ResolveUrl("~/images/info.png") %>' alt="info" />
            </a>
        </div>
        <div id="PhotoList">
            <!--BreadCrumbs -->
            <div id="breadCrumbs">
                <a id='navPhotoBreadCrumb' href='<%=ResolveUrl("~/photos.aspx/") %>' class="breadCrumbLink">
                    Photographs <span class="arrow"></span><span class="arrow-bg"></span><span class="arrow-border">
                    </span></a><a class="breadCrumbLink">
                        <%= Model.Name %>
                        <span class="arrow"></span><span class="arrow-border"></span></a>
            </div>
            <!--Photo Gallery List -->
            <div id='PhotoGallery'>
                <% foreach (JGrim.MVC.Models.Photo photo in ((JGrim.MVC.Models.PhotoAlbum)Model).Photos)
                   { %>
                <img alt='<%=photo.Description %>' src='<%=ResolveUrl("~/photos/ScaledImage/"+photo.PhotoID)%>?height=160&width=160' />
                <% }%>
            </div>
        </div>
        <div id="AboutPage">
            <div style="position: relative;">
                <p>
                    <%if (String.IsNullOrEmpty(Model.Description)) { Response.Write("&lt;no information available for this album&gt;"); } else { Response.Write(Model.Description); }  %>
                </p>
                <div id="footer">
                    <div style="padding: 5px; margin-bottom: 5px">
                        <hr style="width: 100%;">
                        To find out information about this again, click
                        <img src='<%=ResolveUrl("~/images/info.png") %>' alt="info" />.
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
