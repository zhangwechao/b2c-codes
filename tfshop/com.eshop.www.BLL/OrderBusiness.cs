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
using com.eshop.www.DAL;
using com.eshop.www.Model;
using System.Transactions;
using System.Data;

namespace com.eshop.www.BLL
{
    public class OrderBusiness
    {
        public bool Add(Order order,List<OrderItem> list)
        {
            TransactionOptions options = new TransactionOptions();
            //可以在事务期间读取可变数据，但是不可以修改。可以在事务期间添加新数据
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                try
                {
                    OrderItemBusiness orderItemDao = new OrderItemBusiness();
                    foreach (OrderItem item in list)
                    {
                        orderItemDao.Add(item);
                    }
                    new OrderDAO().Add(order);
                    scope.Complete();
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool Delete(string Id)
        {
            TransactionOptions options = new TransactionOptions();
            //可以在事务期间读取可变数据，但是不可以修改。可以在事务期间添加新数据
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                try
                {
                    new OrderItemBusiness().Delete(Id);
                    new OrderDAO().Delete(Id);
                    scope.Complete();
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool Update(Order order)
        {
            return new OrderDAO().Update(order);
        }
        public bool UpdateStatus(string Id, int status)
        {
            return new OrderDAO().UpdateStatus(Id,status);
        }

        public Order GetEntity(string Id)
        {
            return new OrderDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new OrderDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
        public bool UpdateTrade(Order order)
        {
            return new OrderDAO().UpdateTrade(order);
        }
        /// <summary>
        /// 确认收货业务逻辑
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool ConfirmReceiveBusiness(string orderNo)
        {
            OrderDAO orderDao = new OrderDAO();
            ProductDetailDAO productDetailDao = new ProductDetailDAO();
            OrderItemDAO orderItemDao = new OrderItemDAO();
            MemberDAO memberDao = new MemberDAO();

            Order order = orderDao.GetEntity(orderNo);
            //更新订单状态
            orderDao.UpdateStatus(orderNo, 4);
            //更新销售数量和库存
            string where = "order_id='" + orderNo + "'";
            string fieldList = "Id,order_id,product_id,price,quantity,total_money";
            DataRecordTable table = orderItemDao.GetList(fieldList, "Id", false, 1, 20, where);
            int productId = 0;
            int quantity = 0;
            foreach (System.Data.DataRow row in table.Table.Rows)
            {
                productId = int.Parse(row["product_id"].ToString());
                quantity = int.Parse(row["quantity"].ToString());
                productDetailDao.UpdateSaleNumber(productId, quantity);
                productDetailDao.UpdateStockNumber(productId, quantity);
            }
            //更新用户各分
            string integral = order.ModifyMoney == null ? order.TotalMoney.ToString("0") : order.ModifyMoney.Value.ToString("0");
            bool issuccess = memberDao.UpdateIntegral(new Member() { UserName = memberDao.GetEntity(order.MemberId).UserName, Integral = int.Parse(integral) });
            return issuccess;
        }
        /// <summary>
        /// 找出付款方式为在线支付，订单状态未完成，订单号是10位的订单
        /// 招商银行专用
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableByCMB()
        {
            return new OrderDAO().GetTableByCMB();
        }
    }
    
}
