using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using GCHeritagePlatform.Services;
using GCHeritagePlatform.Utils;
using System.Data;

namespace GCHeritagePlatform.Modules
{
    /// <summary>
    ///通用的 文件下载与上传模块
    /// </summary>
    public class FileModule : NancyModule
    {
        
        public FileModule():base("file")
        {
         

          
           
            //文件上传
            Options["/Upload/"] = p =>
            {
                return Response.AsText("true").WithHeader("Access-Control-Allow-Origin", "*");
            };

       

           
         
           }

        

    }

    public class FileDto
    {
        public Guid Guid { get; set; }
        public string FileName { get; set; }

        public string ExtName { get; set; }
        public string FilePath { get; set; }
    }
}