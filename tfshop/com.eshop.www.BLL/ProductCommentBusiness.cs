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
    public class ProductCommentBusiness
    {
        public bool Add(ProductComment productComment)
        {
            return new ProductCommentDAO().Add(productComment);
        }

        public bool Delete(int Id)
        {
            return new ProductCommentDAO().Delete(Id);
        }

        public bool Update(ProductComment productComment)
        {
            return new ProductCommentDAO().Update(productComment);
        }

        public ProductComment GetEntity(int Id)
        {
            return new ProductCommentDAO().GetEntity(Id);
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            return new ProductCommentDAO().GetList(fieldList,orderField,orderBy,pageIndex,pageSize,where);
        }
        public int GetRecordCount(string where)
        {
            return new ProductCommentDAO().GetRecordCount(where);
        }
        public float GetAvgCommentByProduct(int productId)
        {
            return new ProductCommentDAO().GetAvgCommentByProduct(productId);
        }
        public int GetSumCommentByProduct(int productId)
        {
            return new ProductCommentDAO().GetSumCommentByProduct(productId);
        }
        public bool UpdateUserName(string newUserName, string oldUserName)
        {
            return new ProductCommentDAO().UpdateUserName(newUserName,oldUserName);
        }
    }
}
