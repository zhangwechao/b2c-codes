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
using System.Text;

namespace com.eshop.www.Model
{
    /// <summary>
    /// 产品详细类
    /// </summary>
    public class ProductDetail
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 产品目录
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 品牌Id
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// 所送积分
        /// </summary>
        public int integral { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 是否是最新产品
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// 是否是热销产品
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 是否是折扣产品，或者特惠（优惠）产品
        /// </summary>
        public bool IsDiscount { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool IsComment { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public float SalePrice { get; set; }
        /// <summary>
        /// 静态页文件名
        /// </summary>
        public string HtmlName { get; set; }
        /// <summary>
        /// 点击率
        /// </summary>
        public int ClickNumber { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public int SaleNumber { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
    }
}
