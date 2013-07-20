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
    /// 简历类
    /// </summary>
    public class Resume
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 相片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticStatus { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 文化程度
        /// </summary>
        public string Degree { get; set; }
        /// <summary>
        /// 健康状况
        /// </summary>
        public string Health { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public bool? IsMarry { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public int? Height { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Profession { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduateDate { get; set; }
        /// <summary>
        /// 外语及水平
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 计算机水平
        /// </summary>
        public string Computer { get; set; }
        /// <summary>
        /// 应聘职位Id
        /// </summary>
        public int JobId { get; set; }
        /// <summary>
        /// 爱好及特长
        /// </summary>
        public string GoodAt { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ContactAdd { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Postcode { get; set; }
        /// <summary>
        /// 要求待遇
        /// </summary>
        public string RequirePay { get; set; }
        /// <summary>
        /// 再教育及培训情况
        /// </summary>
        public string Training { get; set; }
        /// <summary>
        /// 个人简历
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 资历及成果
        /// </summary>
        public string Gain { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public string Other { get; set; }
    }
}
