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
    public class CategoryAttributeBusiness
    {
        public bool Add(CategoryAttribute categoryAttribute) 
        {
            return new CategoryAttributeDAO().Add(categoryAttribute);
        }
        public bool Delete(int Id) 
        {
            return new CategoryAttributeDAO().Delete(Id);
        }
        public bool Update(CategoryAttribute categoryAttribute)
        {
            return new CategoryAttributeDAO().Update(categoryAttribute);
        }
        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new CategoryAttributeDAO().GetList(fieldList, orderField, orderBy, pageIndex, pageSize, where);
        }

        public CategoryAttribute GetEntity(int Id)
        {
            return new CategoryAttributeDAO().GetEntity(Id);
        }
        public bool DeleteByCategory(int categoryId)
        {
            return new CategoryAttributeDAO().DeleteByCategory(categoryId);
        }
        public bool DeleteByAttribute(int attributeId)
        {
            return new CategoryAttributeDAO().DeleteByAttribute(attributeId);
        }
        public bool UpdateState(bool state, int Id)
        {
            return new CategoryAttributeDAO().UpdateState(state, Id);
        }
        public bool IsSame(int categoryId, int attributeId)
        {
            return new CategoryAttributeDAO().IsSame(categoryId,attributeId);
        }
    }
}
