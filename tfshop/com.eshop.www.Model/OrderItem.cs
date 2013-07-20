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
    public class OrderItem
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 所属订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 所属会员
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 产品Id号
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public float TotalMoney { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 是否已经评论
        /// </summary>
        public bool IsComment { get; set; }
    }
}
