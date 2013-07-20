$(document).ready(function(){
    $("#navigation").tabs({selected:2});
    $("#news_category_page").css({"background-image":"url(image/A-dmin-0122.gif)"})
    $("#advSearch").dialog({autoOpen:false,zIndex:100});
   
    $("#btnSearch").bind("click",function(){
        var search = $.trim($("#txtSearch").val());
        window.location.href="news_category.aspx?categoryName="+search;
    });
    $("#btnDisplayAdv").bind("click",function(){
        $("#advSearch").dialog("open");
    });
   
});