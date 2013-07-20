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
    public class ProductDetailBusiness
    {
        public int Add(ProductDetail productDetail)
        {
            return new ProductDetailDAO().Add(productDetail);
        }

        public bool Delete(int Id)
        {
            new ProductDetailDAO().Delete(Id);
            return new ProductAttributeValueDAO().DeleteByProductId(Id);
        }

        public bool Update(ProductDetail productDetail)
        {
            return new ProductDetailDAO().Update(productDetail);
        }

        public ProductDetail GetEntity(int Id)
        {
            return new ProductDetailDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new ProductDetailDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }

        public bool UpdateClickNumber(int Id)
        {
            return new ProductDetailDAO().UpdateClickNumber(Id);
        }
        public bool UpdateDelete(ProductDetail productDetail)
        {
            return new ProductDetailDAO().UpdateDelete(productDetail);
        }
        public bool IsHaveSameName(string productName)
        {
            return new ProductDetailDAO().IsHaveSameName(productName);
        }
        public bool IsHaveProductByCategory(int categoryId)
        {
            return new ProductDetailDAO().IsHaveProductByCategory(categoryId);
        }
        public bool IsHaveProductByBrand(int brandId)
        {
            return new ProductDetailDAO().IsHaveProductByBrand(brandId);
        }
        public int GetMaxOrder()
        {
            return new ProductDetailDAO().GetMaxOrder();
        }
        public int GetRecordCount(string where)
        {
            return new ProductDetailDAO().GetRecordCount(where);
        }
        public ProductDetail GetEntityByMaxOrder()
        {
            return new ProductDetailDAO().GetEntityByMaxOrder();
        }
        public int Next(int Id)
        {
            return new ProductDetailDAO().Next(Id);
        }
        public bool IsHasNext(int Id)
        {
            return new ProductDetailDAO().IsHasNext(Id);

        }
        public int Previous(int Id)
        {
            return new ProductDetailDAO().Previous(Id);
        }
        public bool IsHasPrev(int Id)
        {
            return new ProductDetailDAO().IsHasPrev(Id);
        }
        public bool UpdateSaleNumber(int Id, int number)
        {
            return new ProductDetailDAO().UpdateSaleNumber(Id,number);
        }
        public bool UpdateStockNumber(int Id, int number)
        {
            return new ProductDetailDAO().UpdateStockNumber(Id,number);
        }
        public DataRecordTable GetList2(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new ProductDetailDAO().GetList2(fieldList, orderField, orderBy, pageIndex, pageSize, where);
        }
        public bool UpdateIsShow(int productId, bool isShow)
        {
            return new ProductDetailDAO().UpdateIsShow(productId,isShow);
        }
    
    }
}
