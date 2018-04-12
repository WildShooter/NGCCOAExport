using FrameworkCore.DBInterface;
using FrameworkCore.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace GCHeritagePlatform.Utils
{

    /// <summary>
    /// 数据库操作对象池
    /// </summary>
    public class DBHelperPool
    {
       
        public static Dictionary<string, DBToolModel> dbDiction;

        private static DBHelperPool _instance;
        public static DBHelperPool Instance => _instance ?? (_instance = new DBHelperPool());

        public DBHelperPool()
        {
            if(dbDiction==null)
                dbDiction = new Dictionary<string, DBToolModel>();
        }

        public DBToolModel Add(string name,string dbDLLName,string  dbConnString, string providerName)
        {
            DBToolModel dbtool = new DBToolModel( name,  dbDLLName,   dbConnString, providerName);
            IDBHelper dbHelper= InitDb(dbtool);
            if(!dbDiction.ContainsKey(name))
               dbDiction.Add(name,dbtool);
         
            return dbtool;
        }
        public void Remove(string name)
        {
            if (dbDiction.ContainsKey(name))
            {
                dbDiction[name] = null;
                dbDiction.Remove(name);
            }
        }

        /// <summary>
        /// 内部连接数据库
        /// </summary>
        /// <param name="dbtool"></param>
        /// <returns></returns>
        private static IDBHelper InitDb(DBToolModel dbtool)
        {
            if (dbtool == null)
                return null;
            try
            {
                //是否达到队列上限
                if (dbtool.dbHelperQueue.Count == dbtool.queueNum)
                    return dbtool.dbHelperQueue.Dequeue();
                if (dbtool.assembly==null)
                {
                 string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", dbtool.dbDLLPath);
                Assembly assembly = Assembly.LoadFile(filePath);
                //保存
                dbtool.assembly = assembly;
                }
                //连接数据库
                IDBHelper dbHelper = (IDBHelper)dbtool.assembly.CreateInstance(dbtool.providerName);
                dbHelper.initConnectString(dbtool.dbConnString);
                SystemLogger.getLogger().Info(dbtool.name + "数据库初始化成功");
                if (dbHelper != null)
                    dbtool.dbHelperQueue.Enqueue(dbHelper);
                return dbHelper;
            }
            catch (Exception ex)
            {
                SystemLogger.getLogger().Error("反射初始化数据库失败！", ex);
            }
            return null;
        }


        /// <summary>
        /// 打开新数据库
        /// </summary>
        /// <param name="name">数据库Key值</param>
        /// <returns></returns>
        public IDBHelper GetDbHelper(string name= "mySQL")
        {

            if (!dbDiction.ContainsKey(name))
            {
                AddDiction(name);
            }
            DBToolModel dbtool = dbDiction[name];
            IDBHelper dbHelper = InitDb(dbtool);
            return dbHelper;
        }

        public void AddDiction(string conName,string name= "mySQL") {

            string dbDllPath = System.Configuration.ConfigurationManager.AppSettings.Get(name);
            //连接字符串
            ConnectionStringSettings dbConnect = System.Configuration.ConfigurationManager.ConnectionStrings[name];
            string dbConnStr = dbConnect.ConnectionString;
            string provideName = dbConnect.ProviderName;
            //示例化数据库连接池
            DBHelperPool.Instance.Add(conName, dbDllPath, dbConnStr, provideName);
        }

    }
}