<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.Page>" %>
<script src='<%= ResolveUrl("~/Scripts/PageSpecific/AdminMaster.js") %>' type="text/javascript"></script>
<link href="~/Content/admin.css" rel="stylesheet" type="text/css" />
<p>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <div class="editor-label">
        <h4>
            <%= Html.LabelFor(model => model.Name) %>
        </h4>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(model => model.Name) %>
        <%= Html.ValidationMessageFor(model => model.Name, "Page name is Required!") %>
    </div>
    <div class="editor-label">
        <h4>
            <%= Html.LabelFor(model => model.Url) %></h4>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor<JGrim.MVC.Models.Page, String>(model => model.Url)%>
        <%= Html.ValidationMessageFor(model => model.Url, "Url is Required!") %>
    </div>
    <div class="editor-label">
        <h4>
            Page Content</h4>
    </div>
    <div class="editor-field">
        <%= Html.TextAreaFor<JGrim.MVC.Models.Page, String>(model => model.PageContent, new { @rows = 10 })%>
        <%= Html.ValidationMessageFor(model => model.PageContent) %>
    </div>
    <p class="buttons">
        <input type="submit" value="Create" class="button" />
    </p>
    <% } %>
</p>
<style type="text/css">
    textarea, input[type="text"]
    {
        width: 100%;
        padding: 5 10 5 10;
    }
</style>
<script type="text/javascript">
    $('p.buttons').html('<hr>' + $('p.buttons').html());
    $('button, .button').button();
</script>
