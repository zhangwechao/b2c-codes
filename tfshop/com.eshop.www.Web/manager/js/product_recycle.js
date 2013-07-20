$(document).ready(function(){
    $("#navigation").tabs({selected:1});
    $("#product_content_page").css({"background-image":"url(image/A-dmin-0122.gif)"})
    $("#advSearch").dialog({autoOpen:false,zIndex:100});
    $("#export").dialog({autoOpen:false,zIndex:100});
    $("#message").dialog({autoOpen:false,zIndex:100});
    $("#txtBeginDate").datepicker({dateFormat:'yy-mm-dd'});
    $("#txtEndDate").datepicker({dateFormat:'yy-mm-dd'});
    
    $("#btnSearch").bind("click",function(){
        var search = $.trim($("#txtSearch").val());
        window.location.href="product_recycle.aspx?productName="+search;
    });
    $("#btnDisplayAdv").bind("click",function(){
        $("#advSearch").dialog("open");
    });
    $("#btnExport").bind("click",function(){
        $("#export").dialog("open");
    });
    $("#btnBeginExport").bind("click",function(){
        var fileName = $.trim($("#txtFileName").val());
        if(fileName.length==0){
            alert("请输入要导出的文件名");
            return;
        }
        $("#export").dialog("close");
        var loading = new ol.loading({id:"content"});
        loading.show();
        $.ajax({
            url:'handler/product_recycle.ashx',
            data:'action=export&fileName='+fileName,
            type:'post',
            dataType:'json',
            success:function(res){
                if(res.message=="success"){
                    loading.hide();
                    $("#download").attr("href","export/"+fileName+".xls");
                    $("#message").dialog("open");
                }
            },
            error:function(res){
                loading.hide();
                alert(res.responseText);
            }
        });
    });
});
function ValidataSelect2(){
    var selects = $("div#content_body input[type=checkbox][name!=chkAll]:checked");
    if(selects.length==0){
        alert("请选择要恢复的数据");
        return false;
    }
}