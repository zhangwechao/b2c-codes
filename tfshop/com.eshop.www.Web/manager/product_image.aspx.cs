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
using com.eshop.www.Tools;
using System.IO;
using com.eshop.www.Model;
using com.eshop.www.BLL;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class back_stage_product_image : System.Web.UI.Page
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
        int productId = int.Parse(Request.QueryString["productId"]);
        ProductImageBusiness business = new ProductImageBusiness();
        //读取列表图片
        DataRecordTable table = new ProductImageBusiness().GetList("Id,image,alt", "Id", false, 1, 1, "product_id="+productId+" and type=1");
        if (table.Table.Rows.Count > 0)
        {
            string image = table.Table.Rows[0]["image"].ToString();
            string alt = table.Table.Rows[0]["alt"].ToString();
            string Id = table.Table.Rows[0]["Id"].ToString();
            if (image.Length > 0)
            {
                imgImages.ImageUrl = "~/upload-file/images/product/" + image;
                hiddenImage.Value = image;
            }
            txtAlt.Text = alt;
            //将列表图片的id号放在隐藏字段中
            hiddenId.Value = Id;
        }
        table = business.GetList("Id,product_id,image,alt,zoom_image,type,is_default","Id",false,1,100,"product_id="+productId+" and type=2");
        List<ProductImage> imageList = null;
        //在viewState放一个productImage的列表，productImage必须序列化
        if (ViewState["imageList"] == null)
            imageList = new List<ProductImage>();
        else
            imageList = ViewState["imageList"] as List<ProductImage>;

        ProductImage productImage = null;
        //取出列表的图片，并放入列表中
        foreach (DataRow row in table.Table.Rows)
        {
            productImage = new ProductImage();
            productImage.ProductId = productId;
            productImage.Alt = row["alt"].ToString();
            productImage.Image = row["image"].ToString();
            productImage.IsDefault = bool.Parse(row["is_default"].ToString());
            productImage.Type = int.Parse(row["type"].ToString());
            productImage.ZoomImage = row["zoom_image"].ToString();
            productImage.Id = int.Parse(row["Id"].ToString());
            //将默认的列表页的图片显示在大图中,并且放在隐藏字段中,该隐藏字段的值通过前台js改变
            if (productImage.IsDefault)
            {
                if (productImage.Image.Length > 0)
                {
                    imgDetail.Src = "/upload-file/images/product/" + productImage.Image;
                    hiddenDefaultImage.Value = productImage.Image;
                }
            }
            imageList.Add(productImage);
        }
        ViewState["imageList"] = imageList;
        dlImageBind();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.FileName.Length == 0)
        {
            JavascriptHelper.Alert("请选择上传图片");
            return;
        }
        string ext = Path.GetExtension(FileUpload1.FileName);
        string newFileStr = StringHelper.GetRandomString(8);
        string newFileName = newFileStr + ext;
        string path = "~/upload-file/images/product/";
        string absolutPath = Server.MapPath(path);
        FileUpload1.SaveAs(absolutPath + newFileName);
        imgImages.ImageUrl = path + newFileName;
        hiddenImage.Value = newFileName;
    }
    protected void btnDetailUpload_Click(object sender, EventArgs e)
    {
        ProductImage productImage = new ProductImage();

        if (fuDetail.FileName.Length > 0 && fuZoom.FileName.Length > 0)
        {
            string ext = Path.GetExtension(fuDetail.FileName);
            string newFileStr = StringHelper.GetRandomString(8);
            string newFileName = newFileStr + ext;
            string path = "~/upload-file/images/product/";
            string absolutPath = Server.MapPath(path);
            fuDetail.SaveAs(absolutPath + newFileName);
            productImage.Image = newFileName;

            ext = Path.GetExtension(fuZoom.FileName);
            newFileStr = StringHelper.GetRandomString(8);
            newFileName = newFileStr + ext;
            absolutPath = Server.MapPath(path);
            fuZoom.SaveAs(absolutPath + newFileName);
            productImage.ZoomImage = newFileName;
        }
        else
        {
            JavascriptHelper.Alert("详细页图片必须和放大图一起上传");
            return;
        }

        if (!string.IsNullOrEmpty(productImage.Image))
        {
            if (string.IsNullOrEmpty(productImage.ZoomImage))
                productImage.ZoomImage = "";
            productImage.Alt = txtDetailAlt.Text.Trim();
            productImage.Type = 2;
            productImage.ProductId = int.Parse(Request.QueryString["productId"]);
            productImage.IsDefault = false;
            productImage.Id = 0;

            List<ProductImage> imageList = null;
            if (ViewState["imageList"] == null)
            {
                imageList = new List<ProductImage>();
                imageList.Add(productImage);
            }
            else
            {
                imageList = ViewState["imageList"] as List<ProductImage>;
                imageList.Add(productImage);
            }
            ViewState["imageList"] = imageList;
            dlImageBind();
           
        }

    }
    private void dlImageBind()
    {
        if (ViewState["imageList"] != null)
        {
            List<ProductImage> imageList = ViewState["imageList"] as List<ProductImage>;
            if (imageList != null)
            {
                dlImages.DataSource = imageList;
                dlImages.DataBind();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int productId = int.Parse(Request.QueryString["productId"]);
        ProductImage productImage = new ProductImage();
        ProductImageBusiness business = new ProductImageBusiness();
        productImage.Image = hiddenImage.Value;
        productImage.IsDefault = false;
        productImage.ProductId = productId;
        productImage.Alt = txtAlt.Text.Trim();
        productImage.Type = 1;
        productImage.ZoomImage = "";
        productImage.Id = int.Parse(hiddenId.Value);
        if (productImage.Id == 0)
        {
            if (string.IsNullOrEmpty(productImage.Image))
            {
                JavascriptHelper.Alert("请上传列表页图片");
                return;
            }
            business.Add(productImage);
        }
        else
            business.Update(productImage);

        //得到隐藏字段的值，为选择的默认详细页图片，该隐藏值通过前台js改变
        string defaultImage = hiddenDefaultImage.Value;
        if (ViewState["imageList"] != null)
        {
            List<ProductImage> imageList = ViewState["imageList"] as List<ProductImage>;
            bool flag = false;
            foreach(ProductImage p in imageList)
            {
                if (p.Image == defaultImage)
                {
                    p.IsDefault = true;
                    flag = true;
                }
                else
                {
                    p.IsDefault = false;
                    flag = false;
                }
                if (!flag)
                {
                    JavascriptHelper.Alert("至少选择一张详细页默认图片");
                    return;
                }

                if (p.Id == 0)
                    business.Add(p);
                else
                    business.Update(p);
            }
        }
        JavascriptHelper.Alert("保存信息成功");
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_content.aspx");
    }
    protected void dlImages_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        string image = e.CommandArgument.ToString();
        ProductImageBusiness business = new ProductImageBusiness();
        if (cmdName == "delete")
        {
            List<ProductImage> imageList = ViewState["imageList"] as List<ProductImage>;
            foreach (ProductImage productImage in imageList)
            {
                if (productImage.Image == image)
                {
                    imageList.Remove(productImage);
                    business.Delete(productImage.Id);
                    FileHelper.DeleteFile(Server.MapPath("~/upload-file/images/product/"+image));
                    break;
                }
            }
            ViewState["imageList"] = imageList;
            dlImageBind();
        }
    }
    protected void dlImages_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        bool isDefault = bool.Parse(DataBinder.Eval(e.Item.DataItem, "IsDefault").ToString());
        HtmlInputCheckBox chkSetDefault = e.Item.FindControl("chkSetDefault") as HtmlInputCheckBox;
        if (chkSetDefault != null && isDefault)
            chkSetDefault.Checked = true;
    }
}
