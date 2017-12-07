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
    public class ChuanLanSmsProvider : ISmsProvider
    {
        private SmsChannel _channel;
        public string Message
        {
            get;
            private set;
        }

        public string SendNo
        {
            get;
            private set;
        }

        public bool Send(string mobileno, string content)
        {
            var result = SendSmsByChuangLan(mobileno, content, null);
            if (!result.Success)
            {
                this.Message = result.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 创蓝通道发送短信，为SendNo赋值
        /// </summary>
        /// <param name="mobileno">目标手机号</param>
        /// <param name="msg">短信息内容</param>
        /// <param name="extno">扩展号码，纯数字（1-3位）</param>
        /// <returns></returns>
        private FuncResult SendSmsByChuangLan(string mobileno, string smsContent, string extno)
        {
            if (Debuger.IsDebug)
            {
                return FuncResult.SuccessResult();
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
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

            //string postStrTpl = "account={0}&pswd={1}&mobile={2}&msg={3}&needstatus=true&product=&extno={4}";
            string postStrTpl = "un={0}&pw={1}&phone={2}&msg={3}&rd=1";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, this._channel.Access_Name, this._channel.Access_Key,
                mobileno, smsContent));

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
                    this.SendNo = msgid;
                    if (array[1] != "0")
                    {
                        string error = dic.ContainsKey(array[1]) ? dic[array[1]] : "发送失败";
                        return FuncResult.FailResult(error);
                    }
                    return FuncResult.SuccessResult();
                }
                return FuncResult.FailResult("短信通道访问异常");
            }
            catch (Exception ex)
            {
                return FuncResult.FailResult("发送短信时出现系统繁忙！原因：" + ex.Message);
            }
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
