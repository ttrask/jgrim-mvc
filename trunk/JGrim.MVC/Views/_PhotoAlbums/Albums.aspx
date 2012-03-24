<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JGrim.MVC.Models.PhotoAlbum>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Jason Grim Photography:
    <%= Model.Name %>
</asp:Content>
<asp:Content ID="content5" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href='<%=ResolveUrl("~/galleria/galleria.classic.css")%>' rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        #PhotoGallery
        {
            width: 600px;
            height: 480px;
        }
    </style>
    
    <script type="text/javascript">
        var galleriaThemeUrl = '../../galleria/galleria.classic.js';
    </script>
    <script type="text/javascript" src='<%=ResolveUrl("~/galleria/galleria.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/PageSpecific/Photos.js")%>'></script>
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    
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
            <a href='<% =ResolveUrl("/images/photos"+ photo.FileLocation)%>'>
                <img width='120px' alt="" src='<%=ResolveUrl("/images/photos"+ photo.ThumbnailLocation)%>' />            </a>
            <% }%>
        </div>
    </div>
    <p>
    </p>
    
</asp:Content>
