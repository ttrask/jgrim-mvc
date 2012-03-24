<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.PhotoAlbum>" %>


<div class="editor-label">
    Album Image<br />
    <img src='<% Response.Write(Model!=null ? ResolveUrl(Model.AlbumCoverUrl) : ResolveUrl("~/images/no_Image.png"));%>'
        alt="" style="border: 2px dashed black;" />
    <span>
        <input type="file" id="file1" name="file1" />
    </span>
</div>



