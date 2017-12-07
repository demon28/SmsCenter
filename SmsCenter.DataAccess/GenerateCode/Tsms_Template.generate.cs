   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tsms_Template.generate.cs
 * CreateTime : 2017-03-28 15:32:08
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
	public partial class Tsms_Template : DataAccessBase
	{
		#region 构造和基本
		public Tsms_Template():base()
		{}
		public Tsms_Template(DataRow dataRow):base(dataRow)
		{}
		public const string _TEMPLATE_ID = "TEMPLATE_ID";
		public const string _TEMPLATE_NAME = "TEMPLATE_NAME";
		public const string _CONTENT = "CONTENT";
		public const string _TIMESPAN = "TIMESPAN";
		public const string _CODE_SCALAR = "CODE_SCALAR";
		public const string _CREATETIME = "CREATETIME";
		public const string _TEMPLATE_GUID = "TEMPLATE_GUID";
		public const string _TableName = "TSMS_TEMPLATE";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TSMS_TEMPLATE");
			table.Columns.Add(_TEMPLATE_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_TEMPLATE_NAME,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_CONTENT,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_TIMESPAN,typeof(int)).DefaultValue=10;
			table.Columns.Add(_CODE_SCALAR,typeof(int)).DefaultValue=6;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_TEMPLATE_GUID,typeof(string)).DefaultValue=string.Empty;
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
		public int Template_Id
		{
			get{ return Convert.ToInt32(DataRow[_TEMPLATE_ID]);}
			 set{setProperty(_TEMPLATE_ID, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Template_Name
		{
			get{ return DataRow[_TEMPLATE_NAME].ToString();}
			 set{setProperty(_TEMPLATE_NAME, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			get{ return DataRow[_CONTENT].ToString();}
			 set{setProperty(_CONTENT, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Timespan
		{
			get{ return Convert.ToInt32(DataRow[_TIMESPAN]);}
			 set{setProperty(_TIMESPAN, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Code_Scalar
		{
			get{ return Convert.ToInt32(DataRow[_CODE_SCALAR]);}
			 set{setProperty(_CODE_SCALAR, value);}
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
		public string Template_Guid
		{
			get{ return DataRow[_TEMPLATE_GUID].ToString();}
			 set{setProperty(_TEMPLATE_GUID, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT TEMPLATE_ID,TEMPLATE_NAME,CONTENT,TIMESPAN,CODE_SCALAR,CREATETIME,TEMPLATE_GUID FROM TSMS_TEMPLATE WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TSMS_TEMPLATE WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int template_id)
		{
			string condition = " TEMPLATE_ID=:TEMPLATE_ID";
			AddParameter(_TEMPLATE_ID,template_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " TEMPLATE_ID=:TEMPLATE_ID";
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Template_Id = GetSequence("SELECT SEQ_TSMS_TEMPLATE.nextval FROM DUAL");
			string sql = @"INSERT INTO TSMS_TEMPLATE(TEMPLATE_ID,TEMPLATE_NAME,CONTENT,TIMESPAN,CODE_SCALAR,TEMPLATE_GUID)
			VALUES (:TEMPLATE_ID,:TEMPLATE_NAME,:CONTENT,:TIMESPAN,:CODE_SCALAR,:TEMPLATE_GUID)";
			AddParameter(_TEMPLATE_ID,DataRow[_TEMPLATE_ID]);
			AddParameter(_TEMPLATE_NAME,DataRow[_TEMPLATE_NAME]);
			AddParameter(_CONTENT,DataRow[_CONTENT]);
			AddParameter(_TIMESPAN,DataRow[_TIMESPAN]);
			AddParameter(_CODE_SCALAR,DataRow[_CODE_SCALAR]);
			AddParameter(_TEMPLATE_GUID,DataRow[_TEMPLATE_GUID]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tsms_TemplateCollection.Field,object> alterDic,Dictionary<Tsms_TemplateCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tsms_TemplateCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tsms_TemplateCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_TEMPLATE_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TSMS_TEMPLATE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE TEMPLATE_ID=:TEMPLATE_ID");
			AddParameter(_TEMPLATE_ID, DataRow[_TEMPLATE_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByGUID(string template_guid)
		{
			string condition = " TEMPLATE_GUID=:TEMPLATE_GUID";
			AddParameter(_TEMPLATE_GUID,template_guid);
			return SelectByCondition(condition);
		}
		public bool SelectByPk(int template_id)
		{
			string condition = " TEMPLATE_ID=:TEMPLATE_ID";
			AddParameter(_TEMPLATE_ID,template_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tsms_TemplateCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tsms_TemplateCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tsms_Template().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tsms_Template(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tsms_Template._TableName;}
		}
		public Tsms_Template this[int index]
        {
            get { return new Tsms_Template(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Template_Id=0,
			Template_Name=1,
			Content=2,
			Timespan=3,
			Code_Scalar=4,
			Createtime=5,
			Template_Guid=6,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT TEMPLATE_ID,TEMPLATE_NAME,CONTENT,TIMESPAN,CODE_SCALAR,CREATETIME,TEMPLATE_GUID FROM TSMS_TEMPLATE WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tsms_Template Find(Predicate<Tsms_Template> match)
        {
            foreach (Tsms_Template item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tsms_TemplateCollection FindAll(Predicate<Tsms_Template> match)
        {
            Tsms_TemplateCollection list = new Tsms_TemplateCollection();
            foreach (Tsms_Template item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tsms_Template> match)
        {
            foreach (Tsms_Template item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tsms_Template> match)
        {
            BeginTransaction();
            foreach (Tsms_Template item in this)
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