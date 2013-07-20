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
using System.Web.Security;
using com.eshop.www.Tools;

public partial class back_stage_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    
    protected void button_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string password = txtPassword.Text.Trim();
        Admin admin = new Admin();
        admin.AdminName = userName;
        admin.Password = StringHelper.MD5Encrypt(password);
        bool flag = new AdminBusiness().Validate(admin);
        if (flag)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
            Response.Redirect("manager");
        }
        else
        {
            JavascriptHelper.Alert("用户名或者密码错误，请检查Num lock是否大小写锁定");
            return;
        }
    }
}
