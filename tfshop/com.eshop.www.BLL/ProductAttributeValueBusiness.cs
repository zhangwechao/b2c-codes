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
using System.Data;

namespace com.eshop.www.BLL
{
    public class ProductAttributeValueBusiness
    {
        public bool Add(ProductAttributeValue productAttributeValue)
        {
            return new ProductAttributeValueDAO().Add(productAttributeValue);
        }

        public bool Delete(int Id)
        {
            return new ProductAttributeValueDAO().Delete(Id);
        }

        public bool Update(ProductAttributeValue productAttributeValue)
        {
            return new ProductAttributeValueDAO().Update(productAttributeValue);
        }

        public ProductAttributeValue GetEntity(int Id)
        {
            return new ProductAttributeValueDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new ProductAttributeValueDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
        public ProductAttributeValue GetEntity(ProductAttributeValue o)
        {
            return new ProductAttributeValueDAO().GetEntity(o);
        }
        public int GetRecordCount(string where)
        {
            return new ProductAttributeValueDAO().GetRecordCount(where);
        }
        public bool UpdateByProductIdAndAttributeId(ProductAttributeValue productAttributeValue)
        {
            return new ProductAttributeValueDAO().UpdateByProductIdAndAttributeId(productAttributeValue);
        }
        public bool DeleteByAttributeId(int attributeId)
        {
            return new ProductAttributeValueDAO().DeleteByAttributeId(attributeId);
        }
        public bool DeleteByProductId(int productId)
        {
            return new ProductAttributeValueDAO().DeleteByProductId(productId);
        }
        public DataTable GetList(int attributeId, string attributeValue)
        {
            return new ProductAttributeValueDAO().GetList(attributeId,attributeValue);
        }
    }
}
