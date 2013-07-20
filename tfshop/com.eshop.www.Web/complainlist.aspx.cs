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

public partial class complainlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsLogin();
            rMessageList_load();
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
    private void rMessageList_load()
    {
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
        string where = "member_id="+member.Id+" and is_show=1";
        DataRecordTable table = new MessageBusiness().GetList("Id,title,content,create_date,reply_user,reply_content,reply_date","create_date",true,AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize,where);
        rMessageList.DataSource = table.Table;
        rMessageList.DataBind();

        AspNetPager1.RecordCount = table.RecordCount;

    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        rMessageList_load();
    }
    protected void rMessageList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string replyContent = DataBinder.Eval(e.Item.DataItem,"reply_content").ToString();
        Literal lReplyUser = e.Item.FindControl("lReplyUser") as Literal;
        Literal lReplyDate = e.Item.FindControl("lReplyDate") as Literal;
        Literal lReplyContent = e.Item.FindControl("lReplyContent") as Literal;
        if (string.IsNullOrEmpty(replyContent))
        {
            lReplyContent.Text = "暂未回复";
        }
        else
        {
            lReplyUser.Text = "回复人："+DataBinder.Eval(e.Item.DataItem, "reply_user").ToString();
            lReplyDate.Text = "回复时间：" + DateTime.Parse(DataBinder.Eval(e.Item.DataItem, "reply_date").ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            lReplyContent.Text = replyContent;
        }
            
    }
}
