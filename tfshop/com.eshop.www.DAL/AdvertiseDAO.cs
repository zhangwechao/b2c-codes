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
    public class AdvertiseDAO
    {
        public bool Add(Advertise advertise)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into advertise(adv_name,width,height,image,flash,flag) values (@adv_name,@width,@height,@image,@flash,@flag)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@adv_name", DbType.String, advertise.AdvName);
            database.AddInParameter(cmd, "@width", DbType.Int32, advertise.Width);
            database.AddInParameter(cmd, "@height", DbType.Int32, advertise.Height);
            database.AddInParameter(cmd, "@image", DbType.String, advertise.Image);
            database.AddInParameter(cmd, "@flash", DbType.String, advertise.flash);
            database.AddInParameter(cmd, "@flag", DbType.Boolean, advertise.Flag);
            //database.AddParameter(cmd, "@returnValue", DbType.Int32, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);

            //int row = database.ExecuteNonQuery(cmd);
            //int returnValue = int.Parse(database.GetParameterValue(cmd, "@returnValue").ToString());
            //return returnValue == 0;
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from advertise where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
            //int row = database.ExecuteNonQuery(cmd);
            //int returnValue = int.Parse(database.GetParameterValue(cmd, "@returnValue").ToString());
            //return returnValue == 0;
        }

        public bool Update(Advertise advertise)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update advertise set adv_name=@adv_name,width=@width,height=@height,image=@image,flash=@flash,flag=@flag where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@adv_name", DbType.String, advertise.AdvName);
            database.AddInParameter(cmd, "@width", DbType.Int32, advertise.Width);
            database.AddInParameter(cmd, "@height", DbType.Int32, advertise.Height);
            database.AddInParameter(cmd, "@image", DbType.String, advertise.Image);
            database.AddInParameter(cmd, "@flash", DbType.String, advertise.flash);
            database.AddInParameter(cmd, "@flag", DbType.Boolean, advertise.Flag);
            database.AddInParameter(cmd, "@Id", DbType.Int32, advertise.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public Advertise GetEntity(int Id)
        {
            Advertise advertise = new Advertise();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select adv_name,width,height,image,flash,flag from advertise where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    advertise.Id = Id;
                    advertise.AdvName = reader["adv_name"].ToString();
                    advertise.Width = int.Parse(reader["width"].ToString());
                    advertise.Height = int.Parse(reader["height"].ToString());
                    advertise.Image = reader["image"].ToString();                   
                    advertise.flash = reader["flash"].ToString();
                    advertise.Flag = bool.Parse(reader["flag"].ToString());
                }
            }
            return advertise;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "advertise");
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
        public int Next(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from advertise where Id>@Id order by Id";
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
            string sql = "select max(Id) maxId from advertise";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;

        }
        public int Previous(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from advertise where Id<@Id order by Id desc";
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
            string sql = "select min(Id) minId from advertise";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
    }
}
