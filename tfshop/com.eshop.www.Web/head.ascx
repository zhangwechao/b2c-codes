<%@ Control Language="C#" AutoEventWireup="true" CodeFile="head.ascx.cs" Inherits="head" %>
<ul class="mininav">
    <div class="logo">
        <a href="/">
            <img src="images/icon-000.png" alt="乐购360商城" /></a></div>
    <li class="of-ot-oznt-01">欢迎来到乐购360商城<a id="login" href="login.aspx" class="LineR">请登录</a>
        <a id="registe" href="login.aspx">免费注册</a><a id='logout' style="display: none; cursor: pointer">退出</a>
        <a href="javascript:;" onclick="javascript:window.external.AddFavorite('http://www.baidu.com','aaaa')"
            class="LineR">收藏本站</a><a href="shoppingCart.aspx" class="LineR">我的购物车</a><a href="help.aspx" style=" padding-left:5px;">帮助中心</a>
        <br />
        <img src="images/icon-003.png" alt="400-800-880" />
    </li>
    <li class="clear"></li>
</ul>
<div class="clear">
</div>
<!-- /end mininav -->
<div class="header">
    <!-- /end logo
    <ul class="SearchForm">
        <li class="pf-th-otz">关键字：</li>
        <li class="textarea1">
            <input name="txtKeyword" id="txtKeyword" class="textarea" type="text" /></li>
        <li class="pf-th-otz">价格从</li>
        <li class="textarea2">
            <input name="txtMaxPrice" id="txtMaxPrice" class="textarea" type="text" /></li>
        <li class="pf-th-otz">到</li>
        <li class="textarea2">
            <input name="txtMinPrice" id="txtMinPrice" class="textarea" type="text" /></li>
        <li class="textarea3">
            <input name="btnSearch" id="btnSearch" class="butarea" type="button" value="搜索" /></li>
        <li class="pf-th-otz"><a href="#"></a></li>
    </ul>
/end SearchForm -->
    <ul class="navigation" style="display: none;">
        <li><a href="default.aspx"><span>首页</span></a></li>
        <li><a href="p_list.aspx?isNew=1"><span>最新商品</span></a></li>
        <li><a href="p_list.aspx?categoryId=544"><span>瓷砖·陶瓷</span></a></li>
        <li><a href="p_list.aspx?categoryId=543"><span>地板·天花</span></a></li>
        <li><a href="p_list.aspx?categoryId=542"><span>卫浴·洁具</span></a></li>
        <li><a href="p_list.aspx?categoryId=541"><span>橱柜·衣柜</span></a></li>
        <li><a href="p_list.aspx?categoryId=540"><span>油漆·涂料</span></a></li>
        <li><a href="p_list.aspx?categoryId=539"><span>门窗·灯饰</span></a></li>
        <li><a href="p_list.aspx?categoryId=537"><span>五金·电器</span></a></li>
    </ul>
    <!-- /end navigation -->
    <ul class="navgate">
        <li><a href="default.aspx"><span>首页</span></a></li>
        <li><a href="p_list.aspx?isNew=1"><span>优购</span></a></li>
        <li><a href="p_list.aspx?categoryId=544"><span>特购</span></a></li>
        <li><a href="p_list.aspx?categoryId=543"><span>团购</span></a></li>
        <li><a href="p_list.aspx?categoryId=542"><span>商务</span></a></li>
        <li><a href="p_list.aspx?categoryId=541"><span>特别推荐</span></a></li>
        <li><a href="p_list.aspx?categoryId=540"><span>帮助</span></a></li>
    </ul>
    <!-- /end navigationde -->
</div>
<!-- /end header -->
<div class="defaultDD">
    <div class="dd_left">
        <span>所有商品分类</span>
    </div>
    <div class="dd_right">
        <div class="dd_r_l">
            <span style="overflow: hidden;">
                <img src="images/sousuo.jpg" style="padding: 5px;" /></span> <span class="text">
                    <input id="seachtext" value="" name="seachtext" type="text" />
                </span><span class="btn">
                    <input id="seachbtn" name="seachbtn" value="搜索" type="button" />
                </span>
        </div>
        <div class="dd_r_r">
            <span>
                <img src="images/carimg.jpg" style="padding: 5px 3px;" />
            </span><span style="color: #666; padding-right: 10px;">购物车：0件商品 </span><span class="btns">
                <input id="goto" name="seachbtn" value="去结算>>" type="button" />
            </span>
        </div>
    </div>
</div>
<script src="js/common.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnSearch').bind('click', function () {
            var keyword = $.trim($('#txtKeyword').val());
            var maxPrice = $.trim($('#txtMaxPrice').val());
            var minPrice = $.trim($('#txtMinPrice').val());
            var query = [];
            var queryUrl = "";
            if (keyword.length > 0)
                query.push("keyword=" + keyword);
            var numberReg = /^\d+$/;
            if (maxPrice.length > 0) {
                if (!numberReg.test(maxPrice)) {
                    alert("最高价格必须为数字");
                    return false;
                } else {
                    query.push("maxPrice=" + maxPrice);
                }
            }
            if (minPrice.length > 0) {
                if (!numberReg.test(minPrice)) {
                    alert("最低价格必须为数字");
                    return false;
                } else {
                    query.push("minPrice=" + minPrice);
                }
            }
            queryUrl = query.join('&');
            window.location.href = 'p_list.aspx?' + queryUrl;
        });
        var username = getCookie('tmjcshop_userName');
        if (username != null) {
            $('#member_username').html(username);
            $('#member_username').show();
        } else {
            $('#member_username').hide();
        }
        var isLogin = getCookie('tmjcshop_isLogin');
        if (isLogin != null && isLogin == "yes") {
            $('#logout').show();
            $('#login').hide();
            $('#registe').hide();
            $('#logout').bind('click', function () {
                delCookie('tmjcshop_isLogin');
                $(this).hide();
                window.location.href = 'login.aspx';
            });
        } else {
            $('#logout').hide();
            $('#login').show();
            $('#registe').show();
        }


    });
    
</script>
