using GCHeritagePlatform.Utils;
using System;
using System.Configuration;
using System.Web;

namespace GCHeritagePlatform
{
   

    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            string name = "mySQL";
            //反射加载DLL
            string dbDllPath = System.Configuration.ConfigurationManager.AppSettings.Get(name);
            //连接字符串
            ConnectionStringSettings dbConnect = System.Configuration.ConfigurationManager.ConnectionStrings[name];
            string dbConnStr = dbConnect.ConnectionString;
            string provideName = dbConnect.ProviderName;
            //示例化数据库连接池
            DBHelperPool.Instance.Add(name, dbDllPath, dbConnStr, provideName);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
         
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
           {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}