<%@ Control Language="C#" AutoEventWireup="true" CodeFile="help.ascx.cs" Inherits="help" %>
<div class="helps">
<div class="help">
    <!-- /end file-01 -->
    <dl class="file-02">
        <dt>新手上路</dt>
        <asp:Repeater runat="server" ID="rNovice">
            <ItemTemplate>
                <dd>
                    ·<a href='help.aspx?Id=<%#Eval("Id") %>' target="_blank"><%#Eval("title") %></a></dd>
            </ItemTemplate>
        </asp:Repeater>
    </dl>
    <!-- /end file-02 -->
    <dl class="file-03">
        <dt>购物指南</dt>
        <asp:Repeater runat="server" ID="rGuide">
            <ItemTemplate>
                <dd>
                    ·<a href='help.aspx?Id=<%#Eval("Id") %>' target="_blank"><%#Eval("title") %></a></dd>
            </ItemTemplate>
        </asp:Repeater>
    </dl>
    <!-- /end file-03 -->
    <dl class="file-04">
        <dt>支付/配送方式</dt>
        <asp:Repeater runat="server" ID="rPayment">
            <ItemTemplate>
                <dd>
                    ·<a href='help.aspx?Id=<%#Eval("Id") %>' target="_blank"><%#Eval("title") %></a></dd>
            </ItemTemplate>
        </asp:Repeater>
    </dl>
    <!-- /end file-04 -->
    <dl class="file-05">
        <dt>购物条款</dt>
        <asp:Repeater runat="server" ID="rProvisions">
            <ItemTemplate>
                <dd>
                    ·<a href='help.aspx?Id=<%#Eval("Id") %>'><%#Eval("title") %></a></dd>
            </ItemTemplate>
        </asp:Repeater>
    </dl>
    <!-- /end file-05 -->
    <dl class="file-01">
        <dt>在线帮助</dt>
        <asp:Repeater runat="server" ID="rAboutUs">
            <ItemTemplate>
                <dd>
                    ·<a href='help.aspx?Id=<%#Eval("Id") %>' target="_blank"><%#Eval("title") %></a></dd>
            </ItemTemplate>
        </asp:Repeater>
    </dl>
</div>
</div>
