using FrameworkCore.Utils;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using GCHeritagePlatform.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCHeritagePlatform.Services.Dock.Model;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 保护展示与环境整治工程服务类-现场照片
    /// </summary>
    public class DockBHGC_XCZPServices: DockBaseService
    {
        public DockBHGC_XCZPServices(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId)
        {
        }

        public override string ReceiveData()
        {
            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象
            var cListType = MethodHelper.GetTypeListFileEx(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var ent = JsonHelper.DeserializeJsonToObject<ResultBHGC_XCZPDockModel>(BusinessJsonStr);
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            foreach (var item in ent.DATA)
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
                
                //保护工程和对接数据有关联 
                var strSql = string.Format("select ID from HPF_BHGC where YCDSJID='{0}' and GLYCBTID='{1}' ", nameToValue["GCXMID"], HeritageId);
                var dtMain = dbContext.getDataTableResult(strSql);
                if (dtMain == null || dtMain.Rows.Count == 0)
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "请先对接【保护展示与环境整治工程记录数据】！"));
                }
                //如果有改变PID的属性信息
                nameToValue["GCXMID"] = dtMain.Rows[0][0].ToString();
                var yscid = nameToValue["YCDSJID"] + "";
                listYSJID.Add(yscid);

                foreach (var fitem in ent.FILEPATHLIST)
                {
                    var fnameToValue = fitem.GetNameToValueDic();
                    if (fnameToValue.ContainsKey("YCDSJID") ==nameToValue.ContainsKey("YCDSJID"))
                    {
                        FileInfoEx ReceiveFileInfo = CommonBusiness.GetFileNameByFileID(fnameToValue["FILEID"]as string);
                        switch (FunId)
                        {
                            case "150103"://工程方案的文档
                                {
                                    if (nameToValue.ContainsKey("WDMC"))
                                    {
                                        nameToValue["WDMC"] = ReceiveFileInfo.FILENAME;
                                    }
                                    else
                                    {
                                        nameToValue.Add("WDMC", ReceiveFileInfo.FILENAME);
                                    }
                                    if (nameToValue.ContainsKey("LJ"))
                                    {
                                        nameToValue["LJ"] = ReceiveFileInfo.RELATIVEPATH;
                                    }
                                    else
                                    {
                                        nameToValue.Add("LJ", ReceiveFileInfo.RELATIVEPATH);
                                    }
                                    if (nameToValue.ContainsKey("WDLX"))
                                    {
                                        nameToValue["WDLX"] = ReceiveFileInfo.FILETYPE;
                                    }
                                    else
                                    {
                                        nameToValue.Add("WDLX", ReceiveFileInfo.FILETYPE);
                                    }
                                }
                                break;
                            case "1503"://保护展示的现场照片
                                {
                                    if (nameToValue.ContainsKey("TPMC"))
                                    {
                                        nameToValue["TPMC"] = ReceiveFileInfo.FILENAME;
                                    }
                                    else
                                    {
                                        nameToValue.Add("TPMC", ReceiveFileInfo.FILENAME);
                                    }
                                    if (nameToValue.ContainsKey("TPLJ"))
                                    {
                                        nameToValue["TPLJ"] = ReceiveFileInfo.RELATIVEPATH;
                                    }
                                    else
                                    {
                                        nameToValue.Add("TPLJ", ReceiveFileInfo.RELATIVEPATH);
                                    }
                                    if (nameToValue.ContainsKey("TPGS"))
                                    {
                                        nameToValue["TPGS"] = ReceiveFileInfo.FILETYPE;
                                    }
                                    else
                                    {
                                        nameToValue.Add("TPGS", ReceiveFileInfo.FILETYPE);
                                    }
                                }
                                break;
                        }
                        
                        listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
                    }
                }
                
            }

            if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            return GetExeListSQL(dbContext, listSqlStr);
        }

    }
}