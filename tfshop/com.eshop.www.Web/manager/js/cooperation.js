$(document).ready(function(){
    $("#navigation").tabs({selected:2});
    $("#cooperation_page").css({"background-image":"url(image/A-dmin-0122.gif)"})
    $("#advSearch").dialog({autoOpen:false,zIndex:100});
    $("#export").dialog({autoOpen:false,zIndex:100});
    $("#import").dialog({autoOpen:false,zIndex:100});
    $("#message").dialog({autoOpen:false,zIndex:100});
    $("#txtBeginDate").datepicker({dateFormat:'yy-mm-dd'});
    $("#txtEndDate").datepicker({dateFormat:'yy-mm-dd'});
    $("#btnAdvSearch").bind("click",function(){
        var title = $.trim($("#txtTitle").val());
        var keywords = $.trim($("#txtKeywords").val());
        var summary = $.trim($("#txtSummary").val());
        var isShow = $("#txtIsShow").val();
        var beginDate = $.trim($("#txtBeginDate").val());
        var endDate = $.trim($("#txtEndDate").val());
        var query="";
        if(title.length>0)
            query+="&title="+title;
        if(keywords.length>0)
            query+="&keywords="+keywords;
        if(isShow!="-1")
            query+="&isShow="+isShow;
        if(beginDate.length>0){
            var ds = beginDate.split("-");
            if(ds.length!=3){
                alert("日期格式不正确，无法转成正确的日期格式");
                return false;
            }
            query+="&beginDate="+beginDate;    
        }
        if(endDate.length>0){
            var ds = endDate.split("-");
            if(ds.length!=3){
                alert("日期格式不正确，无法转成正确的日期格式");
                return false;
            }
            query+="&endDate="+endDate;    
        }
        if(query.length>0)
            query = query.substr(1);
        window.location.href="cooperation.aspx?"+query;
    });
    $("#btnSearch").bind("click",function(){
        var search = $.trim($("#txtSearch").val());
        window.location.href="cooperation.aspx?search="+search;
    });
    $("#btnDisplayAdv").bind("click",function(){
        $("#advSearch").dialog("open");
    });
    $("#btnExport").bind("click",function(){
        $("#export").dialog("open");
    });
    $("#btnImport").bind("click",function(){
        $("#import").dialog("open");
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
            url:'handler/cooperation.ashx',
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
    $('#file_upload').uploadify({
        'uploader'  : 'flash/uploadify.swf',
        'script'    : 'handler/cooperation.ashx',
        'cancelImg' : 'image/cancel.png',
        'folder'    : 'import/',
        'auto'      : true,
        'buttonText': '选择上传文件',
        'sizeLimit': 200*1024,
        'fileExt': '*.xls',
        'fileDesc': 'Excel 文件',
        'onSelect':function(e, queueId, fileObj){
            if(fileObj.size>200*1024){
                alert("文件太大，请保持文件在200KB以内");
                return;
            }
        },
        'onComplete':function(event,queueId,fileObj,response,data){
            alert("数据导入成功");
            window.location.href="cooperation.aspx";
        }
    });
});