using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 安消防系统硬件设施
    /// </summary>
    public class HPF_AFXF_AXFXTYJSS
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string XFXTMC { get; set; }

        public string YTFL { get; set; }

        public DateTime? JCSJ { get; set; }

        public double? TZJE { get; set; }

        public string XTGCMS { get; set; }

        public short? JKTTSL { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SFSLSJL { get; set; }

        public string LSBBID { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
    }
    /// <summary>
    /// 安全事故记录
    /// </summary>
    public class HPF_AFXF_AQSGJL
    {

        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string MC { get; set; }

        public string SM { get; set; }

        public DateTime? FSSJ { get; set; }

        public string SGLX { get; set; }

        public string JB { get; set; }

        public string SS { get; set; }

        public string CLQK { get; set; }

        public string CJRID { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
    }
    /// <summary>
    /// 安全事故相关文档
    /// </summary>
    public class HPF_AFXF_AQSGJL_XGWD
    {
        public string ID { get; set; }

        public string AQSGJLID { get; set; }

        public string WDMC { get; set; }

        public string SM { get; set; }

        public string LJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string WDLX { get; set; }

        public string WDBB { get; set; }

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

        public string SFYDJ { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }

    }
}