<%@ Page Title="产品品牌" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="product_brand_detail.aspx.cs" Inherits="back_stage_product_brand_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_product'));
$tabs.tabs('select',indexNum);
$("#product_brand_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>产品品牌</h2>
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
                    目录名&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtBrandName" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    排序号
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtOrderBy" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    是否显示
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsShow" runat="server" Checked="true" />显示
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    所属类别
                </td>
                <td class="inputTableTd">
                    <asp:DropDownList ID="ddlFather" runat="server">
                    </asp:DropDownList>
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
           <%-- <tr>
                <td class="inputTableTd" rowspan="3">代表图片</td>
                <td class="inputTableTd" rowspan="3">
                    <asp:Image ID="imgImages" runat="server" Width="100" Height="100" ImageUrl="image/no.jpg" BorderWidth="1" BorderStyle="Solid" />
                    <asp:HiddenField ID="hiddenImage" runat="server" />
                </td>
                <td class="inputTableTd">上传图片</td>
                <td class="inputTableTd">
                    <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                <td class="inputTableTd">
                    <asp:Button ID="btnUpload" runat="server" Text="上传" onclick="btnUpload_Click" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">图片提示</td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtAlt" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="inputTableTd"></td>
                <td class="inputTableTd">
                </td>
                <td></td>
            </tr>--%>
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
            <li>品牌名最多50个字符，最佳8个字符（两个汉字）</li>
            <li>排序号必须是数字，如果为空则排在最前</li>
            <%--<li>图片最佳为100*100，jpg或者gif图片，30kb以内</li>
            <li>图片提示最多50个字符</li>--%>
            <li>备注最多400个字符</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？'))
        return false;
}
function validata(){
    var brandName = $.trim($("#<%=txtBrandName.ClientID %>").val());
    var orderBy = $.trim($("#<%=txtOrderBy.ClientID %>").val());
    var remark = $.trim($("#<%=txtRemark.ClientID %>").val());
    var categoryId = $('#<%=ddlFather.ClientID %>').val();
    if (brandName.length == 0) {
        alert("请输入品牌名");
        return false;
    }
    if(validataField.getStrLength(categoryName)>50){
        alert("品牌字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
    var orderReg = /^\d+$/;
    if(orderBy.length>0){
        if(!orderReg.test(orderBy)){
            alert("排序号必须为数字");
            return false;
        }
    }
    if(categoryId=="0"){
        alert('请输入所属类别');
        return false;
    }
    if(validataField.getStrLength(remark)>400){
        alert("备注字符超过限制，最多400个字符，一个中文两个字符");
        return false;
    }
//    if(validataField.getStrLength(alt)>50){
//        alert("图片提示字符超过限制，最多50个字符，一个中文两个字符");
//        return false;
//    }
}
</script>
</asp:Content>

