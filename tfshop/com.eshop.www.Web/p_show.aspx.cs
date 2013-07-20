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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.eshop.www.Model;
using com.eshop.www.BLL;
using System.Web.UI.HtmlControls;
using System.Data;
using com.eshop.www.Tools;

public partial class p_show : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataRead(); 
        }
    }
    private void DataRead()
    {
        string productId = Request.QueryString["productId"];
        if (!string.IsNullOrEmpty(productId))
        {
            int Id = int.Parse(productId);
            ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
            ProductCommentBusiness productCommentBusiness = new ProductCommentBusiness();
            ProductDetail productDetail = productDetailBusiness.GetEntity(Id);
            lProductName.Text = productDetail.ProductName;
            lPrice.Text = productDetail.Price.ToString();
            lSalePrice.Text = productDetail.SalePrice.ToString();
            lSaleNumber.Text  = productDetail.SaleNumber.ToString();
            hiddenScore.Value = productCommentBusiness.GetAvgCommentByProduct(Id).ToString();
            lScore.Text = hiddenScore.Value;
            lCommentNumber.Text  =  productCommentBusiness.GetSumCommentByProduct(Id).ToString();
            
            lIntegral.Text = productDetail.integral.ToString();
            lTotalNumber.Text = productDetail.Stock.ToString();
            lDesc.Text = productDetail.Description;

            Page.Title = productDetail.ProductName;

            HtmlMeta keyword = new HtmlMeta();
            keyword.Name = "keywords";
            keyword.Content = productDetail.Keywords;
            Page.Header.Controls.Add(keyword);

            HtmlMeta desc = new HtmlMeta();
            desc.Name = "description";
            desc.Content = productDetail.Summary;
            Page.Header.Controls.Add(desc);

            ProductImage_load(Id);
            ProductComment_load(Id);
            ProductAttribute_load(Id);
            ProductTran_load(Id);
            ProductRelateProduct(Id);
            RelateBrowse(Id);
        }
    }
    /// <summary>
    /// 产品图片
    /// </summary>
    /// <param name="productId"></param>
    private void ProductImage_load(int productId)
    {
        ProductImageBusiness business = new ProductImageBusiness();
        string where = "product_id="+productId+" and type=2 and is_default=1";
        DataRecordTable table = business.GetList("Id,product_id,image,alt,zoom_image","Id",false,1,1,where);
        if (table.Table.Rows.Count > 0)
        {
            DataRow row = table.Table.Rows[0];
            string image = row["image"].ToString();
            string alt = row["alt"].ToString();
            string zoomImage = row["zoom_image"].ToString();
            defautlImage.Src = "upload-file/images/product/"+image;
            defautlImage.Alt = alt;
            defautlImage.Attributes["jqimg"] = "upload-file/images/product/"+zoomImage;
        }

        where = "product_id="+productId+" and type=2";
        table = business.GetList("product_id,image,alt,zoom_image","Id",false,1,10,where);
        rProductImageList.DataSource = table.Table;
        rProductImageList.DataBind();
    }
    /// <summary>
    /// 产品属性展示
    /// </summary>
    /// <param name="productId"></param>
    private void ProductAttribute_load(int productId) 
    {
        string fieldList = "product_id,attribute_id,attribute_value";
        string where = "product_id="+productId;
        DataRecordTable table = new ProductAttributeValueBusiness().GetList(fieldList,"Id",false,1,100,where);
        rProductAttributeList.DataSource = table.Table;
        rProductAttributeList.DataBind();
    }
    /// <summary>
    /// 产品评论
    /// </summary>
    /// <param name="productId"></param>
    private void ProductComment_load(int productId) 
    {
        string where = "product_id="+productId+" and is_show=1";
        string fieldList = "Id,product_id,content,user_name,score,create_date";
        
        DataRecordTable table = new ProductCommentBusiness().GetList(fieldList,"Id",true,AspNetPager2.CurrentPageIndex,AspNetPager2.PageSize,where);
        rProductCommentList.DataSource = table.Table;
        rProductCommentList.DataBind();
        AspNetPager2.RecordCount = table.RecordCount;
    }
    /// <summary>
    ///  交易记录
    /// </summary>
    /// <param name="productId"></param>
    private void ProductTran_load(int productId) 
    {
        string where = "product_id="+productId;
        string fieldList = "product_id,member_id,quantity,total_money,create_date";
        DataRecordTable table = new OrderItemBusiness().GetList(fieldList,"Id",true,AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize,where);
        rProductTranList.DataSource = table.Table;
        rProductTranList.DataBind();
        AspNetPager1.RecordCount = table.RecordCount;
    }
    /// <summary>
    /// 相关产品
    /// </summary>
    /// <param name="productId"></param>
    private void ProductRelateProduct(int productId) 
    {
        string where = "product_id=" + productId + " and is_show=1 and is_delete=0";
        DataRecordTable table = new ProductRelateProductBusiness().GetList("Id,product_name,sale_price,price,order_by","order_by",true,1,4,where);
        rRelateProduct.DataSource = table.Table;
        rRelateProduct.DataBind();
    }
    /// <summary>
    /// 得到图片
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 浏览了其他的商品
    /// </summary>
    /// <param name="productId"></param>
    private void RelateBrowse(int productId)
    {
        string where = "product_id=" + productId + " and is_show=1 and is_delete=0";
        DataRecordTable table = new ProductRelateProductBusiness().GetList("Id,product_name,sale_price,price,click_number", "click_number", true, 1, 4, where);
        rRelateBrowse.DataSource = table.Table;
        rRelateBrowse.DataBind();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {

        ProductComment_load(int.Parse(Request.QueryString["productId"]));
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        ProductTran_load(int.Parse(Request.QueryString["productId"]));
    }
    protected void imgBtnNowBuy_Click(object sender, ImageClickEventArgs e)
    {
        string productId = Request.QueryString["productId"];
        if (!string.IsNullOrEmpty(productId))
        {
            string number = txtNumber.Text.Trim();
            ShoppingCart cart = new ShoppingCart();
            HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
            cart.Add(int.Parse(productId), int.Parse(number), cookie);
            Response.Cookies.Add(cookie);
            Response.Redirect("order.aspx");
        }
    }
    protected void imgBtnAddShppingCart_Click(object sender, ImageClickEventArgs e)
    {
        string productId = Request.QueryString["productId"];
        if (!string.IsNullOrEmpty(productId))
        {
            string number = txtNumber.Text.Trim();
            ShoppingCart cart = new ShoppingCart();
            HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
            cart.Add(int.Parse(productId),int.Parse(number),cookie);
            Response.Cookies.Add(cookie);
            Response.Redirect("shoppingCart.aspx");
        }
    }
    protected void lBtnAddFavorite_Click(object sender, EventArgs e)
    {
        string productId = Request.QueryString["productId"];
        if (!string.IsNullOrEmpty(productId))
        {
            HttpCookie isLogin = Request.Cookies[Cookie.IsLoginCookieName];
            if (isLogin == null || isLogin.Value != "yes")
            {
                HttpCookie favorite = Request.Cookies[Cookie.FavoriteCookieName];
                if (favorite == null)
                    favorite = new HttpCookie(Cookie.FavoriteCookieName);
                favorite.Value = productId;
                Response.Cookies.Add(favorite);
            }
            else
            {
                HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
                Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
                Favorite fav = new Favorite() { ProductId=int.Parse(productId), MemberId=member.Id };
                FavoriteBusiness business = new FavoriteBusiness();
                bool isProduct = business.IsHaveSameProduct(fav);
                if (isProduct)
                {
                    JavascriptHelper.Alert("该产品已经加入收藏夹");
                    return;
                }
                business.Add(fav);
            }
            Response.Redirect("favorite.aspx");
        }
    }
}
