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
    /// 遗产要素单体——遗产要素单体或局部影像及遗产要素现状记录（602）
    /// </summary>
    public class DockYCYSDT_JBXZTServices:DockBaseService
    {
        public DockYCYSDT_JBXZTServices(string jsonStr, string funId, string heritageId, string className):base(jsonStr, funId, heritageId)
        {
            this.ClassName = className;
        }
        public string ClassName { get; set; }

        public override string ReceiveData()
        {
            var heritageId = this.HeritageId;
            var funId = this.FunId;
            var jsonStr = this.BusinessJsonStr;
            try
            {
               
                var xmlConfig = CommonBusiness.GetMornitorConfig(heritageId);
                var funList = xmlConfig.GetFunctionalModules();
                var ent = JsonHelper.DeserializeJsonToObject<ResultYCYSDT2DockModel>(jsonStr);
                var dbContext = DBHelperPool.Instance.GetDbHelper();
                var funModel = funList.FirstOrDefault(e => e.ID == funId);
                var listSqlStr = new List<string>();
                var listYSJID = new List<string>();
                foreach (var item in ent.DATA)
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
                    var yscid = nameToValue["YCDSJID"] + "";
                    listYSJID.Add(yscid);

                   

                    foreach (var fitem in ent.FILEPATHLIST)
                    {
                        var fnameToValue = fitem.GetNameToValueDic();
                        if (fnameToValue.ContainsKey("YCDSJID") == nameToValue.ContainsKey("YCDSJID"))
                        {
                            FileInfoEx ReceiveFileInfo = CommonBusiness.GetFileNameByFileID(fnameToValue["FileID"] as string);
                            if (!string.IsNullOrEmpty(yscid))//有可能对接过来就是 统计过得数据 例如景点日游客量
                            { }
                        }
                    }
                      
                     
                                ////if (nameToValue.ContainsKey("TPMC"))
                                ////{
                                ////    nameToValue["TPMC"] = ReceiveFileInfo.FILENAME;
                                ////}
                                ////else
                                ////{
                                ////    nameToValue.Add("TPMC", ReceiveFileInfo.FILENAME);
                                ////}
                                //if (nameToValue.ContainsKey("TPLJ"))
                                //{
                                //    nameToValue["TPLJ"] = ReceiveFileInfo.RELATIVEPATH;
                                //}
                                //else
                                //{
                                //    nameToValue.Add("TPLJ", ReceiveFileInfo.RELATIVEPATH);
                                //}
                                //if (nameToValue.ContainsKey("TPLX"))
                                //{
                                //    nameToValue["TPLX"] = ReceiveFileInfo.FILETYPE;
                                //}
                                //else
                                //{
                                //    nameToValue.Add("TPLX", ReceiveFileInfo.FILETYPE);
                                //}
                                listSqlStr.Add(dbContext.insertByParamsReturnSQL(funModel.TableName, nameToValue));
                        //    }
                        //}
                    //}
                    //listSqlStr.Add(dbContext.insertByParamsReturnSQL(funModel.TableName, nameToValue)); //HPF_YSDT_YCYSDTHJBCHTJLB
                }
                if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
                }
                else
                {
                    if (dbContext.executeTransactionSQLList(listSqlStr))
                    { return JsonHelper.SerializeObject(new ResultModel(true, "对接成功")); }
                    else
                    { return JsonHelper.SerializeObject(new ResultModel(false, "对接失败")); }
                }
            }
            catch (Exception ex)
            {
                var strErr = string.Format("遗产地对接数据类【遗产要素单体或局部影像及遗产要素现状记录】数据错误,参数：{0}#{1}#{2},具体错误：{3}", jsonStr, funId, heritageId, ex.Message);
                SystemLogger.getLogger().Error(strErr);
                return JsonHelper.SerializeObject(new ResultModel(false, ex.Message));
            }

        }

    }
}