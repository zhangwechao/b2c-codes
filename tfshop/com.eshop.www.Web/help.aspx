<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="help" %>
<%@ Register src="help.ascx" tagname="help" tagprefix="uc1" %>
<%@ Register src="head.ascx" tagname="head" tagprefix="uc2" %>
<%@ Register src="foot.ascx" tagname="foot" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>ESHOP 网商宝商城</title>
<link href="style/common.css" rel="stylesheet" type="text/css" />
<link href="style/help.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="js/jquery.js"></script>
<script language="javascript" src="js/hdcd.js"></script>
<script language="javascript" src="js/indexhome.js"></script>
</head>
<body>
<uc2:head ID="head1" runat="server" />
<div class="banner"><img src="images/ad/adfile001.jpg" alt="" /></div>
<!-- /end banner -->
<div class="defaultMain">
   <div class="defaultMainLeft">
   <dl>
  <dt>关于ESHOP</dt>
  <dd>
  <asp:Repeater runat="server" ID="rAboutUs">
  <ItemTemplate>
  <a href='help.aspx?Id=<%#Eval("Id") %>'  target="_blank"><%#Eval("title") %></a>
  </ItemTemplate>
  </asp:Repeater>
 </dl>
 <!-- /end file-01 -->
 <dl>
  <dt>新手上路</dt>
  <dd>
  <asp:Repeater runat="server" ID="rNovice">
  <ItemTemplate>
  <a href='help.aspx?Id=<%#Eval("Id") %>'  target="_blank"><%#Eval("title") %></a>
  </ItemTemplate>
  </asp:Repeater>
  </dd>
 </dl>
 <!-- /end file-02 -->
 <dl>
  <dt>购物指南</dt>
  <dd>
  <asp:Repeater runat="server" ID="rGuide">
  <ItemTemplate>
  <a href='help.aspx?Id=<%#Eval("Id") %>'  target="_blank"><%#Eval("title") %></a>
  </ItemTemplate>
  </asp:Repeater>
  </dd>
 </dl>
 <!-- /end file-03 -->
 <dl>
  <dt>支付/配送方式</dt>
  <dd>
  <asp:Repeater runat="server" ID="rPayment">
  <ItemTemplate>
  <a href='help.aspx?Id=<%#Eval("Id") %>'  target="_blank"><%#Eval("title") %></a>
  </ItemTemplate>
  </asp:Repeater>
  </dd>

 </dl>
 <!-- /end file-04 -->
 <dl>
  <dt>购物条款</dt>
  <dd>
  <asp:Repeater runat="server" ID="rProvisions">
  <ItemTemplate>
  <a href='help.aspx?Id=<%#Eval("Id") %>'  target="_blank"><%#Eval("title") %></a>
  </ItemTemplate>
  </asp:Repeater>
  </dd>
 </dl>
  </div>
  <!-- /end defaultMainLeft -->
  <div class="defaultMainRight">
    <div class="the_one">
      <dl>
        <dt>帮助中心</dt>
        <dd>
<h3><asp:Literal ID="lContent" runat="server"></asp:Literal>
</h3>
        </dd>
      </dl>
    </div>
  </div>
  <!-- /end defaultMainRight -->
</div>
<!-- /end defaultMain -->
<div class="banner">
<img src="images/ad/adfile004.jpg" alt="" />
</div>
<!-- /end banner -->
<uc1:help ID="help1" runat="server" />    
<!-- /end help -->
<uc3:foot ID="foot1" runat="server" />
</body>
</html>
