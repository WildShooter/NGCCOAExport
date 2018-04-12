using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 使用功能变化记录
    /// </summary>
    public class HPF_SYGN_SYGNBHTJBHJL
    {
        public string ID { get; set; }

        public string TZBH { get; set; }

        public string MC { get; set; }

        public string GLYCBTID { get; set; }

        public short? SFLMT { get; set; }

        public string TZGS { get; set; }

        public string BLC { get; set; }

        public string CHRID { get; set; }

        public DateTime? CTSJ { get; set; }

        public string TZLJ { get; set; }

        public string GLZRDW { get; set; }

        public string SJMJ { get; set; }

        public string ZTGJBHTURL { get; set; }

        public string PG { get; set; }

        public string QKSM { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public DateTime? TJSJ { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
        public Int64 RKN { get; set; }
    }
    /// <summary>
    /// 日志
    /// </summary>
    public class HPF_SYGN_RZ
    {
        public string ID { get; set; }

        public string TZID { get; set; }

        public string CKR { get; set; }

        public DateTime? CKSJ { get; set; }

        public string XZR { get; set; }

        public DateTime? XZSJ { get; set; }

        public short? XZCS { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }
}