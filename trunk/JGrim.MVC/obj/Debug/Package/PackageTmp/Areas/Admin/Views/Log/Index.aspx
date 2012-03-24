<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<JGrim.MVC.Models.Log>" %>
<%@ Import Namespace="JGrim.MVC.Models" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Administration Logs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Error Logs</h2>
     <%using (Html.BeginForm())
      { %>
    <table class="errorLog">
        <colgroup>
            <col class="table-even" />
            <col class="table-odd" />
            <col class="table-even" />
            <col class="table-odd" />
        </colgroup>
        <thead>
            <tr>
                <th scope="col" class="table-odd">
                    Log Message
                </th>
                <th scope="col" class="table-even">
                    Log Date
                </th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
        <tbody>
            <% PagedList<Log> logs= Log.GetPagedLog(Request.QueryString["page"]);  %>
            <%foreach (JGrim.MVC.Models.Log item in logs)
                  { %>
            <tr>
                <td>
                        <%= item.ErrorMessage %>
                </td>
                <td style="white-space:nowrap">
                    <%= Html.Encode(item.LogDate) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <%} %>
    
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">

.errorLog td
{
    border:1px solid black;
}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Subtitle" runat="server">
</asp:Content>
