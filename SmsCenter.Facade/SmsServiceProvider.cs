using SmsCenter.DataAccess;
using SmsCenter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;

namespace SmsCenter.Facade
{
    public class SmsServiceProvider : FacadeBase
    {
        private ISmsProvider _sms;
        private string _mobileno;
        private string _smsContent;
        private int _appid;
        public SmsServiceProvider(ISmsProvider sms, int appid, string mobileno, string smsContent)
        {
            this._sms = sms;
            this._mobileno = mobileno;
            this._smsContent = smsContent;
            this._appid = appid;
        }

        public bool Send()
        {
            StringBuilder logger = new StringBuilder();
            logger.AppendLine("==========================start=========================");
            logger.AppendLine("APPID=" + GetChannelName());
            logger.AppendLine("MOBILENO=" + _mobileno);
            logger.AppendLine(_smsContent);
            bool res = SendSms();
            if (!res)
            {
                logger.AppendLine(this.PromptInfo.CustomMessage);
                logger.AppendLine("发送失败");
            }
            else
            {
                logger.AppendLine("MESSAGEID=" + this._sms.SendNo);
                logger.AppendLine("发送成功");
            }
            logger.Append("==========================finish=========================");
            Log.Info(logger);
            return res;
        }
        private string GetChannelName()
        {
            if (_sms == null)
            {
                return this._appid.ToString();
            }
            var channel = _sms.GetChannel();
            if (channel == null)
            {
                return this._appid.ToString();
            }
            return channel.Channel_Name;
        }
        private bool SendSms()
        {
            if (!CheckData())
            {
                return false;
            }

            BeginTransaction();
            Tsms_Send_His daSendHis = new Tsms_Send_His();
            daSendHis.ReferenceTransactionFrom(Transaction);
            daSendHis.Appid = this._appid;
            daSendHis.Batch_Id = Guid.NewGuid().ToString("N");
            daSendHis.Mobileno = this._mobileno;
            daSendHis.Returntype = 0;
            daSendHis.Sendtime = DateTime.Now;
            daSendHis.Sendtype = 0;
            daSendHis.Smscontent = this._smsContent;
            if (!daSendHis.Insert())
            {
                Rollback();
                Alert("系统错误");
                return false;
            }
            if (!this._sms.Send(this._mobileno, this._smsContent))
            {
                Rollback();
                Alert(_sms.Message);
                return false;
            }
            daSendHis.Sendno = this._sms.SendNo;
            if (!daSendHis.Update())
            {
                Rollback();
                Alert("系统错误");
                return false;
            }
            Commit();
            return true;
        }
        private bool CheckData()
        {
            if (_sms == null)
            {
                Alert("未指定短信发送程序");
                return false;
            }
            if (!CheckMoblie())
                return false;
            if (!CheckSmsContent())
                return false;
            return true;
        }



        protected bool CheckMoblie()
        {
            if (string.IsNullOrEmpty(_mobileno))
            {
                Alert("接收短信的手机号码为空");
                return false;
            }
            if (_mobileno.Length != 11)//国内号码
            {
                Alert("手机号码格式不正确");
                return false;
            }
            if (!Regex.IsMatch(_mobileno, "^1\\d{10}$"))
            {
                Alert("不正确的手机号码格式");
                return false;
            }
            if (IsBlackList(_mobileno))
            {
                Alert("用户设置不接收短信");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是否黑名单用户
        /// </summary>
        /// <param name="mobileNum">The mobile num.</param>
        /// <returns></returns>
        protected bool IsBlackList(string mobileNum)
        {
            Tsms_Blacklist daMobileFilter = new Tsms_Blacklist();
            if (daMobileFilter.SelectByMOBILENO(mobileNum))
            {
                return true;
            }
            return false;
        }

        //检查短信内容
        protected bool CheckSmsContent()
        {
            if (string.IsNullOrEmpty(_smsContent))
            {
                Alert("短信内容不能为空");
                return false;
            }

            if (BadWordsFilter.HasBadWords(_smsContent))
            {
                if (!AppConfig.AutoReplaceBadword)
                {
                    Alert("短信内容出现敏感字符");
                    return false;
                }
                _smsContent = BadWordsFilter.ReplaceBadWords(_smsContent);
            }
            //规定短信内容字符长度不能超过2000
            if (System.Text.Encoding.UTF8.GetByteCount(_smsContent) >= 2000)
            {
                Alert("短信内容过长，不能超过2000字节");
                return false;
            }
            return true;
        }

    }
}
