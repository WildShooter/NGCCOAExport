using GCHeritagePlatform.Models;
using System.IO;

namespace GCHeritagePlatform.Utils
{
    //服务模板类  
    public class ServiceModuleHelper
    {
        public ServiceModuleHelper() { }

        private string GetXMLConfigPath()
        {
            var pathConfig = System.Configuration.ConfigurationManager.AppSettings["ServerXMLPath"];
            var path = System.AppDomain.CurrentDomain.BaseDirectory + pathConfig;
            if (!File.Exists(path)) return "";
            return path;
        }

        //读取xml配置
        public XmlConfig GetSeriveData()
        {
            var xmlPath = GetXMLConfigPath();
            return XMLHelper.DeserializeFromXml<XmlConfig>(xmlPath);
        }
    }
}