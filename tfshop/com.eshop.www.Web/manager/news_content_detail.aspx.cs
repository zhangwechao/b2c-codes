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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.eshop.www.Model;
using com.eshop.www.BLL;
using System.IO;
using com.eshop.www.Tools;

public partial class administrator_news_content_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCategory_load();
            string action = Request.QueryString["action"];
            string type = Request.QueryString["type"];
            int categoryId = 0;
            if (type == "sitepost")
                categoryId = 4;
            else if (type == "help")
                categoryId = 3;
            else if (type == "news")
                categoryId = 2;
            else if (type == "secret")
                categoryId = 5;
            else if (type == "statement")
                categoryId = 6;
            else if (type == "integrate")
                categoryId = 7;
            else if (type == "recruit")
                categoryId = 13;
            if (action == "update")
            {
                NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
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
                if (newsContentBusiness.IsHasNext(id,categoryId))
                {
                    btnNext.Enabled = false;
                    btnNext2.Enabled = false;
                }
                if (newsContentBusiness.IsHasPrev(id,categoryId))
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
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        NewsContent newsContent = newsContentBusiness.GetEntity(id);
        displayInfo(newsContent);
    }
    private void displayInfo(NewsContent newsContent)
    {
        txtTitle.Text = newsContent.Title;
        txtOrderBy.Text = newsContent.OrderBy.ToString();
        ddlCategory.SelectedValue = newsContent.CategoryId.ToString();
        chkIsShow.Checked = newsContent.IsShow;
        chkIsRecommend.Checked = newsContent.IsRecommend;
        txtKeywords.Text = newsContent.Keywords;
        txtSummary.Text = newsContent.Summary;
        txtContent.Value = newsContent.Content;
        txtPageFrom.Text = newsContent.PageFrom;
        txtAuthor.Text = newsContent.Author;
    }
    private bool Save()
    {
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();

        string domain = Request.UserHostName;
        string title = txtTitle.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        int categoryId = int.Parse(ddlCategory.SelectedValue);
        bool isShow = chkIsShow.Checked;
        bool isRecommend = chkIsRecommend.Checked;
        bool isComment = false;
        string keywords = txtKeywords.Text.Trim();
        string summary = txtSummary.Text.Trim();
        string content = txtContent.Value;
        string pageFrom = txtPageFrom.Text.Trim();
        string author = txtAuthor.Text.Trim();

        NewsContent newsContent = new NewsContent();
        newsContent.Alt = "";// alt;
        newsContent.Content = content;
        newsContent.HtmlName = "";
        newsContent.Image = "";// image;
        newsContent.ClickNumber = 0;
        newsContent.IsShow = isShow;
        newsContent.Keywords = keywords;
        newsContent.OrderBy = string.IsNullOrEmpty(orderBy) ? newsContentBusiness.GetMaxOrder() : int.Parse(orderBy);
        newsContent.Summary = summary;
        newsContent.Title = title;
        newsContent.Author = author;
        newsContent.CategoryId = categoryId;
        newsContent.IsCheck = false;
        newsContent.IsComment = isComment;
        newsContent.IsDelete = false;
        newsContent.IsImageNews = false;
        newsContent.IsRecommend = isRecommend;
        newsContent.PageFrom = pageFrom;

        return newsContentBusiness.Add(newsContent);
    }
    private bool Update()
    {
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        NewsContent newsContent = newsContentBusiness.GetEntity(id);

        string domain = Request.UserHostName;
        string title = txtTitle.Text.Trim();
        string orderBy = txtOrderBy.Text.Trim();
        int categoryId = int.Parse(ddlCategory.SelectedValue);
        bool isShow = chkIsShow.Checked;
        bool isRecommend = chkIsRecommend.Checked;
        string keywords = txtKeywords.Text.Trim();
        string summary = txtSummary.Text.Trim();
        string content = txtContent.Value;
        string author = txtAuthor.Text.Trim();
        string pageFrom = txtPageFrom.Text.Trim();

        newsContent.Alt = "";// alt;
        newsContent.Author = author;
        newsContent.CategoryId = categoryId;
        newsContent.Content = content;
        newsContent.Image = "";// image;
        newsContent.IsShow = isShow;
        newsContent.IsRecommend = isRecommend;
        newsContent.Keywords = keywords;
        if(!string.IsNullOrEmpty(orderBy))
            newsContent.OrderBy =  int.Parse(orderBy);
        newsContent.Summary = summary;
        newsContent.Title = title;

        return newsContentBusiness.Update(newsContent);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        string type = Request.QueryString["type"];
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        bool success = newsContentBusiness.Delete(id);
        if (success)
        {
            if (type == "sitepost")
                JavascriptHelper.AlertAndRedirect("删除成功", "website_sitepost.aspx");
            else if (type == "help")
                JavascriptHelper.AlertAndRedirect("删除成功","help.aspx");
            else if(type=="news")
                JavascriptHelper.AlertAndRedirect("删除成功", "news.aspx");
            else if (type == "secret")
                JavascriptHelper.AlertAndRedirect("删除成功", "secret_protect.aspx");
            else if (type == "statement")
                JavascriptHelper.AlertAndRedirect("删除成功", "law_statement.aspx");
            else if (type == "integrate")
                JavascriptHelper.AlertAndRedirect("删除成功", "integrate_purchase.aspx");
            else if (type == "recruit")
                JavascriptHelper.AlertAndRedirect("删除成功", "recruit.aspx");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtTitle.Text = "";
        txtOrderBy.Text = "";
        txtKeywords.Text = "";
        txtSummary.Text = "";
        txtContent.Value = "";
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        string type = Request.QueryString["type"];
        int categoryId = 0;
        if (type == "sitepost")
            categoryId = 4;
        else if (type == "help")
            categoryId = 3;
        else if (type == "news")
            categoryId = 2;
        else if (type == "secret")
            categoryId = 5;
        else if (type == "statement")
            categoryId = 6;
        else if (type == "integrate")
            categoryId = 7;
        else if (type == "recruit")
            categoryId = 13;
        int nextId = newsContentBusiness.Next(id, categoryId);
        Response.Redirect("news_content_detail.aspx?Id=" + nextId + "&action=update&type="+type);
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        string type = Request.QueryString["type"];
        int categoryId = 0;
        if (type == "sitepost")
            categoryId = 4;
        else if (type == "help")
            categoryId = 3;
        else if (type == "news")
            categoryId = 2;
        else if (type == "secret")
            categoryId = 5;
        else if (type == "statement")
            categoryId = 6;
        else if (type == "integrate")
            categoryId = 7;
        else if (type == "recruit")
            categoryId = 13;
        int id = int.Parse(Request.QueryString["Id"]);
        int prevId = newsContentBusiness.Previous(id, categoryId);
        Response.Redirect("news_content_detail.aspx?Id=" + prevId + "&action=update&type=" + type);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        string type = Request.QueryString["type"];
        if (action == "update")
        {
            if (Update())
            {
                if (type == "sitepost")
                    JavascriptHelper.AlertAndRedirect("修改信息成功", "website_sitepost.aspx");
                else if (type == "help")
                    JavascriptHelper.AlertAndRedirect("修改信息成功", "help.aspx");
                else if (type == "news")
                    JavascriptHelper.AlertAndRedirect("修改信息成功", "news.aspx");
                else if (type == "secret")
                    JavascriptHelper.AlertAndRedirect("修改信息成功", "secret_protect.aspx");
                else if (type == "statement")
                    JavascriptHelper.AlertAndRedirect("修改信息成功", "law_statement.aspx");
                else if (type == "integrate")
                    JavascriptHelper.AlertAndRedirect("修改信息成功", "integrate_purchase.aspx");
                else if (type == "recruit")
                    JavascriptHelper.AlertAndRedirect("修改信息成功", "recruit.aspx");
                
            }
        }
        else
        {
            if (Save())
            {
                if (type == "sitepost")
                    JavascriptHelper.AlertAndRedirect("增加信息成功", "website_sitepost.aspx");
                else if (type == "help")
                    JavascriptHelper.AlertAndRedirect("增加信息成功", "help.aspx");
                else if (type == "news")
                    JavascriptHelper.AlertAndRedirect("增加信息成功", "news.aspx");
                else if (type == "secret")
                    JavascriptHelper.AlertAndRedirect("增加信息成功", "secret_protect.aspx");
                else if (type == "statement")
                    JavascriptHelper.AlertAndRedirect("增加信息成功", "law_statement.aspx");
                else if (type == "integrate")
                    JavascriptHelper.AlertAndRedirect("增加信息成功", "integrate_purchase.aspx");
                else if (type == "recruit")
                    JavascriptHelper.AlertAndRedirect("增加信息成功", "recruit.aspx");
                
            }
        }
    }
    
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        if (type == "sitepost")
            Response.Redirect("website_sitepost.aspx");
        else if (type == "help")
            Response.Redirect("help.aspx");
        else if (type == "news")
            Response.Redirect("news.aspx");
        else if (type == "secret")
            Response.Redirect("secret_protect.aspx");
        else if (type == "statement")
            Response.Redirect("law_statement.aspx");
        else if (type == "integrate")
            Response.Redirect("integrate_purchase.aspx");
        else if (type == "recruit")
            Response.Redirect("recruit.aspx");
    }
    private void ddlCategory_load()
    {
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();

        int categoryId = 0;
        string type = Request.QueryString["type"];
        if (type == "sitepost")
            categoryId = 4;
        else if (type == "help")
            categoryId = 3;
        else if (type == "news")
            categoryId = 2;
        else if (type == "secret")
            categoryId = 5;
        else if (type == "statement")
            categoryId = 6;
        else if (type == "integrate")
            categoryId = 7;
        else if (type == "recruit")
            categoryId = 13;
        NewsCategory category = newsCategoryBusiness.GetEntity(categoryId);

        ddlCategory.Items.Clear();
        ListItem item = new ListItem(category.CategoryName,category.Id.ToString());
        ddlCategory.Items.Add(item);

        
        DataRecordTable table = newsCategoryBusiness.GetList("Id,category_name,path,order_by", "order_by", false, 1, 50, "father_id="+category.Id);
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            text = "╋" + row["category_name"].ToString();
            value = row["Id"].ToString();
            item = new ListItem(text, value);

            ddlCategory.Items.Add(item);
            CreateChild(value, "├", table, newsCategoryBusiness);
        }
    }
    private void CreateChild(string parentId, string nodeText, DataRecordTable table, NewsCategoryBusiness newsCategoryBusiness)
    {
        table = newsCategoryBusiness.GetList("Id,category_name,path,order_by", "order_by", false, 1, 50, "father_id=" + parentId);
        ListItem item = null;
        foreach (DataRow row in table.Table.Rows)
        {
            item = new ListItem(nodeText + "─" + row["category_name"].ToString(), row["Id"].ToString());
            ddlCategory.Items.Add(item);
            CreateChild(row["Id"].ToString(), nodeText + "─", table, newsCategoryBusiness);
        }
    }
}
