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
using com.eshop.www.Model;
using com.eshop.www.BLL;

public partial class category : System.Web.UI.UserControl
{
    protected int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            rFatherCategory_load();
    }
    private void rFatherCategory_load()
    {
        string where = "is_show=1 and father_id=0";
        string fieldList = "Id,category_name,is_show,father_id,order_by";
        string orderField = "order_by";
        bool orderby = true;
        DataRecordTable table = new ProductCategoryBusiness().GetList(fieldList, orderField, orderby, 1, 14, where);
        rFatherCategory.DataSource = table.Table;
        rFatherCategory.DataBind();
    }
    protected void rFatherCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int Id = int.Parse(DataBinder.Eval(e.Item.DataItem, "Id").ToString());
        string where = "is_show=1 and father_id="+Id;
        string fieldList = "Id,category_name,is_show,father_id,order_by";
        string orderField = "order_by";
        bool orderby = true;
        Repeater rSonCategory = e.Item.FindControl("rSonCategory") as Repeater;
        DataRecordTable table = new ProductCategoryBusiness().GetList(fieldList, orderField, orderby, 1, 20, where);
        if (rSonCategory != null)
        {
            rSonCategory.DataSource = table.Table;
            rSonCategory.DataBind();
        }

        Repeater rBrandList = e.Item.FindControl("rBrandList") as Repeater;
        table = new ProductBrandBusiness().GetList("Id,category_id,brand_name,order_by","order_by",true,1,20,"is_show=1 and category_id="+Id);
        if (rBrandList != null)
        {
            rBrandList.DataSource = table.Table;
            rBrandList.DataBind();
        }
    }
    protected void rSonCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int Id = int.Parse(DataBinder.Eval(e.Item.DataItem, "Id").ToString());
        string where = "is_show=1 and father_id=" + Id;
        string fieldList = "Id,category_name,is_show,father_id,order_by";
        string orderField = "order_by";
        bool orderby = true;
        Repeater rThreeCategory = e.Item.FindControl("rThreeCategory") as Repeater;
        DataRecordTable table = new ProductCategoryBusiness().GetList(fieldList, orderField, orderby, 1, 20, where);
        if (rThreeCategory != null)
        {
            rThreeCategory.DataSource = table.Table;
            rThreeCategory.DataBind();
        }
    }
    protected void rBrandList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string categoryId = DataBinder.Eval(e.Item.DataItem, "category_id").ToString();
        string brandId = DataBinder.Eval(e.Item.DataItem, "Id").ToString();
        HyperLink hlBrandLink = e.Item.FindControl("hlBrandLink") as HyperLink;
        if (hlBrandLink != null)
            hlBrandLink.NavigateUrl = "p_list.aspx?categoryId="+categoryId+"&brandId="+brandId;

    }
}
