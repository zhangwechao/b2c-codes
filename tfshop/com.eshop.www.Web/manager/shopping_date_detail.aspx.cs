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
using com.eshop.www.BLL;
using com.eshop.www.Model;
using com.eshop.www.Tools;

public partial class back_stage_shopping_date_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                DataRead();
            }
        }
    }

    private void DataRead()
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ShoppingDateBusiness shoppingDateBusiness = new ShoppingDateBusiness();
        ShoppingDate shoppingDate = shoppingDateBusiness.GetEntity(id);
        txtDateType.Text = shoppingDate.DateType;
        txtRemark.Text = shoppingDate.Remark;
    }

    private bool Save()
    {
        ShoppingDateBusiness shoppingDateBusiness = new ShoppingDateBusiness();

        string dateType = txtDateType.Text.Trim();
        string remark = txtRemark.Text.Trim();

        ShoppingDate shoppingDate = new ShoppingDate() { DateType = dateType, Remark = remark };
        return shoppingDateBusiness.Add(shoppingDate);
    }
    private bool Update()
    {
        ShoppingDateBusiness shoppingDateBusiness = new ShoppingDateBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        string dateType = txtDateType.Text.Trim();
        string remark = txtRemark.Text.Trim();

        ShoppingDate shoppingDate = new ShoppingDate() { Id = id, DateType = dateType, Remark = remark };
        return shoppingDateBusiness.Update(shoppingDate);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ShoppingDateBusiness shoppingDateBusiness = new ShoppingDateBusiness();
        bool success = shoppingDateBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("信息删除成功", "shopping_date.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtDateType.Text = "";
        txtRemark.Text = "";
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("shopping_date.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功", "shopping_date.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功", "shopping_date.aspx");
            }
        }
    }
}
