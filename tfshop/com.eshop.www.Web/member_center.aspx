<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member_center.aspx.cs" Inherits="member_center" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="help.ascx" tagname="help" tagprefix="uc1" %>
<%@ Register src="head.ascx" tagname="head" tagprefix="uc2" %>
<%@ Register src="foot.ascx" tagname="foot" tagprefix="uc3" %>
<%@ Register src="member_left.ascx" tagname="member_left" tagprefix="uc4" %>
<%@ Register src="member_level.ascx" tagname="member_level" tagprefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>ESHOP 网商宝商城</title>
<link href="style/common.css" rel="stylesheet" type="text/css" />
<link href="style/member.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="js/jquery.js"></script>
<script language="javascript" src="js/hdcd.js"></script>
<script language="javascript" src="js/indexhome.js"></script>
<script type="text/javascript">
function confirmReceive(){
    if(!confirm('你确定你已经收到货，如果没有收到货而点击确定的话，将造成财物两空，你确定吗?'))
    return false;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
<uc2:head ID="head1" runat="server" />
<!-- /end header -->
<div class="banner"><img src="images/ad/adfile001.jpg" alt="" /></div>
<!-- /end banner -->
<div class="defaultMain">
    <div class="defaultMainLeft">
        <uc4:member_left ID="member_left1" runat="server" />
   </div>
  <!-- /end defaultMainLeft -->
  <div class="defaultMainRight">
      <div class="memnote1">
    <div class="con">
      <p> 如果订单状态为待付款，可以在会员中心修改订单，之后订单就要进入发货流程，将不能在网上修改。</p>
    </div>
  </div>
  <!--会员级别判断 Begin-->
  <div class="Height4_Area"></div>
  <!--普通会员-->
 <uc5:member_level ID="member_level1" runat="server" />
  <div class="Height4_Area"></div>
  <table class="infobox">
    <caption style="text-align:left">
        我的订单 
    </caption>
    <tbody>
		<tr>
			<th>订单号</th>
			<th>会员名</th>
			<th>时间</th>
			<th>订单金额</th>
			<th>收款金额</th>
			<th>支付方式</th>
			<th>订单状态</th>
			<th>操作</th>
		</tr>
		<asp:Repeater ID="rOrderList" runat="server" onitemdatabound="rOrderList_ItemDataBound" 
                        onitemcommand="rOrderList_ItemCommand">
		<ItemTemplate>
		<tr>
			<td><%#Eval("Id").ToString().ToUpper() %></td>
			<td><%#Eval("user_name") %></td>
			<td><%#Eval("create_date","{0:yyyy-M-d}")%></td>
			<td><%#float.Parse(Eval("total_money").ToString()).ToString("0.0") %>元</td>
			<td><%#string.IsNullOrEmpty(Eval("modify_money").ToString())?float.Parse(Eval("total_money").ToString()).ToString("0.0"):float.Parse(Eval("modify_money").ToString()).ToString("0.0") %>元</td>
			<td><%#Eval("paymentMethod") %></td>
			<td><%#Eval("status") %></td>
			<td>
			    <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="detail" CommandArgument='<%#Eval("Id") %>' Text="订单细则"/>
			    <asp:LinkButton ID="lbtnPayment" runat="server" CommandName="payment" CommandArgument='<%#Eval("Id") %>' Text="我要付款" Visible="false"/>
			    <asp:LinkButton ID="lbtnComment" runat="server" CommandName="comment" CommandArgument='<%#Eval("Id") %>' Text="我要评论" Visible="false"/>
			    <asp:LinkButton ID="lbtnReceiveGoods" runat="server" CommandName="receive" CommandArgument='<%#Eval("Id") %>' Text="确认收货" Visible="false" OnClientClick="return confirmReceive()"/>
			    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="delete" CommandArgument='<%#Eval("Id") %>' Text="删除" Visible="false"/>			    
			</td>
		</tr>
		</ItemTemplate>
		</asp:Repeater>
		<tr>
		    <td colspan="8">
		        <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                        NextPageText="下一页" PrevPageText="上一页" 
                        PageSize="10" AlwaysShow="false" 
                        OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
		    </td>
		</tr>
    </tbody>
  </table>
  <br />
   <table class="infobox">
    <caption style="text-align:left">
        订单细则(订单号：<asp:Literal ID="lOrderNo" runat="server"></asp:Literal>)
    </caption>
    <tbody>
		<tr>
			<th>产品编号</th>
			<th>产品名</th>
			<th>市场价</th>
			<th>ESHOP 网商宝商城价</th>
			<th>数量</th>
			<th>金额</th>
		</tr>
		<asp:Repeater ID="rItemList" runat="server">
		<ItemTemplate>
		<tr>
			<td><%#Eval("product_id") %></td>
			<td><%#new com.eshop.www.BLL.ProductDetailBusiness().GetEntity(int.Parse(Eval("product_id").ToString())).ProductName %></td>
			<td><%#new com.eshop.www.BLL.ProductDetailBusiness().GetEntity(int.Parse(Eval("product_id").ToString())).Price.ToString("0.0") %>元</td>
			<td><%#float.Parse(Eval("price").ToString()).ToString("0.0") %>元</td>
			<td><%#Eval("quantity") %></td>
			<td><%#float.Parse(Eval("total_money").ToString()).ToString("0.0") %>元</td>
		</tr>
		</ItemTemplate>
		</asp:Repeater>
		<tr>
		    <td colspan="7" align="left">
		        <asp:Literal ID="lPaymentMethod" runat="server"></asp:Literal>
		    </td>
		</tr>
		<tr>
		    <td colspan="7" align="left">
		        <asp:Literal ID="lShoppingMethod" runat="server"></asp:Literal>
		    </td>
		</tr>
		<tr>
		    <td colspan="7" align="left">
		        <asp:Literal ID="lShoppingDate" runat="server"></asp:Literal>
		    </td>
		</tr>
		<tr>
		    <td colspan="7" align="left">
		        <asp:Literal ID="lReceiver" runat="server"></asp:Literal>
		    </td>
		</tr>
		<tr>
		    <td colspan="7" align="right">
		        <asp:Literal ID="ltotalMoney" runat="server"></asp:Literal>
		        <asp:Literal ID="ldiscountMoney" runat="server"></asp:Literal>
		        <asp:Literal ID="lendMoney" runat="server"></asp:Literal>
		        <asp:Literal ID="lmodifyMoney" runat="server"></asp:Literal>
		        <asp:Literal ID="lmodifySeason" runat="server"></asp:Literal>
		    </td>
		</tr>
    </tbody>
  </table>
  </div>
<uc1:help ID="help1" runat="server" />    
<!-- /end help -->
<uc3:foot ID="foot1" runat="server" />
</form>
</body>
</html>
