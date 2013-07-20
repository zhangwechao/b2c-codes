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
using com.eshop.www.BLL;
using com.eshop.www.Model;

public partial class help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Help_load();
            DataRead();
        }
    }
    private void DataRead()
    {
        string newsId = Request.QueryString["Id"];
        if (!string.IsNullOrEmpty(newsId))
        {
            NewsContentBusiness business = new NewsContentBusiness();
            NewsContent content = business.GetEntity(int.Parse(newsId));
            lContent.Text = content.Content;
            business.UpdateClickNumber(int.Parse(newsId));
        }
    }
    private void Help_load()
    {
        NewsContentBusiness business = new NewsContentBusiness();
        int number = 3;
        string where = "category_id=12 and is_show=1 and is_delete=0";
        string fieldList = "Id,title,order_by,is_show";
        string orderField = "order_by";
        bool orderby = true;
        DataRecordTable table = business.GetList(fieldList, orderField, orderby, 1, number, where);
        rAboutUs.DataSource = table.Table;
        rAboutUs.DataBind();

        where = "category_id=8 and is_show=1 and is_delete=0";
        table = business.GetList(fieldList, orderField, orderby, 1, number, where);
        rNovice.DataSource = table.Table;
        rNovice.DataBind();

        where = "category_id=9 and is_show=1 and is_delete=0";
        table = business.GetList(fieldList, orderField, orderby, 1, number, where);
        rGuide.DataSource = table.Table;
        rGuide.DataBind();

        where = "category_id=10 and is_show=1 and is_delete=0";
        table = business.GetList(fieldList, orderField, orderby, 1, number, where);
        rPayment.DataSource = table.Table;
        rPayment.DataBind();

        where = "category_id=11 and is_show=1 and is_delete=0";
        table = business.GetList(fieldList, orderField, orderby, 1, number, where);
        rProvisions.DataSource = table.Table;
        rProvisions.DataBind();

    }
}
