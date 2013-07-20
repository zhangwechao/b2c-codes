<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="news_content_detail.aspx.cs" Inherits="administrator_news_content_detail" Title="新闻内容"  ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<script src="editor/kindeditor.js" type="text/javascript" charset="utf-8"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_article'));
$tabs.tabs('select',indexNum);
var type = getQueryString("type");
if (type == "sitepost")
    $("#website_sitepost_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
else if (type == "help")
    $("#help_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
else if (type == "news")
    $("#news_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
else if(type=="recruit")
    $("#recruit_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>新闻内容</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable">
            <tr>
                <td colspan="6" align="right" class="inputTableTd">
                    <asp:Button ID="btnDelete2" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnReset2" runat="server" Text="重置" onclick="btnReset_Click" />
                    <asp:Button ID="btnSave2" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnNext2" runat="server" Text="下一条" onclick="btnNext_Click" />
                    <asp:Button ID="btnPrev2" runat="server" Text="上一条" onclick="btnPrev_Click" />
                    <asp:Button ID="btnReturn2" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    标题&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    排序号
                </td>
                <td class="inputTableTd" width="150">
                    <asp:TextBox ID="txtOrderBy" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    所属目录&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:DropDownList ID="ddlCategory" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    显示
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsShow" runat="server" Checked="true" />
                </td>
                <td class="inputTableTd">
                    推荐
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsRecommend" runat="server" />
                </td>
                <td class="inputTableTd">
                </td>
                <td class="inputTableTd">
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">关键字</td>
                <td colspan="5" class="inputTableTd">
                    <asp:TextBox ID="txtKeywords" runat="server" Width="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">摘要</td>
                <td colspan="5" class="inputTableTd">
                    <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Rows="3" Columns="69"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">内容&nbsp;<font color="red">*</font></td>
                <td colspan="5" class="inputTableTd">
                <textarea id="txtContent"  runat="server" style="width:600px;height:300px;">
                
                </textarea>
                <script type="text/javascript">
                    KE.show({
                            id : '<%=txtContent.ClientID %>',
                            imageUploadJson : '/manager/handler/news_upload_json.ashx'
                    });
                </script>
                </td>
            </tr>
            
            <tr>
                <td class="inputTableTd">
                    来源
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtPageFrom" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                   作者
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                </td>
                <td class="inputTableTd">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="right">
                    <asp:Button ID="btnDelete" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnReset" runat="server" Text="重置" onclick="btnReset_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnNext" runat="server" Text="下一条" onclick="btnNext_Click" />
                    <asp:Button ID="btnPrev" runat="server" Text="上一条" onclick="btnPrev_Click" />
                    <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </form>
    </div>
    <div id="content_note">
        <h2>所填注意事项</h2>
        <ul>
            <li>红色*号为必填项</li>
            <li>一个中文占两个字符</li>
            <li>标题最多100个字符</li>
            <li>排序号必须是数字，如果为空则排在最前</li>
            <li>关键字最多50个字符，有多个关键字，最好用｜隔开</li>
            <li>摘要最多300个字符</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？'))
        return false;
}
function validata(){
    var title = $.trim($("#<%=txtTitle.ClientID %>").val());
    var orderBy = $.trim($("#<%=txtOrderBy.ClientID %>").val());
    var category = $("#<%=ddlCategory.ClientID %>").val();
    var keywords = $.trim($("#<%=txtKeywords.ClientID %>").val());
    var summary = $.trim($("#<%=txtSummary.ClientID %>").val());
    var content = KE.html("<%=txtContent.ClientID %>");
    var pageFrom = $.trim($("#<%=txtPageFrom.ClientID %>"));
    var author= $.trim($("#<%=txtAuthor.ClientID %>"));
    if(title.length==0){
        alert("请输入标题");
        return false;
    }
    if(validataField.getStrLength(title)>100){
        alert("标题字符超过限制，最多100个字符，一个中文两个字符");
        return false;
    }
    var orderReg = /^\d+$/;
    if(orderBy.length>0){
        if(!orderReg.test(orderBy)){
            alert("排序号必须为数字");
            return false;
        }
    }
    if(category=="0"){
        alert("请选择所属目录");
        return false;
    }
    if(validataField.getStrLength(keywords)>50){
        alert("关键字字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
    if(validataField.getStrLength(summary)>300){
        alert("摘要字符超过限制，最多300个字符，一个中文两个字符");
        return false;
    }
    if(content.length==0){
        alert("请输入内容");
        return false;
    }
    if(validataField.getStrLength(pageFrom)>50){
        alert("文章来源字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
    if(validataField.getStrLength(author)>50){
        alert("作者字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
}
</script>
</asp:Content>

