<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="member_level_detail.aspx.cs" Inherits="member_level_detail" Title="会员级别" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_member'));
$tabs.tabs('select',indexNum);
$("#member_level_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>会员级别</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable">
            
            <tr>
                <td class="inputTableTd">
                     级别名称&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtLevelName" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    最少积分&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtMinIntegral" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    最大积分&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtMaxIntegral" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">折扣&nbsp;<font color="red">*</font></td>
                <td colspan="5" class="inputTableTd">
                    <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox>
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
            <li>级别名称最多50个字符，最佳8个字符（4个汉字）</li>
            <li>最大积分，最小积分填入数字</li>
            <li>折扣一栏填入小于1的小数,如0.02表示9.8折， 0.10表示9折</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？删除当前记录会对整个网站运营有影响，请慎重！'))
        return false;
}
function validata(){
    var levelName = $.trim($("#<%=txtLevelName.ClientID %>").val());
    var maxIntegral = $.trim($("#<%=txtMaxIntegral.ClientID %>").val());
    var minIntegral = $.trim($("#<%=txtMinIntegral.ClientID %>").val());
    var discount = $.trim($("#<%=txtDiscount.ClientID %>").val());
    if(levelName.length==0){
        alert("请输入级别名称 ");
        return false;
    }
    if(validataField.getStrLength(levelName)>50){
        alert("级别名称字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
    var intReg = /^\d+$/;
    if(!intReg.test(minIntegral)){
        alert("最小积分需要输入数字");
        return false;
    }
    if(!intReg.test(maxIntegral)){
        alert("最大积分需要输入数字");
        return false;
    }
    if(isNaN(parseFloat(discount)) || parseFloat(discount)>1){
        alert("折扣输入的不是数字或者输入数字大于1");
        
        return false;
    }
}
</script>
</asp:Content>

