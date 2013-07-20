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
    public class Message
    {
        /// <summary>
        /// ID号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 会员ID号
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 建议标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 建议内容
        /// </summary>
        public string Content { get; set; }
        public bool IsShow { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 回复人
        /// </summary>
        public string ReplyUser { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? ReplyDate { get; set; }
    }
}
