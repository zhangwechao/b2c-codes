<%@ Page Language="C#" AutoEventWireup="true" CodeFile="complain.aspx.cs" Inherits="complain" EnableViewStateMac="false" %>
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
    var title = $.trim($('#txtTitle').val());
    if(title.length==0){
        alert('请输入标题');
        return false;
    }
     if(validataField.getStrLength(title)>50){
        alert('标题字符数超过限制，最多20个字符');
        return false;
    }
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
        <form method="post" onsubmit="return checksubmit();" action='complain.aspx'>
          <div class="main">
            <h3><span class="stylename"><img src="images/ping.gif" style="padding-right:2px; margin-bottom:-2px;" />意见建议&nbsp;<a href="complainlist.aspx">查看建议列表</a></span> </h3>
            
            <p style="border-bottom:1px dotted #CCCCCC; margin:15px;"> 　</p>
            <table width="100%" border="0" cellspacing="5" cellpadding="0" class="bd">
              <tr>
                <td width="65%" align="left" valign="middle" colspan="2"><strong>标题：</strong><br />
                    <input type="text" name="ATitle" maxlength="20" style="width:488px;padding:2px;" id="txtTitle" />
                  <span class="Yc" id="tword">标题最多输入20字以内</span></td>
              </tr>
              <tr>
                <td valign="top"><strong>正文：</strong><br />
                    <textarea  name="AText"  enableviewstate="true" rows="8" style="width:418px; height:230px;padding:2px;" id="txtContent"></textarea>
                    <br />
                  <span class="Bj" id="word">内容300字以内，一个中文两个字符</span> </td>
                <td rowspan="3" align="left" valign="top" style="padding:6px;">
                  &nbsp;</td>
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

