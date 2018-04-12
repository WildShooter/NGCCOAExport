using GCHeritagePlatform.Models;

using GCHeritagePlatform.Utils;
using Nancy;
using System;
using System.Collections.Generic;

using GCHeritagePlatform.Services.Login;
using GCHeritagePlatform.Services.PersonnelRightsManagement;
using Hprose.Client;
using GCHeritagePlatform.JCBG.WordCode;
using System.IO;
using Nancy.Helpers;
using System.Web;
using GCHeritagePlatform.Services;



//using GCHeritagePlatform.Services.PersonnelRightsManagement.Service;

namespace GCHeritagePlatform.Modules
{

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/Service"] = parameters =>
            {
                IDictionary<string, Type> serviceTypeDiction = new Dictionary<string, Type>();
                // serviceTypeDiction.Add("登陆服务", typeof(LoginService));
                serviceTypeDiction.Add("机构权限管理服务类", typeof(DepartmentRightsManagementService));
                serviceTypeDiction.Add("用户权限管理服务", typeof(UserRightsManagementService));
                serviceTypeDiction.Add("角色权限管理服务", typeof(RoleManagementService));
                serviceTypeDiction.Add("功能权限管理服务", typeof(FuncManageService));// 
                serviceTypeDiction.Add("扩展类管理服务", typeof(SpecialService));
                serviceTypeDiction.Add("word导出服务", typeof(ExportNgccoaWordByAsp));
                IDictionary<string, IList<HproseAttribute>> result = HproseAttExt.getHproseMethodInfoEx(serviceTypeDiction);
                return View["index.cshtml", result].WithHeader("Access-Control-Allow-Origin", "*");
            };
            Get["/oldIndex"] = parameters =>
            {
                IDictionary<string, Type> serviceTypeDiction = new Dictionary<string, Type>();
                serviceTypeDiction.Add("测试服务", typeof(LoginService));
                //serviceTypeDiction.Add("测试服务22", typeof(LoginService));
                //serviceTypeDiction.Add("资料签收服务", typeof(MaterialReceiveService));
                //serviceTypeDiction.Add("资料录入服务", typeof(ArchivesEnteringService));
                //serviceTypeDiction.Add("档案使用服务", typeof(ArchivesUsageService));

                IDictionary<string, IList<HproseAttribute>> result = HproseAttExt.getHproseMethodInfo(serviceTypeDiction);
                return View["index-old.cshtml", result].WithHeader("Access-Control-Allow-Origin", "*");
            };
        }
    }
}