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
using com.eshop.www.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace com.eshop.www.DAL
{
    public class AdminDAO
    {
        public bool Add(Admin admin)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into admin(admin,real_name,password,role_id,state) values (@admin,@real_name,@password,@role_id,@state)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@admin", DbType.String, admin.AdminName);
            database.AddInParameter(cmd, "@real_name", DbType.String, admin.RealName);
            database.AddInParameter(cmd, "@password", DbType.String, admin.Password);
            database.AddInParameter(cmd, "@role_id", DbType.Int32, admin.RoleId);
            database.AddInParameter(cmd,"@state",DbType.Boolean,admin.State);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from admin where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(Admin admin)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update admin set admin=@admin,real_name=@real_name,password=@password,role_id=@role_id,state=@state where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@admin", DbType.String, admin.AdminName);
            database.AddInParameter(cmd, "@real_name", DbType.String, admin.RealName);
            database.AddInParameter(cmd, "@password", DbType.String, admin.Password);
            database.AddInParameter(cmd, "@role_id", DbType.Int32, admin.RoleId);
            database.AddInParameter(cmd, "@state", DbType.Boolean, admin.State);
            database.AddInParameter(cmd, "@Id", DbType.Int32, admin.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool UpdateState(Admin admin)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update admin set state=@state where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@state", DbType.Boolean, admin.State);
            database.AddInParameter(cmd, "@Id", DbType.Int32, admin.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool UpdatePassword(Admin admin)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update admin set password=@password where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@password", DbType.String, admin.Password);
            database.AddInParameter(cmd, "@Id", DbType.Int32, admin.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool Validate(Admin admin)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select password from admin where state=1 and admin=@admin";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@admin", DbType.String, admin.AdminName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length==0)
                return false;
            return obj.ToString() == admin.Password;
        }
        public bool IsSameUserName(Admin admin)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from admin where admin=@admin";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@admin", DbType.String, admin.AdminName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }

        public Admin GetEntity(int Id)
        {
            Admin admin = new Admin();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select admin,real_name,password,role_id,state from admin where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    admin.Id = Id;
                    admin.AdminName = reader["admin"].ToString();
                    admin.RealName = reader["real_name"].ToString();
                    admin.Password = reader["password"].ToString();
                    admin.State = bool.Parse(reader["state"].ToString());
                    admin.RoleId = int.Parse(reader["role_id"].ToString());
                }
            }
            return admin;
        }
        public Admin GetEntityByUserName(string adminName)
        {
            Admin admin = new Admin();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id,admin,real_name,password,state,role_id from admin where admin=@admin";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@admin", DbType.String, adminName);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    admin.Id = int.Parse(reader["Id"].ToString());
                    admin.AdminName = reader["admin"].ToString();
                    admin.RealName = reader["real_name"].ToString();
                    admin.Password = reader["password"].ToString();
                    admin.State = bool.Parse(reader["state"].ToString());
                    admin.RoleId = int.Parse(reader["role_id"].ToString());
                }
            }
            return admin;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "admin");
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
