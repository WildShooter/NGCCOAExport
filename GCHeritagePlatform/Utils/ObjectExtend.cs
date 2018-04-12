using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace GCHeritagePlatform.Utils
{
    public static class ObjectExtend
    {

        /// <summary>
        /// 只转换每个汉字首字母（大写）
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string ToChineseSpell(this string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }
        /// <summary>
        /// 获得第一个汉字的首字母（大写）；
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        private static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;

        }
        public static IList<T> ToEntList<T>(this DataTable dt) where T : new()
        {
            return DataTableToEnt<T>.FillModel(dt);
        }

        public static string GetExtensioName(this string fileName)
        {
            var strArr = fileName.Split('.');
            return strArr[strArr.Length - 1]; ;
        }

        public static string ToDate(this string dateStr) {
            var now = DateTime.Now;
            DateTime.TryParse(dateStr, out now);
            return now.ToShortDateString();
        }
        public static string ToDateTime(this string dateStr)
        {
            var now = DateTime.Now;
            DateTime.TryParse(dateStr, out now);
            return now.ToShortTimeString();
        }
        /// <summary>
        /// 根据自己的需求   通过自定义标签 来获取类上需要的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ent">实体类</param>
        /// <param name="attrbuteName">标签的名称</param>
        /// <param name="isHaveAttributeNameField">属性上有该标签的获取，还是没有标签的获取。默认是没有改标签的获取</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetNameToValueDic<T>(this T ent) {
           return GetNameToValueDic<T>(ent,"NoAddField", false);
        }
        public static Dictionary<string, object> GetNameToValueDic<T>(this T ent,string attrbuteName,bool isHaveAttributeNameField=false)
        {
            var dicName2Value = new Dictionary<string, object>();
            Type t = ent.GetType();//获得该类的Type
            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (isHaveAttributeNameField) //如果需要有某个自定义标签
                {
                    // 标签没有
                    if (pi.CustomAttributes.Count()>0) continue;
                    //如果有标签 且便签和要的标签 不一样 
                    if (pi.CustomAttributes.Count() > 0)
                    {
                        var attrTypeEnt = pi.CustomAttributes.Where(e => e.AttributeType.Name == attrbuteName);
                        if (attrbuteName == null) continue;
                    }
                }
                else {
                    if (pi.CustomAttributes.Count() > 0)
                    {
                        var attrTypeEnt = pi.CustomAttributes.Where(e => e.AttributeType.Name == attrbuteName);
                        if (attrbuteName != null) continue;
                    }
                }
                var value1 = pi.GetValue(ent, null);//用pi.GetValue获得值
                if (value1 == null) continue;
                string name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?))
                {
                    var dtTemp =(DateTime) value1;
                    if(dtTemp.Year==1)continue;
                    dicName2Value.Add(name, value1);
                }else if (pi.PropertyType==typeof(decimal?)||pi.PropertyType==typeof(int?)||pi.PropertyType==typeof(double?))
                {
                    dicName2Value.Add(name, value1);
                }
                else
                {
                    dicName2Value.Add(name, value1 + "");
                }




            }
            return dicName2Value;
        }


      
        /// <summary>
        /// 将数组转换成字符串
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="splitStr">字符串之间的分割 默认为'</param>
        /// <returns></returns>
        public static string ChangeListToString<T>(this IList<T> ids,string splitStr="'")
        {
            var idStr = ids.Aggregate("", (current, item) => current + (string.Format("{0}{1}{0}", splitStr, item).Trim() + ","));
            return idStr.TrimEnd(',');
        }
        public static string ChangeListToString(this IList<string> ids, string startFix,string endFix,char splitStr=',')
        {
            var idStr = ids.Aggregate("", (current, item) => current + (string.Format("{0}{1}{0}", startFix, item, endFix).Trim() + splitStr));
            return idStr.TrimEnd(splitStr);
        }
        public static string ChangeListToString(this IEnumerable<string> ids, string splitStr = "'")
        {
            var idStr = ids.Aggregate("", (current, item) => current + (string.Format("{0}{1}{0}", splitStr, item).Trim() + ","));
            return idStr.TrimEnd(',');
        }
        public static string[] Merge(this string[] strArr,string [] concatArr)
        {
            var lengthSum = strArr.Length + concatArr.Length;
            var strNewArr = new string[lengthSum];
            for (int i = 0; i < strArr.Length; i++)
            {
                strNewArr.SetValue(strArr[i], i);
            }
            for (int j = 0; j < concatArr.Length; j++)
            {
                strNewArr.SetValue(concatArr[j],strArr.Length+j);
            }
            return strNewArr;
        }
}
}