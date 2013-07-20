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
using System.Collections.Generic;
using com.eshop.www.BLL;
using com.eshop.www.Tools;
using Com.Alipay;
using System.Text;

public partial class order_info : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OrderInfo();
            rShoppcart_load();
        }
    }
    private void OrderInfo()
    {
        PaymentMethodBusiness paymentMethodBusiness = new PaymentMethodBusiness();
        ShoppingMethodBusiness shoppingMethodBusiness = new ShoppingMethodBusiness();
        ShoppingDateBusiness shoppingDateBusiness = new ShoppingDateBusiness();
        string receiveName = Request.Form["receiveName"];
        string receiveAddress = Request.Form["receiveAddress"];
        string receiveTel = Request.Form["receiveTel"];
        string paymentType = Request.Form["paymentType"];
        string IsInvoice = Request.Form["IsInvoice"];
        string invoice = Request.Form["invoice"];
        string shoppingType = Request.Form["shoppingType"];
        string shoppingDate = Request.Form["shoppingDate"];
        string remark = Request.Form["remark"];
        if (!string.IsNullOrEmpty(receiveName))
        {
            lReceiveName.Text = receiveName;
            hiddenReceiveName.Value = receiveName;
        }
        if (!string.IsNullOrEmpty(receiveAddress))
        {
            lReceiveAddress.Text = receiveAddress;
            hiddenReceiveAddress.Value = receiveAddress;
        }
        
        if (!string.IsNullOrEmpty(receiveTel))
        {
            lReceiveTel.Text = receiveTel;
            hiddenReceiveTel.Value = receiveTel;
        }
        PaymentMethod paymentMethod = paymentMethodBusiness.GetEntity(int.Parse(paymentType));
        lPaymentType.Text = paymentMethod.Method + "：" + paymentMethod.Remark;
        hiddenPaymentType.Value = paymentType;

        lIsInvoice.Text = IsInvoice == "1" ? "是" : "不开发票";
        hiddenIsInvoice.Value = IsInvoice;

        lInvoice.Text = string.IsNullOrEmpty(invoice) ? "不开发票" : invoice;
        hiddenInvoice.Value = invoice;

        ShoppingMethod shoppingMethod = shoppingMethodBusiness.GetEntity(int.Parse(shoppingType));
        lShoppingType.Text = shoppingMethod.Method + "：" + shoppingMethod.Remark;
        hiddenShoppingType.Value = shoppingType;

        ShoppingDate shoppingDateObj = shoppingDateBusiness.GetEntity(int.Parse(shoppingDate));
        lShoppingDate.Text = shoppingDateObj.DateType;
        hiddenShoppingDate.Value = shoppingDate;

        lRemark.Text = string.IsNullOrEmpty(remark) ? "无" : remark;
        hiddenRemark.Value = remark;
    }
    private void rShoppcart_load()
    {
        ShoppingCart cart = new ShoppingCart();
        HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
        List<ProductDetail> list = cart.GetListByCookie(cookie);
        ltotalMoney.Text = cart.GetTotalMoney(cookie).ToString("0");

        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
        MemberLevel level = new MemberLevelBusiness().GetEntityByIntegral(member.Integral);

        if (level.Discount == 0)
            lDiscount2.Text = "无折扣";
        else
            lDiscount2.Text = ((1 - level.Discount) * 10).ToString("0.0") + "折";

        lDiscountMoney.Text = (cart.GetTotalMoney(cookie) * level.Discount).ToString("0.0");
        lEndTotal.Text = (cart.GetTotalMoney(cookie) * (1 - level.Discount)).ToString("0.0");
        rShoppcart.DataSource = list;
        rShoppcart.DataBind();
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string receiveName = hiddenReceiveName.Value;
        string receiveAddress = hiddenReceiveAddress.Value;
        string receiveTel = hiddenReceiveTel.Value;
        string paymentType = hiddenPaymentType.Value;
        string IsInvoice = hiddenIsInvoice.Value;
        string invoice = hiddenInvoice.Value;
        string shoppingType = hiddenShoppingType.Value;
        string shoppingDate = hiddenShoppingDate.Value;
        string remark = hiddenRemark.Value;

        HttpCookie userName = Request.Cookies[Cookie.UserNameCookieName];
        Member member = new MemberBusiness().GetEntityByUserName(userName.Value);
        MemberLevel level = new MemberLevelBusiness().GetEntityByIntegral(member.Integral);

        Order order = new Order();
        //order.Id = DateTime.Now.ToString("yyyyMMdd") + StringHelper.GetRandomString2(4);
        //由于招商银行的只接收10位数的订单号，所以改成10位数，不知招商银行是什么鸟系统。
        order.Id = DateTime.Now.ToString("yyMMdd") + StringHelper.GetRandomString2(4);
        order.Address = receiveAddress;
        order.InvoiceHead = invoice;
        order.MemberId = member.Id;

        if (paymentType == "1")
            order.OrderStatusId = 1;//等待发货
        else
            order.OrderStatusId = 3;//待付款
        order.PaymentMethodId = int.Parse(paymentType);
        order.Receiver = receiveName;
        order.Remark = remark;
        order.ShippingDateId = int.Parse(shoppingDate);
        order.ShippingMethodId = int.Parse(shoppingType);
        order.Tel = receiveTel;

        ShoppingCart cart = new ShoppingCart();
        HttpCookie cookie = cart.GetShoppingCart(Cookie.ShoppingCartCookieName);
        float endTotal = (cart.GetTotalMoney(cookie) * (1 - level.Discount));

        order.TotalMoney = endTotal;
        order.DiscountMoney = cart.GetTotalMoney(cookie) * level.Discount;
        order.CreateDate = DateTime.Now;


        List<ProductDetail> list = cart.GetListByCookie(cookie);
        List<OrderItem> itemList = new List<OrderItem>();
        OrderItem item = null;
        foreach (ProductDetail p in list)
        {
            item = new OrderItem();
            item.MemberId = member.Id;
            item.OrderId = order.Id;
            item.Price = p.SalePrice;
            item.ProductId = p.Id;
            item.Quantity = p.SaleNumber;
            item.TotalMoney = p.SalePrice * p.SaleNumber;
            itemList.Add(item);
        }

        bool isSuccess = new OrderBusiness().Add(order, itemList);
        if (isSuccess)
        {
            cart.Delete(cookie);
            Response.Cookies.Add(cookie);
            if (paymentType == "1")
                JavascriptHelper.AlertAndRedirect("订单已经生成，点确定查看订单情况", "member_center.aspx");
            else if (paymentType == "2")
                SendAlipay(order, itemList);
            else
                SendCMBChina(order);
            
        }
    }
    /// <summary>
    /// 发送消息到支付宝
    /// </summary>
    /// <param name="order"></param>
    /// <param name="itemList"></param>
    private void SendAlipay(Order order,List<OrderItem> itemList)
    {
        List<string> body = new List<string>();
        ProductDetailBusiness business = new ProductDetailBusiness();
        ProductDetail product = null;
        foreach (OrderItem item in itemList)
        {
            product = business.GetEntity(item.ProductId);
            body.Add(product.ProductName);
        }

        string logistics_fee = "0.00";
        string logistics_type = "EXPRESS";
        string logistics_payment = "SELLER_PAY";

        string quantity = "1";

        string show_url = "/member_center.aspx";
        

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        string title = string.Join("+", body.ToArray());
        sParaTemp.Add("body", title);
        sParaTemp.Add("logistics_fee", logistics_fee);
        sParaTemp.Add("logistics_payment", logistics_payment);
        sParaTemp.Add("logistics_type", logistics_type);
        sParaTemp.Add("out_trade_no", order.Id);
        sParaTemp.Add("payment_type", "1");
        sParaTemp.Add("price", order.TotalMoney.ToString("0.00"));
        sParaTemp.Add("quantity", quantity);
        sParaTemp.Add("receive_address", order.Address);
        sParaTemp.Add("receive_mobile", order.Tel);
        sParaTemp.Add("receive_name", order.Receiver);
        sParaTemp.Add("receive_phone", order.Tel);
        sParaTemp.Add("receive_zip", "");
        sParaTemp.Add("show_url", show_url);
        sParaTemp.Add("subject", StringHelper.CutString(title,200));

        //构造标准双接口表单提交HTML数据，无需修改
        Service ali = new Service();
        string sHtmlText = ali.Trade_create_by_buyer(sParaTemp);
        Response.Write(sHtmlText);
    }
    private void SendCMBChina(Order order)
    {
        
        string action = ConfigurationManager.AppSettings["action"];
        string coNo = ConfigurationManager.AppSettings["CoNo"];
        string branchId = ConfigurationManager.AppSettings["BranchID"];
        string merchantUrl = ConfigurationManager.AppSettings["MerchantUrl"];
        //string merchantCode = CreateMerchantCode(coNo,branchId,merchantUrl,order);//生成校验码
        string returnUrl = "/cmbReturn.aspx";
        string merchantPara = Request.Cookies[Cookie.UserNameCookieName] == null ? "" : Request.Cookies[Cookie.UserNameCookieName].Value;

        StringBuilder from = new StringBuilder();
        from.Append("<form id='cmbform' name='cmbform' action='" + action + "' method='post'>");
        from.Append("<input type='hidden' name='Date' value='"+order.CreateDate.Value.ToString("yyyyMMdd")+"'/>");//交易日期
        from.Append("<input type='hidden' name='CoNo' value='"+coNo+"'/>");//商户号
        from.Append("<input type='hidden' name='BranchID' value='"+branchId+"'/>");//商户开户分行号
        from.Append("<input type='hidden' name='BillNo' value='"+order.Id+"'/>");//订单号
        from.Append("<input type='hidden' name='Amount' value='"+order.TotalMoney.ToString("0.0")+"'/>");//定单总金额
        from.Append("<input type='hidden' name='MerchantUrl' value='"+returnUrl+"'/>");//返回页面
        from.Append("<input type='hidden' name='merchantPara' value='" + merchantPara + "'/>");
        //from.Append("<input type='hidden' name='MerchantCode' value='"+merchantCode+"'/>");//由于是小的交易不需要校验码
        from.Append("</form>");
        from.Append("<input type='submit' value='confirm' style='display:none;'></form>");
        from.Append("<script>document.forms['cmbform'].submit();</script>");
        Response.Write(from.ToString());
    }
}
