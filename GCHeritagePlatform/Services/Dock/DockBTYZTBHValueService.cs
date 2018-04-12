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
    /// </summary>
    public class DockBTYZTBHValueService : DockBaseService
    {
        public DockBTYZTBHValueService(string jsonStr, string funId, string heritageId, string relatedTableName, string relatedID) : base(jsonStr, funId, heritageId)
        {
            this.ClassName = relatedTableName;
            this.RelatedID = relatedID;

        }
        public string ClassName { get; set; }

        public string RelatedID { get; set; }
        public override string ReceiveData()
        {

            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象 
            var cListType = MethodHelper.GetTypeList(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var entList = JsonHelper.DeserializeJsonToObject(BusinessJsonStr, cListType) as IList;//遗产地发过来的字符串（json格式）的项与我们在model中建的功能类的属性是一一对应的,这里进行赋值
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var listSqlStr = new List<string>();
            var listInsertCount = new Dictionary<string,string>();
            foreach (var item in entList)
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
                var strSql = string.Format("select ID from {0} where GLYCBTID='{1}' and BHBH='{2}' ", ClassName, HeritageId,  nameToValue[RelatedID]);
                var dtMain = dbContext.getDataTableResult(strSql);
                if (dtMain == null || dtMain.Rows.Count == 0)
                {
                    var bhid = Guid.NewGuid();
                    var strBHSql = string.Format("insert into  {4} (ID,YCDSJID,BHBH,GLYCBTID,RKSJ) values('{0}','{1}','{2}','{3}','{5}')", bhid, Guid.NewGuid(), nameToValue[RelatedID], HeritageId, this.ClassName, System.DateTime.Now);
                    if (!listInsertCount.ContainsKey(nameToValue[RelatedID]+""))
                    {
                        listInsertCount.Add(nameToValue[RelatedID] + "",bhid.ToString());
                        listSqlStr.Add(strBHSql);
                    }
                    nameToValue[RelatedID] = listInsertCount[nameToValue[RelatedID].ToString()];
                }
                else
                {
                    nameToValue[RelatedID] = dtMain.Rows[0][0].ToString();
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }

            if (!CheckIsDock(listSqlStr, listSqlStr, ClassName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            return GetExeListSQL(dbContext, listSqlStr);
        }

    }
}