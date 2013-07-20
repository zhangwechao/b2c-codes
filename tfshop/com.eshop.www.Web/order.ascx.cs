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
using System.Collections.Generic;
using com.eshop.www.Model;
using com.eshop.www.BLL;

public partial class order : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            rShoppcart_load();
    }
    private void rShoppcart_load()
    {
        ShoppingCart cart = new ShoppingCart();
        HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
        List<ProductDetail> list = cart.GetListByCookie(cookie);
        ltotalMoney.Text = cart.GetTotalMoney(cookie).ToString("0");

        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
        MemberLevel level = new MemberLevelBusiness().GetEntityByIntegral(member.Integral);

        if (level.Discount == 0)
            lDiscount2.Text = "无折扣";
        else
            lDiscount2.Text = ((1 - level.Discount) * 10).ToString("0.0") + "折";

        lDiscountMoney.Text = (cart.GetTotalMoney(cookie) * level.Discount).ToString("0.0");
        lEndTotal.Text = (cart.GetTotalMoney(cookie) * (1 - level.Discount)).ToString("0.0");
        rShoppcart.DataSource = list;
        rShoppcart.DataBind();
    }
}
