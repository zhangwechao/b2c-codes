<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
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
<link href="style/login.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="js/jquery.js"></script>
<script language="javascript" src="js/hdcd.js"></script>
<script language="javascript" src="js/indexhome.js"></script>
<script src="js/validata.field.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function(){
    $('#validataImage').bind('click',function(){
        $(this).attr('src','validata_code.aspx?rnd='+Math.random());
    });
    var userName = getCookie('tmjcshop_userName');
    if(userName!=null){
        $('#userName').val(userName);
    }
    $('#loginForm').bind('submit',function(){
        var username = $.trim($('#userName').val());
        var password = $.trim($('#password').val());
        var validataCode = $.trim($('#validataText').val());
        var isRemember = $('#remember').attr('checked');
        var msg = validataField.validataUserName(username);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        msg = validataField.validataPassword(password);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        msg = validataField.validataCode(validataCode);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        var remember=isRemember?1:0;
        $.ajax({
            url:'login.aspx',
            type:'post',
            data:'userName='+username+'&password='+password+'&validataCode='+validataCode+'&remember='+remember+'&type=login',
            success:function(msg){
                if(msg=='errorCode'){
                    alert('验证码错误');
                }
                if(msg=='codeExpire'){
                    alert('验证码过期');
                }
                if(msg=='errorpass'){
                    alert('错误用户名和密码，请确认Caps Lock是否打开。');
                }
                if(msg=='ok'){
                    var page = getCookie('tmjcshop_page');
                    delCookie('tmjcshop_page');
                    if(page==null)
                        window.location.href='member_center.aspx';
                    else
                        window.location.href=page;    
                }
            },
            error:function(msg){
                alert(msg.responseText);
            }
        }); 
        return false;
    });
    $('#btnReg').bind('click',function(){
        var username = $.trim($('#regUserName').val());
        var password = $.trim($('#regPassword').val());
        var email = $.trim($('#email').val());
        var confirmPass = $.trim($('#regConfirmPass').val());
        var mobile = $.trim($('#mobile').val());
        var isRemember = $('#regRemember').attr('checked');
        var msg = validataField.validataUserName(username);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        msg = validataField.validataPassword(password);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        msg = validataField.validataConfirmPassword(password,confirmPass);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        msg = validataField.validataMobile(mobile,true);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        msg = validataField.validataEmail(email);
        if(msg!=validataField.OK){
            alert(msg);
            return false;
        }
        var remember=isRemember?1:0;
        $.ajax({
            url:'login.aspx',
            type:'post',
            data:'userName='+username+'&password='+password+'&mobile='+mobile+'&email='+email+'&remember='+remember+'&type=reg',
            success:function(msg){
                if(msg=="ok"){
                    alert('恭喜注册成功，点击确定完成。');
                    var page = getCookie('tmjcshop_page');
                    delCookie('tmjcshop_page');
                    if(page==null)
                        window.location.href='member_center.aspx';
                    else
                        window.location.href=page;
                }
                if(msg=="sameName"){
                    alert("该用户名已经存在，请换另外一个用户名");
                }
                if(msg=="sameEmail")
                    alert("该电子邮件已经存在，请输入另外一个电子邮件地址");
            },
            error:function(msg){
                alert(msg.responseText);
            }
        });
        return false;
    });
});
</script>
</head>
<body>
<uc2:head ID="head1" runat="server" />
<!-- /end header -->
<div class="banner"><img src="images/ad/adfile001.jpg" alt="" /></div>
<!-- /end banner -->
<div class="defaultMain">
  <div class="defaultMainLogo">
   <dl>
    <dt>用户登录</dt>
    <dd>
      <form id="loginForm" action="">
      <ul>
       <li>用户名：<input id="userName" type="text"/>*请填写您的用户名</li>
       <li>密&nbsp;&nbsp;码：<input id="password" type="password" />*请填写您的密码</li>
       <li class="cdkey">验证码：<input type="text" id="validataText" /><img alt="看不清，点击更改验证码" src="validata_code.aspx" id="validataImage" />*验证码不能为空</li>
       <li class="cdkey1">记住用户名：<input id="remember" type="checkbox" value="" /></li>
       <li>
       <input type="image" src="images/icon-042.gif" id="login" style=" cursor:pointer; width:93px; height:38px; border:0" />
       </li>
      </ul>
      </form>
    </dd>
   </dl>
      <dl>
    <dt>用户注册</dt>
    <dd>
    <form id="regForm" action="">
      <ul>
       <li>用&nbsp;户&nbsp;名：<input id="regUserName" />*请填写您的用户名</li>
       <li>密&nbsp;&nbsp;&nbsp;&nbsp;码：<input id="regPassword" type="password"/>*请填写您的密码</li>
       <li>确认密码：<input id="regConfirmPass" type="password" />*请确认您的密码</li>
       <li>手机号码：<input id="mobile" type="text" maxlength="15" />*请填写您的手机号码</li>
       <li>电子邮件：<input id="email" type="text" />*请填写您的电子邮件</li>
       <li class="cdkey1">记住用户名：<input name="" id="regRemember" type="checkbox" value="" /></li>
       <li>
       <input type="image" src="images/icon-043.gif" id="btnReg" style="cursor:pointer;width:93px; height:38px; border:0" /></li>
      </ul>
      </form>
    </dd>
   </dl>
  </div>
  <!-- /end defaultMainLeft -->
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
