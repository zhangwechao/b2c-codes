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
using System.Text;
using Wuqi.Webdiyer;
using com.eshop.www.Tools;

public partial class administrator_news_category : System.Web.UI.Page
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
                if (node != null && node.Parent!=null)
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
        string categoryName = Request.QueryString["categoryName"];
        string categoryId = Request.QueryString["categoryId"];
        string fatherId = Request.QueryString["fatherId"];
        string isShow = Request.QueryString["isShow"];
        string type = Request.QueryString["type"];

        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        if (string.IsNullOrEmpty(categoryId))
        {
            if (type == "help")
                categoryId = "3";
            else if(type=="news")
                categoryId = "2";
        }

        string path = newsCategoryBusiness.GetEntity(int.Parse(categoryId)).Path;
        string prefix = path.Substring(0,path.IndexOf(categoryId)+categoryId.Length);
        where.Append("path like '" + prefix + ",%'");

        if (!string.IsNullOrEmpty(categoryName))
            where.Append(" and category_name like '%" + categoryName + "%'");
        if (!string.IsNullOrEmpty(fatherId))
            where.Append(" and father_id="+fatherId);
        if (!string.IsNullOrEmpty(isShow))
            where.Append(" and is_show=" + isShow + "");


        string fieldList = "Id,category_name,father_id,order_by,is_show";
        string orderField = "order_by";
        bool orderBy = true;
        DataRecordTable table = newsCategoryBusiness.GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
        if (cmd == "update")
        {
            string type = Request.QueryString["type"];
            Response.Redirect("news_category_detail.aspx?Id=" + id + "&action=update&type="+type);
        }
        else if (cmd == "delete")
        {
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
            newsCategoryBusiness.Delete(id);
            rData_bind();
            TreeView_load();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        Response.Redirect("news_category_detail.aspx?type="+type);
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
        CheckBox chkselect = null;
        bool flag = false;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                if (newsCategoryBusiness.IsHaveSonCategory(Id))
                {
                    flag = true;
                    continue;
                }
                if (newsContentBusiness.IsHaveNews(Id))
                {
                    flag = true;
                    continue;
                }
                newsCategoryBusiness.Delete(Id);
            }
        }
        if (flag)
        {
            JavascriptHelper.Alert("部分的目录由于有子目录或者有文章条目，无法删除");
        }
        rData_bind();
        TreeView_load();
    }
    private void TreeView_load()
    {
        NewsCategoryBusiness newsCategoryBusines = new NewsCategoryBusiness();

        string type = Request.QueryString["type"];
        int fatherId = 0;
        if (type == "help")
            fatherId = 3;
        else if (type == "news")
            fatherId = 2;
        NewsCategory category = newsCategoryBusines.GetEntity(fatherId);

        ddlFather.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlFather.Items.Add(item);

        TreeView1.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = category.CategoryName;
        root.NavigateUrl = "news_category.aspx";

        


        DataRecordTable drt = new NewsCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, "father_Id="+fatherId);
        TreeNode node = null;
        string text = string.Empty;
        string value = string.Empty;
        foreach (DataRow dr in drt.Table.Rows)
        {
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();

            node = new TreeNode();
            node.Text = text;
            node.Value = value;
            root.ChildNodes.Add(node);

            item = new ListItem("╋" + text, value);
            ddlFather.Items.Add(item);

            createChild(node, "├");
        }
        TreeView1.Nodes.Add(root);

    }
    private void createChild(TreeNode treeNode,string prefix)
    {
        string where = "father_Id=" + treeNode.Value;
        DataRecordTable drt = new NewsCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, where);
        TreeNode node = null;
        ListItem item = null;
        string text = string.Empty;
        string value = string.Empty;
        prefix = prefix + "─";
        foreach (DataRow dr in drt.Table.Rows)
        {
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();

            node = new TreeNode();
            node.Text = text;
            node.Value = value;
            treeNode.ChildNodes.Add(node);
            
            item = new ListItem(prefix + text, value);
            ddlFather.Items.Add(item);
            createChild(node,prefix);
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string categoryId = TreeView1.SelectedNode.Value;
        string categoryName = TreeView1.SelectedNode.Text;
        string type = Request.QueryString["type"];
        string path = "news_category.aspx?categoryId=" + categoryId+"&type="+type;
        Context.Items["path"] = TreeView1.SelectedNode.ValuePath;
        Server.Transfer(path);
    }
    public string GetFatherName(int categoryId)
    {
        string fatherName = new NewsCategoryBusiness().GetEntity(categoryId).CategoryName;
        return string.IsNullOrEmpty(fatherName) ? "一级目录" : fatherName;
    }
    
}
