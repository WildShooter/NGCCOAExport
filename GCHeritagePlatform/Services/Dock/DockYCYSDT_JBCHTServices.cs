using FrameworkCore.Utils;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using GCHeritagePlatform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCHeritagePlatform.Services.Dock.Model;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 遗产要素单体——遗产要素单体或局部测绘图及遗产要素现状记录（601）
    /// </summary>
    public class DockYCYSDT_JBCHTServices : DockBaseService
    {

        public DockYCYSDT_JBCHTServices(string jsonStr, string funId, string heritageId, string className):base(jsonStr, funId, heritageId)
        {
            this.ClassName = className;
        }
        public string ClassName { get; set; }

        public override string ReceiveData()
        {
                var funModel= FindFunModel(HeritageId, FunId);
                var dbContext = DBHelperPool.Instance.GetDbHelper();
                var ent = JsonHelper.DeserializeJsonToObject<ResultYCYSDT1DockModel>(BusinessJsonStr);
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
                    
                    var yscid = nameToValue["YCDSJID"] + "";
                    if (!string.IsNullOrEmpty(yscid))//有可能对接过来就是 统计过得数据 例如景点日游客量
                    {
                        listYSJID.Add(yscid);
                        foreach (var fitem in ent.FILEPATHLIST)
                        {
                            var fnameToValue = fitem.GetNameToValueDic();
                            if (fnameToValue.ContainsKey("YCDSJID") == nameToValue.ContainsKey("YCDSJID"))
                            {
                                FileInfoEx ReceiveFileInfo = CommonBusiness.GetFileNameByFileID(fnameToValue["FileID"] as string);
                                if (nameToValue.ContainsKey("TZMC"))
                                {
                                    nameToValue["TZMC"] = ReceiveFileInfo.FILENAME;
                                }
                                else
                                {
                                    nameToValue.Add("TZMC", ReceiveFileInfo.FILENAME);
                                }
                                if (nameToValue.ContainsKey("TZLJ"))
                                {
                                    nameToValue["TZLJ"] = ReceiveFileInfo.RELATIVEPATH;
                                }
                                else
                                {
                                    nameToValue.Add("TZLJ", ReceiveFileInfo.RELATIVEPATH);
                                }
                                if (nameToValue.ContainsKey("TZLX"))
                                {
                                    nameToValue["TZLX"] = ReceiveFileInfo.FILETYPE;
                                }
                                else
                                {
                                    nameToValue.Add("TZLX", ReceiveFileInfo.FILETYPE);
                                }
                                listSqlStr.Add(dbContext.insertByParamsReturnSQL(funModel.TableName, nameToValue));
                            }
                        }
                    }
                }
                if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
                }
            return GetExeListSQL(dbContext, listSqlStr);

        }
    }
}