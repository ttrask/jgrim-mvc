<%@ Page Title="" Language="C#" MasterPageFile="~/areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<JGrim.MVC.Models.Page>>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">

      var deleteLinkObj;
      var addLinkObj;

      $(document).ready(function () {
          $('.delete-link').click(function () {
              deleteLinkObj = $(this);  //for future use
              $('#delete-dialog').dialog("open");
              return false;
          });

          $('#delete-dialog').dialog({
              autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
              buttons: {
                  "Continue": function () {
                      $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                          window.location.reload();
                      });
                      $(this).dialog("close");
                  },
                  "Cancel": function () {
                      $(this).dialog("close");
                  }
              }
          });



          $('a.edit').click(function (event) {
              event.preventDefault();


              $('#Edit').html();
              $('#Edit').load($(this).attr("href"), function () {
                  $(this).height(this.scrollHeight);

                  $(this).width(this.scrollWidth - 5);
                  $('#Edit').dialog("open");

                  editLinkObj = $(this);
              });
          });

          $('#Edit').dialog({
              resizable: false,
              autoOpen: false,
              modal: true,
              title: 'Edit Page',
              width: "560px",
              close: function () {
                  $('#Edit').html("");
              }
          });

          $('#Add').dialog({
              resizable: false,
              autoOpen: false,
              modal: true,
              title: 'Add Page',
              width: "560px"
          });

          $('a#New').click(function (event) {
              
              event.preventDefault();

              $('#Add').html();
              $('#Add').load($(this).attr("href"), function () {
                  $(this).height(this.scrollHeight);
                  $(this).width(this.scrollWidth);
                  $(this).dialog('open');
              });
          });

      });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Page Admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm())
      { %>
    <h2>
        Admin</h2>
    <table>
        <colgroup>
            <col class="table-even" />
            <col class="table-odd" />
            <col class="table-even" />
            <col class="table-odd" />
        </colgroup>
        <thead>
            <tr>
                <th scope="col" class="table-odd">
                    Name
                </th>
                <th scope="col" class="table-even">
                    Url
                </th>
                <th scope="col" class="table-odd">
                    isActive
                </th>
                <th scope="col" class="table-even">
                </th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
        <tbody>
            <%if (Model != null) foreach (JGrim.MVC.Models.Page item in Model)
                  { %>
            <tr>
                <td>
                    <a class="edit" href='<%= ResolveUrl("~/Admin/Pages.aspx/Edit/"+item.PageID) %>'>
                        <%= item.Name %>
                    </a>
                </td>
                <td>
                    <%= Html.Encode(item.Url) %>
                </td>
                <td>
                    <%= Html.Encode(item.IsActive) %>
                </td>
                <td>
                    <div style='display:<% Response.Write(item.IsPermanent? "none" : "inherit");%>'>
                    <%= Html.ActionLink("Delete", "Delete", new { id = item.PageID}, new { @class = "delete-link" })%>
                    <!--a onclick="deleteRecord(<%= item.PageID %>)" href="JavaScript:void(0)">Delete</a!-->
                    </div>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create", null, new { id = "New", @class="button" })%>
    </p>
    <div id="Edit" style="overflow: hidden;">
    </div>
    <div id="Add" style="overflow: hidden;">
    </div>
    <%} %>
    <div id="delete-dialog" title="Delete Page?">
        <p>
            Are you sure you want to delete this Page?</p>
    </div>
   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Subtitle" runat="server">
    Pages
</asp:Content>
