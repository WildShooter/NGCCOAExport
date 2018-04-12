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
    /// 旅游管理 
    /// </summary>
    public class DockLYGLService : DockBaseService
    {
        public DockLYGLService(string jsonStr, string funId, string heritageId,string className):base(jsonStr,funId,heritageId)
        {
            this.ClassName = className;
        }
        public string ClassName { get; set; }

        public List<HPF_LYYYKGL_LYJD> GetHeritageLYJD(IDBHelper dbContext) {
            var strSql = string.Format("select ID from HPF_LYYYKGL_LYJD where GLYCBTID='{0}'  ", this.HeritageId);
            var dt = dbContext.getDataTableResult(strSql);
            return DataTableToEnt<HPF_LYYYKGL_LYJD>.FillModel(dt);
        }
        public override string ReceiveData()
        {


            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //根据传入两个表名来获取对应的数据结构模型的类型
            var eType = MethodHelper.GetTypeList(funModel.TableName,"HPF_LYYYKGL_LYJD");
            //将Json串按指定的类型进行反序列化为dynamic实体对象
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, eType);


            var entJDMXList = ent.DATA as IList;
            var entJDLList = ent.DATADETAIL as IList;//var entList = JsonHelper.DeserializeJsonToObject<List<HPF_ZRHJ_TFLJXX>>(jsonStr) ;
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var listSqlStr = new List<string>();
            var entJDDic =new Dictionary<string, string>();
            var entExistJD = this.GetHeritageLYJD(dbContext);
            var listYSJID = new List<string>();
            foreach (var item in entJDLList)
            {
                var nameToValue = item.GetNameToValueDic();
                var ycdsjid = nameToValue["YCDSJID"]+"";
                var entJD = entExistJD.FirstOrDefault(e => e.YCDSJID == ycdsjid);
                if (entJD != null) continue;
               
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = HeritageId;
                }
                var oldJDId = nameToValue["YCDSJID"];
                var id = Guid.NewGuid().ToString(); 
                nameToValue["ID"] = id;
                entJDDic.Add(ycdsjid, id);
                //如果有改变PID的属性信息
                listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_LYYYKGL_LYJD", nameToValue));
            }

            foreach (var item in entJDMXList)
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
                if (!string.IsNullOrEmpty(yscid))//有可能对接过来就是 统计过得数据 例如景点日游客量
                {
                    listYSJID.Add(yscid);//防止重复对接
                }

                var lyjdid = nameToValue["LYJDID"] + "";
                if (entJDDic.ContainsKey(lyjdid))
                {
                    nameToValue["LYJDID"] = entJDDic[lyjdid];
                }
                else
                {
                    var entJD = entExistJD.FirstOrDefault(e => e.YCDSJID == lyjdid&&e.GLYCBTID== HeritageId);
                    if (entJD == null)
                    {
                        return JsonHelper.SerializeObject(new ResultModel(false, "对接基础数据景点信息错误!"));
                    }
                    nameToValue["LYJDID"] = entJD.ID;
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYSJID, ClassName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            return GetExeListSQL(dbContext, listSqlStr);

        }
     
    }
}