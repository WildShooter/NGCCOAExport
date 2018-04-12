using System;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 日常巡查异常记录
    /// </summary>
    public class HPF_RCXC_RCXCYCJL
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public DateTime? XCRQ { get; set; }
        public string YCSJ { get; set; }
        public string XCY { get; set; }
        public string DSCQCS { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SFYDJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public string SJMS { get; set; }
        public string FSWZ { get; set; }
        public string FSYCD { get; set; }
        public double? JD { get; set; }
        public double? WD { get; set; }
        public string PG { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 保养与维护工程记录
    /// </summary>
    public class HPF_BHGC_BYWHJL
    {
        public string ID { get; set; }
        public string GCXMID { get; set; }
        public string BYYWHRQ { get; set; }
        public string BYYWHDX { get; set; }
        public string BYYWHNR { get; set; }
        public string SSZ { get; set; }
        public string BYYWHGZSM { get; set; }
        public string CJRID { get; set; }
        public string CJSJ { get; set; }
        public string DJRID { get; set; }
        public string DJSJ { get; set; }
        public string SFYDJ { get; set; }
        public string SHRID { get; set; }
        public string SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string NF { get; set; }
        public string YF { get; set; }
        public string SHYC { get; set; }
        public string RKSJ { get; set; }
        public string YCDSJID { get; set; }
    }
}