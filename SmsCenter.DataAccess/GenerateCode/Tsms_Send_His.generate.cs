   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tsms_Send_His.generate.cs
 * CreateTime : 2017-03-20 17:09:58
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

namespace SmsCenter.DataAccess
{
	public partial class Tsms_Send_His : DataAccessBase
	{
		#region 构造和基本
		public Tsms_Send_His():base()
		{}
		public Tsms_Send_His(DataRow dataRow):base(dataRow)
		{}
		public const string _HISID = "HISID";
		public const string _MOBILENO = "MOBILENO";
		public const string _SMSCONTENT = "SMSCONTENT";
		public const string _APPID = "APPID";
		public const string _SENDTIME = "SENDTIME";
		public const string _SENDTYPE = "SENDTYPE";
		public const string _REMARKS = "REMARKS";
		public const string _CREATETIME = "CREATETIME";
		public const string _RETURNTYPE = "RETURNTYPE";
		public const string _SENDNO = "SENDNO";
		public const string _BATCH_ID = "BATCH_ID";
		public const string _TableName = "TSMS_SEND_HIS";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TSMS_SEND_HIS");
			table.Columns.Add(_HISID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_MOBILENO,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_SMSCONTENT,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_APPID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_SENDTIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_SENDTYPE,typeof(int)).DefaultValue=0;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_RETURNTYPE,typeof(int)).DefaultValue=0;
			table.Columns.Add(_SENDNO,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_BATCH_ID,typeof(string)).DefaultValue=DBNull.Value;
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
		public int Hisid
		{
			get{ return Convert.ToInt32(DataRow[_HISID]);}
			 set{setProperty(_HISID, value);}
		}
		/// <summary>
		/// 手机号码，多个号码时以分号（；）隔开
		/// </summary>
		public string Mobileno
		{
			get{ return DataRow[_MOBILENO].ToString();}
			 set{setProperty(_MOBILENO, value);}
		}
		/// <summary>
		/// 短信内容
		/// </summary>
		public string Smscontent
		{
			get{ return DataRow[_SMSCONTENT].ToString();}
			 set{setProperty(_SMSCONTENT, value);}
		}
		/// <summary>
		/// 平台ID
		/// </summary>
		public int Appid
		{
			get{ return Convert.ToInt32(DataRow[_APPID]);}
			 set{setProperty(_APPID, value);}
		}
		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime Sendtime
		{
			get{ return Convert.ToDateTime(DataRow[_SENDTIME]);}
			 set{setProperty(_SENDTIME, value);}
		}
		/// <summary>
		/// 发送类型，用枚举定义：0为单条立刻发生；1为批量立刻发送；2为单条定时发送，3为批量定时发送
		/// </summary>
		public int Sendtype
		{
			get{ return Convert.ToInt32(DataRow[_SENDTYPE]);}
			 set{setProperty(_SENDTYPE, value);}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime Createtime
		{
			get{ return Convert.ToDateTime(DataRow[_CREATETIME]);}
		}
		/// <summary>
		/// 回复类型，有枚举定义：0为不回复，1为可回复（已作废）
		/// </summary>
		public int Returntype
		{
			get{ return Convert.ToInt32(DataRow[_RETURNTYPE]);}
			 set{setProperty(_RETURNTYPE, value);}
		}
		/// <summary>
		/// 发送成功后返回的编号
		/// </summary>
		public string Sendno
		{
			get{ return DataRow[_SENDNO].ToString();}
			 set{setProperty(_SENDNO, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Batch_Id
		{
			get{ return DataRow[_BATCH_ID].ToString();}
			 set{setProperty(_BATCH_ID, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT HISID,MOBILENO,SMSCONTENT,APPID,SENDTIME,SENDTYPE,REMARKS,CREATETIME,RETURNTYPE,SENDNO,BATCH_ID FROM TSMS_SEND_HIS WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TSMS_SEND_HIS WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int hisid)
		{
			string condition = " HISID=:HISID";
			AddParameter(_HISID,hisid);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " HISID=:HISID";
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Hisid = GetSequence("SELECT SEQ_TSMS_SEND_HIS.nextval FROM DUAL");
			string sql = @"INSERT INTO TSMS_SEND_HIS(HISID,MOBILENO,SMSCONTENT,APPID,SENDTIME,SENDTYPE,REMARKS,RETURNTYPE,SENDNO,BATCH_ID)
			VALUES (:HISID,:MOBILENO,:SMSCONTENT,:APPID,:SENDTIME,:SENDTYPE,:REMARKS,:RETURNTYPE,:SENDNO,:BATCH_ID)";
			AddParameter(_HISID,DataRow[_HISID]);
			AddParameter(_MOBILENO,DataRow[_MOBILENO]);
			AddParameter(_SMSCONTENT,DataRow[_SMSCONTENT]);
			AddParameter(_APPID,DataRow[_APPID]);
			AddParameter(_SENDTIME,DataRow[_SENDTIME]);
			AddParameter(_SENDTYPE,DataRow[_SENDTYPE]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_RETURNTYPE,DataRow[_RETURNTYPE]);
			AddParameter(_SENDNO,DataRow[_SENDNO]);
			AddParameter(_BATCH_ID,DataRow[_BATCH_ID]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tsms_Send_HisCollection.Field,object> alterDic,Dictionary<Tsms_Send_HisCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tsms_Send_HisCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tsms_Send_HisCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_HISID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TSMS_SEND_HIS SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE HISID=:HISID");
			AddParameter(_HISID, DataRow[_HISID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int hisid)
		{
			string condition = " HISID=:HISID";
			AddParameter(_HISID,hisid);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tsms_Send_HisCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tsms_Send_HisCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tsms_Send_His().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tsms_Send_His(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tsms_Send_His._TableName;}
		}
		public Tsms_Send_His this[int index]
        {
            get { return new Tsms_Send_His(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Hisid=0,
			Mobileno=1,
			Smscontent=2,
			Appid=3,
			Sendtime=4,
			Sendtype=5,
			Remarks=6,
			Createtime=7,
			Returntype=8,
			Sendno=9,
			Batch_Id=10,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT HISID,MOBILENO,SMSCONTENT,APPID,SENDTIME,SENDTYPE,REMARKS,CREATETIME,RETURNTYPE,SENDNO,BATCH_ID FROM TSMS_SEND_HIS WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tsms_Send_His Find(Predicate<Tsms_Send_His> match)
        {
            foreach (Tsms_Send_His item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tsms_Send_HisCollection FindAll(Predicate<Tsms_Send_His> match)
        {
            Tsms_Send_HisCollection list = new Tsms_Send_HisCollection();
            foreach (Tsms_Send_His item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tsms_Send_His> match)
        {
            foreach (Tsms_Send_His item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tsms_Send_His> match)
        {
            BeginTransaction();
            foreach (Tsms_Send_His item in this)
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