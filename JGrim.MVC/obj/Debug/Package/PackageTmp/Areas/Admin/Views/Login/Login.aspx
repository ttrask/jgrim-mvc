<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<html>
<head>
    <title>Administration: Login</title>
    <link href='<%=ResolveUrl("~/Content/Admin.css") %>' rel="stylesheet" type="text/css" />
    <style type="text/css">
        div.login
        {
            border:1px solid grey;
            width: 200px;
            padding:20px;
            margin-left:auto;
            margin-right:auto;
            margin-top:auto;
        }
        div.page
        {
        }
        p
        {
            font-weight:bold;
        }
    </style>
</head>
<body>
    <% using (Html.BeginForm())
       { %>
    <div id="login_form" class="page">
        
        <div class="main" style="width:100%; border: 0;vertical-align:middle; text-align:center;">
            <div class="login" style="text-align:left;">
                <p>Please Login</p>
                <label for="username">
                    User name</label>
                <%= Html.TextBox("username")%>
                <label for="password">
                    Password</label>
                <%= Html.Password("password")%>
                <button type="submit" title="login" value="Login">
                    Login</button>
            </div>
        </div>
    </div>
    <% } %>
</body>
</html>
