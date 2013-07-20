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
using System.Text.RegularExpressions;

namespace com.eshop.www.Tools
{
    public class ValidataField
    {
        public static string OK = "ok";
        private static string chineseReg = @"[^\x00-\xff]";
        private static string emailReg = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        private static string telReg = @"^[0-9]{3,}-[0-9]{6,8}$";
        private static string mobileReg = @"^1[0-9]{10}$";
        private static string postcodeReg = @"^\d{6}$";
        private static string cardIDReg = @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
        private static string numberReg = @"^\d+$";
        public static string ValidataUserName(string userName)
        {
            int len = GetLength(userName);
            if (userName.Trim().Length == 0)
            {
                return "请输入用户名"; 
            }
            if (len < 5 || len > 20)
            {
                return "用户名长度不够或者超过范围";
            }
            if (userName.IndexOf('\'') > -1)
            {
                return "用户名中含有的单引号为非法字符";
            }
            if (userName.IndexOf('\"') > -1)
            {
                return "用户名中含有的双引号为非法字符";
            }
            return OK;
        }
        public static string ValidataPassword(string password)
        {
            int len = GetLength(password);
            if (len == 0)
            {
                return "请输入密码";
            }
            else if (len < 6 || len > 50)
            {
                return "密码长度不够或超出范围";
            }
            return OK;
        }
        public static string ValidataConfirmPassword(string password, string confirPassword)
        {
            if (confirPassword.Trim().Length == 0)
            {
                return "请确认密码";
            }
            else if (password != confirPassword)
            {
                return "密码前后输入不一致";
            }
            return OK;
        }
        public static string ValidataEmail(string email)
        {
            int len = GetLength(email);
            if (len == 0)
            {
                return "请输入电子邮件";
            }
            if (!Regex.IsMatch(email, emailReg, RegexOptions.IgnoreCase))
            {
                return "电子邮件格式不正确";
            }
            return OK;
        }
        public static string ValidataDate(string date, bool flag)
        {

            try
            {
                int len = GetLength(date);
                if (len > 0)
                {
                    if (flag)
                    {
                        if (DateTime.Parse(date) > DateTime.Today)
                        {
                            return "所输入日期大于当前日期";
                        }
                    }
                    else
                    {
                        if (DateTime.Parse(date) < DateTime.Today)
                        {
                            return "所输入日期小于当前日期";
                        }
                    }
                }
            }
            catch
            {
                return "日期格式不正确，无法转成正确的日期格式";
            }

            return OK;
        }
        public static string ValidataCode(string code, object sessionCode)
        {
            int len = GetLength(code);
            if (len == 0)
            {
                return "请输入验证码";
            }
            if (sessionCode == null)
            {
                return "验证码已经过期";
            }
            if (code.Trim().ToLower() != sessionCode.ToString().Trim().ToLower())
            {
                return "验证码输入不正确";
            }
            return OK;

        }
        public static string ValidataTel(string tel)
        {
            int len = GetLength(tel);
            if (len > 0)
            {
                if (!Regex.IsMatch(tel, telReg, RegexOptions.IgnoreCase))
                {
                    return "电话号码格式不正确";
                }
            }
            return OK;
        }
        public static string ValidataMobile(string mobile, bool flag)
        {
            int len = GetLength(mobile);
            if (flag && len == 0)
            {
                return "请输入手机号码";
            }
            if (len > 0)
            {
                if (len != 11)
                {
                    return "手机号码为11位的数字";
                }
                if (!Regex.IsMatch(mobile, mobileReg, RegexOptions.IgnoreCase))
                {
                    return "手机号码格式不正确";
                }
            }
            return OK;
        }
        public static string ValidataFax(string fax)
        {
            int len = GetLength(fax);
            if (len > 0)
            {
                if (!Regex.IsMatch(fax, telReg, RegexOptions.IgnoreCase))
                {
                    return "传真号码格式不正确";
                }
            }
            return OK;
        }

        public static string ValidataPostcode(string postcode)
        {
            int len = GetLength(postcode);
            if (len > 0)
            {
                if (!Regex.IsMatch(postcode, postcodeReg, RegexOptions.IgnoreCase))
                {
                    return "邮政编码格式不正确";
                }
            }
            return OK;
        }
        public static string ValidataCardID(string cardID)
        {
            int len = GetLength(cardID);
            if (len > 0)
            {
                if (!Regex.IsMatch(cardID, cardIDReg, RegexOptions.IgnoreCase))
                {
                    return "身份证码格式不正确";
                }
            }
            return OK;
        }
        public static string ValidataNumber(string number)
        {
            int len = GetLength(number);
            if (len > 0)
            {
                if (!Regex.IsMatch(number, numberReg, RegexOptions.IgnoreCase))
                {
                    return "身高必须是一个数字";
                }
            }
            return OK;
        }
        public static int GetLength(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            return Regex.Replace(str.Trim(), chineseReg, "**", RegexOptions.IgnoreCase).Length;
        }
        public static string CommonValidate(string str, string fieldName, int lengthRange)
        {
            int len = GetLength(str);
            if (len > lengthRange)
            {
                return fieldName + "长度超出范围";
            }
            return OK;
        }
    }
}
