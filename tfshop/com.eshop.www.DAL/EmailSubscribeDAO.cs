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
    public class EmailSubscribeDAO
    {
        public bool Add(EmailSubscribe emailSubscribe)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into email_subscribe(email,is_subscribe) values (@email,@is_subscribe)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@email", DbType.String, emailSubscribe.Email);
            database.AddInParameter(cmd, "@is_subscribe", DbType.Boolean, emailSubscribe.IsSubscribe);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from email_subscribe where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool DeleteByEmail(EmailSubscribe emailSubscribe)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from email_subscribe where email=@email";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@email",DbType.String,emailSubscribe.Email);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(EmailSubscribe emailSubscribe)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update email_subscribe set email=@email,is_subscribe=@is_subscribe where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@email", DbType.String, emailSubscribe.Email);
            database.AddInParameter(cmd, "@is_subscribe", DbType.Boolean, emailSubscribe.IsSubscribe);
            database.AddInParameter(cmd, "@Id", DbType.Int32, emailSubscribe.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool UpdateSubscribe(EmailSubscribe emailSubscribe)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update email_subscribe set is_subscribe=@is_subscribe where email=@email";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@email", DbType.String, emailSubscribe.Email);
            database.AddInParameter(cmd, "@is_subscribe", DbType.Boolean, emailSubscribe.IsSubscribe);
            return database.ExecuteNonQuery(cmd)>0;
        }
        public bool IsHaveSameName(EmailSubscribe emailSubscribe)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from email_subscribe where email=@email";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@email", DbType.String, emailSubscribe.Email);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(database.ExecuteScalar(cmd).ToString()) > 0;
        }
        public EmailSubscribe GetEntity(int Id)
        {
            EmailSubscribe emailSubscribe = new EmailSubscribe();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select email,is_subscribe from email_subscribe where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    emailSubscribe.Id = Id;
                    emailSubscribe.Email = reader["email"].ToString();
                    emailSubscribe.IsSubscribe = bool.Parse(reader["is_subscribe"].ToString());
                }
            }
            return emailSubscribe;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "email_subscribe");
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
        public EmailSubscribe GetEntityByEmail(EmailSubscribe emailObj)
        {
            EmailSubscribe emailSubscribe = new EmailSubscribe();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id, email,is_subscribe from email_subscribe where email=@email";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@email",DbType.String,emailObj.Email);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    emailSubscribe.Id = int.Parse(reader["Id"].ToString());
                    emailSubscribe.Email = emailObj.Email;
                    emailSubscribe.IsSubscribe = bool.Parse(reader["is_subscribe"].ToString());
                }
            }
            return emailSubscribe;
        }
    }
}
