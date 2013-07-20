﻿//--------------------------------------------------
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
//51aspx.com 下载 http://www.51aspx.com
namespace com.eshop.www.DAL
{
    public class MemberAddressDAO
    {
        public bool Add(MemberAddress memberAddress)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into member_address (recerver,address,phone,member_id) values (@recerver,@address,@phone,@member_id)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@receiver",DbType.String,memberAddress.Receiver);
            database.AddInParameter(cmd, "@address", DbType.String, memberAddress.Address);
            database.AddInParameter(cmd, "@phone", DbType.String, memberAddress.Phone);
            database.AddInParameter(cmd, "@member_id", DbType.Int32, memberAddress.MemberId);
            return database.ExecuteNonQuery(cmd)>0;
        }
        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from member_address where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool Update(MemberAddress memberAddress)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update member_address set receiver=@receiver,address=@address,phone=@phone where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@receiver", DbType.String, memberAddress.Receiver);
            database.AddInParameter(cmd, "@address", DbType.String, memberAddress.Address);
            database.AddInParameter(cmd, "@phone", DbType.String, memberAddress.Phone);
            database.AddInParameter(cmd, "@Id", DbType.Int32, memberAddress.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public MemberAddress GetEntity(int Id)
        {
            MemberAddress memberAddress = new MemberAddress();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select receiver,address,phone,member_id from member_address where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    memberAddress.Address = reader["address"].ToString();
                    memberAddress.Id = Id;
                    memberAddress.MemberId = int.Parse(reader["member"].ToString());
                    memberAddress.Phone = reader["phone"].ToString();
                    memberAddress.Receiver = reader["receiver"].ToString();
                }
            }
            return memberAddress;
        }
        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "member_address");
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
            string sql = "select count(Id) recordCount from member_address where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj!=null && obj.ToString().Length>0)
                return int.Parse(obj.ToString());
            else
                return 0;
        }
    }
}