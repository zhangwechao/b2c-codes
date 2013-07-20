<%@ Page Title="" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="product_relate_article.aspx.cs" Inherits="back_stage_product_relate_article" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("#btnSearch").bind('click', function() {
            var title = $('#txtSearch').val();
            if (title.length == 0) {
                alert('请输入搜索关键字');
                return;
            }
            var productId = getQueryString('productId');
            window.location.href = 'product_relate_article.aspx?productId='+productId+'&title='+title;
        });
        var getQueryString = function(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null; ;
        };
    });
</script>
<style type="text/css">
#mytable th {
font: bold 12px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
color: #4f6b72;
border-right: 1px solid #C1DAD7;
border-bottom: 1px solid #C1DAD7;
border-top: 1px solid #C1DAD7;
letter-spacing: 2px;
text-transform: uppercase;
text-align: left;
padding: 6px 6px 6px 12px;
background: #CAE8EA  no-repeat;
}
#mytable td {
border-right: 1px solid #C1DAD7;
border-bottom: 1px solid #C1DAD7;
background: #fff;
font-size:12px;
padding: 6px 6px 6px 12px;
color: #4f6b72;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_product'));
$tabs.tabs('select',indexNum);
$("#product_content_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <form id="form1"  action="" runat="server">
        <h2>相关文章</h2>
        <table id="inputTable">
            <tr>
                <td colspan="2" align="left" class="inputTableTd">
                    搜索文章：<input type="text" id="txtSearch" />&nbsp;
                    <input id="btnSearch" type="button" value="搜索" />&nbsp;
                    <input id="btnReturn" type="button" value="返回" onclick="window.location='product_content.aspx'"/>
                    &nbsp;备注：只有文章设为显示才能相关。
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    已有文章
                </td>
                <td class="inputTableTd">
                    当前<font color='red'><asp:Literal ID="lProductName" runat="server"></asp:Literal></font>已相关文章
                </td>
            </tr>
            <tr>
                <td class="inputTableTd" valign="top">
                    <table id="mytable">
                        <asp:Repeater ID="rRelate" runat="server" onitemcommand="rRelate_ItemCommand">
                            <HeaderTemplate>
                                <tr>
                                    <th>序号</th>
                                    <th>标题</th>
                                    <th>操作</th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td title='<%#Eval("title") %>' style="cursor:pointer"><%#com.eshop.www.Tools.StringHelper.CutString(Eval("title").ToString(),50) %></td>
                                    <td>
                                        <asp:Button ID="btnRelate" runat="server" Text="添加相关" CommandName="relate" CommandArgument='<%#Eval("Id") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="3">
                                <webdiyer:AspNetPager ID="AspNetPager2" runat="server" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                                    NextPageText="下一页" PagingButtonLayoutType="Span" PrevPageText="上一页"
                                    PageSize="20" AlwaysShow="true" 
                                    OnPageChanged="AspNetPager2_PageChanged"></webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="inputTableTd" valign="top">
                    <table id="mytable">
                        <asp:Repeater ID="rYesRelate" runat="server" onitemcommand="rYesRelate_ItemCommand">
                            <HeaderTemplate>
                                <tr>
                                    <th>序号</th>
                                    <th>标题</th>
                                    <th>操作</th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td title='<%#Eval("title") %>' style="cursor:pointer"><%#com.eshop.www.Tools.StringHelper.CutString(Eval("title").ToString(),50) %></td>
                                    <td>
                                        <asp:Button ID="btnNoRelate" runat="server" Text="去掉相关" CommandName="NoRelate" CommandArgument='<%#Eval("relateId") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</div>
</asp:Content>


