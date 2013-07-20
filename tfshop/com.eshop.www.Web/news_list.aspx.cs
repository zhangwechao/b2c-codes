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

public partial class news_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rNewsList_load();
        }
    }
    private void rNewsList_load()
    {
        string categoryId = Request.QueryString["categoryId"];
        if (!string.IsNullOrEmpty(categoryId))
        {
            lCategoryName.Text = new NewsCategoryBusiness().GetEntity(int.Parse(categoryId)).CategoryName;
            string where = "category_id="+categoryId+" and is_show=1 and is_delete=0";
            string fieldList = "Id,title,order_by,is_show,create_date";
            string orderField = "order_by";
            bool orderby = true;
            DataRecordTable table = new NewsContentBusiness().GetList(fieldList, orderField, orderby, AspNetPager2.CurrentPageIndex, AspNetPager2.PageSize, where);
            rNewsList.DataSource = table.Table;
            rNewsList.DataBind();
            AspNetPager2.RecordCount = table.RecordCount;
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        rNewsList_load();
    }
}
