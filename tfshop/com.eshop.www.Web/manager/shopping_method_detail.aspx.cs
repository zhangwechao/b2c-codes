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

public partial class back_stage_shopping_method_detail : System.Web.UI.Page
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
        ShoppingMethodBusiness shoppingMethodBusiness = new ShoppingMethodBusiness();
        ShoppingMethod shoppingMethod = shoppingMethodBusiness.GetEntity(id);
        txtMethod.Text = shoppingMethod.Method;
        txtRemark.Text = shoppingMethod.Remark;
    }
    
    private bool Save()
    {
        ShoppingMethodBusiness shoppingMethodBusiness = new ShoppingMethodBusiness();

        string method = txtMethod.Text.Trim();
        string remark = txtRemark.Text.Trim();

        ShoppingMethod shoppingMethod = new ShoppingMethod() { Method = method, Remark = remark };
        return shoppingMethodBusiness.Add(shoppingMethod);
    }
    private bool Update()
    {
        ShoppingMethodBusiness shoppingMethodBusiness = new ShoppingMethodBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        string method = txtMethod.Text.Trim();
        string remark = txtRemark.Text.Trim();

        ShoppingMethod shoppingMethod = new ShoppingMethod() { Id=id, Method = method, Remark = remark };
        return shoppingMethodBusiness.Update(shoppingMethod);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        ShoppingMethodBusiness shoppingMethodBusiness = new ShoppingMethodBusiness();
        bool success = shoppingMethodBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("信息删除成功", "shopping_method.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtMethod.Text = "";
        txtRemark.Text = "";
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("shopping_method.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","shopping_method.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功", "shopping_method.aspx");
            }
        }
    }
}
