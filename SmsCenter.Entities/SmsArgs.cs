﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.Entities
{
    public class SmsArgs
    {
        public string UserCode { get; set; }
        public string UserPwd { get; set; }
        public string Mobileno { get; set; }
        public string Content { get; set; }
    }
}
