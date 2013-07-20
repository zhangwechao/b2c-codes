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
    public class ProductVideoDAO
    {
        public bool Add(ProductVideo productVideo)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_video(product_id,video_id) values (@product_id,@video_id)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productVideo.ProductId);
            database.AddInParameter(cmd, "@video_id", DbType.Int32, productVideo.VideoId);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_video where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductVideo productVideo)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_video set product_id=@product_id,video_id=@video_id where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productVideo.ProductId);
            database.AddInParameter(cmd, "@video_id", DbType.Int32, productVideo.VideoId);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productVideo.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductVideo GetEntity(int Id)
        {
            ProductVideo productVideo = new ProductVideo();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select product_id,video_id from product_video where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productVideo.Id = Id;
                    productVideo.ProductId = int.Parse(reader["product_id"].ToString());
                    productVideo.VideoId = int.Parse(reader["video_id"].ToString());
                }
            }
            return productVideo;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_video");
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
        public DataTable GetListByProductId(int productId)
        {
            string sql = "select video.Id id,video.title title,video.file_name file_name,video.order_by order_by,video.remark remark,video.create_date create_date from video inner join product_video" +
                "  on video.Id=product_video.video_id where product_video.product_id=@productId and video.is_show=1 order by video.order_by desc";
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@productId", DbType.Int32, productId);
            DataSet ds = database.ExecuteDataSet(cmd);
            return ds.Tables[0];
        }
    }
}
