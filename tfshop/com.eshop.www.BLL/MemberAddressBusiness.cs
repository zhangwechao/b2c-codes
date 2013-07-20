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
    public class MemberAddressBusiness
    {
        public bool Add(MemberAddress memberAddress)
        {
            return new MemberAddressDAO().Add(memberAddress);
        }
        public bool Delete(int Id)
        {
            return new MemberAddressDAO().Delete(Id);
        }
        public bool Update(MemberAddress memberAddress)
        {
            return new MemberAddressDAO().Update(memberAddress);
        }
        public MemberAddress GetEntity(int Id)
        {
            return new MemberAddressDAO().GetEntity(Id);
        }
        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new MemberAddressDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
        public int GetRecordCount(string where)
        {
            return new MemberAddressDAO().GetRecordCount(where);
        }
    }
}
