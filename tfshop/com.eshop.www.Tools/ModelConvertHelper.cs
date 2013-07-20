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
using System.Data;
using System.Reflection;

namespace com.eshop.www.Tools
{
    public class ModelConvertHelper<T>where T:new()
    {
        public static IList<T> ConvertToModel(DataTable dt)
         {
            // 定义集合
             IList<T> ts = new List<T>();

            // 获得此模型的类型
             Type type = typeof(T);

            string tempName = "";

            foreach (DataRow dr in dt.Rows)
             {
                 T t = new T();

                // 获得此模型的公共属性
                 PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                 {
                     tempName = pi.Name;

                    // 检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                     {
                        object value = dr[tempName];
                        pi.SetValue(t, value, null);
                     }
                 }

                 ts.Add(t);
             }

            return ts;

         }
     }
    
}
