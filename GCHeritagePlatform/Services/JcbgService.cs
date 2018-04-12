using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services
{
    public static class JcbgService
    {
        private static Dictionary<string, ExportConfig> Dic4Relationship {get; set;}
        public static Dictionary<string, ExportConfig> GetDic()
        {
            if (Dic4Relationship != null)
                return Dic4Relationship;
            Dic4Relationship = new Dictionary<string, ExportConfig>();

            SetGWGL(Dic4Relationship);
            SetHTGL(Dic4Relationship);
            SetRSGL(Dic4Relationship);
            SetZCGl(Dic4Relationship);
            return Dic4Relationship;
        }
        /// <summary>
        /// 公文管理
        /// </summary>
        /// <param name="dic"></param>
        public static void SetGWGL(Dictionary<string, ExportConfig> dic)
        {
            //收文管理
            dic.Add("附件2-收文登记", new ExportConfig("附件2-收文登记", "getDocumentById"));
            //发文管理
            dic.Add("附件1-发文拟稿", new ExportConfig("附件1-发文拟稿", "getFDocumentById"));
            
        }
        /// <summary>
        /// 合同管理
        /// </summary>
        /// <param name="dic"></param>
        public static void SetHTGL(Dictionary<string, ExportConfig> dic)
        {
            //合同审核
            dic.Add("附件1-合同签订审批表", new ExportConfig("附件1-合同签订审批表", "getHtxxInfo"));
            //合同审核
            dic.Add("附件2-合同履约完成表", new ExportConfig("附件2-合同履约完成表", "getHtlyxxInfo"));
        }
        public static void SetRSGL(Dictionary<string, ExportConfig> dic)
        {
            //请假管理
            dic.Add("附件2-请假条", new ExportConfig("附件2-请假条", "getQjtDetail"));
            //培训管理
            dic.Add("附件9-培训申请模版", new ExportConfig("附件9-培训申请模版", "getNbpxByid"));
            dic.Add("附件19-培训登记模版（内部）", new ExportConfig("附件19-培训登记模版（内部）", "getNbpxjlByJlid"));
            dic.Add("附件20-培训登记模版（外部）", new ExportConfig("附件20-培训登记模版（外部）", "getWbpxinfo"));
            //证照管理
            dic.Add("附件6-国家基础地理信息中心在职干部因私事出国(境)领用表", new ExportConfig("附件6-国家基础地理信息中心在职干部因私事出国(境)领用表", "getLySpDetailNew"));
            dic.Add("附件6-国家基础地理信息中心在职干部因私事出国(境)领用表and审批表", new ExportConfig("附件6-国家基础地理信息中心在职干部因私事出国(境)领用表and审批表", "getLySpDetailNew"));
            dic.Add("附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表", new ExportConfig("附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表", "getLySpDetailNew"));
            dic.Add("附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表and保密表and审批表涉密", new ExportConfig("附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表and保密表and审批表涉密", "getLySpDetailNew"));
            dic.Add("附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表and保密表and审批表涉密and审批表非涉密", new ExportConfig("附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表and保密表and审批表涉密and审批表非涉密", "getLySpDetailNew"));
            
            
        }
        public static void SetZCGl(Dictionary<string, ExportConfig> dic)
        {
            //资产购置
            dic.Add("附件4-固定资产购置申请、审批表", new ExportConfig("附件4-固定资产购置申请、审批表", "getGzsqXq", "XH#WPMC#GGXHJCSSM#DJ#SL#ZJ%DJ%SL%#NBGSYRID#NCFD#SFSMSY#JFZCKMID"));
            //资产入库
            dic.Add("附件18-固定资产验收及入库单（家具）", new ExportConfig("附件18-固定资产验收及入库单（家具）", "getYsrkdInfo", "XH#MC#PP#GGXH#ZMYSJZ#QDFS#ZCBH#CFDD#SFSM#ZCSYR#BZ"));
            dic.Add("附件19-固定资产验收及入库单（设备）", new ExportConfig("附件19-固定资产验收及入库单（设备）", "getYsrkdInfo", "XH#MC#PP#GGXH#ZMYSJZ#QDFS#ZCBH#CFDD#SFSM#SYRID#CPXLH#BZ"));
            dic.Add("附件3-固定资产验收及入库单（房屋、土地、交通）", new ExportConfig("附件3-固定资产验收及入库单（房屋、土地、交通）", "getYsrkdInfo", "XH#MC#PP#GGXH#ZMYSJZ#QDFS#ZCBH#CFDD#SFSM#SYRID#BZ"));
            //资产借出
            dic.Add("附件7-固定资产借出登记申请表", new ExportConfig("附件7-固定资产借出登记申请表", "getJcsqbInfo", "XH#ZCBH#ZCMC#XHSM#GZSJ#GZJE#SFSM#GHRQ"));
            //资产外拨
            dic.Add("附件6-固定资产调配调拨单", new ExportConfig("附件6-固定资产调配调拨单", "getDbdInfo", "XH#ZCBH#ZCMC#YZ#YCFDD#YBGSYR&REALNAME#YSFSM#XCFDD#XBGSYRID#XSFSM"));
            //资产变更
            dic.Add("附件5-固定资产管理信息变更表", new ExportConfig("附件5-固定资产管理信息变更表", "getBgdInfo", "XH#ZCBH#ZCMC#ZCXXBGNR#BGQXX#BGHXX#"));
            //低值易耗品
            dic.Add("附件9-低值易耗品购置申请表", new ExportConfig("附件9-低值易耗品购置申请表", "getDzyhsqInfo", "XH#CLMC#XHSM#DJ#SL#ZJ%DJ%SL%#SMSYSL#BZ"));
            //资产处置
            dic.Add("附件8-固定资产处置申请表", new ExportConfig("附件8-固定资产处置申请表", "getCzsqbInfo","XH#ZCBH#ZCMC#PP#XHSM#YZ#GZSJ#SQCZFS#SFSM"));
        }
    }
}