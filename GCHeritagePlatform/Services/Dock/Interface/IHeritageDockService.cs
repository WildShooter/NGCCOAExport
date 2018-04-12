using GCHeritagePlatform.Services.BaseInfo.Model;
using GCHeritagePlatform.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCHeritagePlatform.Services.PublicMornitor.Interface
{
    /// <summary>
    /// 后台对接服务类 根据遗产地 
    /// </summary>
    interface IHeritageDockService
    {

        string ReceiveData(string jsonStr, string funId, string heritageId);
        string ReceiveFile(byte[] fileInfo, string fileNmae, string businessClassID, string heritageId);


        string UpdateData(string jsonStr, string funId, string heritageId);

        string VerifyBaseData(string jsonStr,string funId, string heritageId);

    }


  

}
