using System;

namespace GCHeritagePlatform.Models
{
    /// <summary>
    /// 服务描述 方便发布服务的时候用的
    /// </summary>
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// 服务类属性
        /// </summary>
        /// <param name="serviceDescription">服务类描述</param>
        /// <param name="prefix">基类中方法的前缀</param>
        public ServiceAttribute(string serviceDescription,string prefix)
        {
            Description = serviceDescription;
            Prefix = prefix;
        }
        public string Prefix { get; set; }
        /// <summary>
        ///  服务说明
        /// </summary>
        public string Description { private set; get; }
    }
    /// <summary>
    /// Hprose方法的备注说明信息
    /// </summary>
    public class HproseAttribute:Attribute
    {
        public HproseAttribute(string method,string description)
        {
            Method = method;
            Description = description;
        }
        /// <summary>
        /// 方法描述
        /// </summary>
        public string Method { set; get; }

        /// <summary>
        ///  备注说明信息
        /// </summary>
        public string Description { set; get; }
    }


    public class NoUpdateField : Attribute
    {
        public NoUpdateField() { }
        public NoUpdateField(string description) {
            this.Description = description;
        }
        /// <summary>
        ///  备注说明信息
        /// </summary>
        public string Description { private set; get; }

    }
    /// <summary>
    /// 不增加
    /// </summary>
    public class NoAddField : Attribute
    {
        public NoAddField() { }
        public NoAddField(string description)
        {
            this.Description = description;
        }
        /// <summary>
        ///  备注说明信息
        /// </summary>
        public string Description { private set; get; }
    }
}