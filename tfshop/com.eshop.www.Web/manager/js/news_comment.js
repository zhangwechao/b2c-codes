$(document).ready(function(){
    $("#navigation").tabs({selected:2});
    $("#news_comment_page").css({"background-image":"url(image/A-dmin-0122.gif)"})
    $("#advSearch").dialog({autoOpen:false,zIndex:100});
    $("#export").dialog({autoOpen:false,zIndex:100});
    $("#message").dialog({autoOpen:false,zIndex:100});
    $("#txtBeginDate").datepicker({dateFormat:'yy-mm-dd'});
    $("#txtEndDate").datepicker({dateFormat:'yy-mm-dd'});
    $("#btnAdvSearch").bind("click",function(){
        var ip = $.trim($("#txtIP").val());
        var userName = $.trim($("#txtUserName").val());
        var content = $.trim($("#txtContent").val());
        var newsId = $("#txtNewsTitle").val();
        var isShow = $("#txtIsShow").val();
        var beginDate = $.trim($("#txtBeginDate").val());
        var endDate = $.trim($("#txtEndDate").val());
        var query="";
        if(ip.length>0)
            query+="&ip="+ip;
        if(userName.length>0)
            query+="&userName="+userName;
        if(content.length>0)
            query+="&content="+content;
        if(isShow!="-1")
            query+="&isShow="+isShow;
        if(newsId!="0")
            query+="&newsId="+newsId;    
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
        window.location.href="news_comment.aspx?"+query;
    });
    $("#btnSearch").bind("click",function(){
        var search = $.trim($("#txtSearch").val());
        window.location.href="news_comment.aspx?content="+search;
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
            url:'handler/news_comment.ashx',
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
    $("#btnSearchNewsTitle").bind("click",function(){
        var newsTitle = $.trim($("#txtSearchTitle").val());
        if(newsTitle.length==0){
            alert("请输入要搜索的新闻标题");
            return;
        }
        var loading = new ol.loading({id:'advSearch'});
        loading.show();
        $.ajax({
            url:'handler/news_comment.ashx',
            data:'action=search&title='+newsTitle,
            type:'post',
            dataType:'json',
            success:function(res){
                var len = res.news_content.length;
                var id="";
                var title="";
                var option="";
                for(var i=0;i<len;i++){
                    id = res.news_content[i].id;
                    title = res.news_content[i].title;
                    option = "<option value='"+id+"'>"+title+"</option>";
                    $("#txtNewsTitle").append(option);
                    
                }
                if(len>0)
                    $("#txtNewsTitle").val(res.news_content[0].id);
                loading.hide();
            },
            error:function(res){
                loading.hide();
                alert(res.responseText);
                
            }
        });
    })
});
function ValidataSelect2(){
    var selects = $("div#content_body input[type=checkbox][name!=chkAll]:checked");
    if(selects.length==0){
        alert("请选择要禁止显示的数据");
        return false;
    }
}