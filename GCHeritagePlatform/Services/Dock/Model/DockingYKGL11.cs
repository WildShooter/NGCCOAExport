using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
   
    public class DockingYKGL11
    {
        
    }

    /// <summary>
    /// 日游客量限制值
    /// </summary>
    public  class HPF_LYYYKGL_RYKLXZZ
    {
        public string ID { get; set; }

        public string LYJDID { get; set; }

        public DateTime? JCRQ { get; set; }

        public int? RYKLXZZ { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string SHYC { get; set; }

        public string YCDSJID { get; set; }
    }

    /// <summary>
    /// 瞬时游客量限制值
    /// </summary>
    public  class HPF_LYYYKGL_SSYKLXZZ
    {
        public string ID { get; set; }

        public string LYJDID { get; set; }

        public DateTime? JCSJ { get; set; }

        public int? SSYKLXZZ { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHRID { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public DateTime? DJSJ { get; set; }

        public string DJRID { get; set; }

        public string SFYDJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string SHYC { get; set; }

        public string YCDSJID { get; set; }
    }

    /// <summary>
    /// 日游客量
    /// </summary>
    public  class HPF_LYYYKGL_RYKL
    {
        public string ID { get; set; }

        public string LYJDID { get; set; }

        public DateTime? JCRQ { get; set; }

        public int? YKL { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }

    /// <summary>
    /// 瞬时游客量
    /// </summary>
    public  class HPF_LYYYKGL_SSYKL
    {
        public string ID { get; set; }

        public string LYJDID { get; set; }

        public DateTime? JCSJ { get; set; }

        public int? YKL { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }

    /// <summary>
    /// 旅游景点
    /// </summary>
    public  class HPF_LYYYKGL_LYJD
    {

        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string MC { get; set; }

        public string SM { get; set; }

        public double? JD { get; set; }

        public double? WD { get; set; }

        public string XQURL { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }
        public string WZSM { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
        public string YCYSBM { get; set; }

    }

    /// <summary>
    /// 旅游景点_客流高峰时段现场照片
    /// </summary>
    public class HPF_LYYYKGL_KLGFSDXCZP
    {
        public string ID { get; set; }

        public string LYJDID { get; set; }

        public DateTime? JCSJ { get; set; }

        public string ZPMC { get; set; }

        public string SM { get; set; }

        public string ZPLJ { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public string TPGS { get; set; }

        public string PZRID { get; set; }

        public DateTime? PZSJ { get; set; }

        public string CJDZBXX { get; set; }

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string XZCS { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }

    /// <summary>
    /// 旅游景点_相关照片
    /// </summary>
    public class HPF_LYYYKGL_LYJDXGZP
    {
        public string ID { get; set; }

        public string LYJDID { get; set; }

        public string MC { get; set; }

        public string SM { get; set; }

        public string LJ { get; set; }

        public short? PX { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SHYC { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string CJDZBXX { get; set; }

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string XZCS { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }

    }
    /// <summary>
    /// 旅游效益年度记录
    /// </summary>
    public class HPF_LYYYKGL_LYXYNDJL
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string ND { get; set; }

        public double? MPSR { get; set; }

        public double? GLBMJYYFWSR { get; set; }

        public int? CSXGGZHJYDJMSL { get; set; }

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

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 游客管理月度记录
    /// </summary>
    public class HPF_LYYYKGL_YKGLYDJL
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string NF { get; set; }

        public string YF { get; set; }

        public int? YKZRCS { get; set; }

        public int? YYYKRCS { get; set; }

        public int? GWYKRCS { get; set; }

        public int? JJYJJCS { get; set; }

        public int? WBDYCS { get; set; }

        public int? SPXDWBDYCS { get; set; }

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

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 游客影响评估
    /// </summary>
    public class HPF_LYYYKGL_YKYXPGJL
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string NF { get; set; }

        public string YF { get; set; }

        public string PG { get; set; }

        public string PGSM { get; set; }

        public string PGR { get; set; }

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

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }

}