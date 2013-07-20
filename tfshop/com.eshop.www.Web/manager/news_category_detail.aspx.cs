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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using com.eshop.www.Model;
using com.eshop.www.BLL;
using com.eshop.www.Tools;
using System.IO;

public partial class administrator_news_category_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFather_load();
            string action = Request.QueryString["action"];
            string type = Request.QueryString["type"];
            int categoryId = 0;
            if (type == "help")
                categoryId = 3;
            else if (type == "news")
                categoryId = 2;
            if (action == "update")
            {
                NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
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
                if (newsCategoryBusiness.IsHasNext(id, categoryId))
                {
                    btnNext.Enabled = false;
                    btnNext2.Enabled = false;
                }
                if (newsCategoryBusiness.IsHasPrev(id, categoryId))
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
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        NewsCategory newsCategory = newsCategoryBusiness.GetEntity(id);
        displayInfo(newsCategory);
    }
    private void displayInfo(NewsCategory newsCategory)
    {
        txtCategoryName.Text = newsCategory.CategoryName;
        txtOrderBy.Text = newsCategory.OrderBy.ToString();
        chkIsShow.Checked = newsCategory.IsShow;
        txtRemark.Text = newsCategory.Remark;
        foreach (ListItem item in ddlFather.Items)
        {
            string fatherId = item.Value.Split('|')[0];
            if (fatherId == newsCategory.FatherId.ToString())
            {
                item.Selected = true;
                break;
            }
        }
        //imgImages.ImageUrl = "~/upload-file/images/product/" + newsCategory.Image;
        //hiddenImage.Value = newsCategory.Image;
        //txtAlt.Text = newsCategory.Alt;
    }
    private void ddlFather_load()
    {
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();

        int fatherId = 0;
        string type = Request.QueryString["type"];
        if (type == "help")
            fatherId = 3;
        else if (type == "news")
            fatherId = 2;
        NewsCategory newsCategory = newsCategoryBusiness.GetEntity(fatherId);

        ddlFather.Items.Clear();
        ListItem item = new ListItem(newsCategory.CategoryName, newsCategory.Id.ToString()+"|"+newsCategory.Path);
        ddlFather.Items.Add(item);

        DataRecordTable table = newsCategoryBusiness.GetList("Id,category_name,path,order_by", "order_by", false, 1, 50, "father_id="+fatherId);
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            text = "╋"+row["category_name"].ToString();
            value = row["Id"].ToString()+"|"+row["path"].ToString();
            item = new ListItem(text, value);
            
            ddlFather.Items.Add(item);
            CreateChild(row["Id"].ToString(), "├", table, newsCategoryBusiness);
        }
    }
    private void CreateChild(string parentId, string nodeText,DataRecordTable table,NewsCategoryBusiness newsCategoryBusiness)
    {
        table = newsCategoryBusiness.GetList("Id,category_name,path,order_by","order_by",false,1,50,"father_id="+parentId);
        ListItem item = null;
        foreach (DataRow row in table.Table.Rows)
        {
            item = new ListItem(nodeText + "─"+row["category_name"].ToString(),row["Id"].ToString()+"|"+row["path"].ToString());
            ddlFather.Items.Add(item);
            CreateChild(row["Id"].ToString(), nodeText + "─",table,newsCategoryBusiness);
        }
    }
    private bool Save()
    {
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();

        string categoryName = txtCategoryName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string remark = txtRemark.Text.Trim();
        //string image = hiddenImage.Value;
        //string alt = txtAlt.Text.Trim();
        int fatherId = int.Parse(ddlFather.SelectedValue.Split('|')[0]);
        string path = fatherId == 0 ? "" : ddlFather.SelectedValue.Split('|')[1];

        NewsCategory newsCategory = new NewsCategory();
        newsCategory.Alt = "";// alt;
        newsCategory.Image = "";// image;
        newsCategory.IsShow = isShow;
        newsCategory.OrderBy = string.IsNullOrEmpty(orderBy) ? newsCategoryBusiness.GetMaxOrder() : int.Parse(orderBy);
        newsCategory.Remark = remark;
        newsCategory.CategoryName = categoryName;
        newsCategory.FatherId = fatherId;
        newsCategory.Path = path;

        if (newsCategoryBusiness.IsHaveSameName(categoryName))
        {
            JavascriptHelper.Alert("已经有相同的目录名，请输入另外一个目录名");
            return false;
        }
        return newsCategoryBusiness.Add(newsCategory);
    }
    private bool Update()
    {
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        NewsCategory oldNewsCategory = newsCategoryBusiness.GetEntity(id);

        string categoryName = txtCategoryName.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        bool isShow = chkIsShow.Checked;
        string remark = txtRemark.Text.Trim();
        //string image = hiddenImage.Value;
        //string alt = txtAlt.Text.Trim();
        int fatherId = int.Parse(ddlFather.SelectedValue.Split('|')[0]);
        string path = fatherId == 0 ? id.ToString() : ddlFather.SelectedValue.Split('|')[1] + "," + id.ToString();

        NewsCategory newsCategory = new NewsCategory();
        newsCategory.Id = id;
        newsCategory.Alt = ""; //alt;
        newsCategory.Image = "";// image;
        newsCategory.IsShow = isShow;
        newsCategory.OrderBy = string.IsNullOrEmpty(orderBy) ? newsCategoryBusiness.GetMaxOrder() : int.Parse(orderBy);
        newsCategory.Remark = remark;
        newsCategory.CategoryName = categoryName;
        newsCategory.FatherId = fatherId;
        newsCategory.Path = path;

        if (string.Compare(oldNewsCategory.CategoryName, categoryName,false) != 0)
        {
            if (newsCategoryBusiness.IsHaveSameName(categoryName))
            {
                JavascriptHelper.Alert("已经有相同的目录名，请输入另外一个目录名");
                return false;
            }
        }
        return newsCategoryBusiness.Update(newsCategory);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        if (newsCategoryBusiness.IsHaveSonCategory(id))
        {
            JavascriptHelper.Alert("该目录下面有子目录，请删除子目录再删除该目录");
            return;
        }
        if (newsContentBusiness.IsHaveNews(id))
        {
            JavascriptHelper.Alert("该目录下面还有文章条目，请删除文章条目再删除该目录");
            return;
        }
        bool success = newsCategoryBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("目录删除成功", "news_category.aspx");
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
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        string type = Request.QueryString["type"];
        int categoryId = 0;
        if (type == "help")
            categoryId = 3;
        else if (type == "news")
            categoryId = 2;
        int nextId = newsCategoryBusiness.Next(id,categoryId);
        Response.Redirect("news_category_detail.aspx?Id=" + nextId + "&action=update&type="+type);
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        string type = Request.QueryString["type"];
        int categoryId = 0;
        if (type == "help")
            categoryId = 3;
        else if (type == "news")
            categoryId = 2;
        int  prevId = newsCategoryBusiness.Previous(id, categoryId);
        Response.Redirect("news_category_detail.aspx?Id=" + prevId+ "&action=update&type="+type);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","news_category.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功","news_category.aspx");
                ddlFather_load();
                btnReset_Click(null, null);
            }
        }
    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    string ext = Path.GetExtension(FileUpload1.FileName);
    //    string newFileStr = StringHelper.GetRandomString(8);
    //    string newFileName = newFileStr + ext;
    //    string path = "~/upload-file/images/product";
    //    string absolutPath = Server.MapPath(path);
    //    FileUpload1.SaveAs(absolutPath + newFileName);
    //    imgImages.ImageUrl = path + newFileName;
    //    hiddenImage.Value = newFileName;
    //}
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        Response.Redirect("news_category.aspx?type="+type);
    }
}
