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
    public class FriendLinkDAO
    {
        public bool Add(FriendLink friendLink)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into friend_link(link_name,url,order_by,is_show,image,alt) values (@link_name,@url,@order_by,@is_show,@image,@alt)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@link_name", DbType.String, friendLink.LinkName);
            database.AddInParameter(cmd, "@url", DbType.String, friendLink.URL);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, friendLink.OrderBy);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, friendLink.IsShow);
            database.AddInParameter(cmd, "@image", DbType.String, friendLink.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, friendLink.Alt);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from friend_link where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(FriendLink friendLink)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update friend_link set link_name=@link_name,url=@url,order_by=@order_by,is_show=@is_show,image=@image,alt=@alt where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@link_name", DbType.String, friendLink.LinkName);
            database.AddInParameter(cmd, "@url", DbType.String, friendLink.URL);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, friendLink.OrderBy);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, friendLink.IsShow);
            database.AddInParameter(cmd, "@image", DbType.String, friendLink.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, friendLink.Alt);
            database.AddInParameter(cmd, "@Id", DbType.Int32, friendLink.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public FriendLink GetEntity(int Id)
        {
            FriendLink friendLink = new FriendLink();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select link_name,url,order_by,is_show,image,alt from friend_link where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    friendLink.Id = Id;
                    friendLink.LinkName = reader["link_name"].ToString();
                    friendLink.URL = reader["url"].ToString();
                    friendLink.OrderBy = int.Parse(reader["order_by"].ToString());
                    friendLink.IsShow = bool.Parse(reader["is_show"].ToString());
                    friendLink.Image = reader["image"].ToString();
                    friendLink.Alt = reader["alt"].ToString();
                }
            }
            return friendLink;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "friend_link");
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
        public bool IsHaveSameName(FriendLink friendLink) 
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from friend_link where link_name=@link_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@link_name", DbType.String, friendLink.LinkName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        public int GetMaxOrder()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(order_by) maxOrderBy from friend_link";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length==0)
                return 1;
            else
                return int.Parse(obj.ToString()) + 1;
        }
    }
}
