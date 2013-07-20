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
    public class MemberBusiness
    {
        public bool Add(Member member)
        {
            return new MemberDAO().Add(member);
        }

        public bool Delete(int Id)
        {
            return new MemberDAO().Delete(Id);
        }

        public bool Update(Member member)
        {
            return new MemberDAO().Update(member);
        }
        public bool UpdateIntegral(Member member)
        {
            return new MemberDAO().UpdateIntegral(member);
        }
        public bool UpdateState(Member member)
        {
            return new MemberDAO().UpdateState(member);
        }
        public bool UpdatePassword(Member member)
        {
            return new MemberDAO().UpdatePassword(member);
        }
        public bool Validate(Member member)
        {
            return new MemberDAO().Validate(member);
        }
        public bool IsSameUserName(string userName)
        {
            return new MemberDAO().IsSameUserName(userName);
        }

        public Member GetEntity(int Id)
        {
            return new MemberDAO().GetEntity(Id);
        }
        public Member GetEntityByUserName(string userName)
        {
            return new MemberDAO().GetEntityByUserName(userName);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new MemberDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
        public bool IsSameEmail(string email)
        {
            return new MemberDAO().IsSameEmail(email);
        }
        
    }
}
