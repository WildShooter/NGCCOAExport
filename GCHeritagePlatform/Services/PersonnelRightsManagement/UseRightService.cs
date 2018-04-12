using System.Collections.Generic;
using System.Linq;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.PersonnelRightsManagement.Interface;
using GCHeritagePlatform.Services.PersonnelRightsManagement.Model;
using GCHeritagePlatform.Utils;

namespace GCHeritagePlatform.Services.PersonnelRightsManagement
{
    public class UseRightService : IUserRightService
    {

        public string GetUserByFunID(string funID)
        {
            var sql = $@"select ID,NAME,REALNAME from PRIVS_USER where id in (select USERID from PRIVS_USER_ROLE where ROLEID in (select ROLEID from PRIVS_ROLE_FUNCPRIVS where PRIVSID ='{funID}'))";
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null)
                return JsonHelper.SerializeObject(new ResultModel(false, "数据库连接错误!"));
            var dt = context.getDataTableResult(sql);
            return JsonHelper.SerializeObject(new ResultModel(true, dt));
        }

        public string GetFunByUserID(string userID)
        {
            var sql =
                $@"select distinct b.NAME,b.ID,b.PID,ARGS from PRIVS_ROLE_FUNCPRIVS a
                    left join PRIVS_FUNCPRIVS b on  a.PRIVSID = b.id where a.roleID in
( select ifnull(RoleID,'-1') from PRIVS_USER_ROLE where UserID='{userID}') and ID is not null order by b.INDEXOFORDER";
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null)
                return JsonHelper.SerializeObject(new ResultModel(false, "数据库连接错误!"));
            var dt = context.getDataTableResult(sql);
            if(dt==null||dt.Rows.Count==0)
                return JsonHelper.SerializeObject(new ResultModel(false, "用户不存在!"));
            var entList = dt.ToEntList<FuncTree>();
            var el = entList.Where(e =>string.IsNullOrEmpty(e.PID)).ToList();
            foreach (var item in el)
            {
                GetFunC(entList, item);
            }
            return JsonHelper.SerializeObject(ToolResult.Success(el));
        }

        public string GetFunByRole(string roleID)
        {
            var sql =
                $@"
                    select b.NAME,b.ID,b.PID,a.ARGS from PRIVS_ROLE_FUNCPRIVS a
                    left join PRIVS_FUNC b on a.PRIVSID = b.id where a.roleID='{roleID}'";
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null)
                return JsonHelper.SerializeObject(new ResultModel(false, "数据库连接错误!"));

            var dt = context.getDataTableResult(sql);
            var entList = dt.ToEntList<FuncTree>();
            var el = entList.Where(e => e.PID==null).ToList();
            foreach (var item in el)
            {
                GetFunC(entList,item);
            }
            return JsonHelper.SerializeObject(ToolResult.Success(el));
        }

        private void GetFunC(IList<FuncTree> listAll, FuncTree func )
        {
            var entList = listAll.Where(e => e.PID == func.ID);
            if (entList.Count() == 0) return;
            foreach (var item in entList)
            {
                 GetFunC(listAll, item);
            }
            func.CHILDREN.AddRange(entList);
        }
    }
}