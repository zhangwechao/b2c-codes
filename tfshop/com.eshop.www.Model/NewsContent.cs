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
    /// 新闻内容类
    /// </summary>
    public class NewsContent
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 新闻标题,meta title标签
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻摘要,meta description标签
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content { get; set; }
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
        /// 是否审核
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// 是否已经删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool IsComment { get; set; }
        /// <summary>
        /// 是否成为图片新闻
        /// </summary>
        public bool IsImageNews { get; set; }
        /// <summary>
        /// 点击率
        /// </summary>
        public int ClickNumber { get; set; }
        /// <summary>
        /// 新闻来源
        /// </summary>
        public string PageFrom { get; set; }
        /// <summary>
        /// 关键字, meta keyword标签
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 主题图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 图片提示
        /// </summary>
        public string Alt { get; set; }
        /// <summary>
        /// 生成html文件名称
        /// </summary>
        public string HtmlName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 新闻目录
        /// </summary>
        public int CategoryId { get; set; }
    }
}
