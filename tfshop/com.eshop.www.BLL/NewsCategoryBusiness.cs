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
    public class NewsCategoryBusiness
    {
        public bool Add(NewsCategory newsCategory) 
        {
            NewsCategoryDAO dao = new NewsCategoryDAO();
            int id =  dao.Add(newsCategory);
            if (newsCategory.FatherId == 0)
                newsCategory.Path = id.ToString();
            else
                newsCategory.Path += ","+id.ToString();
            newsCategory.Id = id;
            return dao.Update(newsCategory);
        }
        public bool Delete(int Id)
        {
            return new NewsCategoryDAO().Delete(Id);
        }
        public bool IsHaveSonCategory(int Id)
        {
            return new NewsCategoryDAO().IsHaveSonCategory(Id);
        }
        public bool IsHaveSameName(string categoryName)
        {
            return new NewsCategoryDAO().IsHaveSameName(categoryName);
        }
        public bool Update(NewsCategory newsCategory)
        {
            return new NewsCategoryDAO().Update(newsCategory);
        }

        public NewsCategory GetEntity(int Id)
        {
            return new NewsCategoryDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new NewsCategoryDAO().GetList(fieldList, orderField, orderBy, pageIndex, pageSize, where);
        }

        public int GetMaxOrder()
        {
            return new NewsCategoryDAO().GetMaxOrder();
        }
        public NewsCategory GetEntityByCategoryName(string categoryName)
        {
            return new NewsCategoryDAO().GetEntityByCategoryName(categoryName);
        }
        public int GetRecordCount(string where)
        {
            return new NewsCategoryDAO().GetRecordCount(where);
        }
        public NewsCategory GetEntityByMaxOrder()
        {
            return new NewsCategoryDAO().GetEntityByMaxOrder();
        }
        public int Next(int Id)
        {
            return new NewsCategoryDAO().Next(Id);
        }
        public bool IsHasNext(int Id)
        {
            return new NewsCategoryDAO().IsHasNext(Id);
        }
        public int Previous(int Id)
        {
            return new NewsCategoryDAO().Previous(Id);
        }
        public bool IsHasPrev(int Id)
        {
            return new NewsCategoryDAO().IsHasPrev(Id);
        }

        public int Next(int Id,int categoryId)
        {
            return new NewsCategoryDAO().Next(Id,categoryId);
        }
        public bool IsHasNext(int Id,int categoryId)
        {
            return new NewsCategoryDAO().IsHasNext(Id,categoryId);
        }
        public int Previous(int Id,int categoryId)
        {
            return new NewsCategoryDAO().Previous(Id,categoryId);
        }
        public bool IsHasPrev(int Id,int categoryId)
        {
            return new NewsCategoryDAO().IsHasPrev(Id,categoryId);
        }
    }
}
