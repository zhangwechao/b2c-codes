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
using System.Web;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;

namespace com.eshop.www.Tools
{
    public class WebFormHandler  
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public static ArrayList SaveFiles(string filePath,string newFileName)
        {
            ///'遍历File表单元素
            HttpFileCollection files = HttpContext.Current.Request.Files;
            ArrayList list = new ArrayList();
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    ///'检查文件扩展名字
                    HttpPostedFile postedFile = files[i];
                    string fileExtension=System.IO.Path.GetExtension(postedFile.FileName);
                    string fileName = newFileName + fileExtension; 
                    if (fileName != "")
                    {
                        ///注意：可能要修改你的文件夹的匿名写入权限。
                        postedFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath(filePath) + fileName);
                    }
                    list.Add(fileName);
                }
                list.TrimToSize();
                return list;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

       
        /// <summary>
        /// 显示状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string StateView(bool state)
        {
            if (state)
                return string.Format("<font color=\"red\">{0}</font>", "√");
            else
                return "×";
        }
        ///   <summary> 
        ///   传入URL返回网页的html代码 
        ///   </summary> 
        ///   <param   name="Url">URL</param> 
        ///   <returns></returns> 
        public  static string GetUrltoHtml(string Url)
        {
            string html = "";
            Uri uri = new Uri(Url);
            System.Net.HttpWebRequest wReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
            System.Net.HttpWebResponse wResp = (System.Net.HttpWebResponse)wReq.GetResponse();
            using (System.IO.Stream respStream = wResp.GetResponseStream())
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, System.Text.Encoding.GetEncoding("UTF-8")))
                {
                    html = reader.ReadToEnd();
                }
            }
            return html;
        }
        /// <summary>
        /// 根据文件名和url创建静态文件
        /// </summary>
        /// <param name="fileName">静态文件名,不带后缀名,后缀名为htm</param>
        /// <param name="url">带域名的url</param>
        public static void CreateHMTLFile(string fileName,string url)
        {
            string pathFile = HttpContext.Current.Server.MapPath("~/" + fileName + ".htm");
            string html = GetUrltoHtml(url);
            if (!File.Exists(pathFile))
            {
                using (FileStream fs = File.Create(pathFile))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(html);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(pathFile))
                {
                    sw.Write(html);
                }
                
            }
        }
        /// <summary>
        /// 将实体类转成Json格式,实体类的属性在对应前台,区分大小写
        /// </summary>
        /// <param name="o">任何定义的实体类</param>
        /// <returns>json字符串</returns>
        public static string ModelConvertJson(Object o)
        {
            IsoDateTimeConverter datetimeConvert = new IsoDateTimeConverter();
            datetimeConvert.DateTimeFormat = "yyyy-MM-dd";
            string json = JsonConvert.SerializeObject(o,Formatting.Indented,datetimeConvert);
            return json;
        }
        /// <summary>
        /// 将Datatabel转成json格式
        /// </summary>
        /// <param name="dt">DataTabel对象</param>
        /// <param name="tableName">Table名称</param>
        /// <returns>json字符串</returns>
        public static string CreateJsonParameters(DataTable dt,string tableName,string dateFormat)
        {

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer ser = new JsonSerializer();
                IsoDateTimeConverter datetimeConvert = new IsoDateTimeConverter();
                datetimeConvert.DateTimeFormat = dateFormat;
                ser.Converters.Add(datetimeConvert);

                jw.Formatting = Formatting.Indented;
                jw.WriteStartObject();
                jw.WritePropertyName(tableName);
                jw.WriteStartArray();
                

                foreach (DataRow dr in dt.Rows)
                {
                    jw.WriteStartObject();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        
                        //将列名转成小写的原因是因为在Ext Template中Id不被Exj识别
                        jw.WritePropertyName(dc.ColumnName.ToLower());
                        //此处dr[dc]不能加toString(),因为这样日期就不能转成想要的格式
                        ser.Serialize(jw, dr[dc]);
                    }

                    jw.WriteEndObject();
                }
                jw.WriteEndArray();
                jw.WriteEndObject();

                sw.Close();
                jw.Close();

            }

            return sb.ToString();

        }
        public static string CreateJsonParameters(DataTable dt, string tableName)
        {

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer ser = new JsonSerializer();
                IsoDateTimeConverter datetimeConvert = new IsoDateTimeConverter();
                datetimeConvert.DateTimeFormat = "yyyy-MM-dd";
                ser.Converters.Add(datetimeConvert);

                jw.Formatting = Formatting.Indented;
                jw.WriteStartObject();
                jw.WritePropertyName(tableName);
                jw.WriteStartArray();


                foreach (DataRow dr in dt.Rows)
                {
                    jw.WriteStartObject();

                    foreach (DataColumn dc in dt.Columns)
                    {

                        //将列名转成小写的原因是因为在Ext Template中Id不被Exj识别
                        jw.WritePropertyName(dc.ColumnName.ToLower());
                        //此处dr[dc]不能加toString(),因为这样日期就不能转成想要的格式
                        ser.Serialize(jw, dr[dc]);
                    }

                    jw.WriteEndObject();
                }
                jw.WriteEndArray();
                jw.WriteEndObject();

                sw.Close();
                jw.Close();

            }

            return sb.ToString();

        }
        /// <summary>
        /// 将dataTable转成XML格式
        /// </summary>
        /// <param name="dt">DataTabel对象</param>
        /// <returns>xml字符串</returns>
        public static string CreateXMLParameters(DataTable dt)
        {
            StringBuilder xmlstring = new StringBuilder();
            xmlstring.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlstring.Append("<"+dt.TableName+">");
            for (int i=0;i<dt.Rows.Count;i++)
            {
                
                xmlstring.Append("<rows>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    xmlstring.Append("<"+dt.Columns[j].ColumnName+">");
                    xmlstring.Append("<![CDATA["+dt.Rows[i][j].ToString()+"]]>");
                    xmlstring.Append("</"+dt.Columns[j].ColumnName+">");
                }
                xmlstring.Append("</rows>");
            }
            xmlstring.Append("</"+dt.TableName+">");
            return xmlstring.ToString();
        }
        
        
    }
}
