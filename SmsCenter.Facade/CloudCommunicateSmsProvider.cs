using SmsCenter.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;

namespace SmsCenter.Facade
{
    public class CloudCommunicateSmsProvider : ISmsProvider
    {
        private SmsChannel _channel;
        /// <summary>
        /// 创蓝通道发送短信
        /// </summary>
        /// <param name="mobileno">目标手机号</param>
        /// <param name="msg">短信息内容</param>
        /// <param name="extno">扩展号码，纯数字（1-3位）</param>
        /// <returns></returns>
        public FuncResult SendSmsByChuangLan(string mobileno, string smsContent, string extno)
        {
            var dic = GetErrorDictionary();

            FuncResult result = new FuncResult();
            string postStrTpl = "account={0}&pswd={1}&mobile={2}&msg={3}&needstatus=true&product=&extno={4}";

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, this._channel.Access_Name, this._channel.Access_Key,
                mobileno, smsContent, extno));

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(this._channel.Service_Url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = postData.Length;

                Stream newStream = myRequest.GetRequestStream();
                // Send the data.
                newStream.Write(postData, 0, postData.Length);
                newStream.Flush();
                newStream.Close();

                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    string respText = reader.ReadToEnd();
                    Log.Info(respText);
                    TextReader tr = new System.IO.StringReader(respText);
                    var resText = tr.ReadLine();
                    var msgid = tr.ReadLine();
                    var array = resText.Split(',');
                    var isSuccess = array[1] == "0";
                    this.SendNo = msgid;
                    result.Success = isSuccess;
                    result.Message = dic.ContainsKey(array[1]) ? dic[array[1]] : "发送失败";
                    result.StatusCode = isSuccess ? 1 : 400;
                    return result;
                }
                result.Success = false;
                result.Message = "网络链接失败";
                result.StatusCode = 503;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "连接超时";
                result.StatusCode = 500;
                return result;
            }

        }
        private static Dictionary<string, string> dic;

        public string SendNo { get; private set; }

        public string Message { get; private set; }

        private static Dictionary<string, string> GetErrorDictionary()
        {
            if (dic == null)
            {
                dic = new Dictionary<string, string>();
                dic.Add("0", "提交成功");
                dic.Add("101", "无此用户");
                dic.Add("102", "密码错");
                dic.Add("103", "提交过快（提交速度超过流速限制）");
                dic.Add("104", "系统忙（因平台侧原因，暂时无法处理提交的短信）");
                dic.Add("105", "敏感短信（短信内容包含敏感词）");
                dic.Add("106", "消息长度错（>536或<=0）");
                dic.Add("107", "包含错误的手机号码");
                dic.Add("108", "手机号码个数错（群发>50000或<=0;单发>200或<=0）");
                dic.Add("109", "无发送额度（该用户可用短信数已使用完）");
                dic.Add("110", "不在发送时间内");
                dic.Add("111", "超出该账户当月发送额度限制");
                dic.Add("112", "无此产品，用户没有订购该产品");
                dic.Add("113", "extno格式错（非数字或者长度不对）");
                dic.Add("115", "自动审核驳回");
                dic.Add("116", "签名不合法，未带签名（用户必须带签名的前提下）");
                dic.Add("117", "IP地址认证错,请求调用的IP地址不是系统登记的IP地址");
                dic.Add("118", "用户没有相应的发送权限");
                dic.Add("119", "用户已过期");
                dic.Add("120", "短信内容不在白名单中");
            }
            return dic;
        }

        public bool Send(string mobileno, string content)
        {
            var res = SendSmsByChuangLan(mobileno, content, null);
            if (!res.Success)
            {
                this.Message = res.Message;
            }
            return res.Success;
        }

        public void SetChannel(SmsChannel channel)
        {
            this._channel = channel;
        }

        public SmsChannel GetChannel()
        {
            return this._channel;
        }
    }
}
