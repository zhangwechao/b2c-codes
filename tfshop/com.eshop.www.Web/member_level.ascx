<%@ Control Language="C#" AutoEventWireup="true" CodeFile="member_level.ascx.cs" Inherits="member_level" %>
<div class="grade">
    <ul>
    <li class="g1"> 您现在积分<span class="blod" style="font-weight:bold; font-size:16px"><asp:Literal ID="lIntegral" runat="server"></asp:Literal>分</span>，是<span class="blod" style="font-weight:bold; font-size:16px"><asp:Literal ID="lLevelName" runat="server"></asp:Literal></span>，
        所有产品<span class="blod" style="font-weight:bold; font-size:16px;color:Red"><asp:Literal ID="lDiscount" runat="server"></asp:Literal></span> </li>
<%--    <li class="g3"> <a target="_blank" href="#"> 快速升级金卡</a><a target="_blank" href="#"> 快速升级钻石卡</a></li>--%>
	</ul>
</div>