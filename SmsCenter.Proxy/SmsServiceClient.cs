using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Winner.SmsCenter.Client
{
    /// <summary>
    /// 短信服务客户端代理类
    /// </summary>
    public class SmsServiceClient
    {
        private const string SEND_SMS_SERVICE_NAME = "sms_service";
        private const string VALID_CODE_SEND_SERVICE = "validate_code_send_service";
        private const string VALID_CODE_VERIFY_SERVICE = "validate_code_verify_service";
        private string _url;
        private string _userCode;
        private string _userPwd;
        /// <summary>
        /// 短信服务客户端代理类
        /// </summary>
        /// <param name="userCode">账号</param>
        /// <param name="userPwd">密码</param>
        public SmsServiceClient(string userCode, string userPwd)
        {
            this._userCode = userCode;
            this._userPwd = userPwd;
            this._url = this.ReadConfigUrl;
        }
        /// <summary>
        /// 短信服务客户端代理类
        /// </summary>
        /// <param name="userCode">账号</param>
        /// <param name="userPwd">密码</param>
        /// <param name="url">短信服务地址</param>
        public SmsServiceClient(string userCode, string userPwd, string url) : this(userCode, userPwd)
        {
            this._url = url;
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// app名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// API服务地址
        /// </summary>
        public string ServiceUrl
        {
            get
            {
                return _url;
            }
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobileno">手机号码</param>
        /// <param name="content">短信内容</param>
        /// <returns></returns>
        public bool Send(string mobileno, string content)
        {
            string arg = string.Concat("{", string.Format("\"UserCode\":\"{0}\",\"UserPwd\":\"{1}\",\"Mobileno\":\"{2}\",\"Content\":\"{3}\"", _userCode, _userPwd, mobileno, content), "}");
            var resp = http(SEND_SMS_SERVICE_NAME, arg);
            if (string.IsNullOrEmpty(resp))
            {
                return false;
            }
            JsonObject obj = JsonObject.Parse(resp);
            bool isSuccess = obj.GetBoolean("Success");
            string message = obj.GetString("Message");
            this.Message = isSuccess ? null : message;
            return isSuccess;
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="mobileno">手机号码</param>
        /// <param name="gid">验证码模版ID</param>
        /// <param name="templateParams">模版参数,键值对</param>
        /// <returns></returns>
        public bool SendValidateCode(string mobileno, string gid, Dictionary<string, string> templateParams)
        {
            string arg = string.Concat("{", string.Format("\"UserCode\":\"{0}\",\"UserPwd\":\"{1}\",\"Mobileno\":\"{2}\",\"Gid\":\"{3}\",\"AppName\":\"{4}\"", _userCode, _userPwd, mobileno, gid, this.AppName), "}");
            var resp = http(VALID_CODE_SEND_SERVICE, arg);
            JsonObject obj = JsonObject.Parse(resp);
            bool isSuccess = obj.GetBoolean("Success");
            string message = obj.GetString("Message");
            this.Message = isSuccess ? null : message;
            return isSuccess;
        }
        /// <summary>
        /// 验证码校验
        /// </summary>
        /// <param name="mobileno">手机号</param>
        /// <param name="gid">验证码模版ID</param>
        /// <param name="validateCode">用户收到的验证码</param>
        /// <returns></returns>
        public bool ValidateCode(string mobileno, string gid, string validateCode)
        {
            string arg = string.Format("\"UserCode\":\"{0}\",\"UserPwd\":\"{1}\",\"Mobileno\":\"{2}\",\"Gid\":\"{3}\",\"Code\":\"{4}\"", _userCode, _userPwd, mobileno, gid, validateCode);
            arg = string.Concat("{", arg, "}");
            var resp = http(VALID_CODE_VERIFY_SERVICE, arg);
            JsonObject obj = JsonObject.Parse(resp);
            bool isSuccess = obj.GetBoolean("Success");
            string message = obj.GetString("Message");
            this.Message = isSuccess ? null : message;
            return isSuccess;
        }
        private string http(string interfaceName, string s)
        {
            var interface_url = this._url;
            if (interface_url.EndsWith("/") || interface_url.EndsWith("\\"))
            {
                interface_url += interfaceName;
            }
            else
            {
                interface_url += "/" + interfaceName;
            }
            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(s);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(interface_url);
                myRequest.Method = "POST";
                //myRequest.ContentType = "application/x-www-form-urlencoded";
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
                    return respText;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private string ReadConfigUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["sms_service_url"];
                return url;
            }
        }
    }
}
