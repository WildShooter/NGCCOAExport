using GCHeritagePlatform.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GCHeritagePlatform.Utils
{
    public class HproseAttExt {
        /// <summary>
        /// 根据字段的中文意思
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IList<HproseAttribute> getHproseMethodInfo(Type type)
        {
            IList<HproseAttribute> result = null;
            MethodInfo[] methodArr = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            if (methodArr != null && methodArr.Length > 0)
            {
                result = new List<HproseAttribute>(methodArr.Length);
                foreach (MethodInfo tmpmethod in methodArr)
                {
                    Attribute attribute = Attribute.GetCustomAttribute(tmpmethod, typeof(HproseAttribute));
                    if (attribute == null)
                        continue;
                    HproseAttribute dAttribute = attribute as HproseAttribute;
                    result.Add(dAttribute);
                }
            }
            return result;
        }

        /// <summary>
        /// 支持分组的方法说明
        /// </summary>
        /// <param name="serviceTypeDiction"></param>
        /// <returns></returns>
        public static IDictionary<string, IList<HproseAttribute>> getHproseMethodInfo(IDictionary<string, Type> serviceTypeDiction)
        {
            if (serviceTypeDiction == null || serviceTypeDiction.Count == 0)
                return null;
            IDictionary<string, IList<HproseAttribute>> hproseListDiction = new Dictionary<string, IList<HproseAttribute>>();
            foreach (string key in serviceTypeDiction.Keys)
            {
                Type type = serviceTypeDiction[key];
                IList<HproseAttribute> result = null;
                MethodInfo[] methodArr = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                if (methodArr != null && methodArr.Length > 0)
                {
                    result = new List<HproseAttribute>(methodArr.Length);
                    foreach (MethodInfo tmpmethod in methodArr)
                    {
                        Attribute attribute = Attribute.GetCustomAttribute(tmpmethod, typeof(HproseAttribute));
                        if (attribute == null)
                            continue;
                        HproseAttribute dAttribute = attribute as HproseAttribute;
                        result.Add(dAttribute);
                    }
                }
                hproseListDiction.Add(key, result);
            }

            return hproseListDiction;
        }

        public static IDictionary<string, IList<HproseAttribute>> getHproseMethodInfoEx(IDictionary<string, Type> serviceTypeDiction)
        {
            if (serviceTypeDiction == null || serviceTypeDiction.Count == 0)
                return null;
            IDictionary<string, IList<HproseAttribute>> hproseListDiction = new Dictionary<string, IList<HproseAttribute>>();
            foreach (string key in serviceTypeDiction.Keys)
            {
                Type type = serviceTypeDiction[key];
                IList<HproseAttribute> result = null;
                var customAttribute = type.GetCustomAttribute(typeof(ServiceAttribute));
                MethodInfo[] methodArr = type.GetMethods();//BindingFlags.Instance | BindingFlags.DeclaredOnly |
                if (methodArr != null && methodArr.Length > 0)
                {
                    result = new List<HproseAttribute>();
                    foreach (MethodInfo tmpmethod in methodArr)
                    {
                        Attribute attribute = Attribute.GetCustomAttribute(tmpmethod, typeof(HproseAttribute));
                        if (attribute == null)
                            continue;
                        HproseAttribute dAttribute = attribute as HproseAttribute;
                        if (customAttribute != null)
                        {
                            var sAttribute = customAttribute as ServiceAttribute;
                            dAttribute.Description = string.Format(dAttribute.Description, sAttribute.Description);
                            dAttribute.Method = string.Format(dAttribute.Method, sAttribute.Prefix);
                        }
                        result.Add(dAttribute);
                    }
                }
                hproseListDiction.Add(key, result);
            }

            return hproseListDiction;
        }
    }
}