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
    public class OrderDAO
    {
        public bool Add(Order order)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into [order] (Id,member_id,total_money,discount_money,shipping_method_id,payment_method_id,shipping_date_id,remark,create_date,order_status_id,invoice_head,receiver,address,tel,modify_money,modify_season) values (@Id,@member_id,@total_money,@discount_money,@shipping_method_id,@payment_method_id,@shipping_date_id,@remark,default,@order_status_id,@invoice_head,@receiver,@address,@tel,@modify_money,@modify_season)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.String, order.Id);
            database.AddInParameter(cmd,"@member_id",DbType.Int32,order.MemberId);
            database.AddInParameter(cmd, "@total_money", DbType.Decimal, order.TotalMoney);
            database.AddInParameter(cmd, "@discount_money", DbType.Decimal, order.DiscountMoney);
            database.AddInParameter(cmd, "@shipping_method_id", DbType.Int32, order.ShippingDateId);
            database.AddInParameter(cmd, "@payment_method_id", DbType.Int32, order.PaymentMethodId);
            database.AddInParameter(cmd, "@shipping_date_id", DbType.Int32, order.ShippingDateId);
            database.AddInParameter(cmd, "@order_status_id", DbType.Int32, order.OrderStatusId);
            database.AddInParameter(cmd, "@remark", DbType.String, order.Remark);
            database.AddInParameter(cmd, "@invoice_head", DbType.String, order.InvoiceHead);
            database.AddInParameter(cmd, "@receiver", DbType.String, order.Receiver);
            database.AddInParameter(cmd, "@address", DbType.String, order.Address);
            database.AddInParameter(cmd, "@tel", DbType.String, order.Tel);
            database.AddInParameter(cmd,"@modify_money",DbType.Decimal,order.ModifyMoney);
            database.AddInParameter(cmd,"@modify_season",DbType.String,order.ModifySeason);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(string Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from [order] where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.String, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(Order order)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update [order] set total_money=@total_money,discount_money=@discount_money,shipping_method_id=@shipping_method_id,payment_method_id=@payment_method_id,shipping_date_id=@shipping_date_id,remark=@remark,order_status_id=@order_status_id,invoice_head=@invoice_head,receiver=@receiver,address=@address,tel=@tel,modify_money=@modify_money,modify_season=@modify_season where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.String, order.Id);
            database.AddInParameter(cmd, "@total_money", DbType.Decimal, order.TotalMoney);
            database.AddInParameter(cmd, "@discount_money", DbType.Decimal, order.DiscountMoney);
            database.AddInParameter(cmd, "@shipping_method_id", DbType.Int32, order.ShippingDateId);
            database.AddInParameter(cmd, "@payment_method_id", DbType.Int32, order.PaymentMethodId);
            database.AddInParameter(cmd, "@shipping_date_id", DbType.Int32, order.ShippingDateId);
            database.AddInParameter(cmd, "@order_status_id", DbType.Int32, order.OrderStatusId);
            database.AddInParameter(cmd, "@remark", DbType.String, order.Remark);
            database.AddInParameter(cmd, "@invoice_head", DbType.String, order.InvoiceHead);
            database.AddInParameter(cmd, "@receiver", DbType.String, order.Receiver);
            database.AddInParameter(cmd, "@address", DbType.String, order.Address);
            database.AddInParameter(cmd, "@tel", DbType.String, order.Tel);
            database.AddInParameter(cmd, "@modify_money", DbType.Decimal, order.ModifyMoney);
            database.AddInParameter(cmd, "@modify_season", DbType.String, order.ModifySeason);

            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateTrade(Order order)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update [order] set trade_no=@trade_no,buyer_email=@buyer_email,trade_date=@trade_date where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.String, order.Id);
            database.AddInParameter(cmd, "@trade_no", DbType.String, order.TradeNo);
            database.AddInParameter(cmd, "@buyer_email", DbType.String, order.BuyerEmail);
            database.AddInParameter(cmd, "@trade_date", DbType.DateTime, order.TradeDate);

            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }
        public bool UpdateStatus(string Id,int status)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update [order] set order_status_id=@order_status_id where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.String, Id);
            database.AddInParameter(cmd, "@order_status_id", DbType.Int32, status);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public Order GetEntity(string Id)
        {
            Order order = new Order();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select member_id,total_money,discount_money,shipping_method_id,payment_method_id,shipping_date_id,remark,create_date,order_status_id,invoice_head,receiver,address,tel,modify_money,modify_season,trade_no,buyer_email,trade_date from [order] where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.String, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    order.Id = Id;
                    order.MemberId = int.Parse(reader["member_id"].ToString());
                    order.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                    order.OrderStatusId = int.Parse(reader["order_status_id"].ToString());
                    order.PaymentMethodId = int.Parse(reader["payment_method_id"].ToString());
                    order.ShippingDateId = int.Parse(reader["shipping_date_id"].ToString());
                    order.ShippingMethodId = int.Parse(reader["shipping_method_id"].ToString());
                    order.TotalMoney = float.Parse(reader["total_money"].ToString());
                    order.DiscountMoney = float.Parse(reader["discount_money"].ToString());
                    order.Remark = reader["remark"].ToString();
                    order.InvoiceHead = reader["invoice_head"].ToString();
                    order.Receiver = reader["receiver"].ToString();
                    order.Address = reader["address"].ToString();
                    order.Tel = reader["tel"].ToString();
                    if (reader["modify_money"].ToString().Length > 0)
                        order.ModifyMoney = float.Parse(reader["modify_money"].ToString());
                    order.ModifySeason = reader["modify_season"].ToString();
                    order.TradeNo = reader["trade_no"].ToString();
                    order.BuyerEmail = reader["buyer_email"].ToString();
                    if (reader["trade_date"].ToString().Length > 0)
                        order.TradeDate = DateTime.Parse(reader["trade_date"].ToString());
                }
            }
            return order;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "view_order");
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
        /// <summary>
        /// 找出付款方式为在线支付，订单状态未完成，订单号是10位的订单
        /// 招商银行专用
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableByCMB()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select Id,create_date from [order] where payment_method_id=3 and order_status_id!=4 and len(Id)=10";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            return database.ExecuteDataSet(cmd).Tables[0];
        }
    }
}
