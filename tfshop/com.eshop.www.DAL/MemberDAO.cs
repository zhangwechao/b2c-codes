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
//51aspx.com 下载 http://www.51aspx.com
namespace com.eshop.www.DAL
{
    public class MemberDAO
    {
        public bool Add(Member member)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into member(user_name,password,mobile,email,integral,state) values (@user_name,@password,@mobile,@email,@integral,@state)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@user_name", DbType.String, member.UserName);
            database.AddInParameter(cmd, "@password", DbType.String, member.Password);
            database.AddInParameter(cmd, "@mobile", DbType.String, member.Mobile);
            database.AddInParameter(cmd, "@integral", DbType.Int32, member.Integral);
            database.AddInParameter(cmd, "@email", DbType.String, member.Email);
            database.AddInParameter(cmd, "@state", DbType.Boolean, member.State);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from member where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(Member member)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update member set user_name=@user_name,password=@password,mobile=@mobile,integral=@integral,email=@email,state=@state where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@user_name", DbType.String, member.UserName);
            database.AddInParameter(cmd, "@password", DbType.String, member.Password);
            database.AddInParameter(cmd, "@mobile", DbType.String, member.Mobile);
            database.AddInParameter(cmd, "@integral", DbType.Int32, member.Integral);
            database.AddInParameter(cmd, "@email", DbType.String, member.Email);
            database.AddInParameter(cmd, "@state", DbType.Boolean, member.State);
            database.AddInParameter(cmd, "@Id", DbType.Int32, member.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateIntegral(Member member)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update member set integral=integral+@integral where user_name=@user_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@user_name", DbType.String, member.UserName);
            database.AddInParameter(cmd, "@integral", DbType.Int32, member.Integral);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateState(Member member)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update member set state=@state where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@state", DbType.Boolean, member.State);
            database.AddInParameter(cmd, "@Id", DbType.Int32, member.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdatePassword(Member member)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update member set password=@password where user_name=@user_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@password",DbType.String,member.Password);
            database.AddInParameter(cmd,"@user_name",DbType.String,member.UserName);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool Validate(Member member)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select password from member where state=1 and user_name=@user_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@user_name",DbType.String,member.UserName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return obj.ToString() == member.Password;
        }
        public bool IsSameUserName(string userName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from member where user_name=@user_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@user_name", DbType.String, userName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        
        public Member GetEntity(int Id)
        {
            Member member = new Member();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select user_name,password,mobile,email,integral,state from member where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    member.Id = Id;
                    member.UserName = reader["user_name"].ToString();
                    member.Password = reader["password"].ToString();
                    member.Email = reader["email"].ToString();
                    member.Integral = int.Parse(reader["integral"].ToString());
                    member.State = bool.Parse(reader["state"].ToString());
                    member.Mobile = reader["mobile"].ToString();
                }
            }
            return member;
        }
        public Member GetEntityByUserName(string userName)
        {
            Member member = new Member();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id,user_name,password,mobile,email,integral,state from member where user_name=@user_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@user_name",DbType.String,userName);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    member.Id = int.Parse(reader["Id"].ToString());
                    member.UserName = reader["user_name"].ToString();
                    member.Email = reader["email"].ToString();
                    member.Password = reader["password"].ToString();
                    member.Integral = int.Parse(reader["integral"].ToString());
                    member.State = bool.Parse(reader["state"].ToString());
                    member.Mobile = reader["mobile"].ToString();
                }
            }
            return member;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "member");
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
        public bool IsSameEmail(string email)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from member where email=@email";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@email", DbType.String, email);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }
    }
}
