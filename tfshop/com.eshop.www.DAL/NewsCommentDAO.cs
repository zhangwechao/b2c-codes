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
    public class NewsCommentDAO
    {
        public bool Add(NewsComment newsComment)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into news_comment(news_id,[content],ip,user_name,is_show,create_date) values (@news_id,@content,@ip,@user_name,@is_show,default)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@news_id", DbType.Int32, newsComment.NewsId);
            database.AddInParameter(cmd, "@content", DbType.String, newsComment.Content);
            database.AddInParameter(cmd, "@ip", DbType.String, newsComment.IP);
            database.AddInParameter(cmd, "@user_name", DbType.String, newsComment.UserName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, newsComment.IsShow);

            return database.ExecuteNonQuery(cmd)>0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from news_comment where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(NewsComment newsComment)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update news_comment set news_id=@news_id,[content]=@content,ip=@ip,user_name=@user_name,is_show=@is_show where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@news_id", DbType.Int32, newsComment.NewsId);
            database.AddInParameter(cmd, "@content", DbType.String, newsComment.Content);
            database.AddInParameter(cmd, "@ip", DbType.String, newsComment.IP);
            database.AddInParameter(cmd, "@user_name", DbType.String, newsComment.UserName);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, newsComment.IsShow);
            database.AddInParameter(cmd, "@Id", DbType.Int32, newsComment.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public NewsComment GetEntity(int Id)
        {
            NewsComment newsComment = new NewsComment();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select news_id,[content],ip,user_name,is_show,create_date from news_comment where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    newsComment.Id = Id;
                    newsComment.NewsId = int.Parse(reader["news_id"].ToString());
                    newsComment.Content = reader["content"].ToString();
                    newsComment.IP = reader["ip"].ToString();
                    newsComment.UserName = reader["user_name"].ToString();
                    newsComment.IsShow = bool.Parse(reader["is_show"].ToString());
                    newsComment.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                }
            }
            return newsComment;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "news_comment");
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
            string sql = "select count(Id) recordCount from news_comment where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        public bool UpdateIsShow(bool isShow,int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update news_comment set is_show=@is_show where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean,isShow);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
    }
}
