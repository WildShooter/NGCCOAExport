using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 考古项目
    /// </summary>
    public class HPF_KGFJ_KGXM
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string XMBH { get; set; }

        public string XMMC { get; set; }

        public string SM { get; set; }

        public DateTime? LXRQ { get; set; }

        public double? JD { get; set; }

        public double? WD { get; set; }

        public string WZHFWSM { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string GLDYCYSID { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }
        public string YCDSJID { get; set; }
    }

    /// <summary>
    /// 考古报告信息（8月1日更改之后的new）
    /// </summary>
    public class HPF_KGFJ_KGBG
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }
        public string KGBGMC { get; set; }

        public string BGLX { get; set; }

        public string BZDW { get; set; }

        public string BXRY { get; set; }

        public string CBZT { get; set; }

        public DateTime? CBSJ { get; set; }

        public string CBDW { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
        public string XGWD { get; set; }

    }

    /// <summary>
    /// 考古发掘工作
    /// </summary>
    public class HPF_KGFJ_KGXM_KGFJGZ
    {

        public string ID { get; set; }
        public string GLYCBTID { get; set; }

        public string KGXMID { get; set; }

        public string XMMC { get; set; }

        public string DJCFJ { get; set; }

        public string WWBMPZWH { get; set; }

        public double? PZFJMJ { get; set; }

        public DateTime? KSSJ { get; set; }

        public DateTime? JSSJ { get; set; }

        public double? FJMJ { get; set; }

        public double? HTMJ { get; set; }

        public double? JFTRZE { get; set; }

        public double? YYFJXCBHDJF { get; set; }

        public string YFBDJB { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
    }

    /// <summary>
    /// 考古发掘工作进展记录
    /// </summary>
    public class HPF_KGFJ_KGXM_KGFJGZ_JZJL
    {
        public string ID { get; set; }

        public string KGXMKGFJGZID { get; set; }

        public DateTime? JZRQ { get; set; }

        public double? BCFJMJ { get; set; }

        public string BT { get; set; }

        public string JZQKSM { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
        public string GLYCBTID { get; set; }

    }

    /// <summary>
    /// 考古发掘现场照片（8月1日更改之后的new）
    /// </summary>
    public class HPF_KGFJ_XGZP
    {
        public string ID { get; set; }

        public string TPMC { get; set; }

        public string SM { get; set; }

        public string TPLJ { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public string TPGS { get; set; }

        public DateTime? CJRQ { get; set; }

        public string PZRID { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string CJDZBXX { get; set; }

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string XZCS { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
        public string GLYCBTID { get; set; }
        public string KGFJJLID { get; set; }
    }

    /// <summary>
    /// 考古项目相关资料
    /// </summary>
    public class HPF_KGFJ_KGXM_XGZL
    {
        public string ID { get; set; }

        public string WDMC { get; set; }

        public string WDLX { get; set; }

        public string WDBB { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string XZCS { get; set; }

        public string KGXMID { get; set; }

        public string SFYDJ { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
    }

    /// <summary>
    /// 已发掘面积
    /// </summary>
    public class HPF_KGFJ_KGXM_YFJZMJ
    {
        public string ID { get; set; }

        public string KGXMID { get; set; }

        public string YFJMJ { get; set; }

        public DateTime? ZHTJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }

    }

    /// <summary>
    /// 考古发掘记录（8月1日更改之后的new）
    /// </summary>
    public class HPF_KGFJ_KGFJJL
    {

        public string ID { get; set; }
        public string GLYCBTID { get; set; }

        public string XMMC { get; set; }

        public string DJCFJ { get; set; }

        public string WWBMPZWH { get; set; }

        public double? PZFJMJ { get; set; }

        public DateTime? KSSJ { get; set; }

        public DateTime? JSSJ { get; set; }

        public double? FJMJ { get; set; }

        public double? HTMJ { get; set; }

        public double? JFTRZE { get; set; }

        public double? YYFJXCBHDJF { get; set; }

        public string YFBDJB { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
    }

    public class HPF_KGFJ_KGFJJL_XGWD
    {
        public string ID { get; set; }
        public string FJJLID { get; set; }
        public string WDMC { get; set; }
        public string WDLX { get; set; }
        public string WDBB { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string SJMJ { get; set; }
        public string FBFW { get; set; }
        public string SFYDJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
        public string YCDSJID { get; set; }
        public string LJ { get; set; }
    }
}
