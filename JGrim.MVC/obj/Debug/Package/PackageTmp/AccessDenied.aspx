<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<html>
<head>
    <title>Administration: Login</title>
    <link href='<%=ResolveUrl("~/Content/Admin.css") %>' rel="stylesheet" type="text/css" />
    <style type="text/css">
        div.AccessDenied
        {
            width:100%;
            text-align:center;
            padding-top:1em;
            font-family:verdana, Trebuchet MS, Sans-Serif;
            font-size:3em;
            font-weight:bold;
        }
    </style>
</head>
<body>
    <div  class="page">
        
        <div class="main" style="width:100%; border: 0;vertical-align:middle; text-align:center;">
            <div class="AccessDenied" >
                Access Denied
            </div>
            <div >
            <a href='<%=ResolveUrl("~/Admin/home.aspx") %>' >Log in</a></div>
        </div>
    </div>
    
</body>
</html>
