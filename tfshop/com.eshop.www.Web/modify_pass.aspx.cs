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
using com.eshop.www.Model;
using com.eshop.www.BLL;
using com.eshop.www.Tools;

public partial class modify_pass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsLogin();
        }
    }
    private void IsLogin()
    {
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        HttpCookie isYes = Request.Cookies[Cookie.IsLoginCookieName];
        if (userName == null || userName.Value.Length == 0 || isYes == null || isYes.Value != "yes")
        {
            HttpCookie cookie = new HttpCookie(Cookie.PrevPageCookieName);
            cookie.Value = Request.RawUrl;
            Response.Cookies.Add(cookie);
            Response.Redirect("login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        string oldPass = txtPass.Text;
        string newPass = txtNewPass.Text;
        string confirmPass = txtConfirmPass.Text;
        MemberBusiness memberBusiness = new MemberBusiness();
        Member member = new Member();
        member.UserName = userName.Value;
        member.Password = StringHelper.MD5Encrypt(oldPass);
        bool isPass = memberBusiness.Validate(member);
        if (!isPass)
        {
            JavascriptHelper.AlertAndRedirect("原密码错误","modify_pass.aspx");
            return;
        }
        if (newPass != confirmPass)
        {
            JavascriptHelper.AlertAndRedirect("密码前后输入不一致", "modify_pass.aspx");
            return;
        }
        member.Password = StringHelper.MD5Encrypt(newPass);
        bool isSuccess = memberBusiness.UpdatePassword(member);
        if (isSuccess)
        {
            JavascriptHelper.AlertAndRedirect("密码修改成功", "modify_pass.aspx");
        }
    }
}
