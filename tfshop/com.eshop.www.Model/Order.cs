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
using System.Text;

namespace com.eshop.www.Model
{
    public class Order
    {
        /// <summary>
        /// 订单号Id号，8位随机号组成
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 会员Id号，如果是未注册用户，ID为0
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public float TotalMoney { get; set; }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public float DiscountMoney { get; set; }
        /// <summary>
        /// 送货方式Id号
        /// </summary>
        public int ShippingMethodId { get; set; }
        /// <summary>
        /// 付款方式Id号
        /// </summary>
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// 送货时间Id号
        /// </summary>
        public int ShippingDateId { get; set; }
        /// <summary>
        /// 备注事项
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 订单当前状态
        /// </summary>
        public int OrderStatusId { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceHead { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 接收地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 修改后金额
        /// </summary>
        public float? ModifyMoney { get; set; }
        /// <summary>
        /// 修改原因
        /// </summary>
        public string ModifySeason { get; set; }
        /// <summary>
        /// 支付宝的交易号
        /// </summary>
        public string TradeNo { get; set; }
        /// <summary>
        /// 买家的支付宝账号
        /// </summary>
        public string BuyerEmail { get; set; }
        /// <summary>
        /// 交易发生的时间
        /// </summary>
        public DateTime TradeDate { get; set; }
    }
}
