
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 保护管理机构
    /// </summary>
    public class HPF_JGJS_BHGLJG
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string JGMC { get; set; }
        /// <summary>
        /// 组织结构代码
        /// </summary>
        public string ZZJGDM { get; set; }
        /// <summary>
        /// 所在行政区
        /// </summary>
        public string SZXZQ { get; set; }
      
        public string CDDZR { get; set; }
        public string GLQYSM { get; set; }
        public string FDDBR { get; set; }
        public string LXRXM { get; set; }
        public string LXDH { get; set; }
        public string DZYX { get; set; }
        public string GFWZ { get; set; }
        public string SJGLDW { get; set; }
        public DateTime? JGCLSJ { get; set; }
        public string JGJB { get; set; }
        public decimal? BZRS { get; set; }
        public decimal? GZRYZS { get; set; }
        public string ZYJFLY { get; set; }
        public DateTime? JGCXSJ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SFYDJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHBTGSM { get; set; }
        public string SHZT { get; set; }
        public string BBH { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
    }

    /// <summary>
    /// 监测机构信息
    /// </summary>
    public class HPF_JGJS_JCJGXX
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string JCJGMC { get; set; }
        public string JCJGDM { get; set; }
        public string FZR { get; set; }
        public string FZRLXDH { get; set; }
        public string CDDZR { get; set; }
        public string LXRXM { get; set; }
        public string LXDH { get; set; }
        public string DZYX { get; set; }
        public string JCGZWZ { get; set; }
        public string SJGLDW { get; set; }
        public DateTime? JGCLRQ { get; set; }
        public decimal? RYZS { get; set; }
        public DateTime? JGCXRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string BBH { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SFYDJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 专项保护管理法规、规章
    /// </summary>
    public class HPF_JGJS_ZXBHGLFGGZ
    {
        public string ID { get; set; }
        public string YCDSJID { get; set; }
        public string LB { get; set; }
        public string MC { get; set; }
        public DateTime? GBSJ { get; set; }
        public string GBWH { get; set; }
        public DateTime? SSSJ { get; set; }
        public string ZT { get; set; }
        public string FJ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SFYDJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string GLYCBTID { get; set; }
        public string WJNR { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 保护管理相关培训记录
    /// </summary>
    public class HPF_JGJS_BHGLXGPXJL
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string ZZPXDW { get; set; }
        public string PXXMMC { get; set; }
        public string PXXMSM { get; set; }
        public DateTime? PXKSSJ { get; set; }
        public string PXJZSJ { get; set; }
        public string PXSC { get; set; }
        public decimal? SXRYSL { get; set; }
        public string PXDD { get; set; }
        public decimal? PXZCJF { get; set; }
        public string BZSM { get; set; }
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
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }

    public class V_HPF_JGJS_BHGLXGPXJL
    {
        public Int64  PXY { get; set; }
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string ZZPXDW { get; set; }
        public string PXXMMC { get; set; }
        public string PXXMSM { get; set; }
        public DateTime? PXKSSJ { get; set; }
        public string PXJZSJ { get; set; }
        public string PXSC { get; set; }
        public decimal? SXRYSL { get; set; }
        public string PXDD { get; set; }
        public decimal? PXZCJF { get; set; }
        public string BZSM { get; set; }
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
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }



    /// <summary>
    /// 保护管理经费
    /// </summary>
    public class HPF_JGJS_BHGLJF
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string BHGLDW { get; set; }
        public string NF { get; set; }
        public double? BHGLJF { get; set; }
        public DateTime? JFHQSJ { get; set; }
        public string JFLYSM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? TJSJ { get; set; }
        public string SFYDJ { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string BHGLJFLX { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }

    public class V_HPF_JGJS_BHGLJF
    {
        public Int64 JFY { get; set; }
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string BHGLDW { get; set; }
        public string NF { get; set; }
        public double? BHGLJF { get; set; }
        public DateTime? JFHQSJ { get; set; }
        public string JFLYSM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? TJSJ { get; set; }
        public string SFYDJ { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string BHGLJFLX { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 管理区域图
    /// </summary>
    public class HPF_JGJS_GLQYT
    {
        public string ID { get; set; }
        public string BHGLJGID { get; set; }
        public string TZMC { get; set; }
        public string TZSM { get; set; }
        public string TZLJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string BLC { get; set; }
        public string CHRID { get; set; }
        public DateTime? HZSJ { get; set; }
        public string TJRID { get; set; }
        public DateTime? TJSJ { get; set; }
        public decimal? SFYTJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string SJMJ { get; set; }
        public string FBFW { get; set; }
        public string XZCS { get; set; }
        public string SFYDJ { get; set; }
        public string SHYC { get; set; }
        public string SCRID { get; set; }
        public DateTime? SCSJ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 专项法规相关文档
    /// </summary>
    public class HPF_JGJS_ZXFGXGWD
    {
        public string ID { get; set; }
        public string FGID { get; set; }
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
        public string YSWDSCDD { get; set; }
        public string YSWDBH { get; set; }
        public string YSWDSCDW { get; set; }
        public DateTime? YSWDSCSJ { get; set; }
        public string SZHFZRID { get; set; }
        public DateTime? SZHSJ { get; set; }
        public string SJMJ { get; set; }
        public string FBFW { get; set; }
        public string CCLJ { get; set; }
        public decimal? SFYDJ { get; set; }
        public string SHYC { get; set; }
        public string SCRID { get; set; }
        public DateTime? SCSJ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
}