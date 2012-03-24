<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>



<% using(Html.BeginForm("Authenticate", "Login")) { %>
<div id="login_form">

    <p>Enter your details</p>
    
    <label for="username">User name</label>
    <%= Html.TextBox("username") %>
    
    <label for="password">Password</label>
    <%= Html.Password("password", null, new { @class = "Password" })%>

    <button type="submit" title="login" value="Login" >Login</button>
</div>
<% } %>


