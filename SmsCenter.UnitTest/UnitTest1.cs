using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winner.SmsCenter.Client;

namespace SmsCenter.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ClientProxyTest()
        {
            SmsServiceClient client = new SmsServiceClient("DZR", "123456789_DZR");
            var res = client.Send("18675534882", "【253云通讯】欢迎体验253云通讯产品验证码是253253");
            string message = client.Message;
            Assert.IsTrue(res);
        }
    }
}
