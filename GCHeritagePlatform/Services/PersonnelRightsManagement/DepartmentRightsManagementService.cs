using System.Data;
using System.Linq;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.PersonnelRightsManagement.Model;
using GCHeritagePlatform.Utils;
using System.Text;
using System.Collections.Generic;

namespace GCHeritagePlatform.Services.PersonnelRightsManagement
{
    public class DepartmentRightsManagementService : BaseService<PRIVS_DEPARTMENT>
    {
        public DepartmentRightsManagementService() : base()
        {
            this.DataTableName = "PRIVS_DEPARTMENT";
            this.PrimaryKey = "ID";
        }
        [Hprose("string InsertDepartment(string info)", "新建机构信息")]
        public string InsertDepartment(string info)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_DEPARTMENT>(info);
            var bISuccess = base.Add(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess , bISuccess  ? "新建机构信息成功！" : "新建机构信息失败！"));
        }
        [Hprose("string GetDepartmentInfo(string id)", "获取机构信息")]
        public string GetDepartmentInfo(string id)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql =
                $@"select b.Name as PARENTName,a.* from PRIVS_DEPARTMENT a
left join PRIVS_DEPARTMENT b on a.PARENTID=b.ID  where a.ID = '{id}'";
            DataTable dt = dbContext.getDataTableResult(sql);
            return JsonHelper.SerializeObject(dt);
        }
        [Hprose("string UpdateDepartment(string info)", "编辑机构信息")]
        public string UpdateDepartment(string info)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_DEPARTMENT>(info);
            var bISuccess = base.Update(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "编辑机构信息成功！" : "编辑机构信息失败！"));
        }

        [Hprose("string CancelDepartment(string id)", "撤销机构信息")]
        public string CancelDepartment(string id)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_DEPARTMENT>(id);
            var bISuccess = base.Update(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "编辑机构信息成功！" : "编辑机构信息失败！"));
        }

        [HproseAttribute("string DeleteDepartmentInfo(string ids)", "删除机构信息")]
        public string DeleteDepartmentInfo(string ids)
        {
            var bISuccess = base.Delete(ids.Split(','));
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "删除成功！" : "删除失败！"));
        }





        [Hprose("string  GetDepartmentList(string keywords,int page, int num)", "获取机构列表")]
        public string GetDepartmentList(string keywords, int page, int num)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sqlstringbuilder = new StringBuilder(@"select b.Name as PARENTNANE,a.* from PRIVS_DEPARTMENT a
left join PRIVS_DEPARTMENT b on a.PARENTID=b.ID  where 1=1");

            if (!string.IsNullOrEmpty(keywords))
            {
                sqlstringbuilder.AppendFormat(" and (a.NAME like '%{0}%' or a.ALIASNAME like '%{0}%') ", keywords);
            }
           
            sqlstringbuilder.AppendFormat(" order by a.CJSJ desc");
            var busness = new CommonBaseBusiness();
            var pageModel = busness.Select(sqlstringbuilder.ToString(), (page - 1) * num, num);
            return JsonHelper.SerializeObject(pageModel);
        }
        [HproseAttribute("string GetDepartmentTree()", "获取机构树")]
        public string GetDepartmentTree()
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = "select ID,NAME,PARENTID as PID from PRIVS_DEPARTMENT";
            var dt = dbContext.getDataTableResult(sql);
            var entList = dt.ToEntList<FuncTree>();
            var el = entList.Where(e => e.PID == null).ToList();
            foreach (var item in el)
            {
                GetFunC(entList, item);
            }
            return JsonHelper.SerializeObject(el);
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