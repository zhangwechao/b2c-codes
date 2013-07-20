$(document).ready(function() {
    $("#navigation").tabs({ selected: 1 });
    $("#product_content_page").css({ "background-image": "url(image/A-dmin-0122.gif)" })
    $("#advSearch").dialog({ autoOpen: false, zIndex: 100 });
    $("#export").dialog({ autoOpen: false, zIndex: 100 });
    $("#import").dialog({ autoOpen: false, zIndex: 100 });
    $("#message").dialog({ autoOpen: false, zIndex: 100 });
    $("#txtBeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
    $("#txtEndDate").datepicker({ dateFormat: 'yy-mm-dd' });

    $("#btnSearch").bind("click", function() {
        var search = $.trim($("#txtSearch").val());
        window.location.href = "product_content.aspx?productName=" + search;
    });
    $("#btnDisplayAdv").bind("click", function() {
        $("#advSearch").dialog("open");
    });
    $("#btnExport").bind("click", function() {
        $("#export").dialog("open");
    });
    $("#btnImport").bind("click", function() {
        $("#import").dialog("open");
    });
    $("#btnBeginExport").bind("click", function() {
        var fileName = $.trim($("#txtFileName").val());
        if (fileName.length == 0) {
            alert("请输入要导出的文件名");
            return;
        }
        $("#export").dialog("close");
        var loading = new ol.loading({ id: "content" });
        loading.show();
        $.ajax({
            url: 'handler/product_content.ashx',
            data: 'action=export&fileName=' + fileName,
            type: 'post',
            dataType: 'json',
            success: function(res) {
                if (res.message == "success") {
                    loading.hide();
                    $("#download").attr("href", "export/" + fileName + ".xls");
                    $("#message").dialog("open");
                }
            },
            error: function(res) {
                loading.hide();
                alert(res.responseText);
            }
        });
    });
    $('#file_upload').uploadify({
        'uploader': 'flash/uploadify.swf',
        'script': 'handler/product_content.ashx',
        'cancelImg': 'image/cancel.png',
        'folder': 'import/',
        'auto': true,
        'buttonText': '选择上传文件',
        'sizeLimit': 2000 * 1024,
        'fileExt': '*.xls',
        'fileDesc': 'Excel 文件',
        'onSelect': function(e, queueId, fileObj) {
            if (fileObj.size > 2000 * 1024) {
                alert("文件太大，请保持文件在2000KB以内");
                return;
            }
        },
        'onComplete': function(event, queueId, fileObj, response, data) {
            alert("数据导入成功");
            window.location.href = "product_content.aspx";
        },
        'onError': function(event, queueId, fileObj, errorObj) {
            alert("Excel格式不正确，请下载最新的Excle导入格式");
        }

    });
});