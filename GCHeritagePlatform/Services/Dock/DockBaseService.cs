using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using System.IO;
using GCHeritagePlatform.Utils;
using Hprose.Server;
using GCHeritagePlatform.Models;
using System.Reflection;
using GCHeritagePlatform.Services.BaseInfo.Interface;
using System.Collections;
using FrameworkCore.Utils;
using GCHeritagePlatform.Services.Models;
using FrameworkCore.DBInterface;
using System.Data;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 遗产地通用对接类
    /// </summary>
    public class DockBaseService: IDockBaseService
    {
        /// <summary>
        /// 业务json数据,每一个大类都有固有的JSON格式 。
        /// </summary>
        public string BusinessJsonStr { get; set; }
        /// <summary>
        /// 与遗产地对应的配置功能ID 
        /// </summary>
        public string FunId { get; set; }
        /// <summary>
        /// 遗产地ID
        /// </summary>
        public string HeritageId { get; set; }
        public DockBaseService(string jsonStr, string funId, string heritageId)
        {
            this.BusinessJsonStr = jsonStr;
            this.FunId = funId;
            this.HeritageId = heritageId;
        }
        //检验数据是否对接过
        protected bool CheckIsDock(List<string> listSql,IList<string> listStr, string tableName, IDBHelper idbHelper)
        {
           
            if (listStr==null||listStr.Count == 0) return true;
            var ids = listStr.ChangeListToString();
            var sql = string.Format("select * from {0} where YCDSJID in ({1})", tableName, ids);
            var dt = idbHelper.getDataTableResult(sql);
            if(dt!=null&&dt.Rows.Count!=0 &&dt.Rows.Count!=listStr.Count)
            {
                var deleteIds = dt.Rows.Cast<DataRow>().Select(e => e["YCDSJID"] + "").ChangeListToString();
                var deleteSql = string.Format("delete from {0} where YCDSJID in ({1})", tableName, deleteIds);
                listSql.Insert(0, deleteSql);
                return true;
            }
            return dt == null || dt.Rows.Count == 0;
        }

        protected string GetModelName(string viewName)
        {   //v_HPF_ZRHJ_TF->HPF_ZRHJ_TF
            //有的时候在XML中建的是视图,但是我们是往表中插入数据,所以要把传进来的视图名进行改正,也就是去掉V（约定视图名以v或V开头）
            return viewName.Replace("v_", "").Replace("V_", "");
        }

        protected string GetExeListSQL(IDBHelper dbContext,List<string> listSQL)
        {
            var bResult= dbContext.executeTransactionSQLList(listSQL);
            return JsonHelper.SerializeObject(new ResultModel(bResult, bResult ? "对接成功!" : "对接失败!"));
        }
        protected FunctionalModule FindFunModel(string heritageId,string funId)
        {
            //根据遗产地ID 返回XML文件
            var xmlConfig = CommonBusiness.GetMornitorConfig(heritageId);
            var funList = xmlConfig.GetFunctionalModules();
            return funList.FirstOrDefault(e => e.ID == funId);
        }
        /// <summary>
        /// 在病害调查监测工作情况记录中查找ID
        /// </summary>
        /// <param name="dbHelper"></param>
        /// <param name="bhjcdcjlid"></param>
        /// <param name="bhbh"></param>
        /// <param name="newBhId"></param>
        /// <returns></returns>
        public bool GetBhdcjlId(IDBHelper dbHelper, string bhjcdcjlid, string bhbh, out string newBhId)
        {
            newBhId = "";
            var sql = string.Format(" select ID from HPF_BTYZTBH_BHDCJCGZQKJLB where GLYCBTID='{0}' and (YCDSJID='{1}' or BHBH='{2}')", HeritageId, bhjcdcjlid, bhbh);
            var dt = dbHelper.getDataTableResult(sql);
            if (dt == null || dt.Rows.Count == 0) return false;
            newBhId = dt.Rows[0]["ID"].ToString();
            return true;
        }
        public virtual string ReceiveData()
        {
            var funModel = FindFunModel(HeritageId,FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象
            var cListType = MethodHelper.GetTypeList(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var entList = JsonHelper.DeserializeJsonToObject(BusinessJsonStr, cListType) as IList;//遗产地发过来的字符串（json格式）的项与我们在model中建的功能类的属性是一一对应的,这里进行赋值

            var listSqlStr = new List<string>();//组织SQL 统一插入
            var listYSJID = new List<string>(); //遗产地数据ID 验证对接
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            foreach (var item in entList)//因为要将接收过来的数据写到总平台数据库中,所以需要添加ID,以及进行遗产地数据ID进行检查,防止重复插入
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = HeritageId;
                }
                if (nameToValue.ContainsKey("ID"))
                {
                    nameToValue["ID"] = Guid.NewGuid();
                }
                else
                {
                    nameToValue.Add("ID", Guid.NewGuid());
                }
                var yscid = nameToValue["YCDSJID"].ToString() + "";
                if (!string.IsNullOrEmpty(yscid))
                {
                    listYSJID.Add(yscid);//防止重复对接
                }
                listSqlStr.Add(context.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }

            if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, context))  {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            }
            return GetExeListSQL(context, listSqlStr);
        }


        public virtual string UpdateData()
        {
            var funModel = FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象
            var cListType = MethodHelper.GetTypeList(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var entList = JsonHelper.DeserializeJsonToObject(BusinessJsonStr, cListType) as IList;//遗产地发过来的字符串（json格式）的项与我们在model中建的功能类的属性是一一对应的,这里进行赋值

            var listSqlStr = new List<string>();//组织SQL 统一插入
            var listYSJID = new List<string>(); //遗产地数据ID 验证对接
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            foreach (var item in entList)//因为要将接收过来的数据写到总平台数据库中,所以需要添加ID,以及进行遗产地数据ID进行检查,防止重复插入
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = HeritageId;
                }
                var yscid = nameToValue["YCDSJID"] + "";
                var id = GetDockedDataID(HeritageId, GetModelName(funModel.TableName), yscid, context);
                if (string.IsNullOrEmpty(id))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "没有找到对接过得数据信息！"));
                }
                nameToValue["ID"] = id;
                if (nameToValue.ContainsKey("ID"))
                {
                    nameToValue["ID"] = Guid.NewGuid();
                }
                listSqlStr.Add(context.updateByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            return GetExeListSQL(context, listSqlStr);
        }

        private string GetDockedDataID(string heritageId,string tableName,string yscid, IDBHelper dbcontext)
        {
            var sql = string.Format("select ID from {0} where GLYCBTID='{1}' and YCDSJID='{2}'", tableName, heritageId,
                yscid);
            var dtMain = dbcontext.getDataTableResult(sql);
            if (dtMain == null || dtMain.Rows.Count == 0)
            {
                return "";
            }
            return dtMain.Rows[0]["ID"] + "";
        }
    }
}