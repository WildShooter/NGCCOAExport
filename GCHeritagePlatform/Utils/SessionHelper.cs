using System;
using System.Collections.Generic;
using System.Web;
using GCHeritagePlatform.Models;

namespace GCHeritagePlatform.Utils
{
    public class SessionHelper
    {
        public static T RequestValue<T>(string ValueName)
        {
            HttpContext rq = HttpContext.Current;
            T TempValue;
            if (rq.Request.QueryString[ValueName] != null)
            {
                TempValue = (T)Convert.ChangeType(rq.Request.QueryString[ValueName], typeof(T));
            }
            else
            {
                TempValue = default(T);
            }

            return TempValue;
        }
        private static string loginUserStr = "hpfuser";
        private static string checkCode = "hpfyzcode";
        private static string jcsj = "jcsjlist";
        public static void SetLoginUser(LoginUser o)
        {
            HttpContext.Current.Session[loginUserStr] = o;
        }

        public static LoginUser GetUser()
        {
           return HttpContext.Current.Session[loginUserStr] as LoginUser;
        }

        public static void SetCheckCode(string yzm)
        {
            HttpContext.Current.Session[checkCode] = yzm;
        }

        public static string GetCheckCode()
        {
            return HttpContext.Current.Session[checkCode]+"";
        }

        public static void SetJCSJList(object o)
        {
            HttpContext.Current.Session[jcsj] = o;
        }
       
    }
}