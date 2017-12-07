   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tsms_Valid_Code.generate.cs
 * CreateTime : 2017-03-28 16:01:31
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
	public partial class Tsms_Valid_Code : DataAccessBase
	{
		#region 构造和基本
		public Tsms_Valid_Code():base()
		{}
		public Tsms_Valid_Code(DataRow dataRow):base(dataRow)
		{}
		public const string _CODE_ID = "CODE_ID";
		public const string _MOBILENO = "MOBILENO";
		public const string _VALID_CODE = "VALID_CODE";
		public const string _CREATETIME = "CREATETIME";
		public const string _EXPIRE_TIME = "EXPIRE_TIME";
		public const string _TEMPLATE_ID = "TEMPLATE_ID";
		public const string _STATUS = "STATUS";
		public const string _SEND_TIMES = "SEND_TIMES";
		public const string _PRIVATE_VALUE = "PRIVATE_VALUE";
		public const string _TableName = "TSMS_VALID_CODE";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TSMS_VALID_CODE");
			table.Columns.Add(_CODE_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_MOBILENO,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_VALID_CODE,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_EXPIRE_TIME,typeof(DateTime)).DefaultValue=DBNull.Value;
			table.Columns.Add(_TEMPLATE_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=0;
			table.Columns.Add(_SEND_TIMES,typeof(int)).DefaultValue=1;
			table.Columns.Add(_PRIVATE_VALUE,typeof(string)).DefaultValue=DBNull.Value;
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
		public int Code_Id
		{
			get{ return Convert.ToInt32(DataRow[_CODE_ID]);}
			 set{setProperty(_CODE_ID, value);}
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
		public string Valid_Code
		{
			get{ return DataRow[_VALID_CODE].ToString();}
			 set{setProperty(_VALID_CODE, value);}
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
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Send_Times
		{
			get{ return Convert.ToInt32(DataRow[_SEND_TIMES]);}
			 set{setProperty(_SEND_TIMES, value);}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Private_Value
		{
			get{ return DataRow[_PRIVATE_VALUE].ToString();}
			 set{setProperty(_PRIVATE_VALUE, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT CODE_ID,MOBILENO,VALID_CODE,CREATETIME,EXPIRE_TIME,TEMPLATE_ID,STATUS,SEND_TIMES,PRIVATE_VALUE FROM TSMS_VALID_CODE WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TSMS_VALID_CODE WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int code_id)
		{
			string condition = " CODE_ID=:CODE_ID";
			AddParameter(_CODE_ID,code_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " CODE_ID=:CODE_ID";
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Code_Id = GetSequence("SELECT SEQ_TSMS_VALID_CODE.nextval FROM DUAL");
			string sql = @"INSERT INTO TSMS_VALID_CODE(CODE_ID,MOBILENO,VALID_CODE,EXPIRE_TIME,TEMPLATE_ID,STATUS,SEND_TIMES,PRIVATE_VALUE)
			VALUES (:CODE_ID,:MOBILENO,:VALID_CODE,:EXPIRE_TIME,:TEMPLATE_ID,:STATUS,:SEND_TIMES,:PRIVATE_VALUE)";
			AddParameter(_CODE_ID,DataRow[_CODE_ID]);
			AddParameter(_MOBILENO,DataRow[_MOBILENO]);
			AddParameter(_VALID_CODE,DataRow[_VALID_CODE]);
			AddParameter(_EXPIRE_TIME,DataRow[_EXPIRE_TIME]);
			AddParameter(_TEMPLATE_ID,DataRow[_TEMPLATE_ID]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_SEND_TIMES,DataRow[_SEND_TIMES]);
			AddParameter(_PRIVATE_VALUE,DataRow[_PRIVATE_VALUE]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tsms_Valid_CodeCollection.Field,object> alterDic,Dictionary<Tsms_Valid_CodeCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tsms_Valid_CodeCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tsms_Valid_CodeCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_CODE_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TSMS_VALID_CODE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE CODE_ID=:CODE_ID");
			AddParameter(_CODE_ID, DataRow[_CODE_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int code_id)
		{
			string condition = " CODE_ID=:CODE_ID";
			AddParameter(_CODE_ID,code_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tsms_Valid_CodeCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tsms_Valid_CodeCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tsms_Valid_Code().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tsms_Valid_Code(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tsms_Valid_Code._TableName;}
		}
		public Tsms_Valid_Code this[int index]
        {
            get { return new Tsms_Valid_Code(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Code_Id=0,
			Mobileno=1,
			Valid_Code=2,
			Createtime=3,
			Expire_Time=4,
			Template_Id=5,
			Status=6,
			Send_Times=7,
			Private_Value=8,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT CODE_ID,MOBILENO,VALID_CODE,CREATETIME,EXPIRE_TIME,TEMPLATE_ID,STATUS,SEND_TIMES,PRIVATE_VALUE FROM TSMS_VALID_CODE WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tsms_Valid_Code Find(Predicate<Tsms_Valid_Code> match)
        {
            foreach (Tsms_Valid_Code item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tsms_Valid_CodeCollection FindAll(Predicate<Tsms_Valid_Code> match)
        {
            Tsms_Valid_CodeCollection list = new Tsms_Valid_CodeCollection();
            foreach (Tsms_Valid_Code item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tsms_Valid_Code> match)
        {
            foreach (Tsms_Valid_Code item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tsms_Valid_Code> match)
        {
            BeginTransaction();
            foreach (Tsms_Valid_Code item in this)
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