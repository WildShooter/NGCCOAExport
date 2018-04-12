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
    /// 遗产基础信息304
    /// </summary>
    public class DockYCJCXX_YSDTServices:DockBaseService
    {
        public DockYCJCXX_YSDTServices(string jsonStr, string funId, string heritageId, string className):base(jsonStr, funId, heritageId)
        {
            this.ClassName = className;
        }
        
        public string ClassName { get; set; }
        public override string ReceiveData()
        {
            string ResultInfo = "";
            bool CHJZTResult = false;
            bool JBTPResult = false;
            var heritageId = this.HeritageId;
            var funId = this.FunId;
            var jsonStr = this.BusinessJsonStr;
            try
            {
                var listSqlStr = new List<string>();
                var listYSJID = new List<string>();
                var xmlConfig = CommonBusiness.GetMornitorConfig(heritageId);
                var funList = xmlConfig.GetFunctionalModules();
                var ent = JsonHelper.DeserializeJsonToObject<ResultYCJCXX4DockModel>(jsonStr);
                var dbContext = DBHelperPool.Instance.GetDbHelper();
                #region 遗产要素单体或局部测绘基准图
                string funId_chjzt = funId + "01";
                var funModel = funList.FirstOrDefault(e => e.ID == funId_chjzt);
                if (ent.DATADETAIL == null || ent.DATA ==null)
                    return JsonHelper.SerializeObject(new ResultModel(false, "对接主表或子表数据为空,对接失败"));
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
                    if (!string.IsNullOrEmpty(yscid))//有可能对接过来就是 统计过得数据 例如景点日游客量
                    {
                        listYSJID.Add(yscid);
                    }
                    listSqlStr.Add(dbContext.insertByParamsReturnSQL(funModel.TableName, nameToValue));
                }

                try
                {
                    if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext))
                    {
                        ResultInfo = "已经存在对接的【遗产要素单体或局部测绘基准图】数据" + "\r\n";
                        CHJZTResult = false;
                    }
                    else
                    {
                        dbContext.executeTransactionSQLList(listSqlStr);
                        ResultInfo = "【遗产要素单体或局部测绘基准图】数据对接成功" + "\r\n";
                        CHJZTResult = true;
                    }
                }
                catch (Exception ex)
                {
                    var strErr = string.Format("遗产地对接数据类【遗产要素单体或局部测绘基准图】数据错误,参数：{0}#{1}#{2},具体错误：{3}", jsonStr, funId, heritageId, ex.Message);
                    SystemLogger.getLogger().Error(strErr);
                    CHJZTResult = false;
                    return JsonHelper.SerializeObject(new ResultModel(false, ex.Message));
                }
                #endregion

                #region 遗产要素单体或局部图片
                string funId_jbtp = funId + "02";
                var funModel_jbtp = funList.FirstOrDefault(e => e.ID == funId_jbtp);
                listSqlStr = new List<string>();
                foreach (var item in ent.DATADETAIL)
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
                    if (!string.IsNullOrEmpty(yscid))//有可能对接过来就是 统计过得数据 例如景点日游客量
                    {
                        listYSJID.Add(yscid);
                    }
                    listSqlStr.Add(dbContext.insertByParamsReturnSQL(funModel.TableName, nameToValue));
                }
                try
                {
                    if (!CheckIsDock(listSqlStr, listYSJID, funModel.TableName, dbContext))
                    {
                        ResultInfo = "已经存在对接的【遗产要素单体或局部图片】数据" + "\r\n";
                        JBTPResult = false;
                    }
                    else
                    {
                        dbContext.executeTransactionSQLList(listSqlStr);
                        ResultInfo = "【遗产要素单体或局部图片】数据对接成功" + "\r\n";
                        JBTPResult = true;
                    }
                }
                catch (Exception ex)
                {
                    var strErr = string.Format("遗产地对接数据类【遗产要素单体或局部图片】数据错误,参数：{0}#{1}#{2},具体错误：{3}", jsonStr, funId, heritageId, ex.Message);
                    SystemLogger.getLogger().Error(strErr);
                    JBTPResult = false;
                    ResultInfo = ResultInfo + ex.Message;
                }
                #endregion
                if (CHJZTResult == true && JBTPResult == true)
                {
                    return JsonHelper.SerializeObject(new ResultModel(true, ResultInfo));
                }
                else
                {
                    var strSql = string.Format("delete from " + funModel.TableName + " where YCDSJID='{0}' and GLYCBTID='{1}' ", ent.DATADETAIL[0].YCDSJID, heritageId);
                    dbContext.execute(strSql);
                    strSql = string.Format("delete from " + funModel_jbtp.TableName + " where YCDSJID='{0}' and GLYCBTID='{1}' ", ent.DATADETAIL[0].YCDSJID, heritageId);
                    dbContext.execute(strSql);
                    return JsonHelper.SerializeObject(new ResultModel(false, "数据对接失败"));
                }
            }
            catch (Exception ex)
            {
                var strErr = string.Format("遗产地对接数据类数据错误,参数：{0}#{1}#{2},具体错误：{3}", jsonStr, funId, heritageId, ex.Message);
                SystemLogger.getLogger().Error(strErr);
                return JsonHelper.SerializeObject(new ResultModel(false, ex.Message));
            }
        }

    }

}