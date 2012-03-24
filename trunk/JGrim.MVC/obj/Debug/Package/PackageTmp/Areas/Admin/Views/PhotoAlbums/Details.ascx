<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.PhotoAlbum>" %>
<div id="Details">
    <table class="Details">
        <tr>
            <td style="text-align: left; padding-right: 10px;">
                <div class="display-label">
                    <%= Html.LabelFor(model => model.Name) %>
                </div>
                <div class="display-field">
                    <%= Model.Name %>
                </div>
                <hr />
                <div class="display-label">
                    <%= Html.LabelFor(model => model.Description) %>
                </div>
                <div class="display-field">
                    <%= Model.Description%>
                </div>
            </td>
            <td style="width: 160px;">
                <div class="display-label">
                    Album Cover<br />
                    <img src='<%=ResolveUrl(Model.AlbumCoverUrl) %>' alt="Album Cover" style="max-width: 160px;
                        max-height: 160px;" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="disabled <% =(Model.IsActive?"inactive":"active")%>">
                    Disabled
                </div>
                <div class="enabled <%=(Model.IsActive?"active":"inactive")%>">
                    Enabled
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
                <%= Html.ActionLink("Edit", "Edit", new { id=Model.PhotoAlbumID }, new {@class="button", @id="Edit"} )%>
                <button id="btnClose" value="Close" title="Close">
                    Close</button>
            </td>
        </tr>
    </table>
    <style type="text/css">
        
        div#Details
        {
            width:100%;
        }
        
        table.Details
        {
            display: inline-table;
            width: 98%;
        }
        
        table.Details > div
        {
            border: 1px solid black;
        }
        .display-label
        {
            font-weight: bold;
            display: inline;
        }
        .display-field
        {
            padding-bottom: 5px;
        }
        
        .disabled, .enabled
        {
            width: 100%;
            margin: auto 0 auto 0;
            padding: 3px;
            vertical-align: middle;
            text-align: center;
            font-weight: bold;
            border: 1px solid grey;
        }
        
        .disabled
        {
            background-color: #FFAAB0;
        }
        
        .enabled
        {
            background-color: #B5FFB7;
        }
        
        .active
        {
            display: auto;
        }
        
        .inactive
        {
            display: none;
        }
    </style>
</div>
