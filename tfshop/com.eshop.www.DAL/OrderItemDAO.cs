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
    public class OrderItemDAO
    {
        public bool Add(OrderItem orderItem)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into order_item(order_id,member_id,product_id,quantity,price,total_money,create_date) values (@order_id,@member_id,@product_id,@quantity,@price,@total_money,default)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@order_id", DbType.String, orderItem.OrderId);
            database.AddInParameter(cmd,"@member_id",DbType.Int32,orderItem.MemberId);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, orderItem.ProductId);
            database.AddInParameter(cmd, "@quantity", DbType.Int32, orderItem.Quantity);
            database.AddInParameter(cmd, "@price", DbType.Decimal, orderItem.Price);
            database.AddInParameter(cmd, "@total_money", DbType.Decimal, orderItem.TotalMoney);
            return database.ExecuteNonQuery(cmd)>0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from order_item where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool Delete(string orderId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from order_item where order_id=@order_id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@order_id", DbType.String,orderId);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public bool Update(OrderItem orderItem)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update order_item set order_id=@order_id,member_id=@member_id,product_id=@product_id,quantity=@quantity,price=@price,total_money=@total_money,is_comment=@is_comment where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@order_id", DbType.String, orderItem.OrderId);
            database.AddInParameter(cmd, "@member_id", DbType.Int32, orderItem.MemberId);
            database.AddInParameter(cmd, "@product_id", DbType.Int32, orderItem.ProductId);
            database.AddInParameter(cmd, "@quantity", DbType.Int32, orderItem.Quantity);
            database.AddInParameter(cmd, "@price", DbType.Decimal, orderItem.Price);
            database.AddInParameter(cmd, "@total_money", DbType.Decimal, orderItem.TotalMoney);
            database.AddInParameter(cmd, "@is_comment", DbType.Boolean, orderItem.IsComment);
            database.AddInParameter(cmd, "@Id", DbType.Int32, orderItem.Id);
         
            return database.ExecuteNonQuery(cmd) > 0;
        }
        public bool UpdateIsComment(bool isComment,int id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update order_item set is_comment=@is_comment where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@is_comment", DbType.Boolean, isComment);
            database.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return database.ExecuteNonQuery(cmd) > 0;
        }

        public OrderItem GetEntity(int Id)
        {
            OrderItem orderItem = new OrderItem();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select order_id,member_id,product_id,quantity,price,total_money,create_date,is_comment from order_item where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    orderItem.Id = Id;
                    orderItem.OrderId = reader["order_id"].ToString();
                    orderItem.MemberId = int.Parse(reader["member_id"].ToString());
                    orderItem.Price = float.Parse(reader["price"].ToString());
                    orderItem.ProductId = int.Parse(reader["product_id"].ToString());
                    orderItem.Quantity = int.Parse(reader["quantity"].ToString());
                    orderItem.TotalMoney = float.Parse(reader["total_money"].ToString());
                    orderItem.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                    orderItem.IsComment = bool.Parse(reader["is_comment"].ToString());
                }
            }
            return orderItem;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "order_item");
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
        public DataRecordTable GetList2(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "view_orderItem");
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
        public int GetCommentNumByOrderNo(string orderNo,bool isComment)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) from order_item where order_id=@orderNo and is_comment=@is_comment";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd,"@orderNo",DbType.String,orderNo);
            database.AddInParameter(cmd,"@is_comment",DbType.Boolean,isComment);
            return int.Parse(database.ExecuteScalar(cmd).ToString());
        }
    }
}
