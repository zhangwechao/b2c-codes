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
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using com.eshop.www.Model;
using com.eshop.www.BLL;
using Wuqi.Webdiyer;
using System.Collections.Generic;

public partial class p_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rNewProductList_load();
            rCategoryList_load();
            rBrandList_load();
            rAttributeList_load();
        }
    }

    private void rNewProductList_load()
    {
        StringBuilder where = new StringBuilder();
        DataRecordTable table = null;
        where.Append("is_show=1 and is_delete=0");
        string isNew = Request.QueryString["isNew"];
        string categoryId = Request.QueryString["categoryId"];
        string brandId = Request.QueryString["brandId"];
        string keyword = Request.QueryString["keyword"];
        string maxPrice = Request.QueryString["maxPrice"];
        string minPrice = Request.QueryString["minPrice"];

        if (!string.IsNullOrEmpty(isNew))
            where.Append(" and is_new="+isNew);
        if (!string.IsNullOrEmpty(categoryId))
        {
            ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
            string path = productCategoryBusiness.GetEntity(int.Parse(categoryId)).Path;
            string prefix = path.Substring(0, path.IndexOf(categoryId) + categoryId.Length);
            table = productCategoryBusiness.GetList("Id,category_name,order_by", "order_by", false, 1, 500, "path like '" + prefix + "%'");
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
            //where.Append(" and category_id=" + categoryId);
            //将选择的目录id号放在隐藏字段里面
            hiddenCategoryId.Value = categoryId;
            //如果有目录查询，移出条件显示，默认为不显示
            lBtnCategoryRemove.Visible = true;
        }
        if (!string.IsNullOrEmpty(brandId))
        {
            where.Append(" and brand_id=" + brandId);
            //如果有品牌查询，移出条件显示，默认为不显示
            lBtnBrandRemove.Visible = true;
        }
        if (!string.IsNullOrEmpty(keyword))
            where.Append(" and product_name like '%"+keyword+"%'");
        if (!string.IsNullOrEmpty(maxPrice))
            where.Append(" and sale_price<="+maxPrice);
        if (!string.IsNullOrEmpty(minPrice))
            where.Append(" and sale_price>="+minPrice);
        

        GetAttributeValuesList(where);

        string fieldList = "Id,product_name,sale_price,price,category_id,brand_id,order_by";
        //用GetList2,用view_product_attribute_value视图查询
        table = new ProductDetailBusiness().GetList2(fieldList,"order_by",true,AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize,where.ToString());
        rNewProductList.DataSource = table.Table;
        rNewProductList.DataBind();

        AspNetPager1.RecordCount = table.RecordCount;
        AspNetPager2.RecordCount = table.RecordCount;
    }
    //根据所选择的属性和值列出所有产品
    private void GetAttributeValuesList(StringBuilder where)
    {
        //得到显示的属性列表
        DataRecordTable table = GetAttributeList();

        string Id = string.Empty;
        string argName = string.Empty;
        string attributeValue = string.Empty;
        string attrIds = string.Empty;
        string values = string.Empty;
        if (table != null && table.Table.Rows.Count>0)
        {
            
            for (int i = 0; i<table.Table.Rows.Count;i++ )
            {
                DataRow row = table.Table.Rows[i];
                Id = row["attribute_id"].ToString();
                //请求的属性名称
                argName = "attribute_" + Id;
                //得到属性的值
                attributeValue = Request.QueryString[argName];
                if (!string.IsNullOrEmpty(attributeValue))
                {
                    //此处用like,请查看view_product_attribute_value视图结构
                    where.Append(" and attribute_id like '%" + Id + "%' and attribute_value like '%" + attributeValue.Split('_')[0] + "%'");
                    
                }
            }
        }
        
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        AspNetPager page = (AspNetPager)sender;
        AspNetPager2.CurrentPageIndex = page.CurrentPageIndex;
        rNewProductList_load();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        AspNetPager page = (AspNetPager)sender;
        AspNetPager1.CurrentPageIndex = page.CurrentPageIndex;
        rNewProductList_load();
    }
    //显示图片
    protected ProductImage GetImage(int productId)
    {
        string where = "product_id=" + productId + " and type=1";
        string fieldList = "Id,product_id,image,alt";
        string orderFiled = "Id";
        bool orderby = false;
        DataRecordTable table = new ProductImageBusiness().GetList(fieldList, orderFiled, orderby, 1, 1, where);
        ProductImage productImage = new ProductImage();
        if (table.Table.Rows.Count > 0)
        {
            productImage.Image = table.Table.Rows[0]["image"].ToString();
            productImage.Alt = table.Table.Rows[0]["alt"].ToString();
        }
        return productImage;
    }
    private int GetFatherId(int categoryId)
    {
        int fatherId = 0;
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        ProductCategory productCategory = productCategoryBusiness.GetEntity(categoryId);
        if (productCategory.FatherId == 0)
            fatherId = productCategory.Id;
        else
            fatherId = GetFatherId(productCategory.FatherId);
        return fatherId;
    }
    //筛选处的品牌绑定
    private void rBrandList_load()
    {
        int categoryId = 2;
        if (!string.IsNullOrEmpty(Request.QueryString["categoryId"]))
            categoryId = int.Parse(Request.QueryString["categoryId"]);
        int fatherId = GetFatherId(categoryId);

        string where = "is_show=1 and category_id="+fatherId;
        DataRecordTable table = new ProductBrandBusiness().GetList("Id,brand_name,order_by","order_by",false,1,20,where);
        rBrandList.DataSource = table.Table;
        rBrandList.DataBind();
    }
    //筛选处的目录绑定
    private void rCategoryList_load()
    {
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();

        int fatherId = 0;
        string categoryId = Request.QueryString["categoryId"];
        if (!string.IsNullOrEmpty(categoryId))
            fatherId = productCategoryBusiness.GetEntity(int.Parse(categoryId)).FatherId;
        else
            fatherId = int.Parse(hiddenCategoryId.Value);

        string where = "is_show=1 and father_id="+fatherId;
        DataRecordTable table = productCategoryBusiness.GetList("Id,category_name,order_by", "order_by", false, 1, 20, where);
        rCategoryList.DataSource = table.Table;
        rCategoryList.DataBind();
    }
    private void rAttributeList_load()
    {
        DataRecordTable table = GetAttributeList();
        if (table != null)
        {
            rAttributeList.DataSource = table.Table;
            rAttributeList.DataBind();
        }
    }
    // 根据目录Id显示相应的属性
    private DataRecordTable GetAttributeList()
    {
        string categoryId = Request.QueryString["categoryId"];
        DataRecordTable table = null;
        if (!string.IsNullOrEmpty(categoryId))
        {
            string where = "category_id=" + categoryId + " and state=1 and is_filter=1 and is_multiple=0";
            string fieldList = "attribute_id,attribute,values_list";
            string orderField = "Id";
            bool orderby = false;
            table = new CategoryAttributeBusiness().GetList(fieldList, orderField, orderby, 1, 10, where);
        }
        return table;
    }
    //显示属性值
    protected void rAttributeList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        string valuesList = DataBinder.Eval(e.Item.DataItem, "values_list").ToString();//属性值
        string attributeName = DataBinder.Eval(e.Item.DataItem,"attribute").ToString();//属性名
        int attributeId = int.Parse(DataBinder.Eval(e.Item.DataItem, "attribute_id").ToString());//属性ID
        LinkButton lbtnRemove = e.Item.FindControl("lBtnRemove") as LinkButton;//移出按钮
        
        string argList = Request.QueryString.ToString();
        if (argList.ToLower().IndexOf(("attribute_" + attributeId).ToLower()) > -1)
            lbtnRemove.Visible = true;


        hiddenAttributeIds.Value += attributeId + ",";//将所用显示属性的ID值放在隐藏字值中，这个值在页面js用到
        Repeater rAttributeValueList = e.Item.FindControl("rAttributeValueList") as Repeater;
        string [] strs = null;
        if(!string.IsNullOrEmpty(valuesList))
            strs = valuesList.Split(',');
        ProductAttribute productAttribute = null; 
        
        List<ProductAttribute> list = new List<ProductAttribute>();

        for (int i = 0; i<strs.Length;i++ )
        {
            if (!string.IsNullOrEmpty(strs[i]))
            {
                productAttribute = new ProductAttribute();
                productAttribute.ValuesList = strs[i];
                productAttribute.Attribute = attributeName;
                productAttribute.Id = attributeId;
                list.Add(productAttribute);
            }
        }
        if (rAttributeValueList != null)
        {
            rAttributeValueList.DataSource = list;
            rAttributeValueList.DataBind();
        }
    }
    //根据参数显示url,重要
    protected string GetUrl(string argName,string argValue)
    {
        if (argName == "categoryId") 
        {
            return argName + "=" + argValue;
        }
        string argList = Request.QueryString.ToString();
        if (argList.ToLower().IndexOf(argName.ToLower()) > -1)
        {
            string[] argAndValue = argList.Split('&');
            string[] args = new string[argAndValue.Length];
            string[] values = new string[argAndValue.Length];
            for (int i = 0; i < argAndValue.Length; i++)
            {
                args[i] = argAndValue[i].Split('=')[0];
                values[i] = argAndValue[i].Split('=')[1];
            }
            StringBuilder url = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                if (argName.ToLower() == args[i].ToLower())
                    url.Append(args[i] + "=" + argValue + "&");
                else
                    url.Append(args[i] + "=" + values[i] + "&");
            }
            return url.ToString().TrimEnd('&');
        }
        else
        {
            return argList + "&" + argName + "=" + argValue;
        }
        
    }

    protected void lBtnCategoryRemove_Click(object sender, EventArgs e)
    {
        string url = RemoveArg("categoryId");
        Response.Redirect("p_list.aspx?"+url);
    }
    //移出筛选值
    private string RemoveArg(string argName)
    {
        string argList = Request.QueryString.ToString();
        if (argList.ToLower().IndexOf(argName.ToLower()) > -1)
        {
            string[] argAndValue = argList.Split('&');
            string[] args = new string[argAndValue.Length];
            string[] values = new string[argAndValue.Length];
            for (int i = 0; i < argAndValue.Length; i++)
            {
                args[i] = argAndValue[i].Split('=')[0];
                values[i] = argAndValue[i].Split('=')[1];
            }
            StringBuilder url = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                if (argName.ToLower() != args[i].ToLower())
                    url.Append(args[i] + "=" + values[i] + "&");
            }
            return url.ToString().TrimEnd('&');
        }
        return "";
    }
    protected void lBtnBrandRemove_Click(object sender, EventArgs e)
    {
        string url = RemoveArg("brandId");
        Response.Redirect("p_list.aspx?" + url);
    }
    protected void rAttributeList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        string attributeId = e.CommandArgument.ToString();
        if (cmdName == "remove")
        {
            string url = RemoveArg("attribute_"+attributeId);
            Response.Redirect("p_list.aspx?"+url);
        }
    }
}
