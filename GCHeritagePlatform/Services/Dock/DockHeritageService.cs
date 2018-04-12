using System;
using System.IO;
using GCHeritagePlatform.Utils;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.BaseInfo.Interface;
using FrameworkCore.Utils;
using GCHeritagePlatform.Services.PublicMornitor.Interface;
using System.Collections.Generic;
using GCHeritagePlatform.Services.Models;
using GCHeritagePlatform.Services.Dock;

namespace GCHeritagePlatform.Services.PublicMornitor
{
    /// <summary>
    /// 遗产地对接 通用服务类   内部遗产地对接的入口 
    /// </summary>
    public class HeritageBaseDockService : IHeritageDockService
    {
        [HproseAttribute("string ReceiveData(string jsonStr,string funId,string heritageId1)", "对接数据类,对接那一个遗产地那一类的数据项")]
        public virtual string ReceiveData(string jsonStr, string funId, string heritageId)
        {
            var beforeVerifyStr = VerifyBaseData(jsonStr, funId, heritageId);
            if (!string.IsNullOrEmpty(beforeVerifyStr)) return beforeVerifyStr;
            SystemLogger.getLogger().Info(string.Format("遗产地ID为{2},funId为：{1},时间{3},遗产地数据Json为：{0},", jsonStr, funId, heritageId,DateTime.Now));
            IDockBaseService businseeService = null;
            //各个逻辑 对外同一接口 不同逻辑处理方式不同
            switch (funId)
            {
                case "101"://申遗承诺事项进展
                    businseeService = new DockSYCNServices(jsonStr, funId, heritageId);
                    break;
                case "20101"://保护管理机构
                case "202"://专项保护管理法规、规章
                case "1302"://安防消防 管理制度和应急预案记录
                    businseeService = new DockBHGLJG_Service(jsonStr, funId, heritageId);
                        break;
                case "304"://遗产要素单体或局部测绘基准图和标志性影像
                    businseeService = new DockYCJCXX_YSDTServices(jsonStr, funId, heritageId, "");
                    break;
                case "601"://遗产要素单体或局部测绘图及遗产要素现状记录
                    businseeService = new DockYCYSDT_JCeSJServices(jsonStr, funId, heritageId, "HPF_YCYSDT_YCYSDTHJBXZT");
                    break;
                case "602"://遗产要素单体或局部影像及遗产要素现状记录
                    businseeService = new DockYCYSDT_JCeSJServices(jsonStr, funId, heritageId, "HPF_YCYSDT_YCYSDTHJBTP");
                    break;
                case "702"://病害调查监测工作情况记录(附件未实现，不知存到哪个表中)
                    businseeService = new DockFileBaseService(jsonStr, funId, heritageId);
                    break;
                case "70301"://裂缝_文档类
                case "70303"://沉降_文档类
                //case "70304"://变形_文档类
                case "70307"://糟朽
                case "70308"://白蚁
                case "70309"://钢筋锈蚀
                case "70310"://植物根系
                case "70311"://渗漏水
                case "70312"://脱落
                    businseeService = new DockBTYZTBHService(jsonStr, funId, heritageId);
                    break;
                case "70302": //裂缝数值类
                    businseeService = new DockBTYZTBHValueService(jsonStr, funId, heritageId, "HPF_BTYZTBH_LF", "LFID");
                    break;
                case "70305": //水平位移数值类
                    businseeService = new DockBTYZTBHValueService(jsonStr, funId, heritageId, "HPF_BTYZTBH_SPWY", "SPWYID");
                    break;
                case "70306"://沉降数值类（设备监测）
                    businseeService = new DockBTYZTBHValueService(jsonStr, funId, heritageId, "HPF_BTYZTBH_CJ", "CJID");
                    break;
                case "70501"://移动采集端通用文档类的对接接口
                    businseeService = new DockTyWd(jsonStr, funId, heritageId, "HPF_BTYZTBH_WDLBHTYBZP","WDID");
                    break;
                case "70502"://移动采集端通用照片类的对接接口
                    businseeService = new DockTyZp(jsonStr, funId, heritageId);
                    break;
                case "70503"://移动采集端通用测项的对接接口
                    businseeService = new DockCeXiangTable(jsonStr, funId, heritageId);
                    break;
                case "70504"://移动采集端通用病害采集记录表的对接接口
                    businseeService = new DockTyjlTable(jsonStr, funId, heritageId);
                    break;
                case "8020403"://台风路径信息
                    businseeService = new DockZRHJ_TFLJService(jsonStr, funId, heritageId, "HPF_ZRHJ_TFLJXX");
                    break;
                case "8020404"://台风预估点信息
                    businseeService = new DockZRHJ_TFLJService(jsonStr, funId, heritageId, "HPF_ZRHJ_TFYGDXX");
                    break;
                //case "90201"://新建项目记录（新建项目记录走基类，带附件的项目范围图（鼓浪屿除外，鼓浪屿使用的超擎的矢量）和现场环境照片走这里。）
                case "90202"://带附件的项目范围图（鼓浪屿除外，鼓浪屿使用的超擎的矢量）。
                case "90203"://新建项目现场环境照片
                    businseeService = new DockJSKZService(jsonStr, funId, heritageId);
                    break;
                case "1101"://日游客容量限制值
                case "1102"://瞬时游客容量限制值
                case "1103"://日游客量
                case "1104"://瞬时游客量
                case "1105"://客流高峰时段现场照片
                    businseeService = new DockLYYYKGLService(jsonStr, funId, heritageId);
                    break;
                
                   
                case "1401"://考古报告信息
                case "1403"://考古发掘现场照片
                    businseeService = new DockKGFJ_KGFileServices(jsonStr, funId, heritageId);
                    break;
                case "1402"://考古发掘记录(其中有已发表的简报文件的对接文件，没有的不对)
                // case "1402"://考古发掘工作（8月1日改后，已经走了基类new DockBaseService）
                businseeService = new DockKGFJService(jsonStr, funId, heritageId);
                break;
                //case "150103"://保护工程工程方案文档，总平台不要
                case "1503"://保护现场照片
                    businseeService = new DockBHGC_XCZPServices(jsonStr, funId, heritageId);
                    break;
                case "1501"://保护展示与环境整治工程记录
                    businseeService = new DockProtectDERPRService(jsonStr, funId, heritageId);
                    break;
                case "1601"://保护管理规划编制记录
                    businseeService = new DockBHGLGHService(jsonStr, funId, heritageId);
                    break;
                case "1602"://现行保护管理规划执行情况
                    businseeService = new DockXXGHZXQKJL(jsonStr, funId, heritageId);
                    break;
                default:
                    businseeService = new DockBaseService(jsonStr, funId, heritageId);//base为只存数据，无附件的基类
                    break;
            }
            return businseeService.ReceiveData();
            
        }
        [HproseAttribute("string ReceiveFile(byte[] fileInfo,string fileNmae, string businessFunID, string heritageId1)", "对接文件类")]
        public virtual string ReceiveFile(byte[] fileInfo, string fileNmae, string businessFunID, string heritageId)
        {
            //遗产地 + 大类+ 功能 +文件
            //var ftpUserID   = System.Configuration.ConfigurationManager.AppSettings["ftpUser"];
            //var ftpPassword = System.Configuration.ConfigurationManager.AppSettings["ftpPassword"];
            SystemLogger.getLogger().Info(DateTime.Now + "访问bug");
            var ftpAddress  = System.Configuration.ConfigurationManager.AppSettings["Address"];
            var rPath = "";
            var filePath    = CommonBusiness.GetFileTempPath(businessFunID, heritageId,ftpAddress,out  rPath);
            var fileNewGuid = Guid.NewGuid().ToString();
            
            var filePathAll = Path.Combine(filePath, fileNewGuid + "." + fileNmae.GetExtensioName());
            if (!System.IO.Directory.Exists(filePath))
            {
                SystemLogger.getLogger().Info(DateTime.Now+"创建目录");
                System.IO.Directory.CreateDirectory(filePath);
             
            }
            FileStream fstream = File.Create(filePathAll, fileInfo.Length);
            fstream.Write(fileInfo, 0, fileInfo.Length);   //
            SystemLogger.getLogger().Info(DateTime.Now + "创建目录正常");
            try
            {
                var dbContext = DBHelperPool.Instance.GetDbHelper();
                if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
                var sql = @"INSERT INTO `HPF_TEMP_FILE` (`ID`,`WJMC`,`WJLX`,`WJLJ`)	VALUES ('{0}','{1}','{2}','{3}')";
                SystemLogger.getLogger().Info(DateTime.Now + "访问bug1");
                var iResult= dbContext.execute(string.Format(sql,fileNewGuid,fileNmae,fileNmae.GetExtensioName(),rPath + fileNewGuid + "." + fileNmae.GetExtensioName()));
                SystemLogger.getLogger().Info(DateTime.Now + "访问bug2");
                SystemLogger.getLogger().Info("返回结果："+iResult);
                return iResult > 0 ? fileNewGuid : "";

            }
            catch (Exception ex)
            {
                var strErr = string.Format("遗产地对接文件类数据错误，参数：{0}#{1}#{2}，具体错误：{3}", fileNmae, businessFunID, heritageId, ex.Message);
                SystemLogger.getLogger().Error(strErr);
            }
            finally
            {
                fstream.Close();
            }
            SystemLogger.getLogger().Info(DateTime.Now + "访问bug4");
            return fileNewGuid;
        }
        [HproseAttribute("string UpdateData(string jsonStr,string funId,string heritageId)", "对接数据类,对接那一个遗产地那一类的数据项进行修改")]
        public string UpdateData(string jsonStr, string funId, string heritageId)
        {
            try
            {

            
            SystemLogger.getLogger().Info(string.Format("遗产地ID为{2},funId为：{1},修改时间{3},遗产地数据Json为：{0},", jsonStr, funId, heritageId, DateTime.Now));
            var businseeService = new DockBaseService(jsonStr, funId, heritageId);
            return businseeService.UpdateData();
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public string VerifyBaseData(string jsonStr,string funId,string heritageId)
        {
            //判断传过来的json是否为空
           if(string.IsNullOrEmpty(jsonStr)||jsonStr.Length<10) return   JsonHelper.SerializeObject(ToolResult.Failure("传输字符串为空，不能对接!"));
           //funid 找不到 返回错

            return "";
        }
    }
}




