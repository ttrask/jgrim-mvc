<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.PhotoAlbum>" %>
<% using (Html.BeginForm("Create", "PhotoAlbums", FormMethod.Post,
   new { enctype = "multipart/form-data", @id="uploadForm" }))

   {%>
<%= Html.ValidationSummary(true) %>
<table width="100%">
    <tr>
        <td width="50%">
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Description) %>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Description, new { @rows = "10" })%>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </div>
        </td>
        <td width="50%" style="padding-left:20px;">
            <%Html.RenderPartial("AlbumArtUpload"); %>
        </td>
    </tr>
</table>
<hr />
<div style="position: relative absolute; bottom: 0; left: 0;">
    <input type="submit" class="button save"  value="Create" />
</div>
<% } %>
