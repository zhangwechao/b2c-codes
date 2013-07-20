<%@ WebHandler Language="C#" Class="product_category" %>

using System;
using System.Web;
using com.eshop.www.BLL;
using com.eshop.www.Model;
using System.Data.OleDb;
using System.Data;
using com.eshop.www.Tools;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


public class product_category : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string action = context.Request.Form["action"];
        if (action == "export")
            Export();
        else
            Import();

    }
    private void Export()
    {
        //将模板excel另存为
        string fileName = HttpContext.Current.Request.Form["fileName"];
        string tempPath = HttpContext.Current.Server.MapPath("../export/product_category.xls");
        string destPath = HttpContext.Current.Server.MapPath("../export/") + fileName + ".xls";
        FileHelper.FileSaveAs(tempPath, destPath);

        //取出数据库所有数据
        string fieldList = "category_name,father_id";
        string orderField = "order_by";
        bool orderBy = false;
        ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
        int recordCount = productCategoryBusiness.GetRecordCount("1=1");
        DataRecordTable table = productCategoryBusiness.GetList(fieldList, orderField, orderBy, 1, recordCount, "");

        //用oleDb写入excel
        string excelConn = string.Format(System.Configuration.ConfigurationManager.AppSettings["execlConnStr"], destPath);
        using (OleDbConnection conn = new OleDbConnection(excelConn))
        {
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            string tempSql = "insert into [sheet1$] ([目录名],[上级目录]) values (@categoryName,@fatherName)";
            string categoryName = string.Empty;
            int fatherId = 0;
            string fatherCategoryName = string.Empty;
            foreach (DataRow row in table.Table.Rows)
            {
                categoryName = row["category_name"].ToString();
                fatherId = int.Parse(row["father_id"].ToString());
                fatherCategoryName = fatherId == 0 ? "一级目录" : productCategoryBusiness.GetEntity(fatherId).CategoryName;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@categoryName",categoryName);
                cmd.Parameters.AddWithValue("@fatherName",fatherCategoryName);
                cmd.CommandText = tempSql;
                cmd.ExecuteNonQuery();
                
            }
        }
        HttpContext.Current.Response.Write("{\"message\":\"success\"}");

    }
    private void Import()
    {
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
            string sql = "select [目录名],[上级目录] from [sheet1$]";
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
            ProductCategoryBusiness productCategoryBusiness = new ProductCategoryBusiness();
            string categoryName = string.Empty;
            string fatherCategory = string.Empty;
            int orderBy = 0;
            int fatherId = 0;
            ProductCategory productCategory = null;
            foreach (DataRow row in excelTable.Rows)
            {
                categoryName = row[0].ToString();
                fatherCategory = row[1].ToString();
                if (categoryName.Length != 0 && fatherCategory.Length != 0)
                {
                    orderBy = productCategoryBusiness.GetMaxOrder();
                    fatherId = productCategoryBusiness.GetEntityByCategoryName(fatherCategory).Id;
                    productCategory = new ProductCategory();
                    productCategory.Alt = "";
                    productCategory.CategoryName = categoryName;
                    productCategory.FatherId = fatherId;
                    productCategory.Image = "";
                    productCategory.IsShow = true;
                    productCategory.OrderBy = orderBy;
                    productCategory.Path = "";
                    productCategory.Remark = "";
                    if (productCategoryBusiness.IsHaveSameName(categoryName)) continue;
                    productCategoryBusiness.Add(productCategory);
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