using SmsCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Winner.Framework.Core.Facade;

namespace SmsCenter.Facade
{
    /// <summary>
    /// 短信验证码类
    /// </summary>
    public class SmsValidCodeFacade : FacadeBase
    {
        private string _userCode, _userPwd;
        public SmsValidCodeFacade(string userCode, string userPwd)
        {
            this._userCode = userCode;
            this._userPwd = userPwd;
        }
        public bool SendSmsValidCode(string mobileno, string templateGuid, string sourceApp)
        {
            Tsms_Thirdparty dathirdparty;
            string error;
            if (!SmsUtils.CheckUserCode(_userCode, _userPwd, mobileno, out dathirdparty, out error))
            {
                Alert(error);
                return false;
            }
            SmsValidateTemplate model = SmsTemplateFactory.GetModelByGid(templateGuid);
            if (model == null)
            {
                Alert(string.Format("找不到GID={0}的验证码配置信息", templateGuid));
                return false;
            }
            if (!CheckFrequency(mobileno, 10))
            {
                Alert("短信验证码申请过于频繁，请稍后再申请");
                return false;
            }
            this.Code = GenerateCode(model.Scalar);
            var daValidCode = new Tsms_Valid_Code();
            if (!daValidCode.SelectEffectiveCode(mobileno, model.Template_Id))
            {
                daValidCode.Template_Id = model.Template_Id;
                daValidCode.Valid_Code = this.Code;
                daValidCode.Expire_Time = DateTime.Now.AddMinutes(model.TimeSpan);
                daValidCode.Mobileno = mobileno;
                daValidCode.Private_Value = sourceApp;
                daValidCode.Status = 0;
                daValidCode.Send_Times = 1;
                if (!daValidCode.Insert())
                {
                    Alert("系统繁忙，请稍后重试！");
                    return false;
                }
            }
            else
            {
                this.Code = daValidCode.Valid_Code;
                daValidCode.Send_Times++;
                daValidCode.Update();
            }
            string smsContent = model.SmsContent
                .Replace("$APPNAME$", sourceApp)
                .Replace("$TIMESPAN$", model.TimeSpan.ToString())
                .Replace("$VALIDCODE$", this.Code)
                .Replace("$DATE$", DateTime.Now.ToString("yyyy年MM月dd日"));

            var sms = SmsServiceFactory.GetSmsServiceByChannel(dathirdparty.Channel_Id, out error);
            SmsServiceProvider provider = new SmsServiceProvider(sms, dathirdparty.Appid, mobileno, smsContent);
            bool res = provider.Send();
            if (!res)
            {
                Alert(provider.PromptInfo);
                return false;
            }
            return true;
        }
        public string Code { get; set; }
        public bool ValidSmsCode(string mobileno, string gid, string validCode)
        {
            Tsms_Thirdparty dathirdparty;
            string error;
            if (!SmsUtils.CheckUserCode(_userCode, _userPwd, mobileno, out dathirdparty, out error))
            {
                Alert(error);
                return false;
            }
            var model = SmsTemplateFactory.GetModelByGid(gid);
            var daValidCode = new Tsms_Valid_Code();
            if (!daValidCode.SelectByCodeAndBizType(validCode, mobileno, model.Template_Id))
            {
                Alert("验证码不正确");
                return false;
            }
            if (daValidCode.Status == 1)
            {
                Alert("验证码已被使用");
                return false;
            }
            if (daValidCode.Expire_Time < DateTime.Now)
            {
                Alert("验证码已过期");
                return false;
            }
            daValidCode.UpdateStatus(validCode, mobileno, model.Template_Id);
            return true;
        }
        private string GenerateCode(int scalar)
        {
            if (scalar <= 0)
            {
                scalar = 4;
            }
            if (scalar > 8)
            {
                scalar = 4;
            }
            double baseMi = Math.Pow(10, (double)scalar);
            double minEdge = 0.1d;
            double maxEdge = 0.999999999999999999999999d;
            int minValue = (int)Math.Ceiling(minEdge * baseMi);
            int maxValue = (int)Math.Ceiling(maxEdge * baseMi);
            var random = new Random();
            string text = random.Next(minValue, maxValue).ToString();
            return text;
        }
        private bool CheckFrequency(string mobileno, int limit)
        {
            var daValidCode = new Tsms_Valid_Code();
            return daValidCode.GetSendTimes(mobileno) <= limit;
        }
    }
    public class SmsValidateTemplate
    {
        public int Template_Id { get; set; }
        public string Gid { get; set; }
        public string SmsContent { get; set; }
        public int TimeSpan { get; set; }
        public int Scalar { get; set; }
    }
    public class SmsTemplateFactory
    {
        private static List<SmsValidateTemplate> models = new List<SmsValidateTemplate>();
        public static SmsValidateTemplate GetModelByGid(string template_guid)
        {
            if (models.Count == 0)
            {
                Tsms_TemplateCollection daTemplateColl = new Tsms_TemplateCollection();
                daTemplateColl.ListAll();
                foreach (Tsms_Template template in daTemplateColl)
                {
                    SmsValidateTemplate model = new SmsValidateTemplate();
                    model.Template_Id = template.Template_Id;
                    model.Gid = template.Template_Guid;
                    model.TimeSpan = template.Timespan;
                    model.SmsContent = template.Content;
                    model.Scalar = template.Code_Scalar;
                    models.Add(model);
                }
            }
            return models.Find(it => it.Gid == template_guid);
        }
    }
}
