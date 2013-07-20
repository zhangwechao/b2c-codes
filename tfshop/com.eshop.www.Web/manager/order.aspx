<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="order.aspx.cs" Inherits="back_stage_order" Title="订单" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/list.js" type="text/javascript"></script>
<link href="css/list.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("#advSearch").dialog({ autoOpen: false, zIndex: 100,width:400 });
        $("#btnAdvSearch").bind("click", function() {
            var orderNo = $.trim($("#txtOrderNo").val());
            var userName = $.trim($("#txtUserName").val());
            var shoppingMethodId = $("#<%=ddlShoppingMethod.ClientID %>").val();
            var shoppingDateId = $("#<%=ddlShoppingDate.ClientID %>").val();
            var statusId = $("#<%=ddlStatus.ClientID %>").val();
            var paymentMethodId = $("#<%=ddlPaymentMethod.ClientID %>").val();
            var beginDate = $.trim($("#txtBeginDate").val());
            var endDate = $.trim($("#txtEndDate").val());
            var query = "";
            if (orderNo.length > 0)
                query += "&orderNo=" + orderNo;
            if (userName.length > 0)
                query += "&userName=" + userName;
            if (shoppingMethodId != "0")
                query += "&shoppingMethodId=" + shoppingMethodId;
            if (shoppingDateId != "0")
                query += "&shoppingDateId=" + shoppingDateId;
            if (statusId != "0")
                query += "&statusId=" + statusId;
            if (paymentMethodId != "0")
                query += "&paymentMethodId=" + paymentMethodId;
            if (beginDate.length > 0) {
                var ds = beginDate.split("-");
                if (ds.length != 3) {
                    alert("日期格式不正确，无法转成正确的日期格式");
                    return false;
                }
                query += "&beginDate=" + beginDate;
            }
            if (endDate.length > 0) {
                var ds = endDate.split("-");
                if (ds.length != 3) {
                    alert("日期格式不正确，无法转成正确的日期格式");
                    return false;
                }
                query += "&endDate=" + endDate;
            }
            if (query.length > 0)
                query = query.substr(1);
            window.location.href = "order.aspx?" + query;
        });
        $("#btnSearch").bind("click", function() {
            var search = $.trim($("#txtSearch").val());
            window.location.href = "order.aspx?orderNo=" + search;
        });
        $("#btnDisplayAdv").bind("click", function() {
            $("#advSearch").dialog("open");
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_order'));
$tabs.tabs('select',indexNum);
$("#order_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
  <form id="form1"  action="" runat="server">
  <div id="advSearch" title="高级搜索" style="display:none">
    <table>
        <tr>
            <td>订单号</td>
            <td><input type="text" id="txtOrderNo" /></td>
        </tr>
        <tr>
            <td>会员名</td>
            <td><input type="text" id="txtUserName" /></td>
        </tr>
        <tr>
            <td>运送方式</td>
            <td><asp:DropDownList ID="ddlShoppingMethod" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>时间限制</td>
            <td><asp:DropDownList ID="ddlShoppingDate" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>订单状态</td>
            <td><asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>付款方式</td>
            <td><asp:DropDownList ID="ddlPaymentMethod" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>开始时间</td>
            <td><input type="text" id="txtBeginDate" /></td>
        </tr>
        <tr>
            <td>结束时间</td>
            <td><input type="text" id="txtEndDate" /></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input type="button" value="开始搜索" id="btnAdvSearch" />
            </td>
        </tr>
    </table>
</div>
 <div id="content">    
    <div id="content_head">
        <ul>
            <li>
                以订单号搜索:<input type="text" id="txtSearch" /></li><li>
                <input type="button" id="btnSearch" value="" class="btnSearch" />
            </li>
            <li><input type="button" value="" id="btnDisplayAdv" class="btnDisplayAdv" /></li>
            <li><input type="button" value="" onclick="window.location.reload()" class="btnReload"/></li>
        </ul>
    </div>
    <div id="page">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条，共%RecordCount%条记录"
                        CustomInfoTextAlign="Left" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                        NextPageText="下一页" PagingButtonLayoutType="Span" PrevPageText="上一页" ShowBoxThreshold="10"
                        ShowCustomInfoSection="Left" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="转到" PageSize="10" AlwaysShow="true" ShowPageIndexBox="Always"
                        OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
    </div>
    <div id="content_body">
        <table id="mytable">
            <asp:Repeater ID="rData" runat="server" onitemcommand="rData_ItemCommand">
                <HeaderTemplate>
                    <tr>
                        <th>序号</th>
                        <th>订单号</th>
                        <th>会员名</th>
                        <th>总金额</th>
                        <th>折扣金额</th>
                        <th>运送方式</th>
                        <th>付款方式</th>
                        <th>时间限制</th>
                        <th>订单状态</th>
                        <th>创建日期</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td><%#Eval("Id") %></td>
                        <td><%#Eval("user_name") %></td>
                        <td><%#float.Parse(Eval("total_money").ToString()).ToString("0.0") %></td>
                        <td><%#float.Parse(Eval("discount_money").ToString()).ToString("0.0") %></td>
                        <td><%#Eval("shoppingMethod")%></td>
                        <td><%#Eval("paymentMethod")%></td>
                        <td><%#Eval("date_type")%></td>
                        <td><%#Eval("status")%></td>
                        <td><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-MM-dd") %></td>
                        <td>
                            <asp:LinkButton ID="btnUpdate" runat="server" Text="查看并修改" CommandName="update" CommandArgument='<%#Eval("Id") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div id="page2">
        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条，共%RecordCount%条记录"
                        CustomInfoTextAlign="Left" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                        NextPageText="下一页" PagingButtonLayoutType="Span" PrevPageText="上一页" ShowBoxThreshold="10"
                        ShowCustomInfoSection="Left" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="转到" PageSize="10" AlwaysShow="true" ShowPageIndexBox="Always"
                        OnPageChanged="AspNetPager2_PageChanged"></webdiyer:AspNetPager>
    </div>

</div>

</form>
</asp:Content>
   


