using System.Data;
using System.Linq;
using FrameworkCore.DBInterface;
using GCHeritagePlatform.Utils;

namespace GCHeritagePlatform.Services
{
    public class BaseCommon
    {
        /// <summary>
        /// 获取遗产地编码  根据权限 
        public static string GetYCDBMWhere( IDBHelper dbContext)
        {

            var o = SessionHelper.GetUser();
            if (o == null) return "";
            var sql = $@"select b.BM,DEPARTMENTID,USERID,ROLEID,Province,ROLETYPE from v_PRIVS_USER a

left join (
select BM,XZQBM as SF from HPF_YCJCXX_SJWHYC) b on a.Province=b.SF
  where USERID='{o.ID}' and ROLETYPE='省级' ";
            var dt = dbContext.getDataTableResult(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                var bmList = dt.Rows.Cast<DataRow>().Select(e => e["BM"] + "").ToList();
                return bmList.ChangeListToString("");
            }
            return "";
        }

        public static string GetYCDBMWhereEx(IDBHelper dbContext)
        {
            var o = SessionHelper.GetUser();
            if (o == null) return "";
            if (o.JGJB != "1" && o.JGJB != "2") return "";
            if (o.JGJB == "2") //? "省级" : "遗产地级";
                return " BM=" + o.ZCBFBM;
            var sql = $@"select b.BM,DEPARTMENTID,USERID,ROLEID,Province,ROLETYPE from v_PRIVS_USER a

left join (
select BM,XZQBM as SF from HPF_YCJCXX_SJWHYC) b on a.Province=b.SF
  where USERID='{o.ID}' and ROLETYPE='省级' ";
            var dt = dbContext.getDataTableResult(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                var bmList = dt.Rows.Cast<DataRow>().Select(e => e["BM"] + "").ToList();
                return string.Format("  SJWHYCBM in ({0})", bmList.ChangeListToString(""));
            }
            return "";
        }

        public static string GetYCDBMWhereEx1(IDBHelper dbContext)
        {
            var o = SessionHelper.GetUser();
            if (o == null) return "";
            if (o.JGJB != "1" && o.JGJB != "2") return "";
            if (o.JGJB == "2") //? "省级" : "遗产地级";
                return  o.SJWHYCBM;
            var sql = $@"select b.BM,DEPARTMENTID,USERID,ROLEID,Province,ROLETYPE from v_PRIVS_USER a

left join (
select BM,XZQBM as SF from HPF_YCJCXX_SJWHYC) b on a.Province=b.SF
  where USERID='{o.ID}' and ROLETYPE='省级' ";
            var dt = dbContext.getDataTableResult(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                var bmList = dt.Rows.Cast<DataRow>().Select(e => e["BM"] + "").ToList();
                return bmList.ChangeListToString("");
            }
            return "";
        }


      
    }
}