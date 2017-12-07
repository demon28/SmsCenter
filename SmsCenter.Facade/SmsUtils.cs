using SmsCenter.DataAccess;
using SmsCenter.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmsCenter.Facade
{
    public static class SmsUtils
    {
        public static bool CheckUserCode(string userCode, string userPwd, string mobileno, out Tsms_Thirdparty dathirdparty, out string error)
        {
            error = null;
            dathirdparty = null;
            if (string.IsNullOrEmpty(userCode))
            {
                error = "应用账号不能为空";
                return false;
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                error = "应用密码不能为空";
                return false;
            }
            dathirdparty = new Tsms_Thirdparty();
            if (!dathirdparty.SelectByUSERCODE(userCode))
            {
                error = "应用账号不存在";
                return false;
            }
            if (dathirdparty.Userpwd != userPwd)
            {
                error = "应用密码不正确";
                return false;
            }
            if (dathirdparty.Status == (int)AppUserState.未启用)
            {
                error = "账号未启用";
                return false;
            }
            if (dathirdparty.Usage_Quota.HasValue)
            {
                if (string.IsNullOrEmpty(mobileno))
                {
                    error = "接收短信的手机号码为空";
                    return false;
                }
                if (!Regex.IsMatch(mobileno, "^1\\d{10}$"))
                {
                    error = "接收短信的手机号码格式不正确";
                    return false;
                }
                if (dathirdparty.Usage_Quota < dathirdparty.Sendnum + 1)
                {
                    error = "账号发送数量已超过限制，最多还可发送" + (dathirdparty.Usage_Quota - dathirdparty.Sendnum) + "条";
                    return false;
                }
            }
            return true;
        }
    }
}
