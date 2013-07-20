<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="admin_detail.aspx.cs" Inherits="back_stage_admin_detail" Title="管理登录" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_system'));
$tabs.tabs('select',indexNum);
$("#admin_list_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>管理登录</h2>
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
                    登录名&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtAdminName" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                真实姓名&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                状态
                </td>
                <td class="inputTableTd">
                <asp:CheckBox ID="chkState" runat="server" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    密码&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                确认密码&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                <asp:TextBox ID="txtConfirPass" runat="server"  TextMode="Password"></asp:TextBox>
                </td>
                <td class="inputTableTd">
               用户角色
                </td>
                <td class="inputTableTd">
                <asp:DropDownList ID="ddlRole" runat="server">
                    </asp:DropDownList>
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
    var userName = $.trim($("#<%=txtAdminName.ClientID %>").val());
    var realName = $.trim($("#<%=txtRealName.ClientID %>").val());
    if(userName.length==0){
        alert("请输入登录名");
        return false;
    }
    if(realName.length==0){
        alert("请输入真实姓名");
        return false;
    }
}
</script>
</asp:Content>

