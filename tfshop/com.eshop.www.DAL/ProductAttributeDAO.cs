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
    public class ProductAttributeDAO
    {
        public int Add(ProductAttribute productAttribute)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_attribute(attribute,values_list,is_multiple,is_filter) values (@attribute,@values_list,@is_multiple,@is_filter) select @@identity";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@attribute", DbType.String, productAttribute.Attribute);
            database.AddInParameter(cmd, "@values_list", DbType.String, productAttribute.ValuesList);
            database.AddInParameter(cmd, "@is_multiple", DbType.Boolean, productAttribute.isMultiple);
            database.AddInParameter(cmd, "@is_filter", DbType.Boolean, productAttribute.IsFilter);
            return int.Parse(database.ExecuteScalar(cmd).ToString());
        }
        public bool IsHaveSameName(string attributeName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_attribute where attribute=@attribute";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@attribute", DbType.String, attributeName);
            return int.Parse(database.ExecuteScalar(cmd).ToString())>0;
        }
        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_attribute where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductAttribute productAttribute)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_attribute set attribute=@attribute,values_list=@values_list,is_multiple=@is_multiple,is_filter=@is_filter where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@attribute", DbType.String, productAttribute.Attribute);
            database.AddInParameter(cmd, "@values_list", DbType.String, productAttribute.ValuesList);
            database.AddInParameter(cmd, "@is_multiple", DbType.Boolean, productAttribute.isMultiple);
            database.AddInParameter(cmd, "@is_filter", DbType.Boolean, productAttribute.IsFilter);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productAttribute.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductAttribute GetEntity(int Id)
        {
            ProductAttribute productAttribute = new ProductAttribute();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select attribute,values_list,is_multiple,is_filter from product_attribute where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productAttribute.Id = Id;
                    productAttribute.Attribute = reader["attribute"].ToString();
                    productAttribute.ValuesList = reader["values_list"].ToString();
                    productAttribute.isMultiple = bool.Parse(reader["is_multiple"].ToString());
                    productAttribute.IsFilter = bool.Parse(reader["is_filter"].ToString());
                }
            }
            return productAttribute;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_attribute");
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
        public int Next(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_attribute where Id>@Id order by Id";
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
            string sql = "select max(Id) maxId from product_attribute";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
        public int Previous(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_attribute where Id<@Id order by Id desc";
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
            string sql = "select min(Id) minId from product_attribute";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
    }
}
