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
using System.Data;

public partial class administrator_product_category : System.Web.UI.Page
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
                if (node != null)
                {
                    NodeExpanded(node);
                    node.Selected = true;
                }
            }
        }
    }
    private void NodeExpanded(TreeNode node)
    {
        if (node.Parent != null)
        {
            node.Parent.Expanded = true;
            NodeExpanded(node.Parent);
        }
    }
    private void rData_bind()
    {
        StringBuilder where = new StringBuilder();
        
        string categoryName = Request.QueryString["categoryName"];
        string categoryId = Request.QueryString["categoryId"];
        string fatherId = Request.QueryString["fatherId"];
        string isShow = Request.QueryString["isShow"];

        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();

        if (!string.IsNullOrEmpty(categoryName))
            where.Append(" and category_name like '%" + categoryName + "%'");
        if (!string.IsNullOrEmpty(categoryId))
        {
            where.Append(" and Id="+categoryId);
        }
        if (!string.IsNullOrEmpty(fatherId))
            where.Append(" and father_id=" + fatherId);
        if (!string.IsNullOrEmpty(isShow))
            where.Append(" and is_show=" + isShow + "");

        if (where.Length > 0)
            where = where.Remove(0,5);

        string fieldList = "Id,category_name,father_id,order_by,is_show";
        string orderField = "order_by";
        bool orderBy = true;
        DataRecordTable table = productCategoryBusiness.GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
            Response.Redirect("product_category_detail.aspx?Id=" + id + "&action=update");
        }
        else if (cmd == "delete")
        {
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
            productCategoryBusiness.Delete(id);
            rData_bind();
            TreeView_load();
        }
        else if (cmd == "relate")
        {
            Response.Redirect("product_content.aspx?categoryId=" + id);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_category_detail.aspx");
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        CheckBox chkselect = null;
        bool flag = false;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                if (productCategoryBusiness.IsHaveSonCategory(Id))
                {
                    flag = true;
                    continue;
                }
                if (productDetailBusiness.IsHaveProductByCategory(Id))
                {
                    flag = true;
                    continue;
                }
                productCategoryBusiness.Delete(Id);
            }
        }
        if (flag)
        {
            JavascriptHelper.Alert("部分的目录由于有子目录或者有产品条目，无法删除");
        }
        rData_bind();
        TreeView_load();
    }
    private void TreeView_load()
    {

        ddlFather.Items.Clear();
        ListItem item = new ListItem("--请选择--", "0");
        ddlFather.Items.Add(item);

        TreeView1.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = "产品目录";
        root.NavigateUrl = "product_category.aspx";

        DataTable dt = new ProductCategoryBusiness().GetTable();
        TreeNode node = null;
        string text = string.Empty;
        string value = string.Empty;
        DataRow[] rowList = dt.Select("father_id=0");

        foreach (DataRow dr in rowList)
        {
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();

            node = new TreeNode();
            node.Text = text;
            node.Value = value;
            root.ChildNodes.Add(node);

            item = new ListItem("╋" + text, value);
            ddlFather.Items.Add(item);

            createChild(node, "├",dt);

            
        }
        TreeView1.Nodes.Add(root);

    }
    private void createChild(TreeNode treeNode, string prefix,DataTable dt)
    {
        
        TreeNode node = null;
        ListItem item = null;
        string text = string.Empty;
        string value = string.Empty;
        prefix = prefix + "─";

        DataRow[] rowList = dt.Select("father_id=" + treeNode.Value);
        foreach (DataRow dr in rowList)
        {
            text = dr["category_name"].ToString();
            value = dr["Id"].ToString();

            node = new TreeNode();
            node.Text = text;
            node.Value = value;
            treeNode.ChildNodes.Add(node);

            item = new ListItem(prefix + text, value);
            ddlFather.Items.Add(item);
            createChild(node, prefix,dt);
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string categoryId = TreeView1.SelectedNode.Value;
        string categoryName = TreeView1.SelectedNode.Text;
        string path = "product_category.aspx?categoryId=" + categoryId;
        Context.Items["path"] = TreeView1.SelectedNode.ValuePath;
        Server.Transfer(path);
    }
    public string GetFatherName(int categoryId)
    {
        string fatherName = new ProductCategoryBusiness().GetEntity(categoryId).CategoryName;
        return string.IsNullOrEmpty(fatherName) ? "一级目录" : fatherName;
    }
}
