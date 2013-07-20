<%@ Control Language="C#" AutoEventWireup="true" CodeFile="category_2.ascx.cs" Inherits="category_2" %>
<asp:Repeater ID="rCategoryList" runat="server" 
    onitemdatabound="rCategoryList_ItemDataBound">
<ItemTemplate>
<asp:HyperLink ID="hlcategorylink" runat="server"><%#Eval("category_name") %></asp:HyperLink>
</ItemTemplate>
</asp:Repeater>
