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
using com.eshop.www.Model;
using com.eshop.www.BLL;
using Wuqi.Webdiyer;

public partial class back_stage_member_level : System.Web.UI.Page
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
        string levelName = Request.QueryString["levelName"];
        if (!string.IsNullOrEmpty(levelName))
            where = " level_name like '%" + levelName + "%'";

        string fieldList = "Id,min_integral,max_integral,level_name,discount";
        string orderField = "Id";
        bool orderBy = false;
        DataRecordTable table = new MemberLevelBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
        MemberLevelBusiness memberLevelBusiness = new MemberLevelBusiness();
        string cmd = e.CommandName;
        int id = int.Parse(e.CommandArgument.ToString());

        switch (cmd)
        {
            case "update":
                Response.Redirect("member_level_detail.aspx?Id=" + id + "&action=update");
                break;
            case "delete":
                memberLevelBusiness.Delete(id);
                rData_bind();
                break;
        }
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        MemberLevelBusiness memberLevelBusiness = new MemberLevelBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                memberLevelBusiness.Delete(Id);
            }
        }
        rData_bind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("member_level_detail.aspx");
    }
}
