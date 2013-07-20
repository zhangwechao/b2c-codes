<%@ Page Title="产品属性" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="product_attribute.aspx.cs" Inherits="back_stage_product_attribute" %>
<%@ Import Namespace="com.eshop.www.Model" %>
<%@ Import Namespace="com.eshop.www.BLL" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="css/detail.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_product'));
$tabs.tabs('select',indexNum);
$("#product_content_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>产品属性</h2>
        </div>
        <table id="inputTable">
        <tr>
            <td colspan="2" align="right" class="inputTableTd">
                <input type="button" value="返回产品列表" onclick="window.location='product_content.aspx'" />
            </td>
        </tr>
                <asp:Repeater ID="attributeList" runat="server" 
                    onitemdatabound="attributeList_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td class="inputTableTd">
                            <%#Eval("attribute") %>：
                            <asp:HiddenField ID="hiddenValuesList" runat="server" Value='<%#Eval("values_list") %>' />
                            <asp:HiddenField ID="hiddenIsMultiple" runat="server" Value='<%#bool.Parse(Eval("is_multiple").ToString())?1:0 %>' />
                            <asp:HiddenField ID="hiddenAttributeId" runat="server" Value='<%#Eval("attribute_id") %>' />
                        </td>
                        <td class="inputTableTd">
                            <asp:CheckBoxList ID="chkValues" runat="server" Visible="false" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                            <asp:DropDownList ID="ddlValues" runat="server" Visible="false">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtValues" runat="server" Visible="false" ></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                </asp:Repeater>
             <tr>   
                <td align="right" colspan="2" class="inputTableTd">
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click"/>
                </td>
            </tr>
        </table>
    </form>
    </div>
</div>
</asp:Content>


