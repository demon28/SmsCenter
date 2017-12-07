using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.Entities
{
    public class SmsChannel
    {
        public string Channel_ID { get; set; }
        public string Channel_Name { get; set; }
        public string Access_Name { get; set; }
        public string Access_Key { get; set; }
        public string Service_Url { get; set; }
        public string Provider { get; set; }
        public string Createtime { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
