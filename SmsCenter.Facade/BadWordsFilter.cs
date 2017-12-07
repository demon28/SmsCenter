using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.Facade
{
    public class BadWordsFilter
    {
        internal static bool HasBadWords(string _smsContent)
        {
            return false;
        }

        internal static string ReplaceBadWords(string _smsContent)
        {
            return _smsContent;
        }
    }
}
