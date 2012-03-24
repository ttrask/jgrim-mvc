<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<JGrim.MVC.Models.PhotoAlbum>>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">


    function LoadImagesIntoDB() {

        var url = '<%=ResolveUrl("~/Services/AjaxService.SVC/LoadImagesToDB") %>';
        

        $('#btnLoadImages').disabled = true;
        $('#btnLoadImages').text("Saving..");
        $.post(url, function (data) {  //Post to action
            window.location.reload();
        }
        );

    };
</script>

<style>
    button
    {
        font-size:1em;
    }
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Admin
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Photo Admin</h2>

    <div style="float:right; position:relative absolute; top:0; right:0;">
        <button id="btnLoadImages" type="button" onclick="javascript:LoadImagesIntoDB();" >Load Images into Database</button>
    </div>

    <div>
    <% foreach (var album in Model) { 
    %>
        <a class="NavIcon" href='<%=ResolveUrl("~/admin/Photos.aspx/Albums/"+album.Name) %>' >
        <img src='<% =ResolveUrl(album.AlbumCoverUrl) %>' alt='Photo link' />
        <h5>
            <%=album.Name%>
        </h5>
        </a>
    <%
           } 
    %>
        
    </div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Subtitle" runat="server">
</asp:Content>
