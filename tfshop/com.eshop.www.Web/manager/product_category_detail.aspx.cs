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

public partial class administrator_product_category_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFather_load();
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
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
                if (productCategoryBusiness.IsHasNext(id))
                {
                    btnNext.Enabled = false;
                    btnNext2.Enabled = false;
                }
                if (productCategoryBusiness.IsHasPrev(id))
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
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        ProductCategory productCategory = productCategoryBusiness.GetEntity(id);
        displayInfo(productCategory);
    }
    private void displayInfo(ProductCategory productCategory)
    {
        txtCategoryName.Text = productCategory.CategoryName;
        txtOrderBy.Text = productCategory.OrderBy.ToString();
        chkIsShow.Checked = productCategory.IsShow;
        txtRemark.Text = productCategory.Remark;
        foreach (ListItem item in ddlFather.Items)
        {
            string fatherId = item.Value.Split('|')[0];
            if (fatherId == productCategory.FatherId.ToString())
            {
                item.Selected = true;
                break;
            }
        }
        //if (productCategory.Image.Length > 0)
        //{
        //    imgImages.ImageUrl = "~/upload-file/images/product/" + productCategory.Image;
        //    hiddenImage.Value = productCategory.Image;
        //}
        //txtAlt.Text = productCategory.Alt;
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
            value = row["Id"].ToString() + "|" + row["path"].ToString();
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
            item = new ListItem(nodeText + "─" + row["category_name"].ToString(), row["Id"].ToString() + "|" + row["path"].ToString());
            ddlFather.Items.Add(item);
            CreateChild(row["Id"].ToString(), nodeText + "─", table, productCategoryBusiness);
        }
    }
    private bool Save()
    {
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();

        string categoryName = txtCategoryName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string remark = txtRemark.Text.Trim();
        //string image = hiddenImage.Value;
        //string alt = txtAlt.Text.Trim();
        int fatherId = int.Parse(ddlFather.SelectedValue.Split('|')[0]);
        string path = fatherId == 0 ? "" : ddlFather.SelectedValue.Split('|')[1];

        ProductCategory productCategory = new ProductCategory();
        productCategory.Alt = "";// alt;
        productCategory.Image = "";// image;
        productCategory.IsShow = isShow;
        productCategory.OrderBy = string.IsNullOrEmpty(orderBy) ? productCategoryBusiness.GetMaxOrder() : int.Parse(orderBy);
        productCategory.Remark = remark;
        productCategory.CategoryName = categoryName;
        productCategory.FatherId = fatherId;
        productCategory.Path = path;

        //if (productCategoryBusiness.IsHaveSameName(categoryName))
        //{
        //    JavascriptHelper.Alert("已经有相同的目录名，请输入另外一个目录名");
        //    return false;
        //}
        return productCategoryBusiness.Add(productCategory);
    }
    private bool Update()
    {
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        ProductCategory productCategory = productCategoryBusiness.GetEntity(id);

        string categoryName = txtCategoryName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string remark = txtRemark.Text.Trim();
        //string image = hiddenImage.Value;
        //string alt = txtAlt.Text.Trim();
        int fatherId = int.Parse(ddlFather.SelectedValue.Split('|')[0]);
        string path = fatherId == 0 ? id.ToString() : ddlFather.SelectedValue.Split('|')[1] + "," + id.ToString();

        //if (string.Compare(productCategory.CategoryName, categoryName, false) != 0)
        //{
        //    if (productCategoryBusiness.IsHaveSameName(categoryName))
        //    {
        //        JavascriptHelper.Alert("已经有相同的目录名，请输入另外一个目录名");
        //        return false;
        //    }
        //}

        productCategory.Id = id;
        productCategory.Alt = "";// alt;
        productCategory.Image = "";// image;
        productCategory.IsShow = isShow;
        productCategory.OrderBy = string.IsNullOrEmpty(orderBy) ? productCategoryBusiness.GetMaxOrder() : int.Parse(orderBy);
        productCategory.Remark = remark;
        productCategory.CategoryName = categoryName;
        productCategory.FatherId = fatherId;
        productCategory.Path = path;

        
        return productCategoryBusiness.Update(productCategory);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        if (productCategoryBusiness.IsHaveSonCategory(id))
        {
            JavascriptHelper.Alert("该目录下面有子目录，请删除子目录再删除该目录");
            return;
        }
        if (productDetailBusiness.IsHaveProductByCategory(id))
        {
            JavascriptHelper.Alert("该目录下面还有产品条目，请删除产品条目再删除该目录");
            return;
        }
        bool success = productCategoryBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("目录删除成功", "product_category.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtCategoryName.Text = "";
        txtOrderBy.Text = "";
        txtRemark.Text = "";
        //imgImages.ImageUrl = "image/no.jpg";
        //hiddenImage.Value = "";
        //txtAlt.Text = "";
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int nextId = productCategoryBusiness.Next(id);
        Response.Redirect("product_category_detail.aspx?Id=" + nextId + "&action=update");
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int prevId = productCategoryBusiness.Previous(id);
        Response.Redirect("product_category_detail.aspx?Id=" + prevId + "&action=update");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功", "product_category.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功", "product_category.aspx");
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
        Response.Redirect("product_category.aspx");
    }
}
