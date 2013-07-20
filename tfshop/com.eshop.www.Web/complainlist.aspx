<%@ Page Language="C#" AutoEventWireup="true" CodeFile="complainlist.aspx.cs" Inherits="complainlist" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="help.ascx" TagName="help" TagPrefix="uc1" %>
<%@ Register Src="head.ascx" TagName="head" TagPrefix="uc2" %>
<%@ Register Src="foot.ascx" TagName="foot" TagPrefix="uc3" %>
<%@ Register Src="member_left.ascx" TagName="member_left" TagPrefix="uc4" %>
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

</head>
<body>
<form id="form1" runat="server">
    <uc2:head ID="head1" runat="server" />
    <!-- /end header -->
    <div class="banner">
        <img src="images/ad/adfile001.jpg" alt="" /></div>
    <!-- /end banner -->
    <div class="defaultMain">
        <div class="defaultMainLeft">
            <uc4:member_left ID="member_left1" runat="server" />
        </div>
        <!-- /end defaultMainLeft -->
        <div class="defaultMainRight">
            <div id="right">
                <div class="dh" style="height: 30px;">
                    <div class="span">
                        <p>
                            <a href="#">我的建议</a></p>
                    </div>
                </div>
                <div class="styCCC">
                    <p style="font-size: 0;">
                        <img src="images/etbj.gif" /></p>
                    <div class="impCont">
                        <p class="plWZ">
                            <strong><span style="font-size: 14px; padding-right: 5px;">我发表的建议</span></strong>
                        </p>
                        <asp:Repeater ID="rMessageList" runat="server" 
                            onitemdatabound="rMessageList_ItemDataBound">
                        <ItemTemplate>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background: url(/images/Usercenter/bg_img/bkdx.gif) left bottom repeat-x;">
                            <tr>
                                <td width="49%" height="77" style="line-height: 180%; padding: 15px 0 15px 12px;">
                                    <p style="font-size:14px; margin:0 7px 7px 0; float:left;"><%#Eval("title") %></p><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-MM-dd HH:mm") %><br />
                                    <p style="clear: left;">
                                        <%#Eval("content") %><br />
                                    </p>
                                </td>
                                <td width="1%">
                                    <p style="margin: 0; padding: 0; height: 105px; border-left: 1px solid #e1dfe0;">
                                        <img src="images/space.gif" width="1" /></p>
                                </td>
                                <td width="50%" align="left" style="padding-left: 25px; line-height: 22px;">
                                    <p style="font-size:14px; margin:0 7px 7px 0; float:left;">
                                        <asp:Literal ID="lReplyUser" runat="server"></asp:Literal></p><asp:Literal ID="lReplyDate" runat="server"></asp:Literal><br />
                                    <p style="clear: left;">
                                        <asp:Literal ID="lReplyContent" runat="server"></asp:Literal><br />
                                    </p>
                                </td>
                            </tr>
                        </table>
                        </ItemTemplate>
                        </asp:Repeater>
                        <table border='0' cellpadding='0' style='border-collapse: collapse' width='100%' height='20'>
                            <tr>
                                <td align='right'>
                                   <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                                        NextPageText="下一页" PrevPageText="上一页"
                                        PageSize="10" AlwaysShow="true" 
                                        OnPageChanged="AspNetPager2_PageChanged"></webdiyer:AspNetPager>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <p style="font-size: 0;">
                        <img src="images/cencenBottom.gif" /></p>
                </div>
            </div>
            <!-- /end defaultMainRight -->
        </div>
        <!-- /end defaultMain -->
        <uc1:help ID="help1" runat="server" />
        <uc3:foot ID="foot1" runat="server" />
</body>
</form>
</html>
