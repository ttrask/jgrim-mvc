<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.PhotoAlbum>" %>
<a class="NavIcon" href='<%=ResolveUrl("~/Photos.aspx/Albums/"+Model.Name) %>'>
    <table>
        <tr>
            <td colspan="2">
            <h5>
            <%=Model.Name%>
        </h5>
            </td>
        </tr>
        <tr style="text-align:center;">
            <td class="albumPhotos" onclick='window.location=\"<%=ResolveUrl("~/Admin/Photos.aspx/Albums/"+Model.Name) %>\"'>
                Photos
            </td>
           <td class="albumDetails" onclick='javascript:ShowAlbumDetails(<%=Model.PhotoAlbumID %>)'>
            </td>
        </tr>
    </table>

