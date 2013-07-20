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

public partial class news : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataRead();
        }
    }
    private void DataRead()
    {
        string newsId = Request.QueryString["Id"];
        if (!string.IsNullOrEmpty(newsId))
        {
            NewsContentBusiness business = new NewsContentBusiness();
            NewsContent content = business.GetEntity(int.Parse(newsId));
            if (content.CategoryId == 13)
                lContent.Text = content.Content;
            else
            {
                ltitle.Text = content.Title;
                lCreateDate.Text = "时间："+content.CreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                lContent.Text = content.Content;
            }
            lCategoryName.Text = new NewsCategoryBusiness().GetEntity(content.CategoryId).CategoryName;
            business.UpdateClickNumber(int.Parse(newsId));
        }
    }
}
