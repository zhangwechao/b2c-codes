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
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using com.eshop.www.Model;
using System.Data;

namespace com.eshop.www.DAL
{
    public class MessageDAO
    {
        public bool Add(Message message)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into message (member_id,title,[content],is_show,create_date) values (@member_id,@title,@content,@is_show,default)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@member_id", DbType.Int32, message.MemberId);
            database.AddInParameter(cmd, "@title", DbType.String, message.Title);
            database.AddInParameter(cmd, "@content", DbType.String, message.Content);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, message.IsShow);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from message where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(Message message)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update message set member_id=@member_id,title=@title,[content]=@content,is_show=@is_show where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@member_id", DbType.Int32, message.MemberId);
            database.AddInParameter(cmd, "@title", DbType.String, message.Title);
            database.AddInParameter(cmd, "@content", DbType.String, message.Content);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, message.IsShow);
            database.AddInParameter(cmd, "@Id", DbType.Int32, message.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Reply(Message message)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update message set reply_user=@reply_user,reply_content=@reply_content,reply_date=getdate(),is_show=@is_show where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@reply_user", DbType.String, message.ReplyUser);
            database.AddInParameter(cmd, "@reply_content", DbType.String, message.ReplyContent);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, message.IsShow);
            database.AddInParameter(cmd, "@Id", DbType.Int32, message.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public Message GetEntity(int Id)
        {
            Message message = new Message();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select member_id,title,[content],is_show,create_date,reply_user,reply_content,reply_date from message where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    message.Id = Id;
                    message.Content = reader["content"].ToString();
                    message.MemberId = int.Parse(reader["member_id"].ToString());
                    message.Title = reader["title"].ToString();
                    message.IsShow = bool.Parse(reader["is_show"].ToString());
                    message.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                    message.ReplyUser = reader["reply_user"].ToString();
                    message.ReplyContent = reader["reply_content"].ToString();
                    if (reader["reply_date"] != null && reader["reply_date"].ToString().Length > 0)
                        message.ReplyDate = DateTime.Parse(reader["reply_date"].ToString());
                }

            }
            return message;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "message");
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
    }
}
