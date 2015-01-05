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
    [BindIndex("PK_Log", true, "id")]
    [BindIndex("IX_Log_UserId", false, "UserId")]
    [BindRelation("UserId", false, "User", "id")]
    [BindTable("Log", Description = "", ConnName = "AnalyticalPlatform", DbType = DatabaseType.SqlServer)]
    public partial class Log : ILog
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

        private String _Content;
        /// <summary></summary>
        [DisplayName("Content")]
        [Description("")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn(2, "Content", "", null, "nvarchar(500)", 0, 0, true)]
        public virtual String Content
        {
            get { return _Content; }
            set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } }
        }

        private Int32 _UserId;
        /// <summary></summary>
        [DisplayName("UserId")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(3, "UserId", "", null, "int", 10, 0, false)]
        public virtual Int32 UserId
        {
            get { return _UserId; }
            set { if (OnPropertyChanging(__.UserId, value)) { _UserId = value; OnPropertyChanged(__.UserId); } }
        }

        private DateTime _Date;
        /// <summary></summary>
        [DisplayName("Date")]
        [Description("")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(4, "Date", "", null, "datetime", 3, 0, false)]
        public virtual DateTime Date
        {
            get { return _Date; }
            set { if (OnPropertyChanging(__.Date, value)) { _Date = value; OnPropertyChanged(__.Date); } }
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
                    case __.Content : return _Content;
                    case __.UserId : return _UserId;
                    case __.Date : return _Date;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.id : _id = Convert.ToInt32(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    case __.UserId : _UserId = Convert.ToInt32(value); break;
                    case __.Date : _Date = Convert.ToDateTime(value); break;
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
            public static readonly Field Content = FindByName(__.Content);

            ///<summary></summary>
            public static readonly Field UserId = FindByName(__.UserId);

            ///<summary></summary>
            public static readonly Field Date = FindByName(__.Date);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String id = "id";

            ///<summary></summary>
            public const String Content = "Content";

            ///<summary></summary>
            public const String UserId = "UserId";

            ///<summary></summary>
            public const String Date = "Date";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface ILog
    {
        #region 属性
        /// <summary></summary>
        Int32 id { get; set; }

        /// <summary></summary>
        String Content { get; set; }

        /// <summary></summary>
        Int32 UserId { get; set; }

        /// <summary></summary>
        DateTime Date { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}