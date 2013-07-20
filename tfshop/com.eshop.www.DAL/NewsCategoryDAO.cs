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
    public class NewsCategoryDAO
    {
        public int Add(NewsCategory newsCategory)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into news_category(category_name,is_show,order_by,father_id,path,image,alt,remark) values (@category_name,@is_show,@order_by,@father_id,@path,@image,@alt,@remark) select @@identity";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_name", DbType.String, newsCategory.CategoryName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, newsCategory.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, newsCategory.OrderBy);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, newsCategory.FatherId);
            database.AddInParameter(cmd, "@path", DbType.String, newsCategory.Path);
            database.AddInParameter(cmd, "@image", DbType.String, newsCategory.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, newsCategory.Alt);
            database.AddInParameter(cmd, "@remark", DbType.String, newsCategory.Remark);
            return int.Parse(database.ExecuteScalar(cmd).ToString());
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from news_category where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool IsHaveSonCategory(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from news_category where father_id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@Id",DbType.Int32,Id);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }
        public bool IsHaveSameName(string categoryName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from news_category where category_name=@category_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_name", DbType.String, categoryName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        public bool Update(NewsCategory newsCategory)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update news_category set category_name=@category_name,is_show=@is_show,order_by=@order_by,father_id=@father_id,path=@path,image=@image,alt=@alt,remark=@remark where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_name", DbType.String, newsCategory.CategoryName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, newsCategory.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, newsCategory.OrderBy);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, newsCategory.FatherId);
            database.AddInParameter(cmd, "@path", DbType.String, newsCategory.Path);
            database.AddInParameter(cmd, "@image", DbType.String, newsCategory.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, newsCategory.Alt);
            database.AddInParameter(cmd, "@remark", DbType.String, newsCategory.Remark);
            database.AddInParameter(cmd, "@Id", DbType.Int32, newsCategory.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public NewsCategory GetEntity(int Id)
        {
            NewsCategory newsCategory = new NewsCategory();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select category_name,is_show,order_by,father_id,path,image,alt,remark from news_category where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    newsCategory.Id = Id;
                    newsCategory.CategoryName = reader["category_name"].ToString();
                    newsCategory.IsShow = bool.Parse(reader["is_show"].ToString());
                    newsCategory.OrderBy = int.Parse(reader["order_by"].ToString());
                    newsCategory.FatherId = int.Parse(reader["father_id"].ToString());
                    newsCategory.Path = reader["path"].ToString();
                    newsCategory.Remark = reader["remark"].ToString();
                    newsCategory.Image = reader["image"].ToString();
                    newsCategory.Alt = reader["alt"].ToString();
                }
            }
            return newsCategory;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "news_category");
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
            string sql = "select max(order_by) maxOrderBy from news_category";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 1;
             return int.Parse(obj.ToString()) + 1;
        }
        public NewsCategory GetEntityByCategoryName(string categoryName)
        {
            NewsCategory newsCategory = new NewsCategory();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id, category_name,is_show,order_by,father_id,path,image,alt,remark from news_category where category_name=@category_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@category_name",DbType.String,categoryName);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    newsCategory.Id = int.Parse(reader["Id"].ToString());
                    newsCategory.CategoryName = reader["category_name"].ToString();
                    newsCategory.IsShow = bool.Parse(reader["is_show"].ToString());
                    newsCategory.OrderBy = int.Parse(reader["order_by"].ToString());
                    newsCategory.FatherId = int.Parse(reader["father_id"].ToString());
                    newsCategory.Path = reader["path"].ToString();
                    newsCategory.Remark = reader["remark"].ToString();
                    newsCategory.Image = reader["image"].ToString();
                    newsCategory.Alt = reader["alt"].ToString();
                }
            }
            return newsCategory;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from news_category where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public NewsCategory GetEntityByMaxOrder()
        {
            NewsCategory newsCategory = new NewsCategory();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id, category_name,is_show,order_by,father_id,path,image,alt,remark from news_category where order_by=(select max(order_by) from news_category)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    newsCategory.Id = int.Parse(reader["Id"].ToString());
                    newsCategory.CategoryName = reader["category_name"].ToString();
                    newsCategory.IsShow = bool.Parse(reader["is_show"].ToString());
                    newsCategory.OrderBy = int.Parse(reader["order_by"].ToString());
                    newsCategory.FatherId = int.Parse(reader["father_id"].ToString());
                    newsCategory.Path = reader["path"].ToString();
                    newsCategory.Remark = reader["remark"].ToString();
                    newsCategory.Image = reader["image"].ToString();
                    newsCategory.Alt = reader["alt"].ToString();
                }
            }
            return newsCategory;
        }
        public int Next(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_category where  Id>@Id order by Id";
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
            string sql = "select max(Id) maxId from news_category";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())==Id;
        }
        public int Previous(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_category where Id<@Id order by Id desc";
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
            string sql = "select min(Id) minId from news_category";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())==Id;
        }
        public int Next(int Id,int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_category where father_id=@father_id and Id>@Id order by Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasNext(int Id,int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(Id) maxId from news_category where father_id=@father_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
        public int Previous(int Id,int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_category where father_id=@father_id and Id<@Id order by Id desc";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, categoryId);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasPrev(int Id,int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select min(Id) minId from news_category where father_id=@father_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@father_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
    }
}
