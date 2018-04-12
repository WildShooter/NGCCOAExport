using GCHeritagePlatform.Models;
using GCHeritagePlatform.Utils;
using System.Data;

namespace GCHeritagePlatform.Services
{
    public  class CommonBaseBusiness : ICommonBusiness
    {
        public CommonBaseBusiness() { }
        public CommonBaseBusiness(string tableName) { this.TableName = tableName; }
        public CommonBaseBusiness(string tableName,string heritageSiteID) { this.TableName = tableName; this.HeritageSiteID = heritageSiteID; }
        public string TableName { get; set; }
        /// <summary>
        /// 遗产地ID 
        /// </summary>
        public string HeritageSiteID { get; set; }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="mainId">主键ID</param>
        /// <returns>Json表</returns>
        [HproseAttribute("{0}GetDetailByMainID(string mainId)","{0}获取详情")]
        public virtual string GetDetailByMainID(string mainId)
        {
            string sql = $"select * from {TableName} where id='{mainId}'";
            var context = DBHelperPool.Instance.GetDbHelper();
            if(context==null)return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var datatable = context.getDataTableResult(sql);
            return JsonHelper.SerializeObject(ToolResult.Success(datatable));
        }
        public virtual string GetDetailByMainID(string mainId,string cols,string keyField= "id")
        {
            string sql =
                $"select {(string.IsNullOrEmpty(cols) ? "*" : cols)} from {TableName} where {keyField}='{mainId}'";
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var datatable = context.getDataTableResult(sql);
            return JsonHelper.SerializeObject(ToolResult.Success(datatable));
        }

        public virtual DataTable GetDetail( string cols, string mainId, string keyField = "id")
        {
            
            string sql =
                $"select {(string.IsNullOrEmpty(cols) ? "*" : cols)} from {TableName} where {keyField}='{mainId}'";
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null) return null;
            return context.getDataTableResult(sql);
        }

        public virtual DataTable GetDetail202(string cols, string mainId, string keyField = "z.GZID")
        {

            string sql =
                $"select {(string.IsNullOrEmpty(cols) ? "*" : cols)} from {TableName} where {keyField}='{mainId}'";
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null) return null;
            return context.getDataTableResult(sql);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cols">字段集</param>
        /// <param name="where">过滤条件</param>
        /// <param name="orderBy">排序依据</param>
        /// <param name="pageSize">每页最大记录数</param>
        /// <param name="pageIndex">页索引（从0起）</param>
        /// <param name="bReturnSum">是否返回记录数</param>
        /// <returns>Json表</returns>
        [HproseAttribute("{0}SelectByWhere(string cols, string where, string orderBy," +
                         " int pageSize, int pageIndex, bool bReturnSum)", "{0}条件选择")]
        public virtual string SelectByWhere(string cols, string where, 
            string orderBy, int pageSize, int pageIndex, bool bReturnSum)
        {
            var sqlTemplate = "select {0} from {1}  where 1=1 {2} {3} limit {4},{5}";
            var whereStr = string.IsNullOrEmpty(where) ? "" : where; //需要加入遗产地的默认条件
            var orderByStr = string.IsNullOrEmpty(orderBy) ? "" : " order by " + orderBy;

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            var limit = (pageIndex - 1) * pageSize;
            var sql = string.Format(sqlTemplate, string.IsNullOrEmpty(cols) ? "*" : 
                cols, TableName, whereStr,orderByStr, limit, pageSize);
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            DataTable datatable = context.getDataTableResult(sql);
            if (!bReturnSum)
                return JsonHelper.SerializeObject(ToolResult.Success(datatable));
            var sqlSum= $"select count(*) from {GetModelName(TableName)}  where 1=1 {whereStr} {orderByStr}";
            int count = 0;
            int.TryParse(context.executeScalar(sqlSum).ToString(),out count);
            return JsonHelper.SerializeObject(ToolResult.Success(new { data=datatable,sum=count}));
        }

        public virtual PageModel Select(string cols, string where, string orderBy, int pageSize, int pageIndex, bool bReturnSum)
        {
          
            var sqlTemplate = "select {0} from {1}  where 1=1 {2} {3} ";
            var whereStr = string.IsNullOrEmpty(where) ? "" : where; //需要加入遗产地的默认条件
            var orderByStr = string.IsNullOrEmpty(orderBy) ? "" : " order by " + orderBy;
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            var limit = (pageIndex - 1) * pageSize;
            var sql = string.Format(sqlTemplate, string.IsNullOrEmpty(cols) ? "*" : cols, TableName, whereStr, orderByStr);
            return Select(sql, limit, pageSize);
        }
        public virtual PageModel Select(string cols, string where,string groupby, string orderBy, int pageIndex, int pageSize, bool bReturnSum)
        {
            var sqlTemplate = "select {0} from {1}  where 1=1 {2} {3} {4} ";
            var whereStr = string.IsNullOrEmpty(where) ? "" : where; //需要加入遗产地的默认条件
            var groupbyStr = string.IsNullOrEmpty(groupby) ? "" : " group by " + groupby;
            var orderByStr = string.IsNullOrEmpty(orderBy) ? "" : " order by " + orderBy;
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            var limit = (pageIndex - 1) * pageSize;
            var sql = string.Format(sqlTemplate, string.IsNullOrEmpty(cols) ? "*" : cols, TableName, whereStr, groupbyStr,orderByStr);
            return Select(sql, limit, pageSize);
        }
        public  DataTable  Select(string cols, string where)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var sqlTemplate = "select {0} from {1}  where 1=1 {2}";
            var whereStr = string.IsNullOrEmpty(where) ? "" : where; //需要加入遗产地的默认条件
            var sql = string.Format(sqlTemplate, string.IsNullOrEmpty(cols) ? "*" : cols, TableName, whereStr);
            DataTable dt = dbContext.getDataTableResult(sql);
            return dt;
        }

        public virtual PageModel Select(string tableSql, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            DataTable datatable = null;
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null) return new PageModel() { Data = null, Total = 0 };
            var sql = $"{tableSql} limit {pageIndex},{pageSize}";
            datatable = context.getDataTableResult(sql);
            var sqlSum = $"select count(*) from ({tableSql}) a ";
            int count = 0;
            int.TryParse(context.executeScalar(sqlSum).ToString(), out count);
            return new PageModel() { Data = datatable, Total = count };
        }

        private string GetModelName(string viewName)
        {   //v_HPF_ZRHJ_TF->HPF_ZRHJ_TF
            //有的时候在XML中建的是视图,但是我们是往表中插入数据,所以要把传进来的视图名进行改正,也就是去掉V（约定视图名以v或V开头）
            return viewName.Replace("v_", "").Replace("V_", "");
        }
        public virtual string[] GetMethodName()
        {
            return new string[] { "GetDetailByMainID", "SelectByWhere" };
        }

    }
    public class PageModel
    {
        public DataTable Data { get; set; }
        public int Total { get; set; }
    }
}