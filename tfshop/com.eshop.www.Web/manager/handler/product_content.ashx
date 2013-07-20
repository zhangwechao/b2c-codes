<%@ WebHandler Language="C#" Class="product_content" %>

using System;
using System.Web;
using com.eshop.www.BLL;
using com.eshop.www.Model;
using System.Data.OleDb;
using System.Data;
using com.eshop.www.Tools;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


public class product_content : IHttpHandler
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
        int recordCount = productDetailBusiness.GetRecordCount("is_delete=0");
        DataRecordTable table = productDetailBusiness.GetList(fieldList, orderField, orderBy, 1, recordCount, "is_delete=0");

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
                cmd.Parameters.AddWithValue("@content",content);
                cmd.Parameters.AddWithValue("@keywords",keyword);
                cmd.Parameters.AddWithValue("@categoryName",categoryName);
                cmd.Parameters.AddWithValue("@brandName",brandName);
                cmd.CommandText = tempSql;
                cmd.ExecuteNonQuery();

            }
        }
        HttpContext.Current.Response.Write("{\"message\":\"success\"}");

        #region
        // //大批量导出的方法，用oledb和excel组件均太慢，采用流写入文件。
       // string fileName = HttpContext.Current.Request.Form["fileName"];
       // string destPath = HttpContext.Current.Server.MapPath("../export/") + fileName + ".xls";
       // string temp = HttpContext.Current.Server.MapPath("../export/temp.txt");

       // ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
       // ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
       // Database database = DatabaseFactory.CreateDatabase();
       // string sql = "select product_name,summary,description,keywords,category_id,brand_id from product_detail where is_delete=0";
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
        #endregion

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
            string sql = "select [产品名称],[摘要],[内容],[关键字],[目录名],[品牌名] from [sheet1$]";
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
            ProductDetailBusiness productDetailBusiness = new ProductDetailBusiness();
            ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
            ProductBrandBusiness productBrandBusiness = new ProductBrandBusiness();
            string productName = string.Empty;
            string summary = string.Empty;
            string content = string.Empty;
            string keywords = string.Empty;
            string categoryName = string.Empty;
            string brandName = string.Empty;
            int orderBy = 0;
            int categoryId = 0;
            int brandId = 0;
            ProductDetail productDetail = null;
            
            foreach (DataRow row in excelTable.Rows)
            {
                productName = row[0].ToString();
                summary = row[1].ToString();
                content = row[2].ToString();
                keywords = row[3].ToString();
                categoryName = row[4].ToString();
                brandName = row[5].ToString();
                if (productName.Length != 0 && summary.Length != 0 && content.Length != 0 && keywords.Length != 0&&categoryName.Length!=0)
                {
                    orderBy = productDetailBusiness.GetMaxOrder();
                    categoryId = productCategoryBusiness.GetEntityByCategoryName(categoryName).Id;
                    brandId = productBrandBusiness.GetEntityByBrandName(brandName).Id;
                    if (categoryId == 0) continue;
                    productDetail = new ProductDetail();
                    productDetail.BrandId = brandId;
                    productDetail.CategoryId = categoryId;
                    productDetail.ClickNumber = 0;
                    productDetail.Description = content;
                    productDetail.HtmlName = "";
                    productDetail.IsComment = true;
                    productDetail.IsDelete = false;
                    productDetail.IsRecommend = false;
                    productDetail.IsShow = true;
                    productDetail.IsDiscount = false;
                    productDetail.IsHot = false;
                    productDetail.IsNew = false;
                    productDetail.integral = 0;
                    productDetail.SalePrice = 0;
                    productDetail.Price = 0;
                    productDetail.Keywords = keywords;
                    productDetail.OrderBy = orderBy;
                    productDetail.Summary = summary;
                    productDetail.ProductName = productName;
                    productDetailBusiness.Add(productDetail);
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