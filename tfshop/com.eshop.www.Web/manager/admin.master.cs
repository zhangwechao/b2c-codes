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
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using com.eshop.www.Model;
using com.eshop.www.BLL;

public partial class administrator_admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Dataload();
            rModuleList_load();
        }
    }
    private void Dataload()
    {
        //string cnName = ConfigurationManager.AppSettings["cnname"];
        //string enName = ConfigurationManager.AppSettings["enname"];
        lUserName.Text = "您好：" + Page.User.Identity.Name;
        lRole.Text = new AdminRoleBusiness().GetEntity(new AdminBusiness().GetEntityByUserName(Page.User.Identity.Name).RoleId).Role;
        //lCompanyName.Text = cnName;
        lCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        //lEnglishName.Text = enName;
    }
    private void rModuleList_load()
    {
        string loginName = Page.User.Identity.Name;
        Admin admin = new AdminBusiness().GetEntityByUserName(loginName);
        DataRecordTable table = new ModuleRoleBusiness().GetList("Id,module_id,role_id","Id",false,1,20,"role_id="+admin.RoleId);
        rModuleList.DataSource = table.Table;
        rModuleList.DataBind();

        rFatherModuleList.DataSource = table.Table;
        rFatherModuleList.DataBind();

    }
    protected Module GetModule(int moduleId)
    {
        return new ModuleBusiness().GetEntity(moduleId);
    }
    protected void rFatherModuleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string moduleId = DataBinder.Eval(e.Item.DataItem, "module_id").ToString();
        Repeater rSonModuleList = e.Item.FindControl("rSonModuleList") as Repeater;
        DataRecordTable table = new ModuleBusiness().GetList("Id,module_name,remark,father_id","Id",false,1,20,"father_id="+moduleId);
        if (rSonModuleList != null)
        {
            rSonModuleList.DataSource = table.Table;
            rSonModuleList.DataBind();
        }
    }
}
