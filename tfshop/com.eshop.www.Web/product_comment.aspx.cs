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

public partial class product_comment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsLogin();
            dlProductList_load();
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
    private void dlProductList_load()
    {
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
        string where = "member_id="+member.Id+" and order_status_id=4 and is_comment=0";
        string orderNo = Request.QueryString["orderNo"];
        if (!string.IsNullOrEmpty(orderNo))
            where += " and order_id='"+orderNo+"'";

        string fieldList = "Id,product_id,member_id,create_date";
        DataRecordTable table = new OrderItemBusiness().GetList2(fieldList,"Id",false,AspNetPager2.CurrentPageIndex,AspNetPager2.PageSize,where);
        dlProductList.DataSource = table.Table;
        dlProductList.DataBind();
    }
    protected ProductImage GetImage(int productId)
    {
        string where = "product_id=" + productId + " and type=1";
        string fieldList = "Id,product_id,image,alt";
        string orderFiled = "Id";
        bool orderby = false;
        DataRecordTable table = new ProductImageBusiness().GetList(fieldList, orderFiled, orderby, 1, 1, where);
        ProductImage productImage = new ProductImage();
        if (table.Table.Rows.Count > 0)
        {
            productImage.Image = table.Table.Rows[0]["image"].ToString();
            productImage.Alt = table.Table.Rows[0]["alt"].ToString();
        }
        return productImage;
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        dlProductList_load();
    }
}
