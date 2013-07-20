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
    public class NewsContentDAO
    {
        public bool Add(NewsContent newsContent)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into news_content(title,summary,[content],is_show,order_by,is_recommend,is_check,is_delete,is_comment,is_image_news,click_number,page_from,keywords,author,image,alt,html_name,create_date,category_id) values (@title,@summary,@content,@is_show,@order_by,@is_recommend,@is_check,@is_delete,@is_comment,@is_image_news,@click_number,@page_from,@keywords,@author,@image,@alt,@html_name,default,@category_id)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@title", DbType.String, newsContent.Title);
            database.AddInParameter(cmd, "@summary", DbType.String, newsContent.Summary);
            database.AddInParameter(cmd, "@content", DbType.String, newsContent.Content);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, newsContent.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, newsContent.OrderBy);
            database.AddInParameter(cmd, "@is_recommend", DbType.Boolean, newsContent.IsRecommend);
            database.AddInParameter(cmd, "@is_check", DbType.Boolean, newsContent.IsCheck);
            database.AddInParameter(cmd, "@is_delete", DbType.Boolean, newsContent.IsDelete);
            database.AddInParameter(cmd, "@is_comment", DbType.Boolean, newsContent.IsComment);
            database.AddInParameter(cmd, "@is_image_news", DbType.Boolean, newsContent.IsImageNews);
            database.AddInParameter(cmd,"@click_number",DbType.Int32,newsContent.ClickNumber);
            database.AddInParameter(cmd, "@page_from", DbType.String, newsContent.PageFrom);
            database.AddInParameter(cmd, "@keywords", DbType.String, newsContent.Keywords);
            database.AddInParameter(cmd, "@author", DbType.String, newsContent.Author);
            database.AddInParameter(cmd, "@image", DbType.String, newsContent.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, newsContent.Alt);
            database.AddInParameter(cmd, "@html_name", DbType.String, newsContent.HtmlName);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, newsContent.CategoryId);

            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from news_content where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(NewsContent newsContent)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update news_content set title=@title,summary=@summary,[content]=@content,is_show=@is_show,order_by=@order_by,is_recommend=@is_recommend,is_check=@is_check,is_delete=@is_delete,is_comment=@is_comment,is_image_news=@is_image_news,page_from=@page_from,keywords=@keywords,author=@author,image=@image,alt=@alt,html_name=@html_name,category_id=@category_id where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@title", DbType.String, newsContent.Title);
            database.AddInParameter(cmd, "@summary", DbType.String, newsContent.Summary);
            database.AddInParameter(cmd, "@content", DbType.String, newsContent.Content);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, newsContent.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, newsContent.OrderBy);
            database.AddInParameter(cmd, "@is_recommend", DbType.Boolean, newsContent.IsRecommend);
            database.AddInParameter(cmd, "@is_check", DbType.Boolean, newsContent.IsCheck);
            database.AddInParameter(cmd, "@is_delete", DbType.Boolean, newsContent.IsDelete);
            database.AddInParameter(cmd, "@is_comment", DbType.Boolean, newsContent.IsComment);
            database.AddInParameter(cmd, "@is_image_news", DbType.Boolean, newsContent.IsImageNews);
            database.AddInParameter(cmd, "@page_from", DbType.String, newsContent.PageFrom);
            database.AddInParameter(cmd, "@keywords", DbType.String, newsContent.Keywords);
            database.AddInParameter(cmd, "@author", DbType.String, newsContent.Author);
            database.AddInParameter(cmd, "@image", DbType.String, newsContent.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, newsContent.Alt);
            database.AddInParameter(cmd, "@html_name", DbType.String, newsContent.HtmlName);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, newsContent.CategoryId);
            database.AddInParameter(cmd, "@Id", DbType.Int32, newsContent.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool UpdateDelete(bool isDelete,int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update news_content set is_delete=@is_delete where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@is_delete", DbType.Boolean, isDelete);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool IsHaveNews(int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from news_content where category_id=@category_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@category_id",DbType.Int32,categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }
        public NewsContent GetEntity(int Id)
        {
            NewsContent newsContent = new NewsContent();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select title,summary,[content],is_show,order_by,is_recommend,is_check,is_delete,is_comment,is_image_news,click_number,page_from,keywords,author,image,alt,html_name,create_date,category_id from news_content where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    newsContent.Id = Id;
                    newsContent.Title = reader["title"].ToString();
                    newsContent.Summary = reader["summary"].ToString();
                    newsContent.Content = reader["content"].ToString();
                    newsContent.IsShow = bool.Parse(reader["is_show"].ToString());
                    newsContent.OrderBy = int.Parse(reader["order_by"].ToString());
                    newsContent.IsRecommend = bool.Parse(reader["is_recommend"].ToString());
                    newsContent.IsCheck = bool.Parse(reader["is_check"].ToString());
                    newsContent.IsComment = bool.Parse(reader["is_comment"].ToString());
                    newsContent.IsImageNews = bool.Parse(reader["is_image_news"].ToString());
                    newsContent.IsDelete = bool.Parse(reader["is_delete"].ToString());
                    newsContent.PageFrom = reader["page_from"].ToString();
                    newsContent.ClickNumber = int.Parse(reader["click_number"].ToString());
                    newsContent.Keywords = reader["keywords"].ToString();
                    newsContent.Author = reader["author"].ToString();
                    newsContent.Image = reader["image"].ToString();
                    newsContent.Alt = reader["alt"].ToString();
                    newsContent.HtmlName = reader["html_name"].ToString();
                    newsContent.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                    newsContent.CategoryId = int.Parse(reader["category_id"].ToString());
                }
            }
            return newsContent;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "news_content");
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
            string sql = "update news_content set click_number=click_number+1 where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public int GetMaxOrder()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(order_by) maxOrderBy from news_content";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 1;
            return int.Parse(obj.ToString()) + 1;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from news_content where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public NewsContent GetEntityByMaxOrder()
        {
            NewsContent newsContent = new NewsContent();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id, title,summary,[content],is_show,order_by,is_recommend,is_check,is_delete,is_comment,is_image_news,click_number,page_from,keywords,author,image,alt,html_name,create_date,category_id from news_content where order_by=(select max(order_by) from news_content)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    newsContent.Id = int.Parse(reader["Id"].ToString());
                    newsContent.Title = reader["title"].ToString();
                    newsContent.Summary = reader["summary"].ToString();
                    newsContent.Content = reader["content"].ToString();
                    newsContent.IsShow = bool.Parse(reader["is_show"].ToString());
                    newsContent.OrderBy = int.Parse(reader["order_by"].ToString());
                    newsContent.IsRecommend = bool.Parse(reader["is_recommend"].ToString());
                    newsContent.IsCheck = bool.Parse(reader["is_check"].ToString());
                    newsContent.IsComment = bool.Parse(reader["is_comment"].ToString());
                    newsContent.IsImageNews = bool.Parse(reader["is_image_news"].ToString());
                    newsContent.IsDelete = bool.Parse(reader["is_delete"].ToString());
                    newsContent.PageFrom = reader["page_from"].ToString();
                    newsContent.ClickNumber = int.Parse(reader["click_number"].ToString());
                    newsContent.Keywords = reader["keywords"].ToString();
                    newsContent.Author = reader["author"].ToString();
                    newsContent.Image = reader["image"].ToString();
                    newsContent.Alt = reader["alt"].ToString();
                    newsContent.HtmlName = reader["html_name"].ToString();
                    newsContent.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                    newsContent.CategoryId = int.Parse(reader["category_id"].ToString());
                }
            }
            return newsContent;
        }
        public int  Next(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_content where Id>@Id order by Id";
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
            string sql = "select max(Id) maxId from news_content";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;

        }
        public int Previous(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_content where Id<@Id order by Id desc";
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
            string sql = "select min(Id) minId from news_content";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
        public int Next(int Id,int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_content where category_id=@category_id and Id>@Id order by Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasNext(int Id,int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(Id) maxId from news_content where category_id=@category_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;

        }
        public int Previous(int Id,int categoryId)
        {
            NewsContent newsContent = new NewsContent();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_content where category_id=@category_id and  Id<@Id order by Id desc";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasPrev(int Id,int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select min(Id) minId from news_content where category_id=@category_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }

        public int Next(int Id, string categoryIds)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_content where category_id in ("+categoryIds+") and Id>@Id order by Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasNext(int Id, string categoryIds)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(Id) maxId from news_content where category_id in ("+categoryIds+")";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;

        }
        public int Previous(int Id, string categoryIds)
        {
            NewsContent newsContent = new NewsContent();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from news_content where category_id in ("+categoryIds+") and  Id<@Id order by Id desc";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool IsHasPrev(int Id, string categoryIds)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select min(Id) minId from news_content where category_id in ("+categoryIds+")";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(database.ExecuteScalar(cmd).ToString()) == Id;
        }
        
    }
}
