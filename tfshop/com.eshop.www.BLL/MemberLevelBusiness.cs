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
    public class MemberLevelBusiness
    {
        public bool Add(MemberLevel memberLevel)
        {
            return new MemberLevelDAO().Add(memberLevel);
        }

        public bool Delete(int Id)
        {
            return new MemberLevelDAO().Delete(Id);
        }

        public bool Update(MemberLevel memberLevel)
        {
            return new MemberLevelDAO().Update(memberLevel);
        }

        public MemberLevel GetEntity(int Id)
        {
            return new MemberLevelDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new MemberLevelDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
        public MemberLevel GetEntityByIntegral(int integral)
        {
            return new MemberLevelDAO().GetEntityByIntegral(integral);
        }
    }
}
