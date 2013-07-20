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

using com.eshop.www.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace com.eshop.www.DAL
{
    public class ProductArticleDAO
    {
        public bool Add(ProductArticle productArticle)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_article(title,summary,[content],keywords,order_by,is_show,image,alt,html_name) values (@title,@summary,@content,@keywords,@order_by,@is_show,@image,@alt,@html_name)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@title", DbType.String, productArticle.Title);
            database.AddInParameter(cmd, "@summary", DbType.String, productArticle.Summary);
            database.AddInParameter(cmd, "@content", DbType.String, productArticle.Content);
            database.AddInParameter(cmd, "@keywords", DbType.String, productArticle.Keywords);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productArticle.OrderBy);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productArticle.IsShow);
            database.AddInParameter(cmd, "@image", DbType.String, productArticle.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productArticle.Alt);
            database.AddInParameter(cmd, "@html_name", DbType.String, productArticle.HtmlName);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_article where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductArticle productArticle)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_article set title=@title,summary=@summary,[content]=@content,keywords=@keywords,order_by=@order_by,is_show=@is_show,image=@image,alt=@alt,html_name=@html_name where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@title", DbType.String, productArticle.Title);
            database.AddInParameter(cmd, "@summary", DbType.String, productArticle.Summary);
            database.AddInParameter(cmd, "@content", DbType.String, productArticle.Content);
            database.AddInParameter(cmd, "@keywords", DbType.String, productArticle.Keywords);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productArticle.OrderBy);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productArticle.IsShow);
            database.AddInParameter(cmd, "@image", DbType.String, productArticle.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productArticle.Alt);
            database.AddInParameter(cmd, "@html_name", DbType.String, productArticle.HtmlName);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productArticle.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductArticle GetEntity(int Id)
        {
            ProductArticle productArticle = new ProductArticle();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select title,summary,[content],click_number,keywords,order_by,is_show,image,alt,html_name,create_date from product_article where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productArticle.Id = Id;
                    productArticle.Title = reader["title"].ToString();
                    productArticle.Summary = reader["summary"].ToString();
                    productArticle.Content = reader["content"].ToString();
                    productArticle.ClickNumber = int.Parse(reader["click_number"].ToString());
                    productArticle.Keywords = reader["keywords"].ToString();
                    productArticle.OrderBy = int.Parse(reader["order_by"].ToString());
                    productArticle.IsShow = bool.Parse(reader["is_show"].ToString());
                    productArticle.Image = reader["image"].ToString();
                    productArticle.Alt = reader["alt"].ToString();
                    productArticle.HtmlName = reader["html_name"].ToString();
                    productArticle.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                }
            }
            return productArticle;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_article");
            database.AddInParameter(cmd, "@FieldList", DbType.String, fieldList);
            database.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            database.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            database.AddInParameter(cmd, "@OrderField", DbType.String, orderField);
            database.AddInParameter(cmd, "@OrderType", DbType.Boolean, orderBy);
            database.AddInParameter(cmd, "@Where", DbType.String, where);
            database.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);
            database.AddOutParameter(cmd, "@PageCount", DbType.Int32, 4);

            DataSet ds = database.ExecuteDataSet(cmd);
            int recordCount = Convert.ToInt32(database.GetParameterValue(cmd, "@RecordCount"));
            int pageCount = Convert.ToInt32(database.GetParameterValue(cmd, "@PageCount"));
            table.Table = ds.Tables[0];
            table.PageSize = pageSize;
            table.PageIndex = pageIndex;
            table.PageCount = pageCount;
            table.RecordCount = recordCount;
            return table;
        }

        public bool UpdateClickNumber(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_article set click_number=click_number+1 where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public int GetMaxOrder()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(order_by) maxOrderBy from product_article";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 1;
            return int.Parse(obj.ToString()) + 1;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from product_article where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public int Next(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_article where Id>@Id order by Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasNext(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(Id) maxId from product_article";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;

        }
        public int Previous(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_article where Id<@Id order by Id desc";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasPrev(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select min(Id) minId from product_article";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }

    }
}
