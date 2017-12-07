using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.Entities
{
    public class CodeVerificationArgs
    {
        public string UserCode { get; set; }
        public string UserPwd { get; set; }
        public string Mobileno { get; set; }
        public string Gid { get; set; }
        public string Code { get; set; }
    }
}
