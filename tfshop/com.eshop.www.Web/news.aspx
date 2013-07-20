<%@ Page Language="C#" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="news" %>
<%@ Register src="help.ascx" tagname="help" tagprefix="uc1" %>
<%@ Register src="head.ascx" tagname="head" tagprefix="uc2" %>
<%@ Register src="foot.ascx" tagname="foot" tagprefix="uc3" %>
<%@ Register src="category.ascx" tagname="category" tagprefix="uc4" %>
<%@ Register src="product_click.ascx" tagname="product_click" tagprefix="uc5" %>
<%@ Register src="product_by_category.ascx" tagname="product_by_category" tagprefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>ESHOP 网商宝商城</title>
<link href="style/common.css" rel="stylesheet" type="text/css" />
<link href="style/news.css" rel="stylesheet" type="text/css" />
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
    <h2 class="dirtitle">全部商品分类</h2>
    <uc4:category ID="category1" runat="server" />
    <!-- /end 商品分类 -->
     <uc5:product_click ID="product_click1" runat="server" />
    <!-- /end theleftList01 -->
    <div class="banner"><img src="images/ad/adfile005.jpg" alt="" /></div>
    <!-- /end banner -->
    <uc6:product_by_category  ID="product_by_category1" runat="server" CategoryId="544" />
    <!-- /end theleftList02 -->
    <div class="banner"><img src="images/ad/adfile006.jpg" alt="" /></div>
    <!-- /end banner -->
    <div class="banner"><img src="images/ad/adfile007.jpg" alt="" /></div>
    <!-- /end banner -->
    <uc6:product_by_category  ID="product_by_category2" runat="server" CategoryId="543" />
    <!-- /end theleftList03 -->
    <div class="banner"><img src="images/ad/adfile008.jpg" alt="" /></div>
    <!-- /end banner -->
    <div class="banner"><img src="images/ad/adfile009.jpg" alt="" /></div>
    <!-- /end banner -->
  </div>
  <!-- /end defaultMainLeft -->
  <div class="defaultMainRight">
    <div class="the_one">
      <dl>
        <dt><asp:Literal ID="lCategoryName" runat="server"></asp:Literal></dt>
        <dd>
          <h1><asp:Literal runat="server" ID="ltitle"></asp:Literal></h1>
          <h2><asp:Literal runat="server" ID="lCreateDate"></asp:Literal></h2>
         <h3><asp:Literal runat="server" ID="lContent"></asp:Literal>
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
