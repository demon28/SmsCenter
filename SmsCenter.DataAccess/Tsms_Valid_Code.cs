using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsCenter.DataAccess
{
    public partial class Tsms_Valid_Code
    {
        public bool SelectByCodeAndBizType(string code, string mobileno, int template_id)
        {
            string sql_condition = string.Format("{0}=:{0} AND {1}=:{1} AND {2}=:{2}", _VALID_CODE, _MOBILENO, _TEMPLATE_ID);
            AddParameter(_VALID_CODE, code);
            AddParameter(_MOBILENO, mobileno);
            AddParameter(_TEMPLATE_ID, template_id);
            return SelectByCondition(sql_condition);
        }
        public bool UpdateStatus(string code, string mobileno, int template_id)
        {
            string sql = string.Format("UPDATE TSMS_VALID_CODE SET {0}=:AFTER_{0} WHERE {0}=:BEFORE_{0} AND {1}=:{1} AND {2}=:{2} AND {3}=:{3}", _STATUS, _VALID_CODE, _MOBILENO, _TEMPLATE_ID);
            AddParameter("AFTER_" + _STATUS, 1);
            AddParameter("BEFORE_" + _STATUS, 0);
            AddParameter(_VALID_CODE, code);
            AddParameter(_MOBILENO, mobileno);
            AddParameter(_TEMPLATE_ID, template_id);
            return UpdateBySql(sql);
        }

        public int GetSendTimes(string mobileno)
        {
            string sql = "SELECT SUM(SEND_TIMES) FROM TSMS_VALID_CODE WHERE CREATETIME>SYSDATE-1/24/6";
            var res = GetIntValue(sql);
            return res.HasValue ? res.Value : 0;
        }

        public bool SelectEffectiveCode(string mobileno, int template_id)
        {
            string condition = string.Format("{0}=:{0} AND {1}=:{1} AND {2}=:{2} AND EXPIRE_TIME>SYSDATE", _MOBILENO, _TEMPLATE_ID,_STATUS);
            AddParameter(_MOBILENO, mobileno);
            AddParameter(_TEMPLATE_ID, template_id);
            AddParameter(_STATUS, 0);
            return SelectByCondition(condition);
        }
    }
    public partial class Tsms_Valid_CodeCollection
    {

    }
}
