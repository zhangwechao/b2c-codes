<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="message_detail.aspx.cs" Inherits="back_stage_message_detail" Title="回复建议" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_member'));
$tabs.tabs('select',indexNum);
$("#online_message_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>回复信息</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable" width="90%">
            <tr>
                <td class="inputTableTd">
                    会员名
                </td>
                <td class="inputTableTd">
                     <asp:Literal ID="lUserName" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                     时间
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lCreateDate" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                    是否显示
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsShow" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                建议标题
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:TextBox ID="txtTitle" runat="server" Enabled="false" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                建议内容
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:TextBox ID="txtContent" runat="server" Enabled="false" TextMode="MultiLine" Width="90%" Height="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                回复人
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:TextBox ID="txtReplyUser" runat="server" Text="管理员"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                回复内容
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:TextBox ID="txtReplyContent" runat="server"  TextMode="MultiLine" Width="90%" Height="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="right">
                    <asp:Button ID="btnDelete" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </form>
    </div>
    <div id="content_note">
        <h2>所填注意事项</h2>
        <ul>
            <li>回复人和内容不能为空</li>
            <li>回复人默认为管理员</li>
            <li>回复内客最多250个汉字</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？'))
        return false;
}
function validata(){
    var replyUser = $.trim($("#<%=txtReplyUser.ClientID %>").val());
    var replyContent = $.trim($("#<%=txtReplyContent.ClientID %>").val());
    if(replyUser.length==0){
        alert("请输入回复人");
        return false;
    }
    if(replyContent.length==0){
        alert('请输入回复内容');
        return false;
    }
}
</script>
</asp:Content>

