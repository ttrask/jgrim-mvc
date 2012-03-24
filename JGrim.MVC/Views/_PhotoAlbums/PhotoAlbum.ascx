<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.PhotoAlbum>" %>
    
    <a class="NavIcon" href='<%=ResolveUrl("~/Photos.aspx/Albums/"+Model.Name) %>' >
        <img src='<% =ResolveUrl(Model.AlbumCoverUrl) %>' alt='Photo link' />
        <h5>
            <%=Model.Name%>
        </h5>
    </a>