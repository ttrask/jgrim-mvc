<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JGrim.MVC.Models.PhotoAlbum>" %>
<div id="Edit">
    <% using (Html.BeginForm("Edit", "PhotoAlbums", FormMethod.Post, new { @id = "Edit", enctype = "multipart/form-data" }))
       {%>
    <%= Html.ValidationSummary(true)%>
    <table class="editor">
        <tr>
            <td style="text-align: left; padding-right: 10px;">
                <div class="editor-label">
                    <%= Html.LabelFor(model => model.Name)%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(model => model.Name, new { maxlength = 50 })%>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(model => model.Description)%>
                </div>
                <div class="editor-field">
                    <%= Html.TextAreaFor(model => model.Description, new { @rows = "5" })%>
                </div>
            </td>
            <td style="width: 160px;">
                <div class="editor-label">
                    Album Image<br />
                    <img src='<%=ResolveUrl(Model.AlbumCoverUrl) %>' alt="Album Cover" style="max-width: 160px;
                        max-height: 160px;" />
                    <div class="_upload">
                        <input type="file" id="upload" name="upload" />
                    </div>
                </div>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <hr />
                <button type="submit" value="Save" title="Save">
                    Save</button>
                <%= Html.ActionLink("Details", "Details", new { id = Model.PhotoAlbumID}, new { @class = "button", @id = "Details" })%>
                <button value="Close" title="Close" id="btnClose">
                    Close</button>
            </td>
        </tr>
    </table>
    

    
    <style type="text/css">
        
        
        div#Edit
        {
            width:100%;
        }
        
        .editor
        {
            display: inline-table;
            width: 98%;
            padding: 0px !important;
            margin: 0px !important;
        }
        .editor-label
        {
            border-spacing: 0;
            padding: 0;
            margin: 0;
            font-weight: bold;
            display: inline;
        }
        .editor-field
        {
            border-spacing: 0;
            padding: 0;
            margin: 0;
            padding-bottom: 5px;
        }
        div.IsActive
        {
            width: 100%;
            display: inline-table;
        }
        div.IsActive > #disabled, div.IsActive > #enabled
        {
            display: inline;
            width: 50%;
            font-weight: bold;
            padding: 5px;
            margin: 1px;
        }
        
        div.IsActive > #disabled
        {
            -webkit-border-top-left-radius: 3px;
            -webkit-border-bottom-left-radius: 3px;
            -moz-border-radius-topleft: 3px;
            -moz-border-radius-bottomleft: 3px;
            border-top-left-radius: 3px;
            border-bottom-left-radius: 3px;
            background-color: #FFAAB0;
        }
        
        div.IsActive > #enabled
        {
            -webkit-border-top-right-radius: 3px;
            -webkit-border-bottom-right-radius: 3px;
            -moz-border-radius-topright: 3px;
            -moz-border-radius-bottomright: 3px;
            border-top-right-radius: 3px;
            border-bottom-right-radius: 3px;
            background-color: #B5FFB7;
        }
        
        div.IsActive > .inactive
        {
            cursor: pointer;
            opacity: 0.8;
            filter: alpha(opacity=80);
        }
        
        div._upload>div
        {
         padding:0px 0px 0px 0px;
         
        }
    </style>
    <script type="text/javascript">

        function LoadPopupEvents() {
            $('input#upload').fileinput({
                buttonText: '',
                inputText: '<% =Model.AlbumCoverUrl.Substring(Model.AlbumCoverUrl.LastIndexOf("/")+1) %>'
            });
        };

        //onload function
        $(function () {

            var $activityDiv = $('div.IsActive');
            var IsActive = $('#IsActive', $activityDiv).attr("value").toLowerCase();

            switch (IsActive) {
                case "false":
                    $('#disabled', $activityDiv).removeClass('inactive');
                    $('#enabled', $activityDiv).addClass('inactive');
                    break;
                case "true":
                    $('#disabled', $activityDiv).addClass('inactive');
                    $('#enabled', $activityDiv).removeClass('inactive');
                    break;
            }


            $('div', $activityDiv).click(function () {
                switch ($(this).attr('name')) {
                    case "disabled":
                        //switches IsActive status from Enabled to Disabled
                        $('#disabled', $activityDiv).removeClass('inactive');
                        $('#enabled', $activityDiv).addClass('inactive');
                        $('#IsActive', $activityDiv).attr("value", "False");
                        break;
                    case "enabled":
                        //switches IsActive Status from disabled to Enabled
                        $('#disabled', $activityDiv).addClass('inactive');
                        $('#enabled', $activityDiv).removeClass('inactive');
                        $('#IsActive', $activityDiv).attr("value", "True");
                        break;
                }
            });

        });

    </script>
    <% } %>
</div>
