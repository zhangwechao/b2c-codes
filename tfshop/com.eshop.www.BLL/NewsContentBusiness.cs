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
    public class NewsContentBusiness
    {
        public bool Add(NewsContent newsContent)
        {
            return new NewsContentDAO().Add(newsContent);
        }

        public bool Delete(int Id)
        {
            return new NewsContentDAO().Delete(Id);
        }

        public bool Update(NewsContent newsContent)
        {
            return new NewsContentDAO().Update(newsContent);
        }
        public bool UpdateDelete(bool isDelete, int Id)
        {
            return new NewsContentDAO().UpdateDelete(isDelete,Id);
        }
        public bool IsHaveNews(int categoryId)
        {
            return new NewsContentDAO().IsHaveNews(categoryId);
        }
        public NewsContent GetEntity(int Id)
        {
            return new NewsContentDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new NewsContentDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }

        public bool UpdateClickNumber(int Id)
        {
            return new NewsContentDAO().UpdateClickNumber(Id);
        }

        public int GetMaxOrder()
        {
            return new NewsContentDAO().GetMaxOrder();
        }
        public int GetRecordCount(string where)
        {
            return new NewsContentDAO().GetRecordCount(where);
        }
        public NewsContent GetEntityByMaxOrder()
        {
            return new NewsContentDAO().GetEntityByMaxOrder();
        }
        public int Next(int Id)
        {
            return new NewsContentDAO().Next(Id);
        }
        public bool IsHasNext(int Id)
        {
            return new NewsContentDAO().IsHasNext(Id);
        }
        public int Previous(int Id)
        {
            return new NewsContentDAO().Previous(Id);
        }
        public bool IsHasPrev(int Id)
        {
            return new NewsContentDAO().IsHasPrev(Id);
        }

        public int Next(int Id,int categoryId)
        {
            NewsContentDAO dao = new NewsContentDAO();
            if (categoryId == 3)
            {
                string ids = GetIds(categoryId);
                return dao.Next(Id, ids);
            }
            return dao.Next(Id,categoryId);
        }
        public bool IsHasNext(int Id,int categoryId)
        {
            NewsContentDAO dao = new NewsContentDAO();
            if (categoryId == 3)
            {
                string ids = GetIds(categoryId);
                return dao.IsHasNext(Id,ids);
            }
            return dao.IsHasNext(Id,categoryId);
        }
        public int Previous(int Id,int categoryId)
        {
            NewsContentDAO dao = new NewsContentDAO();
            if (categoryId == 3)
            {
                string ids = GetIds(categoryId);
                return dao.Previous(Id,ids);
            }
            return dao.Previous(Id,categoryId);
        }
        public bool IsHasPrev(int Id,int categoryId)
        {
            NewsContentDAO dao = new NewsContentDAO();
            if (categoryId == 3)
            {
                string ids = GetIds(categoryId);
                return dao.IsHasPrev(Id,ids);
            }
            return dao.IsHasPrev(Id,categoryId);
        }
        private string GetIds(int categoryId)
        {
            NewsCategoryDAO newsCategoryDao = new NewsCategoryDAO();
            string path = newsCategoryDao.GetEntity(categoryId).Path;
            string prefix = path.Substring(0, path.IndexOf(categoryId.ToString()) + 1);
            DataRecordTable table = newsCategoryDao.GetList("Id,category_name", "order_by", false, 1, 100, "path like '" + prefix + "%'");
            int count = table.Table.Rows.Count;
            DataRow row = null;
            string id = string.Empty;
            string ids = string.Empty;
            for (int i = 0; i < count; i++)
            {
                row = table.Table.Rows[i];
                id = row["Id"].ToString();
                if (i == count - 1)
                    ids += id;
                else
                    ids += id + ",";
            }
            return ids;
        }
        
    }
}
