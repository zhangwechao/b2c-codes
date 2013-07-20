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

public partial class category_2 : System.Web.UI.UserControl
{
    private bool isNew = false;

    public bool IsNew
    {
        get { return isNew; }
        set { isNew = value; }
    }
    private bool isHot = false;

    public bool IsHot
    {
        get { return isHot; }
        set { isHot = value; }
    }
    private bool isDiscount = false;

    public bool IsDiscount
    {
        get { return isDiscount; }
        set { isDiscount = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            rCategoryList_load();
    }
    private void rCategoryList_load()
    {
        string where = "is_show=1 and father_id=0";
        string fieldList = "Id,category_name,order_by,is_show";
        string orderField = "order_by";
        bool orderby = true;
        DataRecordTable table = new ProductCategoryBusiness().GetList(fieldList,orderField,orderby,1,6,where);
        rCategoryList.DataSource = table.Table;
        rCategoryList.DataBind();
    }
    protected void rCategoryList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string id = DataBinder.Eval(e.Item.DataItem,"Id").ToString();

        HyperLink hlcategorylink = e.Item.FindControl("hlcategorylink") as HyperLink;
        if (hlcategorylink != null)
        {
            if (isNew)
                hlcategorylink.NavigateUrl = "p_list.aspx?categoryId="+id+"&isNew=1";
            if (isHot)
                hlcategorylink.NavigateUrl = "p_list.aspx?categoryId="+id+"&isHot=1";
            if (isDiscount)
                hlcategorylink.NavigateUrl = "p_list.aspx?categoryId="+id+"&isDiscount=1";
        }
    }
}
