   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tsms_Blacklist.generate.cs
 * CreateTime : 2017-03-20 17:09:57
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
	public partial class Tsms_Blacklist : DataAccessBase
	{
		#region 构造和基本
		public Tsms_Blacklist():base()
		{}
		public Tsms_Blacklist(DataRow dataRow):base(dataRow)
		{}
		public const string _BIZ_ID = "BIZ_ID";
		public const string _MOBILENO = "MOBILENO";
		public const string _CREATETIME = "CREATETIME";
		public const string _EXPIRE_TIME = "EXPIRE_TIME";
		public const string _TableName = "TSMS_BLACKLIST";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TSMS_BLACKLIST");
			table.Columns.Add(_BIZ_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_MOBILENO,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_EXPIRE_TIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Biz_Id
		{
			get{ return Convert.ToInt32(DataRow[_BIZ_ID]);}
			 set{setProperty(_BIZ_ID, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mobileno
		{
			get{ return DataRow[_MOBILENO].ToString();}
			 set{setProperty(_MOBILENO, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Createtime
		{
			get{ return Convert.ToDateTime(DataRow[_CREATETIME]);}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Expire_Time
		{
			get{ return Convert.ToDateTime(DataRow[_EXPIRE_TIME]);}
			 set{setProperty(_EXPIRE_TIME, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT BIZ_ID,MOBILENO,CREATETIME,EXPIRE_TIME FROM TSMS_BLACKLIST WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TSMS_BLACKLIST WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int biz_id)
		{
			string condition = " BIZ_ID=:BIZ_ID";
			AddParameter(_BIZ_ID,biz_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " BIZ_ID=:BIZ_ID";
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Biz_Id = GetSequence("SELECT SEQ_TSMS_BLACKLIST.nextval FROM DUAL");
			string sql = @"INSERT INTO TSMS_BLACKLIST(BIZ_ID,MOBILENO,EXPIRE_TIME)
			VALUES (:BIZ_ID,:MOBILENO,:EXPIRE_TIME)";
			AddParameter(_BIZ_ID,DataRow[_BIZ_ID]);
			AddParameter(_MOBILENO,DataRow[_MOBILENO]);
			AddParameter(_EXPIRE_TIME,DataRow[_EXPIRE_TIME]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tsms_BlacklistCollection.Field,object> alterDic,Dictionary<Tsms_BlacklistCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tsms_BlacklistCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tsms_BlacklistCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_BIZ_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TSMS_BLACKLIST SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE BIZ_ID=:BIZ_ID");
			AddParameter(_BIZ_ID, DataRow[_BIZ_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByMOBILENO(string mobileno)
		{
			string condition = " MOBILENO=:MOBILENO";
			AddParameter(_MOBILENO,mobileno);
			return SelectByCondition(condition);
		}
		public bool SelectByPk(int biz_id)
		{
			string condition = " BIZ_ID=:BIZ_ID";
			AddParameter(_BIZ_ID,biz_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tsms_BlacklistCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tsms_BlacklistCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tsms_Blacklist().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tsms_Blacklist(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tsms_Blacklist._TableName;}
		}
		public Tsms_Blacklist this[int index]
        {
            get { return new Tsms_Blacklist(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Biz_Id=0,
			Mobileno=1,
			Createtime=2,
			Expire_Time=3,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT BIZ_ID,MOBILENO,CREATETIME,EXPIRE_TIME FROM TSMS_BLACKLIST WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tsms_Blacklist Find(Predicate<Tsms_Blacklist> match)
        {
            foreach (Tsms_Blacklist item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tsms_BlacklistCollection FindAll(Predicate<Tsms_Blacklist> match)
        {
            Tsms_BlacklistCollection list = new Tsms_BlacklistCollection();
            foreach (Tsms_Blacklist item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tsms_Blacklist> match)
        {
            foreach (Tsms_Blacklist item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tsms_Blacklist> match)
        {
            BeginTransaction();
            foreach (Tsms_Blacklist item in this)
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