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
using com.eshop.www.Model;
using com.eshop.www.BLL;
using System.Data;
using com.eshop.www.Tools;

public partial class back_stage_product_relate_article : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rRelate_load();
            rYesRelate_load();
        }
    }
    private void rRelate_load()
    {
        string title = Request.QueryString["title"];
        string where = "is_show=1";
        if (!string.IsNullOrEmpty(title))
            where += " and title like '%"+title+"%'";
        DataRecordTable table = new ProductArticleBusiness().GetList("Id,title,order_by,is_show","order_by",false,AspNetPager2.CurrentPageIndex,AspNetPager2.PageSize,where);
        rRelate.DataSource = table.Table;
        rRelate.DataBind();
        AspNetPager2.RecordCount = table.RecordCount;
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        rRelate_load();
    }
    private void rYesRelate_load()
    {
        int productId = int.Parse(Request.QueryString["productId"]);
        lProductName.Text = new ProductDetailBusiness().GetEntity(productId).ProductName;
        string where = "product_id="+productId;
        DataTable table = new ProductRelateArticleBusiness().GetListByProductId(productId);
        rYesRelate.DataSource = table;
        rYesRelate.DataBind();
    }
    protected void rRelate_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        int productId = int.Parse(Request.QueryString["productId"]);
        int articleId = int.Parse(e.CommandArgument.ToString());
        if (cmdName == "relate")
        {
            ProductRelateArticleBusiness business = new ProductRelateArticleBusiness();
            bool isHasSame = business.IsSameRecord(productId,articleId);
            if(isHasSame)
            {
                JavascriptHelper.Alert("已经存在相关的文章");
                return;
            }
            business.Add(new ProductRelateArticle() { ProductId=productId, RelatedArticleId=articleId});
            rYesRelate_load();
        }

    }
    protected void rYesRelate_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        int Id = int.Parse(e.CommandArgument.ToString());
        if (cmdName == "NoRelate")
        {
            ProductRelateArticleBusiness business = new ProductRelateArticleBusiness();
            business.Delete(Id);
            rYesRelate_load();
        }
    }
}
