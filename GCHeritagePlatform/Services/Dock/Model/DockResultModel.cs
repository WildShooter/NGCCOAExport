using System;
using System.Collections.Generic;
using GCHeritagePlatform.Services.PublicMornitor.Model;

namespace GCHeritagePlatform.Services.Dock.Model
{



    /// <summary>
    /// 定义对接返回对象格式
    /// </summary>
    ///
    /// </summary>
    /// 数据主表+明细表+名字子表+附件
    ///  </summary>
    public class DockResultMulti<T1, T2, T3> where T1 : class where T2 : class where T3 : class
    {
        public List<T1> DATA { set; get; }
        public List<T2> DATADETAIL { set; get; }
        public List<T3> DATADETAILSON { set; get; }
        public List<FileInfoEx> FILEPATHLIST { set; get; }
    }

    /// </summary>
    /// 数据主表+明细表+附件
    ///  </summary>
    public class DockResultDataDataDetailModel<T1, T2> where T1 : class where T2 : class
    {
        public List<T1> DATA { set; get; }
        /// <summary>
        /// 返回结果数据的关联子表
        /// </summary>
        public List<T2> DATADETAIL { set; get; }
        public List<FileInfoEx> FILEPATHLIST { set; get; }
    }
    /// </summary>
    /// 数据主表+附件
    ///  </summary>
    public class DockResultDataFileModel<T1> where T1 : class
    {
        public List<T1> DATA { get; set; }
        public List<FileInfoEx> FILEPATHLIST { get; set; }
    }
    public class DockResultDataFileModelEx<T1> where T1 : class
    {
        public List<T1> DATA { get; set; }
        public List<FileInfoSignEx> FILEPATHLIST { get; set; }
    }
    public class DockResultModel
    {
        /// <summary>
        /// 返回结果数据表
        /// </summary>
        public Object DATA { set; get; }
        /// <summary>
        /// 返回结果数据的关联子表
        /// </summary>
        public Object DATADETAIL { set; get; }

        public List<string> FILEPATHLIST { set; get; }
        public DockResultModel() { }
        /// <summary>
        /// 构造函数    
        /// </summary>
        /// <param name="ycdid">遗产地ID</param>
        /// <param name="ycdsjid">遗产地数据ID</param>
        /// <param name="resultvalue">返回结果对象</param>
        public DockResultModel(string ycdid,  Object data)
        {
            DATA = data;
            FILEPATHLIST = new List<string>();
        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="ycdid">遗产地ID</param>
        /// <param name="data">返回结果对象</param>
        /// <param name="datadetail">返回结果对象关联的子表</param>
        public DockResultModel(string ycdid,Object data,Object datadetail)
        {
            DATA = data;
            DATADETAIL = datadetail;
            FILEPATHLIST = new List<string>();
        }
    }

    public class FileDoc
    {
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Remark { get; set; }
        public string Version { get; set; }

    }

    public class ResultBHGCDockModel
    {
        public List<HPF_BHGC> DATA { get; set; }
        public List<HPF_BHGC_BHZSHHJZZGC_XMFWT> DATADETAIL { get; set; }
        public List<HPF_BHGC_XGWD> DATADETAILSON { get; set; }
        public List<FileInfoEx> FILEPATHLIST { get; set; }
    }

    public class ResultYCYSDT1DockModel
    {
        public List<HPF_YCYSDT_YCYSDTHJBXZT> DATA { get; set; }
        
        //public string DataDetail { get; set; }
        public List<FileInfoEx> FILEPATHLIST { get; set; }
    }

    public class ResultYCYSDT2DockModel
    {
        public List<HPF_YCYSDT_YCYSDTHJBTP> DATA { get; set; }
        //public string DataDetail { get; set; }
        public List<FileInfoEx> FILEPATHLIST { get; set; }
    }

    public class ResultYCJCXX4DockModel
    {
        public List<HPF_YCJCXX_YCYSDTHJBCHJZT> DATA { get; set; }
        public List<HPF_YCJCXX_YCYSDTHJBTP> DATADETAIL { get; set; }
    }

    public class ResultBHGC_XCZPDockModel
    {
        public List<HPF_BHGC_BHZSHHJZZGC_XCZP> DATA { get; set; }
        //public string DataDetail { get; set; }
        public List<FileInfoEx> FILEPATHLIST { get; set; }
    }
}