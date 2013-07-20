<%@ Control Language="C#" AutoEventWireup="true" CodeFile="order.ascx.cs" Inherits="order" %>
<%@ Import Namespace="com.eshop.www.Model" %>
<%@ Import Namespace="com.eshop.www.BLL" %>
<%@ Import Namespace="System.Data" %>
<div class="SmainBox">
    <div class="SmainContent">
        <h2 class="f14b">
            填写订单收货信息</h2>
        <div class="Smsg-list">
            <ul>
                <li><span><font color="red">*</font>收&nbsp;&nbsp;货&nbsp;&nbsp;人：</span><input type="text" name="receiveName" id="receiveName"/></li>
                <li><span><font color="red">*</font>收件人地址：</span><input  style="width:350px" type="text" name="receiveAddress" id="receiveAddress"/></li>
                <li><span><font color="red">*</font>联系电话：</span>&nbsp;&nbsp;<input type="text" name="receiveTel" id="receiveTel" /></li>
            </ul>
        </div>
        <h2 class="f14b">
            支付和配送方式</h2>
        <div class="Smsg-list">
            <ul>
                <li><b>支付方式：</b></li>
                <%DataRecordTable table = new PaymentMethodBusiness().GetList("Id,method,remark","Id",false,1,10,"");
                  string method = string.Empty;
                  string remark = string.Empty;
                  string Id = string.Empty;
                    foreach (DataRow row in table.Table.Rows)
                    {
                        method = row["method"].ToString();
                        remark = row["remark"].ToString();
                        Id = row["Id"].ToString();
                        if (Id == "1")
                        { %>
                            <li>
                    <input name="paymentType" type="radio" value="<%=Id %>" checked="checked" /><span><%=method%>：</span><%=remark%>
                    </li>
                       <% }
                        else if (Id == "3")
                        { 
                        %>
                           <li>
                                <input name="paymentType" type="radio" value="<%=Id %>" /><span><%=method%>：</span><%=remark%>
                                <p class="bank">
                                    <b>支持以下银行在线支付</b>
                                    <img src="images/longye.jpg" alt="中国农业银行" width="132px" height="43px"/>
                                </p>
                            </li> 
                        <%}
                        else { %>
                            <li>
                                <input name="paymentType" type="radio" value="<%=Id %>" /><span><%=method %>：</span><%=remark %>
                            </li>
                       <% }
                    }
                     %>
                
                
                <li><span>是否开发票：</span><input type="checkbox" value="1" name="IsInvoice" /></li>
                <li><span>发票抬头：</span><input name="invoice" id="invoice" type="text" /></li>
                <li><b>配送方式：</b></li>
                
                <%table = new ShoppingMethodBusiness().GetList("Id,method,remark","Id",false,1,10,"");
                  
                  foreach (DataRow row in table.Table.Rows) 
                  {
                      Id = row["Id"].ToString();
                      method = row["method"].ToString();
                      remark = row["remark"].ToString();
                      if (Id == "1")
                      { %>
                        <li>
                        <input name="shoppingType" type="radio" value="<%=Id %>" checked="checked" />
                        <%=method%>：<%=remark%>
                        </li>
                     <% }
                      else { %>
                        <li>  
                            <input name="shoppingType" type="radio" value="<%=Id %>" />
                            <%=method %>：<%=remark %>
                          </li>
                     <% }
                  }
                    %>   
                
                    
                <li><b>送货日期规定：</b></li>
                <%table = new ShoppingDateBusiness().GetList("Id,date_type,remark","Id",false,1,10,"");
                  string date_type = string.Empty;
                    foreach(DataRow row in table.Table.Rows)
                    {
                        Id = row["Id"].ToString();
                        date_type = row["date_type"].ToString();
                        remark = row["remark"].ToString();
                        if(Id=="1")
                        {%>
                            <li>
                            <input name="shoppingDate" type="radio" value="<%=Id %>" checked="checked" />
                            <%=date_type %>
                            </li>
                        <%}else
                        {%>
                            <li>  
                            <input name="shoppingDate" type="radio" value="<%=Id %>" />
                           <%=date_type %>
                            </li>
                       <% }
                    }
                       %>
                 
            </ul>
        </div>
        <h2 class="f14b">
            订单备注</h2>
            <div class="Smsg-list">
                <ul>
                    <li>
                         <span>备注：</span>&nbsp;&nbsp;<input  style="width:350px" type="text" name="remark" id="remark"/>
                    </li>
                </ul>
            </div>
        <h2 class="f14b">
            订单配送信息</h2>
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
        <input type="button" value="提交订单信息" style="width:150px; height:30px;" id="btnConfirm" />
    </div>
</div>
<script type="text/javascript">
$(document).ready(function(){
    $('#btnConfirm').bind('click',function(){
        var receiveName = $.trim($('#receiveName').val());
        var receiveAddress = $.trim($('#receiveAddress').val());
        var receiveTel = $.trim($('#receiveTel').val());
        if(receiveName.length==0){
            alert('请输入收货人姓名');
            return;
        }
        if(receiveAddress.length==0){
            alert('请输入收货人地址');
            return;
        }
        if(receiveTel.length==0){
            alert('请输入收货人电话');
            return;
        }
        document.forms[0].action="order_info.aspx";
        document.forms[0].submit();
    });
});
</script>
