<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modify_pass.aspx.cs" Inherits="modify_pass" %>
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

    <script src="js/validata.field.js" type="text/javascript"></script>
    <script type="text/javascript">
    $(function(){
        $('#<%=btnSave.ClientID %>').bind('click',function(){
            var pass = $.trim($('#<%=txtPass.ClientID %>').val());
            var newPass = $.trim($('#<%=txtNewPass.ClientID %>').val());
            var confirmPass = $.trim($('#<%=txtConfirmPass.ClientID %>').val());
            if(pass.length==0){
                alert('请输入原密码');
                return false;
            }
            var msg = validataField.validataPassword(newPass);
            if(msg!=validataField.OK){
                alert(msg);
                return false;
            }
            msg = validataField.validataConfirmPassword(newPass,confirmPass);
            if(msg!=validataField.OK){
                alert(msg);
                return false;
            }
        });
    });
    </script>

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
                        修改个人信息
                    </caption>
                    <tbody>
                        <tr>
                            <th class="th1">
                                原密码
                            </th>
                            <td>
                                <asp:TextBox  ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                新密码
                            </th>
                            <td>
                                <asp:TextBox  ID="txtNewPass" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                确认新密码
                            </th>
                            <td>
                                <asp:TextBox  ID="txtConfirmPass" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                &nbsp;
                            </th>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="修改" onclick="btnSave_Click" />
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
