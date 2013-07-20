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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using com.eshop.www.Model;


namespace com.eshop.www.DAL
{
    public class CategoryAttributeDAO
    {
        public bool Add(CategoryAttribute categoryAttribute)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into category_attribute(category_id,attribute_id,state) values (@category_id,@attribute_id,@state)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryAttribute.CategoryId);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, categoryAttribute.AttributeId);
            database.AddInParameter(cmd,"@state",DbType.Boolean,categoryAttribute.State);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from category_attribute where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool UpdateState(bool state, int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update category_attribute set state=@state where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@state", DbType.Boolean, state);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(CategoryAttribute categoryAttribute)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update category_attribute set state=@state where category_id=@category_id and attribute_id=@attribute_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryAttribute.CategoryId);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, categoryAttribute.AttributeId);
            database.AddInParameter(cmd, "@state", DbType.Boolean, categoryAttribute.State);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public CategoryAttribute GetEntity(int Id)
        {
            CategoryAttribute categoryAttribute = new CategoryAttribute();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select category_id,attribute_id,state from category_attribute where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    categoryAttribute.Id = Id;
                    categoryAttribute.CategoryId = int.Parse(reader["category_id"].ToString());
                    categoryAttribute.AttributeId = int.Parse(reader["attribute_id"].ToString());
                    categoryAttribute.State = bool.Parse(reader["state"].ToString());
                }
            }
            return categoryAttribute;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "view_category_attribute");
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
        public bool DeleteByAttribute(int attributeId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from category_attribute where attribute_id=@attribute_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, attributeId);
            return database.ExecuteNonQuery(cmd)>0;

        }
        public bool DeleteByCategory(int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from category_attribute where category_id=@category_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryId);
            return database.ExecuteNonQuery(cmd) > 0;

        }
        public bool IsSame(int categoryId, int attributeId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from category_attribute where category_id=@category_id and attribute_id=@attribute_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryId);
            database.AddInParameter(cmd, "@attribute_id", DbType.Int32, attributeId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }
    }
}
