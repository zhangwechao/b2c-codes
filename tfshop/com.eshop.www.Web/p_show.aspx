<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_show.aspx.cs" Inherits="p_show" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="help.ascx" tagname="help" tagprefix="uc1" %>
<%@ Register src="head.ascx" tagname="head" tagprefix="uc2" %>
<%@ Register src="foot.ascx" tagname="foot" tagprefix="uc3" %>
<%@ Register src="category.ascx" tagname="category" tagprefix="uc4" %>
<%@ Register src="product_click.ascx" tagname="product_click" tagprefix="uc5" %>
<%@ Register src="product_by_category.ascx" tagname="product_by_category" tagprefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>ESHOP 网商宝商城</title>
<link href="style/jquery.jcarousel.w.css" rel="stylesheet" type="text/css" media="all" />
<link href="style/pshow.w.css" rel="stylesheet" type="text/css" media="all" />
<link rel="stylesheet" type="text/css" href="style/base20100719.w.css" media="all">
<link rel="stylesheet" type="text/css" href="style/plist.w.css" media="all">
<link href="style/common.css" rel="stylesheet" type="text/css" />    
<link href="style/p_show.css" rel="stylesheet" type="text/css" />
<link href="style/jquery.rater.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="js/jquery.js"></script>
<script language="javascript" src="js/hdcd.js"></script>
<script language="javascript" src="js/indexhome.js"></script>
<script src="js/jquery.rater.js" type="text/javascript"></script>
<script type="text/javascript"> 
function mycarousel_initCallback(carousel){
	$("#mycarousel li").mouseover(function(){
		$("#Product_BigImage img")[0].src=this.getElementsByTagName("img")[0].src;
		$("#Product_BigImage img")[0].jqimg=this.getElementsByTagName("img")[0].name;
		$(this).siblings().each(function(){
			this.getElementsByTagName("img")[0].className="";
		})
		this.getElementsByTagName("img")[0].className="curr";
	})
};
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
};
$(function(){
    jQuery("#mycarousel").jcarousel({initCallback:mycarousel_initCallback});
    jQuery("#Fi_pro").jcarousel();
   $(".jqzoom").jqueryzoom({
		xzoom:303,
		yzoom:260,
		offset:10,
		position:"right",
        preload:1,
		lens:1
	});
	var curvalue = $("#hiddenScore").val();
	$("#xing").rater(null,{maxvalue:5,curvalue:curvalue,enabled:false});
	$('li.li8 input[type=image]').bind('click',function(){
	    var reg = /^\d+$/;
	    var number = $.trim($('#txtNumber').val());
	    if(!reg.test(number)){
	        alert('在购买数量处请输入大于0的数字');
	        return false;
	    }else
	    {
	        if(parseInt(number)<=0){
	            alert('在购买数量处请输入大于0的数字');
	            return false;
	        }
	    }
	});
	var tabIndex = getQueryString('tab');
	if(tabIndex!=null){
	    SetTab('tab1',tabIndex,5);
	}
	
});
</script>
</head>
<body>
<form id="form1" runat="server">
<uc2:head ID="head1" runat="server" />
<div class="banner"><img src="images/ad/adfile001.jpg" alt="" /></div>
<!-- /end banner -->
<div class="defaultMain" id="defaultMain">
   <div class="defaultMainLeft" onmousemove='document.getElementById("defaultMain").style.position="relative";'>
    <h2 class="dirtitle">全部商品分类</h2>
    <uc4:category ID="category1" runat="server"></uc4:category>
    <!-- /end 商品分类 -->
    <uc5:product_click ID="product_click1" runat="server"></uc5:product_click>
    <!-- /end theleftList01 -->
    <div class="banner"><img src="images/ad/adfile005.jpg" alt="" /></div>
    <!-- /end banner -->
    <uc6:product_by_category ID="product_by_category1" runat="server" CategoryId="544"></uc6:product_by_category>
    <!-- /end theleftList02 -->
    <div class="banner"><img src="images/ad/adfile006.jpg" alt="" /></div>
    <!-- /end banner -->
    <div class="banner"><img src="images/ad/adfile007.jpg" alt="" /></div>
    <!-- /end banner -->
    <uc6:product_by_category ID="product_by_category2" runat="server" CategoryId="543"></uc6:product_by_category>
    <!-- /end theleftList03 -->
    <div class="banner"><img src="images/ad/adfile008.jpg" alt="" /></div>
    <!-- /end banner -->
    <div class="banner"><img src="images/ad/adfile009.jpg" alt="" /></div>
    <!-- /end banner -->
  </div>
  <!-- /end defaultMainLeft -->
  <div class="defaultMainRight">
    <div class="the_one">
       <h1><b></b><asp:Literal ID="lProductName" runat="server"></asp:Literal></h1>
      <div class="left_pic">
        <div class="theMainRight">
           <div id="Product_Intro_Left" >
             <div id="Product_BigImage" class="jqzoom"><img  onerror="upload-file/images/product/none_50.gif" runat="server" id="defautlImage"  alt="" src="" width="310" height="310" jqimg=""/></div>
    <div id="Product_LittleImage">
     <ul id="mycarousel" class="jcarousel-skin-tango">
     <asp:Repeater ID="rProductImageList" runat="server">
     <ItemTemplate>
           <li><img  onerror="this.src='upload-file/images/product/none_50.gif'"  alt='<%#Eval("Alt") %>' src='upload-file/images/product/<%#Eval("image") %>' name='upload-file/images/product/<%#Eval("zoom_image") %>'/></li>
     </ItemTemplate>
     </asp:Repeater>
     </ul>
    </div>
   </div>
              <div class="Description" id="Product_Intro_Right">   
     <li id="cx" style="display: none"></li>
     <li id="liLargess" class='largess' style="display:none"></li>
     <li id="nocoupon" style="display:none"></li>
     <li class="ons" id="oldtonew" style="display:none"></li>
    <span id='pShowSkuId' style='display:none'></span>
    <div id="shipBarPanel" style="display:none;"></div>
    </div>
          </div>  
      </div>
      <div class="right_word">
       <ul>
        <li class="li1"><p>价&nbsp;&nbsp;&nbsp;&nbsp;格</p>
          ：￥<asp:Literal ID="lPrice"  runat="server"></asp:Literal>
        </li>
        <li class="li2"><p>促&nbsp;&nbsp;&nbsp;&nbsp;销</p>：<b>￥</b><b><asp:Literal ID="lSalePrice"  runat="server"></asp:Literal>
        </b></li>
        <li class="li3"><p>运&nbsp;&nbsp;&nbsp;&nbsp;费</p>：卖家承担运费</li>
        <li class="li4"><p>已&nbsp;出&nbsp;售</p>：<asp:Literal ID="lSaleNumber"  runat="server"></asp:Literal>件</li>
        <li class="li9"><p>评&nbsp;&nbsp;&nbsp;&nbsp;价</p><div style="float:left">：</div><div id="xing" style="float:left"></div>
            <asp:Literal ID="lScore" runat="server"></asp:Literal>分
            <asp:HiddenField ID="hiddenScore" runat="server" />
            （<asp:Literal ID="lCommentNumber"  runat="server"></asp:Literal>人评价）</li>
        <li class="li5"><p>送&nbsp;积&nbsp;分</p>：单件送<asp:Literal ID="lIntegral"  runat="server"></asp:Literal>积分</li>
        <li class="li6"><p>付款方式</p>：<img src="images/icon-032.gif" alt="" />支持用信用卡付款</li>
        <li class="li7"><p>数&nbsp;&nbsp;&nbsp;&nbsp;量</p>：<span><asp:TextBox ID="txtNumber" runat="server" Text="1"></asp:TextBox></span>件<b>（库存<asp:Literal ID="lTotalNumber"  runat="server"></asp:Literal>件）</b>
        &nbsp;<asp:LinkButton ID="lBtnAddFavorite" runat="server" ForeColor="Navy" 
                onclick="lBtnAddFavorite_Click">加入收藏</asp:LinkButton>
        </li>
        <li class="li8">
        <asp:ImageButton ID="imgBtnNowBuy" runat="server" ImageUrl="images/icon-033.gif" 
                onclick="imgBtnNowBuy_Click"  />&nbsp;&nbsp;
        <asp:ImageButton ID="imgBtnAddShppingCart" runat="server" 
                ImageUrl="images/icon-034.gif" onclick="imgBtnAddShppingCart_Click"  /></li>
       </ul>
      </div>
    </div>
    <div class="the_two">
        <div class="tmenu">
            <ul id="tab1">
                <li class="on" onclick="SetTab('tab1',0,5);">商品介绍</li>
                <li onclick="SetTab('tab1',1,5);">评价详情</li>
                <li onclick="SetTab('tab1',2,5);">交易记录</li>
            </ul>
        </div>
        <%--<div class="caption">
          <dl>
            <dt>商城承诺：您付款后如果商家商品缺货，可以获得商品价格5%的（不大于30元）的赔偿金。</dt>
            <dd><img src="images/a/icon-009.gif" alt="" />单笔订单满200减80元！不上封顶<br />请大家用购物车购买，即可享受立减。不使用购物车购买无法累计金额。</dd>
          </dl>
        </div>--%>
          <div class="tbox">
            <div id="tab1-content0" class="block">
                  <div class="Properties">
          <dl>
            <dt><img src="images/icon-039.gif" alt="" /></dt>
            <dd>
            <asp:Repeater ID="rProductAttributeList" runat="server">
            <ItemTemplate>
            <li><%#new com.eshop.www.BLL.ProductAttributeBusiness().GetEntity(int.Parse(Eval("attribute_id").ToString())).Attribute %>：<%#Eval("attribute_value")%></li>
            </ItemTemplate>
            </asp:Repeater>
            </dd>
            
           </dl>
        </div>
             <br /><asp:Literal  ID="lDesc" runat="server"></asp:Literal>
            </div>
            <div id="tab1-content1" class="none">
                <table width="100%">
                <asp:Repeater ID="rProductCommentList" runat="server">
                <HeaderTemplate>
                    <tr>
                        <th width="55%" align="left">
                            &nbsp;评论
                        </th>
                        <th width="15%">
                           分数
                        </th>
                        <th width="15%" align="center">
                            评论人
                        </th>
                        <th width="15%" align="center">时间</th>
                    </tr>
                </HeaderTemplate>
             <ItemTemplate>
                    <tr>
                        <td>&nbsp;<%#Eval("content") %></td>
                        <td align="center"><%#Eval("score") %>分</td>
                        <td align="center"><%#Eval("user_name") %></td>
                        <td align="center"><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-M-d HH:mm:ss") %></td>
                    </tr>
                    
             </ItemTemplate>
             </asp:Repeater>
             <tr>
                <td colspan="4">
                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条，共%RecordCount%条记录"
                        CustomInfoTextAlign="Left" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                        NextPageText="下一页" PagingButtonLayoutType="Span" PrevPageText="上一页" ShowBoxThreshold="10"
                        ShowCustomInfoSection="Left" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="转到" PageSize="20" AlwaysShow="true" ShowPageIndexBox="Always"
                        OnPageChanged="AspNetPager2_PageChanged"></webdiyer:AspNetPager>
                </td>
             </tr>
                </table>
                
            </div>
            <div id="tab1-content2" class="none">
              <table width="100%">
                <asp:Repeater ID="rProductTranList" runat="server">
                    <HeaderTemplate>
                        <tr>
                            <th width="15%" align="left">产品名称</th>
                            <th width="15%" align="center">交易数量</th>
                            <th width="15%" align="center">金额</th>
                            <th align="center">交易情况</th>
                            <th width="15%" align="center">买家</th>
                            <th width="15%" align="center">时间</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td height="15%"><%#new com.eshop.www.BLL.ProductDetailBusiness().GetEntity(int.Parse(Eval("product_id").ToString())).ProductName %></td>
                            <td height="15%" align="center"><%#Eval("quantity") %></td>
                            <td height="15%" align="center"><%#float.Parse(Eval("total_money").ToString()).ToString("0") %></td>
                            <td height="15%" align="center">成功</td>
                            <td height="15%" align="center"><%#new com.eshop.www.BLL.MemberBusiness().GetEntity(int.Parse(Eval("member_id").ToString())).UserName %></td>
                            <td height="15%" align="center"><%#DateTime.Parse(Eval("create_date").ToString()).ToString("yyyy-M-d HH:mm:ss") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="6">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条，共%RecordCount%条记录"
                        CustomInfoTextAlign="Left" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                        NextPageText="下一页" PagingButtonLayoutType="Span" PrevPageText="上一页" ShowBoxThreshold="10"
                        ShowCustomInfoSection="Left" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="转到" PageSize="20" AlwaysShow="true" ShowPageIndexBox="Always"
                        OnPageChanged="AspNetPager2_PageChanged"></webdiyer:AspNetPager>
                    </td>
                </tr>
              </table>
            </div>
      </div>
        <div class="other_prodtct">
          <dl>
            <dt>浏览了该商品的用户还浏览了</dt>
            <dd><ul>
            <asp:Repeater ID="rRelateBrowse" runat="server">
            <ItemTemplate>
            <li><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank"><img src='upload-file/images/product/<%#GetImage(int.Parse(Eval("Id").ToString())).Image %>' alt='<%#GetImage(int.Parse(Eval("Id").ToString())).Alt %>' width="162px" height="162px" /></a><p><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank"><%#Eval("product_name") %></a></p><%#float.Parse(Eval("sale_price").ToString()).ToString("0") %>元<span><%#float.Parse(Eval("price").ToString()).ToString("0") %>元</span></li>
            </ItemTemplate>
            </asp:Repeater>
            </ul>
            </dd>
          </dl>
          <dl>
            <dt>相关产品</dt>
            <dd><ul>
            <asp:Repeater ID="rRelateProduct" runat="server">
            <ItemTemplate>
            <li><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank"><img src='upload-file/images/product/<%#GetImage(int.Parse(Eval("Id").ToString())).Image %>' alt='<%#GetImage(int.Parse(Eval("Id").ToString())).Alt %>' width="162px" height="162px" /></a><p><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank"><%#Eval("product_name") %></a></p><%#float.Parse(Eval("sale_price").ToString()).ToString("0") %>元<span><%#float.Parse(Eval("price").ToString()).ToString("0") %>元</span></li>
            </ItemTemplate>
            </asp:Repeater>
            </ul>
            </dd>
          </dl>
        </div>
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
<script type="text/javascript" src="js/jquery-plugins-vs1-20090707.js"></script>
<script type="text/javascript" src="js/p.pshow.v0122.js"></script>
</form>
</body>
</html>
