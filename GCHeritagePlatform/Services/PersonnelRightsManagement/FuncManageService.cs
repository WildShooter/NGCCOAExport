using System;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.PersonnelRightsManagement.Model;
using GCHeritagePlatform.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCHeritagePlatform.Services.PersonnelRightsManagement
{
    public class FuncManageService:BaseService<PRIVS_FUNCPRIVS>
    {
        public FuncManageService() : base()
        {
            this.DataTableName = "PRIVS_FUNCPRIVS";
            this.PrimaryKey = "ID";
        }
        [HproseAttribute("string InsertFunc(string jsonStr)", "新增功能")]
        public string InsertFunc(string jsonStr)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_FUNCPRIVS>(jsonStr);
            bool bISuccess = base.Add(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "功能新建成功！" : "功能新建失败！"));
        }
        [HproseAttribute("string GetFuncInfo(id)", "查看功能信息")]
        public string GetFuncInfo(string id)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var funlistFunclist = new List<FuncTree>();
            var sql = $"select * from PRIVS_FUNCPRIVS  where id='{id}'";
            var dt = dbContext.getDataTableResult(sql);
            return JsonHelper.SerializeObject(dt);
        }
        [HproseAttribute("string UpdateFun(string jsonStr)", "更新功能")]
        public string UpdateFun(string jsonStr)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_FUNCPRIVS>(jsonStr);
            bool bISuccess = base.Update(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "功能更新成功！" : "功能更新失败！"));
        }
        [HproseAttribute("string DeleteFun(string ids)", "删除功能")]
        public string DeleteFun(string ids)
        {
            var bISuccess = base.Delete(ids.Split(','));
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "删除成功！" : "删除失败！"));
        }

    
        [HproseAttribute("string GetFunList(string keyword, int page, int num)", "获取功能列表")]
        public string GetFunList(string keyword, string typeid, int page, int num)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = "select * from PRIVS_FUNCPRIVS  where 1=1 ";
            var sqlstringbuilder = new StringBuilder(sql);
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlstringbuilder.AppendFormat(" and NAME like '%{0}%'", keyword);
            }
           
            sqlstringbuilder.AppendFormat(" order by  INDEXOFORDER ");
            var busness = new CommonBaseBusiness();
            var pageModel = busness.Select(sqlstringbuilder.ToString(), (page - 1) * num, num);
            return JsonHelper.SerializeObject(pageModel);
        }
        [HproseAttribute("string GetFuncTree()", "获取功能树")]
        public string GetFuncTree()
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = "select ID,NAME,PID,ARGS from PRIVS_FUNCPRIVS order by  INDEXOFORDER";
            var dt = dbContext.getDataTableResult(sql);
            if (dt==null||dt.Rows.Count == 0)
            {
                return JsonHelper.SerializeObject(dt);
            }
            var entList = dt.ToEntList<FuncTree>();
            var el = entList.Where(e => string.IsNullOrEmpty(e.PID)).ToList();
            foreach (var item in el)
            {
                GetFunC(entList, item);
            }
            return JsonHelper.SerializeObject(el);
        }

        [HproseAttribute("string GetFuncTree(string funID,string userID)", "根据用户ID、功能ID,获取功能树")]
        public string GetFuncTree(string funID,string userID)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
          var sql =
                $@"select distinct b.NAME,b.ID,b.PID,ARGS from PRIVS_ROLE_FUNCPRIVS a
                    left join PRIVS_FUNCPRIVS b on  a.PRIVSID = b.id where a.roleID in
( select ifnull(RoleID,'-1') from PRIVS_USER_ROLE where UserID='{userID}') and ID is not null order by b.INDEXOFORDER";
            var dt = dbContext.getDataTableResult(sql);
            if (dt == null || dt.Rows.Count == 0)
            {
                return JsonHelper.SerializeObject(dt);
            }
            var entList = dt.ToEntList<FuncTree>();
            var el = entList.Where(e => e.ID==funID).ToList();
            foreach (var item in el)
            {
                GetFunC(entList, item);
            }
            return JsonHelper.SerializeObject(el);
        }


        [HproseAttribute("string ExportFuncTree(listFuncTree)", "导入功能树")]
        public string ExportFuncTree(List<FuncTree> listFuncTree)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var listSql=new List<string >() { "delete from PRIVS_FUNCPRIVS" };
           var sqlTemplate= @"insert into PRIVS_FUNCPRIVS(ID,NAME,PID,DESCRIPTION,INDEXOFORDER,
                ARGS,GROUPNAME,SYSTEMID,CJSJ)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
            SetFuncListSql(listFuncTree, listSql, sqlTemplate);
            var b=dbContext.executeTransactionSQLList(listSql);
            return JsonHelper.SerializeObject(new ResultModel(b,b?"导入功能树成功":"导入功能树失败"));
        }

        public void SetFuncListSql(List<FuncTree> funcTree, List<string> listStr,string sqlTemplate)
        {
            foreach (var item in funcTree)
            {
                if (item.CHILDREN.Count > 0)
                {
                    SetFuncListSql(item.CHILDREN,listStr,sqlTemplate);
                }
                var sql = string.Format(sqlTemplate, item.ID, item.NAME,item.PID, item.DESCRIPTION, item.INDEXOFORDER, item.ARGS,
                    item.GROUPNAME,string.IsNullOrEmpty(item.SYSTEMID)?"1": item.SYSTEMID, DateTime.Now);
                listStr.Add(sql);
            }
        }

        private void GetFunC(IList<FuncTree> listAll, FuncTree func)
        {

            var entList = listAll.Where(e => e.PID == func.ID);
            var funcTrees = entList as FuncTree[] ?? entList.ToArray();
            if (!funcTrees.Any()) return;
            foreach (var item in funcTrees)
            {
                GetFunC(listAll, item);
            }
            func.CHILDREN.AddRange(funcTrees);
        }

     

      
    }
}