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
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using com.eshop.www.Model;
using com.eshop.www.BLL;

/// <summary>
/// Summary description for ShoppingCart
/// </summary>
public class ShoppingCart
{
    public ShoppingCart()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string[] ProductIdArr { get; set; }
    public string[] NumberArr { get; set; }
    public HttpCookie GetShoppingCart(string name) 
    {
        HttpCookie shoppingCart = HttpContext.Current.Request.Cookies[name];
        if (shoppingCart == null)
            shoppingCart = new HttpCookie(name);
        return shoppingCart;
    }
    public void SplitArray(HttpCookie cookie)
    {
        string shoppingCartValue = cookie.Value;
        if (!string.IsNullOrEmpty(shoppingCartValue))
        {
            string[] values = shoppingCartValue.Split(',');
            ProductIdArr = new string[values.Length];
            NumberArr = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                ProductIdArr[i] = values[i].Split('|')[0];
                NumberArr[i] = values[i].Split('|')[1];
            }
        }
    }
    public void Add(int productId, int number,HttpCookie cookie)
    {
        if (cookie == null) return;

        string newCartValue = string.Empty;
        string shoppingCartValue = cookie.Value;
        if (string.IsNullOrEmpty(shoppingCartValue))
            newCartValue = productId + "|" + number;
        else
        {
            SplitArray(cookie);

            bool flag = false;
            for (int j = 0; j < ProductIdArr.Length; j++)
            {
                if (ProductIdArr[j] == productId.ToString())
                {
                    NumberArr[j] = (int.Parse(NumberArr[j]) + number).ToString();
                    flag = true;
                }
                newCartValue += ProductIdArr[j] + "|" + NumberArr[j] + ",";
            }
            if (!flag)
                newCartValue += productId + "|" + number + ",";
            if (!string.IsNullOrEmpty(newCartValue))
                newCartValue = newCartValue.TrimEnd(',');
        }
        cookie.Value = newCartValue;
    }
    public void Replace(int productId, int number, HttpCookie cookie)
    {
        if (cookie == null) return;

        string newCartValue = string.Empty;
        SplitArray(cookie);
        for (int j = 0; j < ProductIdArr.Length; j++)
        {
            if (ProductIdArr[j] == productId.ToString())
            {
                NumberArr[j] = number.ToString();
            }
            newCartValue += ProductIdArr[j] + "|" + NumberArr[j] + ",";
        }
        if (!string.IsNullOrEmpty(newCartValue))
            newCartValue = newCartValue.TrimEnd(',');
        
        cookie.Value = newCartValue;
    }

    public void Div(int productId, int number, HttpCookie cookie)
    {
        if (cookie == null) return;

        string newCartValue = string.Empty;
        SplitArray(cookie);
        for (int j = 0; j < ProductIdArr.Length; j++)
        {
            if (ProductIdArr[j] == productId.ToString())
            {
                int oldNumber = int.Parse(NumberArr[j]);
                if(oldNumber>1)
                    NumberArr[j] = (oldNumber - number).ToString();
            }
            newCartValue += ProductIdArr[j] + "|" + NumberArr[j] + ",";
        }
        if (!string.IsNullOrEmpty(newCartValue))
            newCartValue = newCartValue.TrimEnd(',');
        cookie.Value = newCartValue;
    }
    public void Delete(int productId, HttpCookie cookie)
    {
        if (cookie == null) return;

        string newCartValue = string.Empty;
        SplitArray(cookie);
        List<string> list = new List<string>();
        for (int j = 0; j < ProductIdArr.Length; j++)
        {
            if (ProductIdArr[j] != productId.ToString())
            {
                list.Add(ProductIdArr[j]+"|"+NumberArr[j]);
            }
        }
        newCartValue = string.Join(",",list.ToArray());
        cookie.Value = newCartValue;
    }
    public void Delete(HttpCookie cookie)
    {
        if (cookie == null) return;
        cookie.Value = "";
        cookie.Expires = DateTime.Now.AddDays(-1);

    }
    public List<ProductDetail> GetListByCookie(HttpCookie cookie) 
    {
        if (cookie == null) return null;
        SplitArray(cookie);
        List<ProductDetail> list = new List<ProductDetail>();
        ProductDetailBusiness business = new ProductDetailBusiness();
        ProductDetail productDetail = null;
        int productId = 0;
        if (ProductIdArr != null)
        {
            for (int j = 0; j < ProductIdArr.Length; j++)
            {
                if (!string.IsNullOrEmpty(ProductIdArr[j]))
                {
                    productId = int.Parse(ProductIdArr[j]);
                    productDetail = business.GetEntity(productId);
                    productDetail.SaleNumber = int.Parse(NumberArr[j]);
                    list.Add(productDetail);
                }
            }
        }
        
        return list;
    }
    public float GetTotalMoney(HttpCookie cookie)
    {
        if (cookie == null) return 0;
        SplitArray(cookie);
        ProductDetailBusiness business = new ProductDetailBusiness();
        ProductDetail productDetail = null;
        int productId = 0;
        float totalMoney = 0;
        if (ProductIdArr != null)
        {
            for (int j = 0; j < ProductIdArr.Length; j++)
            {
                if (!string.IsNullOrEmpty(ProductIdArr[j]))
                {
                    productId = int.Parse(ProductIdArr[j]);
                    productDetail = business.GetEntity(productId);
                    totalMoney += int.Parse(NumberArr[j]) * productDetail.SalePrice;
                }
            }
        }
        return totalMoney;
    }

}
