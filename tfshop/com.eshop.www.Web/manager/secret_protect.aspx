<%@ Page Title="隐私保护" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="secret_protect.aspx.cs" Inherits="back_stage_secret_protect" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/list.js" type="text/javascript"></script>
<link href="css/list.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("#advSearch").dialog({ autoOpen: false, zIndex: 100 })
        $("#txtBeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#txtEndDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#btnAdvSearch").bind("click", function() {
            var title = $.trim($("#txtTitle").val());
            var keywords = $.trim($("#txtKeywords").val());
            var summary = $.trim($("#txtSummary").val());
            var isShow = $("#txtIsShow").val();
            var beginDate = $.trim($("#txtBeginDate").val());
            var endDate = $.trim($("#txtEndDate").val());
            var query = "";
            if (title.length > 0)
                query += "&title=" + title;
            if (keywords.length > 0)
                query += "&keywords=" + keywords;
            if (isShow != "-1")
                query += "&isShow=" + isShow;
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
            window.location.href = "secret_protect.aspx?" + query;
        });
        $("#btnSearch").bind("click", function() {
            var search = $.trim($("#txtSearch").val());
            window.location.href = "secret_protect.aspx?title=" + search;
        });
        $("#btnDisplayAdv").bind("click", function() {
            $("#advSearch").dialog("open");
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_article'));
$tabs.tabs('select',indexNum);
$("#secret_protect_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
 <div id="content">   
    <form id="form1"  action="" runat="server">
    <div id="advSearch" title="高级搜索" style="display:none">
    <table>
        <tr>
            <td>标题</td>
            <td><input type="text" id="txtTitle" /></td>
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
    <div id="content_head">
        <ul>
            <li>
                以标题搜索:<input type="text" id="txtSearch" /></li><li>
                <input type="button" id="btnSearch" value="" class="btnSearch" />
            </li> 
            <li><input type="button" value="" id="btnDisplayAdv" class="btnDisplayAdv" /></li>
            <li><asp:Button ID="btnAdd" runat="server" Text="" onclick="btnAdd_Click" class="btnAdd" /></li>
            <li><asp:Button ID="btnAllDel" runat="server" Text="" class="btnAllDel"
                    OnClientClick="return ValidataSelect()" onclick="btnAllDel_Click" /></li>
            <li><input type="button" value="" onclick="window.location.href='news_recycle.aspx'" class="btnRecycle" /></li>        
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
                        <th>标题</th>
                        <th>点击率</th>
                        <th>推荐</th>
                        <th>排序号</th>
                        <th>显示</th>
                        <th>所属目录</th>
                        <th>创建日期</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="chkselect" runat="server" Text="" ToolTip='<%#Eval("Id") %>' /></td>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td title='<%#Eval("title") %>' style="cursor:pointer"><%#com.eshop.www.Tools.StringHelper.CutString(Eval("title").ToString(),50) %></td>
                        <td><%#Eval("click_number") %></td>
                        <td><%#bool.Parse(Eval("is_recommend").ToString())?"是":"否"%></td>
                        <td><%#Eval("order_by") %></td>
                        <td><%#bool.Parse(Eval("is_show").ToString())?"是":"否"%></td>
                        <td><%#new com.eshop.www.BLL.NewsCategoryBusiness().GetEntity(int.Parse(Eval("category_id").ToString())).CategoryName %></td>
                        <td><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-MM-dd") %></td>
                        <td>
                            <asp:LinkButton ID="btnUpdate" runat="server" Text="修改" CommandName="update" CommandArgument='<%#Eval("Id") %>' />
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
   </form>
</div>

</asp:Content>

