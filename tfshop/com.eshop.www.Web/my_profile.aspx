<%@ Page Language="C#" AutoEventWireup="true" CodeFile="my_profile.aspx.cs" Inherits="my_profile" %>

<%@ Register Src="head.ascx" TagName="head" TagPrefix="uc1" %>
<%@ Register Src="member_left.ascx" TagName="member_left" TagPrefix="uc2" %>
<%@ Register Src="help.ascx" TagName="help" TagPrefix="uc3" %>
<%@ Register Src="foot.ascx" TagName="foot" TagPrefix="uc4" %>
<%@ Register src="member_level.ascx" tagname="member_level" tagprefix="uc5" %>
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
            <div class="htArea_right">
                
                <!--普通会员-->
                <uc5:member_level ID="member_level1" runat="server" />
                <div class="Height4_Area">
                </div>
                <table class="infobox">
                    <caption>
                        个人信息<a href="modify_info.aspx" class="a2">更改</a>
                    </caption>
                    <tbody>
                        <tr>
                            <th class="th1">
                                会员名
                            </th>
                            <td>
                                <asp:Literal ID="luserName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th class="th1">
                                手机号码
                            </th>
                            <td>
                                <asp:Literal ID="lMobile" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                电子邮件
                            </th>
                            <td>
                                <asp:Literal ID="lEmail" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                积分
                            </th>
                            <td>
                                 <asp:Literal ID="lIntegral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                会员等级
                            </th>
                            <td>
                                <asp:Literal ID="lLevelName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                状态
                            </th>
                            <td>
                                <asp:Literal ID="lState" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                享受折扣
                            </th>
                            <td>
                                <asp:Literal ID="lDiscount" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        
                    </tbody>
                </table>
            </div>
        </div>
        <!-- /end defaultMainRight -->
    </div>
    </div>
    <!-- /end defaultMain -->
    <uc3:help ID="help1" runat="server" />
    <uc4:foot ID="foot1" runat="server" />
    </form>
</body>
</html>
