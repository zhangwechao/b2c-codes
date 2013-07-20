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
    /// 产品属性类
    /// </summary>
    public class ProductAttribute
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Attribute { get; set; }
        /// <summary>
        /// 所选值列表，中间用逗号隔开
        /// </summary>
        public string ValuesList { get; set; }
        /// <summary>
        /// 该属性的值是多个还是一个。
        /// 如果为真，表示后台输入是一组多选check
        /// 如果是假，表示后台输入是单选select
        /// </summary>
        public bool isMultiple { get; set; }
        /// <summary>
        /// 是否用于前台界面上筛选条件
        /// </summary>
        public bool IsFilter { get; set; }
    }
}
