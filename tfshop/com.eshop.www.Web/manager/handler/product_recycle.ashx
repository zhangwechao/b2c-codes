<%@ WebHandler Language="C#" Class="product_recycle" %>

using System;
using System.Web;
using com.eshop.www.BLL;
using com.eshop.www.Model;
using System.Data.OleDb;
using System.Data;
using com.eshop.www.Tools;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


public class product_recycle : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string action  = context.Request.Form["action"];
        if (action == "export")
            Export();
            
    }
    private void Export()
    {
        //将模板excel另存为
        string fileName = HttpContext.Current.Request.Form["fileName"];
        string tempPath = HttpContext.Current.Server.MapPath("../export/product_content.xls");
        string destPath = HttpContext.Current.Server.MapPath("../export/") + fileName + ".xls";
        FileHelper.FileSaveAs(tempPath, destPath);

        //取出数据库所有数据
        string fieldList = "product_name,summary,description,keywords,category_id,brand_id";
        string orderField = "order_by";
        bool orderBy = false;
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
        ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
        int recordCount = productDetailBusiness.GetRecordCount("is_delete=1");
        DataRecordTable table = productDetailBusiness.GetList(fieldList, orderField, orderBy, 1, recordCount, "is_delete=1");

        //用oleDb写入excel
        string excelConn = string.Format(System.Configuration.ConfigurationManager.AppSettings["execlConnStr"], destPath);
        using (OleDbConnection conn = new OleDbConnection(excelConn))
        {
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            string tempSql = "insert into [sheet1$] ([产品名称],[摘要],[内容],[关键字],[目录名],[品牌名]) values (@productName,@summary,@content,@keywords,@categoryName,@brandName)";
            string categoryName = string.Empty;
            string brandName = string.Empty;
            string productName = string.Empty;
            string summary = string.Empty;
            string content = string.Empty;
            string keyword = string.Empty;
            int categoryId = 0;
            int brandId = 0;
            foreach (DataRow row in table.Table.Rows)
            {
                productName = row["product_name"].ToString();
                summary = row["summary"].ToString();
                content = row["description"].ToString();
                keyword = row["keywords"].ToString();
                categoryId = int.Parse(row["category_id"].ToString());
                brandId = int.Parse(row["brand_id"].ToString());
                categoryName = productCategoryBusiness.GetEntity(categoryId).CategoryName;
                if (brandId == 0)
                    brandName = "";
                else
                    brandName = productBrandBusiness.GetEntity(brandId).BrandName;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@productName", productName);
                cmd.Parameters.AddWithValue("@summary", summary);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@keywords", keyword);
                cmd.Parameters.AddWithValue("@categoryName", categoryName);
                cmd.Parameters.AddWithValue("@brandName", brandName);
                cmd.CommandText = tempSql;
                cmd.ExecuteNonQuery();

            }
        }
        HttpContext.Current.Response.Write("{\"message\":\"success\"}");
        
       // //大批量导出的方法，用oledb和excel组件均太慢，采用流写入文件。
       // string fileName = HttpContext.Current.Request.Form["fileName"];
       // string destPath = HttpContext.Current.Server.MapPath("../export/") + fileName + ".xls";
       // string temp = HttpContext.Current.Server.MapPath("../export/temp.txt");

       // ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
       // ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
       // Database database = DatabaseFactory.CreateDatabase();
       // string sql = "select product_name,summary,description,keywords,category_id,brand_id from product_detail where is_delete=1";
       // DbCommand cmd = database.GetSqlStringCommand(sql);
       // string productName = string.Empty;
       // string summary = string.Empty;
       // string content = string.Empty;
       // string keywords = string.Empty;
       // int categoryId = 0;
       // int brandId = 0;
       // string categoryName = string.Empty;
       // string brandName = string.Empty;
       // System.Text.StringBuilder excelstr = new System.Text.StringBuilder();
       // //下面是excel标题，
       // excelstr.Append("<tr height=19 style='height:14.25pt'>");
       // excelstr.Append("<td height=19 width=72 style='height:14.25pt;width:54pt'>产品名称</td>");
       // excelstr.Append("<td width=72 style='width:54pt'>摘要</td>");
       // excelstr.Append("<td width=72 style='width:54pt'>内容</td>");
       // excelstr.Append("<td width=72 style='width:54pt'>关键字</td>");
       // excelstr.Append("<td width=72 style='width:54pt'>目录名</td>");
       // excelstr.Append("<td width=72 style='width:54pt'>品牌名</td>");
       // excelstr.Append("</tr>");
       // using (IDataReader reader = database.ExecuteReader(cmd))
       // {
       //     while (reader.Read())
       //     {
       //         productName = reader["product_name"].ToString();
       //         summary = reader["summary"].ToString();
       //         content = reader["description"].ToString();
       //         keywords = reader["keywords"].ToString();
       //         categoryId = int.Parse(reader["category_id"].ToString());
       //         brandId = int.Parse(reader["brand_id"].ToString());
       //         categoryName = productCategoryBusiness.GetEntity(categoryId).CategoryName;
       //         brandName = productBrandBusiness.GetEntity(brandId).BrandName;
       //         //这里是数据
       //         excelstr.Append("<tr height=19 style='height:14.25pt'>");
       //         excelstr.Append("<td height=19 width=72 style='height:14.25pt;width:54pt'>" + productName + "</td>");
       //         excelstr.Append("<td width=72 style='width:54pt'>" + summary + "</td>");
       //         excelstr.Append("<td width=72 style='width:54pt'>" + content + "</td>");
       //         excelstr.Append("<td width=72 style='width:54pt'>" + keywords + "</td>");
       //         excelstr.Append("<td width=72 style='width:54pt'>" + categoryName + "</td>");
       //         excelstr.Append("<td width=72 style='width:54pt'>" + brandName + "</td>");
       //         excelstr.Append("</tr>");
       //     }
       // }
       // //excel的头
       // string excelhead = FileHelper.FileToString(HttpContext.Current.Server.MapPath("../export/excel_head.txt"));
       // //excel的尾
       // string excelfoot = FileHelper.FileToString(HttpContext.Current.Server.MapPath("../export/excel_foot.txt"));
        
       ////删除临时文件和目录文件，如果有
       // FileHelper.DeleteFile(temp);
       // FileHelper.DeleteFile(destPath);
       // //创建临时文件
       // FileHelper.CreateFile(temp);
       // FileHelper.WriteText(temp, excelhead + excelstr.ToString() + excelfoot);
       // //另存为excel文件
       // System.IO.FileInfo file = new System.IO.FileInfo(temp);
       // file.MoveTo(destPath);
        
       // HttpContext.Current.Response.Write("{\"message\":\"success\"}");
        
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}