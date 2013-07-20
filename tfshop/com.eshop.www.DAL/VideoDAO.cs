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
    public class VideoDAO
    {
        public bool Add(Video video)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into video(title,keywords,file_name,remark,is_show,order_by,image,alt,create_date) values (@title,@keywords,@file_name,@remark,@is_show,@order_by,@image,@alt,default)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@title", DbType.String, video.Title);
            database.AddInParameter(cmd, "@keywords", DbType.String, video.Keywords);
            database.AddInParameter(cmd, "@file_name", DbType.String, video.FileName);
            database.AddInParameter(cmd, "@remark", DbType.String, video.Remark);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, video.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, video.OrderBy);
            database.AddInParameter(cmd, "@image", DbType.String, video.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, video.Alt);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from video where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(Video video)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update video set title=@title,keywords=@keywords,file_name=@file_name,remark=@remark,is_show=@is_show,order_by=@order_by,image=@image,alt=@alt where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@title", DbType.String, video.Title);
            database.AddInParameter(cmd, "@keywords", DbType.String, video.Keywords);
            database.AddInParameter(cmd, "@file_name", DbType.String, video.FileName);
            database.AddInParameter(cmd, "@remark", DbType.String, video.Remark);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, video.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, video.OrderBy);
            database.AddInParameter(cmd, "@image", DbType.String, video.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, video.Alt);
            database.AddInParameter(cmd, "@Id", DbType.Int32, video.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public Video GetEntity(int Id)
        {
            Video video = new Video();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select title,keywords,file_name,remark,is_show,order_by,image,alt,create_date from video where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    video.Id = Id;
                    video.Title = reader["title"].ToString();
                    video.Keywords = reader["summary"].ToString();
                    video.FileName = reader["content"].ToString();
                    video.Remark = reader["click_number"].ToString();
                    video.IsShow = bool.Parse(reader["keywords"].ToString());
                    video.OrderBy = int.Parse(reader["order_by"].ToString());
                    video.Image = reader["image"].ToString();
                    video.Alt = reader["alt"].ToString();
                    video.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                }
            }
            return video;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "video");
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
            string sql = "select max(order_by) maxOrderBy from video";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 1;
           return int.Parse(obj.ToString()) + 1;
        }
    }
}
