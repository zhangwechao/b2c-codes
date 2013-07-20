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
using com.eshop.www.Model;
using com.eshop.www.BLL;
using System.Data;
using com.eshop.www.Tools;

public partial class category_attribute_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TreeView_load();
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
                int id = int.Parse(Request.QueryString["attributeId"]);
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
                if (productAttributeBusiness.IsHasNext(id))
                {
                    btnNext.Enabled = false;
                    btnNext2.Enabled = false;
                }
                if (productAttributeBusiness.IsHasPrev(id))
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
        int id = int.Parse(Request.QueryString["attributeId"]);
        ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
        ProductAttribute productAttribute = productAttributeBusiness.GetEntity(id);
        displayInfo(productAttribute);
    }
    private void displayInfo(ProductAttribute productAttribute)
    {
        txtAttributeName.Text = productAttribute.Attribute;
        txtValuesList.Text = productAttribute.ValuesList;
        chkMultiple.Checked = productAttribute.isMultiple;
        chkFilter.Checked = productAttribute.IsFilter;

        string fieldList = "Id,category_id,path,attribute_id,state";
        string orderField = "Id";
        bool orderBy = false;
        string where = "attribute_id="+productAttribute.Id+" and state=1";
        DataRecordTable drt = new CategoryAttributeBusiness().GetList(fieldList,orderField,orderBy,1,100,where);
        foreach (DataRow row in drt.Table.Rows)
        {
            string path = "0,"+row["path"].ToString();
            TreeNode node = TreeView1.FindNode(path);
            if (node != null)
            {
                node.Checked = true;
                Expand(node);
            }
        }
    }
    private void Expand(TreeNode node)
    {
        if (node.Parent != null)
        {
            node.Parent.Expanded = true;
            Expand(node.Parent);
        }
        
    }
    private void SaveTree(TreeNodeCollection treeNodes,int attributeId)
    {
        CategoryAttributeBusiness categoryAttributeBusiness = new CategoryAttributeBusiness();
        CategoryAttribute categoryAttribute = null;
        foreach (TreeNode node in treeNodes)
        {
            categoryAttribute = new CategoryAttribute();
            categoryAttribute.AttributeId = attributeId;
            categoryAttribute.CategoryId = int.Parse(node.Value);
            if (node.Checked)
                categoryAttribute.State = true;
            else
                categoryAttribute.State = false;
            categoryAttributeBusiness.Add(categoryAttribute);
            SaveTree(node.ChildNodes, attributeId);
        }
    }
    
    private bool Save()
    {
        ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
        CategoryAttributeBusiness categoryAttributeBusiness = new CategoryAttributeBusiness();
        

        ProductAttribute productAttribute = new ProductAttribute();
        productAttribute.Attribute = txtAttributeName.Text.Trim();
        productAttribute.ValuesList = txtValuesList.Text.Trim();
        productAttribute.isMultiple = chkMultiple.Checked;
        productAttribute.IsFilter = chkFilter.Checked;
        
        int attributeId = productAttributeBusiness.Add(productAttribute);
        SaveTree(TreeView1.Nodes, attributeId);
        return true;
    }
    private void UpdateTree(TreeNodeCollection treeNodes, int attributeId)
    {
        CategoryAttributeBusiness categoryAttributeBusiness = new CategoryAttributeBusiness();
        CategoryAttribute categoryAttribute = null;
        foreach (TreeNode node in treeNodes)
        {
            categoryAttribute = new CategoryAttribute();
            categoryAttribute.AttributeId = attributeId;
            categoryAttribute.CategoryId = int.Parse(node.Value);
            if (node.Checked)
                categoryAttribute.State = true;
            else
                categoryAttribute.State = false;
            bool isSame = categoryAttributeBusiness.IsSame(categoryAttribute.CategoryId, categoryAttribute.AttributeId);
            if (isSame)
                categoryAttributeBusiness.Update(categoryAttribute);
            else
                categoryAttributeBusiness.Add(categoryAttribute);
            UpdateTree(node.ChildNodes, attributeId);
        }
    }
    private bool Update()
    {
        ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
        CategoryAttributeBusiness categoryAttributeBusiness = new CategoryAttributeBusiness();
        int id = int.Parse(Request.QueryString["attributeId"]);
        ProductAttribute productAttribute = new ProductAttribute();

        productAttribute.Attribute = txtAttributeName.Text.Trim();
        productAttribute.ValuesList = txtValuesList.Text.Trim();
        productAttribute.isMultiple = chkMultiple.Checked;
        productAttribute.IsFilter = chkFilter.Checked;
        productAttribute.Id = id;

        productAttributeBusiness.Update(productAttribute);

        UpdateTree(TreeView1.Nodes,id);
        return true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["attributeId"]);
        ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
        bool success = productAttributeBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("删除成功", "category_attribute.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtAttributeName.Text = "";
        
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
        int id = int.Parse(Request.QueryString["attributeId"]);
        int nextId = productAttributeBusiness.Next(id);
        Response.Redirect("category_attribute_detail.aspx?attributeId=" + nextId + "&action=update");
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        ProductAttributeBusiness productAttributeBusiness = new ProductAttributeBusiness();
        int id = int.Parse(Request.QueryString["attributeId"]);
        int prevId = productAttributeBusiness.Previous(id);
        Response.Redirect("category_attribute_detail.aspx?attributeId=" + prevId + "&action=update");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功", "category_attribute.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功","category_attribute.aspx");
                btnReset_Click(null, null);
            }
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("category_attribute.aspx");
    }
    private void TreeView_load()
    {

        TreeView1.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = "产品目录";
        root.Value = "0";
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
    
}
