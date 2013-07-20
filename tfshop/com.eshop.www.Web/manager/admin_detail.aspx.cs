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

public partial class back_stage_admin_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlRole_load();
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                DataRead();
            }
        }
    }
    private void ddlRole_load()
    {
        DataRecordTable table = new AdminRoleBusiness().GetList("Id,role","Id",false,1,20,"");
        ddlRole.DataSource = table.Table;
        ddlRole.DataTextField = "role";
        ddlRole.DataValueField = "Id";
        ddlRole.DataBind();
    }
    private void DataRead()
    {
        int id = int.Parse(Request.QueryString["Id"]);
        AdminBusiness adminBusiness = new AdminBusiness();
        Admin admin = adminBusiness.GetEntity(id);
        txtAdminName.Text = admin.AdminName;
        txtRealName.Text = admin.RealName;
        chkState.Checked = admin.State;
        ddlRole.SelectedValue = admin.RoleId.ToString();
    }

    private bool Save()
    {
        AdminBusiness adminBusiness = new AdminBusiness();

        string adminName = txtAdminName.Text.Trim();
        string realName = txtRealName.Text.Trim();
        string password = txtPassword.Text.Trim();
        string confirmPassword = txtConfirPass.Text.Trim();
        int roleId = int.Parse(ddlRole.SelectedValue);
        bool state = chkState.Checked;
        if (password.Length == 0)
        {
            JavascriptHelper.Alert("请输入密码");
            return false;
        }
        if (password != confirmPassword)
        {
            JavascriptHelper.Alert("密码前后输入不一致");
            return false;
        }

        Admin admin = new Admin() { AdminName = adminName, Password = StringHelper.MD5Encrypt(password), RealName = realName, RoleId = roleId, State = state };
        return adminBusiness.Add(admin);
    }
    private bool Update()
    {
        AdminBusiness adminBusiness = new AdminBusiness();

        int id = int.Parse(Request.QueryString["Id"]);
        string adminName = txtAdminName.Text.Trim();
        string realName = txtRealName.Text.Trim();
        string password = txtPassword.Text.Trim();
        string confirmPassword = txtConfirPass.Text.Trim();
        int roleId = int.Parse(ddlRole.SelectedValue);
        bool state = chkState.Checked;

        Admin admin = adminBusiness.GetEntity(id);
        admin.AdminName = adminName;
        admin.RealName = realName;
        admin.State = state;
        admin.RoleId = roleId;
        admin.Id = id;
        if (password.Length > 0)
            admin.Password = StringHelper.MD5Encrypt(password);
        
        return adminBusiness.Update(admin);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        AdminBusiness adminBusiness = new AdminBusiness();
        bool success = adminBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("信息删除成功", "admin_list.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtAdminName.Text = "";
        txtRealName.Text = "";
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin_list.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","admin_list.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功","admin_list.aspx");
                btnReset_Click(null, null);
            }
        }
    }
}
