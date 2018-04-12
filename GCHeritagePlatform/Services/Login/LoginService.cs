using GCHeritagePlatform.Models;
using GCHeritagePlatform.Services.Interface;
using System;
using GCHeritagePlatform.Utils;
using MessageCheck;

namespace GCHeritagePlatform.Services.Login
{
   
    public class LoginService: ILoginService
    {
        [HproseAttribute("string LoginOut(string userID)", "用户退出")]
        public string LoginOut(string userID)
        {
            SessionHelper.SetLoginUser(null);
            return JsonHelper.SerializeObject(new ResultModel(true, "退出登录!"));
        }
        [HproseAttribute("string CheckLogin(string userID)", "用户退出")]
        public string CheckLogin(string userID)
        {
           var user= SessionHelper.GetUser();
            if (user == null || user.ID != userID)
            {
                SessionHelper.SetLoginUser(null);
                return JsonHelper.SerializeObject(new ResultModel(false, "请重新登录!"));
            }
         
           return JsonHelper.SerializeObject(new ResultModel(true, "已经登录!"));
        }
//        [HproseAttribute("string UpdatePWD(string user,string phone,string pwd)", "忘记密码")]
//        public string UpdatePWD(string userName, string phone, string newPwd)
//        {
//            var context = DBHelperPool.Instance.GetDbHelper();
//            if (context == null)
//                return JsonHelper.SerializeObject(new ResultModel(false, "数据库连接错误!"));
//            var sql = @"SELECT MOBILE,
//                    a.REALNAME, b.Name AS DWName, a.ID,JGJB,c.Name as JGJBName,SSSF
//                FROM
//                    PRIVS_USER a
//                        LEFT JOIN
//                    PRIVS_DEPARTMENT b ON a.DEPARTMENTID = b.id
//                    left join (
//select * from ysj_domain_enumitem where enumID='1B6A7930-D2C0-44B3-984C-9E7870E1B229' ) c on c.code=b.JGJB
//            where a.NAMEPassword='{0}'";
//            var dt = context.getDataTableResult(string.Format(sql, DESHelper.EncodePassword(userName)));
//            if (dt == null || dt.Rows.Count == 0)
//            {
//                return JsonHelper.SerializeObject(new ResultModel(false, "用户名输入不正确！"));
//            }
//            var phoneM = DESHelper.EncodePassword(dt.Rows[0]["MOBILE"] +"")== DESHelper.EncodePassword(phone);
//            if (!phoneM)
//            {
//                return JsonHelper.SerializeObject(new ResultModel(false, "输入的电话不正确！"));
//            }
//            var updateM = $@"update PRIVS_USER set PASSWORD='{DESHelper.EncodePassword(newPwd)}' where NAME='{userName}' and MOBILE='{phone}'";
//           var i= context.execute(updateM);
//            return JsonHelper.SerializeObject(ToolResult.Success(i>0?"修改密码成功！":"修改失败，请联系管理员！"));
//        }

        [HproseAttribute("string Login(string user,string pwd)", "用户登录（采集端）")]
        public string Login(string userName, string pwd)
        {
            return CommonLogion(userName, pwd);
        }
        [HproseAttribute("string LoginZPT(string user,string pwd,string code)", "用户登录（总平台）")]
        public string LoginZPT(string userName, string pwd,string code)
        {
            return CommonLogion(userName, pwd,code);
        }
        [Hprose("string GetMessageCode(userName)", "获取短信验证码")]
        public string GetMessageCode(string userName)
        {
            try
            {

           
            DateTime dataTime = DateTime.Now;
            // 验证请求验证码用户是否存在，若不存在，返回特定字符串给前台 [4/7/2015 ZYQ]
            var context = DBHelperPool.Instance.GetDbHelper();
            var dtUser = context.getDataTableResult(string.Format("select  * from PRIVS_USER where NAMEPassword='{0}'", DESHelper.EncodePassword(userName)));
            if (dtUser == null || dtUser.Rows.Count == 0)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "用户名不存在！"));
            }
            string phonenumber = dtUser.Rows[0]["MOBILE"] +"";
            //获取验证信息
            string checkCode = GenerateRandomNumber(4);
            SessionHelper.SetCheckCode(checkCode);
            string smsCode = "【国信司南】短信登录验证码为：" + checkCode + "，此验证码只用于登录您在 中国世界文化遗产监测预警总平台 用户的帐号，验证码提供给他人将导致您的帐号信息被盗，请勿转发。";
            //获取短信ID
            long smsID = dataTime.ToFileTime();
            // 验证码存入数据库 [4/7/2015 ZYQ]
            //softwareSerialNo注册序列号（正式发布需要改为读取配置文件）
            string softwareSerialNo = "9SDK-EMY-0229-JCWUO";
            //key：用户自定义key值，相当于注册时候的密码（正式发布需要改为读取配置文件）
            string key = "951040";
            YMMessageCheck ymMessageCheck = new YMMessageCheck(softwareSerialNo, key);
            int iresult = ymMessageCheck.Send_sms(phonenumber, smsCode, smsID);
            return JsonHelper.SerializeObject(new ResultModel(true, iresult>=0? "验证发送成功！":"验证发送失败！"));
            }
            catch (Exception ex)
            {

                return JsonHelper.SerializeObject(new ResultModel(false, ex.Message));
            }
        }
        private static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static string GenerateRandomNumber(int length)//调用时想生成几位就几位；Length等于多少就多少位。
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }
        private string CommonLogion(string userName, string pwd,string code="")
        {
            if (!string.IsNullOrEmpty(code)&&code!=SessionHelper.GetCheckCode())
            {
                return JsonHelper.SerializeObject(new ResultModel(false, "短信验证码错误!"));
            }
            var context = DBHelperPool.Instance.GetDbHelper();
            if (context == null)
                return JsonHelper.SerializeObject(new ResultModel(false, "数据库连接错误!"));


            var sql = @"
                    select a.*,c.Name as POSITIONNAME,b.ROLENAME as LEADERName,d.NAME as DEPARTMENTName   from PRIVS_USER  a
                left join PRIVS_DEPARTMENT d on a.DEPARTMENTID=d.id
                left join PRIVS_LEADER b on a.ID=b.USERID
                left join PRIVS_POSITION c on a.POSITIONID=c.ID where a.Name='{1}'  and Password='{0}'";//a.NAMEPassword='{0}'
            //防注入代码记得放开
            var dt = context.getDataTableResult(string.Format(sql, DESHelper.EncodePassword(pwd),userName));// DESHelper.EncodePassword(userName),
            if (dt == null || dt.Rows.Count == 0)
            {
                var dtUser = context.getDataTableResult(
                    $"select  * from PRIVS_USER where NAME='{userName}'");
                var strMsgUser = dtUser != null && dtUser.Rows.Count > 0 ? "密码错误!" : "用户名错误!";
                return JsonHelper.SerializeObject(new ResultModel(false, strMsgUser));
            }
            return JsonHelper.SerializeObject(ToolResult.Success(dt));
        }

        [Hprose("string GetUserPZ(userID)", "获取短信验证码")]
        public string GetUserPZ(string userID)
        {
            try
            {
                var o= SessionHelper.GetUser();
                if(o.ID!=userID) return JsonHelper.SerializeObject(new ResultModel(false, "用户没有登录！"));
                return JsonHelper.SerializeObject(ToolResult.Success(o));
            }
            catch (Exception ex)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, ex.Message));
            }

        }

      

    }
}