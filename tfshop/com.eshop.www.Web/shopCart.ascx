<%@ Control Language="C#" AutoEventWireup="true" CodeFile="shopCart.ascx.cs" Inherits="shopCart" %>
<%@ Register src="member_level.ascx" tagname="member_level" tagprefix="uc1" %>
<div class="Area_products">
    <div class="gofutop">
        <ul>
            <li class="left"></li>
            <li class="right"></li>
        </ul>
    </div>
    <uc1:member_level ID="member_level1" runat="server" />
    <div>
        <table width='100%' cellpadding='0' cellspacing='1' bgcolor="AACDED" class='Table'
            id='CartTb'>
            <tr class='align_Center Thead'>
                <td width='7%' bgcolor="EBF4FB" style='height: 30px'>
                    商品编号
                </td>
                <td bgcolor="EBF4FB">
                    商品名称
                </td>
                <td width='14%' bgcolor="EBF4FB">
                    市场价
                </td>
                <td width='8%' bgcolor="EBF4FB">
                    ESHOP 网商宝商城价
                </td>
                <td width='9%' bgcolor="EBF4FB">
                    商品数量
                </td>
                <td width='9%' bgcolor="EBF4FB">
                    金额
                </td>
                <td width='7%' bgcolor="EBF4FB">
                    删除商品
                </td>
            </tr>
            <asp:Repeater ID="rProductList" runat="server" 
                onitemcommand="rProductList_ItemCommand">
            <ItemTemplate>
            <tr class='align_Center'>
                <td bgcolor="ffffff" style='padding: 5px 0 5px 0;'>
                    <asp:Literal ID="lProductId" Text='<%#Eval("Id") %>' runat="server"></asp:Literal>
                </td>
                <td bgcolor="ffffff" class='align_Left'>
                    <a target='_blank' href='p_show.aspx?productId=<%#Eval("Id") %>' onclick='this.blur();'>
                       <%#Eval("ProductName") %></a>
                </td>
                <td bgcolor="ffffff">
                    <span class='price'>￥<%#float.Parse(Eval("Price").ToString()).ToString("0") %></span>
                </td>
                <td bgcolor="ffffff">
                    ￥<%#float.Parse(Eval("SalePrice").ToString()).ToString("0") %>
                </td>
                <td bgcolor="ffffff">
                    <asp:ImageButton ID="imgBtnDiv" runat="server" ImageUrl="images/bag_close.gif" CommandArgument='<%#Eval("Id") %>' CommandName="div" />
                    <asp:TextBox ID="txtNumber" runat="server" Text='<%#Eval("SaleNumber") %>' Width="30" MaxLength="4" AutoPostBack="true" ToolTip='<%#Eval("Id") %>' OnTextChanged="txtNumber_TextChanged"></asp:TextBox>
                    <asp:ImageButton ID="imgBtnAdd" runat="server" ImageUrl="images/bag_open.gif"  CommandArgument='<%#Eval("Id") %>' CommandName="add" />
                </td>
                <td bgcolor="ffffff">
                    ￥<%#(float.Parse(Eval("SalePrice").ToString())*int.Parse(Eval("SaleNumber").ToString())).ToString("0") %>
                </td>
                <td bgcolor="ffffff">
                    <asp:LinkButton ID="lBtnDelete" runat="server" CommandName="delete" CommandArgument='<%#Eval("Id") %>'>删除</asp:LinkButton>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan='7' align="right" bgcolor="EBF4FB" class='align_Right Tfoot' style='height: 30px;
                    line-height: 20px;'>
                    <span style='font-size: 14px'>
                    <b>
                    赠送积分<asp:Literal ID="lGetIntegral" runat="server"></asp:Literal>分，商品总金额(含运费)：<span class='price' id='cartBottom_price'>￥<asp:Literal ID="ltotalMoney" runat="server"></asp:Literal></span>元
                    -折扣<asp:Literal ID="lDiscountMoney" runat="server"></asp:Literal>元（<asp:Literal ID="lDiscount2" runat="server"></asp:Literal>）= <asp:Literal ID="lEndTotal" runat="server"></asp:Literal>元
                    </b>
                    </span>
                </td>
            </tr>
        </table>
    </div>
    <div class="jiesu">
        <ul>
            <li class="a3"><asp:LinkButton ID="lBtnDeleteCart" runat="server" 
                    onclick="lBtnDeleteCart_Click">清空购物车</asp:LinkButton></li>
            <li class="a5">
                <a href="order.aspx"><img alt="结算"  src="images/Ck-f-040.gif" id="imgBalance" runat="server" /></a></li>
            <li class="a6">
                <a href="p_list.aspx"><img alt="继续购物" src="images/Ck-f-039.gif"  /></a></li>
        </ul>
    </div>
</div>
