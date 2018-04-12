using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.PersonnelRightsManagement.Model;
using GCHeritagePlatform.Utils;
using System.Text;

namespace GCHeritagePlatform.Services.PersonnelRightsManagement
{
    public class UserRightsManagementService:BaseService<PRIVS_USER>
    {
      
        public UserRightsManagementService() : base()
        {
            this.DataTableName = "PRIVS_USER";
            this.PrimaryKey = "ID";
        }
        [Hprose("string InsertUser(string info,string roleArrid,string departid)", "新建用户信息")]
        public string InsertUser(string info,string roleid,string departid)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_USER>(info);
            var gid = Guid.NewGuid();
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            ent.NAMEPassword = DESHelper.EncodePassword(ent.NAME);
            ent.PASSWORD = DESHelper.EncodePassword(ent.PASSWORD);
            ent.ID = gid.ToString();
            ent.DEPARTMENTID = departid;
            var nameToValue = ent.GetNameToValueDic("NoAddField");
            var sql = dbContext.insertByParamsReturnSQL(DataTableName, nameToValue);
            var listSql = new List<string> {sql};
            var roleArr = roleid.Split(',');
            var roleTemplate = "INSERT INTO PRIVS_USER_ROLE(USERID,ROLEID) values ('{0}','{1}') ";
            listSql.AddRange(roleArr.Select(item => string.Format(roleTemplate, gid, item)));

          
            var bIsSuccess = dbContext.executeTransactionSQLList(listSql);
            return JsonHelper.SerializeObject(new ResultModel(bIsSuccess, bIsSuccess ? "新建用户信息成功！" : "新建用户信息失败！"));
        }

        [Hprose("string CheckLoginName(string name )", "检查用户名称不能重复")]
        public string CheckLoginName(string name)
        {

            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $@"SELECT * from PRIVS_USER where NAME= '{name}'";
            var bIsSuccess = (dbContext.getDataTableResult(sql)??new DataTable()).Rows.Count>0;
            return JsonHelper.SerializeObject(new ResultModel(bIsSuccess, bIsSuccess ? "用户名已经存在！":"可以正常使用！"));
        }

        [Hprose("string LockUser(string id,string isLook 1为锁 0位解锁 )", "锁定用户")]
        public string LockUser(string id,string isLook)
        {
            var ids = id.Replace(",", "','");
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $@"update PRIVS_USER set ISLOCKED='{isLook}' where id in ('{ids}')";
            var bIsSuccess = dbContext.execute(sql)>=0;
            return JsonHelper.SerializeObject(new ResultModel(bIsSuccess , bIsSuccess ? "操作成功！" : "操作失败！"));
        }
        [HproseAttribute("string UpdateUserPassword(string userid,string pwd)", "修改用户密码")]
        public string UpdateUserPassword(string userid,  string newPwd)
        {
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null)
                return JsonHelper.SerializeObject(new ResultModel(false, "数据库连接错误!"));
         
            var updateM = $@"update PRIVS_USER set PASSWORD='{DESHelper.EncodePassword(newPwd)}' where ID='{userid}'";
            var i = context.execute(updateM)>0;
            return JsonHelper.SerializeObject(new ResultModel(i ,i ? "修改密码成功！" : "修改失败，请联系管理员！"));
        }


        [Hprose("string GetUser(string id )", "获取用户信息")]
        public string GetUser(string id )
        {
           
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $@"
                    select a.*,c.Name as POSITIONNAME,b.ROLENAME as LEADERName,d.NAME as DEPARTMENTName   from PRIVS_USER  a
                left join PRIVS_DEPARTMENT d on a.DEPARTMENTID=d.id
                left join PRIVS_LEADER b on a.ID=b.USERID
                left join PRIVS_POSITION c on a.POSITIONID=c.ID  where a.ID = '{id}'";
            DataTable dt = dbContext.getDataTableResult(sql);
            DataTable dtRole = dbContext.getDataTableResult($@"select a.*,b.Name  from  PRIVS_USER_ROLE  a
left Join PRIVS_ROLE b on a.ROLEID = b.ID
where  a.USERID ='{id}'");
            return JsonHelper.SerializeObject(new {User= dt,Role=dtRole});
        }

        [Hprose("string GetUserByName(string userName )", "获取用户信息")]
        public string GetUserByName(string userName)
        {

            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $@"
                    select a.*,c.Name as POSITIONNAME,b.ROLENAME as LEADERName,d.NAME as DEPARTMENTName   from PRIVS_USER  a
                left join PRIVS_DEPARTMENT d on a.DEPARTMENTID=d.id
                left join PRIVS_LEADER b on a.ID=b.USERID
                left join PRIVS_POSITION c on a.POSITIONID=c.ID  where a.name = '{userName}' or  a.realname = '{userName}' ";
            DataTable dt = dbContext.getDataTableResult(sql);
            DataTable dtRole = dbContext.getDataTableResult($@"select a.*,b.Name  from  PRIVS_USER_ROLE  a
left Join PRIVS_ROLE b on a.ROLEID = b.ID
where  a.USERID in (select id from PRIVS_USER where name = '{userName}' or  realname = '{userName}' )");
            return JsonHelper.SerializeObject(new { User = dt, Role = dtRole });
        }

        [Hprose("string UpdateUser(string info, string roleid, string departid)", "编辑用户信息")]
        public string UpdateUser(string info, string roleid, string departid)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_USER>(info);
            ent.DEPARTMENTID = departid;
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var nameToValue = ent.GetNameToValueDic("NoUpdateField");
            nameToValue.Remove("PASSWORD");
            var sql = dbContext.updateByParamsReturnSQL(DataTableName, nameToValue);
            var listSql = new List<string> {sql};

            //先删后加的逻辑 比判断哪些删除 哪些新增的逻辑更简化 
            var roleArr = roleid.Split(',');
            listSql.Add($" delete from PRIVS_USER_ROLE where USERID='{ent.ID}'");
            var roleTemplate = "INSERT INTO PRIVS_USER_ROLE(USERID,ROLEID) values ('{0}','{1}') ";
            listSql.AddRange(roleArr.Select(item => string.Format(roleTemplate, ent.ID, item)));
            var bIsSuccess = dbContext.executeTransactionSQLList(listSql);
            return JsonHelper.SerializeObject(new ResultModel(bIsSuccess,bIsSuccess ? "编辑用户信息成功" : "编辑用户信息失败"));
        }


        [Hprose("string DeleteUser(string ids )", "删除用户信息")]
        public string DeleteUser(string ids)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            var filter = $" id in ({ids.Split(',').ChangeListToString()}) ";
            string deleteSqlTemplate = " delete from {0} where  {1}";
            var sql = string.Format(deleteSqlTemplate, DataTableName, filter);
            var delArr=new List<string >() { $"delete from PRIVS_USER_ROLE where UserID in ('{ids.Replace(",","','")}')" ,sql};
            var bSuccess = dbContext.executeTransactionSQLList(delArr);
            return JsonHelper.SerializeObject(new ResultModel(bSuccess, bSuccess ? "删除成功" : "删除失败"));
        }
        [Hprose("string  GetUserList(string keywords,int page ,int num)", "获取用户列表")]
        public string  GetUserList(string keywords,int page, int num)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));

            var sql =@"
                    select a.*,c.Name as POSITIONNAME,b.ROLENAME as LEADERName,d.NAME as DEPARTMENTName   from PRIVS_USER  a
                left join PRIVS_DEPARTMENT d on a.DEPARTMENTID=d.id
                left join PRIVS_LEADER b on a.ID=b.USERID
                left join PRIVS_POSITION c on a.POSITIONID=c.ID  where 1=1 ";
            var sqlstringbuilder = new StringBuilder(sql);
            if (!string.IsNullOrEmpty(keywords))
            {
                sqlstringbuilder.AppendFormat("  and( a.REALNAME   like '%{0}%' or  a.NAME like '%{0}%' or b.ROLENAME like '%{0}%' or d.NAME like '%{0}%' and c.Name LIKE '%{0}%')", keywords);
            }
            sqlstringbuilder.AppendFormat(" order by CJSJ desc");
            var busness = new CommonBaseBusiness();
            var pageModel = busness.Select(sqlstringbuilder.ToString(), (page - 1) * num, num);
            return JsonHelper.SerializeObject(pageModel);

        }

    }
}