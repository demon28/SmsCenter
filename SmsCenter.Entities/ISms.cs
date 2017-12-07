using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.Entities
{
    public interface ISmsProvider
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobileno"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        bool Send(string mobileno, string content);
        /// <summary>
        /// 设置短信通道账户信息
        /// </summary>
        /// <param name="channel"></param>
        void SetChannel(SmsChannel channel);
        /// <summary>
        /// 获取短信通道账户信息
        /// </summary>
        /// <returns></returns>
        SmsChannel GetChannel();
        /// <summary>
        /// 第三方服务商提供的短信编码
        /// </summary>
        string SendNo { get; }
        /// <summary>
        /// 如果失败，错误信息在这里
        /// </summary>
        string Message { get; }
    }
}
