<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="favorite.aspx.cs" Inherits="back_stage_favorite" Title="收藏夹" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/jquery.bgiframe.min.js" type="text/javascript"></script>
<script src="js/list.js" type="text/javascript"></script>
<link href="css/list.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("#advSearch").dialog({ autoOpen: false, zIndex: 100,width:400 });
        $("#btnAdvSearch").bind("click", function() {
            var userName = $.trim($("#txtUserName").val());
            var productName = $.trim($("#txtProductName").val());
            var beginDate = $.trim($("#txtBeginDate").val());
            var endDate = $.trim($("#txtEndDate").val());
            var query = "";
            if (userName.length > 0)
                query += "&userName=" + userName;
            if (productName.length>0)
                query += "&productName=" + productName;
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
            window.location.href = "favorite.aspx?" + query;
        });
        $("#btnSearch").bind("click", function() {
            var search = $.trim($("#txtSearch").val());
            window.location.href = "favorite.aspx?userName=" + search;
        });
        $("#btnDisplayAdv").bind("click", function() {
            $("#advSearch").dialog("open");
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_member'));
$tabs.tabs('select',indexNum);
$("#favorite_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
  <form id="form1"  action="" runat="server">
  <div id="advSearch" title="高级搜索" style="display:none">
    <table>
        <tr>
            <td>会员名</td>
            <td><input type="text" id="txtUserName" /></td>
        </tr>
        <tr>
            <td>会员名</td>
            <td><input type="text" id="txtProductName" /></td>
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
 <div id="content" style="clear:both">   
   
    <div id="content_head">
        <ul>
            <li>
                以会员名搜索:<input type="text" id="txtSearch" /></li>
                <li>
                <input type="button" id="btnSearch" value="" class="btnSearch"/>
            </li>
            <li><input type="button" value="" id="btnDisplayAdv" class="btnDisplayAdv"/></li>
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
                        <th><input type="checkbox" name="chkAll" id="chkAll" onclick="selectData()" /><label for="chkAll">全选</label></th>
                        <th>序号</th>
                        <th>会员名称</th>
                        <th>产品名称</th>
                        <th>市场价</th>
                        <th>捷联价</th>
                        <th>创建日期</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="chkselect" runat="server" Text="" ToolTip='<%#Eval("Id") %>' /></td>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td><%#Eval("user_name") %></td>
                        <td><%#Eval("product_name") %></td>
                        <td><%#float.Parse(Eval("price").ToString()).ToString("0.0") %></td>
                        <td><%#float.Parse(Eval("sale_price").ToString()).ToString("0.0") %></td>
                        <td><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-MM-dd") %></td>
                        <td>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("Id") %>' OnClientClick="return ask()" />
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
