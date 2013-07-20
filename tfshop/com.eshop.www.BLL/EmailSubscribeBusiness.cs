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

namespace com.eshop.www.BLL
{
    public class EmailSubscribeBusiness
    {
        public bool Add(EmailSubscribe emailSubscribe) 
        { 
            return new EmailSubscribeDAO().Add(emailSubscribe); 
        }
        public bool Delete(int Id) 
        { 
            return new EmailSubscribeDAO().Delete(Id); 
        }
        public bool Update(EmailSubscribe emailSubscribe) 
        { 
            return new EmailSubscribeDAO().Update(emailSubscribe); 
        }
        public EmailSubscribe GetEntity(int Id) 
        { 
            return new EmailSubscribeDAO().GetEntity(Id); 
        }
        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new EmailSubscribeDAO().GetList(fieldList, orderField, orderBy, pageIndex, pageSize, where);
        }
        public bool IsHaveSameName(EmailSubscribe emailSubscribe) 
        { 
            return new EmailSubscribeDAO().IsHaveSameName(emailSubscribe); 
        }
        public bool DeleteByEmail(EmailSubscribe emailSubscribe) 
        { 
            return new EmailSubscribeDAO().DeleteByEmail(emailSubscribe); 
        }
        public bool UpdateSubscribe(EmailSubscribe emailSubscribe) 
        { 
            return new EmailSubscribeDAO().UpdateSubscribe(emailSubscribe); 
        }
        public EmailSubscribe GetEntityByEmail(EmailSubscribe emailObj) 
        { 
            return new EmailSubscribeDAO().GetEntityByEmail(emailObj); 
        }
    }
}
