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
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Cookie
/// </summary>
public class Cookie
{
    
    public static string UserNameCookieName = "eshop_userName";
    public static string IsLoginCookieName = "eshop_isLogin";
    public static string ShoppingCartCookieName = "eshop_shoppingCart";
    public static string FavoriteCookieName = "eshop_favorite";
    public static string PrevPageCookieName = "eshop_page";
}
