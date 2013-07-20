//--------------------------------------------------
// 网商宝商城免费开源版 V1.0.110909
// 本程序仅用于学习和研究，不得作为商业用途。
// 如需进行商城运营，请与我公司联系购买商业版本。
//
// 东莞市捷联科技有限公司
// 网址：www.128.com.cn
// QQ：1316108492
// 电话：400-678-1128
//--------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.eshop.www.Model;
using com.eshop.www.BLL;

public partial class favorite : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rProductList_load();
        }
    }
    private void rProductList_load()
    { 
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        if (userName != null && !string.IsNullOrEmpty(userName.Value))
        {
            Member member = new MemberBusiness().GetEntityByUserName(userName.Value);

            string fieldList = "Id,product_id,product_name,sale_price,price,create_date";
            DataRecordTable table = new FavoriteBusiness().GetList(fieldList, "create_date", true, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize,"member_id="+member.Id);
            rProductList.DataSource = table.Table;
            rProductList.DataBind();
            AspNetPager1.RecordCount = table.RecordCount;
        }
    }
    protected ProductImage GetImage(int productId)
    {
        string where = "product_id=" + productId + " and type=1";
        string fieldList = "Id,product_id,image,alt";
        string orderFiled = "Id";
        bool orderby = false;
        DataRecordTable table = new ProductImageBusiness().GetList(fieldList, orderFiled, orderby, 1, 1, where);
        ProductImage productImage = new ProductImage();
        if (table.Table.Rows.Count > 0)
        {
            productImage.Image = table.Table.Rows[0]["image"].ToString();
            productImage.Alt = table.Table.Rows[0]["alt"].ToString();
        }
        return productImage;
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        rProductList_load();
    }
    protected void rProductList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        switch (cmdName)
        {
            case "buyNow":
                AddProduct(int.Parse(e.CommandArgument.ToString()));
                Response.Redirect("order.aspx");
                break;
            case "addShoppingCart":
                AddProduct(int.Parse(e.CommandArgument.ToString()));
                Response.Redirect("shoppingCart.aspx");
                break;
            case "delete":
                int Id = int.Parse(e.CommandArgument.ToString());
                new FavoriteBusiness().Delete(Id);
                rProductList_load();
                break;
        }

    }
    private void AddProduct(int productId)
    {
        ShoppingCart cart = new ShoppingCart();
        HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
        cart.Add(productId, 1, cookie);
        Response.Cookies.Add(cookie);
    }
}
