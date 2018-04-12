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
using System.Linq;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 定义一个本体与载体病害初始化的结构体
    /// </summary>
    public class DockStructInitClass
    {
        /// <summary>
        /// 主表名,例如裂缝
        /// </summary>
        public string MainTableName { get; set; }
        /// <summary>
        /// 子表名,例如裂缝记录
        /// </summary>
        public string SubordinateTableName { get; set; }
        /// <summary>
        /// 指定的病害的ID,对应病害记录表中的病害ID
        /// </summary>
        public string RelatedID { get; set; }
        /// <summary>
        /// 对应的照片表的名称
        /// </summary>
        public string RelatedZPTableName { get; set; }
        /// <summary>
        /// 指定病害的记录ID,对应照片表中的病害记录ID
        /// </summary>
        public string RelatedJLID { get; set; }
        public DockStructInitClass(string maintablename, string subordinatetablename, string relatedid, string relatedzptablename, string relatedjlid)
        {
            this.MainTableName = maintablename;
            this.SubordinateTableName = subordinatetablename;
            this.RelatedID = relatedid;
            this.RelatedZPTableName = relatedzptablename;
            this.RelatedJLID = relatedjlid;
        }

        public DockStructInitClass(string maintablename, string subordinatetablename, string relatedid)
        {
            this.MainTableName = maintablename;
            this.SubordinateTableName = subordinatetablename;
            this.RelatedID = relatedid;
        }
        //public DockBTYZTBHStructInitClass(string maintablename,string relatedzptablename,string relatedjlid) { }
        public DockStructInitClass() { }
    }
    /// <summary>
    /// 本体与载体病害对接服务类
    /// </summary>
    public class DockBTYZTBHService : DockBaseService
    {
        public string sqlInsertTP = "insert into {0} (ID,MC,TPLJ,{1},TPGS,YCDSJID,RKSJ)values ('{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
       
        /// 构造方法
        /// </summary>
        /// <param name="jsonStr">接收的Json串</param>
        /// <param name="funId">功能ID</param>
        /// <param name="heritageId">遗产地ID</param>
        public DockBTYZTBHService(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId)
        {
           // getTableName(funId);
        }
        public virtual DockStructInitClass GetTableName(string funId)
        {
            var dockBTYZTBHStructInitClass = new DockStructInitClass(); 
            switch (funId)
            {
                case "702":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_BHDCJCGZQKJLB", "HPF_BTYZTBH_BHDCJCGZQKJLFJ", "JLID");
                    break;
                case "70301":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_LF", "HPF_BTYZTBH_LFJL", "LFID", "HPF_BTYZTBH_LFZP", "JLID");//裂缝_文档类
                    break;
                case "70303":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_CJ", "HPF_BTYZTBH_CJJL", "CJID", "HPF_BTYZTBH_CJZP", "CJJLID");//沉降_文档类
                    break;
                case "70304":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_SPWY", "HPF_BTYZTBH_SPWYJL", "SPWYID", "HPF_BTYZTBH_SPWYZP", "JLID");//变形
                    break;
                case "70307":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_ZX", "HPF_BTYZTBH_ZXJL", "ZXID", "HPF_BTYZTBH_ZXZP", "ZXJLID");//糟朽
                    break;
                case "70308":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_BY", "HPF_BTYZTBH_BYJL", "BYID", "HPF_BTYZTBH_BYZP", "BYJLID");//白蚁
                    break;
                case "70309":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_GJXS", "HPF_BTYZTBH_GJXSJL", "XSID", "HPF_BTYZTBH_GJXSZP", "XSJLID");//钢筋锈蚀
                    break;
                case "70310":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_ZW", "HPF_BTYZTBH_ZWJL", "ZWID", "HPF_BTYZTBH_ZWZP", "ZWJLID");//植物根系
                    break;
                case "70311":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_SL", "HPF_BTYZTBH_SLJL", "SLID", "HPF_BTYZTBH_SLZP", "SLJLID");//渗漏水
                    break;
                case "70312":
                    dockBTYZTBHStructInitClass = new DockStructInitClass("HPF_BTYZTBH_TL", "HPF_BTYZTBH_TLJL", "TLID", "HPF_BTYZTBH_TLZP", "TLJLID");//脱落
                    break;
            }
            return dockBTYZTBHStructInitClass;
        }
        //对接文档数据  病害工作监测记录 应该先对接 。这个方法返回的是具体的汉字
        public bool GetBHBHByOldDataID(IDBHelper dbHelper,string bhbh,out string newBHBH) {
            newBHBH = "";
            var sql = string.Format(" select * from HPF_BTYZTBH_BHDCJCGZQKJLB where GLYCBTID='{1}' and YCDSJID='{0}'", bhbh,HeritageId);
            var dt= dbHelper.getDataTableResult(sql);
            if (dt == null || dt.Rows.Count == 0) return false;
            newBHBH = dt.Rows[0]["BHBH"].ToString();
            return true;
        }

        public override string ReceiveData()
        {
            var dockBTYZTBHStructInitClass = GetTableName(FunId);
            //根据传入两个表名来获取对应的数据结构模型的类型
            var eType = MethodHelper.GetTypeList(dockBTYZTBHStructInitClass.MainTableName,dockBTYZTBHStructInitClass.SubordinateTableName);
            //将Json串按指定的类型进行反序列化为dynamic实体对象
            var ent = JsonHelper.DeserializeJsonToDynamicObject(this.BusinessJsonStr, eType);


            var entBHList = ent.DATA as IList;
            var entBHJLList = ent.DATADETAIL as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();

            var dicOldToNew = new Dictionary<string, Guid>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            //主表
            foreach (var item in entBHList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                }
                if (nameToValue.ContainsKey("BHBH"))
                {
                    string newBHBH;
                    //代码限定,要求对方传来的病害编号是编号ID,而不是具体的编号内容,并且该ID必须是病害调查监测工作情况记录表中的主键
                    var bIsExistBHBH=  GetBHBHByOldDataID(dbContext,nameToValue["BHBH"] + "", out newBHBH);
                    if (!bIsExistBHBH) {
                        return JsonHelper.SerializeObject(new ResultModel(false, "请先对接病害的工作监测记录！"));
                    }
                    nameToValue["BHBH"] = newBHBH;
                }
                var ysjid = nameToValue["YCDSJID"] + "";//元数据ID
                var guid = Guid.NewGuid();
                //这里保存的是LF的ID
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
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(dockBTYZTBHStructInitClass.MainTableName, nameToValue));
            }
            //明细
            var dicFileRelatedID = new Dictionary<string, Guid>();
            foreach (var item in entBHJLList)
            {
                var nameToValue = item.GetNameToValueDic();
                if (!nameToValue.ContainsKey("YCDSJID"))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "元数据ID不存在,对接失败！"));
                }
                //将裂缝记录中的老外键ID记录下来，保存的裂缝中的ycdsjid
                var lfid = nameToValue[dockBTYZTBHStructInitClass.RelatedID] + "";
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
                nameToValue[dockBTYZTBHStructInitClass.RelatedID] = dicOldToNew[lfid];
                var ysjid = nameToValue["YCDSJID"] + "";
                //这里保存的是裂缝JL的ID
                dicFileRelatedID.Add(ysjid, guid);
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(dockBTYZTBHStructInitClass.SubordinateTableName, nameToValue));
            }

            //附件
            if (entPathList != null && entPathList.Count > 0)
            {
                var fileIds = entPathList.Select(e => e.FILEID).ToList();
                var fileInfoList = CommonBusiness.GetFileListByFileID(fileIds);
                if (fileInfoList == null || fileInfoList.Count != fileIds.Count)
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "文件信息不对应"));
                }
                foreach (var item in fileInfoList)
                {
                    var entFile = entPathList.FirstOrDefault(e => e.FILEID == item.FILEID);

                    //public string sqlInsertTP = "insert into {0} (ID,MC,TPLJ,{1},TPGS,YCDSJID,RKSJ)values ('{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                    var sql = string.Format(this.sqlInsertTP, dockBTYZTBHStructInitClass.RelatedZPTableName, dockBTYZTBHStructInitClass.RelatedJLID, Guid.NewGuid(), item.FILENAME, item.RELATIVEPATH, dicFileRelatedID[entFile.YCDSJID], item.FILETYPE, entFile.YCDSJID, DateTime.Now.ToString());
                    ///JLID
                    listSqlStr.Add(sql);
                }
            }
            if (!CheckIsDock(listSqlStr, listYSJID, dockBTYZTBHStructInitClass.MainTableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            return GetExeListSQL(dbContext, listSqlStr);
        }

    }
}