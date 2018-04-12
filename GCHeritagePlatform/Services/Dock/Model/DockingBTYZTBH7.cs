using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 病害调查监测工作情况记录表
    /// </summary>
    public class HPF_BTYZTBH_BHDCJCGZQKJLB
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string BHBH { get; set; }
        public string BHWZ { get; set; }
        public double? BHJD { get; set; }
        public double? BHWD { get; set; }
        public string BHSYT { get; set; }
        public string BHCZZT { get; set; }
        public string BHLX { get; set; }
        public DateTime? JCKSSJ { get; set; }
        public DateTime? JCJSSJ { get; set; }
        public string JCFF { get; set; }
        public string SJCJDBH { get; set; }
        public string JCZQ { get; set; }
        public string SSJG { get; set; }
        public string JCSJKSYFW { get; set; }
        public string JCJLBCDD { get; set; }
        public string JCJLBCSJ { get; set; }
        public string CJRID { get; set; }
        public string CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public short? SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string BBH { get; set; }
        public DateTime? TJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
        public string YCYSBM { get; set; }
    }
    public class HPF_BTYZTBH_BHDCJCGZQKJLFJ {
        public string ID { get; set; }
        public string JLID { get; set; }
        public string MC { get; set; }
        public string SJL { get; set; }
        public string GS { get; set; }
        public string LJ { get; set; }
        public DateTime? CJSJ { get; set; }
        public string CJR { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 裂缝（文档类）
    /// </summary>
    public class HPF_BTYZTBH_LF
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string BM { get; set; }
        public string GLYCBTID { get; set; }
        public string JCDX { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string BHWZ { get; set; }
        public string YCDSJID { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? DJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 裂缝记录
    /// </summary>
    public class HPF_BTYZTBH_LFJL
    {
        public string ID { get; set; }
        public string LFID { get; set; }
        public string BHWZ { get; set; }
        public string LFMXT { get; set; }
        public string LFCD { get; set; }
        public string LFKD { get; set; }
        public string LFSD { get; set; }
        public string LFKKFW { get; set; }
        public string LFCTQK { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCRID { get; set; }
        public DateTime? DCSJ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public short? SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 裂缝宽度（数值类）HPF_BTYZTBH_LFKD
    /// </summary>
    public class HPF_BTYZTBH_LFKD
    {
        public string ID { get; set; }
        public string YCDSJID { get; set; }
        public string CXBH { get; set; }
        public string LFID { get; set; }
        public string LFKD { get; set; }
        public string PJKDYCD { get; set; }
        public string PJKDZPT { get; set; }
        public string ZDKD { get; set; }
        public string ZXKD { get; set; }
        public string SSQW { get; set; }
        public string SFYJ { get; set; }
        public string JCSJ { get; set; }
        public string PG { get; set; }
        public string SHRID { get; set; }
        public string SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public string DJSJ { get; set; }
        public string RKSJ { get; set; }
    }
    /// <summary>
    /// 变形（文档类）
    /// </summary>
    public class HPF_BTYZTBH_SPWY
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string GLYCBTID { get; set; }
        public string JCDX { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public string SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public string DJSJ { get; set; }
        public string BHWZ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 变形监测记录
    /// </summary>
    public class HPF_BTYZTBH_SPWYJL
    {
        public string ID { get; set; }
        public string SPWYID { get; set; }
        public string CXBH { get; set; }
        public decimal? SPWYZ { get; set; }
        public string WZSM { get; set; }
        public decimal? QW { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCRID { get; set; }
        public DateTime? DCSJ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public short? SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 变形（数值类）
    /// </summary>
    public class HPF_BTYZTBH_SPWYZ
    {
        public string ID { get; set; }
        public string SPWYID { get; set; }
        public decimal? SPWYZ { get; set; }
        public decimal? SPWYZYCD { get; set; }
        public decimal? GCZX { get; set; }
        public decimal? GCZY { get; set; }
        public decimal? GCZZ { get; set; }
        public string PG { get; set; }
        public DateTime? JCSJ { get; set; }
        public string SFYJ { get; set; }
        public string BZ { get; set; }
        public string DCR { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public short? SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    public class HPF_BTYZTBH_SPWYZP
    {
        public string ID { get; set; }
        public string JLID { get; set; }
        public string MC { get; set; }
        public string TPSJL { get; set; }
        public string TPGS { get; set; }
        public string PSSB { get; set; }
        public string PSSJ { get; set; }
        public DateTime? RKSJ { get; set; }
        public string TPLJ { get; set; }
        public string PSR { get; set; }
    }
    /// <summary>
    /// 糟朽（文档类）
    /// </summary>
    public class HPF_BTYZTBH_ZX
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string GLYCBTID { get; set; }
        public string JCDX { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string BHWZ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
        public string YCDSJID { get; set; }
    }
    /// <summary>
    /// 糟朽记录
    /// </summary>
    public partial class HPF_BTYZTBH_ZXJL
    {
        public string ID { get; set; }
        public string ZXID { get; set; }
        public DateTime? SCJYSJ { get; set; }
        public string GJSCMJ { get; set; }
        public string GJZXMJ { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCRID { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
        public string YCDSJID { get; set; }
    }
    /// <summary>
    /// 白蚁（文档类）
    /// </summary>
    public class HPF_BTYZTBH_BY
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string GLYCBTID { get; set; }
        public string WZSM { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 白蚁记录
    /// </summary>
    public class HPF_BTYZTBH_BYJL
    {
        public string ID { get; set; }
        public string BYID { get; set; }
        public DateTime? SCJYSJ { get; set; }
        public string GJSBYYXDMJ { get; set; }
        public decimal? WD { get; set; }
        public decimal? SD { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCR { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? TJSJ { get; set; }
    }
    /// <summary>
    /// 沉降
    /// </summary>
    public class HPF_BTYZTBH_CJ
    {
        public string YCDSJID { get; set; }
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string JCDX { get; set; }
        public string GLYCBTID { get; set; }
        public string MC { get; set; }
        public string BHWZ { get; set; }
        public string SM { get; set; }
        public string WZSM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 沉降记录
    /// </summary>
    public class HPF_BTYZTBH_CJJL
    {
        public string ID { get; set; }
        public string CJID { get; set; }
        public string CXBH { get; set; }
        public DateTime? CJJCSJ { get; set; }
        public double? CJZ { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCR { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
        public string YCDSJID { get; set; }
    }
    /// <summary>
    /// 沉降数值
    /// </summary>
    public partial class HPF_BTYZTBH_CJSZ
    {
        public string YCDSJID { get; set; }
        public string ID { get; set; }
        public string CXBH { get; set; }
        public string CJID { get; set; }
        public double? CJZ { get; set; }
        public DateTime? JCSJ { get; set; }
        public double? PJZYCD { get; set; }
        public string PG { get; set; }
        public short? SFYJ { get; set; }
        public string DCR { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 钢筋锈蚀
    /// </summary>
    public class HPF_BTYZTBH_GJXS
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string GLYCBTID { get; set; }
        public string JCDX { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string BHWZ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 钢筋锈蚀记录
    /// </summary>
    public class HPF_BTYZTBH_GJXSJL
    {
        public string ID { get; set; }
        public string XSID { get; set; }
        public DateTime? SCJYSJ { get; set; }
        public string GJSCMJ { get; set; }
        public string GJFSMJ { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCRID { get; set; }
        public DateTime? DCSJ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 植物根系
    /// </summary>
    public class HPF_BTYZTBH_ZW
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string ZWZL { get; set; }
        public string GLYCDBTID { get; set; }
        public string WZSM { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 植物记录
    /// </summary>
    public class HPF_BTYZTBH_ZWJL
    {
        public string ID { get; set; }
        public string ZWID { get; set; }
        public string YXMJ { get; set; }
        public string BHLX { get; set; }
        public string SHCD { get; set; }
        public string ZZ { get; set; }
        public string SCL { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCRID { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 渗漏水
    /// </summary>
    public class HPF_BTYZTBH_SL
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string WZSM { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 渗漏记录
    /// </summary>
    public class HPF_BTYZTBH_SLJL
    {
        public string ID { get; set; }
        public string SLID { get; set; }
        public DateTime? SCJYSJ { get; set; }
        public string SLMJ { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCRID { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 脱落
    /// </summary>
    public class HPF_BTYZTBH_TL
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string GLYCBTID { get; set; }
        public string WZSM { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
        public string YCDSJID { get; set; }
    }
    /// <summary>
    /// 脱落记录
    /// </summary>
    public class HPF_BTYZTBH_TLJL
    {
        public string ID { get; set; }
        public string YCDSJID { get; set; }
        public string TLID { get; set; }
        public decimal? TLCD { get; set; }
        public decimal? TLKD { get; set; }
        public decimal? TLMJ { get; set; }
        public string PG { get; set; }
        public string PGSM { get; set; }
        public string DCRID { get; set; }
        public DateTime? DCRQ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 病害控制状态评估
    /// </summary>
    public class HPF_BTYZTBH_BHKZZTPG
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public string BHDID { get; set; }
        public string BHKZZTPG { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 病害分布图
    /// </summary>
    public class HPF_BTYZTBH_BHFBT
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string BHBH { get; set; }
        public DateTime? RKSJ { get; set; }
        public Int64 RKM { get; set; }

    }
    /// <summary>
    /// 病害测项表
    /// </summary>
    public class HPF_BTYZTBH_BHCXB
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public string BHBH { get; set; }
        public string CXBH { get; set; }
        public string CXMC { get; set; }
        public DateTime? JCQSSJ { get; set; }
        public DateTime? JCJSSJ { get; set; }
        public string JCFF { get; set; }
        public string JCCJDBH { get; set; }
        public string JCZQ { get; set; }
        public string SSJG { get; set; }
        public string JCSJKSYFW { get; set; }
        public string JCJLBCDD { get; set; }
        public string JCJLBCSJ { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
        public string BHDCJLID { get; set; }
    }
    /// <summary>
    /// 病害记录表
    /// </summary>
    public class HPF_BTYZTBH_BHJLB
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string BHBH { get; set; }

        public string BHWZ { get; set; }

        public double? BHJD { get; set; }

        public double? BHWD { get; set; }

        public string BHSYT { get; set; }

        public string BHCZZT { get; set; }

        public string BHLX { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHBTGSM { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string BBH { get; set; }

        public DateTime? TJSJ { get; set; }

        public string SHYC { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 病害记录表（通用的）
    /// </summary>
    public class HPF_BTYZTBH_TYJL
    {
        public string ID { get; set; }

        public string BHBH { get; set; }

        public string CXBH { get; set; }

        public string BHLX { get; set; }

        public string CJDBH { get; set; }

        public string BHZL { get; set; }

        public string SJLX { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 病害通用数据表
    /// </summary>
    public class HPF_BTYZTBH_TYSJ
    {
        public string ID { get; set; }

        public string BHJLID { get; set; }

        public string JCX { get; set; }

        public string JCZ { get; set; }

        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 文档类病害通用表
    /// </summary>
    public class HPF_BTYZTBH_WDLBHTYB
    {
        public string ID { get; set; }
        public string BHBH { get; set; }
        public string BHMS { get; set; }
        public string TPLJ { get; set; }
        public string TPMC { get; set; }
        public string TPGS { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
        public string BHDCJLID { get; set; }
    }
    /// <summary>
    /// 文档类病害通用附件
    /// </summary>
    public class HPF_BTYZTBH_WDLBHTYBZP
    {
        public string ID { get; set; }
        public string WDID { get; set; }
        public string TPLJ { get; set; }
        public string TPMC { get; set; }
        public string TPMS { get; set; }
        public string TPSJL { get; set; }
        public string PSSB { get; set; }
        public string TPGS { get; set; }
        public string PZSJ { get; set; }
        public string SJMJ { get; set; }
        public string FBFW { get; set; }
        public string XZCS { get; set; }
    }
    /// <summary>
    /// 照片病害通用表
    /// </summary>
    public class HPF_BTYZTBH_BHTYZP
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string TPLJ { get; set; }
        public string TPMC { get; set; }
        public string TPMS { get; set; }
        public  string TPSJL { get; set; }
        public string PSSB { get; set; }
        public  string TPGS { get; set; }
        public DateTime? PZSJ { get; set; }
        public string PZRID { get; set;}
        public string SJCJBH { get; set; }
        public string CJDZBXX { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHBTGSM { get; set; }
        public string SHZT { get; set; }
        public string SJMJ { get; set; }
        public string FBFW { get; set; }
        public string XZCS { get; set; }

        public string BHBH { get; set; }

        public string TXRID { get; set; }
        public DateTime? TXSJ { get; set; }
        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
        public string BHDCJLID { get; set; }
    }
}

