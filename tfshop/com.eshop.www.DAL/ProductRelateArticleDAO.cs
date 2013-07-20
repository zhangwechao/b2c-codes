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
    public class ProductRelateArticleDAO
    {
        public bool Add(ProductRelateArticle productRelateArticle)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_relate_article(product_id,relate_article_id) values (@product_id,@relate_article_id)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productRelateArticle.ProductId);
            database.AddInParameter(cmd, "@relate_article_id", DbType.Int32, productRelateArticle.RelatedArticleId);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_relate_article where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductRelateArticle productRelateArticle)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_relate_article set product_id=@product_id,relate_article_id=@relate_article_id where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productRelateArticle.ProductId);
            database.AddInParameter(cmd, "@relate_article_id", DbType.Int32, productRelateArticle.RelatedArticleId);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productRelateArticle.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductRelateArticle GetEntity(int Id)
        {
            ProductRelateArticle productRelateArticle = new ProductRelateArticle();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select product_id,relate_article_id from product_relate_article where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productRelateArticle.Id = Id;
                    productRelateArticle.ProductId = int.Parse(reader["product_id"].ToString());
                    productRelateArticle.RelatedArticleId = int.Parse(reader["relate_article_id"].ToString());
                }
            }
            return productRelateArticle;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_relate_article");
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
        public DataTable GetListByProductId(int productId) 
        {
            string sql = "select product_article.Id id,product_article.title title,product_article.html_name html_name,product_article.order_by order_by,product_article.create_date create_date,product_relate_article.Id relateId from product_article inner join product_relate_article" +
                "  on product_article.Id=product_relate_article.relate_article_id where product_relate_article.product_id=@productId and product_article.is_show=1 order by product_article.order_by desc";
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@productId",DbType.Int32,productId);
            DataSet ds = database.ExecuteDataSet(cmd);
            return ds.Tables[0];
        }
        public bool IsSameRecord(int productId, int articleId)
        {
            string sql = "select count(Id) cnt from product_relate_article where product_id=@product_id and relate_article_id=@relate_article_id";
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@product_id",DbType.Int32,productId);
            database.AddInParameter(cmd,"@relate_article_id",DbType.Int32,articleId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }
    }
}
