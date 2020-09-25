/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : uwt

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2020-09-25 08:58:47
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
INSERT INTO `uwt_bbs_areas` VALUES ('1', '0', '官方论坛', '', '0', '', '/Uploads/2020-08-16/22/白底.jpg', '1', 'show', 'publish');
INSERT INTO `uwt_bbs_areas` VALUES ('2', '1', '子论坛', '', '0', '说明', '/', '1', 'show', 'publish');

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
INSERT INTO `uwt_bbs_follows` VALUES ('1', '1', '2', '2020-09-21 15:33:16', '1');
INSERT INTO `uwt_bbs_follows` VALUES ('2', '2', '1', '2020-09-21 15:33:19', '1');

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

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
INSERT INTO `uwt_bbs_users` VALUES ('1', '1', '神俊涛', '/admins/images/header.jpg', '1', '0', '2020-08-16 22:12:22', 'topic,comment,like,thumbs-up,enter', '1');
INSERT INTO `uwt_bbs_users` VALUES ('2', '1', '神俊涛2', '/admins/images/header.jpg', '1', '0', '2020-08-16 22:12:22', 'topic,comment,like,thumbs-up,enter', '1');

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
INSERT INTO `uwt_bbs_user_levels` VALUES ('1', '一级', '/', '1', '10', '1');

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
INSERT INTO `uwt_bbs_user_level_types` VALUES ('1', '经济配置', '0');
INSERT INTO `uwt_bbs_user_level_types` VALUES ('2', '一般会员', '1');

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
INSERT INTO `uwt_bbs_user_properties` VALUES ('1', '1', '1', 0x2F);
INSERT INTO `uwt_bbs_user_properties` VALUES ('2', '1', '2', 0xE58C97E4BAAC);
INSERT INTO `uwt_bbs_user_properties` VALUES ('3', '1', '3', 0xE6B2B3E58C97);
INSERT INTO `uwt_bbs_user_properties` VALUES ('4', '1', '4', 0x3230303030);
INSERT INTO `uwt_bbs_user_properties` VALUES ('5', '1', '5', 0x534A54616F);

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
INSERT INTO `uwt_bbs_user_prop_configs` VALUES ('1', '个人主页', 'text', '1');
INSERT INTO `uwt_bbs_user_prop_configs` VALUES ('2', '住址', 'text', '1');
INSERT INTO `uwt_bbs_user_prop_configs` VALUES ('3', '祖籍', 'text', '1');
INSERT INTO `uwt_bbs_user_prop_configs` VALUES ('4', 'QQ', 'text', '2');
INSERT INTO `uwt_bbs_user_prop_configs` VALUES ('5', '微信', 'text', '2');
INSERT INTO `uwt_bbs_user_prop_configs` VALUES ('6', '大学', 'text', '3');
INSERT INTO `uwt_bbs_user_prop_configs` VALUES ('7', '中学', 'text', '3');

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
INSERT INTO `uwt_bbs_user_prop_groups` VALUES ('1', '基本资料');
INSERT INTO `uwt_bbs_user_prop_groups` VALUES ('2', '联系方式');
INSERT INTO `uwt_bbs_user_prop_groups` VALUES ('3', '教育经历');

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
INSERT INTO `uwt_helpers` VALUES ('1', '123', 0x0A202020202020202020202020202020202020202020202020202020203132330A202020202020202020202020202020202020202020202020, '123123123123123123123123123123123123123', '2020-06-07 07:34:37', 0x3B2F486F6D652F496E6465783B2F48656C7065724D67722F4164643B, '老沈', '1', '1', '2020-06-06 08:45:51', '2020-06-08 09:47:51', '1');
INSERT INTO `uwt_helpers` VALUES ('2', '测试一下', 0x0A202020202020202020202020202020202020202020202020202020203C703EE8BF993C737472696B653EE698AFE6B58B3C2F737472696B653EE8AF953C623EE58685E5AEB93C2F623E3C696D67207372633D2268747470733A2F2F6C6F63616C686F73743A353030312F5F636F6E74656E742F5557542E54656D706C617465732F6C69622F6C617975692F696D616765732F666163652F33392E6769662220616C743D225BE9BC93E68E8C5D223E3C2F703E3C703E3C62723E3C2F703E3C703E3C696D67207372633D222F55706C6F6164732F323032302D30362D32302F30382F3132332E6769662220616C743D223132332E676966223E3C62723E3C2F703E3C703EE591B5E591B53C2F703E0A202020202020202020202020202020202020202020202020, '123', null, 0x3B2F486F6D652F496E6465783B2F486F6D652F4C6F67733B2F4E65777343617465732F496E6465783B2F4E65777343617465732F4D6F646966793B, '正经', '1', '1', '2020-06-20 08:52:33', '2020-06-20 08:59:32', '1');
INSERT INTO `uwt_helpers` VALUES ('3', '帮助添加/编辑', 0x0A202020202020202020202020202020202020202020202020202020203C703E3132333132333C2F703E3C703E3C62723E3C2F703E3C703E3132333132333C2F703E3C703E3C62723E3C2F703E3C703E313233313233313C2F703E3C703E3C62723E3C2F703E0A202020202020202020202020202020202020202020202020, '', '2020-06-20 21:38:24', 0x3B2F48656C7065724D67722F4164643B2F48656C7065724D67722F4D6F646966793B, '123', '1', '1', '2020-06-20 11:27:30', '2020-06-20 21:38:25', '1');

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
INSERT INTO `uwt_normals_files` VALUES ('1', '微信图片_20171126102647.jpg', null, '/Uploads/2020-04-25/07/微信图片_20171126102647.jpg', '188809', 'image/jpeg', '0', '2020-04-25 07:57:06');
INSERT INTO `uwt_normals_files` VALUES ('2', '白底.jpg', null, '/Uploads/2020-04-25/10/白底.jpg', '87615', 'image/jpeg', '0', '2020-04-25 10:07:58');
INSERT INTO `uwt_normals_files` VALUES ('3', '白底.jpg', null, '/Uploads/2020-04-25/10/1106888-9365/白底.jpg', '87615', 'image/jpeg', '0', '2020-04-25 10:11:07');
INSERT INTO `uwt_normals_files` VALUES ('4', '白底.jpg', null, '/Uploads/2020-04-25/10/1139255-6871/白底.jpg', '87615', 'image/jpeg', '0', '2020-04-25 10:11:39');
INSERT INTO `uwt_normals_files` VALUES ('5', '微信图片_20180901130225.jpg', null, '/Uploads/2020-04-25/10/微信图片_20180901130225.jpg', '96715', 'image/jpeg', '0', '2020-04-25 10:41:51');
INSERT INTO `uwt_normals_files` VALUES ('6', '白底.jpg', null, '/Uploads/2020-04-25/10/4233054-8224/白底.jpg', '87615', 'image/jpeg', '0', '2020-04-25 10:42:33');
INSERT INTO `uwt_normals_files` VALUES ('7', 'wxpay.png', null, '/Uploads/2020-04-25/10/wxpay.png', '17799', 'image/png', '0', '2020-04-25 10:51:43');
INSERT INTO `uwt_normals_files` VALUES ('8', '白底.jpg', null, '/Uploads/2020-04-25/10/5953766-8055/白底.jpg', '87615', 'image/jpeg', '0', '2020-04-25 10:59:54');
INSERT INTO `uwt_normals_files` VALUES ('9', '3.pdf', null, '/Uploads/2020-06-13/09/3.pdf', '123901', 'application/pdf', '0', '2020-06-13 09:54:12');
INSERT INTO `uwt_normals_files` VALUES ('10', '核验单.jpg', null, '/Uploads/2020-06-13/10/核验单.jpg', '2782570', 'image/jpeg', '0', '2020-06-13 10:09:58');
INSERT INTO `uwt_normals_files` VALUES ('11', '123.gif', null, '/Uploads/2020-06-20/08/123.gif', '316419', 'image/gif', '0', '2020-06-20 08:49:09');
INSERT INTO `uwt_normals_files` VALUES ('12', 'qrcode_ali.png', null, '/Uploads/2020-06-20/12/qrcode_ali.png', '29398', 'image/png', '0', '2020-06-20 12:13:35');
INSERT INTO `uwt_normals_files` VALUES ('13', 'qrcode_wx.png', null, '/Uploads/2020-06-20/12/qrcode_wx.png', '21926', 'image/png', '0', '2020-06-20 12:13:36');
INSERT INTO `uwt_normals_files` VALUES ('14', 'qrcode_ali.png', null, '/Uploads/2020-06-20/12/2913589-3242/qrcode_ali.png', '29398', 'image/png', '0', '2020-06-20 12:29:14');
INSERT INTO `uwt_normals_files` VALUES ('15', 'alipay.png', null, '/Uploads/2020-08-16/22/alipay.png', '10340', 'image/png', '0', '2020-08-16 22:30:30');
INSERT INTO `uwt_normals_files` VALUES ('16', '白底.jpg', null, '/Uploads/2020-08-16/22/白底.jpg', '87615', 'image/jpeg', '0', '2020-08-16 22:32:39');

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
INSERT INTO `uwt_normals_news_cates` VALUES ('1', '234', '234', '234', '0', '', '', '', '1', '2020-04-25 10:07:34', '2020-04-25 10:07:34');
INSERT INTO `uwt_normals_news_cates` VALUES ('2', '234', '234', '234', '0', '', '', '', '1', '2020-04-25 10:11:17', '2020-04-25 10:11:17');
INSERT INTO `uwt_normals_news_cates` VALUES ('3', '234', '234', '234', '0', '', '', '', '1', '2020-04-25 10:42:12', '2020-04-25 10:42:12');
INSERT INTO `uwt_normals_news_cates` VALUES ('4', '123', '123', '123', '0', '', '', '', '1', '2020-04-25 10:51:49', '2020-04-25 10:51:49');

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
INSERT INTO `uwt_users_accounts` VALUES ('1', '123', '123', 'mgr', '2', 'enabled', '2020-09-16 09:48:46', '2020-05-04 19:00:23');

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
) ENGINE=InnoDB AUTO_INCREMENT=476 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_login_his
-- ----------------------------
INSERT INTO `uwt_users_login_his` VALUES ('1', '123', '123', '', '1', '1', '2020-05-05 21:50:15');
INSERT INTO `uwt_users_login_his` VALUES ('2', '123', '123', '', '1', '1', '2020-05-05 21:53:24');
INSERT INTO `uwt_users_login_his` VALUES ('3', '123', '123', '', '1', '1', '2020-05-06 07:21:35');
INSERT INTO `uwt_users_login_his` VALUES ('4', '123', '4333', '', '0', '0', '2020-05-06 07:25:36');
INSERT INTO `uwt_users_login_his` VALUES ('5', 'sroot', '22', '', '0', '0', '2020-05-06 07:25:40');
INSERT INTO `uwt_users_login_his` VALUES ('6', 'sroot', '323', '', '0', '0', '2020-05-06 07:27:01');
INSERT INTO `uwt_users_login_his` VALUES ('7', 'sroot', '33', '', '0', '0', '2020-05-06 07:27:39');
INSERT INTO `uwt_users_login_his` VALUES ('8', 'sroot', '2', '', '0', '0', '2020-05-06 09:07:00');
INSERT INTO `uwt_users_login_his` VALUES ('9', 'sroot', '2', '', '0', '0', '2020-05-06 09:07:05');
INSERT INTO `uwt_users_login_his` VALUES ('10', 'sroot', '2', '', '0', '0', '2020-05-06 09:07:08');
INSERT INTO `uwt_users_login_his` VALUES ('11', '123', '123', '', '1', '1', '2020-05-06 09:07:13');
INSERT INTO `uwt_users_login_his` VALUES ('12', 'sroot', '123', '', '0', '0', '2020-05-07 20:26:48');
INSERT INTO `uwt_users_login_his` VALUES ('13', 'sroot', '123456', '', '0', '0', '2020-05-07 20:27:49');
INSERT INTO `uwt_users_login_his` VALUES ('14', 'sroot', '123456', '', '0', '0', '2020-05-07 20:27:52');
INSERT INTO `uwt_users_login_his` VALUES ('15', 'sroot', '123456', '', '0', '0', '2020-05-07 20:27:56');
INSERT INTO `uwt_users_login_his` VALUES ('16', '123', '123', '', '1', '1', '2020-05-07 20:31:08');
INSERT INTO `uwt_users_login_his` VALUES ('17', '123', '123', '', '1', '1', '2020-05-07 20:35:56');
INSERT INTO `uwt_users_login_his` VALUES ('18', '123', '123', '', '1', '1', '2020-05-10 16:07:45');
INSERT INTO `uwt_users_login_his` VALUES ('19', '123', '123', '', '1', '1', '2020-05-10 16:13:44');
INSERT INTO `uwt_users_login_his` VALUES ('20', '123', '123', '', '1', '1', '2020-05-10 16:20:33');
INSERT INTO `uwt_users_login_his` VALUES ('21', '123', '123', '', '1', '1', '2020-05-10 16:39:26');
INSERT INTO `uwt_users_login_his` VALUES ('22', '123', '123', '', '1', '1', '2020-05-10 16:43:42');
INSERT INTO `uwt_users_login_his` VALUES ('23', '123', '123', '', '1', '1', '2020-05-10 16:55:12');
INSERT INTO `uwt_users_login_his` VALUES ('24', '123', '123', '', '1', '1', '2020-05-10 19:01:49');
INSERT INTO `uwt_users_login_his` VALUES ('25', '123', '123', '', '1', '1', '2020-05-10 19:16:32');
INSERT INTO `uwt_users_login_his` VALUES ('26', '123', '123', '', '1', '1', '2020-05-10 19:26:01');
INSERT INTO `uwt_users_login_his` VALUES ('27', '123', '123', '', '1', '1', '2020-05-10 19:28:21');
INSERT INTO `uwt_users_login_his` VALUES ('28', '123', '123', '', '1', '1', '2020-05-10 21:15:14');
INSERT INTO `uwt_users_login_his` VALUES ('29', '123', '123', '', '1', '1', '2020-05-10 21:17:03');
INSERT INTO `uwt_users_login_his` VALUES ('30', '123', '123', '', '1', '1', '2020-05-11 08:27:17');
INSERT INTO `uwt_users_login_his` VALUES ('31', '123', '123', '', '1', '1', '2020-05-11 08:34:58');
INSERT INTO `uwt_users_login_his` VALUES ('32', '123', '123', '', '1', '1', '2020-05-12 07:27:24');
INSERT INTO `uwt_users_login_his` VALUES ('33', '123', '123', '', '1', '1', '2020-05-12 08:16:04');
INSERT INTO `uwt_users_login_his` VALUES ('34', '123', '123', '', '1', '1', '2020-05-12 08:17:42');
INSERT INTO `uwt_users_login_his` VALUES ('35', '123', '123', '', '1', '1', '2020-05-16 08:16:20');
INSERT INTO `uwt_users_login_his` VALUES ('36', '123', '123', '', '1', '1', '2020-05-16 08:18:30');
INSERT INTO `uwt_users_login_his` VALUES ('37', '123', '123', '', '1', '1', '2020-05-16 08:22:59');
INSERT INTO `uwt_users_login_his` VALUES ('38', '123', '123', '', '1', '1', '2020-05-16 08:26:07');
INSERT INTO `uwt_users_login_his` VALUES ('39', '123', '123', '', '1', '1', '2020-05-16 08:51:32');
INSERT INTO `uwt_users_login_his` VALUES ('40', '123', '123', '', '1', '1', '2020-05-17 11:01:50');
INSERT INTO `uwt_users_login_his` VALUES ('41', '123', '123', '', '1', '1', '2020-05-18 22:01:03');
INSERT INTO `uwt_users_login_his` VALUES ('42', '123', '123', '', '1', '1', '2020-05-18 22:01:11');
INSERT INTO `uwt_users_login_his` VALUES ('43', '123', '123', '', '1', '1', '2020-05-18 22:01:37');
INSERT INTO `uwt_users_login_his` VALUES ('44', '123', '123', '', '1', '1', '2020-05-18 22:03:32');
INSERT INTO `uwt_users_login_his` VALUES ('45', '123', '123', '', '1', '1', '2020-05-23 11:51:19');
INSERT INTO `uwt_users_login_his` VALUES ('46', '123', '123', '', '1', '1', '2020-05-23 11:58:31');
INSERT INTO `uwt_users_login_his` VALUES ('47', '123', '123', '', '1', '1', '2020-05-23 12:00:09');
INSERT INTO `uwt_users_login_his` VALUES ('48', '123', '123', '', '1', '1', '2020-05-23 12:34:23');
INSERT INTO `uwt_users_login_his` VALUES ('49', '123', '123', '', '1', '1', '2020-05-23 12:45:10');
INSERT INTO `uwt_users_login_his` VALUES ('50', '123', '123', '', '1', '1', '2020-05-25 21:46:05');
INSERT INTO `uwt_users_login_his` VALUES ('51', '123', '123', '', '1', '1', '2020-05-25 21:50:43');
INSERT INTO `uwt_users_login_his` VALUES ('52', '123', '123', '', '1', '1', '2020-05-25 21:53:01');
INSERT INTO `uwt_users_login_his` VALUES ('53', '123', '123', '', '1', '1', '2020-05-25 21:59:21');
INSERT INTO `uwt_users_login_his` VALUES ('54', '123', '123', '', '1', '1', '2020-05-25 22:06:51');
INSERT INTO `uwt_users_login_his` VALUES ('55', '123', '123', '', '1', '1', '2020-05-27 18:41:07');
INSERT INTO `uwt_users_login_his` VALUES ('56', '123', '123', '', '1', '1', '2020-05-27 21:20:02');
INSERT INTO `uwt_users_login_his` VALUES ('57', '123', '123', '', '1', '1', '2020-05-27 21:35:25');
INSERT INTO `uwt_users_login_his` VALUES ('58', '123', '123', '', '1', '1', '2020-05-27 21:48:43');
INSERT INTO `uwt_users_login_his` VALUES ('59', '123', '123', '', '1', '1', '2020-05-27 21:54:53');
INSERT INTO `uwt_users_login_his` VALUES ('60', '123', '123', '', '1', '1', '2020-05-27 21:56:00');
INSERT INTO `uwt_users_login_his` VALUES ('61', '123', '123', '', '1', '1', '2020-05-27 21:58:17');
INSERT INTO `uwt_users_login_his` VALUES ('62', '123', '123', '', '1', '1', '2020-05-27 21:59:52');
INSERT INTO `uwt_users_login_his` VALUES ('63', '123', '123', '', '1', '1', '2020-05-27 22:04:20');
INSERT INTO `uwt_users_login_his` VALUES ('64', '123', '123', '', '1', '1', '2020-05-27 22:05:15');
INSERT INTO `uwt_users_login_his` VALUES ('65', '123', '123', '', '1', '1', '2020-05-27 22:12:30');
INSERT INTO `uwt_users_login_his` VALUES ('66', '123', '123', '', '1', '1', '2020-05-27 22:20:03');
INSERT INTO `uwt_users_login_his` VALUES ('67', '123', '123', '', '1', '1', '2020-05-27 22:24:07');
INSERT INTO `uwt_users_login_his` VALUES ('68', '123', '123', '', '1', '1', '2020-05-27 22:28:07');
INSERT INTO `uwt_users_login_his` VALUES ('69', '123', '123', '', '1', '1', '2020-05-27 22:29:29');
INSERT INTO `uwt_users_login_his` VALUES ('70', '123', '123', '', '1', '1', '2020-05-27 22:30:32');
INSERT INTO `uwt_users_login_his` VALUES ('71', '123', '123', '', '1', '1', '2020-05-27 22:31:51');
INSERT INTO `uwt_users_login_his` VALUES ('72', '123', '123', '', '1', '1', '2020-05-27 22:35:23');
INSERT INTO `uwt_users_login_his` VALUES ('73', '123', '123', '', '1', '1', '2020-05-27 22:37:21');
INSERT INTO `uwt_users_login_his` VALUES ('74', '123', '123', '', '1', '1', '2020-05-27 22:44:07');
INSERT INTO `uwt_users_login_his` VALUES ('75', '123', '123', '', '1', '1', '2020-05-28 05:48:37');
INSERT INTO `uwt_users_login_his` VALUES ('76', '123', '123', '', '1', '1', '2020-05-28 05:59:40');
INSERT INTO `uwt_users_login_his` VALUES ('77', '123', '123', '', '1', '1', '2020-05-28 08:29:32');
INSERT INTO `uwt_users_login_his` VALUES ('78', '123', '123', '', '1', '1', '2020-05-28 08:37:58');
INSERT INTO `uwt_users_login_his` VALUES ('79', '123', '123', '', '1', '1', '2020-05-28 08:43:03');
INSERT INTO `uwt_users_login_his` VALUES ('80', '123', '123', '', '1', '1', '2020-05-28 08:45:23');
INSERT INTO `uwt_users_login_his` VALUES ('81', '123', '123', '', '1', '1', '2020-05-28 08:47:09');
INSERT INTO `uwt_users_login_his` VALUES ('82', '123', '123', '', '1', '1', '2020-05-28 08:52:13');
INSERT INTO `uwt_users_login_his` VALUES ('83', '123', '123', '', '1', '1', '2020-05-28 08:55:58');
INSERT INTO `uwt_users_login_his` VALUES ('84', '123', '123', '', '1', '1', '2020-05-28 08:56:14');
INSERT INTO `uwt_users_login_his` VALUES ('85', '123', '123', '', '1', '1', '2020-05-28 08:58:27');
INSERT INTO `uwt_users_login_his` VALUES ('86', '123', '123', '', '1', '1', '2020-05-28 09:01:33');
INSERT INTO `uwt_users_login_his` VALUES ('87', '123', '123', '', '1', '1', '2020-05-28 09:02:40');
INSERT INTO `uwt_users_login_his` VALUES ('88', '123', '123', '', '1', '1', '2020-05-28 09:11:44');
INSERT INTO `uwt_users_login_his` VALUES ('89', '123', '123', '', '1', '1', '2020-05-28 09:14:23');
INSERT INTO `uwt_users_login_his` VALUES ('90', '123', '123', '', '1', '1', '2020-05-28 09:16:55');
INSERT INTO `uwt_users_login_his` VALUES ('91', '123', '123', '', '1', '1', '2020-05-28 09:22:45');
INSERT INTO `uwt_users_login_his` VALUES ('92', '123', '123', '', '1', '1', '2020-05-28 09:25:17');
INSERT INTO `uwt_users_login_his` VALUES ('93', '123', '123', '', '1', '1', '2020-05-28 09:33:53');
INSERT INTO `uwt_users_login_his` VALUES ('94', '123', '123', '', '1', '1', '2020-05-28 09:41:33');
INSERT INTO `uwt_users_login_his` VALUES ('95', '123', '123', '', '1', '1', '2020-05-28 09:47:06');
INSERT INTO `uwt_users_login_his` VALUES ('96', '123', '123', '', '1', '1', '2020-05-28 09:58:48');
INSERT INTO `uwt_users_login_his` VALUES ('97', '123', '123', '', '0', '0', '2020-05-29 05:48:46');
INSERT INTO `uwt_users_login_his` VALUES ('98', '123', '123', '', '0', '0', '2020-05-29 05:48:53');
INSERT INTO `uwt_users_login_his` VALUES ('99', '123', '123', '', '1', '1', '2020-05-29 05:56:43');
INSERT INTO `uwt_users_login_his` VALUES ('100', '123', '123', '', '1', '1', '2020-05-29 08:51:54');
INSERT INTO `uwt_users_login_his` VALUES ('101', '123', '123', '', '1', '1', '2020-05-29 08:56:00');
INSERT INTO `uwt_users_login_his` VALUES ('102', '123', '123', '', '1', '1', '2020-05-29 09:04:02');
INSERT INTO `uwt_users_login_his` VALUES ('103', '123', '123', '', '1', '1', '2020-05-29 09:05:37');
INSERT INTO `uwt_users_login_his` VALUES ('104', '123', '123', '', '1', '1', '2020-05-29 09:37:12');
INSERT INTO `uwt_users_login_his` VALUES ('105', '123', '123', '', '1', '1', '2020-05-29 09:46:48');
INSERT INTO `uwt_users_login_his` VALUES ('106', '123', '123', '', '1', '1', '2020-05-29 16:51:31');
INSERT INTO `uwt_users_login_his` VALUES ('107', '123', '123', '', '1', '1', '2020-05-29 17:10:32');
INSERT INTO `uwt_users_login_his` VALUES ('108', '123', '123', '', '1', '1', '2020-05-29 17:22:21');
INSERT INTO `uwt_users_login_his` VALUES ('109', '123', '123', '', '1', '1', '2020-05-29 22:02:27');
INSERT INTO `uwt_users_login_his` VALUES ('110', '123', '123', '', '1', '1', '2020-05-30 06:48:03');
INSERT INTO `uwt_users_login_his` VALUES ('111', '123', '123', '', '1', '1', '2020-05-30 06:52:05');
INSERT INTO `uwt_users_login_his` VALUES ('112', '123', '123', '', '1', '1', '2020-05-30 06:56:09');
INSERT INTO `uwt_users_login_his` VALUES ('113', '123', '123', '', '1', '1', '2020-05-30 07:58:45');
INSERT INTO `uwt_users_login_his` VALUES ('114', '123', '123', '', '1', '1', '2020-05-30 08:01:16');
INSERT INTO `uwt_users_login_his` VALUES ('115', '123', '123', '', '1', '1', '2020-05-30 08:55:11');
INSERT INTO `uwt_users_login_his` VALUES ('116', '123', '123', '', '1', '1', '2020-05-30 09:00:24');
INSERT INTO `uwt_users_login_his` VALUES ('117', '123', '123', '', '1', '1', '2020-05-30 09:38:12');
INSERT INTO `uwt_users_login_his` VALUES ('118', '123', '123', '', '1', '1', '2020-05-30 09:45:33');
INSERT INTO `uwt_users_login_his` VALUES ('119', '123', '123', '', '1', '1', '2020-05-30 09:49:10');
INSERT INTO `uwt_users_login_his` VALUES ('120', '123', '123', '', '1', '1', '2020-06-01 14:27:31');
INSERT INTO `uwt_users_login_his` VALUES ('121', '123', '123', '', '1', '1', '2020-06-03 22:27:07');
INSERT INTO `uwt_users_login_his` VALUES ('122', '123', '123', '', '1', '1', '2020-06-03 22:30:38');
INSERT INTO `uwt_users_login_his` VALUES ('123', '123', '123', '', '1', '1', '2020-06-03 22:37:10');
INSERT INTO `uwt_users_login_his` VALUES ('124', '123', '123', '', '1', '1', '2020-06-04 09:11:23');
INSERT INTO `uwt_users_login_his` VALUES ('125', '123', '123', '', '1', '1', '2020-06-04 09:41:04');
INSERT INTO `uwt_users_login_his` VALUES ('126', '123', '123', '', '1', '1', '2020-06-04 09:47:57');
INSERT INTO `uwt_users_login_his` VALUES ('127', '123', '123', '', '1', '1', '2020-06-06 08:34:32');
INSERT INTO `uwt_users_login_his` VALUES ('128', '123', '123', '', '1', '1', '2020-06-06 08:41:21');
INSERT INTO `uwt_users_login_his` VALUES ('129', '123', '123', '', '1', '1', '2020-06-06 08:43:46');
INSERT INTO `uwt_users_login_his` VALUES ('130', '123', '123', '', '1', '1', '2020-06-06 08:45:26');
INSERT INTO `uwt_users_login_his` VALUES ('131', '123', '123', '', '1', '1', '2020-06-06 08:46:59');
INSERT INTO `uwt_users_login_his` VALUES ('132', '123', '123', '', '1', '1', '2020-06-06 08:49:25');
INSERT INTO `uwt_users_login_his` VALUES ('133', '123', '123', '', '1', '1', '2020-06-06 08:55:45');
INSERT INTO `uwt_users_login_his` VALUES ('134', '123', '123', '', '1', '1', '2020-06-06 09:00:40');
INSERT INTO `uwt_users_login_his` VALUES ('135', '123', '123', '', '1', '1', '2020-06-06 09:26:58');
INSERT INTO `uwt_users_login_his` VALUES ('136', '123', '123', '', '1', '1', '2020-06-06 09:38:47');
INSERT INTO `uwt_users_login_his` VALUES ('137', '123', '123', '', '1', '1', '2020-06-06 09:47:13');
INSERT INTO `uwt_users_login_his` VALUES ('138', '123', '123', '', '1', '1', '2020-06-06 09:55:06');
INSERT INTO `uwt_users_login_his` VALUES ('139', '123', '123', '', '1', '1', '2020-06-06 18:48:36');
INSERT INTO `uwt_users_login_his` VALUES ('140', '123', '123', '', '1', '1', '2020-06-06 18:50:15');
INSERT INTO `uwt_users_login_his` VALUES ('141', '123', '123', '', '1', '1', '2020-06-06 20:42:14');
INSERT INTO `uwt_users_login_his` VALUES ('142', '123', '123', '', '1', '1', '2020-06-06 22:04:50');
INSERT INTO `uwt_users_login_his` VALUES ('143', '123', '123', '', '1', '1', '2020-06-06 22:08:02');
INSERT INTO `uwt_users_login_his` VALUES ('144', '123', '123', '', '1', '1', '2020-06-06 22:10:49');
INSERT INTO `uwt_users_login_his` VALUES ('145', '123', '123', '', '1', '1', '2020-06-06 22:12:04');
INSERT INTO `uwt_users_login_his` VALUES ('146', '123', '123', '', '1', '1', '2020-06-06 22:24:32');
INSERT INTO `uwt_users_login_his` VALUES ('147', '123', '123', '', '1', '1', '2020-06-06 22:29:05');
INSERT INTO `uwt_users_login_his` VALUES ('148', '123', '123', '', '1', '1', '2020-06-06 22:38:41');
INSERT INTO `uwt_users_login_his` VALUES ('149', '123', '123', '', '1', '1', '2020-06-06 22:41:21');
INSERT INTO `uwt_users_login_his` VALUES ('150', '123', '123', '', '1', '1', '2020-06-06 22:43:16');
INSERT INTO `uwt_users_login_his` VALUES ('151', '123', '123', '', '1', '1', '2020-06-06 22:45:19');
INSERT INTO `uwt_users_login_his` VALUES ('152', '123', '123', '', '1', '1', '2020-06-06 22:47:04');
INSERT INTO `uwt_users_login_his` VALUES ('153', '123', '123', '', '1', '1', '2020-06-06 22:58:43');
INSERT INTO `uwt_users_login_his` VALUES ('154', '123', '123', '', '1', '1', '2020-06-07 06:12:23');
INSERT INTO `uwt_users_login_his` VALUES ('155', '123', '123', '', '1', '1', '2020-06-07 06:16:16');
INSERT INTO `uwt_users_login_his` VALUES ('156', '123', '123', '', '1', '1', '2020-06-07 07:23:20');
INSERT INTO `uwt_users_login_his` VALUES ('157', '123', '123', '', '1', '1', '2020-06-07 07:28:30');
INSERT INTO `uwt_users_login_his` VALUES ('158', '123', '123', '', '1', '1', '2020-06-07 07:33:43');
INSERT INTO `uwt_users_login_his` VALUES ('159', '123', '123', '', '1', '1', '2020-06-07 07:46:37');
INSERT INTO `uwt_users_login_his` VALUES ('160', '123', '123', '', '1', '1', '2020-06-07 14:46:41');
INSERT INTO `uwt_users_login_his` VALUES ('161', '123', '123', '', '1', '1', '2020-06-07 14:56:02');
INSERT INTO `uwt_users_login_his` VALUES ('162', '123', '123', '', '1', '1', '2020-06-07 15:14:44');
INSERT INTO `uwt_users_login_his` VALUES ('163', '123', '123', '', '1', '1', '2020-06-07 16:26:53');
INSERT INTO `uwt_users_login_his` VALUES ('164', 'lll', '123', '', '4', '1', '2020-06-07 16:28:11');
INSERT INTO `uwt_users_login_his` VALUES ('165', '123', '123', '', '1', '1', '2020-06-07 16:32:17');
INSERT INTO `uwt_users_login_his` VALUES ('166', '123', '123', '', '1', '1', '2020-06-07 16:35:47');
INSERT INTO `uwt_users_login_his` VALUES ('167', '123', '123', '', '1', '1', '2020-06-07 16:36:58');
INSERT INTO `uwt_users_login_his` VALUES ('168', '123', '123', '', '1', '1', '2020-06-07 16:38:17');
INSERT INTO `uwt_users_login_his` VALUES ('169', '123', '123', '', '1', '1', '2020-06-07 16:39:40');
INSERT INTO `uwt_users_login_his` VALUES ('170', '123', '123', '', '1', '1', '2020-06-07 16:41:04');
INSERT INTO `uwt_users_login_his` VALUES ('171', '123', '123', '', '1', '1', '2020-06-07 18:24:15');
INSERT INTO `uwt_users_login_his` VALUES ('172', '123', '123', '', '1', '1', '2020-06-07 18:30:32');
INSERT INTO `uwt_users_login_his` VALUES ('173', '123', '123', '', '1', '1', '2020-06-07 20:25:16');
INSERT INTO `uwt_users_login_his` VALUES ('174', '123', '123', '', '1', '1', '2020-06-07 20:31:40');
INSERT INTO `uwt_users_login_his` VALUES ('175', '123', '123', '', '1', '1', '2020-06-07 20:39:24');
INSERT INTO `uwt_users_login_his` VALUES ('176', '123', '123', '', '1', '1', '2020-06-07 20:40:26');
INSERT INTO `uwt_users_login_his` VALUES ('177', '123', '123', '', '1', '1', '2020-06-07 20:49:05');
INSERT INTO `uwt_users_login_his` VALUES ('178', '123', '123', '', '1', '1', '2020-06-07 21:00:04');
INSERT INTO `uwt_users_login_his` VALUES ('179', '123', '123', '', '1', '1', '2020-06-07 21:30:52');
INSERT INTO `uwt_users_login_his` VALUES ('180', '123', '123', '', '1', '1', '2020-06-07 21:32:02');
INSERT INTO `uwt_users_login_his` VALUES ('181', '123', '123', '', '1', '1', '2020-06-07 21:34:48');
INSERT INTO `uwt_users_login_his` VALUES ('182', '123', '123', '', '1', '1', '2020-06-07 22:01:42');
INSERT INTO `uwt_users_login_his` VALUES ('183', '123', '123', '', '1', '1', '2020-06-07 22:04:21');
INSERT INTO `uwt_users_login_his` VALUES ('184', '123', '123', '', '1', '1', '2020-06-08 05:32:58');
INSERT INTO `uwt_users_login_his` VALUES ('185', '123', '123', '', '1', '1', '2020-06-08 05:48:15');
INSERT INTO `uwt_users_login_his` VALUES ('186', '123', '123', '', '1', '1', '2020-06-08 05:58:38');
INSERT INTO `uwt_users_login_his` VALUES ('187', '123', '123', '', '1', '1', '2020-06-08 05:59:37');
INSERT INTO `uwt_users_login_his` VALUES ('188', '123', '123', '', '1', '1', '2020-06-08 05:59:40');
INSERT INTO `uwt_users_login_his` VALUES ('189', '123', '123', '', '1', '1', '2020-06-08 06:00:01');
INSERT INTO `uwt_users_login_his` VALUES ('190', '123', '123', '', '1', '1', '2020-06-08 06:00:12');
INSERT INTO `uwt_users_login_his` VALUES ('191', '123', '123', '', '1', '1', '2020-06-08 06:01:15');
INSERT INTO `uwt_users_login_his` VALUES ('192', '123', '123', '', '1', '1', '2020-06-08 06:02:05');
INSERT INTO `uwt_users_login_his` VALUES ('193', '123', '123', '', '1', '1', '2020-06-08 21:37:40');
INSERT INTO `uwt_users_login_his` VALUES ('194', '123', '123', '', '1', '1', '2020-06-08 21:40:03');
INSERT INTO `uwt_users_login_his` VALUES ('195', '123', '123', '', '1', '1', '2020-06-08 21:41:26');
INSERT INTO `uwt_users_login_his` VALUES ('196', '123', '123', '', '1', '1', '2020-06-09 18:40:12');
INSERT INTO `uwt_users_login_his` VALUES ('197', '123', '123', '', '1', '1', '2020-06-10 05:46:27');
INSERT INTO `uwt_users_login_his` VALUES ('198', '123', '123', '', '1', '1', '2020-06-10 08:43:32');
INSERT INTO `uwt_users_login_his` VALUES ('199', '123', '123', '', '1', '1', '2020-06-10 08:47:59');
INSERT INTO `uwt_users_login_his` VALUES ('200', '123', '123', '', '1', '1', '2020-06-10 08:53:21');
INSERT INTO `uwt_users_login_his` VALUES ('201', '123', '123', '', '1', '1', '2020-06-10 08:57:05');
INSERT INTO `uwt_users_login_his` VALUES ('202', '123', '123', '', '1', '1', '2020-06-10 09:06:54');
INSERT INTO `uwt_users_login_his` VALUES ('203', '123', '123', '', '1', '1', '2020-06-10 09:26:07');
INSERT INTO `uwt_users_login_his` VALUES ('204', '123', '123', '', '1', '1', '2020-06-10 09:33:49');
INSERT INTO `uwt_users_login_his` VALUES ('205', '123', '123', '', '1', '1', '2020-06-10 09:33:55');
INSERT INTO `uwt_users_login_his` VALUES ('206', '123', '123', '', '1', '1', '2020-06-10 09:34:59');
INSERT INTO `uwt_users_login_his` VALUES ('207', '123', '123', '', '1', '1', '2020-06-10 09:53:57');
INSERT INTO `uwt_users_login_his` VALUES ('208', '123', '123', '', '1', '1', '2020-06-10 10:25:07');
INSERT INTO `uwt_users_login_his` VALUES ('209', '123', '123', '', '1', '1', '2020-06-10 10:27:41');
INSERT INTO `uwt_users_login_his` VALUES ('210', '123', '123', '', '1', '1', '2020-06-10 10:27:51');
INSERT INTO `uwt_users_login_his` VALUES ('211', '123', '123', '', '1', '1', '2020-06-10 10:29:47');
INSERT INTO `uwt_users_login_his` VALUES ('212', '123', '123', '', '1', '1', '2020-06-10 10:30:26');
INSERT INTO `uwt_users_login_his` VALUES ('213', '123', '123', '', '1', '1', '2020-06-10 10:34:50');
INSERT INTO `uwt_users_login_his` VALUES ('214', '123', '123', '', '1', '1', '2020-06-10 10:41:00');
INSERT INTO `uwt_users_login_his` VALUES ('215', '123', '123', '', '1', '1', '2020-06-10 10:47:31');
INSERT INTO `uwt_users_login_his` VALUES ('216', '123', '123', '', '1', '1', '2020-06-10 10:48:47');
INSERT INTO `uwt_users_login_his` VALUES ('217', '123', '123', '', '1', '1', '2020-06-11 10:15:33');
INSERT INTO `uwt_users_login_his` VALUES ('218', '123', '123', '', '1', '1', '2020-06-11 10:17:16');
INSERT INTO `uwt_users_login_his` VALUES ('219', '123', '123', '', '1', '1', '2020-06-11 10:23:02');
INSERT INTO `uwt_users_login_his` VALUES ('220', '123', '123', '', '1', '1', '2020-06-11 10:24:42');
INSERT INTO `uwt_users_login_his` VALUES ('221', '123', '123', '', '1', '1', '2020-06-11 12:47:16');
INSERT INTO `uwt_users_login_his` VALUES ('222', '123', '123', '', '1', '1', '2020-06-11 16:05:23');
INSERT INTO `uwt_users_login_his` VALUES ('223', '123', '123', '', '1', '1', '2020-06-11 16:06:52');
INSERT INTO `uwt_users_login_his` VALUES ('224', '123', '123', '', '1', '1', '2020-06-11 16:23:04');
INSERT INTO `uwt_users_login_his` VALUES ('225', '123', '123', '', '1', '1', '2020-06-11 17:19:38');
INSERT INTO `uwt_users_login_his` VALUES ('226', '123', '123', '', '1', '1', '2020-06-11 17:22:03');
INSERT INTO `uwt_users_login_his` VALUES ('227', '123', '123', '', '1', '1', '2020-06-11 17:23:55');
INSERT INTO `uwt_users_login_his` VALUES ('228', '123', '123', '', '1', '1', '2020-06-11 17:26:27');
INSERT INTO `uwt_users_login_his` VALUES ('229', '123', '123', '', '1', '1', '2020-06-11 17:29:44');
INSERT INTO `uwt_users_login_his` VALUES ('230', '123', '123', '', '1', '1', '2020-06-11 17:42:36');
INSERT INTO `uwt_users_login_his` VALUES ('231', '123', '123', '', '1', '1', '2020-06-11 17:50:11');
INSERT INTO `uwt_users_login_his` VALUES ('232', '123', '123', '', '1', '1', '2020-06-11 21:33:52');
INSERT INTO `uwt_users_login_his` VALUES ('233', '123', '123', '', '1', '1', '2020-06-11 21:43:35');
INSERT INTO `uwt_users_login_his` VALUES ('234', '123', '123', '', '1', '1', '2020-06-11 21:45:29');
INSERT INTO `uwt_users_login_his` VALUES ('235', '123', '123', '', '1', '1', '2020-06-11 21:47:50');
INSERT INTO `uwt_users_login_his` VALUES ('236', '123', '123', '', '1', '1', '2020-06-11 21:52:23');
INSERT INTO `uwt_users_login_his` VALUES ('237', '123', '123', '', '1', '1', '2020-06-11 21:58:11');
INSERT INTO `uwt_users_login_his` VALUES ('238', '123', '123', '', '1', '1', '2020-06-11 22:04:57');
INSERT INTO `uwt_users_login_his` VALUES ('239', '123', '123', '', '1', '1', '2020-06-11 22:06:08');
INSERT INTO `uwt_users_login_his` VALUES ('240', '123', '123', '', '1', '1', '2020-06-11 22:07:31');
INSERT INTO `uwt_users_login_his` VALUES ('241', '123', '123', '', '1', '1', '2020-06-11 22:20:26');
INSERT INTO `uwt_users_login_his` VALUES ('242', '123', '123', '', '1', '1', '2020-06-11 22:33:34');
INSERT INTO `uwt_users_login_his` VALUES ('243', '123', '123', '', '1', '1', '2020-06-12 08:42:39');
INSERT INTO `uwt_users_login_his` VALUES ('244', '123', '123', '', '1', '1', '2020-06-12 08:45:23');
INSERT INTO `uwt_users_login_his` VALUES ('245', '123', '123', '', '1', '1', '2020-06-12 08:46:38');
INSERT INTO `uwt_users_login_his` VALUES ('246', 'liuhao', '123456', '', '0', '0', '2020-06-12 10:15:01');
INSERT INTO `uwt_users_login_his` VALUES ('247', 'liuhao', '123456', '', '0', '0', '2020-06-12 10:15:13');
INSERT INTO `uwt_users_login_his` VALUES ('248', 'liuhao', '123456', '', '0', '0', '2020-06-12 10:15:25');
INSERT INTO `uwt_users_login_his` VALUES ('249', '123', '123', '', '1', '1', '2020-06-12 10:15:31');
INSERT INTO `uwt_users_login_his` VALUES ('250', '123', '123', '', '1', '1', '2020-06-12 11:15:16');
INSERT INTO `uwt_users_login_his` VALUES ('251', '123', '123', '', '1', '1', '2020-06-12 11:30:02');
INSERT INTO `uwt_users_login_his` VALUES ('252', '123', '123', '', '1', '1', '2020-06-12 11:32:08');
INSERT INTO `uwt_users_login_his` VALUES ('253', '123', '123', '', '1', '1', '2020-06-12 11:33:16');
INSERT INTO `uwt_users_login_his` VALUES ('254', '123', '123', '', '1', '1', '2020-06-12 11:42:02');
INSERT INTO `uwt_users_login_his` VALUES ('255', '123', '123', '', '1', '1', '2020-06-12 11:53:56');
INSERT INTO `uwt_users_login_his` VALUES ('256', '123', '123', '', '1', '1', '2020-06-12 11:58:57');
INSERT INTO `uwt_users_login_his` VALUES ('257', '123', '123', '', '1', '1', '2020-06-12 12:01:36');
INSERT INTO `uwt_users_login_his` VALUES ('258', '123', '123', '', '1', '1', '2020-06-12 12:01:54');
INSERT INTO `uwt_users_login_his` VALUES ('259', '123', '123', '', '1', '1', '2020-06-12 12:07:57');
INSERT INTO `uwt_users_login_his` VALUES ('260', '123', '123', '', '1', '1', '2020-06-12 21:31:14');
INSERT INTO `uwt_users_login_his` VALUES ('261', '123', '123', '', '1', '1', '2020-06-12 21:33:00');
INSERT INTO `uwt_users_login_his` VALUES ('262', '123', '123', '', '1', '1', '2020-06-12 21:36:37');
INSERT INTO `uwt_users_login_his` VALUES ('263', '123', '123', '', '1', '1', '2020-06-12 21:36:47');
INSERT INTO `uwt_users_login_his` VALUES ('264', '123', '123', '', '1', '1', '2020-06-12 21:47:07');
INSERT INTO `uwt_users_login_his` VALUES ('265', '123', '123', '', '1', '1', '2020-06-12 21:48:53');
INSERT INTO `uwt_users_login_his` VALUES ('266', '123', '123', '', '1', '1', '2020-06-12 21:50:29');
INSERT INTO `uwt_users_login_his` VALUES ('267', '123', '123', '', '1', '1', '2020-06-12 21:53:27');
INSERT INTO `uwt_users_login_his` VALUES ('268', '123', '123', '', '1', '1', '2020-06-13 06:28:06');
INSERT INTO `uwt_users_login_his` VALUES ('269', '123', '123', '', '1', '1', '2020-06-13 06:32:41');
INSERT INTO `uwt_users_login_his` VALUES ('270', '123', '123', '', '1', '1', '2020-06-13 06:56:59');
INSERT INTO `uwt_users_login_his` VALUES ('271', '123', '123', '', '1', '1', '2020-06-13 07:06:58');
INSERT INTO `uwt_users_login_his` VALUES ('272', '123', '123', '', '1', '1', '2020-06-13 09:37:44');
INSERT INTO `uwt_users_login_his` VALUES ('273', '123', '123', '', '1', '1', '2020-06-13 09:52:41');
INSERT INTO `uwt_users_login_his` VALUES ('274', '123', '123', '', '1', '1', '2020-06-13 09:53:40');
INSERT INTO `uwt_users_login_his` VALUES ('275', '123', '123', '', '1', '1', '2020-06-13 10:09:36');
INSERT INTO `uwt_users_login_his` VALUES ('276', '123', '123', '', '1', '1', '2020-06-13 10:10:17');
INSERT INTO `uwt_users_login_his` VALUES ('277', '123', '123', '', '1', '1', '2020-06-13 10:15:42');
INSERT INTO `uwt_users_login_his` VALUES ('278', '123', '123', '', '1', '1', '2020-06-13 22:28:02');
INSERT INTO `uwt_users_login_his` VALUES ('279', '123', '123', '', '1', '1', '2020-06-14 11:37:46');
INSERT INTO `uwt_users_login_his` VALUES ('280', '123', '123', '', '1', '1', '2020-06-14 11:39:43');
INSERT INTO `uwt_users_login_his` VALUES ('281', '123', '123', '', '1', '1', '2020-06-14 11:42:06');
INSERT INTO `uwt_users_login_his` VALUES ('282', '123', '123', '', '1', '1', '2020-06-15 06:24:28');
INSERT INTO `uwt_users_login_his` VALUES ('283', '123', '123', '', '1', '1', '2020-06-15 06:36:53');
INSERT INTO `uwt_users_login_his` VALUES ('284', '123', '123', '', '1', '1', '2020-06-15 21:25:45');
INSERT INTO `uwt_users_login_his` VALUES ('285', '123', '123', '', '1', '1', '2020-06-15 21:34:49');
INSERT INTO `uwt_users_login_his` VALUES ('286', '123', '123', '', '1', '1', '2020-06-15 21:50:16');
INSERT INTO `uwt_users_login_his` VALUES ('287', '123', '123', '', '1', '1', '2020-06-15 21:59:06');
INSERT INTO `uwt_users_login_his` VALUES ('288', '123', '123', '', '1', '1', '2020-06-15 22:14:15');
INSERT INTO `uwt_users_login_his` VALUES ('289', '123', '123', '', '1', '1', '2020-06-16 17:07:34');
INSERT INTO `uwt_users_login_his` VALUES ('290', '123', '123', '', '1', '1', '2020-06-16 17:22:32');
INSERT INTO `uwt_users_login_his` VALUES ('291', '123', '123', '', '1', '1', '2020-06-16 17:28:17');
INSERT INTO `uwt_users_login_his` VALUES ('292', '123', '123', '', '1', '1', '2020-06-16 17:42:58');
INSERT INTO `uwt_users_login_his` VALUES ('293', '123', '123', '', '1', '1', '2020-06-16 17:44:49');
INSERT INTO `uwt_users_login_his` VALUES ('294', '123', '123', '', '1', '1', '2020-06-17 15:32:56');
INSERT INTO `uwt_users_login_his` VALUES ('295', '123', '123', '', '1', '1', '2020-06-17 15:42:55');
INSERT INTO `uwt_users_login_his` VALUES ('296', '123', '123', '', '1', '1', '2020-06-17 15:44:43');
INSERT INTO `uwt_users_login_his` VALUES ('297', '123', '123', '', '1', '1', '2020-06-17 17:44:16');
INSERT INTO `uwt_users_login_his` VALUES ('298', '123', '123', '', '1', '1', '2020-06-17 17:47:51');
INSERT INTO `uwt_users_login_his` VALUES ('299', '123', '123', '', '1', '1', '2020-06-17 17:57:50');
INSERT INTO `uwt_users_login_his` VALUES ('300', '123', '123', '', '1', '1', '2020-06-17 18:00:29');
INSERT INTO `uwt_users_login_his` VALUES ('301', '123', '123', '', '1', '1', '2020-06-17 18:02:16');
INSERT INTO `uwt_users_login_his` VALUES ('302', '123', '123', '', '1', '1', '2020-06-17 18:07:35');
INSERT INTO `uwt_users_login_his` VALUES ('303', '123', '123', '', '1', '1', '2020-06-19 09:54:10');
INSERT INTO `uwt_users_login_his` VALUES ('304', '123', '123', '', '1', '1', '2020-06-19 10:35:59');
INSERT INTO `uwt_users_login_his` VALUES ('305', '123', '123', '', '1', '1', '2020-06-20 07:37:06');
INSERT INTO `uwt_users_login_his` VALUES ('306', '123', '123', '', '1', '1', '2020-06-20 07:40:43');
INSERT INTO `uwt_users_login_his` VALUES ('307', '123', '123', '', '1', '1', '2020-06-20 07:45:05');
INSERT INTO `uwt_users_login_his` VALUES ('308', '123', '123', '', '1', '1', '2020-06-20 08:23:23');
INSERT INTO `uwt_users_login_his` VALUES ('309', '123', '123', '', '1', '1', '2020-06-20 08:42:00');
INSERT INTO `uwt_users_login_his` VALUES ('310', '123', '123', '', '1', '1', '2020-06-20 08:52:27');
INSERT INTO `uwt_users_login_his` VALUES ('311', '123', '123', '', '1', '1', '2020-06-20 08:54:32');
INSERT INTO `uwt_users_login_his` VALUES ('312', '123', '123', '', '1', '1', '2020-06-20 08:57:59');
INSERT INTO `uwt_users_login_his` VALUES ('313', '123', '123', '', '1', '1', '2020-06-20 09:07:15');
INSERT INTO `uwt_users_login_his` VALUES ('314', '123', '123', '', '1', '1', '2020-06-20 11:03:39');
INSERT INTO `uwt_users_login_his` VALUES ('315', '123', '123', '', '1', '1', '2020-06-20 11:08:03');
INSERT INTO `uwt_users_login_his` VALUES ('316', '123', '123', '', '1', '1', '2020-06-20 11:22:46');
INSERT INTO `uwt_users_login_his` VALUES ('317', '123', '123', '', '1', '1', '2020-06-20 11:27:10');
INSERT INTO `uwt_users_login_his` VALUES ('318', '123', '123', '', '1', '1', '2020-06-20 11:54:14');
INSERT INTO `uwt_users_login_his` VALUES ('319', '123', '123', '', '1', '1', '2020-06-20 11:55:35');
INSERT INTO `uwt_users_login_his` VALUES ('320', '123', '123', '', '1', '1', '2020-06-20 12:05:20');
INSERT INTO `uwt_users_login_his` VALUES ('321', '123', '123', '', '1', '1', '2020-06-20 12:11:19');
INSERT INTO `uwt_users_login_his` VALUES ('322', '123', '123', '', '1', '1', '2020-06-20 12:26:24');
INSERT INTO `uwt_users_login_his` VALUES ('323', '123', '123', '', '1', '1', '2020-06-20 16:09:56');
INSERT INTO `uwt_users_login_his` VALUES ('324', '123', '123', '', '1', '1', '2020-06-20 21:01:07');
INSERT INTO `uwt_users_login_his` VALUES ('325', '123', '123', '', '1', '1', '2020-06-20 21:09:01');
INSERT INTO `uwt_users_login_his` VALUES ('326', '123', '123', '', '1', '1', '2020-06-20 21:32:06');
INSERT INTO `uwt_users_login_his` VALUES ('327', '123', '123', '', '1', '1', '2020-06-20 21:35:27');
INSERT INTO `uwt_users_login_his` VALUES ('328', '123', '123', '', '1', '1', '2020-06-26 16:06:42');
INSERT INTO `uwt_users_login_his` VALUES ('329', '123', '123', '', '1', '1', '2020-06-26 16:17:44');
INSERT INTO `uwt_users_login_his` VALUES ('330', '123', '123', '', '1', '1', '2020-06-26 16:23:18');
INSERT INTO `uwt_users_login_his` VALUES ('331', '123', '123', '', '1', '1', '2020-06-26 16:35:25');
INSERT INTO `uwt_users_login_his` VALUES ('332', '123', '123', '', '1', '1', '2020-06-26 17:03:19');
INSERT INTO `uwt_users_login_his` VALUES ('333', '123', '123', '', '1', '1', '2020-06-26 17:07:38');
INSERT INTO `uwt_users_login_his` VALUES ('334', '123', '123', '', '1', '1', '2020-06-26 17:09:45');
INSERT INTO `uwt_users_login_his` VALUES ('335', '123', '123', '', '1', '1', '2020-06-26 17:11:05');
INSERT INTO `uwt_users_login_his` VALUES ('336', '123', '123', '', '1', '1', '2020-06-26 17:17:01');
INSERT INTO `uwt_users_login_his` VALUES ('337', '123', '123', '', '1', '1', '2020-06-26 17:35:43');
INSERT INTO `uwt_users_login_his` VALUES ('338', '123', '123', '', '1', '1', '2020-06-26 17:42:01');
INSERT INTO `uwt_users_login_his` VALUES ('339', '123', '123', '', '1', '1', '2020-06-26 17:47:20');
INSERT INTO `uwt_users_login_his` VALUES ('340', '123', '123', '', '1', '1', '2020-06-26 17:49:54');
INSERT INTO `uwt_users_login_his` VALUES ('341', '123', '123', '', '1', '1', '2020-06-26 17:57:36');
INSERT INTO `uwt_users_login_his` VALUES ('342', '123', '123', '', '1', '1', '2020-06-29 09:07:41');
INSERT INTO `uwt_users_login_his` VALUES ('343', '123', '123', '', '1', '1', '2020-06-29 09:14:28');
INSERT INTO `uwt_users_login_his` VALUES ('344', '123', '123', '', '1', '1', '2020-07-02 09:16:49');
INSERT INTO `uwt_users_login_his` VALUES ('345', '123', '123', '', '1', '1', '2020-07-02 09:19:34');
INSERT INTO `uwt_users_login_his` VALUES ('346', '123', '123', '', '1', '1', '2020-07-02 09:25:09');
INSERT INTO `uwt_users_login_his` VALUES ('347', '123', '123', '', '1', '1', '2020-07-02 09:29:30');
INSERT INTO `uwt_users_login_his` VALUES ('348', '123', '123', '', '1', '1', '2020-07-02 09:33:58');
INSERT INTO `uwt_users_login_his` VALUES ('349', '123', '123', '', '1', '1', '2020-07-02 09:44:07');
INSERT INTO `uwt_users_login_his` VALUES ('350', '123', '123', '', '1', '1', '2020-07-02 09:44:59');
INSERT INTO `uwt_users_login_his` VALUES ('351', '123', '123', '', '1', '1', '2020-07-03 10:47:24');
INSERT INTO `uwt_users_login_his` VALUES ('352', '123', '123', '', '1', '1', '2020-07-03 11:18:15');
INSERT INTO `uwt_users_login_his` VALUES ('353', '123', '123', '', '1', '1', '2020-07-09 20:39:29');
INSERT INTO `uwt_users_login_his` VALUES ('354', '123', '123', '', '1', '1', '2020-07-09 20:42:19');
INSERT INTO `uwt_users_login_his` VALUES ('355', '123', '123', '', '1', '1', '2020-07-09 20:45:58');
INSERT INTO `uwt_users_login_his` VALUES ('356', '123', '123', '', '1', '1', '2020-07-11 09:25:47');
INSERT INTO `uwt_users_login_his` VALUES ('357', '123', '123', '', '1', '1', '2020-07-11 09:32:27');
INSERT INTO `uwt_users_login_his` VALUES ('358', '123', '123', '', '1', '1', '2020-07-14 05:55:24');
INSERT INTO `uwt_users_login_his` VALUES ('359', '123', '123', '', '1', '1', '2020-07-14 11:03:33');
INSERT INTO `uwt_users_login_his` VALUES ('360', '123', '123', '', '1', '1', '2020-07-14 11:04:02');
INSERT INTO `uwt_users_login_his` VALUES ('361', '123', '123', '', '1', '1', '2020-07-14 11:04:53');
INSERT INTO `uwt_users_login_his` VALUES ('362', '123', '123', '', '1', '1', '2020-07-22 09:03:55');
INSERT INTO `uwt_users_login_his` VALUES ('363', '123', '123', '', '1', '1', '2020-07-22 09:20:23');
INSERT INTO `uwt_users_login_his` VALUES ('364', '123', '123', '', '1', '1', '2020-07-22 12:43:47');
INSERT INTO `uwt_users_login_his` VALUES ('365', '123', '123', '', '1', '1', '2020-07-22 12:47:37');
INSERT INTO `uwt_users_login_his` VALUES ('366', '123', '123', '', '1', '1', '2020-07-22 12:47:40');
INSERT INTO `uwt_users_login_his` VALUES ('367', '123', '123', '', '1', '1', '2020-07-22 18:03:45');
INSERT INTO `uwt_users_login_his` VALUES ('368', '123', '123', '', '1', '1', '2020-07-22 18:11:05');
INSERT INTO `uwt_users_login_his` VALUES ('369', '123', '123', '', '1', '1', '2020-07-23 08:36:41');
INSERT INTO `uwt_users_login_his` VALUES ('370', '123', '123', '', '1', '1', '2020-07-23 09:04:25');
INSERT INTO `uwt_users_login_his` VALUES ('371', '123', '123', '', '1', '1', '2020-07-23 09:05:57');
INSERT INTO `uwt_users_login_his` VALUES ('372', '123', '123', '', '1', '1', '2020-07-23 09:07:49');
INSERT INTO `uwt_users_login_his` VALUES ('373', '123', '123', '', '1', '1', '2020-07-23 09:13:09');
INSERT INTO `uwt_users_login_his` VALUES ('374', '123', '123', '', '1', '1', '2020-07-23 09:43:45');
INSERT INTO `uwt_users_login_his` VALUES ('375', '123', '123', '', '1', '1', '2020-07-23 10:14:19');
INSERT INTO `uwt_users_login_his` VALUES ('376', '123', '123', '', '1', '1', '2020-07-23 10:57:55');
INSERT INTO `uwt_users_login_his` VALUES ('377', '123', '123', '', '1', '1', '2020-07-23 11:09:04');
INSERT INTO `uwt_users_login_his` VALUES ('378', '123', '123', '', '1', '1', '2020-07-23 11:17:36');
INSERT INTO `uwt_users_login_his` VALUES ('379', '123', '123', '', '1', '1', '2020-07-23 11:26:12');
INSERT INTO `uwt_users_login_his` VALUES ('380', '123', '123', '', '1', '1', '2020-07-25 21:13:20');
INSERT INTO `uwt_users_login_his` VALUES ('381', '123', '123', '', '1', '1', '2020-07-26 08:55:15');
INSERT INTO `uwt_users_login_his` VALUES ('382', '123', '123', '', '1', '1', '2020-07-26 09:04:26');
INSERT INTO `uwt_users_login_his` VALUES ('383', '123', '123', '', '1', '1', '2020-07-26 09:36:39');
INSERT INTO `uwt_users_login_his` VALUES ('384', '123', '123', '', '1', '1', '2020-07-27 08:52:15');
INSERT INTO `uwt_users_login_his` VALUES ('385', '123', '123', '', '1', '1', '2020-07-27 09:49:21');
INSERT INTO `uwt_users_login_his` VALUES ('386', '123', '123', '', '1', '1', '2020-07-27 10:15:39');
INSERT INTO `uwt_users_login_his` VALUES ('387', '123', '123', '', '1', '1', '2020-07-27 10:26:18');
INSERT INTO `uwt_users_login_his` VALUES ('388', '123', '123', '', '1', '1', '2020-07-27 10:36:11');
INSERT INTO `uwt_users_login_his` VALUES ('389', '123', '123', '', '1', '1', '2020-07-27 10:48:40');
INSERT INTO `uwt_users_login_his` VALUES ('390', '123', '123', '', '1', '1', '2020-07-27 11:09:48');
INSERT INTO `uwt_users_login_his` VALUES ('391', '123', '123', '', '1', '1', '2020-07-27 14:11:03');
INSERT INTO `uwt_users_login_his` VALUES ('392', '123', '123', '', '1', '1', '2020-07-27 15:48:31');
INSERT INTO `uwt_users_login_his` VALUES ('393', '123', '123', '', '1', '1', '2020-07-27 15:49:11');
INSERT INTO `uwt_users_login_his` VALUES ('394', '123', '123', '', '1', '1', '2020-07-27 15:50:15');
INSERT INTO `uwt_users_login_his` VALUES ('395', '123', '123', '', '1', '1', '2020-07-28 10:01:52');
INSERT INTO `uwt_users_login_his` VALUES ('396', '123', '123', '', '1', '1', '2020-07-28 14:58:09');
INSERT INTO `uwt_users_login_his` VALUES ('397', '123', '123', '', '1', '1', '2020-07-28 15:12:38');
INSERT INTO `uwt_users_login_his` VALUES ('398', '123', '123', '', '1', '1', '2020-07-28 16:19:40');
INSERT INTO `uwt_users_login_his` VALUES ('399', '123', '123', '', '1', '1', '2020-07-28 16:24:54');
INSERT INTO `uwt_users_login_his` VALUES ('400', '123', '123', '', '1', '1', '2020-07-29 18:42:22');
INSERT INTO `uwt_users_login_his` VALUES ('401', '123', '123', '', '1', '1', '2020-07-29 21:06:12');
INSERT INTO `uwt_users_login_his` VALUES ('402', '123', '123', '', '1', '1', '2020-07-29 21:11:24');
INSERT INTO `uwt_users_login_his` VALUES ('403', '123', '123', '', '1', '1', '2020-07-29 21:56:08');
INSERT INTO `uwt_users_login_his` VALUES ('404', '123', '123', '', '1', '1', '2020-07-29 22:02:54');
INSERT INTO `uwt_users_login_his` VALUES ('405', '123', '123', '', '1', '1', '2020-07-30 08:24:31');
INSERT INTO `uwt_users_login_his` VALUES ('406', '123', '123', '', '1', '1', '2020-07-30 10:02:18');
INSERT INTO `uwt_users_login_his` VALUES ('407', '123', '123', '', '1', '1', '2020-07-30 10:35:29');
INSERT INTO `uwt_users_login_his` VALUES ('408', '123', '123', '', '1', '1', '2020-07-30 10:36:51');
INSERT INTO `uwt_users_login_his` VALUES ('409', '123', '123', '', '1', '1', '2020-07-30 10:38:59');
INSERT INTO `uwt_users_login_his` VALUES ('410', '123', '123', '', '1', '1', '2020-07-30 10:57:38');
INSERT INTO `uwt_users_login_his` VALUES ('411', '123', '123', '', '1', '1', '2020-07-30 11:40:04');
INSERT INTO `uwt_users_login_his` VALUES ('412', '123', '123', '', '1', '1', '2020-07-30 12:01:52');
INSERT INTO `uwt_users_login_his` VALUES ('413', '123', '123', '', '1', '1', '2020-07-30 12:04:04');
INSERT INTO `uwt_users_login_his` VALUES ('414', '123', '123', '', '1', '1', '2020-07-30 12:34:08');
INSERT INTO `uwt_users_login_his` VALUES ('415', '123', '123', '', '1', '1', '2020-07-30 12:40:27');
INSERT INTO `uwt_users_login_his` VALUES ('416', '123', '123', '', '1', '1', '2020-07-30 12:46:38');
INSERT INTO `uwt_users_login_his` VALUES ('417', '123', '123', '', '1', '1', '2020-07-30 12:50:33');
INSERT INTO `uwt_users_login_his` VALUES ('418', '123', '123', '', '1', '1', '2020-07-30 15:34:56');
INSERT INTO `uwt_users_login_his` VALUES ('419', '123', '123', '', '1', '1', '2020-07-30 16:22:42');
INSERT INTO `uwt_users_login_his` VALUES ('420', '123', '123', '', '1', '1', '2020-07-30 21:29:18');
INSERT INTO `uwt_users_login_his` VALUES ('421', '123', '123', '', '1', '1', '2020-07-30 21:31:11');
INSERT INTO `uwt_users_login_his` VALUES ('422', '123', '123', '', '1', '1', '2020-07-30 21:33:18');
INSERT INTO `uwt_users_login_his` VALUES ('423', '123', '123', '', '1', '1', '2020-07-30 21:38:19');
INSERT INTO `uwt_users_login_his` VALUES ('424', '123', '123', '', '1', '1', '2020-07-31 09:43:45');
INSERT INTO `uwt_users_login_his` VALUES ('425', '123', '123', '', '1', '1', '2020-07-31 10:14:36');
INSERT INTO `uwt_users_login_his` VALUES ('426', '123', '123', '', '1', '1', '2020-07-31 10:15:28');
INSERT INTO `uwt_users_login_his` VALUES ('427', '123', '123', '', '1', '1', '2020-07-31 14:40:07');
INSERT INTO `uwt_users_login_his` VALUES ('428', '123', '123', '', '1', '1', '2020-07-31 14:53:54');
INSERT INTO `uwt_users_login_his` VALUES ('429', '123', '123', '', '1', '1', '2020-08-01 20:41:27');
INSERT INTO `uwt_users_login_his` VALUES ('430', '123', '123', '', '1', '1', '2020-08-01 20:43:35');
INSERT INTO `uwt_users_login_his` VALUES ('431', '123', '123', '', '1', '1', '2020-08-01 20:48:39');
INSERT INTO `uwt_users_login_his` VALUES ('432', '123', '123', '', '1', '1', '2020-08-01 22:52:12');
INSERT INTO `uwt_users_login_his` VALUES ('433', '123', '123', '', '1', '1', '2020-08-08 09:35:11');
INSERT INTO `uwt_users_login_his` VALUES ('434', '123', '123', '', '1', '1', '2020-08-08 09:37:06');
INSERT INTO `uwt_users_login_his` VALUES ('435', '123', '123', '', '1', '1', '2020-08-08 09:41:24');
INSERT INTO `uwt_users_login_his` VALUES ('436', '123', '123', '', '1', '1', '2020-08-08 09:42:46');
INSERT INTO `uwt_users_login_his` VALUES ('437', '123', '123', '', '1', '1', '2020-08-08 09:44:39');
INSERT INTO `uwt_users_login_his` VALUES ('438', '123', '123', '', '1', '1', '2020-08-08 09:48:34');
INSERT INTO `uwt_users_login_his` VALUES ('439', '123', '123', '', '1', '1', '2020-08-08 09:55:02');
INSERT INTO `uwt_users_login_his` VALUES ('440', '123', '123', '', '1', '1', '2020-08-08 10:03:21');
INSERT INTO `uwt_users_login_his` VALUES ('441', '123', '123', '', '1', '1', '2020-08-08 10:05:00');
INSERT INTO `uwt_users_login_his` VALUES ('442', '123', '123', '', '1', '1', '2020-08-08 10:22:20');
INSERT INTO `uwt_users_login_his` VALUES ('443', '123', '123', '', '1', '1', '2020-08-08 10:23:40');
INSERT INTO `uwt_users_login_his` VALUES ('444', '123', '123', '', '1', '1', '2020-08-08 10:26:56');
INSERT INTO `uwt_users_login_his` VALUES ('445', '123', '123', '', '1', '1', '2020-08-08 10:34:16');
INSERT INTO `uwt_users_login_his` VALUES ('446', '123', '123', '', '1', '1', '2020-08-10 09:22:46');
INSERT INTO `uwt_users_login_his` VALUES ('447', '123', '123', '', '1', '1', '2020-08-10 09:29:18');
INSERT INTO `uwt_users_login_his` VALUES ('448', '123', '123', '', '1', '1', '2020-08-10 17:44:03');
INSERT INTO `uwt_users_login_his` VALUES ('449', '123', '123', '', '1', '1', '2020-08-10 17:44:58');
INSERT INTO `uwt_users_login_his` VALUES ('450', '123', '123', '', '1', '1', '2020-08-10 17:47:46');
INSERT INTO `uwt_users_login_his` VALUES ('451', '123', '123', '', '1', '1', '2020-08-11 10:49:00');
INSERT INTO `uwt_users_login_his` VALUES ('452', '123', '123', '', '1', '1', '2020-08-11 10:51:45');
INSERT INTO `uwt_users_login_his` VALUES ('453', '123', '123', '', '1', '1', '2020-08-11 10:58:01');
INSERT INTO `uwt_users_login_his` VALUES ('454', '123', '123', '', '1', '1', '2020-08-11 12:57:38');
INSERT INTO `uwt_users_login_his` VALUES ('455', '123', '123', '', '1', '1', '2020-08-11 16:37:59');
INSERT INTO `uwt_users_login_his` VALUES ('456', '123', '123', '', '1', '1', '2020-08-11 17:33:56');
INSERT INTO `uwt_users_login_his` VALUES ('457', '123', '123', 'mgr', '1', '1', '2020-08-14 09:10:10');
INSERT INTO `uwt_users_login_his` VALUES ('458', '123', '123', 'mgr', '1', '1', '2020-08-14 09:43:53');
INSERT INTO `uwt_users_login_his` VALUES ('459', '123', '123', 'mgr', '1', '1', '2020-08-14 09:45:06');
INSERT INTO `uwt_users_login_his` VALUES ('460', '123', '123', 'mgr', '1', '1', '2020-08-14 09:45:22');
INSERT INTO `uwt_users_login_his` VALUES ('461', '123', '123', 'mgr', '1', '1', '2020-08-14 09:48:27');
INSERT INTO `uwt_users_login_his` VALUES ('462', '123', '123', 'mgr', '1', '1', '2020-08-14 17:42:39');
INSERT INTO `uwt_users_login_his` VALUES ('463', '123', '123', 'mgr', '1', '1', '2020-08-16 07:32:13');
INSERT INTO `uwt_users_login_his` VALUES ('464', '123', '123', 'mgr', '1', '1', '2020-08-16 14:56:42');
INSERT INTO `uwt_users_login_his` VALUES ('465', '123', '123', 'mgr', '1', '1', '2020-08-16 16:43:29');
INSERT INTO `uwt_users_login_his` VALUES ('466', '123', '123', 'mgr', '1', '1', '2020-08-16 22:13:20');
INSERT INTO `uwt_users_login_his` VALUES ('467', '123', '123', 'mgr', '1', '1', '2020-08-16 22:22:44');
INSERT INTO `uwt_users_login_his` VALUES ('468', '123', '123', 'mgr', '1', '1', '2020-08-16 22:32:20');
INSERT INTO `uwt_users_login_his` VALUES ('469', '123', '123', 'mgr', '1', '1', '2020-08-16 22:33:14');
INSERT INTO `uwt_users_login_his` VALUES ('470', '123', '123', 'mgr', '1', '1', '2020-08-24 18:40:14');
INSERT INTO `uwt_users_login_his` VALUES ('471', '123', '123', 'mgr', '1', '1', '2020-08-25 08:33:40');
INSERT INTO `uwt_users_login_his` VALUES ('472', '123', '123', 'mgr', '1', '1', '2020-08-25 11:02:08');
INSERT INTO `uwt_users_login_his` VALUES ('473', '123', '123', 'mgr', '1', '1', '2020-08-25 11:18:37');
INSERT INTO `uwt_users_login_his` VALUES ('474', '123', '123', 'mgr', '1', '1', '2020-09-02 09:04:00');
INSERT INTO `uwt_users_login_his` VALUES ('475', '123', '123', 'mgr', '1', '1', '2020-09-16 09:48:45');

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
INSERT INTO `uwt_users_menu_groups` VALUES ('3', '超级管理员菜单组', '', '0', '0', '2020-06-07 14:50:59', '1');

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
INSERT INTO `uwt_users_menu_group_items` VALUES ('10', '系统管理', 'layui-icon-reply-fill', '', '', '0', '3', '205', '0', '2020-06-26 16:08:48', '2020-06-26 16:09:02', '1');

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
) ENGINE=InnoDB AUTO_INCREMENT=1988 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of uwt_users_modules
-- ----------------------------
INSERT INTO `uwt_users_modules` VALUES ('205', '/Banners/Index', '/Banners/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('206', '/Banners/Add', '/Banners/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('207', '/Banners/AddModel', '/Banners/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('208', '/Banners/Modify', '/Banners/Modify', 'page');
INSERT INTO `uwt_users_modules` VALUES ('209', '/Banners/ModifyModel', '/Banners/ModifyModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('210', '/Banners/Del', '/Banners/Del', 'api');
INSERT INTO `uwt_users_modules` VALUES ('211', '/Banners/Detail', '/Banners/Detail', 'page');
INSERT INTO `uwt_users_modules` VALUES ('212', '/Files/List', '/Files/List', 'api');
INSERT INTO `uwt_users_modules` VALUES ('213', '/Files/Upload', '/Files/Upload', 'api');
INSERT INTO `uwt_users_modules` VALUES ('214', '/Home/Index', '/Home/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('215', '/Home/Logs', '/Home/Logs', 'page');
INSERT INTO `uwt_users_modules` VALUES ('216', '/NewsCates/Index', '/NewsCates/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('217', '/NewsCates/GetParentTree', '/NewsCates/GetParentTree', 'api');
INSERT INTO `uwt_users_modules` VALUES ('218', '/NewsCates/Add', '/NewsCates/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('219', '/NewsCates/AddModel', '/NewsCates/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('220', '/NewsCates/Del', '/NewsCates/Del', 'api');
INSERT INTO `uwt_users_modules` VALUES ('221', '/NewsCates/Modify', '/NewsCates/Modify', 'page');
INSERT INTO `uwt_users_modules` VALUES ('222', '/NewsCates/ModifyModel', '/NewsCates/ModifyModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('224', '/News/Index', '/News/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('225', '/News/Add', '/News/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('226', '/News/AddModel', '/News/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('227', '/News/Modify', '/News/Modify', 'page');
INSERT INTO `uwt_users_modules` VALUES ('228', '/News/ModifyModel', '/News/ModifyModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('229', '/News/Del', '/News/Del', 'api');
INSERT INTO `uwt_users_modules` VALUES ('235', '角色管理 - 列表', '/Roles/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('236', '角色管理 - 添加', '/Roles/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('237', '角色管理 - 添加', '/Roles/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('238', '角色管理 - 编辑', '/Roles/Modify', 'page');
INSERT INTO `uwt_users_modules` VALUES ('239', '角色管理 - 编辑', '/Roles/ModifyModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('240', '角色管理 - 删除', '/Roles/Del', 'api');
INSERT INTO `uwt_users_modules` VALUES ('241', '菜单组管理 - 列表', '/MenuGroups/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('242', '菜单组管理 - 添加', '/MenuGroups/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('243', '菜单组管理 - 添加', '/MenuGroups/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('244', '菜单组管理 - 编辑', '/MenuGroups/Modify', 'page');
INSERT INTO `uwt_users_modules` VALUES ('245', '菜单组管理 - 编辑', '/MenuGroups/ModifyModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('246', '菜单组管理 - 编辑树', '/MenuGroups/ModifyTree', 'page');
INSERT INTO `uwt_users_modules` VALUES ('247', '菜单组管理 - 编辑树', '/MenuGroups/ModifyTreeModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1939', '/Home/Form', '/Home/Form', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1940', '/News/ModifyProperties', '/News/ModifyProperties', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1943', '帮助管理 - 列表', '/HelperMgr/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1944', '帮助管理 - 添加', '/HelperMgr/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1945', '帮助管理 - 添加', '/HelperMgr/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1946', '帮助管理 - 编辑', '/HelperMgr/Modify', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1947', '帮助管理 - 编辑', '/HelperMgr/ModifyModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1948', '帮助管理 - 发布', '/HelperMgr/Publish', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1949', '帮助管理 - 撤下', '/HelperMgr/PublishRemove', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1950', '帮助管理 - 删除', '/HelperMgr/Del', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1954', '菜单组管理 - 删除', '/MenuGroups/Del', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1964', '论坛 - 版块管理 - 列表', '/ForumMgr/Areas/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1965', '论坛 - 版块管理 - 添加', '/ForumMgr/Areas/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1966', '论坛 - 版块管理 - 添加', '/ForumMgr/Areas/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1968', '修改配置', '/ForumMgr', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1969', '论坛 - 用户管理 - 列表', '/ForumMgr/Users/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1970', '论坛 - 用户管理 - 违规昵称', '/ForumMgr/Users/NicknameBreak', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1971', '论坛 - 用户管理 - 禁言', '/ForumMgr/Users/BanWords', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1972', '论坛 - 用户管理 - 解除禁言', '/ForumMgr/Users/LiftBanWorks', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1973', '论坛 - 等级 - 列表', '/ForumMgr/Levels/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1974', '论坛 - 等级类型 - 列表', '/ForumMgr/LevelTypes/Index', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1975', '论坛 - 等级类型 - 添加', '/ForumMgr/LevelTypes/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1976', '论坛 - 等级类型 - 添加', '/ForumMgr/LevelTypes/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1977', '论坛 - 等级类型 - 编辑', '/ForumMgr/LevelTypes/Modify', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1978', '论坛 - 等级类型 - 编辑', '/ForumMgr/LevelTypes/ModifyModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1979', '论坛 - 等级类型 - 删除', '/ForumMgr/LevelTypes/Del', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1980', '论坛 - 等级 - 添加', '/ForumMgr/Levels/Add', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1981', '论坛 - 等级 - 添加', '/ForumMgr/Levels/AddModel', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1982', '属性配置', '/ForumMgr', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1983', '/Home/IndexBBS', '/Home/IndexBBS', 'page');
INSERT INTO `uwt_users_modules` VALUES ('1984', '/Topic/CreateTopic', '/Topic/CreateTopic', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1985', '/Topic/CommentTopic', '/Topic/CommentTopic', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1986', '/Topic/TopicList', '/Topic/TopicList', 'api');
INSERT INTO `uwt_users_modules` VALUES ('1987', '/Forums/User/Info', '/Forums/User/Info', 'api');

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
INSERT INTO `uwt_users_roles` VALUES ('2', '超级管理员', '/Home/Index', '', '3', '2020-06-07 16:27:32', '1');

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
INSERT INTO `uwt_users_role_module_ref` VALUES ('16', '2', '205');
INSERT INTO `uwt_users_role_module_ref` VALUES ('17', '2', '206');

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
