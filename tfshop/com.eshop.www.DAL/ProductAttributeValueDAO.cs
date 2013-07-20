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
    public class ProductAttributeValueDAO
    {
        public bool Add(ProductAttributeValue productAttributeValue)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_attribute_value(product_id,attribute_id,attribute_value) values (@product_id,@attribute_id,@attribute_value)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productAttributeValue.ProductId);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, productAttributeValue.AttributeId);
            database.AddInParameter(cmd, "@attribute_value", DbType.String, productAttributeValue.AttributeValue);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_attribute_value where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductAttributeValue productAttributeValue)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_attribute_value set product_id=@product_id,attribute_id=@attribute_id,attribute_value=@attribute_value where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productAttributeValue.ProductId);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, productAttributeValue.AttributeId);
            database.AddInParameter(cmd, "@attribute_value", DbType.String, productAttributeValue.AttributeValue);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productAttributeValue.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateByProductIdAndAttributeId(ProductAttributeValue productAttributeValue)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_attribute_value set attribute_value=@attribute_value where product_id=@product_id and attribute_id=@attribute_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productAttributeValue.ProductId);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, productAttributeValue.AttributeId);
            database.AddInParameter(cmd, "@attribute_value", DbType.String, productAttributeValue.AttributeValue);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productAttributeValue.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductAttributeValue GetEntity(int Id)
        {
            ProductAttributeValue productAttributeValue = new ProductAttributeValue();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select product_id,attribute_id,attribute_value from product_attribute_value where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productAttributeValue.Id = Id;
                    productAttributeValue.ProductId = int.Parse(reader["product_id"].ToString());
                    productAttributeValue.AttributeId = int.Parse(reader["attribute_id"].ToString());
                    productAttributeValue.AttributeValue = reader["attribute_value"].ToString();
                }
            }
            return productAttributeValue;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_attribute_value");
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
        public ProductAttributeValue GetEntity(ProductAttributeValue o)
        {
            ProductAttributeValue productAttributeValue = new ProductAttributeValue();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id, product_id,attribute_id,attribute_value from product_attribute_value where product_id=@product_id and attribute_id=@attribute_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, o.ProductId);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, o.AttributeId);

            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productAttributeValue.Id = int.Parse(reader["Id"].ToString());
                    productAttributeValue.ProductId = int.Parse(reader["product_id"].ToString());
                    productAttributeValue.AttributeId = int.Parse(reader["attribute_id"].ToString());
                    productAttributeValue.AttributeValue = reader["attribute_value"].ToString();
                }
            }
            return productAttributeValue;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from product_attribute_value where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool DeleteByAttributeId(int attributeId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_attribute_value where attribute_id=@attribute_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, attributeId);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool DeleteByProductId(int productId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_attribute_value where product_id=@product_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productId);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public DataTable GetList(int attributeId, string attributeValue)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id,product_id,attribute_id,attribute_value from product_attribute_value where attribute_id=@attribute_id and attribute_value=@attribute_value";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, attributeId);
            database.AddInParameter(cmd, "@attribute_value", DbType.String, attributeValue);
            return database.ExecuteDataSet(cmd).Tables[0];
        }
    }
}
