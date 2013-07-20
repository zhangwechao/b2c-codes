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
    public class ProductCommentDAO
    {
        public bool Add(ProductComment productComment)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_comment(product_id,[content],ip,user_name,score,is_show,create_date) values (@product_id,@content,@ip,@user_name,@score,@is_show,default)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productComment.ProductId);
            database.AddInParameter(cmd, "@content", DbType.String, productComment.Content);
            database.AddInParameter(cmd, "@ip", DbType.String, productComment.IP);
            database.AddInParameter(cmd, "@user_name", DbType.String, productComment.UserName);
            database.AddInParameter(cmd,"@score",DbType.Decimal,productComment.Score);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productComment.IsShow);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_comment where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(ProductComment productComment)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_comment set product_id=@product_id,[content]=@content,ip=@ip,user_name=@user_name,score=@score,is_show=@is_show where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productComment.ProductId);
            database.AddInParameter(cmd, "@content", DbType.String, productComment.Content);
            database.AddInParameter(cmd, "@ip", DbType.String, productComment.IP);
            database.AddInParameter(cmd, "@user_name", DbType.String, productComment.UserName);
            database.AddInParameter(cmd, "@score", DbType.Decimal, productComment.Score);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productComment.IsShow);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productComment.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductComment GetEntity(int Id)
        {
            ProductComment productComment = new ProductComment();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select product_id,[content],ip,user_name,score,is_show,create_date from product_comment where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productComment.Id = Id;
                    productComment.ProductId = int.Parse(reader["product_id"].ToString());
                    productComment.Content = reader["content"].ToString();
                    productComment.IP = reader["ip"].ToString();
                    productComment.UserName = reader["user_name"].ToString();
                    productComment.Score = float.Parse(reader["score"].ToString());
                    productComment.IsShow = bool.Parse(reader["is_show"].ToString());
                    productComment.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                }
            }
            return productComment;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_comment");
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
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from product_comment where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object record = database.ExecuteScalar(cmd);
            if (record!=null && record.ToString().Length > 0)
                return int.Parse(record.ToString());
            else
                return 0;
        }
        public float GetAvgCommentByProduct(int productId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select avg(score) avgScore from product_comment group by product_id having product_id=@product_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@product_id",DbType.Int32,productId);
            object avgScore = database.ExecuteScalar(cmd);
            if (avgScore!=null && avgScore.ToString().Length > 0)
                return float.Parse(float.Parse(avgScore.ToString()).ToString("0.0"));
            else
                return 0;
        }
        public int GetSumCommentByProduct(int productId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_comment group by product_id having product_id=@product_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productId);
            object cnt = database.ExecuteScalar(cmd);
            if (cnt!=null && cnt.ToString().Length > 0)
                return int.Parse(cnt.ToString());
            else
                return 0;
        }
        public bool UpdateUserName(string newUserName,string oldUserName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_comment set user_name=@newUserName where user_name=@oldUserName";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@newUserName", DbType.String, newUserName);
            database.AddInParameter(cmd, "@oldUserName", DbType.String, oldUserName);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
    }
}
