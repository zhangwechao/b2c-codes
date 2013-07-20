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
using com.eshop.www.BLL;
using com.eshop.www.Model;
using Wuqi.Webdiyer;
using com.eshop.www.Tools;
using System.Data;

public partial class administrator_category_attribute : System.Web.UI.Page
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
        string attributeName = Request.QueryString["attributeName"];
        string categoryId = Request.QueryString["categoryId"];

        CategoryAttributeBusiness categoryAttributeBusiness = new CategoryAttributeBusiness();

        where.Append(" state=1");
        if (!string.IsNullOrEmpty(attributeName))
        {
            where.Append(" and attribute like '%" + attributeName + "%'");
        }
        if (!string.IsNullOrEmpty(categoryId))
            where.Append(" and category_id="+categoryId);

        string fieldList = "Id,attribute_id,attribute,category_id,category_name,values_list,is_multiple,is_filter";
        string orderField = "Id";
        bool orderBy = false;
        DataRecordTable table = categoryAttributeBusiness.GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
        if (cmd == "update")
        {
            int attributeId = int.Parse(e.CommandArgument.ToString());
            Response.Redirect("category_attribute_detail.aspx?attributeId=" + attributeId + "&action=update");
        }
        else if (cmd == "delete")
        {
            int id = int.Parse(e.CommandArgument.ToString());
            CategoryAttributeBusiness categoryAttributeBusiness = new CategoryAttributeBusiness();
            categoryAttributeBusiness.UpdateState(false,id);
            rData_bind();
        }
        else if (cmd == "deleteAttribute")
        {
            int attributeId = int.Parse(e.CommandArgument.ToString());
            ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
            productAttributeBusiness.Delete(attributeId);
            rData_bind();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("category_attribute_detail.aspx");
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        CategoryAttributeBusiness categoryAttributeBusines = new CategoryAttributeBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                categoryAttributeBusines.UpdateState(false,Id);
            }
        }
        rData_bind();
    }
    protected void btnAllDelAttribute_Click(object sender, EventArgs e)
    {
        ProductAttributeBusiness productAttributeBusines = new ProductAttributeBusiness();
        CategoryAttributeBusiness categoryAttributeBusiness = new CategoryAttributeBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                int attributeId = categoryAttributeBusiness.GetEntity(Id).AttributeId;
                productAttributeBusines.Delete(attributeId);
            }
        }
        rData_bind();
    }
    private void TreeView_load()
    {

        TreeView1.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = "产品目录";
        root.NavigateUrl = "category_attribute.aspx";
        DataRecordTable drt = new ProductCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, "father_Id=0");
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

            createChild(node);
        }
        TreeView1.Nodes.Add(root);

    }
    private void createChild(TreeNode treeNode)
    {
        string where = "father_Id=" + treeNode.Value;
        DataRecordTable drt = new ProductCategoryBusiness().GetList("Id,category_name,order_by", "order_by", true, 1, 100, where);
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
            treeNode.ChildNodes.Add(node);

            createChild(node);
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string categoryId = TreeView1.SelectedNode.Value;
        string categoryName = TreeView1.SelectedNode.Text;
        string path = "category_attribute.aspx?categoryId=" + categoryId;
        Context.Items["path"] = TreeView1.SelectedNode.ValuePath;
        Server.Transfer(path);
    }
}
