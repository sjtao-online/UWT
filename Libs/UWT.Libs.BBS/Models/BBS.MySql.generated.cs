//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace UWT.Libs.BBS.Models
{

	[Table("uwt_bbs_areas")]
	public partial class UwtBbsArea
	{
		[Column("id"),          PrimaryKey,  Identity] public int    Id        { get; set; } // int(11)
		/// <summary>
		/// 父版块Id
		/// </summary>
		[Column("p_id"),        NotNull              ] public int    PId       { get; set; } // int(11)
		[Column("name"),        NotNull              ] public string Name      { get; set; } // varchar(255)
		/// <summary>
		/// 备注
		/// </summary>
		[Column("desc"),           Nullable          ] public string Desc      { get; set; } // varchar(255)
		/// <summary>
		/// 序号
		/// </summary>
		[Column("index"),       NotNull              ] public int    Index     { get; set; } // int(11)
		/// <summary>
		/// 说明
		/// </summary>
		[Column("summary"),     NotNull              ] public string Summary   { get; set; } // varchar(255)
		[Column("icon"),        NotNull              ] public string Icon      { get; set; } // varchar(255)
		/// <summary>
		/// 版主
		/// </summary>
		[Column("mgr_user_id"), NotNull              ] public int    MgrUserId { get; set; } // int(11)
		[Column("status"),      NotNull              ] public string Status    { get; set; } // enum('show','hidden')
		[Column("apply"),       NotNull              ] public string Apply     { get; set; } // enum('publish','approved')
	}

	[Table("uwt_bbs_area_mgr_ref")]
	public partial class UwtBbsAreaMgrRef
	{
		[Column("id"),    PrimaryKey, Identity] public int    Id    { get; set; } // int(11)
		/// <summary>
		/// 版块Id
		/// </summary>
		[Column("a_id"),  NotNull             ] public int    AId   { get; set; } // int(11)
		/// <summary>
		/// 论坛用户Id
		/// </summary>
		[Column("u_id"),  NotNull             ] public int    UId   { get; set; } // int(11)
		/// <summary>
		/// 权限
		/// </summary>
		[Column("auths"), NotNull             ] public string Auths { get; set; } // set('topic_approved','user_status','topic_top','topic_digest')
	}

	[Table("uwt_bbs_area_topic_ref")]
	public partial class UwtBbsAreaTopicRef
	{
		[Column("id"),     PrimaryKey, Identity] public int    Id     { get; set; } // int(11)
		[Column("t_id"),   NotNull             ] public int    TId    { get; set; } // int(11)
		/// <summary>
		/// 主题内容主体Id
		/// </summary>
		[Column("h_id"),   NotNull             ] public int    HId    { get; set; } // int(11)
		[Column("a_id"),   NotNull             ] public int    AId    { get; set; } // int(11)
		[Column("status"), NotNull             ] public string Status { get; set; } // enum('applying','publish','forbid')
		[Column("ex"),     NotNull             ] public string Ex     { get; set; } // set('digest','top','hot')
	}

	[Table("uwt_bbs_config")]
	public partial class UwtBbsConfig
	{
		[Column("key"),   PrimaryKey,  NotNull] public string Key   { get; set; } // varchar(255)
		[Column("value"),    Nullable         ] public string Value { get; set; } // varchar(255)
	}

	/// <summary>
	/// 关注人与取消关系表
	/// </summary>
	[Table("uwt_bbs_follows")]
	public partial class UwtBbsFollow
	{
		[Column("id"),       PrimaryKey, Identity] public int      Id      { get; set; } // int(11)
		/// <summary>
		/// 补关注的用户Id
		/// </summary>
		[Column("u_id"),     NotNull             ] public int      UId     { get; set; } // int(11)
		/// <summary>
		/// 关注人
		/// </summary>
		[Column("f_id"),     NotNull             ] public int      FId     { get; set; } // int(11)
		/// <summary>
		/// 关注时间
		/// </summary>
		[Column("add_time"), NotNull             ] public DateTime AddTime { get; set; } // datetime
		/// <summary>
		/// 无效代表取消关注了
		/// </summary>
		[Column("valid"),    NotNull             ] public bool     Valid   { get; set; } // tinyint(1)
	}

	[Table("uwt_bbs_topics")]
	public partial class UwtBbsTopic
	{
		[Column("id"),             PrimaryKey, Identity] public int      Id           { get; set; } // int(11)
		/// <summary>
		/// 标题
		/// </summary>
		[Column("title"),          NotNull             ] public string   Title        { get; set; } // varchar(255)
		/// <summary>
		/// 创建者
		/// </summary>
		[Column("create_user_id"), NotNull             ] public int      CreateUserId { get; set; } // int(11)
		/// <summary>
		/// 主题类型分为：讨论，提问，投票
		/// </summary>
		[Column("type"),           NotNull             ] public string   Type         { get; set; } // enum('discuss','question','vote')
		/// <summary>
		/// 根据type不同而意义不同，暂未使用
		/// </summary>
		[Column("type_value"),     NotNull             ] public string   TypeValue    { get; set; } // varchar(255)
		[Column("status"),         NotNull             ] public string   Status       { get; set; } // enum('apply','publish','forbid')
		/// <summary>
		/// 查看次数
		/// </summary>
		[Column("touch_cnt"),      NotNull             ] public int      TouchCnt     { get; set; } // int(11)
		/// <summary>
		/// 创建时间
		/// </summary>
		[Column("add_time"),       NotNull             ] public DateTime AddTime      { get; set; } // datetime
	}

	[Table("uwt_bbs_topic_backs")]
	public partial class UwtBbsTopicBack
	{
		[Column("id"),             PrimaryKey, NotNull] public int      Id           { get; set; } // int(11)
		/// <summary>
		/// 哪个话题
		/// </summary>
		[Column("t_id"),                       NotNull] public int      TId          { get; set; } // int(11)
		/// <summary>
		/// 回复哪条(楼中楼)
		/// </summary>
		[Column("t_b_id"),                     NotNull] public int      TBId         { get; set; } // int(11)
		/// <summary>
		/// 楼层
		/// </summary>
		[Column("index"),                      NotNull] public int      Index        { get; set; } // int(11)
		[Column("add_time"),                   NotNull] public DateTime AddTime      { get; set; } // datetime
		/// <summary>
		/// 状态
		/// </summary>
		[Column("status"),                     NotNull] public string   Status       { get; set; } // enum('normal','disabled','delete')
		[Column("create_user_id"),             NotNull] public int      CreateUserId { get; set; } // int(11)
	}

	[Table("uwt_bbs_topic_back_his")]
	public partial class UwtBbsTopicBackHis
	{
		[Column("id"),       PrimaryKey, Identity] public int      Id      { get; set; } // int(11)
		[Column("t_b_id"),   NotNull             ] public int      TBId    { get; set; } // int(11)
		/// <summary>
		/// 内容
		/// </summary>
		[Column("content"),  NotNull             ] public string   Content { get; set; } // text
		[Column("add_time"), NotNull             ] public DateTime AddTime { get; set; } // datetime
	}

	[Table("uwt_bbs_topic_his")]
	public partial class UwtBbsTopicHis
	{
		[Column("id"),         PrimaryKey,  Identity] public int       Id        { get; set; } // int(11)
		/// <summary>
		/// 主题Id
		/// </summary>
		[Column("t_id"),       NotNull              ] public int       TId       { get; set; } // int(11)
		[Column("title"),      NotNull              ] public string    Title     { get; set; } // varchar(255)
		/// <summary>
		/// 内容
		/// </summary>
		[Column("content"),    NotNull              ] public string    Content   { get; set; } // text
		[Column("status"),     NotNull              ] public string    Status    { get; set; } // enum('draft','wait_apply','publish','forbid')
		[Column("add_time"),   NotNull              ] public DateTime  AddTime   { get; set; } // datetime
		[Column("apply_time"),    Nullable          ] public DateTime? ApplyTime { get; set; } // datetime
		[Column("apply_note"), NotNull              ] public string    ApplyNote { get; set; } // varchar(255)
	}

	/// <summary>
	/// 用户信息
	/// </summary>
	[Table("uwt_bbs_users")]
	public partial class UwtBbsUser
	{
		[Column("id"),            PrimaryKey, Identity] public int      Id          { get; set; } // int(11)
		/// <summary>
		/// 账号Id
		/// </summary>
		[Column("a_id"),          NotNull             ] public int      AId         { get; set; } // int(11)
		[Column("nickname"),      NotNull             ] public string   Nickname    { get; set; } // varchar(255)
		[Column("avatar"),        NotNull             ] public string   Avatar      { get; set; } // varchar(255)
		/// <summary>
		/// 等级类型
		/// </summary>
		[Column("level_type_id"), NotNull             ] public int      LevelTypeId { get; set; } // int(11)
		/// <summary>
		/// 经验
		/// </summary>
		[Column("exp"),           NotNull             ] public uint     Exp         { get; set; } // int(11) unsigned
		[Column("join_time"),     NotNull             ] public DateTime JoinTime    { get; set; } // datetime
		/// <summary>
		/// 执行的操作
		/// </summary>
		[Column("auths"),         NotNull             ] public string   Auths       { get; set; } // set('topic','comment','like','thumbs-up','enter')
		[Column("valid"),         NotNull             ] public bool     Valid       { get; set; } // tinyint(1)
	}

	/// <summary>
	/// 用户等级信息
	/// </summary>
	[Table("uwt_bbs_user_levels")]
	public partial class UwtBbsUserLevel
	{
		[Column("id"),      PrimaryKey, Identity] public int    Id     { get; set; } // int(11)
		[Column("name"),    NotNull             ] public string Name   { get; set; } // varchar(255)
		[Column("avatar"),  NotNull             ] public string Avatar { get; set; } // varchar(255)
		[Column("type_id"), NotNull             ] public int    TypeId { get; set; } // int(11)
		[Column("exp"),     NotNull             ] public uint   Exp    { get; set; } // int(11) unsigned
		[Column("valid"),   NotNull             ] public bool   Valid  { get; set; } // tinyint(1)
	}

	/// <summary>
	/// 用户等级类型
	/// </summary>
	[Table("uwt_bbs_user_level_types")]
	public partial class UwtBbsUserLevelType
	{
		[Column("id"),    PrimaryKey, Identity] public int    Id    { get; set; } // int(11)
		[Column("name"),  NotNull             ] public string Name  { get; set; } // varchar(255)
		[Column("valid"), NotNull             ] public bool   Valid { get; set; } // tinyint(1)
	}

	/// <summary>
	/// 用户可用属性表
	/// </summary>
	[Table("uwt_bbs_user_prop_configs")]
	public partial class UwtBbsUserPropConfig
	{
		[Column("id"),   PrimaryKey, Identity] public int    Id   { get; set; } // int(11)
		/// <summary>
		/// 显示名称
		/// </summary>
		[Column("name"), NotNull             ] public string Name { get; set; } // varchar(255)
		[Column("type"), NotNull             ] public string Type { get; set; } // enum('text','gender','date','time_of_day','weekday','datetime')
		[Column("g_id"), NotNull             ] public int    GId  { get; set; } // int(11)
	}

	/// <summary>
	/// 用户额外信息表
	/// </summary>
	[Table("uwt_bbs_user_properties")]
	public partial class UwtBbsUserProperty
	{
		[Column("id"),    PrimaryKey, Identity] public int    Id    { get; set; } // int(11)
		/// <summary>
		/// 用户Id
		/// </summary>
		[Column("u_id"),  NotNull             ] public int    UId   { get; set; } // int(11)
		/// <summary>
		/// 属性名
		/// </summary>
		[Column("p_id"),  NotNull             ] public int    PId   { get; set; } // int(11)
		/// <summary>
		/// 值
		/// </summary>
		[Column("value"), NotNull             ] public string Value { get; set; } // text
	}

	[Table("uwt_bbs_user_prop_groups")]
	public partial class UwtBbsUserPropGroup
	{
		[Column("id"),   PrimaryKey, Identity] public int    Id   { get; set; } // int(11)
		[Column("name"), NotNull             ] public string Name { get; set; } // varchar(255)
	}

	[Table("uwt_bbs_visit_his")]
	public partial class UwtBbsVisitHis
	{
		[Column("id"),       PrimaryKey, Identity] public int      Id      { get; set; } // int(11)
		/// <summary>
		/// 拜访者Id
		/// </summary>
		[Column("v_id"),     NotNull             ] public int      VId     { get; set; } // int(11)
		/// <summary>
		/// 受访者Id
		/// </summary>
		[Column("u_id"),     NotNull             ] public int      UId     { get; set; } // int(11)
		/// <summary>
		/// 访问的URL
		/// </summary>
		[Column("url"),      NotNull             ] public string   Url     { get; set; } // varchar(255)
		/// <summary>
		/// 访问时间
		/// </summary>
		[Column("add_time"), NotNull             ] public DateTime AddTime { get; set; } // datetime
	}

	public static partial class TableExtensions
	{
		public static UwtBbsArea Find(this ITable<UwtBbsArea> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsArea> TableArea(this DataConnection db)
		{
			return db.GetTable<UwtBbsArea>();
		}

		public static UwtBbsAreaMgrRef Find(this ITable<UwtBbsAreaMgrRef> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsAreaMgrRef> TableAreaMgrRef(this DataConnection db)
		{
			return db.GetTable<UwtBbsAreaMgrRef>();
		}

		public static UwtBbsAreaTopicRef Find(this ITable<UwtBbsAreaTopicRef> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsAreaTopicRef> TableAreaTopicRef(this DataConnection db)
		{
			return db.GetTable<UwtBbsAreaTopicRef>();
		}

		public static UwtBbsConfig Find(this ITable<UwtBbsConfig> table, string Key)
		{
			return table.FirstOrDefault(t =>
				t.Key == Key);
		}

		public static ITable<UwtBbsConfig> TableConfig(this DataConnection db)
		{
			return db.GetTable<UwtBbsConfig>();
		}

		public static UwtBbsFollow Find(this ITable<UwtBbsFollow> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsFollow> TableFollow(this DataConnection db)
		{
			return db.GetTable<UwtBbsFollow>();
		}

		public static UwtBbsTopic Find(this ITable<UwtBbsTopic> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsTopic> TableTopic(this DataConnection db)
		{
			return db.GetTable<UwtBbsTopic>();
		}

		public static UwtBbsTopicBack Find(this ITable<UwtBbsTopicBack> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsTopicBack> TableTopicBack(this DataConnection db)
		{
			return db.GetTable<UwtBbsTopicBack>();
		}

		public static UwtBbsTopicBackHis Find(this ITable<UwtBbsTopicBackHis> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsTopicBackHis> TableTopicBackHis(this DataConnection db)
		{
			return db.GetTable<UwtBbsTopicBackHis>();
		}

		public static UwtBbsTopicHis Find(this ITable<UwtBbsTopicHis> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsTopicHis> TableTopicHis(this DataConnection db)
		{
			return db.GetTable<UwtBbsTopicHis>();
		}

		public static UwtBbsUser Find(this ITable<UwtBbsUser> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsUser> TableUser(this DataConnection db)
		{
			return db.GetTable<UwtBbsUser>();
		}

		public static UwtBbsUserLevel Find(this ITable<UwtBbsUserLevel> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsUserLevel> TableUserLevel(this DataConnection db)
		{
			return db.GetTable<UwtBbsUserLevel>();
		}

		public static UwtBbsUserLevelType Find(this ITable<UwtBbsUserLevelType> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsUserLevelType> TableUserLevelType(this DataConnection db)
		{
			return db.GetTable<UwtBbsUserLevelType>();
		}

		public static UwtBbsUserPropConfig Find(this ITable<UwtBbsUserPropConfig> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsUserPropConfig> TableUserPropConfig(this DataConnection db)
		{
			return db.GetTable<UwtBbsUserPropConfig>();
		}

		public static UwtBbsUserProperty Find(this ITable<UwtBbsUserProperty> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsUserProperty> TableUserProperty(this DataConnection db)
		{
			return db.GetTable<UwtBbsUserProperty>();
		}

		public static UwtBbsUserPropGroup Find(this ITable<UwtBbsUserPropGroup> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsUserPropGroup> TableUserPropGroup(this DataConnection db)
		{
			return db.GetTable<UwtBbsUserPropGroup>();
		}

		public static UwtBbsVisitHis Find(this ITable<UwtBbsVisitHis> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ITable<UwtBbsVisitHis> TableVisitHis(this DataConnection db)
		{
			return db.GetTable<UwtBbsVisitHis>();
		}
	}
}

#pragma warning restore 1591
