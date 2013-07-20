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
using com.eshop.www.DAL;
using com.eshop.www.Model;
namespace com.eshop.www.BLL
{
    public class PaymentMethodBusiness
    {
        public bool Add(PaymentMethod paymentMethod)
        {
            return new PaymentMethodDAO().Add(paymentMethod);
        }

        public bool Delete(int Id)
        {
            return new PaymentMethodDAO().Delete(Id);
        }

        public bool Update(PaymentMethod paymentMethod)
        {
            return new PaymentMethodDAO().Update(paymentMethod);
        }

        public PaymentMethod GetEntity(int Id)
        {
            return new PaymentMethodDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new PaymentMethodDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
    }
}
