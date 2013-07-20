<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_info.aspx.cs" Inherits="order_info"  EnableViewStateMac="false" %>
<%@ Register src="help.ascx" tagname="help" tagprefix="uc1" %>
<%@ Register src="head.ascx" tagname="head" tagprefix="uc2" %>
<%@ Register src="foot.ascx" tagname="foot" tagprefix="uc3" %>
<%@ Register src="member_left.ascx" tagname="member_left" tagprefix="uc4" %>
<%@ Register src="order_info.ascx" tagname="order_info" tagprefix="uc5" %>
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
<div class="banner"><img src="images/ad/adfile001.jpg" alt="" /></div>
<!-- /end banner -->
<div class="defaultMain">
   <div class="defaultMainLeft">
    <uc4:member_left ID="member_left1" runat="server" />
  </div>
  <!-- /end defaultMainLeft -->
  <div class="defaultMainRight">
      <uc5:order_info ID="order_info1" runat="server" />
  </div>
  <!-- /end defaultMainRight -->
</div>
<!-- /end defaultMain -->
<uc1:help ID="help1" runat="server" /> 
<!-- /end help -->
<uc3:foot ID="foot1" runat="server" />
</form>
</body>
</html>
