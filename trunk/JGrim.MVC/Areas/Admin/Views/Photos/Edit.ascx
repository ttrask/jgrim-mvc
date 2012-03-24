<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.Photo>" %>
<div id="Edit" style="width: 100%; height: 100%;">
    <% using (Html.BeginForm("Edit", "Photos", FormMethod.Post, new { @style = "display:inline-block" }))
       {%>
    <%= Html.ValidationSummary(true)%>
    <table width="100%">
        <tr>
            <td style="text-align: center; padding-right: 10px;">
                <img src='<%=ResolveUrl("~/photos/ScaledImage/"+ Model.PhotoID+"/") %>?height=360&width=360'
                    alt="Photo" id="Photo" />
            </td>
            <td style="width: 240px; min-height:100px;">
                <div class="display-label">
                    <%= Html.LabelFor(model => model.Name)%>
                </div>
                <div class="display-field">
                    <%= Model.Name%>
                </div>
                <div class="display-label">
                    <%= Html.LabelFor(model => model.Description)%>
                </div>
                <div class="display-field">
                    <%=Html.TextAreaFor(model => model.Description, new { @rows = "5", @style = "resize:vertical;" })%>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
                <button type="submit" value="Save" title="Save">
                    Save</button>
                <%= Html.ActionLink("Details", "Details", new { id = Model.PhotoID }, new { @class = "button", @id = "Details" })%>
                <button value="Close" title="Close" id="btnClose">
                    Close</button>
            </td>
        </tr>
    </table>
    <% } %>
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
