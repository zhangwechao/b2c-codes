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
    public class ProductBrandBusiness
    {
        public bool Add(ProductBrand productBrand)
        {
            return new ProductBrandDAO().Add(productBrand);
        }

        public bool Delete(int Id)
        {
            return new ProductBrandDAO().Delete(Id);
        }
        public bool IsHaveSameName(string brandName)
        {
            return new ProductBrandDAO().IsHaveSameName(brandName);
        }
        public bool Update(ProductBrand productBrand)
        {
            return new ProductBrandDAO().Update(productBrand);
        }

        public ProductBrand GetEntity(int Id)
        {
            return new ProductBrandDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new ProductBrandDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }

        public int GetMaxOrder()
        {
            return new ProductBrandDAO().GetMaxOrder();
        }
        public ProductBrand GetEntityByBrandName(string brandName)
        {
            return new ProductBrandDAO().GetEntityByBrandName(brandName);
        }
        public int GetRecordCount(string where)
        {
            return new ProductBrandDAO().GetRecordCount(where);
        }

        public ProductBrand GetEntityByMaxOrder()
        {
            return new ProductBrandDAO().GetEntityByMaxOrder();
        }
        public int Next(int Id)
        {
            return new ProductBrandDAO().Next(Id);
        }
        public bool IsHasNext(int Id)
        {
            return new ProductBrandDAO().IsHasNext(Id);
        }
        public int Previous(int Id)
        {
            return new ProductBrandDAO().Previous(Id);
        }
        public bool IsHasPrev(int Id)
        {
            return new ProductBrandDAO().IsHasPrev(Id);
        }
    }
}
