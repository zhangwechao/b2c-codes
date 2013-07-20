<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_list.aspx.cs" Inherits="p_list"
    Title="ESHOP 网商宝商城" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="help.ascx" TagName="help" TagPrefix="uc1" %>
<%@ Register Src="head.ascx" TagName="head" TagPrefix="uc2" %>
<%@ Register Src="foot.ascx" TagName="foot" TagPrefix="uc3" %>
<%@ Register Src="category.ascx" TagName="category" TagPrefix="uc4" %>
<%@ Register Src="product_click.ascx" TagName="product_click" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>ESHOP 网商宝商城</title>
    <link href="style/common.css" rel="stylesheet" type="text/css" />
    <link href="style/p_list.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="js/jquery.js"></script>
    <script language="javascript" src="js/indexhome.js"></script>
    <script language="javascript" type="text/javascript" src="js/flash.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            function getQueryString(name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
                var r = window.location.search.substr(1).match(reg);
                if (r != null)
                    return unescape(r[2]);
                return null;
            };
            var categoryId = getQueryString("categoryId");
            var brandId = getQueryString("brandId");
            var isNew = getQueryString("isNew");
            if (isNew != null)
                $($("ul.navigation a")[1]).addClass('hover');
            else if (categoryId == "544")
                $($("ul.navigation a")[2]).addClass('hover');
            else if (categoryId == "543")
                $($("ul.navigation a")[3]).addClass('hover');
            else if (categoryId == "542")
                $($("ul.navigation a")[4]).addClass('hover');
            else if (categoryId == "541")
                $($("ul.navigation a")[5]).addClass('hover');
            else if (categoryId == "540")
                $($("ul.navigation a")[6]).addClass('hover');
            else if (categoryId == "539")
                $($("ul.navigation a")[7]).addClass('hover');
            else if (categoryId == "537")
                $($("ul.navigation a")[8]).addClass('hover');
            if (categoryId != null && categoryId.length > 0)
                $('li#li_category_' + categoryId + '').css('background-image', 'url(/images/icon-0388.gif)');
            if (brandId != null && brandId.length > 0)
                $('li#li_brand_' + brandId).css('background-image', 'url(/images/icon-0388.gif)');

            var attributesids = $('#hiddenAttributeIds').val();
            var attrIds = attributesids.split(',');
            for (var i = 0; i < attrIds.length; i++) {
                var argValue = getQueryString('attribute_' + attrIds[i]);
                if (argValue != null && argValue.length > 0) {
                    //如果查询的属性值不为空，将查询的属性值变成红色
                    $('li#li_attribute_' + attrIds[i] + '_' + argValue.split('_')[1]).css('background-image', '/images/icon-0388.gif');
                }
            }
        });
    </script>
    <style type="text/css">
        .pageButton
        {
            border: 1px solid #D4D4D4;
            display: block;
            float: left;
            font-size: 12px;
            height: 20px;
            line-height: 20px;
            margin-left: 2px;
            padding: 2px 5px 5px;
            width: 30px;
        }
        .aspNetPage
        {
            float: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:head ID="head1" runat="server" />
    <!-- /end banner -->
    <div class="defaultMain">
        <div class="defaultMainLeft">
            <uc4:category ID="category1" runat="server" />
            <!-- /end 商品分类 -->
            <uc5:product_click ID="product_click1" runat="server" />
            <!-- /end theleftList01 -->
        </div>
        <!-- /end defaultMainLeft -->
        <div class="defaultMainRight">
            <div style="padding-bottom: 10px;">
                <img src="images/bnners.jpg" alt="" />
            </div>
            <!-- /end thelistone -->
            <dl class="thelistwo">
                <dt>
                    <h4>
                        按商品展示</h4>
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" NextPageText="下一页" PrevPageText="上一页"
                        ShowBoxThreshold="6" PageSize="28" AlwaysShow="true" OnPageChanged="AspNetPager2_PageChanged">
                    </webdiyer:AspNetPager>
                </dt>
                <dt class="choosed">
                    <ul>
                        <asp:HiddenField ID="hiddenCategoryId" runat="server" Value="0" />
                        <h5>
                            产品类别：</h5>
                        <asp:Repeater ID="rCategoryList" runat="server">
                            <ItemTemplate>
                                <li id='li_category_<%#Eval("Id") %>'><a href='p_list.aspx?<%#GetUrl("categoryId",Eval("Id").ToString()) %>'>
                                    <%#Eval("category_name") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li class="removeCon">
                            <asp:LinkButton ID="lBtnCategoryRemove" runat="server" ForeColor="#990d00" Visible="false"
                                OnClick="lBtnCategoryRemove_Click">移出条件</asp:LinkButton></li>
                    </ul>
                    <ul>
                        <h5>
                            产品品牌：</h5>
                        <asp:Repeater ID="rBrandList" runat="server">
                            <ItemTemplate>
                                <li id='li_brand_<%#Eval("Id") %>'><a href='p_list.aspx?<%#GetUrl("brandId",Eval("Id").ToString()) %>'>
                                    <%#Eval("brand_name") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li class="removeCon">
                            <asp:LinkButton ID="lBtnBrandRemove" runat="server" ForeColor="#990d00" Visible="false"
                                OnClick="lBtnBrandRemove_Click">移出条件</asp:LinkButton></li>
                    </ul>
                    <asp:HiddenField ID="hiddenAttributeIds" runat="server" />
                    <asp:Repeater ID="rAttributeList" runat="server" OnItemDataBound="rAttributeList_ItemDataBound"
                        OnItemCommand="rAttributeList_ItemCommand">
                        <ItemTemplate>
                            <ul>
                                <h5>
                                    <%#Eval("attribute") %>：</h5>
                                <asp:Repeater ID="rAttributeValueList" runat="server">
                                    <ItemTemplate>
                                        <li id='li_attribute_<%#Eval("Id") %>_<%#Container.ItemIndex %>'><a href='p_list.aspx?<%#GetUrl("attribute_"+Eval("Id").ToString(),Eval("ValuesList").ToString()+"_"+Container.ItemIndex.ToString()) %>'>
                                            <%#Eval("ValuesList")%></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <li class="removeCon">
                                    <asp:LinkButton ID="lBtnRemove" runat="server" ForeColor="red" Visible="false" CommandName="remove"
                                        CommandArgument='<%#Eval("attribute_id") %>'>移出条件</asp:LinkButton></li>
                            </ul>
                        </ItemTemplate>
                    </asp:Repeater>
                </dt>
                <dd>
                    <ul>
                        <asp:Repeater ID="rNewProductList" runat="server">
                            <ItemTemplate>
                                <li><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank">
                                    <img src='upload-file/images/product/<%#GetImage(int.Parse(Eval("Id").ToString())).Image %>'
                                        alt='<%#GetImage(int.Parse(Eval("Id").ToString())).Alt %>' width="162px" height="162px" /></a>
                                    <p>
                                        <a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank">
                                            <%#Eval("product_name") %></a></p>
                                    <%#float.Parse(Eval("sale_price").ToString()).ToString("0") %>元<span>￥<%#float.Parse(Eval("price").ToString()).ToString("0") %>元</span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <ul class="Fenye" style="clear: both">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager2" runat="server" NextPageText="下一页" PrevPageText="上一页"
                                ShowBoxThreshold="6" PageSize="28" AlwaysShow="true" OnPageChanged="AspNetPager2_PageChanged"
                                FirstPageText="First" LastPageText="Last" CurrentPageButtonClass="pageButton"
                                CurrentPageButtonStyle="margin-right: 0px;padding-left: 0px;text-decoration:none"
                                NextPrevButtonClass="page_width" ShowFirstLast="False" SubmitButtonClass="" CssClass="aspNetPage">
                            </webdiyer:AspNetPager>
                        </li>
                    </ul>
                </dd>
            </dl>
            <!-- /end thelistwo -->
        </div>
        <!-- /end defaultMainRight -->
    </div>
    <!-- /end defaultMain -->
    <!-- /end banner -->
    <uc1:help ID="help1" runat="server" />
    <!-- /end help -->
    <uc3:foot ID="foot1" runat="server" />
    </form>
</body>
</html>
