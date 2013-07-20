<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="member_level.aspx.cs" Inherits="back_stage_member_level" Title="会员级别" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/list.js" type="text/javascript"></script>
<link href="css/list.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("#btnSearch").bind("click", function() {
            var search = $.trim($("#txtSearch").val());
            window.location.href = "member_level.aspx?levelName=" + search;
        });
    });
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_member'));
$tabs.tabs('select',indexNum);
$("#member_level_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
 <div id="content" style="clear:both">   
    <form id="form1"  action="" runat="server">
    <div id="content_head">
        <ul>
            <li>
                以级别名称搜索:<input type="text" id="txtSearch" /></li>
                <li>
                <input type="button" id="btnSearch" value="" class="btnSearch" />
            </li>
            <li><asp:Button ID="btnAdd" runat="server" Text="" class="btnAdd"  onclick="btnAdd_Click" /></li>
            <li><asp:Button ID="btnAllDel" runat="server" Text="" class="btnAllDel" 
                    OnClientClick="return ValidataSelect()" onclick="btnAllDel_Click" /></li>      
            <li><input type="button" value="" onclick="window.location.reload()" class="btnReload" /></li>
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
                        <th>积分区间</th>
                        <th>级别名称</th>
                        <th>折扣</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="chkselect" runat="server" Text="" ToolTip='<%#Eval("Id") %>' /></td>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td><%#Eval("min_integral") %>-<%#Eval("max_integral") %></td>
                        <td><%#Eval("level_name") %></td>
                        <td><%#Eval("discount")%></td>
                        <td>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("Id") %>' OnClientClick="return ask()" />&nbsp;
                            <asp:LinkButton ID="btnUpdate" runat="server" Text="修改" CommandName="update" CommandArgument='<%#Eval("Id") %>' />
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

