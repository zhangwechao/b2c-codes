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

public partial class back_stage_favorite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rData_bind();
        }
    }
    private void rData_bind()
    {
        StringBuilder where = new StringBuilder();
        string userName = Request.QueryString["userName"];
        string productName = Request.QueryString["productName"];
        string beginDate = Request.QueryString["beginDate"];
        string endDate = Request.QueryString["endDate"];

        DataRecordTable table = null;
        if (!string.IsNullOrEmpty(userName))
            where.Append(" and user_name like '%" + userName + "%'");
        if (!string.IsNullOrEmpty(productName))
            where.Append(" and product_name like '%" + productName+"%'");
        if (!string.IsNullOrEmpty(beginDate))
            where.Append(" and create_date>'" + beginDate + "'");
        if (!string.IsNullOrEmpty(endDate))
            where.Append(" and create_date<'" + endDate + "'");
        if (where.Length > 0)
            where = where.Remove(0, 5);

        string fieldList = "Id,user_name,product_name,price,sale_price,create_date";
        string orderField = "create_date";
        bool orderBy = true;
        table = new FavoriteBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
        string id = e.CommandArgument.ToString();
        switch (cmd)
        {
            case "delete":
                new FavoriteBusiness().Delete(int.Parse(id));
                rData_bind();
                break;
        }
    }
}
