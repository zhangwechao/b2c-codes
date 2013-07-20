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
    /// 友情链接类
    /// </summary>
    public class FriendLink
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 链接名称
        /// </summary>
        public string LinkName { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 代表图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 图片提示
        /// </summary>
        public string Alt { get; set; }
    }
}
