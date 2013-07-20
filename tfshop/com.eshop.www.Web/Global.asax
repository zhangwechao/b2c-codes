<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    void Application_BeginRequest(Object sender, EventArgs e)
    {
        StartProcessRequest();
    }
    #region SQL注入式攻击代码分析

    ///<summary>
    ///处理用户提交的请求 
    ///</summary>
    private void StartProcessRequest()
    {
        string getkeys = "";
        string sqlErrorPage = "~/error.aspx";//转向的错误提示页面 
        if (System.Web.HttpContext.Current.Request.QueryString != null)
        {

            for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
            {
                getkeys = System.Web.HttpContext.Current.Request.QueryString.Keys[i];
                if (!ProcessSqlStr(System.Web.HttpContext.Current.Request.QueryString[getkeys]))
                {
                    System.Web.HttpContext.Current.Response.Redirect(sqlErrorPage);
                    System.Web.HttpContext.Current.Response.End();
                }
            }
        }
    }
    ///<summary>
    ///分析用户请求是否正常 
    ///</summary>
    ///<param >传入用户提交数据 </param>
    ///<returns>返回是否含有SQL注入式攻击代码 </returns>
    private bool ProcessSqlStr(string Str)
    {
        bool ReturnValue = true;
        if (Str.Trim() != "")
        {
            string SqlStr = "exec,insert,delete,count,*,chr,mid,master,truncate,char,declare,'";

            string[] anySqlStr = SqlStr.Split(',');
            foreach (string ss in anySqlStr)
            {
                if (Str.ToLower().IndexOf(ss) >= 0)
                {
                    ReturnValue = false;
                    break;
                }
            }
        }
        return ReturnValue;
    }
    #endregion   
       
</script>
