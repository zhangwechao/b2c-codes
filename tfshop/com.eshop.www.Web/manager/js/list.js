function ask(){
    if(!confirm('确定要删除当前记录吗？'))
        return false;
}
function ValidataSelect(){
    var selects = $("div#content_body input[type=checkbox][name!=chkAll]:checked");
    if(selects.length==0){
        alert("请选择要删除的数据");
        return false;
    }
    if(!confirm("你确定要删除选择的数据吗？")){
        return false;
    }
}
function selectData(){
    var check = $("div#content_body input[type=checkbox][name=chkAll]").attr("checked");
    if(check)
        $("div#content_body input[type=checkbox][name!=chkAll]").attr("checked",true);
    else
        $("div#content_body input[type=checkbox][name!=chkAll]").attr("checked",false);
}
$(function(){
    $('#mytable tr:odd td').css('background','#eef0f6');
    $('#mytable tr:even td').css('background','#ffffff');
    $('#mytable tr').bind('mouseover',function(){
        $(this).find('td').css('background','#faf1cc');
    });
    $('#mytable tr').bind('mouseout',function(){
        $('#mytable tr:odd td').css('background','#eef0f6');
        $('#mytable tr:even td').css('background','#ffffff');
    });
});