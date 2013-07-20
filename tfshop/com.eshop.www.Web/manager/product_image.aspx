<%@ Page Title="产品图片" Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="product_image.aspx.cs" Inherits="back_stage_product_image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("div#content_input input[type=checkbox]").bind("click", function() {
            $("div#content_input input[type=checkbox]").attr("checked", false);
            $(this).attr("checked", true);
            var title = $(this).val();
            $("#<%=imgDetail.ClientID %>").attr("src", "/upload-file/images/product/" + title);
            $("#<%=hiddenDefaultImage.ClientID %>").val(title);
        });
    });
</script>
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
        <h2>产品图片</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable">
            <tr>
                <td colspan="6" align="right" class="inputTableTd">
                    <asp:Button ID="btnSave2" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnReturn2" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="inputTableTd" rowspan="3">列表图片</td>
                <td class="inputTableTd" rowspan="3">
                    <asp:Image ID="imgImages" runat="server" Width="100" Height="100" ImageUrl="image/no.jpg" BorderWidth="1" BorderStyle="Solid" />
                    <asp:HiddenField ID="hiddenImage" runat="server" />
                    <asp:HiddenField ID="hiddenId" runat="server" Value="0" />
                </td>
                <td class="inputTableTd">上传图片</td>
                <td class="inputTableTd">
                    <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                <td class="inputTableTd">
                    <asp:Button ID="btnUpload" runat="server" Text="上传" onclick="btnUpload_Click" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">图片提示</td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtAlt" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="inputTableTd"></td>
                <td class="inputTableTd">
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="inputTableTd" rowspan="3">详细图片</td>
                <td class="inputTableTd" rowspan="3">
                     <img id="imgDetail" runat="server" src="image/no.jpg" width="100" height="100" style="border:1px solid black" />
                </td>
                <td class="inputTableTd">上传图片</td>
                <td class="inputTableTd">
                    <asp:FileUpload ID="fuDetail" runat="server" /></td>
                <td class="inputTableTd">
                    
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">图片提示</td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtDetailAlt" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="inputTableTd">放大图</td>
                <td class="inputTableTd">
                    <asp:FileUpload ID="fuZoom" runat="server" />
                </td>
                <td class="inputTableTd">
                    <asp:Button ID="btnDetailUpload" runat="server" Text="上传" onclick="btnDetailUpload_Click" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd"></td>
                <td class="inputTableTd" colspan="5">
                    <asp:DataList ID="dlImages" RepeatColumns="5" runat="server" 
                        onitemcommand="dlImages_ItemCommand" 
                        onitemdatabound="dlImages_ItemDataBound">
                    <ItemTemplate>
                        <img src='/upload-file/images/product/<%#Eval("Image") %>' width="80" height="80" id="imgDetail" />
                        <br />
                        <asp:Button ID="btnDelete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("image") %>'  />
                        <asp:HiddenField ID="hiddenId" runat="server"  Value='<%#Eval("Id") %>'/>
                        <input type="checkbox" value='<%#Eval("Image") %>' id="chkSetDefault" runat="server" />默认
                    </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
           
            <tr>
                <td colspan="6" align="right">
                    <asp:HiddenField ID="hiddenDefaultImage" runat="server" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </form>
    </div>
    <div id="content_note">
        <h2>所填注意事项</h2>
        <ul>
            <li>红色*号为必填项</li>
            <li>一个中文占两个字符</li>
            <li>列表图片最佳为166*166，jpg或者gif图片，30kb以内</li>
            <li>详细图片最佳为310*310，jpg或者gif图片，30kb以内</li>
            <li>放大图片最佳为466*466，jpg或者gif图片，30kb以内</li>
            <li>图片提示最多50个字符</li>
        </ul>    
    </div>
</div>
</asp:Content>

