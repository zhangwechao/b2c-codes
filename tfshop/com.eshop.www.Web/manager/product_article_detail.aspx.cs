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
using com.eshop.www.BLL;
using com.eshop.www.Model;
using com.eshop.www.Tools;
using System.IO;

public partial class back_stage_product_article_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                ProductArticleBusiness business = new ProductArticleBusiness();
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
                if (business.IsHasNext(id))
                {
                    btnNext.Enabled = false;
                    btnNext2.Enabled = false;
                }
                if (business.IsHasPrev(id))
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
        ProductArticleBusiness productArticleBusiness = new ProductArticleBusiness();
        ProductArticle productArticle = productArticleBusiness.GetEntity(id);
        displayInfo(productArticle);
    }
    private void displayInfo(ProductArticle productArticle)
    {
        txtTitle.Text = productArticle.Title;
        txtOrderBy.Text = productArticle.OrderBy.ToString();
        chkIsShow.Checked = productArticle.IsShow;
        txtKeywords.Text = productArticle.Keywords;
        txtSummary.Text = productArticle.Summary;
        txtContent.Value = productArticle.Content;
        imgImages.ImageUrl = "~/upload-file/images/product/" + productArticle.Image;
        hiddenImage.Value = productArticle.Image;
        txtAlt.Text = productArticle.Alt;
    }
    private bool Save()
    {
        ProductArticleBusiness productArticleBusiness = new ProductArticleBusiness();

        string domain = Request.UserHostName;
        string title = txtTitle.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string keywords = txtKeywords.Text.Trim();
        string summary = txtSummary.Text.Trim();
        string content = txtContent.Value;
        string image = hiddenImage.Value;
        string alt = txtAlt.Text.Trim();

        ProductArticle productArticle = new ProductArticle();
        productArticle.Alt = alt;
        productArticle.Content = content;
        productArticle.HtmlName = "";
        productArticle.Image = image;
        productArticle.ClickNumber = 0;
        productArticle.IsShow = isShow;
        productArticle.Keywords = keywords;
        productArticle.OrderBy = string.IsNullOrEmpty(orderBy) ? productArticleBusiness.GetMaxOrder() : int.Parse(orderBy);
        productArticle.Summary = summary;
        productArticle.Title = title;

        return productArticleBusiness.Add(productArticle);
    }
    private bool Update()
    {
        ProductArticleBusiness productArticleBusiness = new ProductArticleBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        ProductArticle productArticle = productArticleBusiness.GetEntity(id);

        string domain = Request.UserHostName;
        string title = txtTitle.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string keywords = txtKeywords.Text.Trim();
        string summary = txtSummary.Text.Trim();
        string content = txtContent.Value;
        string image = hiddenImage.Value;
        string alt = txtAlt.Text.Trim();

        productArticle.Alt = alt;
        productArticle.Content = content;
        productArticle.Image = image;
        productArticle.IsShow = isShow;
        productArticle.Keywords = keywords;
        if (!string.IsNullOrEmpty(orderBy))
            productArticle.OrderBy = int.Parse(orderBy);
        productArticle.Summary = summary;
        productArticle.Title = title;

        return productArticleBusiness.Update(productArticle);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ProductArticleBusiness productArticleBusiness = new ProductArticleBusiness();
        bool success = productArticleBusiness.Delete(id);
        if (success)
        {
            JavascriptHelper.AlertAndRedirect("删除成功", "product_article.aspx");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtTitle.Text = "";
        txtOrderBy.Text = "";
        txtKeywords.Text = "";
        txtSummary.Text = "";
        txtContent.Value = "";
        imgImages.ImageUrl = "image/no.jpg";
        hiddenImage.Value = "";
        txtAlt.Text = "";
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ProductArticleBusiness productArticleBusiness = new ProductArticleBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int nextId = productArticleBusiness.Next(id);
        Response.Redirect("product_article_detail.aspx?Id=" +nextId);
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        ProductArticleBusiness productArticleBusiness = new ProductArticleBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        int prevId = productArticleBusiness.Previous(id);
        Response.Redirect("product_article_detail.aspx?Id=" + prevId);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功", "product_article.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功", "product_article.aspx");
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string ext = Path.GetExtension(FileUpload1.FileName);
        string newFileStr = StringHelper.GetRandomString(8);
        string newFileName = newFileStr + ext;
        string path = "~/upload-file/images/product/";
        string absolutPath = Server.MapPath(path);
        FileUpload1.SaveAs(absolutPath + newFileName);
        imgImages.ImageUrl = path + newFileName;
        hiddenImage.Value = newFileName;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_article.aspx");
    }  
}
