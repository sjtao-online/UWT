# UWT.Templates使用说明

## 说明

+ 本模板使用linq2db，暂不支持efcore
+ 本模板使用mvc,暂不支持pages或blazor
  + 使用可以自行使用pages或blazor，可以使用本模板提供的其它功能

## 初次使用指南

### 步骤

1. 添加nuget包
2. 启动配置
3. 界面配置
4. 数据库配置

### 具体操作

1.  添加nuget包
    + Microsoft.Extensions.FileProviders.Embedded (5.0)
    + Microsoft.Extensions.Caching.Memory (5.0)
    + linq2db (3.1.4)
      + linq2db.mysql或linq2db.postgresql都可以

2. 启动配置  
   **文件：** Startup.cs

    ```cs
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //  必要处理
            //  登录页面 登录后重定向url的参数名
            services.AddUWT("/", "ref");

            //  添加自己的处理
            //  ...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //  必要处理
            app.UseUWT();
            //  添加数据库支持(文件名是步骤4中的文件)
            app.UseDbSettings<DataModels.UwtDB>(System.IO.Path.Combine(env.ContentRootPath, "db.conf"));
            //  添加后台布局基本配置，也可以使用另外一个重载做权限处理
            app.UseMgrLayout(new Models.LayoutRouteMap());

            //  添加自己的处理
            //  ...
        }
    }
    ```

3. 界面配置  
   **文件：**/Views/_ViewStart.cshtml

    ```cs
    @using UWT.Templates.Services.Extends
    @{
        Layout = "_Layout";

        //  必要处理
        //  重定向布局文件
        this.ViewStartCallback();
    }
    ```

4. 数据库配置

    ```json
    {
      "current": "home",            //  当前使用哪一个
      "dbsettings": {
        //  配置名，可以配置多个
        "home": {
            "dbtype": "MySql",      //  数据库类型
            "dbname": "uwt",        //  数据库名
            "server": "localhost",  //  ip或域名
            "user": "root",         //  用户名
            "pwd": "123456",        //  密码
            "port": "3306",         //  端口
            "charset": "utf8mb4"    //  字符集
        }
      }
    }
    ```

## 高级特性

+ api返回json结构其实是支持自定义的
  
    ```cs
    //  Startup.cs, Configure方法内
    app.UseApiResultTemplate<ApiResultBasic, ApiResultBasicT>();

    //  定义返回模型
    class ApiResultBasic : IResultModelBasic
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonPropertyName("error")]
        public int Code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        [JsonPropertyName("errormsg")]
        public string Msg { get; set; }
    }
    class ApiResultBasicT : ApiResultBasic, IResultModelBasicT
    {
        /// <summary>
        /// 实体
        /// </summary>
        [JsonPropertyName("data")]
        public object Data { get; set; }
    }


    ```

+ 使用less
    
    ```cs
    //  Startup.cs, ConfigureService方法内
    service.AddLess(true);
    //  service.AddUWT()前面

    //  Startup.cs, Configure方法内
    app.UseLess();

    //  cshtml中使用<link-less path="/" filename-no-ext="file"></link-less>
    //  path为路径名，filename-no-ext为除扩展名的文件名
    //  自动根据使用模式使用less或css
    ```

## 自定义管理界面

+ 用户可用section
    + CSS
      > 添加到head中的后面，用于添加link或style
    + Scripts
      > 添加到body最后，一般用于添加script
    + BodyAppend
      > 添加到scripts之前，所有其它标签之后，一般用来做弹出窗之类的

+ 常用cshtml代码
  ```html
  <!-- 公共api,一般用于api访问 -->
  <script src="/admins/js/common.js"></script>
  <!-- 载入jquery,一般用于不想使用layui但又要操作界面或其它功能情况 -->
  <script src="/admins/js/jquery.min.js"></script>

  ```


## 客户端使用说明

+ api返回Json结构定义  
    *以C#举例，其它语言自行转换*

    ```cs
    //  返回基本模型
    class ApiResultBasic
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }
    }
    //  返回带结构实体模型
    class ApiResultBasic<TData> : ApiResultBasic
    {
        /// <summary>
        /// 实体
        /// </summary>
        public TData Data { get; set; }
    }

    //  分页实体模型
    class PageModel<TModel>
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 结果总数
        /// </summary>
        public int ItemTotal { get; set; }
        /// <summary>
        /// 项目组
        /// </summary>
        public List<TModel> Items { get; set; }
    }
    //  返回分页模型
    class ApiResultPageModel<TModel> : ApiResultBasic<PageModel<TModel>> {}
    ```

+ 登录授权  
  *登录接口返回Header中的Set-Cookie字段，使用接口将内容放于Cookies字段中即可。*
  *或将Set-Cookie中的uwt字段取值，放在请求Headers中Key为uwt*

+ 客户端API接口访问
  + 须添加client-version字段到Headers
    > 注意小写

## 错误码设计原则

### 错误码分区表

区间|含意|备注
|:--|:--|--|
-1|未知错误|这种错误只能人看Msg信息
0|成功|一般Msg会是“Success”或“成功”
1-30|登录与权限相关|
31-100|常规错误|每个错误码对应一个准确的错误信息
101-200|一般错误|每个错误对应一个一般的错误信息，需要调试错误
201-1000|其它问题|未完全使用，后期扩展使用
1001-2000|用户错误|由UWT.Libs.Users使用，如引用此类库，请避开使用
2001-3000|用户错误|由UWT.Libs.Normals使用，如引用此类库，请避开使用
3001-4000|用户错误|由UWT.Libs.WeChats使用，如引用此类库，请避开使用
4001-5000|用户错误|由UWT.Libs.Helpers使用，如引用此类库，请避开使用
5000-6000|用户错误|由UWT.Libs.BBS使用,如引用此类库，请避开使用
\>10000|用户错误|用户自行定义错误函数及用法

### 现有错误码

> 请调用/Errors/ErrorCodeMap获得

## 说明注释

1.  列表列宽使用说明
    + 支持三种类型“auto”,“NUM*”,“NUMpx”或“NUM”
        及auto,2*,10px或10
    + auto:自动宽度，由列最宽内容决定
    + NUM*:权重值，由其它列分配剩余宽度按权重分配，前面的数值是占多少
    + NUMpx或NUM:绝对值，以像素为单位  

例子
```cs
    //  自动宽度列“序号1”，以该列占最长的内容为宽度(不会超过MaxWidth属性的值)
    [ListColumn("序号1", Width="auto")]
    public int Index1 { get; set; }
    //  权重宽度1*,数字1省略写法,建议和“序号4”的一起看
    [ListColumn("序号2", Width="*")]
    public int Index2 { get; set; }
    //  20像素宽度
    [ListColumn("序号3", Width="20px")]
    public int Index3 { get; set; }
    //  权重宽度2.5*，在未到最大或最小宽度的情况下“序号4”的宽度永远是“序号2”的2.5倍，直到最大最小宽度
    [ListColumn("序号4", Width="2.5*")]
    public int Index4 { get; set; }
    //  20像素宽度，px省略写法
    [ListColumn("序号5", Width="20")]
    public int Index5 { get; set; }
```

## 感谢

> 使用layui,x-admin  
> 感谢原作者开源贡献
