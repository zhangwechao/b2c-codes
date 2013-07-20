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
    public class ProductCategoryBusiness
    {
        public bool Add(ProductCategory productCategory) 
        {
            ProductCategoryDAO dao = new ProductCategoryDAO();
            int id = dao.Add(productCategory);
            if (productCategory.FatherId == 0)
                productCategory.Path = id.ToString();
            else
                productCategory.Path += "," + id.ToString();
            productCategory.Id = id;
            return dao.Update(productCategory);
        }
        public bool Delete(int Id) 
        {
            new ProductCategoryDAO().Delete(Id);
            return new CategoryAttributeBusiness().DeleteByCategory(Id);

        }
        public bool Update(ProductCategory productCategory) 
        {
            return new ProductCategoryDAO().Update(productCategory); 
        }
        public ProductCategory GetEntity(int Id) 
        {
            return new ProductCategoryDAO().GetEntity(Id); 
        }
        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new ProductCategoryDAO().GetList(fieldList, orderField, orderBy, pageIndex, pageSize, where);
        }
        public int GetMaxOrder() 
        {
            return new ProductCategoryDAO().GetMaxOrder(); 
        }
        public bool IsHaveSonCategory(int Id) 
        {
            return new ProductCategoryDAO().IsHaveSonCategory(Id); 
        }
        public bool IsHaveSameName(string categoryName) 
        {
            return new ProductCategoryDAO().IsHaveSameName(categoryName); 
        }
        public ProductCategory GetEntityByCategoryName(string categoryName)
        {
            return new ProductCategoryDAO().GetEntityByCategoryName(categoryName);
        }
        public int GetRecordCount(string where) 
        {
            return new ProductCategoryDAO().GetRecordCount(where); 
        }
        public ProductCategory GetEntityByMaxOrder() 
        {
            return new ProductCategoryDAO().GetEntityByMaxOrder(); 
        }
        public int Next(int Id) 
        {
            return new ProductCategoryDAO().Next(Id); 
        }
        public bool IsHasNext(int Id) 
        {
            return new ProductCategoryDAO().IsHasNext(Id); 
        }
        public int Previous(int Id) 
        {
            return new ProductCategoryDAO().Previous(Id); 
        }
        public bool IsHasPrev(int Id) 
        {
            return new ProductCategoryDAO().IsHasPrev(Id); 
        }
        public DataTable GetTable()
        {
            return new ProductCategoryDAO().GetTable();
        }
    }
}
