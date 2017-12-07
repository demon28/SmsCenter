   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tsms_Thirdparty.generate.cs
 * CreateTime : 2017-03-20 17:09:59
 * CodeGenerateVersion : 1.0.0.0
 * TemplateVersion: 1.0.0
 * E_Mail : zhj.pavel@gmail.com
 * Blog : 
 * Copyright (C) YXH
 * 
 ***************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winner.Framework.Core.DataAccess.Oracle;
using SmsCenter.Entities;
using Winner.Framework.Utils;

namespace SmsCenter.DataAccess
{
	public partial class Tsms_Thirdparty : DataAccessBase
	{
		#region 构造和基本
		public Tsms_Thirdparty():base()
		{}
		public Tsms_Thirdparty(DataRow dataRow):base(dataRow)
		{}
		public const string _APPID = "APPID";
		public const string _USERCODE = "USERCODE";
		public const string _USERPWD = "USERPWD";
		public const string _CREATETIME = "CREATETIME";
		public const string _USAGE_QUOTA = "USAGE_QUOTA";
		public const string _SENDNUM = "SENDNUM";
		public const string _STATUS = "STATUS";
		public const string _CHANNEL_ID = "CHANNEL_ID";
		public const string _REMARKS = "REMARKS";
		public const string _TableName = "TSMS_THIRDPARTY";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TSMS_THIRDPARTY");
			table.Columns.Add(_APPID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_USERCODE,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_USERPWD,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_USAGE_QUOTA,typeof(int)).DefaultValue=DBNull.Value;
			table.Columns.Add(_SENDNUM,typeof(int)).DefaultValue=0;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=0;
			table.Columns.Add(_CHANNEL_ID,typeof(int)).DefaultValue=1;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue=DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 主键
		/// </summary>
		public int Appid
		{
			get{ return Convert.ToInt32(DataRow[_APPID]);}
			 set{setProperty(_APPID, value);}
		}
		/// <summary>
		/// 第三方名称
		/// </summary>
		public string Usercode
		{
			get{ return DataRow[_USERCODE].ToString();}
			 set{setProperty(_USERCODE, value);}
		}
		/// <summary>
		/// 第三方密码(密文)
		/// </summary>
		public string Userpwd
		{
			get{ return DataRow[_USERPWD].ToString();}
			 set{setProperty(_USERPWD, value);}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime Createtime
		{
			get{ return Convert.ToDateTime(DataRow[_CREATETIME]);}
		}
		/// <summary>
		/// 容许发送数量，为空则不限制
		/// </summary>
		public int? Usage_Quota
		{
			get{ return Helper.ToInt32(DataRow[_USAGE_QUOTA]);}
			 set{setProperty(_USAGE_QUOTA, value);}
		}
		/// <summary>
		/// 已发送的数量
		/// </summary>
		public int Sendnum
		{
			get{ return Convert.ToInt32(DataRow[_SENDNUM]);}
			 set{setProperty(_SENDNUM, value);}
		}
		/// <summary>
		/// 状态标识，0为未启用，1广信接口，2梦网接口，3：重庆中商，4：联通一信通，5：台湾互动资通，6：联通一信通高优先级
		/// </summary>
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Channel_Id
		{
			get{ return Convert.ToInt32(DataRow[_CHANNEL_ID]);}
			 set{setProperty(_CHANNEL_ID, value);}
		}
		/// <summary>
		/// 备注（描述此账号的使用场合）
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT APPID,USERCODE,USERPWD,CREATETIME,USAGE_QUOTA,SENDNUM,STATUS,CHANNEL_ID,REMARKS FROM TSMS_THIRDPARTY WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TSMS_THIRDPARTY WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int appid)
		{
			string condition = " APPID=:APPID";
			AddParameter(_APPID,appid);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " APPID=:APPID";
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Appid = GetSequence("SELECT SEQ_TSMS_THIRDPARTY.nextval FROM DUAL");
			string sql = @"INSERT INTO TSMS_THIRDPARTY(APPID,USERCODE,USERPWD,USAGE_QUOTA,SENDNUM,STATUS,CHANNEL_ID,REMARKS)
			VALUES (:APPID,:USERCODE,:USERPWD,:USAGE_QUOTA,:SENDNUM,:STATUS,:CHANNEL_ID,:REMARKS)";
			AddParameter(_APPID,DataRow[_APPID]);
			AddParameter(_USERCODE,DataRow[_USERCODE]);
			AddParameter(_USERPWD,DataRow[_USERPWD]);
			AddParameter(_USAGE_QUOTA,DataRow[_USAGE_QUOTA]);
			AddParameter(_SENDNUM,DataRow[_SENDNUM]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_CHANNEL_ID,DataRow[_CHANNEL_ID]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tsms_ThirdpartyCollection.Field,object> alterDic,Dictionary<Tsms_ThirdpartyCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tsms_ThirdpartyCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tsms_ThirdpartyCollection.Field key in conditionDic.Keys)
            {
                object value = conditionDic[key];
                string name = key.ToString();
				if (alterDic.Keys.Contains(key))
                {
                    name = string.Concat("condition_", key);
                }
                sql.Append(key).Append("=:").Append(name).Append(" and ");
                AddParameter(name, value);
            }
            int len = " and ".Length;
            sql.Remove(sql.Length - len, len);//移除最后一个and
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_APPID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TSMS_THIRDPARTY SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE APPID=:APPID");
			AddParameter(_APPID, DataRow[_APPID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByUSERCODE(string usercode)
		{
			string condition = " USERCODE=:USERCODE";
			AddParameter(_USERCODE,usercode);
			return SelectByCondition(condition);
		}
		public bool SelectByPk(int appid)
		{
			string condition = " APPID=:APPID";
			AddParameter(_APPID,appid);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tsms_ThirdpartyCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tsms_ThirdpartyCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tsms_Thirdparty().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tsms_Thirdparty(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tsms_Thirdparty._TableName;}
		}
		public Tsms_Thirdparty this[int index]
        {
            get { return new Tsms_Thirdparty(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Appid=0,
			Usercode=1,
			Userpwd=2,
			Createtime=3,
			Usage_Quota=4,
			Sendnum=5,
			Status=6,
			Channel_Id=7,
			Remarks=8,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT APPID,USERCODE,USERPWD,CREATETIME,USAGE_QUOTA,SENDNUM,STATUS,CHANNEL_ID,REMARKS FROM TSMS_THIRDPARTY WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tsms_Thirdparty Find(Predicate<Tsms_Thirdparty> match)
        {
            foreach (Tsms_Thirdparty item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tsms_ThirdpartyCollection FindAll(Predicate<Tsms_Thirdparty> match)
        {
            Tsms_ThirdpartyCollection list = new Tsms_ThirdpartyCollection();
            foreach (Tsms_Thirdparty item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tsms_Thirdparty> match)
        {
            foreach (Tsms_Thirdparty item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tsms_Thirdparty> match)
        {
            BeginTransaction();
            foreach (Tsms_Thirdparty item in this)
            {
                item.ReferenceTransactionFrom(Transaction);
                if (!match(item))
                    continue;
                if (!item.Delete())
                {
                    Rollback();
                    return false;
                }
            }
            Commit();
            return true;
        }
		#endregion
		#endregion		
	}
}