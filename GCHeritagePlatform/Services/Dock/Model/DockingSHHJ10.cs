using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    /// <summary>
    /// 土地利用现状图
    /// </summary>
    public class HPF_SHHJ_TDLYXZT
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public string TDLYXZTURL { get; set; }
        public string TDLYXZ { get; set; }
        public string TZLJ { get; set; }
        public string TZMC { get; set; }
        public string TZLX { get; set; }
        public string TZSJL { get; set; }
        public string YLT { get; set; }
        public string SFLMT { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? DJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
   
    public class V_HPF_SHHJ_TDLYXZT
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public string TDLYXZTURL { get; set; }
        public string TDLYXZ { get; set; }
        public string TZLJ { get; set; }
        public string TZMC { get; set; }
        public string TZLX { get; set; }
        public string TZSJL { get; set; }
        public string YLT { get; set; }
        public string SFLMT { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? DJSJ { get; set; }
        public string DJRID { get; set; }
        public Int64 RKN { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 土地利用规划图
    /// </summary>
    public class HPF_SHHJ_TDLYGHT
    {
        public string ID { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
        public string TDLYGHTURL { get; set; }
        public string TZLJ { get; set; }
        public string TZMC { get; set; }
        public string TZLX { get; set; }
        public string TZSJL { get; set; }
        public string YLT { get; set; }
        public string SFLMT { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? DJSJ { get; set; }
        public string DJRID { get; set; }
        public DateTime? RKSJ { get; set; }
    }
    /// <summary>
    /// 遗产区和缓冲区社会环境年度监测记录
    /// </summary>
    public class HPF_SHHJ_YCQHHCQSHHJNDJCJL
    {
        public string ID { get; set; }
        public int ZYKCDSL { get; set; }
        public int FMYXFW { get; set; }
        public int YZWRGYQYSL { get; set; }
        public string LRMLSDYCQRKSL { get; set; }
        public string DQYCQRKSL { get; set; }
        public string RKSSXQ { get; set; }
        public string LRMLSDHCQRK { get; set; }
        public string DQHCQRKSL { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
    }
    /// <summary>
    /// 遗产所在地社会环境年度监测记录
    /// </summary>
    public class HPF_SHHJ_YCSZDSHHJNDJCJL
    {
        public string ID { get; set; }
        public string RKMD { get; set; }
        public decimal? RJGDP { get; set; }
        public string GJBHDZWZL { get; set; }
        public decimal? ZBFGL { get; set; }
        public string YCSZDYZWRGYQYSL { get; set; }
        public string CJRID { get; set; }
        public DateTime? CJSJ { get; set; }
        public string SHRID { get; set; }
        public DateTime? SHSJ { get; set; }
        public string SHZT { get; set; }
        public string DJRID { get; set; }
        public DateTime? DJSJ { get; set; }
        public string SHYC { get; set; }
        public DateTime? TJSJ { get; set; }
        public DateTime? RKSJ { get; set; }
        public string GLYCBTID { get; set; }
        public string YCDSJID { get; set; }
    }
}