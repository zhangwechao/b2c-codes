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
using com.eshop.www.Tools;

public partial class member_center : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsLogin();
            rOrderList_load();
        }
    }
    private void IsLogin()
    {
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        HttpCookie isYes = Request.Cookies[Cookie.IsLoginCookieName];
        if (userName == null || userName.Value.Length==0 || isYes==null || isYes.Value != "yes")
            Response.Redirect("login.aspx");
    }
    
    private void rOrderList_load()
    {
        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        string where = "user_name='"+userName.Value+"'";
        string fieldList = "Id,total_money,discount_money,user_name,create_date,paymentMethodId,statusId,status,invoice_head,modify_money,paymentMethod";
        string orderField = "create_date";
        bool orderBy = true;
        DataRecordTable table = new OrderBusiness().GetList(fieldList,orderField,orderBy,AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize,where);
        if (table.Table.Rows.Count > 0)
        {
            string orderNo = table.Table.Rows[0]["Id"].ToString();
            Detail(orderNo);
        }
        rOrderList.DataSource = table.Table;
        rOrderList.DataBind();
        AspNetPager1.RecordCount = table.RecordCount;

    }
    protected void rOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int statusId = int.Parse(DataBinder.Eval(e.Item.DataItem,"statusId").ToString());
        string orderNo = DataBinder.Eval(e.Item.DataItem, "Id").ToString();
        int paymentMethodId = int.Parse(DataBinder.Eval(e.Item.DataItem, "paymentMethodId").ToString());
        LinkButton lbtnDelete = e.Item.FindControl("lbtnDelete") as LinkButton;
        LinkButton lbtnPayment = e.Item.FindControl("lbtnPayment") as LinkButton;
        LinkButton lbtnComment = e.Item.FindControl("lbtnComment") as LinkButton;
        LinkButton lbtnReceiveGoods = e.Item.FindControl("lbtnReceiveGoods") as LinkButton;
        OrderItemBusiness orderItemBusiness = new OrderItemBusiness();

        //3为待付款
        if (statusId == 3)
        {
            lbtnDelete.Visible = true;
            lbtnPayment.Visible = true;
        }
        //如果是货到付款并且等待发货，就可以删除
        if (paymentMethodId == 1 && statusId == 1)
        {
            lbtnDelete.Visible = true;
        }
        //2为已经发货
        if (statusId == 2)
            lbtnReceiveGoods.Visible = true;
        //4为已经完成
        if (statusId == 4)
        {
            int num = orderItemBusiness.GetCommentNumByOrderNo(orderNo,false);
            if (num > 0)
                lbtnComment.Visible = true;
            else   
            {
                lbtnComment.Text = "评论完成";
                lbtnComment.Enabled = false;
                lbtnComment.Visible = true;
            }
        }
            
    }
    private void Detail(string orderNo)
    {
        Order order = null;
        OrderBusiness orderBusiness = new OrderBusiness();
        OrderItemBusiness orderItemBusiness = new OrderItemBusiness();
        order = orderBusiness.GetEntity(orderNo);
        PaymentMethod paymentMethod = new PaymentMethodBusiness().GetEntity(order.PaymentMethodId);
        ShoppingDate shoppingDate = new ShoppingDateBusiness().GetEntity(order.ShippingDateId);
        ShoppingMethod shoppingMethod = new ShoppingMethodBusiness().GetEntity(order.ShippingMethodId);

        lOrderNo.Text = orderNo.ToUpper();
        ltotalMoney.Text = "总金额：" + (order.TotalMoney + order.DiscountMoney).ToString("0.0") + "元";
        ldiscountMoney.Text = "-折扣" + order.DiscountMoney.ToString("0.0") + "元";
        lendMoney.Text = "=" + order.TotalMoney.ToString("0.0") + "元";

        if (order.ModifyMoney != null)
        {
            lmodifyMoney.Text = "&nbsp;修改后金额：" + order.ModifyMoney.Value.ToString("0.0") + "元";
            lmodifySeason.Text = "<br/>修改原因：" + order.ModifySeason;
        }
        else
        {
            lmodifyMoney.Text = "";
            lmodifySeason.Text = "";
        }

        lPaymentMethod.Text = "支付方式："+paymentMethod.Method + "，" + paymentMethod.Remark;
        lShoppingDate.Text = "接收时间："+shoppingDate.DateType;
        lShoppingMethod.Text = "送货方式：" + shoppingMethod.Method + "，" + shoppingMethod.Remark;
        lReceiver.Text = "接收人：" + order.Receiver + "，" + "电话："+order.Tel+"，地址："+order.Address+"";

        string where = "order_id='" + orderNo + "'";
        string fieldList = "Id,order_id,product_id,price,quantity,total_money";
        DataRecordTable table = orderItemBusiness.GetList(fieldList, "Id", false, 1, 20, where);
        rItemList.DataSource = table.Table;
        rItemList.DataBind();
    }
    protected void rOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        string orderNo = e.CommandArgument.ToString();
        Order order = null;
        OrderBusiness orderBusiness = new OrderBusiness();
        OrderItemBusiness orderItemBusiness = new OrderItemBusiness();
        if (cmdName == "detail")
        {
            Detail(orderNo);
        }
        if (cmdName == "payment")
        {
            order = orderBusiness.GetEntity(orderNo);
            //2为支付宝
            if (order.PaymentMethodId == 2)
                Response.Redirect("https://lab.alipay.com/consume/record/index.htm");
             //3为银行卡
            else if (order.PaymentMethodId == 3)
                Response.Redirect("http://www.abchina.com/cn/");
        }
        if (cmdName == "receive")
        {
            Receive(orderNo);
        }
        if (cmdName == "comment")
        {
            Response.Redirect("product_comment.aspx?orderNo="+orderNo);
        }
        if (cmdName == "delete")
        {
            bool issuccess = new OrderBusiness().Delete(orderNo);
            if (issuccess)
                rOrderList_load();
        }
    }
    private void Receive(string orderNo)
    {
        OrderBusiness orderBusiness = new OrderBusiness();
        Order order = orderBusiness.GetEntity(orderNo);
        bool isSuccess = orderBusiness.ConfirmReceiveBusiness(orderNo);
        if(isSuccess)
        {
            if (order.PaymentMethodId == 2)
                Response.Redirect("https://lab.alipay.com/consume/record/index.htm");
            else
            {
                int integral = new MemberBusiness().GetEntity(order.MemberId).Integral;
                JavascriptHelper.AlertAndRedirect("你获得积分" + integral + "分,积分越多，折扣越低，继续努力吧！", "member_center.aspx");
            }
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        rOrderList_load();
    }
}
