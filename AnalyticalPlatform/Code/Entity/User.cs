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
    [BindIndex("PK_User", true, "id")]
    [BindIndex("IX_User_DataBaseId", false, "DataBaseId")]
    [BindRelation("id", true, "Log", "UserId")]
    [BindRelation("id", true, "Tasks", "UserId")]
    [BindRelation("DataBaseId", false, "DataBase", "id")]
    [BindTable("User", Description = "", ConnName = "AnalyticalPlatform", DbType = DatabaseType.SqlServer)]
    public partial class User : IUser
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
        [BindColumn(2, "Name", "", null, "varchar(50)", 0, 0, false)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private String _Password;
        /// <summary></summary>
        [DisplayName("Password")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "Password", "", null, "varchar(50)", 0, 0, false)]
        public virtual String Password
        {
            get { return _Password; }
            set { if (OnPropertyChanging(__.Password, value)) { _Password = value; OnPropertyChanged(__.Password); } }
        }

        private Int32 _DataBaseId;
        /// <summary></summary>
        [DisplayName("DataBaseId")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(4, "DataBaseId", "", null, "int", 10, 0, false)]
        public virtual Int32 DataBaseId
        {
            get { return _DataBaseId; }
            set { if (OnPropertyChanging(__.DataBaseId, value)) { _DataBaseId = value; OnPropertyChanged(__.DataBaseId); } }
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
                    case __.Password : return _Password;
                    case __.DataBaseId : return _DataBaseId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.id : _id = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Password : _Password = Convert.ToString(value); break;
                    case __.DataBaseId : _DataBaseId = Convert.ToInt32(value); break;
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
            public static readonly Field Password = FindByName(__.Password);

            ///<summary></summary>
            public static readonly Field DataBaseId = FindByName(__.DataBaseId);

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
            public const String Password = "Password";

            ///<summary></summary>
            public const String DataBaseId = "DataBaseId";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IUser
    {
        #region 属性
        /// <summary></summary>
        Int32 id { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        String Password { get; set; }

        /// <summary></summary>
        Int32 DataBaseId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}