<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<JGrim.MVC.Models.PhotoAlbum>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

	Albums
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Albums</h2>

    

    <% foreach (var album in Model) { 
               Html.RenderPartial("PhotoAlbum", album);
           } 
    %>

    

    <p>
        
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Subtitle" runat="server">
</asp:Content>
