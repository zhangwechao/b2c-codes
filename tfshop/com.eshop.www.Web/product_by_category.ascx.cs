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
using com.eshop.www.BLL;
using com.eshop.www.Model;

public partial class product_by_category : System.Web.UI.UserControl
{
    private int categoryId;
    public int CategoryId
    {
        get { return categoryId; }
        set { categoryId = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lCategoryName.Text = new ProductCategoryBusiness().GetEntity(CategoryId).CategoryName;
            rProductList_load();
        }
    }
    private void rProductList_load()
    {
        string where = "category_id in ("+GetIds(CategoryId)+") and is_show=1 and is_delete=0";
        string fieldList="Id,product_name,price,sale_price,order_by";
        string orderField = "order_by";
        bool orderby = true;
        DataRecordTable table = new ProductDetailBusiness().GetList(fieldList, orderField, orderby, 1, 5, where);
        rProductList.DataSource = table.Table;
        rProductList.DataBind();
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
    private string GetIds(int categoryId)
    {
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        string path = productCategoryBusiness.GetEntity(categoryId).Path;
        if (string.IsNullOrEmpty(path))
            return "0";
        string prefix = path.Substring(0, path.IndexOf(categoryId.ToString()) + categoryId.ToString().Length);
        DataRecordTable table = productCategoryBusiness.GetList("Id,category_name,order_by", "order_by", false, 1, 100, "path like '" + prefix + "%'");
        int count = table.Table.Rows.Count;
        DataRow row = null;
        string id = string.Empty;
        string ids = string.Empty;
        for (int i = 0; i < count; i++)
        {
            row = table.Table.Rows[i];
            id = row["Id"].ToString();
            if (i == count - 1)
                ids += id;
            else
                ids += id + ",";
        }
        return ids;
    }
}
