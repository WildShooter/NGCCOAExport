using System.Collections.Generic;
using System.Xml.Serialization;
namespace GCHeritagePlatform.Models
{
    [XmlType(TypeName = "config")]
    public class XmlConfig
    {
        [XmlArray("Handlers")]
        public List<ServerHandler> ServerHandlerList { get; set; }
    }

    [XmlType(TypeName = "IHandler")]
    public class ServerHandler
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("address")]
        public string Address { get; set; }

        [XmlArray("Services")]
        public List<ServiceModuleModel> ServiceModuleModelList { get; set; }
    }
    [XmlType(TypeName = "Service")]
    public class ServiceModuleModel
    {
        //服务名称
        [XmlAttribute("name")]
        public string Name { get; set; }
        //别名
        [XmlAttribute("aliasname")]
        public string AliasName { get; set; }
        //服务类型
        [XmlAttribute("type")]
        public string ServiceType { get; set; }
        //所属业务模块
        [XmlAttribute("module")]
        public string Module { get; set; }

    }

    public class ServiceModuleModelDto
    {
        //服务名称
        public string Name { get; set; }
        //别名
        public string AliasName { get; set; }
        //服务类型
        public string ServiceType { get; set; }
        //所属业务模块
        public string Module { get; set; }
        //负责处理的IHandler
        public string Handler { get; set; }
        //服务组
        public string ServiceGroup { get; set; }

    }
}