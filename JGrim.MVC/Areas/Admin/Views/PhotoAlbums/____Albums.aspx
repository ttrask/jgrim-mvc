<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JGrim.MVC.Models.PhotoAlbum>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Photo Album Admin:    <%= Model.Name %>
</asp:Content>
<asp:Content ID="content5" ContentPlaceHolderID="HeadContent" runat="server">
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
                <img width='120px' alt="" src='<%=ResolveUrl("/images/photos"+ photo.ThumbnailLocation)%>' />
            </a>
            <% }%>
        </div>
    </div>
    <p>
    </p>
</asp:Content>
