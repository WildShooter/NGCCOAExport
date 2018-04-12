using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FrameworkCore.DBInterface;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.PublicMornitor;
using GCHeritagePlatform.Services.PublicMornitor.Model;
using GCHeritagePlatform.Utils;

namespace GCHeritagePlatform.Services.Dock
{
    /// <summary>
    ///申遗承诺模型C
    /// </summary>
    /// <typeparam name="T1">主表Data的类型</typeparam>
    /// <typeparam name="T2">次表DataDetail的类型</typeparam>
    public class DockResultSYCNodel<T1, T2, T3> where T1 : class where T2 : class where T3 : class
    {
        public List<T1> DATA { set; get; }
        /// <summary>
        /// 返回结果数据的关联子表
        /// </summary>
        public List<T2> DATADETAIL { set; get; }
        public List<T3> DATADETAILSON { set; get; }
        public List<FileInfoEx> FILEPATHLIST { set; get; }
    }
    /// <summary>
    /// 定义一个本体与载体病害初始化的类
    /// </summary>
    public class DockSYCNStructInitClass
    {
        /// <summary>
        /// 主表名,承诺事项
        /// </summary>
        public string MainTableName { get; set; }
        /// <summary>
        /// 子表名,承诺事项进展
        /// </summary>
        public string SubordinateTableName { get; set; }
        /// <summary>
        /// 指定的承诺事项的ID,对应承诺事项进展中的承诺事项ID
        /// </summary>
        public string RelatedID { get; set; }
        /// <summary>
        /// 文档表名
        /// </summary>
        public string WDTableName { get; set; }
        /// <summary>
        /// 指定的承诺事项进展的ID,对应承诺事项进展_相关文档中的承诺事项进展ID
        /// </summary>
        public string RelatedWDID { get; set; }

        public DockSYCNStructInitClass(string maintablename, string subordinatetablename, string relatedid, string wdtablename, string relatedwdid)
        {
            this.MainTableName = maintablename;
            this.SubordinateTableName = subordinatetablename;
            this.RelatedID = relatedid;
            this.WDTableName = wdtablename;
            this.RelatedWDID = relatedwdid;
        }
    }

    //业务说明 每一个承诺事项 可以能执行很久,所以在对接过程中,承诺事项有可能存在,进展持续更新
    public class DockSYCNServices : DockBaseService
    {
        public DockSYCNServices(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId)
        {

        }

        public DockSYCNStructInitClass GetTableName(string funId)
        {
            var dockBTYZTBHStructInitClass = new DockSYCNStructInitClass("HPF_SYCN_CNSX", "HPF_SYCN_CNSXJZ", "CNSXID", "HPF_SYCN_CNSXJZXGWD", "CNSXJZID");
            return dockBTYZTBHStructInitClass;
        }
        // 防止承诺事项重复对接
        // H修正逻辑 由于申遗承诺 每年11-12录入一次   对接的时候发现承诺事项已经对接的 将不再对接 查询元数据做为关联ID
        private Dictionary<string, Guid> SetSYCNDic(IDBHelper dbContext)
        {
            var dic= new Dictionary<string, Guid>(); 
            var sql = string.Format("select ID,YCDSJID from HPF_SYCN_CNSX  where GLYCBTID='{0}'", this.HeritageId);// and YCDSJID='{1}'";
            var dt = dbContext.getDataTableResult(sql);
            if (dt == null) return dic;
            foreach (DataRow dr in dt.Rows)
            {
                var gid = new Guid(dr["ID"] + "");//
                var ycdsjid = dr["YCDSJID"] + "";
                if (!dic.Keys.Contains(ycdsjid)) {
                    dic.Add(dr["YCDSJID"] + "", gid);
                }
            }
            return dic;
        }
        public override string ReceiveData()
        {
            var dockSYCNStructInitClass = GetTableName(FunId);
            var eType = MethodHelper.GetTypeListL(dockSYCNStructInitClass.MainTableName,dockSYCNStructInitClass.SubordinateTableName, dockSYCNStructInitClass.WDTableName);
            //将Json串按指定的类型进行反序列化为dynamic实体对象
            var ent = JsonHelper.DeserializeJsonToDynamicObject(this.BusinessJsonStr, eType);
            var entCNSXList = ent.DATA as IList;//CNSX
            var entCNSXJZList = ent.DATADETAIL as IList;//CNSXJZ
            var entCNSXWDList = ent.DATADETAILSON as IList;//WD
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();

         
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var dicOldToNew = SetSYCNDic(dbContext);//CNSXJZ-CNSX
            var dicWDOldToNew = new Dictionary<string, Guid>();//WD-CNSXJZ
            //主表
            foreach (var item in entCNSXList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                }
                var ysjid = nameToValue["YCDSJID"] + "";//元数据ID
                var guid = Guid.NewGuid();
                if (!dicOldToNew.ContainsKey(ysjid))
                {
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

                    if (!string.IsNullOrEmpty(ysjid))
                    {
                        listYSJID.Add(ysjid);//防止重复对接
                    }
                    listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_SYCN_CNSX", nameToValue));
                }
            }

            //明细
            var dicFileRelatedID = new Dictionary<string, Guid>();
            foreach (var item in entCNSXJZList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                }
                var ysjid = nameToValue["YCDSJID"] + "";
                var guid = Guid.NewGuid();
                dicWDOldToNew.Add(ysjid, guid);
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

                nameToValue["CNSXID"] = dicOldToNew[ysjid];
                dicFileRelatedID.Add(ysjid, guid);
                listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_SYCN_CNSXJZ", nameToValue));

            }
            //文档
            var fileIds = entPathList.Select(e => e.FILEID).ToList();
            var fileInfoList = CommonBusiness.GetFileListByFileID(fileIds);
            if (fileInfoList!=null&&fileIds.Count != fileInfoList.Count)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "文件信息在服务器中找不到,对接失败！"));
            }
            foreach (var item in entCNSXWDList)
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
                if (entPathList.Count > 0)
                {
                    var firstOrDefault = entPathList.FirstOrDefault(e => e.YCDSJID == ysjid);
                    if (firstOrDefault != null)
                    {
                        var fileID = firstOrDefault.FILEID;
                        var fileInfo = fileInfoList.FirstOrDefault(e => e.FILEID == fileID);
                        nameToValue["WDLJ"] = fileInfo.RELATIVEPATH;
                    }
                    nameToValue["CNSXJZID"] = dicWDOldToNew[ysjid];
                    listSqlStr.Add(dbContext.insertByParamsReturnSQL("HPF_SYCN_CNSXJZXGWD", nameToValue));
                }
            }


            if (!CheckIsDock(listSqlStr, listYSJID, "HPF_SYCN_CNSX", dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            var bIsResult = dbContext.executeTransactionSQLList(listSqlStr);
            return JsonHelper.SerializeObject(new ResultModel(bIsResult, bIsResult ? "对接成功" : "对接失败"));
        }
    }
}