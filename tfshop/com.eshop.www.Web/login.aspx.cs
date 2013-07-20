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
using com.eshop.www.Tools;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = Request.Form["type"];
            if (type == "login")
                Validata_login();
            else if (type == "reg")
                Registe();
        }
            
    }
    private void Validata_login()
    {
        string userName = Request.Form["userName"];
        string password = Request.Form["password"];
        string validataCode = Request.Form["validataCode"];
        string remember = Request.Form["remember"];
        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(validataCode) && !string.IsNullOrEmpty(remember))
        {
            if (Session["_ValidateCode"] == null)
            {
                Response.Write("codeExpire");
                Response.End();
            }
            string sessionCode = Session["_ValidateCode"].ToString();
            if (sessionCode.ToLower() != validataCode.ToLower())
            {
                Response.Write("errorCode");
                Response.End();
            }
            password = StringHelper.MD5Encrypt(password);
            Member member = new Member() { UserName=userName, Password=password };
            bool isPass = new MemberBusiness().Validate(member);
            if (!isPass)
            {
                Response.Write("errorpass");
                Response.End();
            }
            else 
            {
                HttpCookie cookie = new HttpCookie(Cookie.UserNameCookieName,userName);
                HttpCookie isLogin = new HttpCookie(Cookie.IsLoginCookieName,"yes");
                //ShoppingCart cart = new ShoppingCart();
                //HttpCookie cartCookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
                //cart.Delete(cartCookie);
                //HttpCookie favorite = Request.Cookies[Cookie.FavoriteCookieName];
                //if (favorite != null)
                //{
                //    favorite.Value = "";
                //    favorite.Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies.Add(favorite);
                //}
                if (remember == "1")
                    cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
                Response.Cookies.Add(isLogin);
                //Response.Cookies.Add(cartCookie);
                Response.Write("ok");
                Response.End();
            }
        }
    }
    private void Registe()
    {
        string userName = Request.Form["userName"];
        string password = Request.Form["password"];
        string email = Request.Form["email"];
        string remember = Request.Form["remember"];
        string mobile = Request.Form["mobile"];
        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(remember) && !string.IsNullOrEmpty(mobile))
        {
            password = StringHelper.MD5Encrypt(password);
            MemberBusiness business = new MemberBusiness();
            Member member = new Member() { Email=email, Integral=0, Password=password,UserName=userName, State=true,Mobile=mobile };
            bool isSameUserName = business.IsSameUserName(userName);
            if(isSameUserName)
            {
                Response.Write("sameName");
                Response.End();
            }
            bool isSameEmail = business.IsSameEmail(email);
            if (isSameEmail)
            {
                Response.Write("sameEmail");
                Response.End();
            }
            bool success = new MemberBusiness().Add(member);
            if (success)
            {
                HttpCookie cookie = new HttpCookie(Cookie.UserNameCookieName, userName);
                HttpCookie isLogin = new HttpCookie(Cookie.IsLoginCookieName, "yes");
                //ShoppingCart cart = new ShoppingCart();
                //HttpCookie cartCookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
                //cart.Delete(cartCookie);
                //HttpCookie favorite = Request.Cookies[Cookie.FavoriteCookieName];
                //if (favorite != null)
                //{
                //    favorite.Value = "";
                //    favorite.Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies.Add(favorite);
                //}
                if (remember == "1")
                    cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
                Response.Cookies.Add(isLogin);
                //Response.Cookies.Add(cartCookie);
                Response.Write("ok");
                Response.End();
            }
        }
    }
}
