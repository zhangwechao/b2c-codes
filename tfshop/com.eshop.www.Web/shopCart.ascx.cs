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
using System.Text.RegularExpressions;
using com.eshop.www.Tools;
using com.eshop.www.BLL;

public partial class shopCart : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   
            DataRead();
            rProductList_load();
            
        }
    }
    private void DataRead()
    {
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
        MemberLevel level = new MemberLevelBusiness().GetEntityByIntegral(member.Integral);
        if (level.Discount == 0)
            lDiscount2.Text = "无折扣";
        else
            lDiscount2.Text = ((1 - level.Discount) * 10).ToString("0.0") + "折";
    }
    private void rProductList_load()
    {
        ShoppingCart cart = new ShoppingCart();
        HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
        List<ProductDetail> list = cart.GetListByCookie(cookie);
        ltotalMoney.Text = cart.GetTotalMoney(cookie).ToString("0");

        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
        MemberLevel level = new MemberLevelBusiness().GetEntityByIntegral(member.Integral);

        lDiscountMoney.Text = (cart.GetTotalMoney(cookie) * level.Discount).ToString("0.0");
        lEndTotal.Text = (cart.GetTotalMoney(cookie) * (1 - level.Discount)).ToString("0.0");
        lGetIntegral.Text = float.Parse(lEndTotal.Text).ToString("0");

        if (list.Count == 0)
            imgBalance.Visible = false;
        rProductList.DataSource = list;
        rProductList.DataBind();
    }
    protected void rProductList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        ShoppingCart cart = new ShoppingCart();
        HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
        int productId = int.Parse(e.CommandArgument.ToString());
        switch (cmdName)
        { 
            case "div":
                cart.Div(productId, 1, cookie);
                break;
            case "add":
                cart.Add(productId,1,cookie);
                break;
            case "delete":
                cart.Delete(productId, cookie);
                break;
        }
        Response.Cookies.Add(cookie);
        rProductList_load();
    }
    protected void lBtnDeleteCart_Click(object sender, EventArgs e)
    {
        ShoppingCart cart = new ShoppingCart();
        HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
        cart.Delete(cookie);
        Response.Cookies.Add(cookie);
        rProductList_load();
    }

    protected void txtNumber_TextChanged(object sender, EventArgs e)
    {
        string reg = @"^([1-9])+$";
        TextBox txtNumber = (TextBox)sender;
        string number = txtNumber.Text;
        if (!Regex.IsMatch(number, reg))
        {
            JavascriptHelper.AlertAndRedirect("请在数量处输入大于0的数字", "shoppingCart.aspx");
        }
        else
        {
            ShoppingCart cart = new ShoppingCart();
            HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
            int productId = int.Parse(txtNumber.ToolTip);
            cart.Replace(productId, int.Parse(number), cookie);
            Response.Cookies.Add(cookie);
            rProductList_load();
        }
        
    }
}
