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
using com.eshop.www.DAL;

namespace com.eshop.www.BLL
{
    public class OrderItemBusiness
    {
        public bool Add(OrderItem orderItem)
        {
            return new OrderItemDAO().Add(orderItem);
        }

        public bool Delete(int Id)
        {
            return new OrderItemDAO().Delete(Id);
        }
        public bool Delete(string orderId)
        {
            return new OrderItemDAO().Delete(orderId);
        }

        public bool Update(OrderItem orderItem)
        {
            return new OrderItemDAO().Update(orderItem);
        }

        public OrderItem GetEntity(int Id)
        {
            return new OrderItemDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new OrderItemDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
        public DataRecordTable GetList2(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new OrderItemDAO().GetList2(fieldList, orderField, orderBy, pageIndex, pageSize, where);
        }
        public bool UpdateIsComment(bool isComment, int id)
        {
            return new OrderItemDAO().UpdateIsComment(isComment,id);
        }
        public int GetCommentNumByOrderNo(string orderNo,bool isComment)
        {
            return new OrderItemDAO().GetCommentNumByOrderNo(orderNo,isComment);
        }
    }
}
