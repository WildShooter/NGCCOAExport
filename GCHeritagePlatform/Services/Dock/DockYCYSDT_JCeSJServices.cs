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
    /// 遗产要素单体——遗产要素单体或局部测绘图及遗产要素现状记录（601）
    /// </summary>
    public class DockYCYSDT_JCeSJServices : DockBaseService
    {
        
        public DockYCYSDT_JCeSJServices(string jsonStr, string funId, string heritageId, string className) : base(jsonStr, funId, heritageId)
        {
            this.ClassName = className;
        }
        public string ClassName { get; set; }
        /// <summary>
        /// 获取自定义结构的方法
        /// </summary>
        /// <param name="funId">业务功能ID</param>
        /// <returns>返回自定义结构的对象</returns>
        public virtual DockStructInitClass GetTableName(string funId)
        {
            var dockYcysdtjcesj=new DockStructInitClass();
            switch (funId)
            {
                case "601":
                    dockYcysdtjcesj = new DockStructInitClass("HPF_YCJCXX_YCYSDTHJBCHJZT", "HPF_YCYSDT_YCYSDTHJBXZT","YCYSDTHJBCHJZTID");
                    break;
                case "602":
                    dockYcysdtjcesj=new DockStructInitClass("HPF_YCJCXX_YCYSDTHJBTP", "HPF_YCYSDT_YCYSDTHJBTP", "YCYSDTHJBTPID");
                    break;
            }
            return dockYcysdtjcesj;
        }
        public bool GetYcysdtJcsjidByDataId(IDBHelper dbHelper, string tableName,string ycdsjid, out string ycysdtJcsjid)
        {
            ycysdtJcsjid = "";
            var sql = string.Format(" select * from {0} where GLYCBTID='{1}' and YCDSJID='{2}'", tableName, HeritageId, ycdsjid);
            var dt = dbHelper.getDataTableResult(sql);
            if (dt == null || dt.Rows.Count == 0) return false;
            ycysdtJcsjid = dt.Rows[0]["ID"].ToString();
            return true;
        }
        public override string ReceiveData()
        {
            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象
            var cListType = MethodHelper.GetTypeListFileSignEx(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, cListType);//遗产地发过来的字符串（json格式）的项与我们在model中建的功能类的属性是一一对应的,这里进行赋值
            var entData = ent.DATA as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoSignEx>;
            if (ent.FILEPATHLIST == null || ent.FILEPATHLIST.Count == 0)
            {
                //SystemLogger.getLogger().Error(string.Format("缺少对接文档信息！,遗产地数据Json为：{0},funId为：{1},遗产地ID为{2}", BusinessJsonStr, FunId, HeritageId));
                //return JsonHelper.SerializeObject(new ResultModel(false, "缺少对接文档信息！"));
            }
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var dockYcysdtjcesj = GetTableName(FunId);

            var receiveAllFileInfo = CommonBusiness.GetFileListByFileID(entPathList.Select(e => e.FILEID));
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
                var yscid = nameToValue["YCDSJID"] + "";
                if (string.IsNullOrEmpty(yscid)) return JsonHelper.SerializeObject(new ResultModel(false, "YCDSJID不能为空！"));
                //根据外键查找遗产基础数据中的对应的遗产基础数据ID
                //string ycjcsjid;
                //var isExistYcjcsjid = GetYcysdtJcsjidByDataId(dbContext, dockYcysdtjcesj.MainTableName,nameToValue[dockYcysdtjcesj.RelatedID].ToString(),
                //    out ycjcsjid);
                //if (!isExistYcjcsjid)
                //    return JsonHelper.SerializeObject(new ResultModel(false, "请先对接遗产要素单体基础数据！"));
                var fileIDs = entPathList.Where(e => e.YCDSJID == yscid).Select(e => e.FILEID).ToList();
                var filePathList = fileIDs.Count==0?null: receiveAllFileInfo.Where(e => fileIDs.Contains(e.FILEID)).ToList();
                switch (FunId)
                {
                    case "601":
                        {
                            if (filePathList != null)
                            {
                                var tz = entPathList.FirstOrDefault(e => e.SIGN == "TZLJ"&&e.YCDSJID==nameToValue["YCDSJID"].ToString());
                                if (tz != null)
                                {
                                    var tzEnt = filePathList.FirstOrDefault(e => e.FILEID == tz.FILEID);//写的绝对 接口文档 要求明确指出
                                    nameToValue["TZLJ"] = tzEnt == null ? "" : tzEnt.RELATIVEPATH;
                                }
                                var YLT = entPathList.FirstOrDefault(e => e.SIGN == "YLT" && e.YCDSJID==nameToValue["YCDSJID"]+"");
                                if (YLT != null)
                                {
                                    var yltEnt = filePathList.FirstOrDefault(e => e.FILEID == YLT.FILEID);//写的绝对 接口文档 要求明确指出
                                    nameToValue["YLTLJ"] = yltEnt == null ? "" : yltEnt.RELATIVEPATH;
                                }
                            }
                        }
                        break;
                    case "602":
                        {
                            if (filePathList != null)
                            {
                                var tp = filePathList.FirstOrDefault();
                                nameToValue["TPLJ"] = tp == null ? "" : tp.RELATIVEPATH;
                                //var ylt = filePathList.FirstOrDefault(e => e.FILETYPE == "jpg");//单体图片没有预览图（鼓浪屿说的）
                                //nameToValue["YLTLJ"] = ylt == null ? "" : ylt.RELATIVEPATH;
                            }
                        }
                        break;
                }
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext))
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            }
            return GetExeListSQL(dbContext, listSqlStr);

        }
    }
}