using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 保护管理规划
    /// </summary>
    public class HPF_BHGH_BHGLGH
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string MC { get; set; }

        public string BZHGBZT { get; set; }

        public string GHKSNF { get; set; }

        public string GHJZNF { get; set; }

        public string GHQX { get; set; }

        public string ZZBZDW { get; set; }

        public string BZDW { get; set; }

        public DateTime? KSBZRQ { get; set; }

        public DateTime? GBSSRQ { get; set; }

        public DateTime? XBRQ { get; set; }

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
    }
    /// <summary>
    /// 保护管理规划相关文档
    /// </summary>
    public class HPF_BHGH_BHGLGH_XGWD
    {
        public string ID { get; set; }

        public string BHGLGHID { get; set; }

        public string WDMC { get; set; }

        public string WDLX { get; set; }

        public string LJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string WDBB { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string XZCS { get; set; }

        public string WDSM { get; set; }

        public string SFYDJ { get; set; }

        public string SHYC { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }

    }
    /// <summary>
    /// 现行规划执行情况记录
    /// </summary>
    public class HPF_BHGH_BHGLGH_XXZXQKJL
    {
        public string ID { get; set; }

        public string BHGLGHID { get; set; }

        public string JQGHXM { get; set; }

        public string SSZT { get; set; }

        public string WSSYY { get; set; }

        public string BZ { get; set; }

        public string ZXQKZHPJ { get; set; }

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
    }
    /// <summary>
    /// 现行规划执行情况综合评价
    /// </summary>
    public class HPF_BHGH_XXGHZXQKZHPJ
    {
        public string ID { get; set; }

        public string NF { get; set; }

        public string ZHPJ { get; set; }

        public DateTime? PJSJ { get; set; }

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
    }
}