﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace AnalyticalPlatform
{
    /// <summary></summary>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("PK_Task", true, "id")]
    [BindIndex("IX_Tasks_UserId", false, "UserId")]
    [BindRelation("UserId", false, "User", "id")]
    [BindTable("Tasks", Description = "", ConnName = "AnalyticalPlatform", DbType = DatabaseType.SqlServer)]
    public partial class Tasks : ITasks
    {
        #region 属性
        private Int32 _id;
        /// <summary></summary>
        [DisplayName("id")]
        [Description("")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn(1, "id", "", null, "int", 10, 0, false)]
        public virtual Int32 id
        {
            get { return _id; }
            set { if (OnPropertyChanging(__.id, value)) { _id = value; OnPropertyChanged(__.id); } }
        }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(2, "Name", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private String _Content;
        /// <summary></summary>
        [DisplayName("Content")]
        [Description("")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn(3, "Content", "", null, "nvarchar(500)", 0, 0, true)]
        public virtual String Content
        {
            get { return _Content; }
            set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } }
        }

        private DateTime _DateTime;
        /// <summary></summary>
        [DisplayName("DateTime")]
        [Description("")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(4, "DateTime", "", null, "datetime", 3, 0, false)]
        public virtual DateTime DateTime
        {
            get { return _DateTime; }
            set { if (OnPropertyChanging(__.DateTime, value)) { _DateTime = value; OnPropertyChanged(__.DateTime); } }
        }

        private Int32 _UserId;
        /// <summary></summary>
        [DisplayName("UserId")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(5, "UserId", "", null, "int", 10, 0, false)]
        public virtual Int32 UserId
        {
            get { return _UserId; }
            set { if (OnPropertyChanging(__.UserId, value)) { _UserId = value; OnPropertyChanged(__.UserId); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.id : return _id;
                    case __.Name : return _Name;
                    case __.Content : return _Content;
                    case __.DateTime : return _DateTime;
                    case __.UserId : return _UserId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.id : _id = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    case __.DateTime : _DateTime = Convert.ToDateTime(value); break;
                    case __.UserId : _UserId = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field id = FindByName(__.id);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field Content = FindByName(__.Content);

            ///<summary></summary>
            public static readonly Field DateTime = FindByName(__.DateTime);

            ///<summary></summary>
            public static readonly Field UserId = FindByName(__.UserId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String id = "id";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String Content = "Content";

            ///<summary></summary>
            public const String DateTime = "DateTime";

            ///<summary></summary>
            public const String UserId = "UserId";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface ITasks
    {
        #region 属性
        /// <summary></summary>
        Int32 id { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        String Content { get; set; }

        /// <summary></summary>
        DateTime DateTime { get; set; }

        /// <summary></summary>
        Int32 UserId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}