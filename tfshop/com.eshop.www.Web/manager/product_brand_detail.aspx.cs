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
using com.eshop.www.Model;
using com.eshop.www.BLL;
using com.eshop.www.Tools;
using System.IO;
using System.Data;

public partial class back_stage_product_brand_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFather_load();
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
                int id = int.Parse(Request.QueryString["Id"]);
                btnReset.Visible = false;
                btnDelete.Visible = true;
                btnSave.Visible = true;
                btnNext.Visible = true;
                btnPrev.Visible = true;

                btnReset2.Visible = false;
                btnDelete2.Visible = true;
                btnSave2.Visible = true;
                btnNext2.Visible = true;
                btnPrev2.Visible = true;
                if (productBrandBusiness.IsHasNext(id))
                {
                    btnNext.Enabled = false;
                    btnNext2.Enabled = false;
                }
                if (productBrandBusiness.IsHasPrev(id))
                {
                    btnPrev.Enabled = false;
                    btnPrev2.Enabled = false;
                }
                DataRead();
            }
            else
            {
                btnReset.Visible = true;
                btnDelete.Visible = false;
                btnSave.Visible = true;
                btnNext.Visible = false;
                btnPrev.Visible = false;

                btnReset2.Visible = true;
                btnDelete2.Visible = false;
                btnSave2.Visible = true;
                btnNext2.Visible = false;
                btnPrev2.Visible = false;
            }
        }
    }

    private void DataRead()
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        ProductBrand productBrand = productBrandBusiness.GetEntity(id);
        displayInfo(productBrand);
    }
    private void displayInfo(ProductBrand productBrand)
    {
        txtBrandName.Text = productBrand.BrandName;
        txtOrderBy.Text = productBrand.OrderBy.ToString();
        chkIsShow.Checked = productBrand.IsShow;
        txtRemark.Text = productBrand.Remark;
        //if (productBrand.Image.Length > 0)
        //{
        //    imgImages.ImageUrl = "~/upload-file/images/product/" + productBrand.Image;
        //    hiddenImage.Value = productBrand.Image;
        //}
        //txtAlt.Text = productBrand.Alt;
        ddlFather.SelectedValue = productBrand.CategoryId.ToString();
    }
    
    private bool Save()
    {
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();

        string brandName = txtBrandName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string remark = txtRemark.Text.Trim();
        //string image = hiddenImage.Value;
        //string alt = txtAlt.Text.Trim();
        int categoryId = int.Parse(ddlFather.SelectedValue);

        ProductBrand productBrand = new ProductBrand();
        productBrand.Alt = "";// alt;
        productBrand.Image = "";// image;
        productBrand.IsShow = isShow;
        productBrand.OrderBy = string.IsNullOrEmpty(orderBy) ? productBrandBusiness.GetMaxOrder() : int.Parse(orderBy);
        productBrand.Remark = remark;
        productBrand.BrandName = brandName;
        productBrand.CategoryId = categoryId;

        if (productBrandBusiness.IsHaveSameName(brandName))
        {
            JavascriptHelper.Alert("已经有相同的品牌名，请输入另外一个目录名");
            return false;
        }
        return productBrandBusiness.Add(productBrand);
    }
    private bool Update()
    {
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        ProductBrand productBrand = productBrandBusiness.GetEntity(id);

        string brandName = txtBrandName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string remark = txtRemark.Text.Trim();
        //string image = hiddenImage.Value;
        //string alt = txtAlt.Text.Trim();
        int categoryId = int.Parse(ddlFather.SelectedValue);

        if (string.Compare(productBrand.BrandName, brandName, false) != 0)
        {
            if (productBrandBusiness.IsHaveSameName(brandName))
            {
                JavascriptHelper.Alert("已经有相同的品牌名，请输入另外一个目录名");
                return false;
            }
        }

        productBrand.Id = id;
        productBrand.Alt = "";// alt;
        productBrand.Image = "";// image;
        productBrand.IsShow = isShow;
        productBrand.OrderBy = string.IsNullOrEmpty(orderBy) ? productBrandBusiness.GetMaxOrder() : int.Parse(orderBy);
        productBrand.Remark = remark;
        productBrand.BrandName = brandName;
        productBrand.CategoryId = categoryId;
        
        return productBrandBusiness.Update(productBrand);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();

        if (productDetailBusiness.IsHaveProductByBrand(id))
        {
            JavascriptHelper.Alert("该品牌下面还有产品条目，请删除产品条目再删除该品牌");
            return;
        }
        bool success = productBrandBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("品牌删除成功", "product_brand.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtBrandName.Text = "";
        txtOrderBy.Text = "";
        txtRemark.Text = "";
        //imgImages.ImageUrl = "image/no.jpg";
        //hiddenImage.Value = "";
        //txtAlt.Text = "";
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int nextId = productBrandBusiness.Next(id);
        Response.Redirect("product_brand_detail.aspx?Id=" + nextId + "&action=update");
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int prevId = productBrandBusiness.Previous(id);
        Response.Redirect("product_brand_detail.aspx?Id=" + prevId + "&action=update");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","product_brand.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功", "product_brand.aspx");
                
            }
        }
    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    string ext = Path.GetExtension(FileUpload1.FileName);
    //    string newFileStr = StringHelper.GetRandomString(8);
    //    string newFileName = newFileStr + ext;
    //    string path = "~/upload-file/images/product/";
    //    string absolutPath = Server.MapPath(path);
    //    FileUpload1.SaveAs(absolutPath + newFileName);
    //    imgImages.ImageUrl = path + newFileName;
    //    hiddenImage.Value = newFileName;
    //}
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_brand.aspx");
    }
    private void ddlFather_load()
    {
        ddlFather.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlFather.Items.Add(item);

        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        DataRecordTable table = productCategoryBusiness.GetList("Id,category_name,path,order_by", "order_by", false, 1, 50, "father_id=0");
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            text = "╋" + row["category_name"].ToString();
            value = row["Id"].ToString();
            item = new ListItem(text, value);

            ddlFather.Items.Add(item);
            CreateChild(value, "├", table, productCategoryBusiness);
        }
    }
    private void CreateChild(string parentId, string nodeText, DataRecordTable table, ProductCategoryBusiness productCategoryBusiness)
    {
        table = productCategoryBusiness.GetList("Id,category_name,path,order_by", "order_by", false, 1, 50, "father_id=" + parentId);
        ListItem item = null;
        foreach (DataRow row in table.Table.Rows)
        {
            item = new ListItem(nodeText + "─" + row["category_name"].ToString(), row["Id"].ToString());
            ddlFather.Items.Add(item);
            CreateChild(row["Id"].ToString(), nodeText + "─", table, productCategoryBusiness);
        }
    }
}
