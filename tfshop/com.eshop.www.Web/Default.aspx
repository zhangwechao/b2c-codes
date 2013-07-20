<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="help.ascx" TagName="help" TagPrefix="uc1" %>
<%@ Register Src="head.ascx" TagName="head" TagPrefix="uc2" %>
<%@ Register Src="foot.ascx" TagName="foot" TagPrefix="uc3" %>
<%@ Register Src="category.ascx" TagName="category" TagPrefix="uc4" %>
<%@ Register Src="category_2.ascx" TagName="category_2" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>乐购360商城</title>
    <link href="style/common.css" rel="stylesheet" type="text/css" />
    <link href="style/index.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.2.6.pack.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="js/indexhome.js"></script>
    <script language="javascript" type="text/javascript" src="js/flash.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $($("ul.navigation a")[0]).addClass('hover');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:head ID="head1" runat="server" />
    <!-- /end banner -->
    <div class="defaultMain">
        <div class="defaultMainLeft">
            <uc4:category ID="category1" runat="server"></uc4:category>
            <!-- /end 商品分类 -->
        </div>
        <!-- /end defaultMainLeft -->
        <div class="defaultMainRight">
            <div style="padding-bottom: 10px;">
                <img src="images/bnners.jpg" alt="" />
            </div>
            <div class="thelistone">
                <div class="slide">
                    <script type="text/javascript">
                        function getId(id) { return document.getElementById(id); }
                        function addLoadEvent(func) {
                            var oldonload = window.onload;
                            if (typeof window.onload != 'function') {
                                window.onload = func;
                            } else {
                                window.onload = function () {
                                    oldonload();
                                    func();
                                }
                            }
                        }
                        function moveElement(elementID, final_x, final_y, interval) {
                            if (!document.getElementById) return false;
                            if (!document.getElementById(elementID)) return false;
                            var elem = document.getElementById(elementID);
                            if (elem.movement) {
                                clearTimeout(elem.movement);
                            }
                            if (!elem.style.left) {
                                elem.style.left = "0px";
                            }
                            if (!elem.style.top) {
                                elem.style.top = "0px";
                            }
                            var xpos = parseInt(elem.style.left);
                            var ypos = parseInt(elem.style.top);
                            if (xpos == final_x && ypos == final_y) {
                                return true;
                            }
                            if (xpos < final_x) {
                                var dist = Math.ceil((final_x - xpos) / 10);
                                xpos = xpos + dist;
                            }
                            if (xpos > final_x) {
                                var dist = Math.ceil((xpos - final_x) / 10);
                                xpos = xpos - dist;
                            }
                            if (ypos < final_y) {
                                var dist = Math.ceil((final_y - ypos) / 10);
                                ypos = ypos + dist;
                            }
                            if (ypos > final_y) {
                                var dist = Math.ceil((ypos - final_y) / 10);
                                ypos = ypos - dist;
                            }
                            elem.style.left = xpos + "px";
                            elem.style.top = ypos + "px";
                            var repeat = "moveElement('" + elementID + "'," + final_x + "," + final_y + "," + interval + ")";
                            elem.movement = setTimeout(repeat, interval);
                        }
                        function classNormal(iFocusBtnID, iFocusTxID) {
                            var iFocusBtns = getId(iFocusBtnID).getElementsByTagName('li');
                            var iFocusTxs = getId(iFocusTxID).getElementsByTagName('li');
                            for (var i = 0; i < iFocusBtns.length; i++) {
                                iFocusBtns[i].className = 'normal';
                                iFocusTxs[i].className = 'normal';
                            }
                        }
                        function classCurrent(iFocusBtnID, iFocusTxID, n) {
                            var iFocusBtns = getId(iFocusBtnID).getElementsByTagName('li');
                            var iFocusTxs = getId(iFocusTxID).getElementsByTagName('li');
                            iFocusBtns[n].className = 'current';
                            iFocusTxs[n].className = 'current';
                        }
                        function iFocusChange() {
                            if (!getId('ifocus')) return false;
                            getId('ifocus').onmouseover = function () { atuokey = true };
                            getId('ifocus').onmouseout = function () { atuokey = false };
                            var iFocusBtns = getId('ifocus_btn').getElementsByTagName('li');
                            var listLength = iFocusBtns.length;
                            iFocusBtns[0].onmouseover = function () {
                                moveElement('ifocus_piclist', 0, 0, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 0);
                            }
                            if (listLength >= 2) {
                                iFocusBtns[1].onmouseover = function () {
                                    moveElement('ifocus_piclist', 0, -226, 5);
                                    classNormal('ifocus_btn', 'ifocus_tx');
                                    classCurrent('ifocus_btn', 'ifocus_tx', 1);
                                }
                            }
                            if (listLength >= 3) {
                                iFocusBtns[2].onmouseover = function () {
                                    moveElement('ifocus_piclist', 0, -452, 5);
                                    classNormal('ifocus_btn', 'ifocus_tx');
                                    classCurrent('ifocus_btn', 'ifocus_tx', 2);
                                }
                            }
                            if (listLength >= 4) {
                                iFocusBtns[3].onmouseover = function () {
                                    moveElement('ifocus_piclist', 0, -678, 5);
                                    classNormal('ifocus_btn', 'ifocus_tx');
                                    classCurrent('ifocus_btn', 'ifocus_tx', 3);
                                }
                            }
                            if (listLength >= 5) {
                                iFocusBtns[4].onmouseover = function () {
                                    moveElement('ifocus_piclist', 0, -904, 5);
                                    classNormal('ifocus_btn', 'ifocus_tx');
                                    classCurrent('ifocus_btn', 'ifocus_tx', 4);
                                }
                            }
                        }
                        setInterval('autoiFocus()', 5000);
                        var atuokey = false;
                        function autoiFocus() {
                            if (!getId('ifocus')) return false;
                            if (atuokey) return false;
                            var focusBtnList = getId('ifocus_btn').getElementsByTagName('li');
                            var listLength = focusBtnList.length;
                            for (var i = 0; i < listLength; i++) {
                                if (focusBtnList[i].className == 'current') var currentNum = i;
                            }
                            if (currentNum == 0 && listLength != 1) {
                                moveElement('ifocus_piclist', 0, -226, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 1);
                            }
                            if (currentNum == 1 && listLength != 2) {
                                moveElement('ifocus_piclist', 0, -452, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 2);
                            }
                            if (currentNum == 2 && listLength != 3) {
                                moveElement('ifocus_piclist', 0, -678, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 3);
                            }
                            if (currentNum == 3 && listLength != 4) {
                                moveElement('ifocus_piclist', 0, -904, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 4);
                            }
                            if (currentNum == 4) {
                                moveElement('ifocus_piclist', 0, 0, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 0);
                            }
                            if (currentNum == 1 && listLength == 2) {
                                moveElement('ifocus_piclist', 0, 0, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 0);
                            }
                            if (currentNum == 2 && listLength == 3) {
                                moveElement('ifocus_piclist', 0, 0, 5);
                                classNormal('ifocus_btn', 'ifocus_tx');
                                classCurrent('ifocus_btn', 'ifocus_tx', 0);
                            }
                        }
                        addLoadEvent(iFocusChange);
                    </script>
                    <div id="ifocus">
                        <div id="ifocus_pic">
                            <div style="top: 0px; left: 0px" id="ifocus_piclist">
                                <ul>
                                    <li><a href="#" target="_blank">
                                        <img alt="乐购360商城" src="images/tbanner1.jpg"></a></li>
                                    <li><a href="#" target="_blank">
                                        <img alt="乐购360商城" src="images/tbanner2.jpg"></a></li>
                                    <li><a href="#" target="_blank">
                                        <img alt="乐购360商城" src="images/tbanner3.jpg"></a></li>
                                    <li><a href="#" target="_blank">
                                        <img alt="乐购360商城" src="images/tbanner4.jpg"></a></li>
                                    <li><a href="#" target="_blank">
                                        <img alt="乐购360商城" src="images/tbanner5.jpg" /></a></li>
                                </ul>
                            </div>
                            <div id="ifocus_btn">
                                <ul>
                                    <a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a>
                                </ul>
                            </div>
                            <div id="ifocus_opdiv">
                            </div>
                            <div style="display: none" id="ifocus_tx">
                                <ul>
                                    <a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a><a href="#" target="_blank" style="color: #fff;">
                                        <li class="current">乐购360商城</li>
                                    </a>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /end slide -->
                <dl class="Notice">
                    <dt>商城公告</dt>
                    <dd>
                        <ul>
                            <asp:Repeater ID="rSitepost" runat="server">
                                <ItemTemplate>
                                    <li><span>[<%#DateTime.Parse(Eval("create_date").ToString()).ToString("M-d")%>]</span><a
                                        href='news.aspx?Id=<%#Eval("Id") %>' target="_blank"><%#com.eshop.www.Tools.StringHelper.CutString(Eval("title").ToString(),25) %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li class="more"><a href="news_list.aspx?categoryId=4" target="_blank">更多..</a></li>
                        </ul>
                    </dd>
                </dl>
                <!-- /end -->
            </div>
            <!-- /end thelistone -->
            <dl class="thelistwo" style="display: none;">
                <dt>
                    <img src="images/icon-015.gif" alt="" /><uc7:category_2 ID="category_21" runat="server"
                        IsNew="true" />
                </dt>
                <dd>
                    <ul>
                        <asp:Repeater ID="rNewsProduct" runat="server">
                            <ItemTemplate>
                                <li><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank">
                                    <img src='/upload-file/images/product/<%#GetImage(int.Parse(Eval("Id").ToString())).Image %>'
                                        alt='<%#GetImage(int.Parse(Eval("Id").ToString())).Alt %>' width="166px" height="166px" /></a><p>
                                            <a href="p_show.aspx" target="_blank">
                                                <%#Eval("product_name") %></a></p>
                                    <%#float.Parse(Eval("sale_price").ToString()).ToString("0") %>元<span>￥<%#float.Parse(Eval("price").ToString()).ToString("0") %>元</span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </dd>
            </dl>
            <!-- /end thelistwo -->
            <div class="banner">
                <div style="float: left; width: 530px; padding-left: 6px;">
                    <img src="images/gunimg.jpg" /></div>
                <div style="float: left; width: 220px; border: 1px solid #ccc;">
                    <img src="images/rt.jpg" />
                </div>
            </div>
            <!-- /end banner -->
            <dl class="thelisthree" style="display: none;">
                <dt>
                    <img src="images/icon-016.gif" alt="" /><uc7:category_2 ID="category_22" runat="server"
                        IsHot="true" />
                </dt>
                <dd>
                    <ul>
                        <asp:Repeater ID="rHotProduct" runat="server">
                            <ItemTemplate>
                                <li><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank">
                                    <img src='/upload-file/images/product/<%#GetImage(int.Parse(Eval("Id").ToString())).Image %>'
                                        alt='<%#GetImage(int.Parse(Eval("Id").ToString())).Alt %>' width="166px" height="166px" /></a><p>
                                            <a href="p_show.aspx" target="_blank">
                                                <%#Eval("product_name") %></a></p>
                                    <%#float.Parse(Eval("sale_price").ToString()).ToString("0") %>元<span>￥<%#float.Parse(Eval("price").ToString()).ToString("0") %>元</span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </dd>
            </dl>
            <!-- /end thelisthree -->
            <dl class="thelistfour" style="display: none;">
                <dt>
                    <img src="images/icon-017.gif" alt="" /><uc7:category_2 ID="category_23" runat="server"
                        IsDiscount="true" />
                </dt>
                <dd>
                    <ul>
                        <asp:Repeater ID="rDiscountProduct" runat="server">
                            <ItemTemplate>
                                <li><a href='p_show.aspx?productId=<%#Eval("Id") %>' target="_blank">
                                    <img src='/upload-file/images/product/<%#GetImage(int.Parse(Eval("Id").ToString())).Image %>'
                                        alt='<%#GetImage(int.Parse(Eval("Id").ToString())).Alt %>' width="166px" height="166px" /></a><p>
                                            <a href="p_show.aspx" target="_blank">
                                                <%#Eval("product_name") %></a></p>
                                    <%#float.Parse(Eval("sale_price").ToString()).ToString("0") %>元<span>￥<%#float.Parse(Eval("price").ToString()).ToString("0") %>元</span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </dd>
            </dl>
            <!-- /end thelistfour -->
        </div>
        <!-- /end defaultMainRight -->
    </div>
    <!-- /end defaultMain -->
    <!-- /end banner -->
    <uc1:help ID="help1" runat="server" />
    <!-- /end help -->
    <uc3:foot ID="foot1" runat="server" />
    </form>
</body>
</html>
