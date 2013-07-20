
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.eshop.www.Model;
using com.eshop.www.BLL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rSitepost_load();
            Product_load();
        }
    }
    private void rSitepost_load()
    {
        int number=7;
        string where = "category_id=4 and is_show=1 and is_delete=0";
        string fieldList = "Id,title,order_by,is_show,create_date";
        string orderField = "order_by";
        bool orderby = true;
        DataRecordTable table = new NewsContentBusiness().GetList(fieldList,orderField,orderby,1,number,where);
        rSitepost.DataSource = table.Table;
        rSitepost.DataBind();
    }
    private void Product_load()
    {
        ProductDetailBusiness business = new ProductDetailBusiness();

        int number=8;
        string where = "is_new=1 and is_show=1 and is_delete=0";
        string fieldList = "Id,product_name,order_by,is_show,price,sale_price";
        string orderField = "order_by";
        bool orderby = true;
        DataRecordTable table = business.GetList(fieldList,orderField,orderby,1,number,where);
        rNewsProduct.DataSource = table.Table;
        rNewsProduct.DataBind();

        where = "is_hot=1 and is_show=1 and is_delete=0";
        table = business.GetList(fieldList,orderField,orderby,1,number,where);
        rHotProduct.DataSource = table.Table;
        rHotProduct.DataBind();

        where = "is_discount=1 and is_show=1 and is_delete=0";
        table = business.GetList(fieldList, orderField, orderby, 1, number, where);
        rDiscountProduct.DataSource = table.Table;
        rDiscountProduct.DataBind();

    }
    
    protected ProductImage GetImage(int productId)
    {
        string where = "product_id="+productId+" and type=1";
        string fieldList = "Id,product_id,image,alt";
        string orderFiled = "Id";
        bool orderby = false;
        DataRecordTable table = new ProductImageBusiness().GetList(fieldList,orderFiled,orderby,1,1,where);
        ProductImage productImage = new ProductImage();
        if (table.Table.Rows.Count > 0)
        {
            productImage.Image = table.Table.Rows[0]["image"].ToString();
            productImage.Alt = table.Table.Rows[0]["alt"].ToString();
        }
        return productImage;
    }
}
