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
    public class DockJGJSService
    {
    }
    public class DockBHGLJG_Service : DockBaseService
    {
        public DockBHGLJG_Service(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public DockStructInitClass GetTableName(string funId)
        {
            var dockJGJSStructInitClass = new DockStructInitClass();
            switch (funId)
            {
                case "20101"://保护管理机构
                    dockJGJSStructInitClass = new DockStructInitClass("HPF_JGJS_BHGLJG", "HPF_JGJS_GLQYT", "BHGLJGID");
                    break;
                default://专项保护管理法规、规章    安防消防-管理制度和应急预案记录
                    dockJGJSStructInitClass = new DockStructInitClass("HPF_JGJS_ZXBHGLFGGZ", "HPF_JGJS_ZXFGXGWD", "FGID");
                    break;
            } 
            return dockJGJSStructInitClass;
        }
        public override string ReceiveData()
        {
            var dockJGJSStructInitClass = GetTableName(FunId);
            var eType = MethodHelper.GetTypeList(dockJGJSStructInitClass.MainTableName, dockJGJSStructInitClass.SubordinateTableName);
            var ent = JsonHelper.DeserializeJsonToDynamicObject(this.BusinessJsonStr, eType);
            var entBHJGDataList = ent.DATA as IList;

            var entGLQYTDataDetailList = ent.DATADETAIL as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;///
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            var dicOldToNew = new Dictionary<string, Guid>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常！"));
            //Data主表
            foreach (var item in entBHJGDataList)
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
            foreach (var item in entGLQYTDataDetailList)
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
                    var file = entPathList.FirstOrDefault(e => e.YCDSJID == ysjid);
                    if (file != null)
                    {
                        var filePath = receiveAllFileInfo.FirstOrDefault(e => e.FILEID == file.FILEID);
                        if (filePath != null)
                        {
                            nameToValue["WDMC"] = filePath.FILENAME;
                            nameToValue["WDLX"] = filePath.FILETYPE;
                            nameToValue["CCLJ"] = filePath.RELATIVEPATH;
                        }
                    }
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(dockJGJSStructInitClass.SubordinateTableName, nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYSJID, dockJGJSStructInitClass.MainTableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已存在对接的数据！"));
            return GetExeListSQL(dbContext, listSqlStr);
        }
    }
}
