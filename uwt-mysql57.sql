/*
 Navicat Premium Data Transfer

 Source Server         : 47.92.200.192_3306
 Source Server Type    : MySQL
 Source Server Version : 50717
 Source Host           : 47.92.200.192:3306
 Source Schema         : uwt

 Target Server Type    : MySQL
 Target Server Version : 50717
 File Encoding         : 65001

 Date: 08/08/2020 10:39:36
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for uwt_bbs_area_mgr_ref
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_area_mgr_ref`;
CREATE TABLE `uwt_bbs_area_mgr_ref`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `a_id` int(11) NOT NULL COMMENT '版块Id',
  `u_id` int(11) NOT NULL COMMENT '论坛用户Id',
  `auths` set('topic_approved','user_status','topic_top','topic_digest') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL COMMENT '权限',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_area_topic_ref
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_area_topic_ref`;
CREATE TABLE `uwt_bbs_area_topic_ref`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `t_id` int(11) NOT NULL,
  `a_id` int(11) NOT NULL,
  `status` enum('applying','publish','forbid') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'applying',
  `ex` set('digest','top','hot') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_areas
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_areas`;
CREATE TABLE `uwt_bbs_areas`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `p_id` int(11) NOT NULL COMMENT '父版块Id',
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `desc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL COMMENT '备注',
  `summary` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL COMMENT '说明',
  `icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `mgr_user_id` int(11) NOT NULL COMMENT '版主',
  `status` enum('show') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'show',
  `apply` enum('publish','approved') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'publish',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_topic_back_his
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topic_back_his`;
CREATE TABLE `uwt_bbs_topic_back_his`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `t_b_id` int(11) NOT NULL,
  `content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL COMMENT '内容',
  `add_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_topic_backs
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topic_backs`;
CREATE TABLE `uwt_bbs_topic_backs`  (
  `id` int(11) NOT NULL,
  `t_id` int(11) NOT NULL COMMENT '哪个话题',
  `t_b_id` int(11) NOT NULL COMMENT '回复哪条(楼中楼)',
  `index` int(11) NOT NULL COMMENT '楼层',
  `add_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `status` enum('normal','disabled','delete') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'normal' COMMENT '状态',
  `create_user_id` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_topic_his
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topic_his`;
CREATE TABLE `uwt_bbs_topic_his`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `t_id` int(11) NOT NULL COMMENT '主题Id',
  `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL COMMENT '内容',
  `add_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_topics
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_topics`;
CREATE TABLE `uwt_bbs_topics`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL COMMENT '标题',
  `create_user_id` int(11) NOT NULL COMMENT '创建者',
  `touch_cnt` int(11) NOT NULL DEFAULT 0 COMMENT '查看次数',
  `add_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `status` enum('draft','wait_apply','publish','forbid') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'draft',
  `apply_time` datetime(0) NULL DEFAULT NULL,
  `apply_note` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '' COMMENT '审核意见',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_user_level_types
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_user_level_types`;
CREATE TABLE `uwt_bbs_user_level_types`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin COMMENT = '用户等级类型' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_user_levels
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_user_levels`;
CREATE TABLE `uwt_bbs_user_levels`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `type_id` int(11) NOT NULL,
  `exp` int(11) UNSIGNED NOT NULL,
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin COMMENT = '用户等级信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_bbs_users
-- ----------------------------
DROP TABLE IF EXISTS `uwt_bbs_users`;
CREATE TABLE `uwt_bbs_users`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nickname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `level_type_id` int(11) NOT NULL COMMENT '等级类型',
  `exp` int(11) UNSIGNED NOT NULL DEFAULT 0 COMMENT '经验',
  `join_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin COMMENT = '用户信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_helpers
-- ----------------------------
DROP TABLE IF EXISTS `uwt_helpers`;
CREATE TABLE `uwt_helpers`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `content` text CHARACTER SET utf16 COLLATE utf16_bin NULL,
  `summary` varchar(255) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `publish_time` datetime(0) NULL DEFAULT NULL,
  `url` text CHARACTER SET utf16 COLLATE utf16_bin NOT NULL,
  `author` varchar(255) CHARACTER SET utf16 COLLATE utf16_bin NOT NULL COMMENT '作者名称（随便写的东西）',
  `modify_id` int(11) NOT NULL COMMENT '编辑者Id',
  `creator_id` int(11) NOT NULL COMMENT '创建者Id',
  `add_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf16 COLLATE = utf16_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_normals_banners
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_banners`;
CREATE TABLE `uwt_normals_banners`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `target` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `target_type` enum('Mini','Web','Page','None') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'Page',
  `image` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `cate` enum('Banner','Environment','Course') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'Banner',
  `title` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `sub_title` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `desc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `index` int(11) NOT NULL,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_normals_files
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_files`;
CREATE TABLE `uwt_normals_files`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `filename` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `desc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `file_size` bigint(20) NOT NULL,
  `type` varchar(63) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `add_account_id` int(11) NOT NULL,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_normals_news
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_news`;
CREATE TABLE `uwt_normals_news`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `summary` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_normals_news_cates
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_news_cates`;
CREATE TABLE `uwt_normals_news_cates`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `sub_title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `desc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `p_id` int(11) NOT NULL DEFAULT 0,
  `large_icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `mini_icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_normals_versions
-- ----------------------------
DROP TABLE IF EXISTS `uwt_normals_versions`;
CREATE TABLE `uwt_normals_versions`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '版本名称',
  `version` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '版本号 显示用',
  `logs` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '日志',
  `version_no` int(11) NOT NULL COMMENT '数值版本号，用于排序，越大版本越新',
  `path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '下载地址',
  `type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '类型',
  `publish_time` datetime(0) NOT NULL COMMENT '发布时间',
  `build_time` datetime(0) NOT NULL COMMENT '编译时间',
  `add_time` datetime(0) NOT NULL COMMENT '添加时间',
  `valid` tinyint(1) NOT NULL DEFAULT 1 COMMENT '有效性',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_users_accounts
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_accounts`;
CREATE TABLE `uwt_users_accounts`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `account` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `password` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '',
  `role_id` int(11) NOT NULL,
  `status` enum('enabled','disabled','writenoff') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'enabled',
  `last_login_time` datetime(0) NULL DEFAULT NULL,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `account_name`(`account`) USING BTREE COMMENT '账号唯一性'
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_users_login_his
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_login_his`;
CREATE TABLE `uwt_users_login_his`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '填写的用户名',
  `pwd` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '填写的密码',
  `a_id` int(11) NOT NULL COMMENT '可能的用户为0是登录时用户名不对',
  `status` tinyint(1) NOT NULL COMMENT '0为失败 1为成功',
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '操作时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 446 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_users_menu_group_items
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_menu_group_items`;
CREATE TABLE `uwt_users_menu_group_items`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '图标',
  `tooltip` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '显示提示',
  `desc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '说明',
  `pid` int(11) NOT NULL DEFAULT 0,
  `group_id` int(11) NOT NULL,
  `url` int(11) NOT NULL DEFAULT 0 COMMENT 'Module的ID',
  `index` int(11) NOT NULL DEFAULT 0,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_users_menu_groups
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_menu_groups`;
CREATE TABLE `uwt_users_menu_groups`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `desc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `page_count` int(11) NOT NULL,
  `auth_count` int(11) NOT NULL,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_users_modules
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_modules`;
CREATE TABLE `uwt_users_modules`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `type` enum('page','api') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'page',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1969 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_users_role_module_ref
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_role_module_ref`;
CREATE TABLE `uwt_users_role_module_ref`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `r_id` int(11) NOT NULL,
  `m_id` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 18 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_users_roles
-- ----------------------------
DROP TABLE IF EXISTS `uwt_users_roles`;
CREATE TABLE `uwt_users_roles`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色名',
  `home_page_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色主页地址',
  `desc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `menu_group_id` int(11) NOT NULL,
  `add_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `valid` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for uwt_wechats_users
-- ----------------------------
DROP TABLE IF EXISTS `uwt_wechats_users`;
CREATE TABLE `uwt_wechats_users`  (
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
  `add_time` datetime(0) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
