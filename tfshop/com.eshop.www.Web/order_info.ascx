<%@ Control Language="C#" AutoEventWireup="true" CodeFile="order_info.ascx.cs" Inherits="order_info" %>
<div class="SmainBox">
  <div class="SmainContent">
    <h2 class="f14b">订单收货信息</h2>
    <div class="Smsg-list">
      <ul>
        <li><span>收 件 人：</span><asp:Literal ID="lReceiveName" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenReceiveName" runat="server" />
        </li>
        <li><span>联系电话：</span><asp:Literal ID="lReceiveTel" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenReceiveTel" runat="server" />
        </li>
        <li><span>收件人地址：</span><asp:Literal ID="lReceiveAddress" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenReceiveAddress" runat="server" />
        </li>
      </ul>
    </div>
    <h2 class="f14b">订单详情</h2>
    <div class="Smsg-list">
      <ul>
        <li><span>支付方式：</span><asp:Literal ID="lPaymentType" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenPaymentType" runat="server" />
        </li>
        <li><span>是否开发票：</span><asp:Literal ID="lIsInvoice" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenIsInvoice" runat="server" />
        </li>
        <li><span>发票抬头：</span><asp:Literal ID="lInvoice" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenInvoice" runat="server" />
        </li>
        <li><span>配送方式：</span><asp:Literal ID="lShoppingType" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenShoppingType" runat="server" />
        </li>
        <li><span>送货日期规定：</span><asp:Literal ID="lShoppingDate" runat="server"></asp:Literal>
        <asp:HiddenField ID="hiddenShoppingDate" runat="server" />
        </li>
      </ul>
    </div>
    <h2 class="f14b">订单备注</h2>
    <div class="Smsg-list">
      <ul>
      <li><span>备注：</span><asp:Literal ID="lRemark" runat="server"></asp:Literal>
       <asp:HiddenField ID="hiddenRemark" runat="server" />
      </li>
      </ul>
      </div>
    <h2 class="f14b">订单配送信息</h2>
    <div class="Sgoods-list">
            <div class="Sgoods-list-content">
                <h3 class="f14b">
                    商品清单</h3>
                <div class="StableBox">
                    <div class="Sthead">
                        <ul>
                            <li class="Sgoods-number">商品编码</li>
                            <li class="Sgoods-name">商品名称</li>
                            <li class="Sgoods-more">数量</li>
                            <li class="Sgoods-money">市场价</li>
                            <li class="Sgoods-money">ESHOP 网商宝商城价</li>
                        </ul>
                        <div class="cB">
                        </div>
                    </div>
                    <asp:Repeater ID="rShoppcart" runat="server">
                    <ItemTemplate>
                    <div class="Stbody">
                        <ul>
                            <li class="Sgoods-numberC"><%#Eval("Id") %></li>
                            <li class="Sgoods-nameC"><a href='p_show.aspx?productId=<%#Eval("Id") %>'>
                                <%#Eval("ProductName") %></a></li>
                            <li class="Sgoods-moreC"><%#Eval("SaleNumber") %></li>
                            <li class="Sgoods-moneyC"><%#float.Parse(Eval("Price").ToString()).ToString("0") %>元</li>
                            <li class="Sgoods-moneyC"><%#float.Parse(Eval("SalePrice").ToString()).ToString("0") %>元</li>
                        </ul>
                        <div class="cB">
                        </div>
                    </div>
                    </ItemTemplate>
                    </asp:Repeater>
                    <div class="Stbody">
                        <ul>
                            <div align="center">
                                商品总金额(含运费)：<span class='price' id='cartBottom_price'>￥<asp:Literal ID="ltotalMoney" runat="server"></asp:Literal></span>元
                                    -折扣<asp:Literal ID="lDiscountMoney" runat="server"></asp:Literal>元（<asp:Literal ID="lDiscount2" runat="server"></asp:Literal>）= <asp:Literal ID="lEndTotal" runat="server"></asp:Literal>元</div>
                        </ul>
                        <div class="cB">
                        </div>
                    </div>
                </div>
            </div>
        </div>
  </div>
  <div style="margin-top:20px; margin-left:300px">
        <input type="button" value="修改订单信息" style="width:150px; height:30px;" onclick="window.history.back()" />&nbsp;<asp:Button 
            ID="btnConfirm" runat="server" Width="150" Height="30" Text="确认订单信息" 
            onclick="btnConfirm_Click" />
  </div>
</div>