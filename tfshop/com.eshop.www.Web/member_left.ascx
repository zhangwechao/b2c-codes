<%@ Control Language="C#" AutoEventWireup="true" CodeFile="member_left.ascx.cs" Inherits="member_left" %>
<div class="memnav-title">
    <a href="membercenter.aspx">会员中心</a></div>
<div class="memnav">
    <ul class="bor2-brown">
        <li class="cur" id="mem-n-3"><a href="member_center.aspx" class="a3"><span>
            我的订单</span></a></li>
        <li class="" id="mem-n-4"><a href="product_comment.aspx" class="a3"><span>商品评论</span></a></li>
        <li id="mem-n-11"><a href="my_comment.aspx" class="a3">
            <span>我的评论</span></a></li>
        <li class="" id="mem-n-6"><a href="favorite.aspx" class="a3"><span>我的收藏</span></a></li>
        <li class="" id="mem-n-7"><a href="shoppingCart.aspx" class="a3"><span>我的购物车</span></a></li>
        <li class="" id="mem-n-10"><a href="complain.aspx" class="a3">
            <span>投诉建议</span></a></li>
        <li id="mem-n-9"><a href="my_profile.aspx" class="a3"><span>我的资料</span></a>
            <ul class="mennavsub">
                <li class=""><a href="modify_info.aspx" class="a3">编辑个人档案</a></li>
                <li class=""><a href="modify_pass.aspx" class="a3">修改密码</a></li>
            </ul>
        </li>
    </ul>
</div>
<script type="text/javascript">
$(function(){
    var page = location.href;
    $("ul.bor2-brown li").removeClass('cur');
    $("ul.mennavsub li").removeClass('cur');
    if(page.indexOf("member_center")>-1)
        $($("ul.bor2-brown li")[0]).addClass('cur');
    if(page.indexOf("product_comment")>-1)
        $($("ul.bor2-brown li")[1]).addClass('cur');
    if(page.indexOf("my_comment")>-1)
        $($("ul.bor2-brown li")[2]).addClass('cur');
    if(page.indexOf("favorite")>-1)
        $($("ul.bor2-brown li")[3]).addClass('cur');
    if(page.indexOf("shoppingCart")>-1)
        $($("ul.bor2-brown li")[4]).addClass('cur');
    if(page.indexOf("complain")>-1)
        $($("ul.bor2-brown li")[5]).addClass('cur');
    if(page.indexOf("my_profile")>-1)
        $($("ul.bor2-brown li")[6]).addClass('cur');
    if(page.indexOf("modify_info")>-1)
        $($("ul.mennavsub li")[0]).addClass('cur');
    if(page.indexOf("modify_pass")>-1)
        $($("ul.mennavsub li")[1]).addClass('cur');
});
</script>