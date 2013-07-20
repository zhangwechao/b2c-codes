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
using System.Data;
using com.eshop.www.BLL;
using com.eshop.www.Tools;

public partial class back_stage_product_attribute : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            attributeList_load();
        }
    }
    private void attributeList_load()
    { 
        int productId = int.Parse(Request.QueryString["productId"]);
        ProductDetail productDetail = new ProductDetailBusiness().GetEntity(productId);
        DataRecordTable table = new CategoryAttributeBusiness().GetList("attribute,attribute_id,values_list,is_multiple,category_id","category_id",false,1,100,"category_id="+productDetail.CategoryId+" and state=1");
        attributeList.DataSource = table.Table;
        attributeList.DataBind();
    }
    protected void attributeList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int productId = int.Parse(Request.QueryString["productId"]);
        int attributeId = int.Parse((e.Item.FindControl("hiddenAttributeId") as HiddenField).Value);
        string valuesList = (e.Item.FindControl("hiddenValuesList") as HiddenField).Value;
        string isMultiple = (e.Item.FindControl("hiddenIsMultiple") as HiddenField).Value;
        CheckBoxList chkValues = e.Item.FindControl("chkValues") as CheckBoxList;
        DropDownList ddlValues = e.Item.FindControl("ddlValues") as DropDownList;
        TextBox txtValues = e.Item.FindControl("txtValues") as TextBox;
        ProductAttributeValueBusiness productAttributeValueBusiness = new ProductAttributeValueBusiness();
        ProductAttributeValue productAttributeValue = productAttributeValueBusiness.GetEntity(new ProductAttributeValue() { ProductId = productId, AttributeId = attributeId });
        if (valuesList.Trim().Length > 0)
        {
            string[] strs = valuesList.Split(',');
            ListItem item = null;
            if (isMultiple == "1")
            {
                chkValues.Visible = true;
                foreach (string str in strs)
                {
                    item = new ListItem(str,str);
                    chkValues.Items.Add(item);
                }
                if (!string.IsNullOrEmpty(productAttributeValue.AttributeValue))
                {
                    string[] values = productAttributeValue.AttributeValue.Split(',');
                    foreach (string value in values)
                    {
                        if (!string.IsNullOrEmpty(value))
                            chkValues.Items.FindByValue(value).Selected = true;
                    }
                }
            }
            else
            {
                ddlValues.Visible = true;
                foreach (string str in strs)
                {
                    item = new ListItem(str,str);
                    ddlValues.Items.Add(item);
                }
                if (!string.IsNullOrEmpty(productAttributeValue.AttributeValue))
                {
                    ddlValues.Items.FindByValue(productAttributeValue.AttributeValue.Trim()).Selected = true;
                }
            }
        }
        else
        {
            txtValues.Visible = true;
            if (!string.IsNullOrEmpty(productAttributeValue.AttributeValue))
                txtValues.Text = productAttributeValue.AttributeValue.Trim();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int productId = int.Parse(Request.QueryString["productId"]);
        int attributeId = 0;
        string valuesList = string.Empty;
        string isMultiple = string.Empty;
        CheckBoxList chkValues = null;
        DropDownList ddlValues = null;
        TextBox txtValues = null;
        ProductAttributeValueBusiness business = new ProductAttributeValueBusiness();
        ProductAttributeValue value = null;
        string where = string.Empty;
        int record = 0;
        string values =string.Empty;
        foreach (RepeaterItem item in attributeList.Items)
        {
            attributeId = int.Parse((item.FindControl("hiddenAttributeId") as HiddenField).Value);
            valuesList = (item.FindControl("hiddenValuesList") as HiddenField).Value;
            isMultiple = (item.FindControl("hiddenIsMultiple") as HiddenField).Value;
            chkValues = item.FindControl("chkValues") as CheckBoxList;
            ddlValues = item.FindControl("ddlValues") as DropDownList;
            txtValues = item.FindControl("txtValues") as TextBox;
            where = "product_id="+productId+" and attribute_id="+attributeId;
            record = business.GetRecordCount(where);
            if (chkValues!=null && chkValues.Visible)
            {
                foreach (ListItem listItem in chkValues.Items)
                {
                    if (listItem.Selected)
                        values += listItem.Value + ",";
                }
            }
            if (ddlValues!=null && ddlValues.Visible)
                values = ddlValues.SelectedValue;
            if (txtValues!=null && txtValues.Visible)
                values = txtValues.Text.Trim();

            value = new ProductAttributeValue() { ProductId = productId, AttributeId = attributeId, AttributeValue = values };
            if (record > 0)
                business.UpdateByProductIdAndAttributeId(value);
            else
                business.Add(value);

        }
        JavascriptHelper.Alert("修改信息成功");
    }
}
