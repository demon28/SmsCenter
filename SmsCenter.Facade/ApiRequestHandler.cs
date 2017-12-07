using SmsCenter.DataAccess;
using SmsCenter.Entities;
using SmsCenter.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;

namespace SmsCenter.Facade
{
    public class ApiRequestHandler : FacadeBase
    {
        public FuncResult Handler(string userCode, string userPwd, string mobileno, string smsContent)
        {
            try
            {
                Tsms_Thirdparty daThirdparty = null;
                string error;
                if (!SmsUtils.CheckUserCode(userCode, userPwd, mobileno, out daThirdparty, out error))
                {
                    return FuncResult.FailResult(error);
                }
                ISmsProvider sms = SmsServiceFactory.GetSmsServiceByChannel(daThirdparty.Channel_Id, out error);
                if (sms == null)
                {
                    return FuncResult.FailResult(error);
                }
                SmsServiceProvider provider = new SmsServiceProvider(sms, daThirdparty.Appid, mobileno, smsContent);
                if (!provider.Send())
                {
                    return FuncResult.FailResult(provider.PromptInfo.CustomMessage);
                }
                return FuncResult.SuccessResult();
            }
            catch (Exception ex)
            {
                Log.Error("handler异常", ex);
                return FuncResult.FailResult(ex.Message);
            }
        }

        public FuncResult Handler(string interfaceName, string dataJson)
        {
            FuncResult result = new FuncResult();
            try
            {
                if ("sms_service".Equals(interfaceName, StringComparison.OrdinalIgnoreCase))
                {
                    var args = JsonProvider.JsonTo<SmsArgs>(dataJson);
                    if (args == null)
                    {
                        return FuncResult.FailResult("参数为空");
                    }
                    result = this.Handler(args.UserCode, args.UserPwd, args.Mobileno, args.Content);
                }
                else if ("validate_code_send_service".Equals(interfaceName, StringComparison.OrdinalIgnoreCase))
                {
                    var args = JsonProvider.JsonTo<SendValidateCodeArgs>(dataJson);
                    if (args == null)
                    {
                        return FuncResult.FailResult("参数为空");
                    }
                    SmsValidCodeFacade valid = new SmsValidCodeFacade(args.UserCode, args.UserPwd);
                    bool res = valid.SendSmsValidCode(args.Mobileno, args.Gid, args.AppName);
                    result.Success = res;
                    result.Message = res ? null : valid.PromptInfo.CustomMessage;
                    result.StatusCode = res ? 1 : 4;
                }
                else if ("validate_code_verify_service".Equals(interfaceName, StringComparison.OrdinalIgnoreCase))
                {
                    var args = JsonProvider.JsonTo<CodeVerificationArgs>(dataJson);
                    if (args == null)
                    {
                        return FuncResult.FailResult("参数为空");
                    }
                    SmsValidCodeFacade valid = new SmsValidCodeFacade(args.UserCode, args.UserPwd);
                    bool isSuccess = valid.ValidSmsCode(args.Mobileno, args.Gid, args.Code);
                    result.Success = isSuccess;
                    result.Message = isSuccess ? null : valid.PromptInfo.CustomMessage;
                    result.StatusCode = isSuccess ? 1 : 4;
                }
                else
                {
                    result.Success = false;
                    result.Message = "未找到指定的服务[" + interfaceName + "]";
                    result.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                Log.Error("API-Handler异常", ex);
                result.Success = false;
                result.Message = "系统错误";
                result.StatusCode = 500;
            }
            return result;
        }
    }
}
