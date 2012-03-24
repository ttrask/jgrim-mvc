<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%-- Register the FlashUpload control so we can use it below --%>
<%@ Register Assembly="FlashUpload" Namespace="FlashUpload" TagPrefix="FlashUpload" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--
        
            The flash upload control.  Source is located in the FlashUplod project with this solution.
            The control encapsulates the creation of the flash object and embeds the .swf file
            within the FlashUpload.dll.
            
            Parameters:
                UploadPage: the page to upload to. This can be a HttpHandler, an .aspx page, an .asp page;
                            any page that can accept uploaded files.  This sample project uses a HttpHandler
                            which in my opinion is the best option.
                OnUploadComplete:   A javascript function which is called after all the files are uploaded.
                                            
                
        
        --%>
        <FlashUpload:FlashUpload ID="flashUpload" runat="server" 
            UploadPage="Upload.axd" OnUploadComplete="UploadComplete()" 
            FileTypeDescription="Images" FileTypes="*.gif; *.png; *.jpg; *.jpeg" 
            UploadFileSizeLimit="1800000" TotalUploadSizeLimit="2097152" />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
    </div>
    </form>
</body>
</html>
