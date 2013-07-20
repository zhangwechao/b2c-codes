<%@ Page Language="C#" AutoEventWireup="true" CodeFile="news_list.aspx.cs" Inherits="news_list" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
<script type="text/javascript">
     $(document).ready(function() {
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        };
        var categoryId = getQueryString("categoryId");
        if(categoryId=="7")
            $($("ul.navigation a")[9]).addClass('hover');
    });
</script>
</head>
<body>
<form runat="server" id="form1">
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
         <ul>
         <asp:Repeater ID="rNewsList" runat="server">
         <ItemTemplate>
         <li><a href='news.aspx?Id=<%#Eval("Id") %>' target="_blank"><span><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-M-d HH:mm:ss") %></span><%#Eval("title") %></a><//li>
         </ItemTemplate>
         </asp:Repeater>
         </ul>
          <ul>
          <li>
          <webdiyer:AspNetPager ID="AspNetPager2" runat="server" 
                        FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                        NextPageText="下一页" PagingButtonLayoutType="Span" PrevPageText="上一页" ShowBoxThreshold="10"
                        PageSize="20" AlwaysShow="false" 
                        OnPageChanged="AspNetPager2_PageChanged"></webdiyer:AspNetPager>
          </li>
          </ul>     
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
</form>
</body>
</html>
