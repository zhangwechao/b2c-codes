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
    /// 招聘信息类
    /// </summary>
    public class JobInformation
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// 职位描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 招聘人数,可以为"若干"
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 过期日期,可以为"长期有效"
        /// </summary>
        public string EffectiveDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
    }
}
