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
    public class ProductCategoryDAO
    {
        public int Add(ProductCategory productCategory)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_category(category_name,is_show,order_by,father_id,path,image,alt,remark) values (@category_name,@is_show,@order_by,@father_id,@path,@image,@alt,@remark) select @@identity";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_name", DbType.String, productCategory.CategoryName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productCategory.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productCategory.OrderBy);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, productCategory.FatherId);
            database.AddInParameter(cmd, "@path", DbType.String, productCategory.Path);
            database.AddInParameter(cmd, "@image", DbType.String, productCategory.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productCategory.Alt);
            database.AddInParameter(cmd, "@remark", DbType.String, productCategory.Alt);
            int id = int.Parse(database.ExecuteScalar(cmd).ToString());
            return id;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_category where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductCategory productCategory)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_category set category_name=@category_name,is_show=@is_show,order_by=@order_by,father_id=@father_id,path=@path,image=@image,alt=@alt,remark=@remark where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_name", DbType.String, productCategory.CategoryName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productCategory.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productCategory.OrderBy);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, productCategory.FatherId);
            database.AddInParameter(cmd, "@path", DbType.String, productCategory.Path);
            database.AddInParameter(cmd, "@image", DbType.String, productCategory.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productCategory.Alt);
            database.AddInParameter(cmd, "@remark", DbType.String, productCategory.Alt);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productCategory.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool IsHaveSonCategory(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_category where father_id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        public bool IsHaveSameName(string categoryName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_category where category_name=@category_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_name", DbType.String, categoryName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        public ProductCategory GetEntity(int Id)
        {
            ProductCategory productCategory = new ProductCategory();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select category_name,is_show,order_by,father_id,path,image,alt,remark from product_category where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productCategory.Id = Id;
                    productCategory.CategoryName = reader["category_name"].ToString();
                    productCategory.IsShow = bool.Parse(reader["is_show"].ToString());
                    productCategory.OrderBy = int.Parse(reader["order_by"].ToString());
                    productCategory.FatherId = int.Parse(reader["father_id"].ToString());
                    productCategory.Path = reader["path"].ToString();
                    productCategory.Image = reader["image"].ToString();
                    productCategory.Alt = reader["alt"].ToString();
                    productCategory.Remark = reader["remark"].ToString();
                }
            }
            return productCategory;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_category");
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

        public int GetMaxOrder()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(order_by) maxOrderBy from product_category";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 1;
            return int.Parse(obj.ToString()) + 1;
        }

        public ProductCategory GetEntityByCategoryName(string categoryName)
        {
            ProductCategory productCategory = new ProductCategory();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id, category_name,is_show,order_by,father_id,path,image,alt,remark from product_category where category_name=@category_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_name", DbType.String, categoryName);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productCategory.Id = int.Parse(reader["Id"].ToString());
                    productCategory.CategoryName = reader["category_name"].ToString();
                    productCategory.IsShow = bool.Parse(reader["is_show"].ToString());
                    productCategory.OrderBy = int.Parse(reader["order_by"].ToString());
                    productCategory.FatherId = int.Parse(reader["father_id"].ToString());
                    productCategory.Path = reader["path"].ToString();
                    productCategory.Remark = reader["remark"].ToString();
                    productCategory.Image = reader["image"].ToString();
                    productCategory.Alt = reader["alt"].ToString();
                }
            }
            return productCategory;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from product_category where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public ProductCategory GetEntityByMaxOrder()
        {
            ProductCategory productCategory = new ProductCategory();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id, category_name,is_show,order_by,father_id,path,image,alt,remark from product_category where order_by=(select max(order_by) from product_category)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productCategory.Id = int.Parse(reader["Id"].ToString());
                    productCategory.CategoryName = reader["category_name"].ToString();
                    productCategory.IsShow = bool.Parse(reader["is_show"].ToString());
                    productCategory.OrderBy = int.Parse(reader["order_by"].ToString());
                    productCategory.FatherId = int.Parse(reader["father_id"].ToString());
                    productCategory.Path = reader["path"].ToString();
                    productCategory.Remark = reader["remark"].ToString();
                    productCategory.Image = reader["image"].ToString();
                    productCategory.Alt = reader["alt"].ToString();
                }
            }
            return productCategory;
        }
        public int Next(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_category where Id>@Id order by Id";
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
            string sql = "select max(Id) maxId from product_category";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())==Id;
        }
        public int Previous(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_category where Id<@Id order by Id desc";
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
            string sql = "select min(Id) minId from product_category";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
        public DataTable GetTable()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id,category_name,father_id,path from product_category";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            DataSet ds = database.ExecuteDataSet(cmd);
            return ds.Tables[0];
        }
    }
}
