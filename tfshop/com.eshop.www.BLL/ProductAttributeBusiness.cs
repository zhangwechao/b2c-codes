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
    public class ProductAttributeBusiness
    {
        public int Add(ProductAttribute productAttribute) 
        { 
            return new ProductAttributeDAO().Add(productAttribute); 
        }
        public bool Delete(int Id) 
        {
            new ProductAttributeDAO().Delete(Id);
            new CategoryAttributeDAO().DeleteByAttribute(Id);
            return new ProductAttributeValueDAO().DeleteByAttributeId(Id);
        }
        public bool Update(ProductAttribute productAttribute) 
        {
            return new ProductAttributeDAO().Update(productAttribute); 
        }
        public ProductAttribute GetEntity(int Id) 
        {
            return new ProductAttributeDAO().GetEntity(Id); 
        }
        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new ProductAttributeDAO().GetList(fieldList, orderField, orderBy, pageIndex, pageSize, where);
        }
        public bool IsHaveSameName(string attributeName) 
        {
            return new ProductAttributeDAO().IsHaveSameName(attributeName); 
        }
        public int Next(int Id)
        {
            return new ProductAttributeDAO().Next(Id);
        }
        public bool IsHasNext(int Id)
        {
            return new ProductAttributeDAO().IsHasNext(Id);
        }
        public int Previous(int Id)
        {
            return new ProductAttributeDAO().Previous(Id);
        }
        public bool IsHasPrev(int Id)
        {
            return new ProductAttributeDAO().IsHasPrev(Id);
        }
    }
}
