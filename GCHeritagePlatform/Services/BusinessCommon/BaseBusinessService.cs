using FrameworkCore.Utils;
using GCHeritagePlatform.Models;
using Newtonsoft.Json;
using System;

namespace GCHeritagePlatform.Services
{
    /// <summary>
    /// 单表的增删改查基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseBusinessService<T>: BaseService<T>, IBaseBusinessService where T : class
     {
        public string MethodPrefix { get; set; }
        public string HproseServiceName { get; set; }
        [HproseAttribute("bool {0}AddBase(string strJson)", "{0}增加")]
        public virtual bool AddBase(string strJson)
        {
            //Session验证 
            try
            {
                var ent = JsonConvert.DeserializeObject<T>(strJson);
                return base.Add(ent);
            }
            catch (Exception ex)
            {
                SystemLogger.getLogger().Error(ex.Message.ToString());
                return false;
            }

        }
        [HproseAttribute("bool {0}UpdateBase(string strJson)", "{0}修改")]
        public virtual bool UpdateBase(string strJson)
        {
            //Session验证 
            try
            {
                var ent = JsonConvert.DeserializeObject<T>(strJson);
                return base.Update(ent);
            }
            catch (Exception ex)
            {
                SystemLogger.getLogger().Error(ex.Message.ToString());
                return false;
            }

        }
        [HproseAttribute("bool {0}DoCheck(Guid id)", "{0}审核")]
        public bool DoCheck(Guid id)
        {
            throw new NotImplementedException();
        }
        [HproseAttribute("bool {0}Sumbit(Guid id)", "{0}提交")]
        public bool Sumbit(Guid id)
        {
            throw new NotImplementedException();
        }
        public virtual  string[] GetMethodName()
        {
            return new string[] { "AddBase", "UpdateBase", "DoCheck", "Sumbit","Delete", "DeleteBySql" };
        }
    }
}