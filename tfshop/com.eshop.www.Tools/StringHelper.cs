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
using System.Text.RegularExpressions;
using System.IO;

namespace com.eshop.www.Tools
{

    /// <summary>
    /// Summary description for StringUtilily
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 随机生成字符串源
        /// </summary>
        public const string RANDOM_STRING_SOURCE = "0123456789abcdefghijklmnopqrstuvwxyz";
        public const string RANDOM_STRING_SOURCE2 = "0123456789";
        public static Random rnd = new Random();
        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <returns>已修改的字符串</returns>
        public static string Replace(string src, string pattern, string replacement)
        {
            return Replace(src, pattern, replacement, RegexOptions.None);
        }

        /// <summary>
        /// 替换字符串,不区分大小写
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <returns>已修改的字符串</returns>
        public static string ReplaceIgnoreCase(string src, string pattern, string replacement)
        {
            return Replace(src, pattern, replacement, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">匹配模式</param>
        /// <returns>已修改的字符串</returns>
        public static string Replace(string src, string pattern, string replacement, RegexOptions options)
        {
            Regex regex = new Regex(pattern, options | RegexOptions.Compiled);

            return regex.Replace(src, replacement);
        }

        /// <summary>
        /// 删除字符串中指定的内容
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要删除的正则表达式模式</param>
        /// <returns>已删除指定内容的字符串</returns>
        public static string Drop(string src, string pattern)
        {
            return Replace(src, pattern, "");
        }

        /// <summary>
        /// 删除字符串中指定的内容,不区分大小写
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要删除的正则表达式模式</param>
        /// <returns>已删除指定内容的字符串</returns>
        public static string DropIgnoreCase(string src, string pattern)
        {
            return ReplaceIgnoreCase(src, pattern, "");
        }

        /// <summary>
        /// 替换字符串到数据库可输入模式
        /// </summary>
        /// <param name="src">待插入数据库的字符串</param>
        /// <returns>可插入数据库的字符串</returns>
        public static string ToSQL(string src)
        {
            if (src == null)
            {
                return null;
            }
            return Replace(src, "'", "''");
        }

        /// <summary>
        /// 去掉html内容中的指定的html标签
        /// </summary>
        /// <param name="content">html内容</param>
        /// <param name="tagName">html标签</param>
        /// <returns>去掉标签的内容</returns>
        public static string DropHtmlTag(string content, string tagName)
        {
            //去掉<tagname>和</tagname>
            return DropIgnoreCase(content, "<[/]{0,1}" + tagName + "[^\\>]*\\>");
        }

        /// <summary>
        /// 去掉html内容中全部标签
        /// </summary>
        /// <param name="content">html内容</param>
        /// <returns>去掉html标签的内容</returns>
        public static string DropHtmlTag(string content)
        {
            //去掉<*>
            return Drop(content, "<[^\\>]*>");
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="num">字符串的位数</param>
        /// <returns>随机字符串</returns>
        public static string GetRandomString(int num)
        {
            string rndStr = "";
           
            for (int i = 0; i < num; i++)
            {
                rndStr += RANDOM_STRING_SOURCE.Substring(Convert.ToInt32(Math.Round(rnd.NextDouble() * 35, 0)), 1);
            }
            return rndStr;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="num">字符串的位数</param>
        /// <returns>随机字符串</returns>
        public static string GetRandomString2(int num)
        {
            string rndStr = "";

            for (int i = 0; i < num; i++)
            {
                rndStr += RANDOM_STRING_SOURCE.Substring(Convert.ToInt32(Math.Round(rnd.NextDouble() * 9, 0)), 1);
            }
            return rndStr;
        }
        /// <summary>
        /// 判断一个数据是不是数字
        /// </summary>
        /// <param name="inputData">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNumeric(string inputData)
        {
            Regex _isNumber = new Regex(@"^\d+$");
            Match m = _isNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 转换html标签为web页可见内容
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string EscapeHtml(string src)
        {
            if (src == null)
            {
                return null;
            }
            string s = src;
            s = Replace(s, ">", "&gt;");
            s = Replace(s, "<", "&lt;");
            return s;
        }

        /// <summary>
        /// 将字符串格式化成HTML代码
        /// </summary>
        /// <param name="str">要格式化的字符串</param>
        /// <returns>格式化后的字符串</returns>
        public static String ToHtml(string str)
        {
            if (str == null || str.Equals(""))
            {
                return str;
            }

            StringBuilder sb = new StringBuilder(str);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br>");
            sb.Replace("\n", "<br>");
            sb.Replace("\t", " ");
            sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }

        /// <summary>
        /// 截断字符串，并且将超过的字符换为...
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="N">最大可以容纳的字符位数,一个中文占两个字符位</param>
        /// <returns></returns>
        public static string CutString(string str, int len)
        {
            if (str == null)
                return "";
            int intLen = str.Length;
            int start = 0;
            int end = intLen;
            int single = 0;
            char[] chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Convert.ToInt32(chars[i]) > 255)
                    start += 2;
                else
                {
                    start += 1;
                    single++;
                }
                if (start >= len)
                {

                    if (end % 2 == 0)
                    {
                        if (single % 2 == 0)
                            end = i + 1;
                        else
                            end = i;
                    }
                    else
                        end = i + 1;
                    break;
                }
            }
            string temp = str.Substring(0, end);
            if (str.Length > end)
                return temp + "...";
            else
                return temp;
        }
        public static string ReplaceChar(string str) 
        {
            if (str == null)
                return "";
            return str.Replace(":", "\\:").Replace("[", "\\[").Replace("{", "\\{")
                .Replace("]", "\\]").Replace("}", "\\}").Replace("\"", "\\\"")
                .Replace("'","\\'").Replace("/","\\/")
                .Replace("\b", "\\b").Replace("\f","\\f").Replace("\n","\\n")
                .Replace("\r","\\r").Replace("\t","\\t");
        }
        public static string SqlSafe(string source)
        {
            source = source.Replace("'", "");
            source = source.Replace("\"", "");
            source = source.Replace("-", "");
            source = source.Replace("+", "");
            source = source.Replace("&", "");
            source = source.Replace("<", "");
            source = source.Replace(">", "");
            source = source.Replace("<>", "");
            return source;
        }
        public static string ByteArrayToString(byte[] bytes) 
        {
            UTF8Encoding charset = new UTF8Encoding(true);
            return charset.GetString(bytes);
        }
        public static byte[] StringToByteArray(string str)
        {
            UTF8Encoding charset = new UTF8Encoding(true);
            return charset.GetBytes(str);
        }
        public static string StreamToString(Stream stream) 
        {
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8)) 
            {
                return reader.ReadToEnd();
            } 
        }
        public static Stream StringToStream(string str)
        {
            byte[] bytes = StringToByteArray(str);
            return new MemoryStream(bytes);
        }
        public static void StringWriteIntoStream(string str,Stream stream)
        {
            using (stream)
            {
                byte[] bytes = StringToByteArray(str);
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        public static string MD5Encrypt(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
    }
}
