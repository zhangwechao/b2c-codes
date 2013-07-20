<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="order_detail.aspx.cs" Inherits="back_stage_order_detail" Title="订单详细" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_order'));
$tabs.tabs('select',indexNum);
$("#order_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input" style="width:730px">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>订单详细</h2>
        <p>请参考右边的提示修改下面的信息
            </p></div>
        <table id="inputTable" width="700">
            <tr>
                <td colspan="4" align="right" class="inputTableTd">
                    <asp:Button ID="btnSave2" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()"/>
                    <asp:Button ID="btnReturn2" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    订单号
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lOrderNo" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                    会员名
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lUserName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    接收人
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lReceiver" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                    接收地址
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lAddress" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    联系电话
                </td>
                <td class="inputTableTd">
                   <asp:Literal ID="lTel" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                    时间
                </td>
                <td class="inputTableTd">
                   <asp:Literal ID="lCreateDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    是否开发票
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lIsInvoice" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                    发票抬头
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lInvoiceHead" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    订单状态
                </td>
                <td class="inputTableTd">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="inputTableTd">
                    运送方式
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lShoppingMethod" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                
                <td class="inputTableTd">
                    付款方式
                </td>
                <td class="inputTableTd">
                    <asp:Literal ID="lPaymentMethod" runat="server"></asp:Literal>
                </td>
                <td class="inputTableTd">
                    送货时间
                </td>
                <td class="inputTableTd">
                   <asp:Literal ID="lDateType" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="inputTableTd">
                    <table width="100%" id="inputTable">
                        <tr>
                            <td colspan="5" class="inputTableTd">
                                商品总金额：
                                <asp:Literal ID="lTotalMoney" runat="server"></asp:Literal>元-折扣<asp:Literal ID="lDiscountMoney" runat="server"></asp:Literal>元
                                (<asp:Literal ID="lDiscount" runat="server"></asp:Literal>)=
                                <asp:Literal ID="lEndMoney" runat="server"></asp:Literal>元
                            </td>
                        </tr>
                        <tr>
                            <td width="12%" class="inputTableTd">商品编号</td>
                            <td width="30%" class="inputTableTd">商品名称</td>
                            <td width="10%" class="inputTableTd">销售价</td>
                            <td width="10%" class="inputTableTd">商品数量</td>
                            <td width="10%" class="inputTableTd">金额</td>
                        </tr>
                        <asp:Repeater ID="rShoppingCart" runat="server">
                        <ItemTemplate>
                        <tr>
                            <td class="inputTableTd"><%#Eval("product_id") %></td>
                            <td class="inputTableTd"><%#new com.eshop.www.BLL.ProductDetailBusiness().GetEntity(int.Parse(Eval("product_id").ToString())).ProductName %></td>
                            <td class="inputTableTd"><%#float.Parse(Eval("price").ToString()).ToString("0.0") %></td>
                            <td class="inputTableTd"><%#Eval("quantity") %></td>
                            <td class="inputTableTd"><%#float.Parse(Eval("total_money").ToString()).ToString("0.0") %></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td  class="inputTableTd">
                                修改后金额：
                            </td>
                            <td colspan="4" class="inputTableTd" align="left">
                                <asp:TextBox ID="txtModifyMoney" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  class="inputTableTd">
                                修改原因：
                            </td>
                            <td colspan="4" class="inputTableTd"  align="left">
                                <asp:TextBox ID="txtModifySeason" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()"/>
                    <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </form>
    </div>
    <div id="content_note">
        <h2>所填注意事项</h2>
        <ul>
            <li>只有订单状态为待付款才能修改订单金额</li>
            <li>一个中文占两个字符</li>
            <li>修改后金额只能是数字</li>
            <li>修改原因最多200个字符</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function validata(){
    var money = $.trim($("#<%=txtModifyMoney.ClientID %>").val());
    var season = $.trim($("#<%=txtModifySeason.ClientID %>").val());
    
    var priceReg = /^\d+(.)?\d{0,1}/;
    if(money.length>0){
        if(!priceReg.test(money)){
            alert("金额必须为数字");
            return false;
        }
    }
    if(validataField.getStrLength(season)>200){
        alert("修改原因字符超过限制，最多200个字符，一个中文两个字符");
        return false;
    }
}
</script>
</asp:Content>

