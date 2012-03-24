<%@ Page Language="C#" AutoEventWireup="true"  %>
<html><head><title>default redirect</title></head><body>
<script runat="server">

    protected override void OnLoad(EventArgs e)
    {
     /*   string originalPath = Request.Path;
        HttpContext.Current.RewritePath(Request.ApplicationPath, false);

        IHttpHandler httpHandler = new MvcHttpHandler();
        httpHandler.ProcessRequest(HttpContext.Current);
        HttpContext.Current.RewritePath(originalPath, false);
        */
      }
    
</script>
<% Response.Redirect("~/Pages/about.aspx"); %>

</body></html>