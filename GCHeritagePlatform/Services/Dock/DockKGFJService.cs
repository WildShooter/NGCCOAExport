using FrameworkCore.DBInterface;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using GCHeritagePlatform.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 考古发掘记录
    /// </summary>
    public class DockKGFJService : DockBaseService
    {
        public DockKGFJService(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public override string ReceiveData()
        {
            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null) return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息！"));
            var cListType = MethodHelper.GetTypeList(GetModelName(funModel.TableName), "HPF_KGFJ_KGFJJL_XGWD");
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, cListType);
            var entData = ent.DATA as IList;
            var entDataDetail = ent.DATADETAIL as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;
            //if (entData.Count != entPathList.Count)
            //    return JsonHelper.SerializeObject(new ResultModel(false, "文件个数与记录条数不符！"));//这个地方不能判断，因为无已发表的简报的情况就附件为空。
            var listSql = new List<string>();
            var listYsjId = new List<string>();
            var kgfjjlid=new Dictionary<string,string>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据库连接异常！"));

            var receiveAllFileInfo = entPathList==null? null : CommonBusiness.GetFileListByFileID(entPathList.Select(e => e.FILEID));
            foreach (var item in entData)
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                    nameToValue["GLYCBTID"] = HeritageId;
                if (nameToValue.ContainsKey("ID"))
                    nameToValue["ID"] = Guid.NewGuid();
                else
                    nameToValue.Add("ID", Guid.NewGuid());
                var ysjid = nameToValue["YCDSJID"] + "";
                kgfjjlid.Add(ysjid,nameToValue["ID"]+"");
                listYsjId.Add(ysjid);
                listSql.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            if (entDataDetail != null)
            {
                foreach (var item in entDataDetail)
                {
                    var nameToValue = item.GetNameToValueDic();
                    if (nameToValue.ContainsKey("GLYCBTID"))
                        nameToValue["GLYCBTID"] = HeritageId;
                    if (nameToValue.ContainsKey("ID"))
                        nameToValue["ID"] = Guid.NewGuid();
                    else
                        nameToValue.Add("ID", Guid.NewGuid());
                    if (nameToValue.ContainsKey("FJJLID"))
                        nameToValue["FJJLID"] = kgfjjlid["FJJLID"];
                    else
                        nameToValue.Add("FJJLID", kgfjjlid["FJJLID"]);
                    var ysjid = nameToValue["YCDSJID"] + "";
                    if (string.IsNullOrEmpty(ysjid))
                        return JsonHelper.SerializeObject(new ResultModel(false, "YCDSJID不能为空！"));
                    var fileIds = entPathList.Where(e => e.YCDSJID == ysjid).Select(e => e.FILEID).ToList();
                    var filePathList = receiveAllFileInfo.Where(e => fileIds.Contains(e.FILEID)).ToList();
                    var file = entPathList.FirstOrDefault(e => e.YCDSJID == ysjid);
                    if (file != null)
                    {
                        var filePath = receiveAllFileInfo.FirstOrDefault(e => e.FILEID == file.FILEID);
                        if (filePath!=null)
                        {
                            nameToValue["WDMC"] = filePath.FILENAME;
                            nameToValue["WDLX"] = filePath.FILETYPE;
                            nameToValue["LJ"] = filePath.RELATIVEPATH;
                        }
                    }
                    listSql.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
                }
            }
            if (!CheckIsDock(listSql, listYsjId, funModel.TableName, dbContext))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            }
            return GetExeListSQL(dbContext, listSql);
        }
    }
    /// <summary>
    /// 考古报告以及考古发掘现场照片（文件类型）
    /// </summary>
    public class DockKGFJ_KGFileServices : DockBaseService
    {
        public DockKGFJ_KGFileServices(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId)
        {
            // this.ClassName = className;
        }
        public string ClassName { get; set; }

        public override string ReceiveData()
        {
            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象
            var cListType = MethodHelper.GetTypeListFileEx(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, cListType);//遗产地发过来的字符串（json格式）的项与我们在model中建的功能类的属性是一一对应的，这里进行赋值
            var entData = ent.DATA as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;
            //判断文件、照片记录的条数与实际文件的个数是否相符
            //if (FunId=="1403"&&entData.Count != entPathList.Count) return JsonHelper.SerializeObject(new ResultModel(false, "文件个数与记录条数不符！"));
            if (FunId == "1403" && (ent.FILEPATHLIST == null || ent.FILEPATHLIST.Count == 0))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "缺少对接文档信息！"));
            }
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var receiveAllFileInfo = entPathList==null? null : CommonBusiness.GetFileListByFileID(entPathList.Select(e => e.FILEID));
            //判断取到的文件数量和上传到的文件数量是否一样？
            if (FunId == "1403" && (receiveAllFileInfo.Count != entPathList.Count)) return JsonHelper.SerializeObject(new ResultModel(false, "取得的文件与上传文件个数不匹配！"));
            foreach (var item in entData)
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
                var ysjid = nameToValue["YCDSJID"] + "";
                if (string.IsNullOrEmpty(ysjid)) return JsonHelper.SerializeObject(new ResultModel(false, "YCDSJID不能为空！"));
                if (entPathList != null)
                {
                    var fileIDs = entPathList.Where(e => e.YCDSJID == ysjid).Select(e => e.FILEID).ToList();

                    var filePathList = receiveAllFileInfo.Where(e => fileIDs.Contains(e.FILEID)).ToList();

                    switch (FunId)
                    {
                        case "1401"://考古报告信息
                            {
                                var file = entPathList.Where(e => e.YCDSJID == ysjid).FirstOrDefault();
                                if (file != null)
                                {
                                    var filePath = receiveAllFileInfo.FirstOrDefault(e => e.FILEID == file.FILEID);
                                    if (filePath != null)
                                    {
                                        nameToValue["XGWD"] = filePath.RELATIVEPATH;
                                    }
                                    //var kgxmjlid = nameToValue["KGXMID"] + "";
                                    //if (!string.IsNullOrEmpty(kgxmjlid))
                                    //{
                                    //    var dtForeignKeyTable = this.GetKGFJGZJZID(dbContext, kgxmjlid, "HPF_KGFJ_KGXM");
                                    //    var entKGBGXXJL = DataTableToEnt<HPF_KGFJ_KGXM>.FillModel(dtForeignKeyTable);
                                    //    //取出项目中的YCDSJID=外键ID的记录
                                    //    var entKGXMJL = entKGBGXXJL.FirstOrDefault(e => e.YCDSJID == kgxmjlid);
                                    //    //判断如果存在外键关联，则把外键进行赋值，目前由于鼓浪屿不存在关联，所以不进行关联。
                                    //    nameToValue["KGXMID"] = entKGXMJL.ID;
                                    //}

                                }
                            }
                            break;
                        case "1403"://考古发掘照片
                            {
                                //将原来的照片中的现场照片中的关联外键进展记录取出来
                                var kgfjgzjzid = nameToValue["KGFJJLID"] + "";
                                //通过外键进展记录在进展表中查询总平台的记录
                                var dtForeignKeyTable = this.GetKGFJGZJZID(dbContext, kgfjgzjzid, "HPF_KGFJ_KGFJJL");
                                var entKGFJJZJL = DataTableToEnt<HPF_KGFJ_KGFJJL>.FillModel(dtForeignKeyTable);
                                foreach (var fileinfo in filePathList)
                                {
                                    nameToValue["TPLJ"] = fileinfo.RELATIVEPATH;
                                    nameToValue["TPMC"] = fileinfo.FILENAME;
                                    nameToValue["TPGS"] = fileinfo.FILETYPE;
                                    //取出进展中的YCDSJID=外键ID的记录
                                    var entKGFJGZJZ = entKGFJJZJL.FirstOrDefault(e => e.YCDSJID == kgfjgzjzid);
                                    if (entKGFJGZJZ == null) return JsonHelper.SerializeObject(new ResultModel(false, "未找到考古发掘工作进展记录！"));
                                    nameToValue["KGFJJLID"] = entKGFJGZJZ.ID;
                                }
                            }
                            break;
                    }
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            }
            return GetExeListSQL(dbContext, listSqlStr);

        }
        public DataTable GetKGFJGZJZID(IDBHelper dbContext, string kgfjgzjzid, string tablename)
        //public List<HPF_KGFJ_KGXM_KGFJGZ_JZJL> GetKGFJGZJZID(IDBHelper dbContext, string kgfjgzjzid)
        {
            var strSql = string.Format("select * from {2} where GLYCBTID='{0}' and YCDSJID = '{1}' ", this.HeritageId, kgfjgzjzid, tablename);
            var dt = dbContext.getDataTableResult(strSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            //return GCHeritagePlatform.Utils.DataTableToEnt<HPF_KGFJ_KGXM_KGFJGZ_JZJL>.FillModel(dt);
            return dt;
        }
    }
}