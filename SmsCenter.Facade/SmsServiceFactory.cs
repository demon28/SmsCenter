using SmsCenter.DataAccess;
using SmsCenter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;

namespace SmsCenter.Facade
{
    public class SmsServiceFactory
    {
        public static ISmsProvider GetSmsServiceByChannel(int channel_id, out string error)
        {
            Tsms_Channel daChannel = new Tsms_Channel();
            if (!daChannel.SelectByPk(channel_id))
            {
                error = "短信通道配置错误";
                return null;
            }
            if (string.IsNullOrEmpty(daChannel.Provider))
            {
                error = "短信通道配置错误";
                return null;
            }
            SmsChannel channel = MapProvider.Map<SmsChannel>(daChannel.DataRow);
            string[] array = null;
            if (daChannel.Provider.Contains(','))
            {
                array = daChannel.Provider.Split(',');
            }
            else
            {
                array = new string[] {
                    "SmsCenter.Facade",
                    daChannel.Provider
                };
            }
            try
            {
                Assembly ass = null;
                ass = Assembly.Load(array[0]);
                Type t = ass.GetType(array[1]);
                object inst = Activator.CreateInstance(t);
                error = null;
                ISmsProvider sms = inst as ISmsProvider;
                if (sms == null)
                {
                    error = "通道配置错误";
                    return null;
                }
                sms.SetChannel(channel);
                return sms;
            }
            catch (Exception ex)
            {
                error = "通道配置错误，无法激活提供程序";
                return null;
            }
        }
    }
}
