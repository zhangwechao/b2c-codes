<%@ Page Title="" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="product_recycle.aspx.cs" Inherits="administrator_product_recycle" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/list.js" type="text/javascript"></script>
<link href="css/list.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
    $("#advSearch").dialog({autoOpen:false,zIndex:100});
    $("#txtBeginDate").datepicker({dateFormat:'yy-mm-dd'});
    $("#txtEndDate").datepicker({dateFormat:'yy-mm-dd'});
    
    $("#btnSearch").bind("click",function(){
        var search = $.trim($("#txtSearch").val());
        window.location.href="product_recycle.aspx?productName="+search;
    });
    $("#btnDisplayAdv").bind("click",function(){
        $("#advSearch").dialog("open");
    });
    
});
function ValidataSelect2(){
    var selects = $("div#content_body input[type=checkbox][name!=chkAll]:checked");
    if(selects.length==0){
        alert("请选择要恢复的数据");
        return false;
    }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_product'));
$tabs.tabs('select',indexNum);
$("#product_content_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
 <form id="form1"  action="" runat="server">
 <div id="advSearch" title="高级搜索"  style="display:none">
    <table>
        <tr>
            <td>产品名称</td>
            <td><input type="text" id="txtProductName" /></td>
        </tr>
        <tr>
            <td>关键字</td>
            <td><input type="text" id="txtKeywords" /></td>
        </tr>
        <tr>
            <td>摘要</td>
            <td><input type="text" id="txtSummary" /></td>
        </tr>
        <tr>
            <td>产品目录</td>
            <td><asp:DropDownList ID="ddlCategory" runat="server">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>产品品牌</td>
            <td><asp:DropDownList ID="ddlBrand" runat="server">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>是否显示</td>
            <td>
                <select id="txtIsShow">
                    <option value="-1">--请选择--</option>
                    <option value="1">是</option>
                    <option value="0">否</option>
                </select>
            </td>
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
                以产品名称搜索:<input type="text" id="txtSearch" /></li><li>
                <input type="button" id="btnSearch" value="" class="btnSearch" />
            </li>
            <li><input type="button" value="" id="btnDisplayAdv"  class="btnDisplayAdv"/></li>
            <li><asp:Button ID="btnAllDel" runat="server" Text=""  class="btnCompleteDel" 
                    OnClientClick="return ValidataSelect()" onclick="btnAllDel_Click" /></li>
             <li><asp:Button ID="btnRecovery" runat="server" Text="" class="btnRecovery"
                    OnClientClick="return ValidataSelect2()" onclick="btnRecovery_Click" /></li>     
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
                        <th>名称</th>
                        <th>推荐</th>
                        <th>最新</th>
                        <th>热销</th>
                        <th>特惠</th>
                        <th>排序号</th>
                        <th>显示</th>
                        <th>目录</th>
                        <th>品牌</th>
                        <th>创建日期</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="chkselect" runat="server" Text="" ToolTip='<%#Eval("Id") %>' /></td>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td title='<%#Eval("product_name") %>' style="cursor:pointer"><%#com.eshop.www.Tools.StringHelper.CutString(Eval("product_name").ToString(),30) %></td>
                        <td><%#bool.Parse(Eval("is_recommend").ToString())?"是":"否"%></td>
                        <td><%#bool.Parse(Eval("is_new").ToString())?"是":"否"%></td>
                        <td><%#bool.Parse(Eval("is_hot").ToString())?"是":"否"%></td>
                        <td><%#bool.Parse(Eval("is_discount").ToString())?"是":"否"%></td>
                        <td><%#Eval("order_by") %></td>
                        <td><%#bool.Parse(Eval("is_show").ToString())?"是":"否"%></td>
                        <td><%#GetCategoryName(int.Parse(Eval("category_id").ToString())) %></td>
                        <td><%#GetBrandName(int.Parse(Eval("brand_id").ToString())) %></td>
                        <td><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-MM-dd") %></td>
                        <td>
                            <asp:LinkButton ID="btnRecovery" runat="server" Text="恢复" CommandName="recovery" CommandArgument='<%#Eval("Id") %>' />&nbsp;
                            <asp:LinkButton ID="btnDelete" runat="server" Text="彻底删除" CommandName="delete" CommandArgument='<%#Eval("Id") %>' OnClientClick="return ask()" />&nbsp;
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
<script type="text/javascript">
    $(document).ready(function() {
        $("#btnAdvSearch").bind("click", function() {
            var productName = $.trim($("#txtProductName").val());
            var keywords = $.trim($("#txtKeywords").val());
            var summary = $.trim($("#txtSummary").val());
            var categoryId = $("#<%=ddlCategory.ClientID %>").val();
            var brandId = $("#<%=ddlBrand.ClientID %>").val();
            var isShow = $("#txtIsShow").val();
            var beginDate = $.trim($("#txtBeginDate").val());
            var endDate = $.trim($("#txtEndDate").val());
            var query = "";
            if (productName.length > 0)
                query += "&productName=" + productName;
            if (keywords.length > 0)
                query += "&keywords=" + keywords;
            if (isShow != "-1")
                query += "&isShow=" + isShow;
            if (categoryId != "0")
                query += "&categoryId=" + categoryId;
            if (brandId != "0")
                query += "&brandId=" + brandId;
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
            window.location.href = "product_recycle.aspx?" + query;
        });
    });
</script>
</asp:Content>

