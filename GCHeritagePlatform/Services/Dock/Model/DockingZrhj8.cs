using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 自然环境监测工作情况记录
    /// </summary>
    public  class HPF_ZRHJ_ZRHJJCGZQKJL
    {
        public string ID { get; set; }

        public string ZRHJJCXMBH { get; set; }

        public string ZRHJJCXMNR { get; set; }

        public string JCDXLX { get; set; }

        public DateTime? JCQSSJ { get; set; }

        public DateTime? JCJSSJ { get; set; }

        public string JCFF { get; set; }


        public string SJCJDBH { get; set; }


        public string JCZQ { get; set; }


        public string SSJG { get; set; }

        public string JCSJKSYFW { get; set; }


        public string JCJLBCDD { get; set; }

        public string JCJLBCSJ { get; set; }


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

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境实时天气
    /// </summary>
    public  class HPF_ZRHJ_SSTQ
    {
        public string ID { get; set; }

        public DateTime? FBSJ { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string DQQW { get; set; }

        public string JYL_1H { get; set; }

        public string FX { get; set; }

        public string FXMS { get; set; }

        public string FS { get; set; }

        public string DQSD { get; set; }

        public string QY { get; set; }

        public string NJD { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public decimal? SHZT { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境天气预警信息
    /// </summary>
    public  class HPF_ZRHJ_TQYJXX
    {
        public string ID { get; set; }
        public DateTime? FBSJ { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string YJXHM { get; set; }

        public string BZ { get; set; }

        public string FBBT { get; set; }

        public string FBNR { get; set; }

        public string FBBM { get; set; }

        public string FBRY { get; set; }

        public string FYZN { get; set; }

        public string SFJCYJ { get; set; }

        public string YJLX { get; set; }

        public string YJTPLJ { get; set; }

        public string YJHY { get; set; }

        public string YJXHMS { get; set; }

        public string YJDJ { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public decimal? SHZT { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境大气质量日报
    /// </summary>
    public  class HPF_ZRHJ_DQZLRB
    {
        public string ID { get; set; }

        public DateTime? FBSJ { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string SO2_24H_PJND { get; set; }

        public string SO2_24H_PJFZS { get; set; }

        public string NO2_24H_PJND { get; set; }

        public string NO2_24H_PJFZS { get; set; }

        public string KLW10_24H_PJND { get; set; }

        public string KLW10_24H_PJFZS { get; set; }

        public string CO_24H_PJND { get; set; }

        public string CO_24H_PJFZS { get; set; }

        public string O3_1H_PJND { get; set; }

        public string O3_1H_PJFZS { get; set; }

        public string O3_8H_HDPJND { get; set; }

        public string O3_8H_HDPJFZS { get; set; }

        public string KLW2D5_24H_PJND { get; set; }

        public string KLW2D5_24H_PJFZS { get; set; }

        public string AQI { get; set; }

        public string SYWRW { get; set; }

        public string AQI_JB { get; set; }

        public string AQI_LB { get; set; }

        public string AQI_LBYS { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public decimal? SHZT { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境大气质量时报
    /// </summary>
    public  class HPF_ZRHJ_DQZLSB
    {
        public string ID { get; set; }

        public DateTime? FBSJ { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string SO2_1H_PJND { get; set; }
        public string SO2_1H_PJFZS { get; set; }

        public string NO2_1H_PJND { get; set; }

        public string NO2_1H_PJFZS { get; set; }

        public string KLW10_1H_PJND { get; set; }

        public string KLW10_1H_PJFZS { get; set; }

        public string CO_1H_PJND { get; set; }

        public string CO_1H_PJFZS { get; set; }

        public string O3_1H_PJND { get; set; }

        public string O3_1H_PJFZS { get; set; }

        public string O3_8H_HDPJND { get; set; }

        public string O3_8H_HDPJFZS { get; set; }

        public string KLW2D5_1H_PJND { get; set; }

        public string KLW2D5_1H_PJFZS { get; set; }

        public string AQI { get; set; }

        public string SYWRW { get; set; }

        public string AQI_JB { get; set; }

        public string AQI_LB { get; set; }

        public string AQI_LBYS { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public decimal? SHZT { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境酸雨元数据
    /// </summary>
    public  class HPF_ZRHJ_SYYSJ
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string CYDMC { get; set; }

        public DateTime? CYKSSJ { get; set; }

        public DateTime? CYZZSJ { get; set; }

        public string JSLX { get; set; }

        public float? JYL { get; set; }

        public float? pH { get; set; }

        public float? DDL { get; set; }

        public float? LSYLZ { get; set; }

        public float? XSGLZ { get; set; }

        public float? FLZ { get; set; }

        public float? LLZ { get; set; }

        public float? ALZ { get; set; }

        public float? GLZ { get; set; }

        public float? MLZ { get; set; }

        public float? NLZ { get; set; }

        public float? JLZ { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境海水质量检测
    /// </summary>
    public  class HPF_ZRHJ_HSZLJC
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string JCZW { get; set; }

        public float? JHJD { get; set; }

        public float? JHWD { get; set; }

        public float? SCJD { get; set; }

        public float? SCWD { get; set; }

        public DateTime? JCSJ { get; set; }

        public double? SS { get; set; }

        public string CYCC { get; set; }

        public double? CYSD { get; set; }

        public double? pH { get; set; }

        public double? YD { get; set; }

        public double? RJY { get; set; }

        public double? HXXYL { get; set; }

        public double? LSY { get; set; }

        public double? YXSYD { get; set; }

        public double? XSYD { get; set; }

        public double? AD { get; set; }

        public double? SYL { get; set; }

        public double? YLSA { get; set; }

        public double? Tong { get; set; }

        public double? Xin { get; set; }

        public double? Ge { get; set; }

        public double? Gong { get; set; }

        public double? G { get; set; }

        public double? Qian { get; set; }

        public double? Shen { get; set; }

        public double? ZD { get; set; }

        public double? ZL { get; set; }

        public double? XFW { get; set; }

        public double? GSY { get; set; }

        public double? LHW { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境地震数据
    /// </summary>
    public  class HPF_ZRHJ_DZ
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public DateTime? DZSK { get; set; }

        public string BTMS { get; set; }

        public string ZJ { get; set; }

        public string SD { get; set; }

        public string JD { get; set; }

        public string WD { get; set; }

        public string WZMS { get; set; }

        public string YXFW { get; set; }

        public string SS { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public short? SHZT { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }

        public string YCDSJID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境台风信息
    /// </summary>
    public  class HPF_ZRHJ_TF
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string TFDM { get; set; }

        public string ZWMC { get; set; }

        public string YWMC { get; set; }

        public string FSNF { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public int? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }
        public DateTime? RKSJ { get; set; }

    }
    /// <summary>
    /// 自然环境台风路径信息
    /// </summary>
    public  class HPF_ZRHJ_TFLJXX
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string PID { get; set; }

        public string FSNF { get; set; }

        public DateTime? FSSJ { get; set; }

        public string TFLX { get; set; }

        public double? JD { get; set; }

        public double? WD { get; set; }

        public int? FL { get; set; }

        public int? FS { get; set; }

        public int? QY { get; set; }

        public int? YDSD { get; set; }

        public string YDFX { get; set; }

        public double? FQ_7 { get; set; }

        public double? FQ_10 { get; set; }

        public double? FQ_12 { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public sbyte? SFKDJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public sbyte? SFYDJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }
        public DateTime? RKSJ { get; set; }

    }
    /// <summary>
    /// 自然环境台风预估点信息
    /// </summary>
    public  class HPF_ZRHJ_TFYGDXX
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string PID { get; set; }

        public string FSNF { get; set; }

        public DateTime? FSSJ { get; set; }

        public string TFLX { get; set; }

        public double? JD { get; set; }

        public double? WD { get; set; }

        public int? FL { get; set; }

        public int? FS { get; set; }

        public int? QY { get; set; }

        public int? YDSD { get; set; }

        public string YDFX { get; set; }

        public double? FQ_7 { get; set; }

        public double? FQ_10 { get; set; }

        public double? FQ_12 { get; set; }

        public DateTime? FBSJ { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public sbyte? SFKDJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public sbyte? SFYDJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }
        public DateTime? RKSJ { get; set; }

    }
    /// <summary>
    /// 自然环境Cl离子、硫酸根离子
    /// </summary>
    public  class HPF_ZRHJ_LLZLSGLZ
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? SJ { get; set; }

        public double? CLLZND { get; set; }

        public double? LSGLZND { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SFSHTG { get; set; }

        public string SHSM { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境基本微环境
    /// </summary>
    public class HPF_ZRHJ_WQXHJD
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string JCD { get; set; }

        public decimal? WD { get; set; }

        public decimal? XDSD { get; set; }

        public decimal? LD { get; set; }

        public decimal? QY { get; set; }

        public decimal LFZFS { get; set; }

        public decimal? SFZFS { get; set; }

        public decimal SSZFS { get; set; }

        public decimal? SSSFS { get; set; }

        public decimal? SSZJFS { get; set; }

        public decimal? SSFFS { get; set; }

        public decimal? SSJFS { get; set; }

        public decimal? SSZWFS { get; set; }

        public decimal? LJZFS { get; set; }

        public decimal? LJSFS { get; set; }

        public decimal? LJZJFS { get; set; }

        public decimal? LJFSJ { get; set; }

        public decimal? LJJFS { get; set; }

        public decimal? LJZWFS { get; set; }

        public decimal? RZS { get; set; }

        public decimal? FS { get; set; }

        public decimal? FX { get; set; }

        public decimal? FL { get; set; }

        public decimal? JSL { get; set; }

        public DateTime? JCSJ { get; set; }

        public string WZSM { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境噪声观测点
    /// </summary>
    public class HPF_ZRHJ_ZSGCD
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public DateTime? SJ { get; set; }

        public double? ZS { get; set; }

        public decimal? FB { get; set; }

        public string ZSZL { get; set; }

        public decimal? PL { get; set; }

        public string JCSB { get; set; }

        public string JCD { get; set; }

        public string WZSM { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public sbyte? SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? TJSJ { get; set; }

        public string YCDSJID { get; set; }

        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 环境影响评估
    /// </summary>
    public class HPF_ZRHJ_HJYXPG
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public string PG { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public string TJSJ { get; set; }
        public string PGSM { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 自然环境受灾记录
    /// </summary>
    public  class HPF_ZRHJ_ZRZHSZJL
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public DateTime? FSSJ { get; set; }

        public string SZLX { get; set; }

        public string ZRHJJCJLBH { get; set; }

        public string WWZSCD { get; set; }

        public string SZQKMS { get; set; }
        public string ZQCQDFFCS { get; set; }

        public decimal? JZJFTRZE { get; set; }

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

    public class V_HPF_ZRHJ_ZRZHSZJL
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }
        public Int64 FSN { get; set; }
        public DateTime? FSSJ { get; set; }

        public string SZLX { get; set; }

        public string ZRHJJCJLBH { get; set; }

        public string WWZSCD { get; set; }

        public string SZQKMS { get; set; }
        public string ZQCQDFFCS { get; set; }

        public decimal? JZJFTRZE { get; set; }

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
}