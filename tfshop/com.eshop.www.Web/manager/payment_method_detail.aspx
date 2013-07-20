<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="payment_method_detail.aspx.cs" Inherits="back_stage_payment_method_detail" Title="付款方式" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_order'));
$tabs.tabs('select',indexNum);
$("#payment_method_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>付款方式</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable">
            <tr>
                <td colspan="6" align="right" class="inputTableTd">
                    <asp:Button ID="btnDelete2" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnReset2" runat="server" Text="重置" onclick="btnReset_Click" />
                    <asp:Button ID="btnSave2" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnReturn2" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    付款方式&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtMethod" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                </td>
                <td class="inputTableTd">
                </td>
                <td class="inputTableTd">
                </td>
                <td class="inputTableTd">
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">备注</td>
                <td colspan="5" class="inputTableTd">
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" Columns="69"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="right">
                    <asp:Button ID="btnDelete" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnReset" runat="server" Text="重置" onclick="btnReset_Click" />
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
            <li>红色*号为必填项</li>
            <li>一个中文占两个字符</li>
            <li>付款方式最多50个字符，最佳8个字符（4个汉字）</li>
            <li>备注最多200个字符</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？删除当前记录会对整个网站运营有影响，请慎重！'))
        return false;
}
function validata(){
    var method = $.trim($("#<%=txtMethod.ClientID %>").val());
    var remark = $.trim($("#<%=txtRemark.ClientID %>").val());
    if(method.length==0){
        alert("请输入运送方式");
        return false;
    }
    if(validataField.getStrLength(method)>50){
        alert("付款方式字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
    if(validataField.getStrLength(remark)>200){
        alert("备注字符超过限制，最多200个字符，一个中文两个字符");
        return false;
    }
}
</script>
</asp:Content>

