<%@ Page Title="目录属性" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="category_attribute.aspx.cs" Inherits="administrator_category_attribute" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="js/loading-min.js" type="text/javascript"></script>
<script src="js/jquery.bgiframe.min.js" type="text/javascript"></script>
<script src="js/swfobject.js" type="text/javascript"></script>
<script src="js/jquery.uploadify.v2.0.3.js" type="text/javascript"></script>
<script src="js/list.js" type="text/javascript"></script>
<link href="css/uploadify.css" rel="stylesheet" type="text/css" />
<link href="css/list.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("#btnSearch").bind("click", function() {
            var search = $.trim($("#txtSearch").val());
            window.location.href = "category_attribute.aspx?attributeName=" + search;
        });
    });
</script>
<style type="text/css">
#content_left{float:left;clear:left;}
#content_center{float:left;}
.TreeStyle{margin-left:15px; margin-top:10px; font-size:12px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_product'));
$tabs.tabs('select',indexNum);
$("#category_attribute_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
 <form id="form1"  action="" runat="server">
 <div id="content"> 
    <div id="content_left">
             <div  id="LeftTitle">
            
            </div>
        <asp:TreeView ID="TreeView1" runat="server" ImageSet="Msdn" NodeWrap="True"  CssClass="TreeStyle" 
                ExpandDepth="1" onselectednodechanged="TreeView1_SelectedNodeChanged" >
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" BackColor="#CCCCCC" 
                    BorderColor="#888888" />
                <SelectedNodeStyle Font-Underline="False" 
                    HorizontalPadding="3px" VerticalPadding="1px" BackColor="White" 
                    BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px" />
                <NodeStyle Font-Names="Verdana" Font-Size="12px" ForeColor="Black" 
                    HorizontalPadding="5px" NodeSpacing="1px" VerticalPadding="5px"  />
         </asp:TreeView>
    </div>
    <div id="content_center">
    <div id="content_head">
        <ul>
            <li>
                以属性名搜索:<input type="text" id="txtSearch" /></li>
                <li>
                <input type="button" id="btnSearch" value="" class="btnSearch" />
            </li>
            <li><asp:Button ID="btnAdd" runat="server" Text="" onclick="btnAdd_Click" CssClass="btnAdd" /></li>
            <li><asp:Button ID="btnAllDel" runat="server" Text="" 
                    OnClientClick="return ValidataSelect()" onclick="btnAllDel_Click" CssClass="btnDelRelation" /></li>
            <li><asp:Button ID="Button1" runat="server" Text="" 
                    OnClientClick="return ValidataSelect()" onclick="btnAllDelAttribute_Click" CssClass="btnDelAttr" /></li>
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
                        <th>属性名</th>
                        <th>值列表</th>
                        <th>是否多选</th>
                        <th>是否筛选</th>
                        <th>所属目录</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="chkselect" runat="server" Text="" ToolTip='<%#Eval("Id") %>' /></td>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td><%#Eval("attribute") %></td>
                        <td><%#com.eshop.www.Tools.StringHelper.CutString(Eval("values_list").ToString(),18) %></td>
                        <td><%#bool.Parse(Eval("is_multiple").ToString())?"是":"否" %></td>
                        <td><%#bool.Parse(Eval("is_filter").ToString())?"是":"否" %></td>
                        <td><%#Eval("category_name") %></td>
                        <td>
                            <asp:LinkButton ID="btnUpdate" runat="server" Text="修改" CommandName="update" CommandArgument='<%#Eval("attribute_id") %>' />&nbsp;
                            <asp:LinkButton ID="btnDeleteRelate" runat="server" Text="删除对应关系" CommandName="delete" CommandArgument='<%#Eval("Id") %>' OnClientClick="return ask()" />&nbsp;
                            <asp:LinkButton ID="btnDeleteAttribute" runat="server" Text="删除当前属性" CommandName="deleteAttribute" CommandArgument='<%#Eval("attribute_id") %>' OnClientClick="return ask()" />
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
   
</div>
</form>
</asp:Content>

