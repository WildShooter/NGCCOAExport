using GCHeritagePlatform.Services.PersonnelRightsManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Utils;

namespace GCHeritagePlatform.Services.PersonnelRightsManagement
{
    public class RoleManagementService:BaseService<PRIVS_ROLE>
    {
        public RoleManagementService() : base()
        {
            this.DataTableName = "PRIVS_ROLE";
            this.PrimaryKey = "ID";
        }
        [Hprose("string CheckRoleName(string name )", "检查角色名称不能重复")]
        public string CheckRoleName(string name)
        {

            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $@"SELECT * from PRIVS_ROLE where NAME= '{name}'";
            var bIsSuccess = (dbContext.getDataTableResult(sql) ?? new DataTable()).Rows.Count > 0;
            return JsonHelper.SerializeObject(new ResultModel(bIsSuccess, bIsSuccess ? "角色名称已经存在！" : "可以正常使用！"));
        }


        [HproseAttribute("string InsertRole(string jsonStr)", "新增角色")]
        public string InsertRole(string jsonStr)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_ROLE>(jsonStr);
            var dicRole = ent.GetNameToValueDic();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            List<string> sqList = new List<string>();
            string gid = Guid.NewGuid().ToString();
            if (dicRole.ContainsKey("ID"))
            {
                dicRole["ID"] = gid;
            }
            dicRole["ID"] = gid;
            var sql = dbContext.insertByParamsReturnSQL("PRIVS_ROLE", dicRole);
            sqList.Add(sql);

            var bISuccess = dbContext.executeTransactionSQLList(sqList);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "保存成功！" : "保存失败！"));

        }
        [HproseAttribute("string GetRoleInfo(string id)", "获取角色信息")]
        public string GetRoleInfo(string id)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $"select * from PRIVS_ROLE where ID = '{id}'";
            var dt = dbContext.getDataTableResult(sql);
            return JsonHelper.SerializeObject(dt);
        }

        [HproseAttribute("string UpdateRole(string jsonStr)", "更新角色")]
        public string UpdateRole(string jsonStr)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_ROLE>(jsonStr);
            bool bISuccess = base.Update(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "角色更新成功！" : "角色更新失败！"));
        }
        [HproseAttribute("string DeleteRole(string id)", "删除角色")]
        public string DeleteRole(string ids)
        {
            var bISuccess = base.Delete(ids.Split(','));
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "删除成功！" : "删除失败！"));
        }
        [HproseAttribute("string GetRoleList(string keyword,int page ,int num)", "获取角色信息列表")]

        public string GetRoleList(string keyword,  int page, int num)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = "select * from PRIVS_ROLE where 1=1 ";
            var sqlstringbuilder = new StringBuilder(sql);
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlstringbuilder.AppendFormat(" and (NAME like '%{0}%' or  ALIASNAME like '%{0}%') ", keyword);
            }
            
            sqlstringbuilder.AppendFormat(" order by  CJSJ desc");
            var busness = new CommonBaseBusiness();
            var pageModel = busness.Select(sqlstringbuilder.ToString(), (page - 1) * num, num);
            return JsonHelper.SerializeObject(pageModel);
        }

        [Hprose("string InsertRoleEx(string info,string funArrid)", "新建角色信息同时加上权限")]
        public string InsertRoleEx(string info, string funArrid)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_ROLE>(info);
            var gid = Guid.NewGuid();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            ent.ID = gid.ToString();
            var nameToValue = ent.GetNameToValueDic("NoAddField");
            var sql = dbContext.insertByParamsReturnSQL(DataTableName, nameToValue);
            var listSql = new List<string> { sql };
            var roleArr = funArrid.Split(',');
            var roleTemplate = "INSERT INTO PRIVS_ROLE_FUNCPRIVS(ROLEID,PRIVSID) values ('{0}','{1}') ";
            listSql.AddRange(roleArr.Select(item => string.Format(roleTemplate, gid, item)));

            var bIsSuccess = dbContext.executeTransactionSQLList(listSql);
            return JsonHelper.SerializeObject(new ResultModel(bIsSuccess, bIsSuccess ? "新建角色信息成功！" : "新建角色信息失败！"));
        }
        [HproseAttribute("string UpdateRoleEx(string jsonStr, string funArrid)", "更新角色同时更新角色所属权限")]
        public string UpdateRoleEx(string jsonStr, string funArrid)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_ROLE>(jsonStr);
            var nameToValue = ent.GetNameToValueDic("NoUpdateField");
            var sql = dbContext.updateByParamsReturnSQL(DataTableName, nameToValue);
            var listSql = new List<string> { sql };
            var roleArr = funArrid.Split(',');
            var roleTemplate = "INSERT INTO PRIVS_ROLE_FUNCPRIVS(ROLEID,PRIVSID) values ('{0}','{1}') ";
            listSql.Add($" delete from PRIVS_ROLE_FUNCPRIVS where ROLEID='{ent.ID}'");
            listSql.AddRange(roleArr.Select(item => string.Format(roleTemplate, ent.ID, item)));
            var bISuccess = dbContext.executeTransactionSQLList(listSql);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "角色更新成功！" : "角色更新失败！"));
        }

        [Hprose("string GetRoleEx(string id )", "获取角色信息及相关权限信息")]
        public string GetRoleEx(string id)
        {

            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $"select * from PRIVS_ROLE where ID = '{id}'";
            var dt = dbContext.getDataTableResult(sql);
            DataTable dtFUN = dbContext.getDataTableResult($@"select a.*,b.NAME  from  PRIVS_ROLE_FUNCPRIVS a
 left join PRIVS_FUNCPRIVS b on a.PRIVSID=b.ID where  a.ROLEID='{id}'");
            return JsonHelper.SerializeObject(new { Role = dt, Fun = dtFUN});
        }
   
       
        [Hprose("string RelationRole2Fun(string roleID,string funArrStr)", "关联角色和功能权限")]
        public string RelationRole2Fun(string roleid, string funArrStr)
        {
            var listSql = new List<string>();
            var dataTemplateSql = "INSERT INTO PRIVS_ROLE_FUNCPRIVS(ROLEID,PRIVSID) values ('{0}','{1}') ";
            var dataArr = funArrStr.Split(',');
            listSql.AddRange(dataArr.Select(item => string.Format(dataTemplateSql, roleid, item)));
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var bIsSuccess = dbContext.executeTransactionSQLList(listSql);
            return ToolResult.SuccessJson(bIsSuccess ? "保存角色功能权限成功" : "保存角色功能失败");
        }
        /// <summary>
        /// 根据角色ID 返回 功能权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [Hprose("string GetFunByRole(string roleID)", "根据角色ID 获得所有权限")]
        public string GetFunByRole(string roleID)
        {
            return new UseRightService().GetFunByRole(roleID);
        }

        [Hprose("string GetFunByUserID(string userID)", "根据用户ID 获得所有权限")]
        public string GetFunByUserID(string userID)
        {
            return new UseRightService().GetFunByUserID(userID);
        }

        [Hprose("string GetUserByFunID(string funID)", "根据功能ID 获得有该功能的用户")]
        public string GetUserByFunID(string funID)
        {
            return new UseRightService().GetUserByFunID(funID);
        }
    }
}