using FrameworkCore.DBInterface;
using FrameworkCore.Utils;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.BaseInfo.Interface;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using GCHeritagePlatform.Utils;
using Nancy.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCHeritagePlatform.Services.Dock.Model;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 保护工程（保护展示与环境整治+项目范围图）
    /// </summary>
    public class DockProtectDERPRService : DockBaseService
    {
        public DockProtectDERPRService(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId)
        {
        }
        public override string ReceiveData()
        {
            var ent = JsonHelper.DeserializeJsonToObject<ResultBHGCDockModel>(BusinessJsonStr);
            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));

            var dicMainOld2New = new Dictionary<string, Guid>();
            foreach (var item in ent.DATA)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = HeritageId;
                }
                var guid = Guid.NewGuid();
                if (nameToValue.ContainsKey("ID"))
                {
                    nameToValue["ID"] = guid;
                }
                else
                {
                    nameToValue.Add("ID", guid);
                }
                var yscid = nameToValue["YCDSJID"] + "";
                listYSJID.Add(yscid);
                dicMainOld2New.Add(yscid, guid);
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            var filePathList = ent.FILEPATHLIST;
            if (ent.DATADETAIL != null)
            {
                foreach (var item in ent.DATADETAIL)
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
                    //如果有改变PID的属性信息
                    nameToValue["GCXMID"] = dicMainOld2New[nameToValue["GCXMID"] + ""];
                    if (filePathList != null && filePathList.Count > 0)
                    {//如果有项目范围图的，具有图纸的文件，则对接图纸，如果使用的超擎的矢量图，则不对接。
                        var fileInfoList = CommonBusiness.GetFileListByFileID(filePathList.Select(e => e.FILEID));
                        foreach (var DtailSon in ent.FILEPATHLIST)
                        {
                            var ycdsjid = nameToValue["YCDSJID"] + "";
                            var fileId = filePathList.FirstOrDefault(e => e.YCDSJID == ycdsjid);
                            FileInfoEx ReceiveFileInfo = fileInfoList.FirstOrDefault(e => e.FILEID == fileId.FILEID);
                            nameToValue["TZMC"] = ReceiveFileInfo.FILENAME;
                            nameToValue["LJ"] = ReceiveFileInfo.RELATIVEPATH;
                            nameToValue["TZGS"] = ReceiveFileInfo.FILETYPE;
                        }
                    }
                    listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_BHGC_BHZSHHJZZGC_XMFWT", nameToValue));
                }
            }

            //if (filePathList != null && filePathList.Count > 0)
            //{//文档上传失败 不对接文档
            //    var fileInfoList = CommonBusiness.GetFileListByFileID(filePathList.Select(e => e.FILEID));
            //    foreach (var item in ent.DataDetailSon)
            //    {
            //        var nameToValue = item.GetNameToValueDic();
            //        if (nameToValue.ContainsKey("GLYCBTID"))
            //        {
            //            nameToValue["GLYCBTID"] = this.HeritageId;
            //        }
            //        if (nameToValue.ContainsKey("ID"))
            //        {
            //            nameToValue["ID"] = Guid.NewGuid();
            //        }
            //        else
            //        {
            //            nameToValue.Add("ID", Guid.NewGuid());
            //        }
            //        nameToValue["GCXMID"] = dicMainOld2New[nameToValue["GCXMID"] + ""];
            //        var ycdsjid = nameToValue["YCDSJID"] + "";
            //        var fileID = filePathList.FirstOrDefault(e => e.YCDSJID == ycdsjid);
            //        FileInfoEx ReceiveFileInfo = fileInfoList.FirstOrDefault(e => e.FILEID == fileID.FILEID);
            //        if (nameToValue.ContainsKey("TPMC"))
            //        {
            //            nameToValue["TPMC"] = ReceiveFileInfo.FILENAME;
            //        }
            //        else
            //        {
            //            nameToValue.Add("TPMC", ReceiveFileInfo.FILENAME);
            //        }
            //        if (nameToValue.ContainsKey("TPLJ"))
            //        {
            //            nameToValue["TPLJ"] = ReceiveFileInfo.RELATIVEPATH;
            //        }
            //        else
            //        {
            //            nameToValue.Add("TPLJ", ReceiveFileInfo.RELATIVEPATH);
            //        }
            //        if (nameToValue.ContainsKey("TPLX"))
            //        {
            //            nameToValue["TPLX"] = ReceiveFileInfo.FILETYPE;
            //        }
            //        else
            //        {
            //            nameToValue.Add("TPLX", ReceiveFileInfo.FILETYPE);
            //        }
            //        listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_BHGC_XGWD", nameToValue));
            //    }
            //}
            if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            return GetExeListSQL(dbContext, listSqlStr);
        }
        //{"Data":[{"ds":"jkg","dg":"fhs"}],"Detail":[{"hj":"kjg","dg":"kgj"}],"Detail1":[{"jd":"hgdj","jkhjg":"irig"}]}




        //public string ProtectXCZP(string jsonStr, string funId, string heritageId)
        //{
        //    var xmlConfig = CommonBusiness.GetMornitorConfig(heritageId);
        //    var funList = xmlConfig.GetFunctionalModules();
        //    var entList = JsonHelper.DeserializeJsonToObject<List<HPF_BHGC_BHZSHHJZZGC_XCZP>>(jsonStr);
        //    var dbContext = DBHelperPool.Instance.GetDbHelper();
        //    var funModel = funList.FirstOrDefault(e => e.ID == funId);
        //    var listSqlStr = new List<string>();
        //    var listYSJID = new List<string>();
        //    foreach (var item in entList)
        //    {
        //        var nameToValue = item.GetNameToValueDic();
        //        if (nameToValue.ContainsKey("GLYCBTID"))
        //        {
        //            nameToValue["GLYCBTID"] = heritageId;
        //        }
        //        if (nameToValue.ContainsKey("ID"))
        //        {
        //            nameToValue["ID"] = Guid.NewGuid();
        //        }
        //        else
        //        {
        //            nameToValue.Add("ID", Guid.NewGuid());
        //        }
        //        //保护工程与现场照片对接数据有关联 
        //        var strSql = string.Format("select ID from HPF_BHGC where YCDSJID='{0}' and GLYCBTID='{1}' ", nameToValue["YCDSJID"], heritageId);
        //        var dtMain = dbContext.getDataTableResult(strSql);
        //        if (dtMain == null || dtMain.Rows.Count == 0)
        //        {
        //            return JsonHelper.SerializeObject(new ResultModel(false, "请先对接保护工程信息！"));
        //        }
        //        else
        //        {
        //            //如果有改变PID的属性信息
        //            nameToValue["PID"] = dtMain.Rows[0][0].ToString();
        //            listSqlStr.Add(dbContext.insertByParamsReturnSQL(funModel.TableName, nameToValue));
        //        }
        //    }
        //    try
        //    {
        //        if (!CheckIsDock(null, funModel.TableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
        //        dbContext.executeTransactionSQLList(listSqlStr);
        //        return JsonHelper.SerializeObject(new ResultModel(true, "对接成功"));
        //    }
        //    catch (Exception ex)
        //    {
        //        var strErr = string.Format("遗产地对接数据类数据【保护工程 保护展示和环境整治工程_现场照片】错误,参数：{0}#{1}#{2},具体错误：{3}", jsonStr, funId, heritageId, ex.Message);
        //        SystemLogger.getLogger().Error(strErr);
        //        return JsonHelper.SerializeObject(new ResultModel(false, ex.Message));
        //    }
        //}



        ////检验数据是否对接过
        //private bool CheckIsDock(IList<string> listStr, string tableName, IDBHelper idbHelper)
        //{
        //    if (listStr==null || listStr.Count == 0) return true;
        //    var ids = listStr.ChangeListToString();
        //    var sql = string.Format("select * from {0} where YCDSJID in ({1})", tableName, ids);
        //    var dt = idbHelper.getDataTableResult(sql);
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}


    }
}