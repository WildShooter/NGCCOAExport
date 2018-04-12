using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services;
using GCHeritagePlatform.Services.PublicMornitor;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using GCHeritagePlatform.Services.Dock;
using GCHeritagePlatform.Services.Dock.Model;

namespace GCHeritagePlatform.Utils
{
    public class MethodHelper
    {
        public static Type GetTypeList(string classPath)
        {
            string path = CommonBusiness.ModelPath;
           //GCHeritagePlatform.Services.PublicMornitor.Model.HPF_BTYZTBH_LFKD
            var cType = Type.GetType(path+classPath);
            Type typeMaster = typeof(List<>);
            return typeMaster.MakeGenericType(cType);
        }
       
        public static Type GetTypeListFileSignEx(string classPath)
        {
            string path = CommonBusiness.ModelPath;
            //GCHeritagePlatform.Services.PublicMornitor.Model.HPF_BTYZTBH_LFKD
            var cType = Type.GetType(path + classPath);
            Type typeMaster = typeof(DockResultDataFileModelEx<>);
            return typeMaster.MakeGenericType(cType);
        }
        public static Type GetTypeListFileEx(string classPath)
        {
            string path = CommonBusiness.ModelPath;
            //GCHeritagePlatform.Services.PublicMornitor.Model.HPF_BTYZTBH_LFKD
            var cType = Type.GetType(path+classPath);
            Type typeMaster = typeof(DockResultDataFileModel<>);
            return typeMaster.MakeGenericType(cType);
        }
        public static Type GetTypeListFileEx(string classPath,string path)
        {
            string Path = path;
            //GCHeritagePlatform.Services.PublicMornitor.Model.HPF_BTYZTBH_LFKD
            var cType = Type.GetType(Path + classPath);
            Type typeMaster = typeof(DockResultDataFileModel<>);
            return typeMaster.MakeGenericType(cType);
        }
        public static Type GetTypeList(string className1,string className2)
        {
            string path = CommonBusiness.ModelPath;
            //GCHeritagePlatform.Services.PublicMornitor.Model.HPF_BTYZTBH_LFKD
            var cType1 = Type.GetType(path+className1);
            var cType2 = Type.GetType(path+className2);
            var typeMaster = typeof(DockResultDataDataDetailModel<,>);
            return typeMaster.MakeGenericType(cType1,cType2);
        }
        /// <summary>
        /// 预警专用
        /// </summary>
        /// <param name="className1"></param>
        /// <param name="className2"></param>
        /// <returns></returns>
        public static Type GetTypeListYJ(string className1, string className2)
        {
            string path = CommonBusiness.Warning2IndexModelPath;
            //GCHeritagePlatform.Services.PublicMornitor.Model.HPF_BTYZTBH_LFKD
            var cType1 = Type.GetType(path + className1);
            var cType2 = Type.GetType(path + className2);
            var typeMaster = typeof(DockResultDataDataDetailModel<,>);
            return typeMaster.MakeGenericType(cType1, cType2);
        }
        public static Type GetTypeListL(string className1, string className2, string className3)
        {
            //GCHeritagePlatform.Services.PublicMornitor.Model.HPF_BTYZTBH_LFKD
            string path = CommonBusiness.ModelPath;
            var cType1 = Type.GetType(path+className1);
            var cType2 = Type.GetType(path+className2);
            var cType3 = Type.GetType(path+className3);
            var typeMaster = typeof(DockResultSYCNodel<,,>);
            return typeMaster.MakeGenericType(cType1, cType2,cType3);
        }
        public static string[] GetHproseMethodAlias(string prefix, string[] serviceMethod)
        {
            var strArr = new string[serviceMethod.Length];
            for (int i = 0; i < serviceMethod.Length; i++)
            {
                strArr.SetValue(prefix + serviceMethod[i],i);
            }
            return strArr;
        }

        /// <summary>
        /// 根据字段的中文意思
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IList<HproseAttribute> getHproseMethodInfo(Type type)
        {
            IList<HproseAttribute> result=null;
            MethodInfo[] methodArr= type.GetMethods(BindingFlags.Instance|BindingFlags.DeclaredOnly|BindingFlags.Public);
            if (methodArr != null&& methodArr.Length>0)
            {
                result = new List<HproseAttribute>(methodArr.Length);
                foreach(MethodInfo tmpmethod  in methodArr)
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
                hproseListDiction.Add(key,result);
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
                        if (customAttribute != null) {
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