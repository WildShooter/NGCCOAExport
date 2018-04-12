using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 建设控制
    /// </summary>
    public class DockingJSKZ9
    {
    }

    /// <summary>
    /// 保护区划图
    /// </summary>
    public class HPF_JSKZ_BHQHT
    {

        public string ID { get; set; }


        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }


        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }


        public string SHBTGSM { get; set; }

        public sbyte? SFKDJ { get; set; }


        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public sbyte? SFYDJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }

        public string BHQHTURL { get; set; }


        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }

    /// <summary>
    /// 保护范围建设控制地带
    /// </summary>
    public class HPF_JSKZ_BHFWJSKZDD
    {

        public string ID { get; set; }


        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }


        public string BHFWJX { get; set; }

        public double? BHFWMJ { get; set; }


        public string BHFWGLGD { get; set; }


        public string JSKZDDJX { get; set; }

        public double? JSKZDDMJ { get; set; }


        public string JSKZDDGLGD { get; set; }


        public string GBDX { get; set; }


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
    /// 遗产区和缓冲区
    /// </summary>
    public class HPF_JSKZ_YCQHCQ
    {
        public  string ID { get; set; }
        public  string GLYCBTID { get; set; }
        public  string YCQBM { get; set; }
        public string YCDSJID { get; set; }
        public string YCQJX { get; set; }
        public string YCQMJ { get; set; }
        public string YCQGLGD { get; set; }
        public string HCQBM { get; set; }
        public string HCQJX { get; set; }
        public string HCQMJ { get; set; }
        public string HCQGLGD { get; set; }
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
    /// 新建项目记录
    /// </summary>
    public class HPF_XJXM_XJXMJL
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string XJXMBH { get; set; }
        public string XMMC { get; set; }
        public string JSMD { get; set; }
        public string WZSM { get; set; }
        public double? JD { get; set; }
        public double? WD { get; set; }
        public DateTime? KGRQ { get; set; }
        public DateTime? JHJGRQ { get; set; }
        public DateTime? JGSJ { get; set; }
        public string WWBMPZXKWH { get; set; }
        public string XMSM { get; set; }
        public double? ZDMJ { get; set; }
        public double? GD { get; set; }
        public string XGYCBTID { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public sbyte? SHZT { get; set; }
        public string SGDW { get; set; }
        public string JLDW { get; set; }
        public string SHBTGSM { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public sbyte? SFYDJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
        public string YCDSJID { get; set; }
    }

    public class V_HPF_XJXM_XJXMJL
    {
        public string ID { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public sbyte? SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public sbyte? SFKDJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public sbyte? SFYDJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public string XJXMBH { get; set; }
        public string XMMC { get; set; }
        public string JSMD { get; set; }
        public string JSDD { get; set; }
        public Int64 KGN { get; set; }//YEAR

        public Int64 KGM { get; set; }//MONTH
        public DateTime? KGRQ { get; set; }
        public DateTime? JGSJ { get; set; }
        public string WWBMPZXKWH { get; set; }
        public double? ZDMJ { get; set; }
        public double? GD { get; set; }
        public string XMJSWZTLJ { get; set; }
        public string XJXMGCFA { get; set; }
        public string SFAZJSXKDFAJX { get; set; }
        public DateTime? RKSJ { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public string SFWJ { get; set; }
    }
    /// <summary>
    /// 建设项目_施工现场环境照片
    /// </summary>
    public class HPF_XJXM_SGXCHJZP
    {
        public string ID { get; set; }
        public string XJXMID { get; set; }
        public string MC { get; set; }
        public string SM { get; set; }
        public string LJ { get; set; }
        public DateTime? PZSJ { get; set; }
        public string PZRID { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public sbyte? SHZT { get; set; }
        public string SHBTGSM { get; set; }
        public string CJDZBXX { get; set; }
        public string SJMJ { get; set; }
        public string FBFW { get; set; }
        public string XZCS { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public sbyte? SFYDJ { get; set; }
        public string SHYC { get; set; }
        public string SCRID { get; set; }
        public DateTime? SCSJ { get; set; }
        public string YCDSJID { get; set; }
    }
}