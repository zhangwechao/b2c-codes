<%@ Control Language="C#" AutoEventWireup="true" CodeFile="favorite.ascx.cs" Inherits="favorite" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="member_level.ascx" tagname="member_level" tagprefix="uc1" %>
<div class="htArea_right">
  <div class="Height4_Area"></div>
  <%--<div class="memnote1">
    <div class="con">
      <p> <span class="fl">如果要查看您的订单，请点击</span><a href="#"
                            class="a2 blod">订单查询</a></p>
      <p> 如果要激活您的礼品卡/礼券，请点击<a href="#" class="ml10 a2 blod">礼品卡/礼券激活</a></p>
      <p> 订单提交1个小时之内可以在“我的麦网”中<a href="#" class="a2 blod">修改订单</a>，之后订单就要进入发货流程，将不能在网上修改。</p>
    </div>
  </div>--%>
  <!--会员级别判断 Begin-->
  <div class="Height4_Area"></div>
  <!--普通会员-->
  <uc1:member_level ID="member_level1" runat="server" />
  <div class="Height4_Area">
    </div>
  <table class="infobox">
    <caption>
      我的收藏（暂存架） 
    </caption>
    <tbody>
		<tr>
			<th colspan="2">商  品 </th>
			<th width="9%">市场价</th>
			<th width="9%">ESHOP 网商宝商城价</th>
			<th width="20%">存放时间</th>
			<th width="20%">操    作</th>
		</tr>
		<asp:Repeater ID="rProductList" runat="server" 
            onitemcommand="rProductList_ItemCommand">
		<ItemTemplate>
		<tr>
			<td width="12%"><img src='upload-file/images/product/<%#GetImage(int.Parse(Eval("product_id").ToString())).Image %>' alt='<%#GetImage(int.Parse(Eval("product_id").ToString())).Alt %>' width="70" height="70" /></td>
			<td width="30%"><%#Eval("product_name") %></td>
			<td>￥<%#float.Parse(Eval("price").ToString()).ToString("0")%></td>
			<td style="color:Red">￥<%#float.Parse(Eval("sale_price").ToString()).ToString("0")%></td>
			<td><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-M-d HH:mm:ss") %></td>
			<td>
                <asp:LinkButton ID="lbtnBuyNow" runat="server" CommandArgument='<%#Eval("product_id") %>' CommandName="buyNow">立即购买</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbtnAddShoppingCart" runat="server" CommandArgument='<%#Eval("product_id") %>' CommandName="addShoppingCart">加入购物车</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="delete">删除</asp:LinkButton>&nbsp;
			</td>
		</tr>
		</ItemTemplate>
		</asp:Repeater>
		<tr>
		    <td colspan="6">
		        <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                        NextPageText="下一页" PrevPageText="上一页" ShowBoxThreshold="6"  
                        PageSize="28" AlwaysShow="false" 
                        OnPageChanged="AspNetPager2_PageChanged"></webdiyer:AspNetPager>
		    </td>
		</tr>
		
    </tbody>
  </table>
  </div>