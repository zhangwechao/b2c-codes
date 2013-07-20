<%@ Page Language="C#" AutoEventWireup="true" CodeFile="back-login.aspx.cs" Inherits="back_stage_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>后台登录</title>
<link href="loginCss/login.css" rel="stylesheet" type="text/css" />
</head>

<body>
<div class="header"></div>
<div class="Area">
 <div class="Area_left"></div>
 <div class="Area_right">
   <form id="form1" runat="server" defaultfocus="txtUserName">
  <dl class="login">
   <dt>
    <p><asp:TextBox Id="txtUserName" runat="server"></asp:TextBox></p>
    <p><asp:TextBox Id="txtPassword" runat="server" TextMode="Password"></asp:TextBox></p>
   </dt>
   <dd>
    <p>
     &nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="button" runat="server" Text="&nbsp;&nbsp;" 
            onclick="button_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
      <input name="button2" type="reset" id="button2" value="&nbsp;&nbsp;" />
    </p>
   </dd>
  </dl>
  </form>
 </div>
</div>
</body>
</html>

