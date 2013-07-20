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
    public class ProductDetailDAO
    {
        public int Add(ProductDetail productDetail)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into product_detail(product_name,summary,description,keywords,category_id,brand_id,integral,is_show,order_by,is_recommend,is_new,is_hot,is_discount,is_delete,is_comment,price,sale_price,html_name,click_number,create_date,sale_number,stock) values (@product_name,@summary,@description,@keywords,@category_id,@brand_id,@integral,@is_show,@order_by,@is_recommend,@is_new,@is_hot,@is_discount,@is_delete,@is_comment,@price,@sale_price,@html_name,default,default,@sale_number,@stock) select @@identity";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_name", DbType.String, productDetail.ProductName);
            database.AddInParameter(cmd, "@summary", DbType.String, productDetail.Summary);
            database.AddInParameter(cmd, "@description", DbType.String, productDetail.Description);
            database.AddInParameter(cmd, "@keywords", DbType.String, productDetail.Keywords);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, productDetail.CategoryId);
            database.AddInParameter(cmd, "@brand_id", DbType.Int32, productDetail.BrandId);
            database.AddInParameter(cmd, "@integral",DbType.Int32,productDetail.integral);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productDetail.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productDetail.OrderBy);
            database.AddInParameter(cmd, "@is_recommend", DbType.Boolean, productDetail.IsRecommend);
            database.AddInParameter(cmd, "@is_new", DbType.Boolean, productDetail.IsNew);
            database.AddInParameter(cmd, "@is_hot", DbType.Boolean, productDetail.IsHot);
            database.AddInParameter(cmd, "@is_discount", DbType.Boolean, productDetail.IsDiscount);
            database.AddInParameter(cmd, "@is_delete", DbType.Boolean, productDetail.IsDelete);
            database.AddInParameter(cmd, "@is_comment", DbType.Boolean, productDetail.IsComment);
            database.AddInParameter(cmd, "@price",DbType.Decimal,productDetail.Price);
            database.AddInParameter(cmd, "@sale_price",DbType.Decimal,productDetail.SalePrice);
            database.AddInParameter(cmd, "@html_name", DbType.String, productDetail.HtmlName);
            database.AddInParameter(cmd,"@sale_number",DbType.Int32,productDetail.SaleNumber);
            database.AddInParameter(cmd, "@stock", DbType.Int32, productDetail.Stock);
            return int.Parse(database.ExecuteScalar(cmd).ToString());
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from product_detail where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(ProductDetail productDetail)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_detail set product_name=@product_name,summary=@summary,description=@description,keywords=@keywords,category_id=@category_id,brand_id=@brand_id,integral=@integral,is_show=@is_show,order_by=@order_by,is_recommend=@is_recommend,is_new=@is_new,is_hot=@is_hot,is_discount=@is_discount,is_delete=@is_delete,is_comment=@is_comment,price=@price,sale_price=@sale_price,html_name=@html_name,sale_number=@sale_number,stock=@stock where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_name", DbType.String, productDetail.ProductName);
            database.AddInParameter(cmd, "@summary", DbType.String, productDetail.Summary);
            database.AddInParameter(cmd, "@description", DbType.String, productDetail.Description);
            database.AddInParameter(cmd, "@keywords", DbType.String, productDetail.Keywords);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, productDetail.CategoryId);
            database.AddInParameter(cmd, "@brand_id", DbType.Int32, productDetail.BrandId);
            database.AddInParameter(cmd, "@integral", DbType.Int32, productDetail.integral);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, productDetail.IsShow);
            database.AddInParameter(cmd, "@order_by", DbType.Int32, productDetail.OrderBy);
            database.AddInParameter(cmd, "@is_recommend", DbType.Boolean, productDetail.IsRecommend);
            database.AddInParameter(cmd, "@is_new", DbType.Boolean, productDetail.IsNew);
            database.AddInParameter(cmd, "@is_hot", DbType.Boolean, productDetail.IsHot);
            database.AddInParameter(cmd, "@is_discount", DbType.Boolean, productDetail.IsDiscount);
            database.AddInParameter(cmd, "@is_delete", DbType.Boolean, productDetail.IsDelete);
            database.AddInParameter(cmd, "@is_comment", DbType.Boolean, productDetail.IsComment);
            database.AddInParameter(cmd, "@price", DbType.Decimal, productDetail.Price);
            database.AddInParameter(cmd, "@sale_price", DbType.Decimal, productDetail.SalePrice);
            database.AddInParameter(cmd, "@html_name", DbType.String, productDetail.HtmlName);
            database.AddInParameter(cmd, "@sale_number", DbType.Int32, productDetail.SaleNumber);
            database.AddInParameter(cmd, "@stock", DbType.Int32, productDetail.Stock);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productDetail.Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public ProductDetail GetEntity(int Id)
        {
            ProductDetail productDetail = new ProductDetail();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select product_name,summary,description,keywords,category_id,brand_id,integral,is_show,order_by,is_recommend,is_new,is_hot,is_discount,is_delete,is_comment,price,sale_price,html_name,click_number,create_date,sale_number,stock from product_detail where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productDetail.Id = Id;
                    productDetail.BrandId = int.Parse(reader["brand_id"].ToString());
                    productDetail.CategoryId = int.Parse(reader["category_id"].ToString());
                    productDetail.ClickNumber = int.Parse(reader["click_number"].ToString());
                    productDetail.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                    productDetail.Description = reader["description"].ToString();
                    productDetail.HtmlName = reader["html_name"].ToString();
                    productDetail.IsComment = bool.Parse(reader["is_comment"].ToString());
                    productDetail.IsDelete= bool.Parse(reader["is_delete"].ToString());
                    productDetail.IsRecommend = bool.Parse(reader["is_recommend"].ToString());
                    productDetail.IsShow = bool.Parse(reader["is_show"].ToString());
                    productDetail.Keywords = reader["keywords"].ToString();
                    productDetail.OrderBy = int.Parse(reader["order_by"].ToString());
                    productDetail.ProductName = reader["product_name"].ToString();
                    productDetail.Summary = reader["summary"].ToString();
                    productDetail.integral = int.Parse(reader["integral"].ToString());
                    productDetail.IsDiscount = bool.Parse(reader["is_discount"].ToString());
                    productDetail.IsHot = bool.Parse(reader["is_hot"].ToString());
                    productDetail.IsNew = bool.Parse(reader["is_new"].ToString());
                    productDetail.Price = float.Parse(reader["price"].ToString());
                    productDetail.SalePrice = float.Parse(reader["sale_price"].ToString());
                    productDetail.SaleNumber = int.Parse(reader["sale_number"].ToString());
                    productDetail.Stock = int.Parse(reader["stock"].ToString());
                }
            }
            return productDetail;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "product_detail");
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

        public bool UpdateClickNumber(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_detail set click_number=click_number+1 where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateSaleNumber(int Id,int number)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_detail set sale_number=sale_number+@number where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            database.AddInParameter(cmd, "@number", DbType.Int32, number);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateStockNumber(int Id, int number)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_detail set stock=stock-@number where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            database.AddInParameter(cmd, "@number", DbType.Int32, number);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateDelete(ProductDetail productDetail)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_detail set is_delete=@is_delete where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@is_delete", DbType.Boolean, productDetail.IsDelete);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productDetail.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool IsHaveSameName(string productName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_detail where product_name=@product_name";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@product_name", DbType.String, productName);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        public bool IsHaveProductByCategory(int categoryId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_detail where category_id=@category_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@category_id", DbType.Int32, categoryId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString())>0;
        }
        public bool IsHaveProductByBrand(int brandId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) cnt from product_detail where brand_id=@brand_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@brand_id", DbType.Int32, brandId);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) > 0;
        }
        public int GetMaxOrder()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select max(order_by) maxOrderBy from product_detail";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            string max = database.ExecuteScalar(cmd).ToString();
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 1;
            else
                return int.Parse(obj.ToString()) + 1;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from product_detail where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return 0;
            else
                return int.Parse(obj.ToString());
        }
        public ProductDetail GetEntityByMaxOrder()
        {
            ProductDetail productDetail = new ProductDetail();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id, product_name,summary,description,keywords,category_id,brand_id,integral,is_show,order_by,is_recommend,is_new,is_hot,is_discount,is_delete,is_comment,price,sale_price,html_name,click_number,create_date,sale_number,stock from product_detail where order_by=(select max(order_by) from product_detail)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    productDetail.Id = int.Parse(reader["Id"].ToString());
                    productDetail.BrandId = int.Parse(reader["brand_id"].ToString());
                    productDetail.CategoryId = int.Parse(reader["category_id"].ToString());
                    productDetail.ClickNumber = int.Parse(reader["click_number"].ToString());
                    productDetail.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                    productDetail.Description = reader["description"].ToString();
                    productDetail.HtmlName = reader["html_name"].ToString();
                    productDetail.IsComment = bool.Parse(reader["is_comment"].ToString());
                    productDetail.IsDelete = bool.Parse(reader["is_delete"].ToString());
                    productDetail.IsRecommend = bool.Parse(reader["is_recommend"].ToString());
                    productDetail.IsShow = bool.Parse(reader["is_show"].ToString());
                    productDetail.Keywords = reader["keywords"].ToString();
                    productDetail.OrderBy = int.Parse(reader["order_by"].ToString());
                    productDetail.ProductName = reader["product_name"].ToString();
                    productDetail.Summary = reader["summary"].ToString();
                    productDetail.integral = int.Parse(reader["integral"].ToString());
                    productDetail.IsDiscount = bool.Parse(reader["is_discount"].ToString());
                    productDetail.IsHot = bool.Parse(reader["is_hot"].ToString());
                    productDetail.IsNew = bool.Parse(reader["is_new"].ToString());
                    productDetail.Price = float.Parse(reader["price"].ToString());
                    productDetail.SalePrice = float.Parse(reader["sale_price"].ToString());
                    productDetail.SaleNumber = int.Parse(reader["sale_number"].ToString());
                    productDetail.Stock = int.Parse(reader["stock"].ToString());
                }
            }
            return productDetail;
        }
        public int Next(int Id)
        {
            ProductDetail productDetail = new ProductDetail();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_detail where Id>@Id order by Id";
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
            string sql = "select max(Id) maxId from product_detail";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;

        }
        public int Previous(int Id)
        {
            ProductDetail productDetail = new ProductDetail();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select top 1 Id from product_detail where Id<@Id order by Id desc";
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
            string sql = "select min(Id) minId from product_detail";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            object obj = database.ExecuteScalar(cmd);
            if (obj == null || obj.ToString().Length == 0)
                return false;
            return int.Parse(obj.ToString()) == Id;
        }
        public DataRecordTable GetList2(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "view_product_attribute_value");
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
        public bool UpdateIsShow(int productId, bool isShow)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update product_detail set is_show=@is_show where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, productId);
            database.AddInParameter(cmd, "@is_show", DbType.Boolean, isShow);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
    }
}
