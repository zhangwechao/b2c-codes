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
    /// 产品相关视频类
    /// </summary>
    public class ProductVideo
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 产品Id号
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 视频Id号
        /// </summary>
        public int VideoId { get; set; }
    }
}
