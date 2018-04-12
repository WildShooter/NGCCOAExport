﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    public class DockingJCXX3
    {
        
    }

    /// <summary>
    /// 遗产总图
    /// </summary>
    public class HPF_YCJCXX_YCZT
    {
        public string ID { get; set; }

        public string YCZTURL { get; set; }

        public string JCXXWSCD { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string SFWLMT { get; set; }

        public string BB { get; set; }

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

    public class V_HPF_YCJCXX_YCZT
    {
        public string ID { get; set; }
        public Int64  RKN { get; set; }
        public string YCZTURL { get; set; }

        public string JCXXWSCD { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string SFWLMT { get; set; }

        public string BB { get; set; }

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
    /// 遗产要素分布图
    /// </summary>
    public class HPF_YCJCXX_YCYSFBT
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string YCYSFBTURL { get; set; }

        public string SFWLMT { get; set; }

        public string BB { get; set; }

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
    /// 使用功能基准图
    /// </summary>
    public class HPF_YCJCXX_SYGNJZT
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string SYGNJZTURL { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? RKSJ { get; set; }

        public decimal? OnlyID { get; set; }

        public string ZWMC { get; set; }

        public string GNLX { get; set; }

        public string Health { get; set; }

        public string PG { get; set; }

        public string QKSM { get; set; }
    }

    /// <summary>
    /// 遗产要素单体或局部测绘基准图
    /// </summary>
    public class HPF_YCJCXX_YCYSDTHJBCHJZT
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string TZBH { get; set; }

        public string TZLJ { get; set; }

        public string TZMC { get; set; }

        public string TZLX { get; set; }

        public string TZSJL { get; set; }

        public string YLT { get; set; }

        public string BLC { get; set; }

        public string CHR { get; set; }

        public string MJ { get; set; }

        public string CJR { get; set; }

        public DateTime? TZCJSJ { get; set; }

        public string GLZRDW { get; set; }

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
    /// 遗产要素单体或局部图片
    /// </summary>
    public class HPF_YCJCXX_YCYSDTHJBTP
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string YCDSJID { get; set; }

        public string TPLJ { get; set; }

        public string TPMC { get; set; }

        public string TPLX { get; set; }

        public string TPSJL { get; set; }

        public string YLT { get; set; }

        public string PSSB { get; set; }

        public string MJ { get; set; }

        public string CJR { get; set; }

        public DateTime? TPCJSJ { get; set; }

        public string GLZRDW { get; set; }

        public string CJRID { get; set; }

        public DateTime? CJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? RKSJ { get; set; }
    }
}