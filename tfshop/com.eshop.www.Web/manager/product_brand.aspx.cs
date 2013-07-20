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

public partial class back_stage_product_brand : System.Web.UI.Page
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

        string brandName = Request.QueryString["brandName"];
        string isShow = Request.QueryString["isShow"];
        string categoryId = Request.QueryString["categoryId"];

        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();

        if (!string.IsNullOrEmpty(brandName))
            where.Append(" and brand_name like '%" + brandName + "%'");
        if (!string.IsNullOrEmpty(isShow))
            where.Append(" and is_show=" + isShow + "");
        if (!string.IsNullOrEmpty(categoryId))
            where.Append(" and category_id="+categoryId);

        if (where.Length > 0)
            where = where.Remove(0, 5);

        string fieldList = "Id,brand_name,order_by,is_show,category_id";
        string orderField = "order_by";
        bool orderBy = true;
        DataRecordTable table = productBrandBusiness.GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
            Response.Redirect("product_brand_detail.aspx?Id=" + id + "&action=update");
        }
        else if (cmd == "delete")
        {
            ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
            ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();

            if (productDetailBusiness.IsHaveProductByBrand(id))
            {
                JavascriptHelper.Alert("该品牌下面还有产品条目，请删除产品条目再删除该品牌");
                return;
            }
            productBrandBusiness.Delete(id);
            rData_bind();
            TreeView_load();
        }
        else if (cmd == "relate")
        {
            Response.Redirect("product_content.aspx?brandId=" + id);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_brand_detail.aspx");
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        CheckBox chkselect = null;
        bool flag = false;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                if (productDetailBusiness.IsHaveProductByBrand(Id))
                {
                    flag = true;
                    continue;
                }
                productBrandBusiness.Delete(Id);
            }
        }
        if (flag)
        {
            JavascriptHelper.Alert("部分的品牌由于有产品条目，无法删除");
        }
        rData_bind();
        TreeView_load();
    }
    private void TreeView_load()
    {

        TreeView1.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = "产品目录";
        root.NavigateUrl = "product_brand.aspx";

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
        string path = "product_brand.aspx?categoryId=" + categoryId;
        Context.Items["path"] = TreeView1.SelectedNode.ValuePath;
        Server.Transfer(path);
    }
}
