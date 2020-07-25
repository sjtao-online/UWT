# Universally Web Template(UWT)

## 概述

UWT虽然写做“通用Web模板”，其实本身是设计为不擅长或不想写前端的后台程序员可以很简单的生成前端相关页面与代码的。  
本身提供简单配置就可以生成列表及增删改查的相关操作及页面布局。  
并提供相关自定义能力。

## 模块

1. UWT.Templates
    > 基础模块，提供很多模板和操作
2. UWT.Users
    > 用户相关，提供用户，角色，菜单，权限等  
    > 基于UWT.Templates
3. UWT.Helpers
    > 帮助文档，提供帮助文档录入和显示  
    > 基于UWT.Templates、UWT.Users
4. UWT.Normals
    > 常用通用模块，提供了文件、新闻、轮播图、程序版本相关管理  
    > 基于UWT.Templates
5. UWT.WeChats
    > 提供给小程序使用相关  
    > 基于UWT.Templates
6. UWT.Publish
    > 非必要模块，用于快速发布生成UWT相关

> 具体请参考各自的说明文档
