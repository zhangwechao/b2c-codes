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
    public class ProductImageDAO
    {
        public bool Add(ProductImage productImage)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_image(product_id,image,alt,zoom_image,type,is_default) values (@product_id,@image,@alt,@zoom_image,@type,@is_default)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productImage.ProductId);
            database.AddInParameter(cmd, "@image", DbType.String, productImage.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productImage.Alt);
            database.AddInParameter(cmd, "@zoom_image", DbType.String, productImage.ZoomImage);
            database.AddInParameter(cmd, "@type", DbType.Int32, productImage.Type);
            database.AddInParameter(cmd,"@is_default",DbType.Boolean,productImage.IsDefault);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_image where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(ProductImage productImage)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_image set product_id=@product_id,image=@image,alt=@alt,zoom_image=@zoom_image,type=@type,is_default=@is_default where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productImage.ProductId);
            database.AddInParameter(cmd, "@image", DbType.String, productImage.Image);
            database.AddInParameter(cmd, "@alt", DbType.String, productImage.Alt);
            database.AddInParameter(cmd, "@zoom_image", DbType.String, productImage.ZoomImage);
            database.AddInParameter(cmd, "@type", DbType.Int32, productImage.Type);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productImage.Id);
            database.AddInParameter(cmd,"@is_default",DbType.Boolean,productImage.IsDefault);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public ProductImage GetEntity(int Id)
        {
            ProductImage productImage = new ProductImage();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select product_id,image,alt,zoom_image,type,is_default from product_image where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productImage.Id = Id;
                    productImage.ProductId = int.Parse(reader["product_id"].ToString());
                    productImage.Image = reader["image"].ToString();
                    productImage.Alt = reader["alt"].ToString();
                    productImage.ZoomImage = reader["zoom_image"].ToString();
                    productImage.Type = int.Parse(reader["type"].ToString());
                    productImage.IsDefault = bool.Parse(reader["is_default"].ToString());
                }
            }
            return productImage;
        }
        public ProductImage GetEntity(int productId,int type)
        {
            ProductImage productImage = new ProductImage();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id, product_id,image,alt,zoom_image,type,is_default from product_image where product_id=@product_id and type=@type";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, productId);
            database.AddInParameter(cmd, "@type", DbType.Int32, type);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productImage.Id = int.Parse(reader["Id"].ToString());
                    productImage.ProductId = int.Parse(reader["product_id"].ToString());
                    productImage.Image = reader["image"].ToString();
                    productImage.Alt = reader["alt"].ToString();
                    productImage.ZoomImage = reader["zoom_image"].ToString();
                    productImage.Type = int.Parse(reader["type"].ToString());
                    productImage.IsDefault = bool.Parse(reader["is_default"].ToString());
                }
            }
            return productImage;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_image");
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
