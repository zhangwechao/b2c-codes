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
using com.eshop.www.Tools;
using System.Text;
using System.Xml;
using Com.Alipay;
using System.Collections.Generic;

public partial class back_stage_order_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlStatus_load();
            string action = Request.QueryString["action"];
            if (action == "update")
            {
                DataRead();
            }
        }
    }
    private void DataRead()
    {
        OrderBusiness orderBusiness = new OrderBusiness();
        ShoppingDateBusiness shoppingDateBusiness = new ShoppingDateBusiness();
        ShoppingMethodBusiness shoppingMethodBusiness = new ShoppingMethodBusiness();
        PaymentMethodBusiness paymentMethodBusiness = new PaymentMethodBusiness();
        OrderStatusBusiness orderStatus = new OrderStatusBusiness();
        MemberBusiness memberBusiness = new MemberBusiness();
        MemberLevelBusiness memberLevelBusiness = new MemberLevelBusiness();
        


        string Id = Request.QueryString["Id"];
        Order order = orderBusiness.GetEntity(Id);
        Member member = memberBusiness.GetEntity(order.MemberId);
        MemberLevel level = memberLevelBusiness.GetEntityByIntegral(member.Integral);

        lOrderNo.Text = order.Id;
        lUserName.Text = member.UserName;
        lCreateDate.Text = order.CreateDate.Value.ToString("yyyy-M-d HH:mm:ss");

        lReceiver.Text = order.Receiver;
        lAddress.Text = order.Address;
        lTel.Text = order.Tel;

        lIsInvoice.Text = string.IsNullOrEmpty(order.InvoiceHead) ? "不开发票" : "是";
        lInvoiceHead.Text = string.IsNullOrEmpty(order.InvoiceHead) ? "不开发票" : order.InvoiceHead;
        ddlStatus.SelectedValue = order.OrderStatusId.ToString();

        ShoppingMethod shoppingMethod = shoppingMethodBusiness.GetEntity(order.ShippingMethodId);
        PaymentMethod paymentMethod = paymentMethodBusiness.GetEntity(order.PaymentMethodId);
        lShoppingMethod.Text = shoppingMethod.Method;
        lPaymentMethod.Text = paymentMethod.Method;
        lDateType.Text = shoppingDateBusiness.GetEntity(order.ShippingDateId).DateType;

        DataRecordTable table = new OrderItemBusiness().GetList("product_id,quantity,price,total_money","Id",false,1,20,"order_id='"+order.Id+"'");
        rShoppingCart.DataSource = table.Table;
        rShoppingCart.DataBind();

        lTotalMoney.Text = (order.TotalMoney + order.DiscountMoney).ToString("0.0");
        lDiscountMoney.Text = order.DiscountMoney.ToString("0.0");
        lDiscount.Text = ((1-level.Discount) * 10).ToString("0.0") + "折";
        lEndMoney.Text = order.TotalMoney.ToString("0.0");


        if (order.ModifyMoney!=null)
            txtModifyMoney.Text = order.ModifyMoney.Value.ToString("0.0");
        txtModifySeason.Text = order.ModifySeason;

        if (order.OrderStatusId != 3)
        {
            txtModifyMoney.Enabled = false;
            txtModifySeason.Enabled = false;
        }
    }
    private void ddlStatus_load()
    {
        DataRecordTable table = new OrderStatusBusiness().GetList("Id,status","Id",false,1,20,"");
        ddlStatus.DataSource = table.Table;
        ddlStatus.DataTextField = "status";
        ddlStatus.DataValueField = "Id";
        ddlStatus.DataBind();
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "update")
        {
            if (Update())
                JavascriptHelper.AlertAndRedirect("修改信息成功","order.aspx");
        }
    }
    private bool Update()
    {
        OrderBusiness orderBusiness = new OrderBusiness();
        string Id = Request.QueryString["Id"];
        Order order = orderBusiness.GetEntity(Id);
        order.OrderStatusId = int.Parse(ddlStatus.SelectedValue);
        //如果是已经发货和付款方式是支付宝付款，则发送到支付宝
        if (order.OrderStatusId == 2 && order.PaymentMethodId == 2)
            SendMessageAlipay(order);

        if(txtModifyMoney.Text.Trim().Length>0)
            order.ModifyMoney = float.Parse(txtModifyMoney.Text.Trim());
        if(txtModifySeason.Text.Trim().Length>0)
            order.ModifySeason = txtModifySeason.Text.Trim();

        return orderBusiness.Update(order);

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("order.aspx");
    }
    private bool SendMessageAlipay(Order order)
    {
        //必填参数//

        //支付宝交易号，支付宝根据商户请求，创建订单生成的支付宝交易号。
        string trade_no = order.TradeNo;


        //物流公司名称，物流公司名称
        string logistics_name = new ShoppingMethodBusiness().GetEntity(order.ShippingMethodId).Method;


        //物流发货单号
        string invoice_no = order.Id;


        //物流发货时的运输类型，三个值可选：POST（平邮）、EXPRESS（快递）、EMS（EMS）
        string transport_type = "EXPRESS";
        //建议与创建交易时选择的运输类型一致

        ////////////////////////////////////////////////////////////////////////////////////////////////

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("trade_no", trade_no);
        sParaTemp.Add("logistics_name", logistics_name);
        sParaTemp.Add("invoice_no", invoice_no);
        sParaTemp.Add("transport_type", transport_type);

        //请在这里加上商户的业务逻辑程序代码

        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——

        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

        //构造确认发货接口，无需修改
        Service ali = new Service();
        XmlDocument xmlDoc = ali.Send_goods_confirm_by_platform(sParaTemp);
        //StringBuilder sbxml = new StringBuilder();
        string nodeIs_success = xmlDoc.SelectSingleNode("/alipay/is_success").InnerText;
        if (nodeIs_success != "T")//请求不成功的错误信息
        {
            return false;
            //sbxml.Append("错误：" + xmlDoc.SelectSingleNode("/alipay/error").InnerText);
        }
        else//请求成功的支付返回宝处理结果信息
        {
            return true;
            //sbxml.Append(xmlDoc.SelectSingleNode("/alipay/response").InnerText);
        }
    }
}
