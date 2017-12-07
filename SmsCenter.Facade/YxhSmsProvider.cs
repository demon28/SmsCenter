using SmsCenter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.Facade
{
    public class YxhSmsProvider : ISmsProvider
    {
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
        }
        private string _sendNo;
        public string SendNo
        {
            get
            {
                return _sendNo;
            }
        }
        private SmsChannel _smsChannel;
        public SmsChannel GetChannel()
        {
            return _smsChannel;
        }

        public bool Send(string mobileno, string content)
        {
            try
            {
                SmsCenter.SmsServiceClient.SmsClient client = new SmsServiceClient.SmsClient(_smsChannel.Access_Name, _smsChannel.Access_Key);
                client.ServiceUrl = _smsChannel.Service_Url;
                bool res = client.SendSms(mobileno, content, null);
                _sendNo = client.Result?.BatchID;
                return res;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                return false;
            }
        }

        public void SetChannel(SmsChannel channel)
        {
            _smsChannel = channel;
        }
    }
}
