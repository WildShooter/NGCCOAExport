using GCHeritagePlatform.Utils;
using System;
using System.Collections.Generic;

namespace GCHeritagePlatform.Services
{
    /// <summary>
    /// 业务实体的增删改查基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T>: IBaseService<T> where T : class
    {
        public BaseService(){
            this.PrimaryKey = "ID";
        }
        /// <summary>
        /// 表名属性
        /// </summary>
        public string DataTableName { get; set; }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string PrimaryKey { get; set;}
        private string deleteSqlTemplate = " delete from {0} where  {1}";
        private string selectSqlTemplate = " select {0} from {1} where {2}";
        /// <summary>
        /// 根据不增加的字段进行插入,遇到不增加的字段进行跳过
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public virtual bool Add(T ent)
         {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var nameToValue = ent.GetNameToValueDic("NoAddField");
            if (!nameToValue.ContainsKey("ID"))
            {
                nameToValue.Add("ID", "");
            }
            if (string.IsNullOrEmpty(nameToValue["ID"]+""))
            {
                var gid = Guid.NewGuid();
                nameToValue["ID"] = gid;
            }
            var sql= dbContext.insertByParamsReturnSQL(DataTableName, nameToValue);
            return dbContext.execute(sql)>0;
        }
        /// <summary>
        /// 修改业务实体
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public virtual bool Update(T ent)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var nameToValue = ent.GetNameToValueDic("NoUpdateField");
            var sql = dbContext.updateByParamsReturnSQL(DataTableName, nameToValue);
            var i = dbContext.execute(sql);
            return i > 0;
        }
        /// <summary>
        /// 通用删除多ID的实现
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual bool Delete(IList<string> ids)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var filter = $" {PrimaryKey} in ({ids.ChangeListToString()}) ";
            var sql = string.Format(deleteSqlTemplate, DataTableName, filter);
            var i = dbContext.execute(sql);
            return i > 0;
        }
        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(string id)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var filter = $" {PrimaryKey} = '{id}' ";
            var sql = string.Format(deleteSqlTemplate, DataTableName, filter);
            var i = dbContext.execute(sql);
            return i > 0;
        }
       /// <summary>
       /// 删除 通过SQL语句
       /// </summary>
       /// <param name="deleteSql"></param>
       /// <returns></returns>
        public bool DeleteBySql(string deleteSql)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var filter = $" {PrimaryKey} in '{deleteSql}' ";
            var sql = string.Format(deleteSqlTemplate, DataTableName, filter);
            var i = dbContext.execute(sql);
            return i > 0;
        }
    }
}