using FrameworkCore.DBInterface;
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
    /// 保护管理规划编制记录
    /// </summary>
    public class DockBHGLGHService : DockBaseService
    {
        public DockBHGLGHService(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public DockStructInitClass GetTableName(string funId)
        {
            var dockJGJSStructInitClass = new DockStructInitClass("HPF_BHGH_BHGLGH", "HPF_BHGH_BHGLGH_XGWD", "BHGLGHID");
            return dockJGJSStructInitClass;
        }
        public override string ReceiveData()
        {
            var dockJGJSStructInitClass = GetTableName(FunId);
            var eType = MethodHelper.GetTypeList(dockJGJSStructInitClass.MainTableName, dockJGJSStructInitClass.SubordinateTableName);
            var ent = JsonHelper.DeserializeJsonToDynamicObject(this.BusinessJsonStr, eType);
            var entBHGLGHDataList = ent.DATA as IList;

            var entBgglghxgwdDataDetailList = ent.DATADETAIL as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;///
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            var dicOldToNew = new Dictionary<string, Guid>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常！"));
            //Data主表
            foreach (var item in entBHGLGHDataList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                var ysjid = nameToValue["YCDSJID"] + "";
                var guid = Guid.NewGuid();
                dicOldToNew.Add(ysjid, guid);
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = guid;
                else
                    nameToValue.Add("ID", guid);
                if (!string.IsNullOrEmpty(ysjid))
                    listYSJID.Add(ysjid);
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(dockJGJSStructInitClass.MainTableName, nameToValue));
            }
            var receiveAllFileInfo = entPathList == null ? null : CommonBusiness.GetFileListByFileID(entPathList.Select(e => e.FILEID));
            //明细
            foreach (var item in entBgglghxgwdDataDetailList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                var ysjid = nameToValue["YCDSJID"] + "";
                var guid = Guid.NewGuid();
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = guid;
                else
                    nameToValue.Add("ID", guid);
                nameToValue[dockJGJSStructInitClass.RelatedID] = dicOldToNew[ysjid];
                //附件
                if (entPathList != null && entPathList.Count > 0)
                {
                    var file = entPathList.Where(e => e.YCDSJID == ysjid).FirstOrDefault();
                    if (file != null)
                    {
                        var filePath = receiveAllFileInfo.FirstOrDefault(e => e.FILEID == file.FILEID);
                        if (filePath != null)
                        {
                            nameToValue["WDMC"] = filePath.FILENAME;
                            nameToValue["WDLX"] = filePath.FILETYPE;
                            nameToValue["LJ"] = filePath.RELATIVEPATH;
                        }
                    }
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(dockJGJSStructInitClass.SubordinateTableName, nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYSJID, dockJGJSStructInitClass.MainTableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已存在对接的数据！"));
            return GetExeListSQL(dbContext, listSqlStr);
        }
    }
    /// <summary>
    /// 现行规划执行情况记录
    /// </summary>
    public class DockXXGHZXQKJL:DockBaseService
    {
        public bool GetBHGLGHIDByOldDataID(IDBHelper dbHelper, string bhglghid, out string newBhglghid)
        {
            newBhglghid = "";
            var sql = string.Format(" select * from HPF_BHGH_BHGLGH where GLYCBTID='{1}' and YCDSJID='{0}'", bhglghid, HeritageId);
            var dt = dbHelper.getDataTableResult(sql);
            if (dt == null || dt.Rows.Count == 0) return false;
            newBhglghid = dt.Rows[0]["ID"].ToString();
            return true;
        }
        public DockXXGHZXQKJL(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public override string ReceiveData()
        {
            var dockBHGLGHStructInitClass = new DockStructInitClass("HPF_BHGH_BHGLGH_XXZXQKJL", "HPF_BHGH_XXGHZXQKZHPJ", "BHGLGHID");
            //根据传入两个表名来获取对应的数据结构模型的类型
            var eType = MethodHelper.GetTypeList(dockBHGLGHStructInitClass.MainTableName, dockBHGLGHStructInitClass.SubordinateTableName);
            //将Json串按指定的类型进行反序列化为dynamic实体对象
            var ent = JsonHelper.DeserializeJsonToDynamicObject(this.BusinessJsonStr, eType);
            var entZXQKJLList = ent.DATA as IList;
            var entZHPJList = ent.DATADETAIL as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;///
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            var dicOldToNew = new Dictionary<string, Guid>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            //主表
            foreach (var item in entZXQKJLList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                }
                if (nameToValue.ContainsKey("BHGLGHID"))
                {
                    string newBhglghid;
                    var bIsExistBHBH = GetBHGLGHIDByOldDataID(dbContext, nameToValue["BHGLGHID"] + "", out newBhglghid);
                    if (!bIsExistBHBH)
                    {
                        return JsonHelper.SerializeObject(new ResultModel(false, "请先对接保护管理规划！"));
                    }
                    nameToValue["BHGLGHID"] = newBhglghid;
                }
                var ysjid = nameToValue["YCDSJID"] + "";//元数据ID
                var guid = Guid.NewGuid();
                dicOldToNew.Add(ysjid, guid);
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = HeritageId;
                }
                if (nameToValue.ContainsKey("ID"))
                {//把ID字段赋一个guid
                    nameToValue["ID"] = guid;
                }
                else
                {//没有的增加
                    nameToValue.Add("ID", guid);
                }

                if (!string.IsNullOrEmpty(ysjid))//有可能对接过来就是 统计过得数据 例如景点日游客量
                {
                    listYSJID.Add(ysjid);//防止重复对接
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(dockBHGLGHStructInitClass.MainTableName, nameToValue));
            }
            //明细
            //var dicFileRelatedID = new Dictionary<string, Guid>();
            foreach (var item in entZHPJList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                }
                var ysjid = nameToValue["YCDSJID"] + "";
                var guid = Guid.NewGuid();
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = HeritageId;
                }
                if (nameToValue.ContainsKey("ID"))
                {
                    nameToValue["ID"] = guid;
                }
                else
                {
                    nameToValue.Add("ID", guid);
                }
                //将裂缝的ID值赋给裂缝记录表中的裂缝ID字段
                //nameToValue[dockBHGLGHStructInitClass.RelatedID] = dicOldToNew[ysjid];
                //dicFileRelatedID.Add(ysjid, guid);
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(dockBHGLGHStructInitClass.SubordinateTableName, nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYSJID, dockBHGLGHStructInitClass.MainTableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            return GetExeListSQL(dbContext, listSqlStr);
        }
        
    }
}
