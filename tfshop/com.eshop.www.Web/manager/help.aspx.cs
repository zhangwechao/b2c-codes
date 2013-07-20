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

public partial class back_stage_help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TreeView_load();
            rData_bind();
            if (Context.Items["path"] != null)
            {
                TreeNode node = TreeView1.FindNode(Context.Items["path"].ToString()) as TreeNode;
                if (node != null && node.Parent != null)
                {
                    node.Parent.Expanded = true;
                    node.Selected = true;
                }
            }
        }
    }
    private void rData_bind()
    {
        StringBuilder where = new StringBuilder();
        string title = Request.QueryString["title"];
        string summary = Request.QueryString["summary"];
        string keywords = Request.QueryString["keywords"];
        string isShow = Request.QueryString["isShow"];
        string beginDate = Request.QueryString["beginDate"];
        string endDate = Request.QueryString["endDate"];
        string categoryId = Request.QueryString["categoryId"];

        DataRecordTable table = null;
        where.Append("is_delete=0");
        if (!string.IsNullOrEmpty(title))
            where.Append(" and title like '%" + title + "%'");
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

        if (string.IsNullOrEmpty(categoryId))
            categoryId = "3";

        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        string path = newsCategoryBusiness.GetEntity(int.Parse(categoryId)).Path;
        string prefix = path.Substring(0, path.IndexOf(categoryId) + categoryId.Length);
        table = newsCategoryBusiness.GetList("Id,category_name", "order_by", false, 1, 100, "path like '" + prefix + "%'");
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
        
        string fieldList = "Id,title,click_number,order_by,is_recommend,is_show,category_id,create_date";
        string orderField = "order_by";
        bool orderBy = true;
        table = new NewsContentBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
        switch (cmd)
        {
            case "update":
                Response.Redirect("news_content_detail.aspx?Id=" + id + "&action=update&type=help");
                break;
            case "delete":
                NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
                newsContentBusiness.UpdateDelete(true,id);
                rData_bind();
                break;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("news_content_detail.aspx?type=help");
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                newsContentBusiness.UpdateDelete(true,Id);
            }
        }
        rData_bind();
    }
    private void TreeView_load()
    {
        TreeView1.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = "帮助目录";
        root.NavigateUrl = "Help.aspx";

        DataRecordTable drt = new NewsCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, "father_Id=3");
        TreeNode node = null;
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow dr in drt.Table.Rows)
        {
            node = new TreeNode();
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();
            node.Text = text;
            node.Value = value;
            root.ChildNodes.Add(node);
            createChild(node);
        }
        TreeView1.Nodes.Add(root);

    }
    private void createChild(TreeNode treeNode)
    {
        string where = "father_Id=" + treeNode.Value;
        DataRecordTable drt = new NewsCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, where);
        TreeNode node = null;
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow dr in drt.Table.Rows)
        {
            node = new TreeNode();
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();
            node.Text = text;
            node.Value = value;
            treeNode.ChildNodes.Add(node);
            createChild(node);
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string categoryId = TreeView1.SelectedNode.Value;
        string categoryName = TreeView1.SelectedNode.Text;
        string path = "help.aspx?categoryId=" + categoryId;
        Context.Items["path"] = TreeView1.SelectedNode.ValuePath;
        Server.Transfer(path);
    }
    public string GetCategoryName(int categoryId)
    {
        string categoryName = new NewsCategoryBusiness().GetEntity(categoryId).CategoryName;
        return string.IsNullOrEmpty(categoryName) ? "一级目录" : categoryName;
    }
}
