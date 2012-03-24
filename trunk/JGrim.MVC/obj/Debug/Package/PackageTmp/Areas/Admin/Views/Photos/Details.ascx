<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.Photo>" %>
<div id="Details" style="width: 100%; height: 100%;">
    <table>
        <tr>
            <td style="text-align: center; padding-right: 10px;">
                <img src='<%=ResolveUrl("~/photos/ScaledImage/"+Model.PhotoID)%>/?width=360&height=360'
                    alt='<%=Model.FileName %>' id="Photo" />
            </td>
            <td style="width: 240px; min-height: 100px;">
                <div class="display-label">
                    <%= Html.LabelFor(model => model.Name) %>
                </div>
                <div class="display-field">
                    <a href='<%=ResolveUrl(String.Format("{0}/{1}/{2}", System.Configuration.ConfigurationManager.AppSettings["PhotoPath"], Model.PhotoAlbum.Name, Model.FileName)) %>'>
                        <%= Model.Name %>
                    </a>
                </div>
                <div class="display-label">
                    <%= Html.LabelFor(model => model.Description) %>
                </div>
                <div class="display-field">
                    <%= Model.Description%>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <hr />
                <%= Html.ActionLink("Edit", "Edit", new { id=Model.PhotoID }, new {@class="button", @id="Edit"} )%>
                <button id="btnClose" value="Close" title="Close">
                    Close</button>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    
</script>
<style type="text/css">
    .display-label
    {
        font-weight: bold;
        display: inline;
    }
    .display-field
    {
        padding-bottom: 5px;
    }
    
    textarea
    {
        border: 3px solid #ccc;
    }
</style>
