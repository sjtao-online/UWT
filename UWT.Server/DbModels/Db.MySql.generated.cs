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
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : uwt
	/// Data Source    : 47.92.200.192
	/// Server Version : 5.7.17-log
	/// </summary>
	public partial class UwtDB : LinqToDB.Data.DataConnection
	{
		public ITable<UwtBbsArea>            UwtBbsAreas            { get { return this.GetTable<UwtBbsArea>(); } }
		public ITable<UwtBbsAreaMgrRef>      UwtBbsAreaMgrRefs      { get { return this.GetTable<UwtBbsAreaMgrRef>(); } }
		public ITable<UwtBbsAreaTopicRef>    UwtBbsAreaTopicRefs    { get { return this.GetTable<UwtBbsAreaTopicRef>(); } }
		public ITable<UwtBbsConfig>          UwtBbsConfigs          { get { return this.GetTable<UwtBbsConfig>(); } }
		/// <summary>
		/// 关注人与取消关系表
		/// </summary>
		public ITable<UwtBbsFollow>          UwtBbsFollows          { get { return this.GetTable<UwtBbsFollow>(); } }
		public ITable<UwtBbsTopic>           UwtBbsTopics           { get { return this.GetTable<UwtBbsTopic>(); } }
		public ITable<UwtBbsTopicBack>       UwtBbsTopicBacks       { get { return this.GetTable<UwtBbsTopicBack>(); } }
		public ITable<UwtBbsTopicBackHis>    UwtBbsTopicBackHis     { get { return this.GetTable<UwtBbsTopicBackHis>(); } }
		public ITable<UwtBbsTopicHis>        UwtBbsTopicHis         { get { return this.GetTable<UwtBbsTopicHis>(); } }
		/// <summary>
		/// 用户信息
		/// </summary>
		public ITable<UwtBbsUser>            UwtBbsUsers            { get { return this.GetTable<UwtBbsUser>(); } }
		/// <summary>
		/// 用户等级信息
		/// </summary>
		public ITable<UwtBbsUserLevel>       UwtBbsUserLevels       { get { return this.GetTable<UwtBbsUserLevel>(); } }
		/// <summary>
		/// 用户等级类型
		/// </summary>
		public ITable<UwtBbsUserLevelType>   UwtBbsUserLevelTypes   { get { return this.GetTable<UwtBbsUserLevelType>(); } }
		/// <summary>
		/// 用户可用属性表
		/// </summary>
		public ITable<UwtBbsUserPropConfig>  UwtBbsUserPropConfigs  { get { return this.GetTable<UwtBbsUserPropConfig>(); } }
		/// <summary>
		/// 用户额外信息表
		/// </summary>
		public ITable<UwtBbsUserProperty>    UwtBbsUserProperties   { get { return this.GetTable<UwtBbsUserProperty>(); } }
		public ITable<UwtBbsUserPropGroup>   UwtBbsUserPropGroups   { get { return this.GetTable<UwtBbsUserPropGroup>(); } }
		public ITable<UwtBbsVisitHis>        UwtBbsVisitHis         { get { return this.GetTable<UwtBbsVisitHis>(); } }
		public ITable<UwtHelper>             UwtHelpers             { get { return this.GetTable<UwtHelper>(); } }
		public ITable<UwtNormalsBanner>      UwtNormalsBanners      { get { return this.GetTable<UwtNormalsBanner>(); } }
		public ITable<UwtNormalsFile>        UwtNormalsFiles        { get { return this.GetTable<UwtNormalsFile>(); } }
		public ITable<UwtNormalsNews>        UwtNormalsNews         { get { return this.GetTable<UwtNormalsNews>(); } }
		public ITable<UwtNormalsNewsCate>    UwtNormalsNewsCates    { get { return this.GetTable<UwtNormalsNewsCate>(); } }
		public ITable<UwtNormalsVersion>     UwtNormalsVersions     { get { return this.GetTable<UwtNormalsVersion>(); } }
		public ITable<UwtUsersAccount>       UwtUsersAccounts       { get { return this.GetTable<UwtUsersAccount>(); } }
		public ITable<UwtUsersLoginHis>      UwtUsersLoginHis       { get { return this.GetTable<UwtUsersLoginHis>(); } }
		public ITable<UwtUsersMenuGroup>     UwtUsersMenuGroups     { get { return this.GetTable<UwtUsersMenuGroup>(); } }
		public ITable<UwtUsersMenuGroupItem> UwtUsersMenuGroupItems { get { return this.GetTable<UwtUsersMenuGroupItem>(); } }
		public ITable<UwtUsersModule>        UwtUsersModules        { get { return this.GetTable<UwtUsersModule>(); } }
		public ITable<UwtUsersRole>          UwtUsersRoles          { get { return this.GetTable<UwtUsersRole>(); } }
		public ITable<UwtUsersRoleModuleRef> UwtUsersRoleModuleRefs { get { return this.GetTable<UwtUsersRoleModuleRef>(); } }
		public ITable<UwtWechatsUser>        UwtWechatsUsers        { get { return this.GetTable<UwtWechatsUser>(); } }

		public UwtDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public UwtDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

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

	[Table("uwt_helpers")]
	public partial class UwtHelper
	{
		[Column("id"),           PrimaryKey,  Identity] public int       Id          { get; set; } // int(11)
		[Column("title"),        NotNull              ] public string    Title       { get; set; } // varchar(255)
		[Column("content"),         Nullable          ] public string    Content     { get; set; } // text
		[Column("summary"),      NotNull              ] public string    Summary     { get; set; } // varchar(255)
		[Column("publish_time"),    Nullable          ] public DateTime? PublishTime { get; set; } // datetime
		/// <summary>
		/// 可以是多个url以;分开，但必须为小写
		/// </summary>
		[Column("url"),          NotNull              ] public string    Url         { get; set; } // text
		/// <summary>
		/// 作者名称（随便写的东西）
		/// </summary>
		[Column("author"),       NotNull              ] public string    Author      { get; set; } // varchar(255)
		/// <summary>
		/// 编辑者Id
		/// </summary>
		[Column("modify_id"),    NotNull              ] public int       ModifyId    { get; set; } // int(11)
		/// <summary>
		/// 创建者Id
		/// </summary>
		[Column("creator_id"),   NotNull              ] public int       CreatorId   { get; set; } // int(11)
		[Column("add_time"),     NotNull              ] public DateTime  AddTime     { get; set; } // datetime
		[Column("update_time"),  NotNull              ] public DateTime  UpdateTime  { get; set; } // datetime
		[Column("valid"),        NotNull              ] public bool      Valid       { get; set; } // tinyint(1)
	}

	[Table("uwt_normals_banners")]
	public partial class UwtNormalsBanner
	{
		[Column("id"),          PrimaryKey,  Identity] public int      Id         { get; set; } // int(11)
		[Column("target"),         Nullable          ] public string   Target     { get; set; } // varchar(255)
		[Column("target_type"), NotNull              ] public string   TargetType { get; set; } // enum('Mini','Web','Page','None')
		[Column("image"),       NotNull              ] public string   Image      { get; set; } // varchar(255)
		[Column("cate"),        NotNull              ] public string   Cate       { get; set; } // enum('Banner','Environment','Course')
		[Column("title"),          Nullable          ] public string   Title      { get; set; } // varchar(15)
		[Column("sub_title"),      Nullable          ] public string   SubTitle   { get; set; } // varchar(15)
		[Column("desc"),           Nullable          ] public string   Desc       { get; set; } // varchar(255)
		[Column("index"),       NotNull              ] public int      Index      { get; set; } // int(11)
		[Column("add_time"),    NotNull              ] public DateTime AddTime    { get; set; } // timestamp
		[Column("update_time"), NotNull              ] public DateTime UpdateTime { get; set; } // timestamp
		[Column("valid"),       NotNull              ] public bool     Valid      { get; set; } // tinyint(1)
	}

	[Table("uwt_normals_files")]
	public partial class UwtNormalsFile
	{
		[Column("id"),             PrimaryKey,  Identity] public int      Id           { get; set; } // int(11)
		[Column("filename"),       NotNull              ] public string   Filename     { get; set; } // varchar(255)
		[Column("desc"),              Nullable          ] public string   Desc         { get; set; } // varchar(255)
		[Column("path"),              Nullable          ] public string   Path         { get; set; } // varchar(255)
		[Column("file_size"),      NotNull              ] public long     FileSize     { get; set; } // bigint(20)
		[Column("type"),           NotNull              ] public string   Type         { get; set; } // varchar(63)
		[Column("add_account_id"), NotNull              ] public int      AddAccountId { get; set; } // int(11)
		[Column("add_time"),       NotNull              ] public DateTime AddTime      { get; set; } // timestamp
	}

	[Table("uwt_normals_news")]
	public partial class UwtNormalsNews
	{
		[Column("id"),          PrimaryKey,  Identity] public int      Id         { get; set; } // int(11)
		[Column("title"),       NotNull              ] public string   Title      { get; set; } // varchar(255)
		[Column("summary"),     NotNull              ] public string   Summary    { get; set; } // varchar(255)
		[Column("content"),        Nullable          ] public string   Content    { get; set; } // longtext
		[Column("add_time"),    NotNull              ] public DateTime AddTime    { get; set; } // timestamp
		[Column("update_time"), NotNull              ] public DateTime UpdateTime { get; set; } // timestamp
		[Column("valid"),       NotNull              ] public bool     Valid      { get; set; } // tinyint(1)
	}

	[Table("uwt_normals_news_cates")]
	public partial class UwtNormalsNewsCate
	{
		[Column("id"),          PrimaryKey,  Identity] public int      Id         { get; set; } // int(11)
		[Column("title"),       NotNull              ] public string   Title      { get; set; } // varchar(255)
		[Column("sub_title"),      Nullable          ] public string   SubTitle   { get; set; } // varchar(255)
		[Column("desc"),           Nullable          ] public string   Desc       { get; set; } // varchar(255)
		[Column("p_id"),        NotNull              ] public int      PId        { get; set; } // int(11)
		[Column("large_icon"),  NotNull              ] public string   LargeIcon  { get; set; } // varchar(255)
		[Column("icon"),        NotNull              ] public string   Icon       { get; set; } // varchar(255)
		[Column("mini_icon"),   NotNull              ] public string   MiniIcon   { get; set; } // varchar(255)
		[Column("valid"),       NotNull              ] public bool     Valid      { get; set; } // tinyint(1)
		[Column("add_time"),    NotNull              ] public DateTime AddTime    { get; set; } // timestamp
		[Column("update_time"), NotNull              ] public DateTime UpdateTime { get; set; } // timestamp
	}

	[Table("uwt_normals_versions")]
	public partial class UwtNormalsVersion
	{
		[Column("id"),           PrimaryKey, Identity] public int      Id          { get; set; } // int(11)
		/// <summary>
		/// 版本名称
		/// </summary>
		[Column("name"),         NotNull             ] public string   Name        { get; set; } // varchar(255)
		/// <summary>
		/// 版本号 显示用
		/// </summary>
		[Column("version"),      NotNull             ] public string   Version     { get; set; } // varchar(255)
		/// <summary>
		/// 日志
		/// </summary>
		[Column("logs"),         NotNull             ] public string   Logs        { get; set; } // text
		/// <summary>
		/// 数值版本号，用于排序，越大版本越新
		/// </summary>
		[Column("version_no"),   NotNull             ] public int      VersionNo   { get; set; } // int(11)
		/// <summary>
		/// 下载地址
		/// </summary>
		[Column("path"),         NotNull             ] public string   Path        { get; set; } // varchar(255)
		/// <summary>
		/// 类型
		/// </summary>
		[Column("type"),         NotNull             ] public string   Type        { get; set; } // varchar(255)
		/// <summary>
		/// 发布时间
		/// </summary>
		[Column("publish_time"), NotNull             ] public DateTime PublishTime { get; set; } // datetime
		/// <summary>
		/// 编译时间
		/// </summary>
		[Column("build_time"),   NotNull             ] public DateTime BuildTime   { get; set; } // datetime
		/// <summary>
		/// 添加时间
		/// </summary>
		[Column("add_time"),     NotNull             ] public DateTime AddTime     { get; set; } // datetime
		/// <summary>
		/// 有效性
		/// </summary>
		[Column("valid"),        NotNull             ] public bool     Valid       { get; set; } // tinyint(1)
	}

	[Table("uwt_users_accounts")]
	public partial class UwtUsersAccount
	{
		[Column("id"),              PrimaryKey,  Identity] public int       Id            { get; set; } // int(11)
		[Column("account"),            Nullable          ] public string    Account       { get; set; } // varchar(64)
		[Column("password"),           Nullable          ] public string    Password      { get; set; } // varchar(40)
		/// <summary>
		/// 账号类型
		/// </summary>
		[Column("type"),            NotNull              ] public string    Type          { get; set; } // varchar(32)
		[Column("role_id"),         NotNull              ] public int       RoleId        { get; set; } // int(11)
		[Column("status"),          NotNull              ] public string    Status        { get; set; } // enum('enabled','disabled','writenoff')
		[Column("last_login_time"),    Nullable          ] public DateTime? LastLoginTime { get; set; } // datetime
		[Column("add_time"),        NotNull              ] public DateTime  AddTime       { get; set; } // timestamp
	}

	[Table("uwt_users_login_his")]
	public partial class UwtUsersLoginHis
	{
		[Column("id"),       PrimaryKey, Identity] public int      Id       { get; set; } // int(11)
		/// <summary>
		/// 填写的用户名
		/// </summary>
		[Column("username"), NotNull             ] public string   Username { get; set; } // varchar(255)
		/// <summary>
		/// 填写的密码
		/// </summary>
		[Column("pwd"),      NotNull             ] public string   Pwd      { get; set; } // varchar(255)
		[Column("type"),     NotNull             ] public string   Type     { get; set; } // varchar(32)
		/// <summary>
		/// 可能的用户为0是登录时用户名不对
		/// </summary>
		[Column("a_id"),     NotNull             ] public int      AId      { get; set; } // int(11)
		/// <summary>
		/// 0为失败 1为成功
		/// </summary>
		[Column("status"),   NotNull             ] public bool     Status   { get; set; } // tinyint(1)
		/// <summary>
		/// 操作时间
		/// </summary>
		[Column("add_time"), NotNull             ] public DateTime AddTime  { get; set; } // timestamp
	}

	[Table("uwt_users_menu_groups")]
	public partial class UwtUsersMenuGroup
	{
		[Column("id"),         PrimaryKey,  Identity] public int      Id        { get; set; } // int(11)
		[Column("name"),       NotNull              ] public string   Name      { get; set; } // varchar(255)
		[Column("desc"),          Nullable          ] public string   Desc      { get; set; } // varchar(255)
		[Column("page_count"), NotNull              ] public int      PageCount { get; set; } // int(11)
		[Column("auth_count"), NotNull              ] public int      AuthCount { get; set; } // int(11)
		[Column("add_time"),   NotNull              ] public DateTime AddTime   { get; set; } // timestamp
		[Column("valid"),      NotNull              ] public bool     Valid     { get; set; } // tinyint(1)
	}

	[Table("uwt_users_menu_group_items")]
	public partial class UwtUsersMenuGroupItem
	{
		[Column("id"),          PrimaryKey,  Identity] public int      Id         { get; set; } // int(11)
		[Column("title"),       NotNull              ] public string   Title      { get; set; } // varchar(255)
		/// <summary>
		/// 图标
		/// </summary>
		[Column("icon"),        NotNull              ] public string   Icon       { get; set; } // varchar(255)
		/// <summary>
		/// 显示提示
		/// </summary>
		[Column("tooltip"),     NotNull              ] public string   Tooltip    { get; set; } // varchar(255)
		/// <summary>
		/// 说明
		/// </summary>
		[Column("desc"),           Nullable          ] public string   Desc       { get; set; } // varchar(255)
		[Column("pid"),         NotNull              ] public int      Pid        { get; set; } // int(11)
		[Column("group_id"),    NotNull              ] public int      GroupId    { get; set; } // int(11)
		/// <summary>
		/// Module的ID
		/// </summary>
		[Column("url"),         NotNull              ] public int      Url        { get; set; } // int(11)
		[Column("index"),       NotNull              ] public int      Index      { get; set; } // int(11)
		[Column("add_time"),    NotNull              ] public DateTime AddTime    { get; set; } // timestamp
		[Column("update_time"), NotNull              ] public DateTime UpdateTime { get; set; } // timestamp
		[Column("valid"),       NotNull              ] public bool     Valid      { get; set; } // tinyint(1)
	}

	[Table("uwt_users_modules")]
	public partial class UwtUsersModule
	{
		[Column("id"),   PrimaryKey, Identity] public int    Id   { get; set; } // int(11)
		[Column("name"), NotNull             ] public string Name { get; set; } // varchar(64)
		[Column("url"),  NotNull             ] public string Url  { get; set; } // varchar(255)
		[Column("type"), NotNull             ] public string Type { get; set; } // enum('page','api')
	}

	[Table("uwt_users_roles")]
	public partial class UwtUsersRole
	{
		[Column("id"),            PrimaryKey,  Identity] public int      Id          { get; set; } // int(11)
		/// <summary>
		/// 角色名
		/// </summary>
		[Column("name"),          NotNull              ] public string   Name        { get; set; } // varchar(255)
		/// <summary>
		/// 角色主页地址
		/// </summary>
		[Column("home_page_url"), NotNull              ] public string   HomePageUrl { get; set; } // varchar(255)
		[Column("desc"),             Nullable          ] public string   Desc        { get; set; } // varchar(255)
		[Column("menu_group_id"), NotNull              ] public int      MenuGroupId { get; set; } // int(11)
		[Column("add_time"),      NotNull              ] public DateTime AddTime     { get; set; } // timestamp
		[Column("valid"),         NotNull              ] public bool     Valid       { get; set; } // tinyint(1)
	}

	[Table("uwt_users_role_module_ref")]
	public partial class UwtUsersRoleModuleRef
	{
		[Column("id"),   PrimaryKey, Identity] public int Id  { get; set; } // int(11)
		[Column("r_id"), NotNull             ] public int RId { get; set; } // int(11)
		[Column("m_id"), NotNull             ] public int MId { get; set; } // int(11)
	}

	[Table("uwt_wechats_users")]
	public partial class UwtWechatsUser
	{
		[Column("id"),         PrimaryKey, Identity] public int      Id        { get; set; } // int(11)
		[Column("nick_name"),  NotNull             ] public string   NickName  { get; set; } // varchar(255)
		[Column("token"),      NotNull             ] public string   Token     { get; set; } // varchar(32)
		[Column("token_exp"),  NotNull             ] public DateTime TokenExp  { get; set; } // datetime
		[Column("open_id"),    NotNull             ] public string   OpenId    { get; set; } // varchar(64)
		[Column("gender"),     NotNull             ] public sbyte    Gender    { get; set; } // tinyint(4)
		[Column("language"),   NotNull             ] public string   Language  { get; set; } // varchar(16)
		[Column("country"),    NotNull             ] public string   Country   { get; set; } // varchar(32)
		[Column("city"),       NotNull             ] public string   City      { get; set; } // varchar(32)
		[Column("province"),   NotNull             ] public string   Province  { get; set; } // varchar(32)
		[Column("avatar_url"), NotNull             ] public string   AvatarUrl { get; set; } // varchar(255)
		[Column("add_time"),   NotNull             ] public DateTime AddTime   { get; set; } // datetime
		[Column("status"),     NotNull             ] public string   Status    { get; set; } // enum('enabled')
	}

	public static partial class TableExtensions
	{
		public static UwtBbsArea Find(this ITable<UwtBbsArea> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsAreaMgrRef Find(this ITable<UwtBbsAreaMgrRef> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsAreaTopicRef Find(this ITable<UwtBbsAreaTopicRef> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsConfig Find(this ITable<UwtBbsConfig> table, string Key)
		{
			return table.FirstOrDefault(t =>
				t.Key == Key);
		}

		public static UwtBbsFollow Find(this ITable<UwtBbsFollow> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsTopic Find(this ITable<UwtBbsTopic> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsTopicBack Find(this ITable<UwtBbsTopicBack> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsTopicBackHis Find(this ITable<UwtBbsTopicBackHis> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsTopicHis Find(this ITable<UwtBbsTopicHis> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsUser Find(this ITable<UwtBbsUser> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsUserLevel Find(this ITable<UwtBbsUserLevel> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsUserLevelType Find(this ITable<UwtBbsUserLevelType> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsUserPropConfig Find(this ITable<UwtBbsUserPropConfig> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsUserProperty Find(this ITable<UwtBbsUserProperty> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsUserPropGroup Find(this ITable<UwtBbsUserPropGroup> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtBbsVisitHis Find(this ITable<UwtBbsVisitHis> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtHelper Find(this ITable<UwtHelper> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtNormalsBanner Find(this ITable<UwtNormalsBanner> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtNormalsFile Find(this ITable<UwtNormalsFile> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtNormalsNews Find(this ITable<UwtNormalsNews> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtNormalsNewsCate Find(this ITable<UwtNormalsNewsCate> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtNormalsVersion Find(this ITable<UwtNormalsVersion> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtUsersAccount Find(this ITable<UwtUsersAccount> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtUsersLoginHis Find(this ITable<UwtUsersLoginHis> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtUsersMenuGroup Find(this ITable<UwtUsersMenuGroup> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtUsersMenuGroupItem Find(this ITable<UwtUsersMenuGroupItem> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtUsersModule Find(this ITable<UwtUsersModule> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtUsersRole Find(this ITable<UwtUsersRole> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtUsersRoleModuleRef Find(this ITable<UwtUsersRoleModuleRef> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UwtWechatsUser Find(this ITable<UwtWechatsUser> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}
	}
}

#pragma warning restore 1591
