<%@ Page Language="C#" AutoEventWireup="true" CodeFile="publish_comment.aspx.cs" Inherits="publish_comment" EnableViewStateMac="false" %>
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
<script src="js/validata.field.js" type="text/javascript"></script>
<script type="text/javascript">
function checksubmit(){
    var content = $.trim($('#txtContent').val());
    if(content.length==0){
        alert('请输入要评论的内容');
        return false;
    }
    if(validataField.getStrLength(content)>300){
        alert('字符数超过限制，最多300个字符，一个中文两个字符');
        return false;
    }
    
}
</script>
</head>
<body>
 <uc1:head ID="head1" runat="server" />
<!-- /end header -->
<div class="banner"><img src="images/ad/adfile001.jpg" alt="" /></div>
<!-- /end banner -->
<div class="defaultMain">
   <div class="defaultMainLeft">
    <uc2:member_left ID="member_left1" runat="server" />
  </div>
  <!-- /end defaultMainLeft -->
  <div class="defaultMainRight">
        <div id="main">
      <div id="orderform" class="mngbox">
        <form method="post" onsubmit="return checksubmit();" action='publish_comment.aspx?productId=<%=Request.QueryString["productId"] %>&itemId=<%=Request.QueryString["itemId"] %>'>
          <div class="main">
            <h3><span class="stylename"><img src="images/ping.gif" style="padding-right:2px; margin-bottom:-2px;" />产品评论：<asp:Literal ID="lProductName" runat="server"></asp:Literal></span> </h3>
            <table border="0" cellpadding="0" class="cc">
              <tr>
                <td>综合打分：</td>
                <td><input type="radio" name="AOverall" value="5"  checked="checked" />
                    <img src="images/s1.gif" /><img src="images/s1.gif" /><img src="images/s1.gif" /><img src="images/s1.gif" /><img src="images/s1.gif" /></td>
                <td class="apppraisetd">非常喜欢</td>
                <td><input type="radio" name="AOverall" value="4" />
                    <img src="images/s1.gif" /><img src="images/s1.gif" /><img src="images/s1.gif" /><img src="images/s1.gif" /></td>
                <td class="apppraisetd">喜欢</td>
                <td><input type="radio" name="AOverall" value="3" />
                    <img src="images/s1.gif" /><img src="images/s1.gif" /><img src="images/s1.gif" /></td>
                <td class="apppraisetd">还行</td>
                <td><input type="radio" name="AOverall" value="2" />
                    <img src="images/s1.gif" /><img src="images/s1.gif" /></td>
                <td class="apppraisetd">不喜欢</td>
                <td><input type="radio" name="AOverall" value="1" />
                    <img src="images/s1.gif" /></td>
                <td class="apppraisetd">很不喜欢</td>
              </tr>
            </table>
            
            <p style="border-bottom:1px dotted #CCCCCC; margin:15px;"> 　</p>
            <table width="100%" border="0" cellspacing="5" cellpadding="0" class="bd">
              <tr>
                <td valign="top"><strong>正文：</strong><br />
                    <textarea  name="AText"  enableviewstate="true" rows="8" style="width:418px; height:230px;padding:2px;" id="txtContent"></textarea>
                    <br />
                  <span class="Bj" id="word">内容300字以内，一个中文两个字符</span> </td>
                <td rowspan="3" align="left" valign="top" style="padding:6px;"><ul style="margin-top:20px; border:1px solid #e4e4e4; width:260px;">
                  <li style="background-color:#A20000; padding:4px 4px 4px 8px; color:#ffffff;">感谢您分享对产品的评价及感受！</li>
                  <li>
                    <ul style="padding:8px;">
                      在您发表评论之前，请注意以下几点：<br />
                      <li><img height="8" width="5" src="images/point.gif"/>评论应该是针对商品本身，而不是针对订单和送货等购物过程。</li>
                      <li><img height="8" width="5" src="images/point.gif"/>不仅评价商品的好或不好，更重要的是阐述自己的观点和理由，以帮助其他顾客判断商品是否适合自己。</li>
                      <li><img height="8" width="5" src="images/point.gif"/>我们鼓励您的原创评论，未经授权的文字请勿转载。</li>
                      <li><img height="8" width="5" src="images/point.gif"/>有关订单和送货等购物过程的问题，请查看 <span class="colorSky"><a href="help.aspx">帮助中心</a></span>，或者<span class="colorSky"><a href="help.aspx">联系客服</a></span>。</li>
                      <li><img height="8" width="5" src="images/point.gif"/>发表评论时请遵守《<span class="colorSky"><a href="help.aspx">ESHOP 网商宝商城预测社区条款</a></span>》</li>
                      <li><img height="8" width="5" src="images/point.gif"/>管理员有权删除违反上述要求的内容。</li>
                    </ul>
                  </li>
                </ul></td>
              </tr>
              <tr>
                <td align="center" style="padding:15px 0 5px;"><input name="image" type="image"  style="margin-right:25px;" src="images/zxlb_010.png" /></td>
              </tr>
            </table>
          </div>
          <input type="hidden" value="true" name="Appraisesubmit" />
        </form>
      </div>
    </div> 
    </div>
  <!-- /end defaultMainRight -->
</div>
<!-- /end defaultMain -->

 <uc3:help ID="help1" runat="server" />
 <uc4:foot ID="foot1" runat="server" />
</body>
</html>
