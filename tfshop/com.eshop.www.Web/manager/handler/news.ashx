<%@ WebHandler Language="C#" Class="news" %>

using System;
using System.Web;
using com.eshop.www.BLL;
using com.eshop.www.Model;
using System.Data.OleDb;
using System.Data;
using com.eshop.www.Tools;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


public class news : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string action  = context.Request.Form["action"];
        if (action == "export")
            Export();
        else
            Import();
            
    }
    private void Export()
    {
        //大批量导出的方法，用oledb和excel组件均太慢，采用流写入文件。
        string fileName = HttpContext.Current.Request.Form["fileName"];
        string destPath = HttpContext.Current.Server.MapPath("../export/") + fileName + ".xls";
        string temp = HttpContext.Current.Server.MapPath("../export/temp.txt");

        NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
        Database database = DatabaseFactory.CreateDatabase();
        string sql = "select title,summary,content,keywords from news_content where is_delete=0 and category_id=2";
        DbCommand cmd = database.GetSqlStringCommand(sql);
        string title = string.Empty;
        string summary = string.Empty;
        string content = string.Empty;
        string keywords = string.Empty;
        System.Text.StringBuilder excelstr = new System.Text.StringBuilder();
        //下面是excel标题，
        excelstr.Append("<tr height=19 style='height:14.25pt'>");
        excelstr.Append("<td height=19 width=72 style='height:14.25pt;width:54pt'>标题</td>");
        excelstr.Append("<td width=72 style='width:54pt'>摘要</td>");
        excelstr.Append("<td width=72 style='width:54pt'>内容</td>");
        excelstr.Append("<td width=72 style='width:54pt'>关键字</td>");
        excelstr.Append("</tr>");
        using (IDataReader reader = database.ExecuteReader(cmd))
        {
            while (reader.Read())
            {
                title = reader["title"].ToString();
                summary = reader["summary"].ToString();
                content = reader["content"].ToString();
                keywords = reader["keywords"].ToString();
                //这里是数据
                excelstr.Append("<tr height=19 style='height:14.25pt'>");
                excelstr.Append("<td height=19 width=72 style='height:14.25pt;width:54pt'>"+title+"</td>");
                excelstr.Append("<td width=72 style='width:54pt'>"+summary+"</td>");
                excelstr.Append("<td width=72 style='width:54pt'>"+content+"</td>");
                excelstr.Append("<td width=72 style='width:54pt'>"+keywords+"</td>");
                excelstr.Append("</tr>");
            }
        }
        //excel的头
        string excelhead = FileHelper.FileToString(HttpContext.Current.Server.MapPath("../export/excel_head.txt"));
        //excel的尾
        string excelfoot = FileHelper.FileToString(HttpContext.Current.Server.MapPath("../export/excel_foot.txt"));
        
       //删除临时文件和目录文件，如果有
        FileHelper.DeleteFile(temp);
        FileHelper.DeleteFile(destPath);
        //创建临时文件
        FileHelper.CreateFile(temp);
        FileHelper.WriteText(temp, excelhead + excelstr.ToString() + excelfoot);
        //另存为excel文件
        System.IO.FileInfo file = new System.IO.FileInfo(temp);
        file.MoveTo(destPath);
        
        HttpContext.Current.Response.Write("{\"message\":\"success\"}");
        
    }
    private void Import()
    {
        //用oledb连接，效率较低数据最好在5000条以下，因为在插入数据的时候涉及操作另一张表，如果只有一张表可以用sqlBulkCopy方法
        HttpPostedFile file = HttpContext.Current.Request.Files["Filedata"];
        string uploadPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request["folder"]) + "\\";

        if (file != null)
        {
            //保存上传文件
            string fileName = file.FileName;
            FileHelper.CreateDirectory(uploadPath);
            string sourceFile = uploadPath + fileName;
            file.SaveAs(sourceFile);
            
            //取出Excel中的数据
            string excelConn = string.Format(System.Configuration.ConfigurationManager.AppSettings["execlConnStr"], sourceFile);
            string sql = "select [标题],[摘要],[内容],[关键字] from [sheet1$]";
            DataTable excelTable = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(excelConn))
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(excelTable);
            }
            
            //将excel中的数据插入数据库
            NewsContentBusiness newsContentBusiness = new NewsContentBusiness();
            NewsCategoryBusiness newsCategoryBusiness = new NewsCategoryBusiness();
            string title = string.Empty;
            string summary = string.Empty;
            string content = string.Empty;
            string keywords = string.Empty;
            int orderBy = 0;
            int categoryId = 0;
            NewsContent newsContent = null;
            
            foreach (DataRow row in excelTable.Rows)
            {
                title = row[0].ToString();
                summary = row[1].ToString();
                content = row[2].ToString();
                keywords = row[3].ToString();
                if (title.Length != 0 && summary.Length != 0 && content.Length != 0 && keywords.Length != 0)
                {
                    orderBy = newsContentBusiness.GetMaxOrder();
                    categoryId = 2;
                    newsContent = new NewsContent();
                    newsContent.Alt = "";
                    newsContent.Author = "";
                    newsContent.CategoryId = categoryId;
                    newsContent.ClickNumber = 0;
                    newsContent.Content = content;
                    newsContent.HtmlName = "";
                    newsContent.Image = "";
                    newsContent.IsCheck = false;
                    newsContent.IsComment = true;
                    newsContent.IsDelete = false;
                    newsContent.IsImageNews = false;
                    newsContent.IsRecommend = false;
                    newsContent.IsShow = true;
                    newsContent.Keywords = keywords;
                    newsContent.OrderBy = orderBy;
                    newsContent.PageFrom = "";
                    newsContent.Summary = summary;
                    newsContent.Title = title;
                    newsContentBusiness.Add(newsContent);
                }
            }
            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            HttpContext.Current.Response.Write("1," + fileName);
        }
        else
        {
            HttpContext.Current.Response.Write("0");
        }
        
        
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}