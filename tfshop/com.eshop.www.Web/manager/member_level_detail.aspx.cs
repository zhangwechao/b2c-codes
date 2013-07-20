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

public partial class member_level_detail : System.Web.UI.Page
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
        MemberLevelBusiness memberLevelBusiness = new MemberLevelBusiness();
        MemberLevel memberLevel = memberLevelBusiness.GetEntity(id);
        txtLevelName.Text = memberLevel.LevelName;
        txtMaxIntegral.Text = memberLevel.MaxIntegral.ToString();
        txtMinIntegral.Text = memberLevel.MinIntegral.ToString();
        txtDiscount.Text = memberLevel.Discount.ToString();
    }

    private bool Save()
    {
        MemberLevelBusiness memberLevelBusiness = new MemberLevelBusiness();

        string levelName = txtLevelName.Text.Trim();
        string maxIntegral = txtMaxIntegral.Text.Trim();
        string minIntegral = txtMinIntegral.Text.Trim();
        string discount = txtDiscount.Text.Trim();

        MemberLevel memberLevel = new MemberLevel() { Discount = float.Parse(discount), MinIntegral=int.Parse(minIntegral), MaxIntegral=int.Parse(maxIntegral), LevelName=levelName };
        return memberLevelBusiness.Add(memberLevel);
    }
    private bool Update()
    {
        MemberLevelBusiness memberLevelBusiness = new MemberLevelBusiness();
        int id = int.Parse(Request.QueryString["Id"]);
        string levelName = txtLevelName.Text.Trim();
        string maxIntegral = txtMaxIntegral.Text.Trim();
        string minIntegral = txtMinIntegral.Text.Trim();
        string discount = txtDiscount.Text.Trim();

        MemberLevel memberLevel = new MemberLevel() { Discount = float.Parse(discount), MinIntegral = int.Parse(minIntegral), MaxIntegral = int.Parse(maxIntegral), LevelName = levelName,Id=id };
        return memberLevelBusiness.Update(memberLevel);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["Id"]);
        MemberLevelBusiness memberLevelBusiness = new MemberLevelBusiness();
        bool success = memberLevelBusiness.Delete(id);
        if (success)
            JavascriptHelper.AlertAndRedirect("信息删除成功", "member_level.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtLevelName.Text = "";
        txtMaxIntegral.Text = "";
        txtMinIntegral.Text = "";
        txtDiscount.Text = "";
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("member_level.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","member_level.aspx");
        }
        else
        {
            if (Save())
            {
                JavascriptHelper.AlertAndRedirect("增加信息成功","member_level.aspx");
                btnReset_Click(null, null);
            }
        }
    }
}
