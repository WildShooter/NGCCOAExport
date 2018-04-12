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
    public class DockJSKZService : DockBaseService
    {
        public DockJSKZService(string jsonStr, string funId, string heritageId) : base(jsonStr, funId, heritageId) { }
        public override string ReceiveData()
        {
            var funModel = this.FindFunModel(HeritageId, FunId);
            if (funModel == null)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "找不到该功能对应的配置信息"));
            }
            //通过xml配置的表名 找到类的路径 反射成 list类 对象
            var cListType = MethodHelper.GetTypeListFileEx(GetModelName(funModel.TableName));//GCHeritagePlatform.Services.PublicMornitor.Model.HPF_RCXC_RCXCYCJL;
            var ent = JsonHelper.DeserializeJsonToDynamicObject(BusinessJsonStr, cListType);//遗产地发过来的字符串（json格式）的项与我们在model中建的功能类的属性是一一对应的,这里进行赋值
            var entData = ent.DATA as IList;
            var entPathList = ent.FILEPATHLIST as List<FileInfoEx>;
            //下面注释的代码表示,当没有附件的时候,记录也得对接上来,不应该报错误信息
            //if (ent.FilePathList == null || ent.FilePathList.Count == 0)
            //{
            //    return JsonHelper.SerializeObject(new ResultModel(false, "缺少对接文档信息！"));
            //}
            var listSqlStr = new List<string>();
            var listYSJID = new List<string>();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var receiveAllFileInfo = entPathList == null ? null : CommonBusiness.GetFileListByFileID(entPathList.Select(e => e.FILEID));
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
                var strSql = string.Format("select ID from HPF_XJXM_XJXMJL where YCDSJID='{0}' and GLYCBTID='{1}'", nameToValue["XJXMID"], HeritageId);
                var dtMain = dbContext.getDataTableResult(strSql);
                if (dtMain == null || dtMain.Rows.Count == 0)
                    return JsonHelper.SerializeObject(new ResultModel(false, "请先对接新建项目记录！"));
                nameToValue["XJXMID"] = dtMain.Rows[0][0] + "";
                var ysjid = nameToValue["YCDSJID"] + "";
                if (string.IsNullOrEmpty(ysjid)) return JsonHelper.SerializeObject(new ResultModel(false, "YCDSJID不能为空！"));
                //附件
                if (entPathList != null && entPathList.Count > 0)
                {
                    var file = entPathList.Where(e => e.YCDSJID == ysjid).FirstOrDefault();
                    if (file != null)
                    {
                        var filePath = receiveAllFileInfo.FirstOrDefault(e => e.FILEID == file.FILEID);
                        if (filePath != null)
                        {
                            switch (FunId)
                            {
                                //项目范围图（如果是超擎的矢量图，则无法走这里，这里只适合带图片的项目范围图附件形式对接！）
                                case "90202":
                                    nameToValue["TZMC"] = filePath.FILENAME;
                                    nameToValue["TZGS"] = filePath.FILETYPE;
                                    nameToValue["TZLJ"] = filePath.RELATIVEPATH;
                                    break;
                                //新建项目现场照片
                                case "90203":
                                    nameToValue["LJ"] = filePath.RELATIVEPATH;
                                    break;
                            }

                        }
                    }
                }

                listSqlStr.Add(dbContext.insertByParamsReturnSQL(GetModelName( funModel.TableName), nameToValue));

            }

            if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext)) return JsonHelper.SerializeObject(new ResultModel(false, "已存在对接的数据！"));
            return GetExeListSQL(dbContext, listSqlStr);
        }
    }
}
