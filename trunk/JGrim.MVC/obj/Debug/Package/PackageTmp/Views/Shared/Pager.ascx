<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Webdiyer.WebControls.Mvc.PagedList<Photo>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc"%>
<%@ Import Namespace="JGrim.MVC.Models"%>
<p>
<%=Html.Pager(Model, 
    new PagerOptions() { PageIndexParameterName = "page", ShowDisabledPagerItems = true, ShowPageIndexBox = true, FirstPageText="<<", LastPageText=">>", PrevPageText="<", NextPageText=">",
                         PageIndexBoxType = PageIndexBoxType.DropDownList, ShowNumericPagerItems=false, ShowMorePagerItems=true,  
                         ShowGoButton = false,
                         CssClass = "pages"
    })%>
</p>


<script type="text/javascript">
    $(document).ready(function () {
        $('.pages a[disabled="disabled"]').addClass("disabled");
        $('.pages a:not([disabled="disabled"])').addClass("enabled");
    });
</script>

<style type="text/css">
.disabled
{
    background-color:#ddd;
    cursor:default;
}

.disabled:hover
{
    background-color:#ddd;
    cursor:default;
}

</style>
