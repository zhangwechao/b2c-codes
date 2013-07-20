<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="role_detail.aspx.cs" Inherits="back_stage_role_detail" Title="角色管理" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_system'));
$tabs.tabs('select',indexNum);
$("#role_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>角色管理</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable" width="90%">
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
                    角色名&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
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
                <td class="inputTableTd">
                    备注&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="95%" Height="60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                管理模块
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:CheckBoxList ID="chkModuleList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" CellPadding="5" CellSpacing="20">
                        
                    </asp:CheckBoxList>
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
            <li>如果是修改用户，密码不填写表示保持原密码不变</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？'))
        return false;
}
function validata(){
    var userName = $.trim($("#<%=txtRoleName.ClientID %>").val());
    if(userName.length==0){
        alert("请输角色名");
        return false;
    }
}
</script>
</asp:Content>
