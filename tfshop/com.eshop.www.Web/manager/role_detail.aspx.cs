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
using com.eshop.www.Tools;
using com.eshop.www.BLL;
using com.eshop.www.Model;

public partial class back_stage_role_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            chkModuleList_load();
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                DataRead();
            }
        }
    }
    private void chkModuleList_load()
    {
        DataRecordTable table = new ModuleBusiness().GetList("Id,module_name", "Id", false, 1, 20, "father_id=0");
        chkModuleList.DataSource = table.Table;
        chkModuleList.DataTextField = "module_name";
        chkModuleList.DataValueField = "Id";
        chkModuleList.DataBind();
    }

    private void DataRead()
    {
        int id = int.Parse(Request.QueryString["Id"]);
        AdminRoleBusiness adminRoleBusiness = new AdminRoleBusiness();
        AdminRole adminRole = adminRoleBusiness.GetEntity(id);
        txtRoleName.Text = adminRole.Role;
        txtRemark.Text = adminRole.Remark;
        DataRecordTable table = new ModuleRoleBusiness().GetList("Id,module_id,role_id","Id",false,1,20,"role_id="+id);
        string moduleId = string.Empty;
        foreach (DataRow row in table.Table.Rows)
        {
            moduleId = row["module_id"].ToString();
            chkModuleList.Items.FindByValue(moduleId).Selected = true;
        }
    }

    private bool Save()
    {
        AdminRoleBusiness adminRoleBusiness = new AdminRoleBusiness();
        ModuleRoleBusiness moduleRoleBusiness = new ModuleRoleBusiness();
        string roleName = txtRoleName.Text;
        string remark = txtRemark.Text;
        int Id = adminRoleBusiness.Add(new AdminRole() { Remark = remark, Role = roleName });
        ModuleRole moduleRole = null;
        foreach (ListItem item in chkModuleList.Items)
        {
            if (item.Selected)
            {
                moduleRole = new ModuleRole();
                moduleRole.ModuleId = int.Parse(item.Value);
                moduleRole.RoleId = Id;
                moduleRoleBusiness.Add(moduleRole);
            }
        }
        return true;
    }
    private bool Update()
    {
        int id = int.Parse(Request.QueryString["Id"]);
        AdminRoleBusiness adminRoleBusiness = new AdminRoleBusiness();
        ModuleRoleBusiness moduleRoleBusiness = new ModuleRoleBusiness();
        string roleName = txtRoleName.Text;
        string remark = txtRemark.Text;
        adminRoleBusiness.Update(new AdminRole() { Remark = remark, Role = roleName, Id = id });

        ModuleRole moduleRole = null;
        bool isHave = false;
        foreach (ListItem item in chkModuleList.Items)
        {
            moduleRole = new ModuleRole();
            moduleRole.RoleId = id;
            moduleRole.ModuleId = int.Parse(item.Value);
            isHave = moduleRoleBusiness.IsHaveRecord(moduleRole);
            if (item.Selected)
            {
                if (!isHave)
                    moduleRoleBusiness.Add(moduleRole);
            }
            else
            {
                if (isHave)
                    moduleRoleBusiness.Delete(moduleRole);
            }
        }
        return true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        AdminRoleBusiness adminRoleBusiness = new AdminRoleBusiness();
        bool success = adminRoleBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("信息删除成功", "role.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtRoleName.Text = "";
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("role.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","role.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功","role.aspx");
            }
        }
    }
    
}
