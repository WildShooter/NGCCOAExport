using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.PersonnelRightsManagement.Model;
using GCHeritagePlatform.Utils;

namespace GCHeritagePlatform.Services.PersonnelRightsManagement
{
    //专用类的服务
    public class SpecialService : BaseService<PRIVS_POSITION>
    {
        public SpecialService() : base()
        {
            this.DataTableName = "PRIVS_POSITION";
            this.PrimaryKey = "ID";
        }
        [Hprose("string GetPositionByDepartment(string departmentId )", "根据部门返回职务")]
        public string GetPositionByDepartment(string departmentId)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $"select * from PRIVS_POSITION where DepartmentID='{departmentId}'";
            var dt = dbContext.getDataTableResult(sql);
            return JsonHelper.SerializeObject(dt);
        }
        [Hprose("string InsertPosition(string info )", "新建职位")]
        public string InsertPosition(string info)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_POSITION>(info);
            var bISuccess = base.Add(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "新建职位成功！" : "新建职位信息失败！"));
        }

        [Hprose("string GetPosition(string id )", "获得职位信息")]
        public string GetPosition(string id)
        {
            var dbContext = DBHelperPool.Instance.GetDbHelper();
            if (dbContext == null) return JsonHelper.SerializeObject(ToolResult.Failure("数据连接异常!"));
            var sql = $"select * from PRIVS_POSITION where ID = '{id}'";
            var dt = dbContext.getDataTableResult(sql);
            return JsonHelper.SerializeObject(dt);
        }
        [Hprose("string UpdatePosition(string info )", "编辑职位")]
        public string UpdatePosition(string info)
        {
            var ent = JsonHelper.DeserializeJsonToObject<PRIVS_POSITION>(info);
            var bISuccess = base.Update(ent);
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "编辑职位成功！" : "编辑职位失败！"));
        }
        [Hprose("string DeletePosition(string ids )", "删除职位")]
        public string DeletePosition(string ids)
        {
            var bISuccess = base.Delete(ids.Split(','));
            return JsonHelper.SerializeObject(new ResultModel(bISuccess, bISuccess ? "删除成功！" : "删除失败！"));
        }
    }
}