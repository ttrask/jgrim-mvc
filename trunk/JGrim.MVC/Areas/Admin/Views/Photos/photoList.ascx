<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

            <% foreach (JGrim.MVC.Models.Photo photo in Model)
               { %>
            <a class="photoLink" href='<% =ResolveUrl(photo.FileLocation+"/" +photo.Name)%>'>
                <img width='120px' alt="" src='<%=ResolveUrl(photo.FileLocation+"/thumbs/" +photo.Name)%>' style="max-height:120px; max-width:120px;" />            </a>
            <% }%>
