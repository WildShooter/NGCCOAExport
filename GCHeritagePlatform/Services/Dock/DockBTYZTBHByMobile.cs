using FrameworkCore.DBInterface;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.PublicMornitor;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using GCHeritagePlatform.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.Dock
{
    /// <summary>
    /// 对接照片类病害通用表
    /// </summary>
    public class DockTyZp : DockBaseService
    {
        public DockTyZp(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public override string ReceiveData()
        {
            var funModel = FindFunModel(HeritageId, FunId);
            if (funModel == null) return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            var cListType = MethodHelper.GetTypeListFileEx(GetModelName(funModel.TableName));
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, cListType);
            var entDataList = ent.DATA as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;
            var listSqlStr = new List<string>();
            var listYsjId = new List<string>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据库连接异常！"));
            var receiveAllFileInfo = CommonBusiness.GetFileListByFileID(entPathList.Select(e => e.FILEID));
            if (receiveAllFileInfo.Count != entPathList.Count) return JsonHelper.SerializeObject(new ResultModel(false, "取得的文件与上传文件个数不匹配！"));
            foreach (var item in entDataList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = Guid.NewGuid();
                else
                    nameToValue.Add("ID", Guid.NewGuid());
                var rksj = System.DateTime.Now;
                if (nameToValue.ContainsKey("RKSJ"))
                    nameToValue["RKSJ"] = rksj;
                else
                    nameToValue.Add("RKSJ", rksj);
                var ysjid = nameToValue["YCDSJID"] + "";
                if (!string.IsNullOrEmpty(ysjid))
                    listYsjId.Add(ysjid);
                var fileIDs = entPathList.Where(e => e.YCDSJID == ysjid).Select(e => e.FILEID).ToList();
                var filePathList = receiveAllFileInfo.Where(e => fileIDs.Contains(e.FILEID)).ToList();
                var newBhdcjlId = "";
                var bIsExistBhjcjlId = GetBhdcjlId(dbContext, nameToValue["BHDCJLID"].ToString(), nameToValue["BHBH"].ToString(), out newBhdcjlId);
                if (!bIsExistBhjcjlId)
                    return JsonHelper.SerializeObject(new ResultModel(false, "请先对接病害的工作监测记录！"));
                nameToValue["BHDCJLID"] = newBhdcjlId;

                var file = entPathList.Where(e => e.YCDSJID == ysjid).FirstOrDefault();
                //下面的代码表示，如果没有相应的附件的时候，可以只插入记录
                if (file != null)
                {
                    var filepath = receiveAllFileInfo.FirstOrDefault(e => e.FILEID == file.FILEID);
                    if (filepath != null)
                    {
                        nameToValue["TPLJ"] = filepath.RELATIVEPATH;
                        nameToValue["TPMC"] = filepath.FILENAME;
                        nameToValue["TPGS"] = filepath.FILETYPE;
                    }
                }

                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYsjId, funModel.TableName, dbContext))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据！"));
            }
            return GetExeListSQL(dbContext, listSqlStr);
        }

    }
    /// <summary>
    /// 对接文档类病害通用表
    /// </summary>
    public class DockTyWd : DockBaseService
    {
        public DockTyWd(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public DockTyWd(string jsonStr, string funId, string heritageId, string relatedTableName, string relatedID) : base(jsonStr, funId, heritageId)
        {
            this.ClassName = relatedTableName;
            this.RelatedID = relatedID;
        }
        public string ClassName { get; set; }
        public string RelatedID { get; set; }
        public override string ReceiveData()
        {
            var funModel = FindFunModel(HeritageId, FunId);
            if (funModel == null) return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            var cListType = MethodHelper.GetTypeList(GetModelName(funModel.TableName), ClassName);
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, cListType);
            var entDataList = ent.DATA as IList;
            var entDataDetail = ent.DATADETAIL as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;
            var listSqlStr = new List<string>();
            var listYsjId = new List<string>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据库连接异常！"));
            var receiveAllFileInfo = CommonBusiness.GetFileListByFileID(entPathList.Select(e => e.FILEID));
            if (receiveAllFileInfo.Count != entPathList.Count) return JsonHelper.SerializeObject(new ResultModel(false, "取得的文件与上传文件个数不匹配！"));
            var dicData = new Dictionary<string, Guid>();
            foreach (var item in entDataList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                var guid = Guid.NewGuid();
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = guid;
                else
                    nameToValue.Add("ID", guid);
                var rksj = System.DateTime.Now;
                if (nameToValue.ContainsKey("RKSJ"))
                {
                    if (string.IsNullOrEmpty(nameToValue["RKSJ"] + ""))
                        nameToValue["RKSJ"] = rksj;
                }
                else
                    nameToValue.Add("RKSJ", rksj);
                var ysjid = nameToValue["YCDSJID"] + "";
                if (!string.IsNullOrEmpty(ysjid))
                    listYsjId.Add(ysjid);
                dicData.Add(ysjid, guid);

                var newBhdcjlId = "";
                var bIsExistBhjcjlId = GetBhdcjlId(dbContext, nameToValue["BHDCJLID"].ToString(), nameToValue["BHBH"].ToString(), out newBhdcjlId);
                if (!bIsExistBhjcjlId)
                    return JsonHelper.SerializeObject(new ResultModel(false, "请先对接病害的工作监测记录！"));
                nameToValue["BHDCJLID"] = newBhdcjlId;
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            var listDetailYsjId = new List<string>();
            foreach (var item in entDataDetail)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = Guid.NewGuid();
                else
                    nameToValue.Add("ID", Guid.NewGuid());
                var ysjid = nameToValue["YCDSJID"] + "";
                if (!string.IsNullOrEmpty(ysjid))
                    listDetailYsjId.Add(ysjid);
                nameToValue[RelatedID] = dicData[nameToValue[RelatedID] + ""];
                var fileIDs = entPathList.Where(e => e.YCDSJID == ysjid).Select(e => e.FILEID).ToList();
                var filePathList = receiveAllFileInfo.Where(e => fileIDs.Contains(e.FILEID)).ToList();
                var file = entPathList.Where(e => e.YCDSJID == ysjid).FirstOrDefault();
                //下面的代码表示，如果没有相应的附件的时候，可以只插入记录
                if (file != null)
                {
                    var filepath = receiveAllFileInfo.FirstOrDefault(e => e.FILEID == file.FILEID);
                    if (filepath != null)
                    {
                        nameToValue["TPLJ"] = filepath.RELATIVEPATH;
                        nameToValue["TPMC"] = filepath.FILENAME;
                        nameToValue["TPGS"] = filepath.FILETYPE;
                    }
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(this.ClassName, nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYsjId, funModel.TableName, dbContext))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据！"));
            }
            return GetExeListSQL(dbContext, listSqlStr);
        }
    }
    /// <summary>
    /// 对接病害测项表
    /// </summary>
    public class DockCeXiangTable : DockBaseService
    {
        public DockCeXiangTable(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public override string ReceiveData()
        {
            var funModel = FindFunModel(HeritageId, FunId);
            if (funModel == null)
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            var CListType = MethodHelper.GetTypeList(GetModelName(funModel.TableName));
            var entList = JsonHelper.DeserializeJsonToObject(BusinessJsonStr, CListType) as IList;
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据库连接异常！"));
            var listSqlStr = new List<string>();
            var listYsjId = new List<string>();

            foreach (var item in entList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = Guid.NewGuid();
                else
                    nameToValue.Add("ID", Guid.NewGuid());
                var rksj = System.DateTime.Now;
                if (nameToValue.ContainsKey("RKSJ"))
                    nameToValue["RKSJ"] = rksj;
                else
                    nameToValue.Add("RKSJ", rksj);
                var ysjid = nameToValue["YCDSJID"] + "";
                if (!string.IsNullOrEmpty(ysjid))
                    listYsjId.Add(ysjid);
                string newBhdcjlId;
                var bIsExistBhjcjlId = GetBhdcjlId(dbContext, nameToValue["BHDCJLID"] + "", nameToValue["BHBH"] + "", out newBhdcjlId);
                if (!bIsExistBhjcjlId) return JsonHelper.SerializeObject(new ResultModel(false, "请先对接病害的工作监测记录"));
                nameToValue["BHDCJLID"] = newBhdcjlId;
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYsjId, funModel.TableName, dbContext))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据！"));
            }
            return GetExeListSQL(dbContext, listSqlStr);
        }
    }
    /// <summary>
    /// 对接通用的病害采集记录及通用数据
    /// </summary>
    public class DockTyjlTable : DockBaseService
    {
        public DockTyjlTable(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public override string ReceiveData()
        {
            var funModel = FindFunModel(HeritageId, FunId);
            if (funModel == null) return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            var CListType = MethodHelper.GetTypeList("HPF_BTYZTBH_TYJL", "HPF_BTYZTBH_TYSJ");
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, CListType);
            var entList = ent.DATA as IList;
            var entDetailList = ent.DATADETAIL as IList;
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据库连接异常！"));
            var listSqlStr = new List<string>();
            var listYsjId = new List<string>();
            var listDicOldNew = new Dictionary<string, Guid>();
            foreach (var item in entList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                var guid = Guid.NewGuid();
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = guid;
                else
                    nameToValue.Add("ID", guid);
                var rksj = System.DateTime.Now;
                if (nameToValue.ContainsKey("RKSJ"))
                    nameToValue["RKSJ"] = rksj;
                else
                    nameToValue.Add("RKSJ", rksj);
                var ysjid = nameToValue["YCDSJID"] + "";
                if (!string.IsNullOrEmpty(ysjid))
                    listYsjId.Add(ysjid);
                listDicOldNew.Add(ysjid, guid);
                listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_BTYZTBH_TYJL", nameToValue));
            }
            foreach (var item in entDetailList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = Guid.NewGuid();
                else
                    nameToValue.Add("ID", Guid.NewGuid());
                var rksj = System.DateTime.Now;
                if (nameToValue.ContainsKey("RKSJ"))
                    nameToValue["RKSJ"] = rksj;
                else
                    nameToValue.Add("RKSJ", rksj);
                var bhjlid = nameToValue["BHJLID"] + "";
                nameToValue["BHJLID"] = listDicOldNew[bhjlid];
                listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_BTYZTBH_TYSJ", nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYsjId, "HPF_BTYZTBH_TYJL", dbContext))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据！"));
            }
            return GetExeListSQL(dbContext, listSqlStr);
        }
    }
}