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

public partial class favorite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsLogin();
            DataLoad();
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
    private void DataLoad()
    {
        HttpCookie favorite = Request.Cookies[Cookie.FavoriteCookieName];
        if (favorite != null && !string.IsNullOrEmpty(favorite.Value))
        {
            int productId = int.Parse(favorite.Value);
            HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
            Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
            FavoriteBusiness business = new FavoriteBusiness();
            Favorite fav = new Favorite() { MemberId=member.Id, ProductId=productId };
            bool isHaveProduct = business.IsHaveSameProduct(fav);
            if (isHaveProduct)
            {
                favorite.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(favorite);
                JavascriptHelper.AlertAndRedirect("你刚才所加收藏夹的产品在收藏夹已经存在","favorite.aspx");
            }
            else
            {
                favorite.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(favorite);
                business.Add(fav);
            }
            
        }
    }
}
