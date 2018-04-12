using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PublicMornitor.Model
{
    public class DockingBHZSYHJZZ15
    {
    }

    /// <summary>
    /// 保护工程
    /// </summary>
    public class HPF_BHGC
    {

        public string ID { get; set; }

        public string GCBH { get; set; }

        public string GCMC { get; set; }

        public string GCFL { get; set; }

        public string GLYCBTID { get; set; }

        public string SJDDYCGCYS { get; set; }

        public string LXPFWJ { get; set; }

        public string LXBG { get; set; }

        public string FAPFWJ { get; set; }

        public DateTime? FAKSSJ { get; set; }

        public string FFBZDW { get; set; }

        public string FFPFNY { get; set; }

        public string FA { get; set; }

        public string FASJGJBZJF { get; set; }

        public string FASJDFPTJF { get; set; }

        public string BTYJQTWJ { get; set; }

        public string SFSBSJYCZX { get; set; }

        public string BHGCJZQK { get; set; }

        public string SGDW { get; set; }

        public string JLDW { get; set; }

        public string JLBG { get; set; }

        public DateTime? KGSJ { get; set; }

        public DateTime? JGSJ { get; set; }

        public string JGBG { get; set; }

        public string QTZL { get; set; }

        public string BHGCGJBZJF { get; set; }

        public string BHGCDFPTJF { get; set; }

        public DateTime? GJWWJYSRQ { get; set; }

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

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }
        public string GJTRZJF { get; set; }
        public string BTBHZSFL { get; set; }
    }

    /// <summary>
    /// 保护展示和环境整治工程_现场照片
    /// </summary>
    public class HPF_BHGC_BHZSHHJZZGC_XCZP
    {
        public string ID { get; set; }

        public string GCXMID { get; set; }

        public string TPMC { get; set; }

        public string LJ { get; set; }

        public string PZRID { get; set; }

        public DateTime? PZSJ { get; set; }

        public string TPGS { get; set; }

        public string CJDZBXX { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string XZCS { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SFYDJ { get; set; }

        public string SHYC { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }

        public virtual HPF_BHGC HPF_BHGC { get; set; }
    }

    /// <summary>
    /// 保护展示和环境整治工程_项目范围图
    /// </summary>
    public class HPF_BHGC_BHZSHHJZZGC_XMFWT
    {
        public string ID { get; set; }

        public string GLYCBTID { get; set; }

        public string GCXMID { get; set; }

        public string TZMC { get; set; }

        public string TZNRLX { get; set; }

        public string TZGS { get; set; }

        public string BLC { get; set; }

        public string CHRID { get; set; }

        public DateTime? CTSJ { get; set; }

        public string DJRID { get; set; }

        public DateTime? DJSJ { get; set; }

        public string SHRID { get; set; }

        public DateTime? SHSJ { get; set; }

        public string SHZT { get; set; }

        public string SHBTGSM { get; set; }

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string XZCS { get; set; }

        public string LJ { get; set; }

        public string SFYDJ { get; set; }

        public string SHYC { get; set; }

        public string SCRID { get; set; }

        public DateTime? SCSJ { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }

        public virtual HPF_BHGC HPF_BHGC { get; set; }
    }

    /// <summary>
    /// 保护工程_相关文档
    /// </summary>
    public class HPF_BHGC_XGWD
    {
        public string ID { get; set; }

        public string GCXMID { get; set; }

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

        public string SJMJ { get; set; }

        public string FBFW { get; set; }

        public string SFYDJ { get; set; }

        public string SHYC { get; set; }

        public DateTime? RKSJ { get; set; }

        public string YCDSJID { get; set; }

        public virtual HPF_BHGC HPF_BHGC { get; set; }
    }

    /// <summary>
    /// 用于鼓浪屿与总平台对接中的“保护展示与环境整治工程记录”
    /// </summary>
    public class GLY_HPF_BHZSHHJZZGCJL
    {
        //序号
        public string ID { get; set; }

        //项目名称
        public string GCMC { get; set; }

        //项目分类
        public string XMFL { get; set; }

        //工程分类
        public string GCFL { get; set; }

        //文物部门批准/许可文号
        public string WWBMPZXKWH { get; set; }

        //经费投入总额
        public string JFTRZE { get; set; }

        //中央财政经费
        public string BHGCGJBZJF { get; set; }

        //保护展示与环境工程档案
        public string BHZSYHJGC { get; set; }

        //对接状态
        public string SFYDJ { get; set; }

        //开始时间
        public DateTime? KGSJ { get; set; }

        //结束时间
        public DateTime? JGSJ { get; set; }

    }
}