<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<JGrim.MVC.Models.PhotoAlbum>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

	Albums
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="text-align:center; width:100%">
    <h1>Photography</h1>

    
    <div style="text-align:left; width:100%">
    <% foreach (var album in Model) { 
               Html.RenderPartial("PhotoAlbum", album);
           } 
    %>
    </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Subtitle" runat="server">
</asp:Content>
