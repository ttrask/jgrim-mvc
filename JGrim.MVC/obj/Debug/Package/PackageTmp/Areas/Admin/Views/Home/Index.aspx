<%@ Page Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<String>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administration - Home Page
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>
    <style type="text/css">
        .ui-button-text
        {
            padding: 15px;
            background-color: #ccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p>
            <h2 style="padding-bottom: 15px;">
                Administration Home</h2>
            This is the place where dreams are monitored.
        </p>
        <div style="width: 100%; text-align: center;">
            <a class="button" href='<%=ResolveUrl("~/Admin/Pages/Admin") %>'>Pages </a><span
                style="padding: 15px;"></span><a class="button" href='<%=ResolveUrl("~/Admin/PhotoAlbums") %>'>
                    Photos </a>
        </div>
    </div>
</asp:Content>
