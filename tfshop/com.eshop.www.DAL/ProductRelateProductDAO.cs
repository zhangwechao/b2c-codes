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
    public class ProductRelateProductDAO
    {
        public bool Add(ProductRelateProduct productRelateProduct)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_relate_product(product_id,relate_product_id) values (@product_id,@relate_product_id)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productRelateProduct.ProductId);
            database.AddInParameter(cmd, "@relate_product_id", DbType.Int32, productRelateProduct.RelateProductId);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_relate_product where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductRelateProduct productRelateProduct)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_relate_product set product_id=@product_id,relate_product_id=@relate_product_id where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productRelateProduct.ProductId);
            database.AddInParameter(cmd, "@relate_product_id", DbType.Int32, productRelateProduct.RelateProductId);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productRelateProduct.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductRelateProduct GetEntity(int Id)
        {
            ProductRelateProduct productRelateProduct = new ProductRelateProduct();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select product_id,relate_product_id from product_relate_product where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productRelateProduct.Id = Id;
                    productRelateProduct.ProductId = int.Parse(reader["product_id"].ToString());
                    productRelateProduct.RelateProductId = int.Parse(reader["relate_product_id"].ToString());
                }
            }
            return productRelateProduct;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "view_relate_product");
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
            string sql = "select p.id,p.product_name,p.html_name,p.order_by,r.Id relateId"+
                    " from product_detail p"+
                    " inner join product_relate_product r"+
                    " on p.id=r.relate_product_id"+
                    " where p.is_show=1 and r.product_id=@productId order by p.order_by desc";
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@productId", DbType.Int32, productId);
            DataSet ds = database.ExecuteDataSet(cmd);
            return ds.Tables[0];
        }
        public bool IsSameRecord(int productId, int relateProductId)
        {
            string sql = "select count(Id) cnt from product_relate_product where product_id=@product_id and relate_product_id=@relate_product_id";
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productId);
            database.AddInParameter(cmd, "@relate_product_id", DbType.Int32, relateProductId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }
    }
}
