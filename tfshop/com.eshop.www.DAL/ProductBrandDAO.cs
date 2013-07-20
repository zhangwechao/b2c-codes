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
    public class ProductBrandDAO
    {
        public bool Add(ProductBrand productBrand)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_brand(brand_name,is_show,order_by,image,alt,remark,category_id) values (@brand_name,@is_show,@order_by,@image,@alt,@remark,@category_id)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@brand_name", DbType.String, productBrand.BrandName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productBrand.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productBrand.OrderBy);
            database.AddInParameter(cmd, "@image", DbType.String, productBrand.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productBrand.Alt);
            database.AddInParameter(cmd, "@remark", DbType.String, productBrand.Alt);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, productBrand.CategoryId);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_brand where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool IsHaveSameName(string brandName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_brand where brand_name=@brand_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@brand_name", DbType.String, brandName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        public bool Update(ProductBrand productBrand)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_brand set brand_name=@brand_name,is_show=@is_show,order_by=@order_by,image=@image,alt=@alt,remark=@remark,category_id=@category_id where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@brand_name", DbType.String, productBrand.BrandName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productBrand.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productBrand.OrderBy);
            database.AddInParameter(cmd, "@image", DbType.String, productBrand.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productBrand.Alt);
            database.AddInParameter(cmd, "@remark", DbType.String, productBrand.Alt);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productBrand.Id);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, productBrand.CategoryId);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductBrand GetEntity(int Id)
        {
            ProductBrand productBrand = new ProductBrand();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select brand_name,is_show,order_by,image,alt,remark,category_id from product_brand where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productBrand.Id = Id;
                    productBrand.BrandName = reader["brand_name"].ToString();
                    productBrand.IsShow = bool.Parse(reader["is_show"].ToString());
                    productBrand.OrderBy = int.Parse(reader["order_by"].ToString());
                    productBrand.Image = reader["image"].ToString();
                    productBrand.Alt = reader["alt"].ToString();
                    productBrand.Remark = reader["remark"].ToString();
                    productBrand.CategoryId = int.Parse(reader["category_id"].ToString());
                }
            }
            return productBrand;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_brand");
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
            string sql = "select max(order_by) maxOrderBy from product_brand";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 1;
            return int.Parse(obj.ToString()) + 1;
        }
        public ProductBrand GetEntityByBrandName(string brandName)
        {
            ProductBrand productBrand = new ProductBrand();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id, brand_name,is_show,order_by,image,alt,remark,category_id from product_brand where brand_name=@brand_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@brand_name", DbType.String, brandName);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productBrand.Id = int.Parse(reader["Id"].ToString());
                    productBrand.BrandName = reader["brand_name"].ToString();
                    productBrand.IsShow = bool.Parse(reader["is_show"].ToString());
                    productBrand.OrderBy = int.Parse(reader["order_by"].ToString());
                    productBrand.Remark = reader["remark"].ToString();
                    productBrand.Image = reader["image"].ToString();
                    productBrand.Alt = reader["alt"].ToString();
                    productBrand.CategoryId = int.Parse(reader["category_id"].ToString());
                }
            }
            return productBrand;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from product_brand where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }

        public ProductBrand GetEntityByMaxOrder()
        {
            ProductBrand productBrand = new ProductBrand();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id, brand_name,is_show,order_by,image,alt,remark,category_id from product_brand where order_by=(select max(order_by) from product_brand)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productBrand.Id = int.Parse(reader["Id"].ToString());
                    productBrand.BrandName = reader["brand_name"].ToString();
                    productBrand.IsShow = bool.Parse(reader["is_show"].ToString());
                    productBrand.OrderBy = int.Parse(reader["order_by"].ToString());
                    productBrand.Remark = reader["remark"].ToString();
                    productBrand.Image = reader["image"].ToString();
                    productBrand.Alt = reader["alt"].ToString();
                    productBrand.CategoryId = int.Parse(reader["category_id"].ToString());
                }
            }
            return productBrand;
        }
        public int Next(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_brand where  Id>@Id order by Id";
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
            string sql = "select max(Id) maxId from product_brand";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
        public int Previous(int Id)
        {
            ProductBrand productBrand = new ProductBrand();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_brand where Id<@Id order by Id desc";
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
            string sql = "select min(Id) minId from product_brand";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
    }
}
