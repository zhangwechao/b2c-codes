<%@ Control Language="C#" AutoEventWireup="true" CodeFile="product_click.ascx.cs" Inherits="product_click" %>
<dl class="theleftList01">
   <dt>商品点击排行</dt>
   <dd>
    <ul>
    <asp:Repeater ID="rProductList" runat="server">
    <ItemTemplate>
        <li><a href='p_show.aspx?productId=<%#Eval("Id") %>'><img src='upload-file/images/product/<%#GetImage(int.Parse(Eval("Id").ToString())).Image %>'  alt='<%#GetImage(int.Parse(Eval("Id").ToString())).Alt %>' width="50px" height="50px"/></a>
          <p><a href='p_show.aspx?productId=<%#Eval("Id") %>'><%#Eval("product_name") %></a><br />
         优惠价：<span>￥<%#float.Parse(Eval("sale_price").ToString()).ToString("0") %>元</span></p></li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
   </dd>
   <dd class="btbg"><img src="images/icon-012.gif" /></dd>
  </dl>