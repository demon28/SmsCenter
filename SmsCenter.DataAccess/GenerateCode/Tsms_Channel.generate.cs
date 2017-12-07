   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tsms_Channel.generate.cs
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
	public partial class Tsms_Channel : DataAccessBase
	{
		#region 构造和基本
		public Tsms_Channel():base()
		{}
		public Tsms_Channel(DataRow dataRow):base(dataRow)
		{}
		public const string _CHANNEL_ID = "CHANNEL_ID";
		public const string _CHANNEL_NAME = "CHANNEL_NAME";
		public const string _ACCESS_NAME = "ACCESS_NAME";
		public const string _ACCESS_KEY = "ACCESS_KEY";
		public const string _SERVICE_URL = "SERVICE_URL";
		public const string _PROVIDER = "PROVIDER";
		public const string _CREATETIME = "CREATETIME";
		public const string _STATUS = "STATUS";
		public const string _REMARKS = "REMARKS";
		public const string _TableName = "TSMS_CHANNEL";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TSMS_CHANNEL");
			table.Columns.Add(_CHANNEL_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_CHANNEL_NAME,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_ACCESS_NAME,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_ACCESS_KEY,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_SERVICE_URL,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_PROVIDER,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=0;
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
		/// 
		/// </summary>
		public int Channel_Id
		{
			get{ return Convert.ToInt32(DataRow[_CHANNEL_ID]);}
			 set{setProperty(_CHANNEL_ID, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Channel_Name
		{
			get{ return DataRow[_CHANNEL_NAME].ToString();}
			 set{setProperty(_CHANNEL_NAME, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Access_Name
		{
			get{ return DataRow[_ACCESS_NAME].ToString();}
			 set{setProperty(_ACCESS_NAME, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Access_Key
		{
			get{ return DataRow[_ACCESS_KEY].ToString();}
			 set{setProperty(_ACCESS_KEY, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Service_Url
		{
			get{ return DataRow[_SERVICE_URL].ToString();}
			 set{setProperty(_SERVICE_URL, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Provider
		{
			get{ return DataRow[_PROVIDER].ToString();}
			 set{setProperty(_PROVIDER, value);}
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
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 
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
			string sql = "SELECT CHANNEL_ID,CHANNEL_NAME,ACCESS_NAME,ACCESS_KEY,SERVICE_URL,PROVIDER,CREATETIME,STATUS,REMARKS FROM TSMS_CHANNEL WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TSMS_CHANNEL WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int channel_id)
		{
			string condition = " CHANNEL_ID=:CHANNEL_ID";
			AddParameter(_CHANNEL_ID,channel_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " CHANNEL_ID=:CHANNEL_ID";
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Channel_Id = GetSequence("SELECT SEQ_TSMS_CHANNEL.nextval FROM DUAL");
			string sql = @"INSERT INTO TSMS_CHANNEL(CHANNEL_ID,CHANNEL_NAME,ACCESS_NAME,ACCESS_KEY,SERVICE_URL,PROVIDER,STATUS,REMARKS)
			VALUES (:CHANNEL_ID,:CHANNEL_NAME,:ACCESS_NAME,:ACCESS_KEY,:SERVICE_URL,:PROVIDER,:STATUS,:REMARKS)";
			AddParameter(_CHANNEL_ID,DataRow[_CHANNEL_ID]);
			AddParameter(_CHANNEL_NAME,DataRow[_CHANNEL_NAME]);
			AddParameter(_ACCESS_NAME,DataRow[_ACCESS_NAME]);
			AddParameter(_ACCESS_KEY,DataRow[_ACCESS_KEY]);
			AddParameter(_SERVICE_URL,DataRow[_SERVICE_URL]);
			AddParameter(_PROVIDER,DataRow[_PROVIDER]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tsms_ChannelCollection.Field,object> alterDic,Dictionary<Tsms_ChannelCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tsms_ChannelCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tsms_ChannelCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_CHANNEL_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TSMS_CHANNEL SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE CHANNEL_ID=:CHANNEL_ID");
			AddParameter(_CHANNEL_ID, DataRow[_CHANNEL_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int channel_id)
		{
			string condition = " CHANNEL_ID=:CHANNEL_ID";
			AddParameter(_CHANNEL_ID,channel_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tsms_ChannelCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tsms_ChannelCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tsms_Channel().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tsms_Channel(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tsms_Channel._TableName;}
		}
		public Tsms_Channel this[int index]
        {
            get { return new Tsms_Channel(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Channel_Id=0,
			Channel_Name=1,
			Access_Name=2,
			Access_Key=3,
			Service_Url=4,
			Provider=5,
			Createtime=6,
			Status=7,
			Remarks=8,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT CHANNEL_ID,CHANNEL_NAME,ACCESS_NAME,ACCESS_KEY,SERVICE_URL,PROVIDER,CREATETIME,STATUS,REMARKS FROM TSMS_CHANNEL WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tsms_Channel Find(Predicate<Tsms_Channel> match)
        {
            foreach (Tsms_Channel item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tsms_ChannelCollection FindAll(Predicate<Tsms_Channel> match)
        {
            Tsms_ChannelCollection list = new Tsms_ChannelCollection();
            foreach (Tsms_Channel item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tsms_Channel> match)
        {
            foreach (Tsms_Channel item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tsms_Channel> match)
        {
            BeginTransaction();
            foreach (Tsms_Channel item in this)
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