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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using com.eshop.www.Model;
using Wuqi.Webdiyer;
using com.eshop.www.BLL;

public partial class product_comment : System.Web.UI.Page
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
        string content = Request.QueryString["content"];
        string isShow = Request.QueryString["isShow"];
        string beginDate = Request.QueryString["beginDate"];
        string endDate = Request.QueryString["endDate"];
        string productId = Request.QueryString["productId"];

        DataRecordTable table = null;
        if (!string.IsNullOrEmpty(productId))
            where.Append(" and product_id="+productId);
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

        string fieldList = "Id,content,IP,user_name,score,product_id,is_show,create_date";
        string orderField = "Id";
        bool orderBy = false;
        table = new ProductCommentBusiness().GetList(fieldList, orderField, orderBy, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, where.ToString());
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
        ProductCommentBusiness productCommentBusiness = new ProductCommentBusiness();
        string cmd = e.CommandName;
        int id = int.Parse(e.CommandArgument.ToString());

        switch (cmd)
        {
            case "updateShow":
                Button btn = e.CommandSource as Button;
                ProductComment comment = productCommentBusiness.GetEntity(id);
                if (btn != null)
                {
                    if (btn.Text == "显示")
                        comment.IsShow = true;
                    else
                        comment.IsShow = false;
                    productCommentBusiness.Update(comment);
                    rData_bind();
                }
                break;
            case "delete":
                productCommentBusiness.Delete(id);
                rData_bind();
                break;
        }
    }
    protected void btnAllDel_Click(object sender, EventArgs e)
    {
        ProductCommentBusiness productCommentBusiness = new ProductCommentBusiness();
        CheckBox chkselect = null;
        foreach (RepeaterItem item in rData.Items)
        {
            chkselect = item.FindControl("chkselect") as CheckBox;
            if (chkselect != null && chkselect.Checked)
            {
                int Id = int.Parse(chkselect.ToolTip);
                productCommentBusiness.Delete(Id);
            }
        }
        rData_bind();
    }
    protected string GetProductName(int productId)
    {
        return new ProductDetailBusiness().GetEntity(productId).ProductName;
    }
}
