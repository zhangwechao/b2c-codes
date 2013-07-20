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

public partial class back_stage_order : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rData_bind();
            ddlPaymentMethod_load();
            ddlShoppingDate_load();
            ddlShoppingMethod_load();
            ddlStatus_load();
        }
    }
    private void rData_bind()
    {
        StringBuilder where = new StringBuilder();
        string orderNo = Request.QueryString["orderNo"];
        string userName = Request.QueryString["userName"];
        string shoppingMethodId = Request.QueryString["shoppingMethodId"];
        string shoppingDateId = Request.QueryString["shoppingDateId"];
        string statusId = Request.QueryString["statusId"];
        string paymentMethodId = Request.QueryString["paymentMethodId"];
        string beginDate = Request.QueryString["beginDate"];
        string endDate = Request.QueryString["endDate"];

        DataRecordTable table = null;
        if (!string.IsNullOrEmpty(orderNo))
            where.Append(" and Id like '%" + orderNo + "%'");
        if (!string.IsNullOrEmpty(userName))
            where.Append(" and user_name like '%" + userName + "%'");
        if (!string.IsNullOrEmpty(shoppingMethodId))
            where.Append(" and shoppingMethodId =" + shoppingMethodId);
        if (!string.IsNullOrEmpty(shoppingDateId))
            where.Append(" and shoppingDateId=" + shoppingDateId);
        if (!string.IsNullOrEmpty(statusId))
            where.Append(" and statusId=" + statusId);
        if (!string.IsNullOrEmpty(paymentMethodId))
            where.Append(" and paymentMethodId=" + paymentMethodId);
        if (!string.IsNullOrEmpty(beginDate))
            where.Append(" and create_date>'" + beginDate + "'");
        if (!string.IsNullOrEmpty(endDate))
            where.Append(" and create_date<'" + endDate + "'");
        if (where.Length > 0)
            where = where.Remove(0,5);

        string fieldList = "Id,user_name,total_money,discount_money,shoppingMethod,paymentMethod,date_type,create_date,status";
        string orderField = "create_date";
        bool orderBy = true;
        table = new OrderBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
            case "update":
                Response.Redirect("order_detail.aspx?Id=" + id + "&action=update");
                break;
        }
    }
    private void ddlShoppingMethod_load()
    {
        ListItem item = new ListItem("--请选择--","0");
        ddlShoppingMethod.Items.Add(item);

        DataRecordTable table = new ShoppingMethodBusiness().GetList("Id,method","Id",false,1,20,"");
        foreach (DataRow row in table.Table.Rows)
        {
            item = new ListItem(row["method"].ToString(),row["Id"].ToString());
            ddlShoppingMethod.Items.Add(item);
        }
    }
    private void ddlShoppingDate_load()
    {
        ListItem item = new ListItem("--请选择--", "0");
        ddlShoppingDate.Items.Add(item);

        DataRecordTable table = new ShoppingDateBusiness().GetList("Id,date_type","Id",false,1,20,"");
        foreach (DataRow row in table.Table.Rows)
        {
            item = new ListItem(row["date_type"].ToString(), row["Id"].ToString());
            ddlShoppingDate.Items.Add(item);
        }
    }
    private void ddlStatus_load()
    {
        ListItem item = new ListItem("--请选择--", "0");
        ddlStatus.Items.Add(item);

        DataRecordTable table = new OrderStatusBusiness().GetList("Id,status", "Id", false, 1, 20, "");
        foreach (DataRow row in table.Table.Rows)
        {
            item = new ListItem(row["status"].ToString(), row["Id"].ToString());
            ddlStatus.Items.Add(item);
        }
    }
    private void ddlPaymentMethod_load()
    {
        ListItem item = new ListItem("--请选择--", "0");
        ddlPaymentMethod.Items.Add(item);

        DataRecordTable table = new PaymentMethodBusiness().GetList("Id,method", "Id", false, 1, 20, "");
        foreach (DataRow row in table.Table.Rows)
        {
            item = new ListItem(row["method"].ToString(), row["Id"].ToString());
            ddlPaymentMethod.Items.Add(item);
        }
    }

}
