<%@ Page Language="C#" MasterPageFile="~/manager/admin.master" AutoEventWireup="true" CodeFile="product_detail.aspx.cs" Inherits="manager_product_detail" Title="产品内容" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/validata.field.js" type="text/javascript"></script>
<script src="editor/kindeditor.js" type="text/javascript" charset="utf-8"></script>
<link href="css/detail.css" rel="stylesheet" type="text/css" />  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
var indexNum = $('ul#ul_nav li').index($('#li_product'));
$tabs.tabs('select',indexNum);
$("#product_content_page").css({ "background-image": "url(image/A-dmin-0122.gif)" });
</script>
<div id="content">
    <div id="content_input" style="width:720px;">
    <form id="form1"  action="" runat="server">
        <div id="tip">
        <h2>产品内容</h2>
        <p>请参考右边的提示输入或者修改下面的信息
            </p></div>
        <table id="inputTable" width="100%">
            <tr>
                <td colspan="6" align="right" class="inputTableTd">
                    <asp:Button ID="btnDelete2" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnReset2" runat="server" Text="重置" onclick="btnReset_Click" />
                    <asp:Button ID="btnSave2" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnNext2" runat="server" Text="下一条" onclick="btnNext_Click" />
                    <asp:Button ID="btnPrev2" runat="server" Text="上一条" onclick="btnPrev_Click" />
                    <asp:Button ID="btnReturn2" runat="server" Text="返回" onclick="btnReturn_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    名称&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:TextBox ID="txtProductName" runat="server" Width="80%"></asp:TextBox>
                </td>
             </tr>
             <tr>   
                <td class="inputTableTd">
                    排序号
                </td>
                <td class="inputTableTd" width="150">
                    <asp:TextBox ID="txtOrderBy" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                库存
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                销售量
                </td>
                <td class="inputTableTd">
                <asp:TextBox ID="txtSaleNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    一级菜单&nbsp;<font color="red">*</font>
                </td>
                <td class="inputTableTd">
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="inputTableTd">
                    二级菜单
                </td>
                <td class="inputTableTd">
                    <asp:DropDownList ID="ddlSecondCategory" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlSecondCategory_SelectedIndexChanged">
                        <asp:ListItem Value="0">--请选择一级菜单--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="inputTableTd">
                    所属类别
                </td>
                <td class="inputTableTd">
                    <asp:DropDownList ID="ddlThirdCategory" runat="server">
                    <asp:ListItem Value="0">--请选择二级菜单--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                     所属品牌
                </td>
                <td class="inputTableTd" colspan="5">
                    <asp:DropDownList ID="ddlBrand" runat="server">
                    <asp:ListItem Value="0">--请选择一级菜单--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="inputTableTd">
                    显示
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsShow" runat="server" Text="显示" Checked="true" />
                </td>
                <td class="inputTableTd">
                    推荐
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsRecomment" runat="server" Text="推荐" />
                </td>
                <td class="inputTableTd">
                    评论
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsComment" runat="server" Text="允许评论" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    热销
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsHot" runat="server" Text="热销产品" />
                </td>
                <td class="inputTableTd">
                    最新
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsNew" runat="server" Text="最新产品" Checked="true" />
                </td>
                <td class="inputTableTd">
                    特惠
                </td>
                <td class="inputTableTd">
                    <asp:CheckBox ID="chkIsDiscount" runat="server" Text="特惠产品" />
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    原价
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    销售价
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtSalePrice" runat="server"></asp:TextBox>
                </td>
                <td class="inputTableTd">
                    送积分
                </td>
                <td class="inputTableTd">
                    <asp:TextBox ID="txtIntegral" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">
                    列表图片
                </td>
                <td class="inputTableTd" colspan="5" valign="middle">
                    <asp:HiddenField ID="hiddenImage" runat="server" />
                    <asp:HiddenField ID="hiddenImageId" runat="server" />
                    <a runat="server" id="aImageUrl" target="_blank">
                     <asp:Image ID="imgImages" runat="server" Width="50" Height="50" ImageUrl="image/no.jpg" BorderWidth="1" BorderStyle="Solid" /></a>
                     <asp:FileUpload ID="fuUploadList" runat="server" />&nbsp;<asp:Button ID="btnUploadList" runat="server" Text="上传" onclick="btnUploadList_Click" />
                </td>
            </tr>
            <tr>
                <td  class="inputTableTd">
                    详细页图片
                </td>
                <td class="inputTableTd" colspan="5">
                    <table id="mytable" width="100%">
                        <tr>
                            <th>序号</th>
                            <th>详细页图片</th>
                            <th>对应放大图</th>
                            <th>默认第一张</th>
                            <th>操作</th>
                        </tr>
                        <asp:Repeater ID="rProductImage" runat="server" 
                            onitemcommand="rProductImage_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td align="center"><%#Container.ItemIndex+1 %></td>
                                    <td align="center"><a href='/upload-file/images/product/<%#Eval("Image") %>'  target='_blank'><img src='/upload-file/images/product/<%#Eval("Image") %>' width="50" height="50" /></a></td>
                                    <td align="center"><a href='/upload-file/images/product/<%#Eval("ZoomImage") %>'  target='_blank'><img src='/upload-file/images/product/<%#Eval("ZoomImage") %>' width="50" height="50" /></a></td>
                                    <td align="center"><%#bool.Parse(Eval("IsDefault").ToString())?"是":"否" %></td>
                                    <td align="center">
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#Eval("Image") %>' CommandName="delete">删除</asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <br />
                    <table width="100%">
                        <tr>
                            <td>详细页图片</td>
                            <td><asp:FileUpload ID="fuDetailImage" runat="server" /></td>
                            <td>放大图</td>
                            <td><asp:FileUpload ID="fuDetailZoomImage" runat="server" /></td>
                            <td>默认第一张<asp:CheckBox ID="chkIsDefault" runat="server" /></td>
                            <td><asp:Button ID="btnUploadDetail" runat="server" Text="上传" 
                                    onclick="btnUploadDetail_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">关键字</td>
                <td colspan="5" class="inputTableTd">
                    <asp:TextBox ID="txtKeywords" runat="server" Width="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">摘要</td>
                <td colspan="5" class="inputTableTd">
                    <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Rows="3" Columns="69"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="inputTableTd">描述&nbsp;<font color="red">*</font></td>
                <td colspan="5" class="inputTableTd">
                <textarea id="txtContent"  runat="server" style="width:600px;height:300px;">
                
                </textarea>
                <script type="text/javascript">
                    KE.show({
                            id : '<%=txtContent.ClientID %>',
                            imageUploadJson : '/manager/handler/upload_json.ashx'
                    });
                </script>
                </td>
            </tr>
            
            <tr>
                <td colspan="6" align="right">
                    <asp:Button ID="btnDelete" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return ask()" />
                    <asp:Button ID="btnReset" runat="server" Text="重置" onclick="btnReset_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return validata()" />
                    <asp:Button ID="btnNext" runat="server" Text="下一条" onclick="btnNext_Click" />
                    <asp:Button ID="btnPrev" runat="server" Text="上一条" onclick="btnPrev_Click" />
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
            <li>产品名称最多100个字符</li>
            <li>排序号必须是数字，如果为空则排在最前</li>
            <li style=" height:60px">关键字最多50个字符，有多个关键字，<br />最好用｜隔开</li>
            <li>摘要最多300个字符</li>
            <li>价格必须为数字</li>
            <li>库存和销售数量不要轻易更改</li>
            <li>积分必须为数字</li>
        </ul>    
    </div>
</div>
<script type="text/javascript">
function ask(){
    if(!confirm('确定要删除当前记录吗？'))
        return false;
}
function validata(){
    var productName = $.trim($("#<%=txtProductName.ClientID %>").val());
    var orderBy = $.trim($("#<%=txtOrderBy.ClientID %>").val());
    var category = $("#<%=ddlCategory.ClientID %>").val();
    var keywords = $.trim($("#<%=txtKeywords.ClientID %>").val());
    var summary = $.trim($("#<%=txtSummary.ClientID %>").val());
    var content = KE.html("<%=txtContent.ClientID %>");
    var price = $.trim($("#<%=txtPrice.ClientID %>").val());
    var salePrice = $.trim($("#<%=txtSalePrice.ClientID %>").val());
    var integral = $.trim($("#<%=txtIntegral.ClientID %>").val());
    var saleNumber = $.trim($("#<%=txtSaleNumber.ClientID %>").val());
    var stock = $.trim($("#<%=txtStock.ClientID %>").val());
    if(productName.length==0){
        alert("请输入标题");
        return false;
    }
    if(validataField.getStrLength(productName)>100){
        alert("标题字符超过限制，最多100个字符，一个中文两个字符");
        return false;
    }
    var orderReg = /^\d+$/;
    if(orderBy.length>0){
        if(!orderReg.test(orderBy)){
            alert("排序号必须为数字");
            return false;
        }
    }
    if(saleNumber.length>0){
        if(!orderReg.test(saleNumber)){
            alert("销售数量必须数字");
            return false;
        }
    }
    if(stock.length>0){
        if(!orderReg.test(stock)){
            alert("库存必须数字");
            return false;
        }
    }
    if(category=="0"){
        alert("请选择所属目录");
        return false;
    }
    if(validataField.getStrLength(keywords)>50){
        alert("关键字字符超过限制，最多50个字符，一个中文两个字符");
        return false;
    }
    if(validataField.getStrLength(summary)>300){
        alert("摘要字符超过限制，最多300个字符，一个中文两个字符");
        return false;
    }
    if(content.length==0){
        alert("请输入产品描述");
        return false;
    }
    var priceReg = /^\d+(.)?\d{0,1}/;
    if (price.length > 0 || salePrice.length > 0) {
        if (!priceReg.test(price) || !priceReg.test(salePrice)) {
            alert("价格请输入数字");
            return false;
        }
    }
    if (integral.length > 0) {
        if (!orderReg.test(integral)) {
            alert("积分请输入数字");
            return false;
        }
    }
}
</script>
</asp:Content>

