<%@ Page Language="C#" AutoEventWireup="true" CodeFile="product_comment.aspx.cs"
    Inherits="product_comment" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="head.ascx" TagName="head" TagPrefix="uc1" %>
<%@ Register Src="member_left.ascx" TagName="member_left" TagPrefix="uc2" %>
<%@ Register Src="help.ascx" TagName="help" TagPrefix="uc3" %>
<%@ Register Src="foot.ascx" TagName="foot" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>ESHOP 网商宝商城</title>
    <link href="style/common.css" rel="stylesheet" type="text/css" />
    <link href="style/member.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="js/jquery.js"></script>
    <script language="javascript" src="js/hdcd.js"></script>
    <script language="javascript" src="js/indexhome.js"></script>
    <style type="text/css">
    .productList{border-top: 1px solid #e1dfe0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:head ID="head1" runat="server" />
    <!-- /end header -->
    <div class="banner">
        <img src="images/ad/adfile001.jpg" alt="" /></div>
    <!-- /end banner -->
    <div class="defaultMain">
        <div class="defaultMainLeft">
            <uc2:member_left ID="member_left1" runat="server" />
        </div>
        <!-- /end defaultMainLeft -->
        <div class="defaultMainRight">
            <div id="right">
                <div class="dh" style="height: 30px;">
                    <div class="span1">
                        <p>
                            <a href="product_comment.aspx">写产品评论</a></p>
                    </div>
                    <div class="span">
                        <p>
                            <a href="my_comment.aspx">我的评论</a></p>
                    </div>
                </div>
                <div class="styCCC">
                    <p style="font-size: 0;">
                        <img src="images/etbj.gif" /></p>
                    <div class="impCont">
                        <p style="border-bottom: 1px solid #a10000; padding: 5px 0; margin: 5px 0 15px 0px;">
                            <strong><span style="font-size: 14px; padding-left: 10px;">写评论:</span></strong>你可对已成功购买的商品进行评价，和大家说说你的切身感受
                        </p>
                        <asp:DataList ID="dlProductList" Width="100%" runat="server" RepeatLayout="Table" RepeatColumns="2"  CssClass="productList" CellPadding="0" CellSpacing="0">
                        <ItemTemplate>
                        <table width="350" height="93" border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #d6d6d6;">
                            <tr>
                                <td width="101" rowspan="2" align="center" valign="middle">
                                    <a href='#' target='_blank'>
                                        <img src='upload-file/images/product/<%#GetImage(int.Parse(Eval("product_id").ToString())).Image %>' alt='<%#GetImage(int.Parse(Eval("product_id").ToString())).Alt %>'
                                            width="68" height="68" style="padding: 8px;" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td width="247" style="padding: 10px 5px 0 3px; line-height: 220%;">
                                    <a style="line-height: 160%;" href='p_show.aspx?productId=<%#Eval("product_id") %>' target='_blank'><%#new com.eshop.www.BLL.ProductDetailBusiness().GetEntity(int.Parse(Eval("product_id").ToString())).ProductName %></a>
                                    <br />
                                    <span style='color: #999999;'><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-MM-dd") %>购买</span> <a href='p_show.aspx?productId=<%#Eval("product_id") %>&tab=1' target='blank_'>查看全部<span
                                        style="color: #a10000;">(<%#new com.eshop.www.BLL.ProductCommentBusiness().GetSumCommentByProduct(int.Parse(Eval("product_id").ToString())) %>)</span>条评论<a><br />
                                            <a href='publish_comment.aspx?productId=<%#Eval("product_id") %>&itemId=<%#Eval("Id") %>'>
                                                <img height='21' width='89' style='margin-top: 10px; margin-bottom: 10px;' src='images/wypl.jpg' /></a>
                                </td>
                            </tr>
                        </table>
                        </ItemTemplate>
                        </asp:DataList>
                        <table border='0' cellpadding='0' style='border-collapse: collapse' width='100%'
                            height='20'>
                            <tr>
                                <td align='right'>
                                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" NextPageText="上一页" 
                                         PrevPageText="下一页"  PageSize="10" AlwaysShow="true" 
                                        OnPageChanged="AspNetPager2_PageChanged" FirstPageText="First" 
                                         LastPageText="Last"  ShowFirstLast="False" ShowPageIndexBox="Never"></webdiyer:AspNetPager>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <p style="font-size: 0;">
                        <img src="images/cencenBottom.gif" /></p>
                </div>
            </div>
        </div>
        <!-- /end defaultMainRight -->
    </div>
    <!-- /end defaultMain -->
    <uc3:help ID="help1" runat="server" />
    <uc4:foot ID="foot1" runat="server" />
    </form>
</body>
</html>
