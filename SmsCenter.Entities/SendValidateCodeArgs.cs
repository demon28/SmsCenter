using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.Entities
{
    public class SendValidateCodeArgs : SmsArgs
    {
        public string Gid { get; set; }
        public string AppName { get; set; }
    }
}
