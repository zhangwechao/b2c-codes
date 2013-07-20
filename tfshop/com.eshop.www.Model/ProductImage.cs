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
    [Serializable]
    public class ProductImage
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 产品ID号
        /// </summary>
        public int ProductId { get;set;}
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 图片提示
        /// </summary>
        public string Alt { get; set; }
        /// <summary>
        /// 放大后的图片
        /// </summary>
        public string ZoomImage { get; set; }
        /// <summary>
        /// 1为列表图片，2为详细页图片，如果是详细页图片，可以提供大图片
        /// 详细页图片可以提供多张
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 是否默认详细页图片
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
