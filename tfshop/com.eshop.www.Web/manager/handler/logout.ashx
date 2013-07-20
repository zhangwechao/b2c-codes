<%@ WebHandler Language="C#" Class="logout" %>

using System;
using System.Web;

public class logout : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        System.Web.Security.FormsAuthentication.SignOut();
        context.Response.Write("ok");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}