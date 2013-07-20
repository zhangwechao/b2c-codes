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
using System.Text;
using com.eshop.www.Model;
using com.eshop.www.BLL;
using Wuqi.Webdiyer;

public partial class back_stage_online_message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rData_bind();
        }
    }
    private void rData_bind()
    {
        StringBuilder where = new StringBuilder();
        string title = Request.QueryString["title"];
        string content = Request.QueryString["content"];
        string isShow = Request.QueryString["isShow"];
        string beginDate = Request.QueryString["beginDate"];
        string endDate = Request.QueryString["endDate"];

        DataRecordTable table = null;
        if (!string.IsNullOrEmpty(title))
            where.Append(" and title like '%"+title+"%'");
        if (!string.IsNullOrEmpty(content))
            where.Append(" and content like '%" + content + "%'");
        if (!string.IsNullOrEmpty(isShow))
            where.Append(" and is_show=" + isShow + "");
        if (!string.IsNullOrEmpty(beginDate))
            where.Append(" and create_date>'" + beginDate + "'");
        if (!string.IsNullOrEmpty(endDate))
            where.Append(" and create_date<'" + endDate + "'");

        if (where.Length > 0)
            where = where.Remove(0, 5);

        string fieldList = "Id,content,title,member_id,is_show,reply_content,create_date";
        string orderField = "Id";
        bool orderBy = false;
        table = new MessageBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
        rData.DataSource = table.Table;
        rData.DataBind();
        AspNetPager1.RecordCount = table.RecordCount;
        AspNetPager2.RecordCount = table.RecordCount;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        AspNetPager page = (AspNetPager)sender;
        AspNetPager2.CurrentPageIndex = page.CurrentPageIndex;
        rData_bind();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        AspNetPager page = (AspNetPager)sender;
        AspNetPager1.CurrentPageIndex = page.CurrentPageIndex;
        rData_bind();
    }
    protected void rData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MessageBusiness messageBusiness = new MessageBusiness();
        string cmd = e.CommandName;
        int id = int.Parse(e.CommandArgument.ToString());

        switch (cmd)
        {
            case "updateShow":
                Button btn = e.CommandSource as Button;
                Message message = messageBusiness.GetEntity(id);
                if (btn != null)
                {
                    if (btn.Text == "显示")
                        message.IsShow = true;
                    else
                        message.IsShow = false;
                    messageBusiness.Update(message);
                    rData_bind();
                }
                break;
            case "delete":
                messageBusiness.Delete(id);
                rData_bind();
                break;
            case "reply":
                Response.Redirect("message_detail.aspx?Id="+id+"&action=update");
                break;
        }
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        MessageBusiness messageBusiness = new MessageBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                messageBusiness.Delete(Id);
            }
        }
        rData_bind();
    }
}
