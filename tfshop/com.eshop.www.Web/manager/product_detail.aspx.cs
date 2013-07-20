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
using com.eshop.www.Tools;
using System.Collections.Generic;
using System.IO;

public partial class manager_product_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCategory_load();
            string action = Request.QueryString["action"];
            if (action == "update")
                UpdateDisplay();
            else
                NoUpdateDisplay();
        }
    }
    //更新时显示内容
    private void UpdateDisplay()
    {
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
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
        if (productDetailBusiness.IsHasNext(id))
        {
            btnNext.Enabled = false;
            btnNext2.Enabled = false;
        }
        if (productDetailBusiness.IsHasPrev(id))
        {
            btnPrev.Enabled = false;
            btnPrev2.Enabled = false;
        }
        DataRead();
    }
    //不是更新时显示内容
    private void NoUpdateDisplay()
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
    //读取数据
    private void DataRead()
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        ProductDetail productDetail = productDetailBusiness.GetEntity(id);
        displayInfo(productDetail);
    }
    //显示数据
    private void displayInfo(ProductDetail productDetail)
    {
        txtProductName.Text = productDetail.ProductName;
        txtOrderBy.Text = productDetail.OrderBy.ToString();

        CategoryDisplay(productDetail.CategoryId);
        ddlBrand.SelectedValue = productDetail.BrandId.ToString();

        chkIsShow.Checked = productDetail.IsShow;
        chkIsRecomment.Checked = productDetail.IsRecommend;
        chkIsNew.Checked = productDetail.IsNew;
        chkIsHot.Checked = productDetail.IsHot;
        chkIsDiscount.Checked = productDetail.IsDiscount;
        chkIsComment.Checked = productDetail.IsComment;

        txtKeywords.Text = productDetail.Keywords;
        txtSummary.Text = productDetail.Summary;
        txtContent.Value = productDetail.Description;

        txtPrice.Text = productDetail.Price.ToString("0.0");
        txtSalePrice.Text = productDetail.SalePrice.ToString("0.0");
        txtIntegral.Text = productDetail.integral.ToString();

        txtSaleNumber.Text = productDetail.SaleNumber.ToString();
        txtStock.Text = productDetail.Stock.ToString();

        ImageDisplay(productDetail.Id);
    }
    //显示图片
    private void ImageDisplay(int productId)
    {
        ProductImageBusiness productImageBusiness =  new ProductImageBusiness();
        ProductImage productImage = productImageBusiness.GetEntity(productId,1);
        hiddenImageId.Value = productImage.Id.ToString();
        if (productImage.Image.Length > 0)
        {
            imgImages.ImageUrl = "~/upload-file/images/product/"+productImage.Image;
            hiddenImage.Value = productImage.Image;
            aImageUrl.HRef = "/upload-file/images/product/"+productImage.Image;
        }

        string where = "product_id="+productId+" and type=2";
        string fieldList = "Id,product_id,image,alt,zoom_image,type,is_default";
        string orderField = "Id";
        bool orderBy = false;
        DataRecordTable table = new ProductImageBusiness().GetList(fieldList,orderField,orderBy,1,20,where);
        productImage = null;
        
        foreach (DataRow row in table.Table.Rows)
        {
            productImage = new ProductImage();
            productImage.Id = int.Parse(row["Id"].ToString());
            productImage.ProductId = productId;
            productImage.Image = row["image"].ToString();
            productImage.IsDefault = bool.Parse(row["is_default"].ToString());
            productImage.Type = int.Parse(row["type"].ToString());
            productImage.ZoomImage = row["zoom_image"].ToString();
            AddProductImageToViewState(productImage);
        }
        rProductImage_load();

    }
    //显示目录
    private void CategoryDisplay(int categoryId)
    {
        ProductCategory productCategory = new ProductCategoryBusiness().GetEntity(categoryId);
        if (!string.IsNullOrEmpty(productCategory.Path))
        {
            string[] categoryStrs = productCategory.Path.Split(new char[] { ',' });
            int firstCategoryId = 0;
            if (categoryStrs.Length == 3)
            {
                //加载第三类别并显示
                ddlThirhCategory_load(categoryStrs[1]);
                ddlThirdCategory.SelectedValue = categoryId.ToString();
                //加载第二类别并显示
                ddlSecondCategory_load(categoryStrs[0]);
                ddlSecondCategory.SelectedValue = categoryStrs[1];
                //显示第一类别
                ddlCategory.SelectedValue = categoryStrs[0];

                firstCategoryId = int.Parse(categoryStrs[0]);
            }
            if (categoryStrs.Length == 2)
            {
                ddlSecondCategory_load(categoryStrs[0]);
                ddlSecondCategory.SelectedValue = categoryStrs[1];

                ddlCategory.SelectedValue = categoryStrs[0];

                firstCategoryId = int.Parse(categoryStrs[0]);
            }
            if (categoryStrs.Length == 1)
            {
                ddlCategory.SelectedValue = categoryStrs[0];
                firstCategoryId = int.Parse(categoryStrs[0]);
            }

            ddlBrand_load(firstCategoryId.ToString());
        }
        
    }
    //得到上级目录不ID 
    private int GetFatherId(int categoryId)
    {
        return new ProductCategoryBusiness().GetEntity(categoryId).FatherId;
    }
    //保存数据
    private bool Save()
    {
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();

        string productName = txtProductName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();

        int categoryId = 0;
        if(ddlCategory.SelectedValue!="0")
            categoryId = int.Parse(ddlCategory.SelectedValue);
        if (ddlSecondCategory.SelectedValue != "0")
            categoryId = int.Parse(ddlSecondCategory.SelectedValue);
        if (ddlThirdCategory.SelectedValue != "0")
            categoryId = int.Parse(ddlThirdCategory.SelectedValue);

        int brandId = int.Parse(ddlBrand.SelectedValue);
        bool isShow = chkIsShow.Checked;
        bool isRecommend = chkIsRecomment.Checked;
        bool isComment = chkIsComment.Checked;
        bool isNew = chkIsNew.Checked;
        bool isHot = chkIsHot.Checked;
        bool IsDiscount = chkIsDiscount.Checked;
        string keywords = txtKeywords.Text.Trim();
        string summary = txtSummary.Text.Trim();
        string description = txtContent.Value;
        string price = txtPrice.Text.Trim();
        string salePrice = txtSalePrice.Text.Trim();
        string integral = txtIntegral.Text.Trim();
        string saleNumber = txtSaleNumber.Text.Trim();
        string stock = txtStock.Text.Trim();

        ProductDetail productDetail = new ProductDetail();
        productDetail.BrandId = brandId;
        productDetail.Description = description;
        productDetail.HtmlName = "";
        productDetail.ClickNumber = 0;
        productDetail.IsShow = isShow;
        productDetail.Keywords = keywords;
        productDetail.OrderBy = string.IsNullOrEmpty(orderBy) ? productDetailBusiness.GetMaxOrder() : int.Parse(orderBy);
        productDetail.Summary = summary;
        productDetail.ProductName = productName;
        productDetail.CategoryId = categoryId;
        productDetail.IsComment = isComment;
        productDetail.IsDelete = false;
        productDetail.IsRecommend = isRecommend;
        productDetail.HtmlName = "";
        if (integral.Length > 0)
            productDetail.integral = int.Parse(integral);
        else
            productDetail.integral = 0;
        productDetail.IsDiscount = IsDiscount;
        productDetail.IsHot = isHot;
        productDetail.IsNew = isNew;
        if (price.Length > 0)
            productDetail.Price = float.Parse(price);
        else
            productDetail.Price = 0;
        if (salePrice.Length > 0)
            productDetail.SalePrice = float.Parse(salePrice);
        else
            productDetail.SalePrice = 0;

        if (saleNumber.Length > 0)
            productDetail.SaleNumber = int.Parse(saleNumber);
        else
            productDetail.SaleNumber = 0;

        if (stock.Length > 0)
            productDetail.Stock = int.Parse(stock);
        else
            productDetail.Stock = 0;

        int productId = productDetailBusiness.Add(productDetail);
        SaveImage(productId);
        return true;
    }
    private void SaveImage(int productId)
    {
        ProductImage productImage = new ProductImage();
        productImage.Image = hiddenImage.Value;
        productImage.IsDefault = false;
        productImage.ProductId = productId;
        productImage.Type = 1;
        productImage.ZoomImage = "";
        new ProductImageBusiness().Add(productImage);
        SaveProductImageByViewState(productId);
        
    }
    //更新数据
    private bool Update()
    {
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        ProductDetail productDetail = productDetailBusiness.GetEntity(id);

        string productName = txtProductName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();

        int categoryId = 0;
        if (ddlCategory.SelectedValue != "0")
            categoryId = int.Parse(ddlCategory.SelectedValue);
        if (ddlSecondCategory.SelectedValue != "0")
            categoryId = int.Parse(ddlSecondCategory.SelectedValue);
        if (ddlThirdCategory.SelectedValue != "0")
            categoryId = int.Parse(ddlThirdCategory.SelectedValue);

        int brandId = int.Parse(ddlBrand.SelectedValue);
        bool isShow = chkIsShow.Checked;
        bool isRecommend = chkIsRecomment.Checked;
        bool isComment = chkIsComment.Checked;
        bool isNew = chkIsNew.Checked;
        bool isHot = chkIsHot.Checked;
        bool IsDiscount = chkIsDiscount.Checked;
        string keywords = txtKeywords.Text.Trim();
        string summary = txtSummary.Text.Trim();
        string description = txtContent.Value;
        string price = txtPrice.Text.Trim();
        string salePrice = txtSalePrice.Text.Trim();
        string integral = txtIntegral.Text.Trim();
        string saleNumber = txtSaleNumber.Text.Trim();
        string stock = txtStock.Text.Trim();

        productDetail.BrandId = brandId;
        productDetail.Description = description;
        productDetail.HtmlName = "";
        productDetail.ClickNumber = 0;
        productDetail.IsShow = isShow;
        productDetail.Keywords = keywords;
        productDetail.OrderBy = string.IsNullOrEmpty(orderBy) ? productDetailBusiness.GetMaxOrder() : int.Parse(orderBy);
        productDetail.Summary = summary;
        productDetail.ProductName = productName;
        productDetail.CategoryId = categoryId;
        productDetail.IsComment = isComment;
        productDetail.IsDelete = false;
        productDetail.IsRecommend = isRecommend;
        if (integral.Length > 0)
            productDetail.integral = int.Parse(integral);
        else
            productDetail.integral = 0;
        productDetail.IsDiscount = IsDiscount;
        productDetail.IsHot = isHot;
        productDetail.IsNew = isNew;
        if (price.Length > 0)
            productDetail.Price = float.Parse(price);
        else
            productDetail.Price = 0;
        if (salePrice.Length > 0)
            productDetail.SalePrice = float.Parse(salePrice);
        else
            productDetail.SalePrice = 0;

        if (saleNumber.Length > 0)
            productDetail.SaleNumber = int.Parse(saleNumber);
        else
            productDetail.SaleNumber = 0;

        if (stock.Length > 0)
            productDetail.Stock = int.Parse(stock);
        else
            productDetail.Stock = 0;

        productDetailBusiness.Update(productDetail);
        UpdateImage(productDetail.Id);
        return true;
    }
    private void UpdateImage(int productId)
    {
        ProductImageBusiness productImageBusiness = new ProductImageBusiness();
        int Id = int.Parse(hiddenImageId.Value);
        ProductImage productImage = productImageBusiness.GetEntity(Id);
        productImage.Image = hiddenImage.Value;
        productImageBusiness.Update(productImage);
        SaveProductImageByViewState(productId);
    }
    //删除按钮事件
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        bool success = productDetailBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("删除成功", "product_content.aspx");
    }
    //重置
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtProductName.Text = "";
        txtOrderBy.Text = "";
        txtKeywords.Text = "";
        txtSummary.Text = "";
        txtContent.Value = "";
        txtPrice.Text = "";
        txtSalePrice.Text = "";
        txtIntegral.Text = "";
    }
    //下一条
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int nextId = productDetailBusiness.Next(id);
        Response.Redirect("product_detail.aspx?Id=" + nextId + "&action=update");
    }
    //上一条
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int prevId = productDetailBusiness.Previous(id);
        Response.Redirect("product_detail.aspx?Id=" + prevId + "&action=update");
    }
    //保存事件
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","product_content.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功","product_content.aspx");
            }
        }
    }
    //返回事件
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_content.aspx");
    }
    //一级目录加载
    private void ddlCategory_load()
    {
        ddlCategory.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlCategory.Items.Add(item);

        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        DataRecordTable table = productCategoryBusiness.GetList("Id,category_name,order_by", "order_by", false, 1, 50, "father_id=0");
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            text = row["category_name"].ToString();
            value = row["Id"].ToString();
            item = new ListItem(text, value);
            ddlCategory.Items.Add(item);
        }
    }
    //一级目录选择索引切换事件
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string categoryId = ((DropDownList)sender).SelectedValue;
        ddlBrand_load(categoryId);
        ddlSecondCategory_load(categoryId);
    }
    //二级目录选择索引切换事件
    protected void ddlSecondCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string categoryId = ((DropDownList)sender).SelectedValue;
        ddlThirhCategory_load(categoryId);
    }
    //三级目录加载
    private void ddlThirhCategory_load(string categoryId)
    {
        ddlThirdCategory.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlThirdCategory.Items.Add(item);

        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        DataRecordTable table = productCategoryBusiness.GetList("Id,category_name,order_by", "order_by", false, 1, 50, "father_id=" + categoryId);
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            text = row["category_name"].ToString();
            value = row["Id"].ToString();
            item = new ListItem(text, value);
            ddlThirdCategory.Items.Add(item);
        }
    }
    //二级目录加载
    private void ddlSecondCategory_load(string categoryId)
    {
        ddlSecondCategory.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlSecondCategory.Items.Add(item);

        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        DataRecordTable table = productCategoryBusiness.GetList("Id,category_name,order_by", "order_by", false, 1, 50, "father_id=" + categoryId);
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            text = row["category_name"].ToString();
            value = row["Id"].ToString();
            item = new ListItem(text, value);
            ddlSecondCategory.Items.Add(item);
        }
    }
    //品牌加载
    private void ddlBrand_load(string categoryId)
    {
        ddlBrand.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlBrand.Items.Add(item);

        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        DataRecordTable table = productBrandBusiness.GetList("Id,brand_name,order_by", "order_by", false, 1, 50, "category_id=" + categoryId);
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            text = row["brand_name"].ToString();
            value = row["Id"].ToString();
            item = new ListItem(text, value);
            ddlBrand.Items.Add(item);
        }
    }

    //添加ProductImage对象到ViewState,ProductImage必须序列化
    private void AddProductImageToViewState(ProductImage productImage)
    {
        List<ProductImage> list = GetProductImageListByViewState();
        list.Add(productImage);
        ViewState["productImage"] = list;
    }
    //通过文件名删除viewState里面的productImage
    private void DeleteProductImageByViewState(string fileName)
    {
        ProductImageBusiness productImageBusiness = new ProductImageBusiness();
        List<ProductImage> list = GetProductImageListByViewState();

        foreach (ProductImage productImage in list)
        {
            if (productImage.Image.ToLower() == fileName.ToLower())
            {
                list.Remove(productImage);
                FileHelper.DeleteFile(Server.MapPath("~/upload-file/images/product/" + productImage.Image));
                FileHelper.DeleteFile(Server.MapPath("~/upload-file/images/product/" + productImage.ZoomImage));
                if (productImage.Id != 0)
                    productImageBusiness.Delete(productImage.Id);
                break;
            }
        }
        ViewState["productImage"] = list;
    }
    //得到ViewState中的List<ProductImage>
    private List<ProductImage> GetProductImageListByViewState()
    {
        List<ProductImage> list = null;
        if (ViewState["productImage"] == null)
            list = new List<ProductImage>();
        else
            list = ViewState["productImage"] as List<ProductImage>;
        return list;
    }
    //绑定ViewState中的List<ProductImage>到rProductImage
    private void rProductImage_load()
    {
        List<ProductImage> list = GetProductImageListByViewState();
        rProductImage.DataSource = list;
        rProductImage.DataBind();
    }
    //保存ViewState中的productImage对象到数据库
    private void SaveProductImageByViewState(int Id)
    {
        List<ProductImage> list = GetProductImageListByViewState();
        ProductImageBusiness productImageBusiness = new ProductImageBusiness();
        foreach (ProductImage productImage in list)
        {
            if (productImage.Id == 0)
            {
                productImage.ProductId = Id;
                productImageBusiness.Add(productImage);
            }
        }
    }
    protected void btnUploadDetail_Click(object sender, EventArgs e)
    {
        if (fuDetailImage.FileName.Length == 0)
        {
            JavascriptHelper.Alert("请选择详细页图片上传");
            return;
        }
        if (fuDetailZoomImage.FileName.Length == 0)
        {
            JavascriptHelper.Alert("请选择详细页放大图片上传");
            return;
        }

        ProductImage productImage = new ProductImage();
        productImage.Id = 0;
        productImage.ProductId = 0;
        productImage.Type = 2;
        
        string ext = Path.GetExtension(fuDetailImage.FileName);
        string newFileStr = StringHelper.GetRandomString(8);
        string newFileName = newFileStr + ext;
        string path = "~/upload-file/images/product/";
        string absolutPath = Server.MapPath(path);
        fuDetailImage.SaveAs(absolutPath + newFileName);
        productImage.Image = newFileName;

        ext = Path.GetExtension(fuDetailZoomImage.FileName);
        newFileStr = StringHelper.GetRandomString(8);
        newFileName = newFileStr + ext;
        path = "~/upload-file/images/product/";
        absolutPath = Server.MapPath(path);
        fuDetailZoomImage.SaveAs(absolutPath + newFileName);
        productImage.ZoomImage = newFileName;
        productImage.IsDefault = chkIsDefault.Checked;

        AddProductImageToViewState(productImage);
        rProductImage_load();

    }
    protected void btnUploadList_Click(object sender, EventArgs e)
    {
        if (fuUploadList.FileName.Length == 0)
        {
            JavascriptHelper.Alert("请选择上传图片");
            return;
        }
        string ext = Path.GetExtension(fuUploadList.FileName);
        string newFileStr = StringHelper.GetRandomString(8);
        string newFileName = newFileStr + ext;
        string path = "~/upload-file/images/product/";
        string absolutPath = Server.MapPath(path);
        fuUploadList.SaveAs(absolutPath + newFileName);
        imgImages.ImageUrl = path + newFileName;
        if (hiddenImage.Value.Length > 0)
            FileHelper.DeleteFile(Server.MapPath(path+hiddenImage.Value));
        hiddenImage.Value = newFileName;
    }

    protected void rProductImage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        string imageFileName = e.CommandArgument.ToString();
        if (cmdName == "delete")
        {
            DeleteProductImageByViewState(imageFileName);
            rProductImage_load();
        }
    }
}
