<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.Page>" %>
<link href="~/Content/admin.css" rel="stylesheet" type="text/css" />
<p>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true)%><div id='EditPageContent'>
        <div>
            <h4>
                Page Name</h4>
            <%=Html.TextBoxFor<JGrim.MVC.Models.Page, String>(m => m.Name)%>
        </div>
        <div>
            <div>
                <h4>
                    Url</h4>
                <%=Html.TextBoxFor<JGrim.MVC.Models.Page, String>(m => m.Url)%>
            </div>
        </div>
        <div>
            <div>
                <h4>
                    Content</h4>
                <%=Html.TextAreaFor<JGrim.MVC.Models.Page, String>(m => m.PageContent, new { @rows = 10, @id = "hPageContent", })%>
            </div>
        </div>
        <%=Html.HiddenFor<JGrim.MVC.Models.Page, int>(m => m.PageID)%>
        
        <hr />
        <p class="buttons">
            
            <button value="Save" id="btnSave" type="submit">
                Save</button>
            <button value="Cancel" id="btnCancel" type="button">
                Cancel</button>
        </p>
        <% }%>
    </div>
</p>
<script type="text/javascript">
    $('button, .button').button();
</script>
<style type="text/css">
</style>
