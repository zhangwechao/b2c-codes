<%@ Page Title="目录属性" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="category_attribute_detail.aspx.cs" Inherits="category_attribute_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.TreeStyle{margin-left:15px; margin-top:10px; font-size:12px;}
#treeTd td {
    height:auto;
    line-height:normal;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_product'));
$tabs.tabs('select',indexNum);
$("#category_attribute_page").css({ "background-image": "url(image/A-dmin-0122.gif)" }); 
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>目录属性</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable">
            <tr>
                <td colspan="2"  align="right" class="inputTableTd">
                    <asp:Button ID="btnDelete2" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnReset2" runat="server" Text="重置" onclick="btnReset_Click" />
                    <asp:Button ID="btnSave2" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnNext2" runat="server" Text="下一条" onclick="btnNext_Click" />
                    <asp:Button ID="btnPrev2" runat="server" Text="上一条" onclick="btnPrev_Click" />
                    <asp:Button ID="btnReturn2" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">属性名<font color="red">*</font></td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtAttributeName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">值列表</td>
                <td class="inputTableTd">
                     <asp:TextBox ID="txtValuesList" runat="server" Width="500" TextMode="MultiLine" Height="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">是否多选</td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkMultiple" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">是否筛选</td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkFilter" runat="server" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">所属类别<br /><input type="checkbox" id="chkAll" name="chkAll" onclick="selectData()" />全选</td>
                <td  class="inputTableTd" id="treeTd">
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="Msdn" NodeWrap="True"  CssClass="TreeStyle" 
                        ExpandDepth="1"  ShowCheckBoxes="All" PathSeparator=","  >
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" BackColor="#CCCCCC" 
                            BorderColor="#888888" />
                        <SelectedNodeStyle Font-Underline="False" 
                            HorizontalPadding="3px" VerticalPadding="1px" BackColor="White" 
                            BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="12px" ForeColor="Black" 
                            HorizontalPadding="5px" NodeSpacing="1px" VerticalPadding="5px"  />
                 </asp:TreeView>
                </td>
            </tr>
            <tr>
                <td colspan="2"  align="right">
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
            <li>属性名最多50个字符，最佳8个字符（4个汉字）</li>
            <li style="height:90px;">值列表如果有多个值中间用英文逗号隔开，<br />如颜色，可以白，黑，红。也可以不用填写<br />
                不填写表示属性值将手动输入。不填写不能用于筛选</li>
            <li style="height:60px;">多选表示属性值以复选框展示，<br />单选表示属性值以下拉框展示</li>
            <li style="height:60px;">是否筛选表示前台页面有没有这个属性的筛选条件<br />这个值必须是单选才可用</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？'))
        return false;
}
function validata(){
    var attributeName = $.trim($("#<%=txtAttributeName.ClientID %>").val());
    var valuelist = $.trim($("#<%=txtValuesList.ClientID %>").val());
    var selects = $("div#content_input input[type=checkbox][name!=chkAll]:checked");
    if (attributeName.length == 0) {
        alert("请输入属性");
        return false;
    }
    if(validataField.getStrLength(attributeName)>50){
        alert("标题字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
    if (validataField.getStrLength(valuelist) > 200) {
        alert("值列表字符超过限制，最多200个字符，一个中文两个字符");
        return false;
    }
    if (selects.length == 0) {
        alert("请选择所属类别");
        return false;
    }
}
function selectData() {
    var check = $("div#content_input input[type=checkbox][name=chkAll]").attr("checked");
    if (check)
        $("td#treeTd input[type=checkbox][name!=chkAll]").attr("checked", true);
    else
        $("td#treeTd input[type=checkbox][name!=chkAll]").attr("checked", false);
}
</script>
</asp:Content>

