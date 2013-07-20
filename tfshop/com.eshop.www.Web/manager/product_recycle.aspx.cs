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
using System.Text;
using com.eshop.www.Model;
using com.eshop.www.BLL;
using System.Data;
using Wuqi.Webdiyer;

public partial class administrator_product_recycle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCategory_load();
            ddlBrand_load();
            rData_bind();
        }
    }
    private void rData_bind()
    {
        StringBuilder where = new StringBuilder();
        string prodouctName = Request.QueryString["productName"];
        string summary = Request.QueryString["summary"];
        string keywords = Request.QueryString["keywords"];
        string isShow = Request.QueryString["isShow"];
        string beginDate = Request.QueryString["beginDate"];
        string endDate = Request.QueryString["endDate"];
        string categoryId = Request.QueryString["categoryId"];
        string brandId = Request.QueryString["brandId"];

        DataRecordTable table = null;
        where.Append("is_delete=1");
        if (!string.IsNullOrEmpty(prodouctName))
            where.Append(" and product_name like '%" + prodouctName + "%'");
        if (!string.IsNullOrEmpty(keywords))
            where.Append(" and keywords like '%" + keywords + "%'");
        if (!string.IsNullOrEmpty(summary))
            where.Append(" and summary like '%" + summary + "%'");
        if (!string.IsNullOrEmpty(isShow))
            where.Append(" and is_show=" + isShow + "");
        if (!string.IsNullOrEmpty(beginDate))
            where.Append(" and create_date>'" + beginDate + "'");
        if (!string.IsNullOrEmpty(endDate))
            where.Append(" and create_date<'" + endDate + "'");
        if (!string.IsNullOrEmpty(categoryId))
        {
            ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
            string path = productCategoryBusiness.GetEntity(int.Parse(categoryId)).Path;
            string prefix = path.Substring(0, path.IndexOf(categoryId) + categoryId.Length);
            table = productCategoryBusiness.GetList("Id,category_name", "order_by", false, 1, 100, "path like '" + prefix + "%'");
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
            where.Append(" and category_id in (" + ids + ")");
        }
        if (!string.IsNullOrEmpty(brandId))
        {
            
            where.Append(" and brand_id="+brandId);
        }
        string fieldList = "Id,product_name,order_by,is_recommend,is_new,is_hot,is_discount,is_show,category_id,brand_id,create_date";
        string orderField = "order_by";
        bool orderBy = true;
        table = new ProductDetailBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
        rData.DataSource = table.Table;
        rData.DataBind();
        AspNetPager1.RecordCount = table.RecordCount;
        AspNetPager2.RecordCount = table.RecordCount;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        AspNetPager page = (AspNetPager)sender;
        AspNetPager2.CurrentPageIndex = page.CurrentPageIndex;
        rData_bind();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        AspNetPager page = (AspNetPager)sender;
        AspNetPager1.CurrentPageIndex = page.CurrentPageIndex;
        rData_bind();
    }
    protected void rData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName;
        int id = int.Parse(e.CommandArgument.ToString());
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        switch (cmd)
        {
            case "recovery":
                productDetailBusiness.UpdateDelete(new ProductDetail() { Id = id, IsDelete = false });
                break;
            case "delete":
                productDetailBusiness.Delete(id);
                break;
        }
        rData_bind();
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                productDetailBusiness.Delete(Id);
            }
        }
        rData_bind();
    }
    protected void btnRecovery_Click(object sender, EventArgs e)
    {
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        CheckBox chkselect = null;
        ProductDetail productDetail = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                productDetail = new ProductDetail() { Id = Id, IsDelete = false };
                productDetailBusiness.UpdateDelete(productDetail);
            }
        }
        rData_bind();
    }
    public string GetCategoryName(int categoryId)
    {
        string categoryName = new ProductCategoryBusiness().GetEntity(categoryId).CategoryName;
        return string.IsNullOrEmpty(categoryName) ? "" : categoryName;
    }
    public string GetBrandName(int brandId)
    {
        string brandName = new ProductBrandBusiness().GetEntity(brandId).BrandName;
        return string.IsNullOrEmpty(brandName) ? "" : brandName;
    }
    private void ddlCategory_load()
    {
        ddlCategory.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlCategory.Items.Add(item);

        DataRecordTable drt = new ProductCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, "father_Id=0");
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow dr in drt.Table.Rows)
        {
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();

            item = new ListItem("╋" + text, value);
            ddlCategory.Items.Add(item);

            createChild(value, "├");
        }

    }
    private void createChild(string fatherId, string prefix)
    {
        string where = "father_Id=" + fatherId;
        DataRecordTable drt = new ProductCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, where);
        ListItem item = null;
        string text = string.Empty;
        string value = string.Empty;
        prefix += "─";
        foreach (DataRow dr in drt.Table.Rows)
        {
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();

            item = new ListItem(prefix + text, value);
            ddlCategory.Items.Add(item);
            createChild(value, prefix);
        }
    }
    private void ddlBrand_load()
    {
        ddlBrand.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlBrand.Items.Add(item);

        DataRecordTable drt = new ProductBrandBusiness().GetList("Id,brand_name,order_by", "order_by", true, 1, 100, "");
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow dr in drt.Table.Rows)
        {
            text = dr["brand_name"].ToString();
            value = dr["Id"].ToString();

            item = new ListItem(text, value);
            ddlBrand.Items.Add(item);
        }
    }
}
