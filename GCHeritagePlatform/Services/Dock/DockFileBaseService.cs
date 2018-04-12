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
    /// 
    /// </summary>
    public class DockFileBaseService : DockBTYZTBHService
    {
        public DockFileBaseService(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId)
        {


        }
        public override string ReceiveData()
        {
            var dockBTYZTBHStructInitClass = GetTableName(FunId);
            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象
            var cListType = MethodHelper.GetTypeListFileEx(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, cListType);//遗产地发过来的字符串（json格式）的项与我们在model中建的功能类的属性是一一对应的,这里进行赋值
            var entBHList = ent.DATA as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;

            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();//遗产地数据ID
            var dicFileRelatedID = new Dictionary<string, Guid>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            foreach (var item in entBHList)//因为要将接收过来的数据写到总平台数据库中,所以需要添加ID,以及进行遗产地数据ID进行检查,防止重复插入
            {
                var nameToValue = item.GetNameToValueDic();
                if (nameToValue.ContainsKey("GLYCBTID"))
                {
                    nameToValue["GLYCBTID"] = HeritageId;
                }
                var guid = Guid.NewGuid();
                nameToValue["ID"] = guid;
                var ysjid = nameToValue["YCDSJID"].ToString() + "";
                if (!string.IsNullOrEmpty(ysjid))//有可能对接过来就是 统计过得数据 例如景点日游客量
                {
                    listYSJID.Add(ysjid);//防止重复对接
                }
                dicFileRelatedID.Add(ysjid, guid);
                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName(funModel.TableName), nameToValue));
            }
            //附件
            if (entPathList != null)
            {
                var fileIds = entPathList.Select(e => e.FILEID).ToList();
                var fileInfoList = CommonBusiness.GetFileListByFileID(fileIds);
                if (fileInfoList != null)
                {
                    if (fileInfoList.Count > 0 && fileInfoList.Count != fileIds.Count)
                    {
                        SystemLogger.getLogger().Info(FunId + "#" + HeritageId + "文件对接信息不对应");
                        return JsonHelper.SerializeObject(new ResultModel(false, "文件信息不对应"));
                    }

                    foreach (var item in fileInfoList)
                    {
                        var entFile = entPathList.FirstOrDefault(e => e.FILEID == item.FILEID);
                        //由于StructInitClass结构体构造方法的重载方法限制,故这里的SubordinateTableName实际为RelatedZPTableName,RelatedID实际为RelatedJLID
                        var sql = string.Format("insert into {0} (ID,MC,LJ,{1},GS,RKSJ,LX) values ('{2}','{3}','{4}','{5}','{6}','{7}','{8}')", dockBTYZTBHStructInitClass.SubordinateTableName, dockBTYZTBHStructInitClass.RelatedID, Guid.NewGuid(), item.FILENAME, item.RELATIVEPATH, dicFileRelatedID[entFile.YCDSJID], item.FILETYPE, DateTime.Now.ToString(),entFile.LX);
                        listSqlStr.Add(sql);
                    }
                }
            }
            if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已经存在对接的数据"));
            return GetExeListSQL(dbContext, listSqlStr);

        }
    }
}