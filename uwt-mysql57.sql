/*
Navicat MySQL Data Transfer

Source Server         : 47.92.200.192_3306
Source Server Version : 50717
Source Host           : 47.92.200.192:3306
Source Database       : uwt

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2020-10-12 09:46:27
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for uwt_bbs_areas
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_areas`;
CREATE TABLE `uwt_bbs_areas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `p_id` int(11) NOT NULL COMMENT '父版块Id',
  `name` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `desc` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL COMMENT '备注',
  `index` int(11) NOT NULL DEFAULT '0' COMMENT '序号',
  `summary` varchar(255) COLLATE utf8mb4_bin NOT NULL COMMENT '说明',
  `icon` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `mgr_user_id` int(11) NOT NULL COMMENT '版主',
  `status` enum('show','hidden') COLLATE utf8mb4_bin NOT NULL DEFAULT 'show',
  `apply` enum('publish','approved') COLLATE utf8mb4_bin NOT NULL DEFAULT 'publish',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Records of uwt_bbs_areas
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_area_mgr_ref
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_area_mgr_ref`;
CREATE TABLE `uwt_bbs_area_mgr_ref` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `a_id` int(11) NOT NULL COMMENT '版块Id',
  `u_id` int(11) NOT NULL COMMENT '论坛用户Id',
  `auths` set('topic_approved','user_status','topic_top','topic_digest') COLLATE utf8mb4_bin NOT NULL COMMENT '权限',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Records of uwt_bbs_area_mgr_ref
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_area_topic_ref
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_area_topic_ref`;
CREATE TABLE `uwt_bbs_area_topic_ref` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `t_id` int(11) NOT NULL,
  `h_id` int(11) NOT NULL COMMENT '主题内容主体Id',
  `a_id` int(11) NOT NULL,
  `status` enum('applying','publish','forbid') COLLATE utf8mb4_bin NOT NULL DEFAULT 'applying',
  `ex` set('digest','top','hot') COLLATE utf8mb4_bin NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Records of uwt_bbs_area_topic_ref
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_config
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_config`;
CREATE TABLE `uwt_bbs_config` (
  `key` varchar(255) COLLATE utf16_bin NOT NULL,
  `value` varchar(255) COLLATE utf16_bin DEFAULT NULL,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_bin;

-- ----------------------------
-- Records of uwt_bbs_config
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_follows
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_follows`;
CREATE TABLE `uwt_bbs_follows` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `u_id` int(11) NOT NULL COMMENT '补关注的用户Id',
  `f_id` int(11) NOT NULL COMMENT '关注人',
  `add_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '关注时间',
  `valid` tinyint(1) NOT NULL DEFAULT '1' COMMENT '无效代表取消关注了',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf16 COLLATE=utf16_bin COMMENT='关注人与取消关系表';

-- ----------------------------
-- Records of uwt_bbs_follows
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_topics
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topics`;
CREATE TABLE `uwt_bbs_topics` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) COLLATE utf8mb4_bin NOT NULL COMMENT '标题',
  `create_user_id` int(11) NOT NULL COMMENT '创建者',
  `type` enum('discuss','question','vote') COLLATE utf8mb4_bin NOT NULL DEFAULT 'discuss' COMMENT '主题类型分为：讨论，提问，投票',
  `type_value` varchar(255) COLLATE utf8mb4_bin NOT NULL COMMENT '根据type不同而意义不同，暂未使用',
  `status` enum('apply','publish','forbid') COLLATE utf8mb4_bin NOT NULL DEFAULT 'publish',
  `touch_cnt` int(11) NOT NULL DEFAULT '0' COMMENT '查看次数',
  `add_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Records of uwt_bbs_topics
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_topic_backs
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topic_backs`;
CREATE TABLE `uwt_bbs_topic_backs` (
  `id` int(11) NOT NULL,
  `t_id` int(11) NOT NULL COMMENT '哪个话题',
  `t_b_id` int(11) NOT NULL COMMENT '回复哪条(楼中楼)',
  `index` int(11) NOT NULL COMMENT '楼层',
  `add_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `status` enum('normal','disabled','delete') COLLATE utf8mb4_bin NOT NULL DEFAULT 'normal' COMMENT '状态',
  `create_user_id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Records of uwt_bbs_topic_backs
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_topic_back_his
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topic_back_his`;
CREATE TABLE `uwt_bbs_topic_back_his` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `t_b_id` int(11) NOT NULL,
  `content` text COLLATE utf8mb4_bin NOT NULL COMMENT '内容',
  `add_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Records of uwt_bbs_topic_back_his
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_topic_his
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topic_his`;
CREATE TABLE `uwt_bbs_topic_his` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `t_id` int(11) NOT NULL COMMENT '主题Id',
  `title` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `content` text COLLATE utf8mb4_bin NOT NULL COMMENT '内容',
  `status` enum('draft','wait_apply','publish','forbid') COLLATE utf8mb4_bin NOT NULL,
  `add_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `apply_time` datetime DEFAULT NULL,
  `apply_note` varchar(255) COLLATE utf8mb4_bin NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Records of uwt_bbs_topic_his
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_users
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_users`;
CREATE TABLE `uwt_bbs_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `a_id` int(11) NOT NULL COMMENT '账号Id',
  `nickname` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `avatar` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `level_type_id` int(11) NOT NULL COMMENT '等级类型',
  `exp` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '经验',
  `join_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `auths` set('topic','comment','like','thumbs-up','enter') COLLATE utf8mb4_bin NOT NULL DEFAULT 'topic,comment,like,thumbs-up,enter' COMMENT '执行的操作',
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin COMMENT='用户信息';

-- ----------------------------
-- Records of uwt_bbs_users
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_user_levels
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_user_levels`;
CREATE TABLE `uwt_bbs_user_levels` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `avatar` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `type_id` int(11) NOT NULL,
  `exp` int(11) unsigned NOT NULL,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin ROW_FORMAT=DYNAMIC COMMENT='用户等级信息';

-- ----------------------------
-- Records of uwt_bbs_user_levels
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_user_level_types
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_user_level_types`;
CREATE TABLE `uwt_bbs_user_level_types` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin COMMENT='用户等级类型';

-- ----------------------------
-- Records of uwt_bbs_user_level_types
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_user_properties
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_user_properties`;
CREATE TABLE `uwt_bbs_user_properties` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `u_id` int(11) NOT NULL COMMENT '用户Id',
  `p_id` int(11) NOT NULL COMMENT '属性名',
  `value` text COLLATE utf16_bin NOT NULL COMMENT '值',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf16 COLLATE=utf16_bin COMMENT='用户额外信息表';

-- ----------------------------
-- Records of uwt_bbs_user_properties
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_user_prop_configs
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_user_prop_configs`;
CREATE TABLE `uwt_bbs_user_prop_configs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf16_bin NOT NULL DEFAULT '' COMMENT '显示名称',
  `type` enum('text','gender','date','time_of_day','weekday','datetime') COLLATE utf16_bin NOT NULL DEFAULT 'text',
  `g_id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf16 COLLATE=utf16_bin COMMENT='用户可用属性表';

-- ----------------------------
-- Records of uwt_bbs_user_prop_configs
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_user_prop_groups
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_user_prop_groups`;
CREATE TABLE `uwt_bbs_user_prop_groups` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf16_bin NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf16 COLLATE=utf16_bin;

-- ----------------------------
-- Records of uwt_bbs_user_prop_groups
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_bbs_visit_his
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_visit_his`;
CREATE TABLE `uwt_bbs_visit_his` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `v_id` int(11) NOT NULL COMMENT '拜访者Id',
  `u_id` int(11) NOT NULL COMMENT '受访者Id',
  `url` varchar(255) COLLATE utf16_bin NOT NULL COMMENT '访问的URL',
  `add_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '访问时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_bin;

-- ----------------------------
-- Records of uwt_bbs_visit_his
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_helpers
-- ----------------------------
DROP TABLE IF EXISTS `uwt_helpers`;
CREATE TABLE `uwt_helpers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) COLLATE utf16_bin NOT NULL,
  `content` text COLLATE utf16_bin,
  `summary` varchar(255) COLLATE utf16_bin NOT NULL,
  `publish_time` datetime DEFAULT NULL,
  `url` text COLLATE utf16_bin NOT NULL,
  `author` varchar(255) COLLATE utf16_bin NOT NULL COMMENT '作者名称（随便写的东西）',
  `modify_id` int(11) NOT NULL COMMENT '编辑者Id',
  `creator_id` int(11) NOT NULL COMMENT '创建者Id',
  `add_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf16 COLLATE=utf16_bin;

-- ----------------------------
-- Records of uwt_helpers
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_normals_banners
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_banners`;
CREATE TABLE `uwt_normals_banners` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `target` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
  `target_type` enum('Mini','Web','Page','None') COLLATE utf8mb4_bin NOT NULL DEFAULT 'Page',
  `image` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `cate` enum('Banner','Environment','Course') COLLATE utf8mb4_bin NOT NULL DEFAULT 'Banner',
  `title` varchar(15) COLLATE utf8mb4_bin DEFAULT NULL,
  `sub_title` varchar(15) COLLATE utf8mb4_bin DEFAULT NULL,
  `desc` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
  `index` int(11) NOT NULL,
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_normals_banners
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_normals_files
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_files`;
CREATE TABLE `uwt_normals_files` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `filename` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `desc` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
  `path` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
  `file_size` bigint(20) NOT NULL,
  `type` varchar(63) COLLATE utf8mb4_bin NOT NULL,
  `add_account_id` int(11) NOT NULL,
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_normals_files
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_normals_news
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_news`;
CREATE TABLE `uwt_normals_news` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `summary` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `content` longtext COLLATE utf8mb4_bin,
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_normals_news
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_normals_news_cates
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_news_cates`;
CREATE TABLE `uwt_normals_news_cates` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `sub_title` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
  `desc` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
  `p_id` int(11) NOT NULL DEFAULT '0',
  `large_icon` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `icon` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `mini_icon` varchar(255) COLLATE utf8mb4_bin NOT NULL,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_normals_news_cates
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_normals_versions
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_versions`;
CREATE TABLE `uwt_normals_versions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL COMMENT '版本名称',
  `version` varchar(255) NOT NULL COMMENT '版本号 显示用',
  `logs` text NOT NULL COMMENT '日志',
  `version_no` int(11) NOT NULL COMMENT '数值版本号，用于排序，越大版本越新',
  `path` varchar(255) NOT NULL COMMENT '下载地址',
  `type` varchar(255) NOT NULL COMMENT '类型',
  `publish_time` datetime NOT NULL COMMENT '发布时间',
  `build_time` datetime NOT NULL COMMENT '编译时间',
  `add_time` datetime NOT NULL COMMENT '添加时间',
  `valid` tinyint(1) NOT NULL DEFAULT '1' COMMENT '有效性',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_normals_versions
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_users_accounts
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_accounts`;
CREATE TABLE `uwt_users_accounts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `account` varchar(64) DEFAULT NULL,
  `password` varchar(40) DEFAULT '',
  `type` varchar(32) NOT NULL DEFAULT 'mgr' COMMENT '账号类型',
  `role_id` int(11) NOT NULL,
  `status` enum('enabled','disabled','writenoff') NOT NULL DEFAULT 'enabled',
  `last_login_time` datetime DEFAULT NULL,
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE KEY `account_name` (`account`,`type`) USING BTREE COMMENT '账号唯一性'
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_accounts
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_users_login_his
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_login_his`;
CREATE TABLE `uwt_users_login_his` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) NOT NULL COMMENT '填写的用户名',
  `pwd` varchar(255) NOT NULL COMMENT '填写的密码',
  `type` varchar(32) NOT NULL DEFAULT '',
  `a_id` int(11) NOT NULL COMMENT '可能的用户为0是登录时用户名不对',
  `status` tinyint(1) NOT NULL COMMENT '0为失败 1为成功',
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '操作时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=477 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_login_his
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_users_menu_groups
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_menu_groups`;
CREATE TABLE `uwt_users_menu_groups` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `desc` varchar(255) DEFAULT NULL,
  `page_count` int(11) NOT NULL,
  `auth_count` int(11) NOT NULL,
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_menu_groups
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_users_menu_group_items
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_menu_group_items`;
CREATE TABLE `uwt_users_menu_group_items` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `icon` varchar(255) NOT NULL COMMENT '图标',
  `tooltip` varchar(255) NOT NULL COMMENT '显示提示',
  `desc` varchar(255) DEFAULT NULL COMMENT '说明',
  `pid` int(11) NOT NULL DEFAULT '0',
  `group_id` int(11) NOT NULL,
  `url` int(11) NOT NULL DEFAULT '0' COMMENT 'Module的ID',
  `index` int(11) NOT NULL DEFAULT '0',
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_menu_group_items
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_users_modules
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_modules`;
CREATE TABLE `uwt_users_modules` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(64) NOT NULL,
  `url` varchar(255) NOT NULL,
  `type` enum('page','api') NOT NULL DEFAULT 'page',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1989 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_modules
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_users_roles
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_roles`;
CREATE TABLE `uwt_users_roles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL COMMENT '角色名',
  `home_page_url` varchar(255) NOT NULL COMMENT '角色主页地址',
  `desc` varchar(255) DEFAULT NULL,
  `menu_group_id` int(11) NOT NULL,
  `add_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_roles
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_users_role_module_ref
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_role_module_ref`;
CREATE TABLE `uwt_users_role_module_ref` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `r_id` int(11) NOT NULL,
  `m_id` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_role_module_ref
-- ----------------------------

-- ----------------------------
-- Table structure for uwt_wechats_users
-- ----------------------------
DROP TABLE IF EXISTS `uwt_wechats_users`;
CREATE TABLE `uwt_wechats_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nick_name` varchar(255) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `token` varchar(32) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `open_id` varchar(64) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `gender` tinyint(4) NOT NULL,
  `language` varchar(16) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `country` varchar(32) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `city` varchar(32) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `province` varchar(32) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `avatar_url` varchar(255) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `add_time` datetime NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_wechats_users
-- ----------------------------
