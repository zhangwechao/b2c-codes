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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.eshop.www.BLL;
using com.eshop.www.Model;
using com.eshop.www.Tools;

public partial class back_stage_message_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                DataRead();
            }
        }
    }

    private void DataRead()
    {
        int id = int.Parse(Request.QueryString["Id"]);
        Message message = new MessageBusiness().GetEntity(id);
        lUserName.Text = new MemberBusiness().GetEntity(message.MemberId).UserName;
        lCreateDate.Text = message.CreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        chkIsShow.Checked = message.IsShow;
        txtTitle.Text = message.Title;
        txtContent.Text = message.Content;
        if (!string.IsNullOrEmpty(message.ReplyUser))
            txtReplyUser.Text = message.ReplyUser;
        txtReplyContent.Text = message.ReplyContent;
    }


    private bool Update()
    {
        MessageBusiness messageBusiness = new MessageBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        Message message = messageBusiness.GetEntity(id);
        message.ReplyUser = txtReplyUser.Text;
        message.IsShow = chkIsShow.Checked;
        message.ReplyContent = txtReplyContent.Text;
        return messageBusiness.Reply(message);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        MessageBusiness messageBusiness = new MessageBusiness();
        bool success = messageBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("信息删除成功", "member_info.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","member_info.aspx");
        }

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("online_message.aspx");
    }
}
