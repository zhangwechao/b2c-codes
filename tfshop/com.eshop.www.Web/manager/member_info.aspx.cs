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
using Wuqi.Webdiyer;
using com.eshop.www.Model;
using com.eshop.www.Tools;

public partial class back_stage_member_info : System.Web.UI.Page
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
        string where = "";
        string userName = Request.QueryString["userName"];
        if (!string.IsNullOrEmpty(userName))
            where = " user_name like '%"+userName+"%'";

        string fieldList = "Id,user_name,email,integral,state";
        string orderField = "Id";
        bool orderBy = false;
        DataRecordTable table = new MemberBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
        MemberBusiness memberBusiness = new MemberBusiness();
        string cmd = e.CommandName;
        int id = int.Parse(e.CommandArgument.ToString());

        switch (cmd)
        {
            case "update":
                Response.Redirect("member_detail.aspx?Id="+id+"&action=update");
                break;
            case "delete":
                memberBusiness.Delete(id);
                rData_bind();
                break;
            case "reset":
                Member member = memberBusiness.GetEntity(id);
                member.Password = StringHelper.MD5Encrypt("123456");
                bool success = memberBusiness.Update(member);
                if (success)
                    JavascriptHelper.Alert("重置密码成功");
                rData_bind();
                break;
        }
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        MemberBusiness memberBusiness = new MemberBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                memberBusiness.Delete(Id);
            }
        }
        rData_bind();
    }
}
