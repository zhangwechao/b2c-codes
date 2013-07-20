<%@ Control Language="C#" AutoEventWireup="true" CodeFile="category.ascx.cs" Inherits="category"  %>
<div class="sn_Menu_content indexMenu" id="SideNav">
    
    <asp:Repeater ID="rFatherCategory" runat="server" 
        onitemdatabound="rFatherCategory_ItemDataBound">
        <ItemTemplate>
            <dl class="sidenav01">
                <dd>
                    <a href='p_list.aspx?categoryId=<%#Eval("Id") %>' class="icon"
                        target="_blank"><%#Eval("category_name") %></a></dd>
                <div class="sidenavlink01 sidenavchild">
                    <ul class="sideleft">
                        <h3>
                             <a href='p_list.aspx?categoryId=<%#Eval("Id") %>'  target="_blank"><%#Eval("category_name") %></a></h3>
                            <asp:Repeater ID="rSonCategory" runat="server" onitemdatabound="rSonCategory_ItemDataBound">
                            <ItemTemplate>
                                 <%if (i == 0) 
                                  { %>
                                <li class="noline">
                                <%}
                                      else
                                      { %>
                                      <li>
                                      <%} i++; %>
                                <b><a href='p_list.aspx?categoryId=<%#Eval("Id") %>'  target="_blank"><%#Eval("category_name") %></a></b>
                                    <div>
                                    <asp:Repeater ID="rThreeCategory" runat="server">
                                    <ItemTemplate>
                                    <a href='p_list.aspx?categoryId=<%#Eval("Id") %>' title='<%#Eval("category_name") %>'
                                            target="_blank"><%#Eval("category_name") %></a>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </div>
                                    <span class="clear"></span></li>
                            </ItemTemplate>
                            </asp:Repeater>
                           <%i = 0; %>
                    </ul>
                    <ul class="sideright">
                        <h3>
                            品牌推荐</h3>
                            <asp:Repeater ID="rBrandList" runat="server" onitemdatabound="rBrandList_ItemDataBound">
                            <ItemTemplate>
                                <li><asp:HyperLink ID="hlBrandLink" runat="server" Target="_blank"><%#Eval("brand_name") %></asp:HyperLink></li>
                            </ItemTemplate>
                            </asp:Repeater>
                    </ul>
                </div>
            </dl>
        </ItemTemplate>
    </asp:Repeater>
</div>
