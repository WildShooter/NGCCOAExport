using Hprose.Server;
using System;
using System.Web;
using FrameworkCore.Utils;
using Hprose.Common;
using GCHeritagePlatform.Services.Login;
using GCHeritagePlatform.Services.PersonnelRightsManagement;
using GCHeritagePlatform.Utils;

namespace GCHeritagePlatform.Handlers
{
   

    /// <summary>
    /// Hprose服务发布页
    /// </summary>   
    public class HproseHandler : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {
        public static string[] GetHproseMethodAlias(string prefix, string[] serviceMethod)
        {
            var strArr = new string[serviceMethod.Length];
            for (int i = 0; i < serviceMethod.Length; i++)
            {
                strArr.SetValue(prefix + serviceMethod[i], i);
            }
            return strArr;
        }
        private static HproseHttpService service = new HproseHttpService();

        static HproseHandler()
        {
            service.Mode = Hprose.IO.HproseMode.MemberMode;
            service.IsDebugEnabled = true;
            service.IsP3pEnabled = true;
            service.IsCrossDomainEnabled = true;
            //不允许获取服务方法列表
            service.IsGetEnabled = true;
            //service.AddFilter();
            service.OnBeforeInvoke += Service_OnBeforeInvoke;
            service.OnSendError += Service_OnSendError;

            //注册hprose服务
           
          
          
            service.Add(new LoginService());
         
            service.Add(new DepartmentRightsManagementService());
            service.Add(new UserRightsManagementService());
          
          
            service.Add(new RoleManagementService());
            service.Add(new FuncManageService());

            service.Add(new UseRightService());
            service.Add(new SpecialService());
            service.Add(new GCHeritagePlatform.Services.ExportNgccoaWordByAsp());
        }

        private static void Service_OnSendError(Exception e, HproseContext context)
        {
           SystemLogger.getLogger().Debug(e.Message);
        }

        /// <summary>
        /// 调用方法前进认证
        /// </summary>
        private static void Service_OnBeforeInvoke(string name, object[] args, bool byRef, HproseContext context)
        {
            //throw new NotImplementedException();

           if( HttpContext.Current.Session== null)
                throw new NotImplementedException();
        }

        public void ProcessRequest(HttpContext context)
        {
            service.Handle(context);
        }

        public bool IsReusable
        {
            get
            {
               return true;
            }
        }
    }
}