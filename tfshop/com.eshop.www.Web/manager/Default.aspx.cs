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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.eshop.www.Model;
using com.eshop.www.BLL;

public partial class back_stage_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect(Permissions());
        }
    }
    private string Permissions()
    {
        string page = "admin_list.aspx";
        string loginName = User.Identity.Name;
        ModuleRoleBusiness moduleRoleBusiness = new ModuleRoleBusiness();
        Admin admin = new AdminBusiness().GetEntityByUserName(loginName);

        ModuleRole moduleRole = new ModuleRole() { ModuleId = 5, RoleId = admin.RoleId };
        bool isHave = moduleRoleBusiness.IsHaveRecord(moduleRole);
        if (isHave)
            page = "admin_list.aspx";

        moduleRole.ModuleId = 4;
        isHave = moduleRoleBusiness.IsHaveRecord(moduleRole);
        if (isHave)
            page = "member_info.aspx";

        moduleRole.ModuleId = 3;
        isHave = moduleRoleBusiness.IsHaveRecord(moduleRole);
        if (isHave)
            page = "order.aspx";

        moduleRole.ModuleId = 2;
        isHave = moduleRoleBusiness.IsHaveRecord(moduleRole);
        if (isHave)
            page = "news.aspx";

        moduleRole.ModuleId = 1;
        isHave = moduleRoleBusiness.IsHaveRecord(moduleRole);
        if (isHave)
            page = "product_content.aspx";

        return page;
    }
}
