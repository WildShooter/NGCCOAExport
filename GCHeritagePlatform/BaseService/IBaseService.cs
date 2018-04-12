using System.Collections.Generic;

namespace GCHeritagePlatform.Services
{
    interface IService { }
    interface IBaseService<T>: IService where T : class
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Add(T t);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(T t);

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        bool Delete(string id);

        /// <summary>
        /// 删除通过sql
        /// </summary>
        /// <param name="deleteSql"></param>
        /// <returns></returns>
        bool DeleteBySql(string deleteSql);
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete(IList<string> ids);

    }
}
