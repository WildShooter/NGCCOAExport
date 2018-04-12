using FrameworkCore.DBInterface;
using FrameworkCore.Utils;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using GCHeritagePlatform.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 自然环境 台风路径信息 HPF_ZRHJ_TFLJXX  台风预估点信息  HPF_ZRHJ_TFYGDXX
    /// </summary>
    public class DockZRHJ_TFLJService: DockBaseService
    {
        public DockZRHJ_TFLJService(string jsonStr, string funId, string heritageId,string className):base(jsonStr,funId,heritageId)
        {
            this.ClassName = className;

        }
        public string ClassName { get; set; }
        public override string ReceiveData()
        {

            var heritageId = this.HeritageId;
            var funId = this.FunId;
            var jsonStr = this.BusinessJsonStr;
            var cListType = MethodHelper.GetTypeList(ClassName);
            var entList = JsonHelper.DeserializeJsonToObject(jsonStr, cListType) as IList;
            //var entList = JsonHelper.DeserializeJsonToObject<List<HPF_ZRHJ_TFLJXX>>(jsonStr) ;
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var listSqlStr = new List<string>();
            foreach (var item in entList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = heritageId;
                }
                if (nameToValue.ContainsKey("ID"))                                                                                                                                                                                                                                                     
                {
                    nameToValue["ID"] = Guid.NewGuid();
                }
                else
                {
                    nameToValue.Add("ID", Guid.NewGuid());
                }
                //台风信息和对接数据有关联 
                var strSql = "";
                if (funId == "8020403")
                {
                     strSql = string.Format("select ID from HPF_ZRHJ_TF where YCDSJID='{0}' and GLYCBTID='{1}' ", nameToValue["PID"], heritageId);
                }
                else
                {
                     strSql = string.Format("select ID from HPF_ZRHJ_TFLJXX where YCDSJID='{0}' and GLYCBTID='{1}' ", nameToValue["PID"], heritageId);
                }
                var dtMain = dbContext.getDataTableResult(strSql);
                if (dtMain == null || dtMain.Rows.Count == 0)
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "请先对接台风信息！"));
                }
                //如果有改变PID的属性信息
                nameToValue["PID"] = dtMain.Rows[0][0].ToString();
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(ClassName, nameToValue));
            }

            try
            {
                if (!CheckIsDock(listSqlStr, listSqlStr, ClassName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
                dbContext.executeTransactionSQLList(listSqlStr);
                return JsonHelper.SerializeObject(new ResultModel(true, "对接成功"));
            }
            catch (Exception ex)
            {
                var strErr = string.Format("遗产地对接数据类数据【自然环境 台风路径或台风预估点】错误,参数：{0}#{1}#{2},具体错误：{3}", jsonStr, funId, heritageId, ex.Message);
                SystemLogger.getLogger().Error(strErr);
                return JsonHelper.SerializeObject(new ResultModel(false, ex.Message));
            }
        }
     
    }
}