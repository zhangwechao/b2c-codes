<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="member_detail.aspx.cs" Inherits="back_stage_member_detail" Title="会员信息" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_member'));
$tabs.tabs('select',indexNum);
$("#member_info_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>会员信息</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable">
            <tr>
                <td class="inputTableTd">
                    会员名
                </td>
                <td class="inputTableTd">
                     <asp:Literal ID="lUserName" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                     电子邮件
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lEmail" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                </td>
                <td class="inputTableTd">
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">积分</td>
                <td class="inputTableTd">
                   <asp:TextBox ID="txtIntegral" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                     状态
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkState" runat="server" />
                </td>
                <td class="inputTableTd">
                </td>
                <td class="inputTableTd">
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
            <li>没有特别需要请不要更改会员资料</li>
            <li>积分请填写数字</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？删除当前记录会对整个网站运营有影响，请慎重！'))
        return false;
}
function validata(){
    var integral = $.trim($("#<%=txtIntegral.ClientID %>").val());
    var inerreg = /^\d+$/;
    if(integral.length>0){
        if(!inerreg.test(integral)){
            alert("请输入数字");
            return false;
        }
    }
}
</script>
</asp:Content>

