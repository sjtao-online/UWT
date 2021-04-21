# 使用

# 常量映射代码
```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddConstDictionary(new Dictionary<string, string>()
    {
        //  Banner
        ["BannerController"] = "Banners",
        ["BannerSizeTip"] = "100 * 100",
        //  文章分类
        ["NewsCatesController"] = "NewsCates",
        ["LargeIconTip"] = "文章分类大图标",
        ["IconTip"] = "文章分类中图标",
        ["MiniIconTip"] = "文章分类小图标",
        ["NewsCateTableName"] = "uwt_normal_news_cates",
        ["NewsCateIdColumnName"] = "id",
        ["NewsCateNameColumnName"] = "title",
        ["NewsCateParentIdColumnName"] = "p_id",
        //  文章
        ["NewsController"] = "News",
        ["NewsModifyProperties"] = "ModifyProperties",
    }, "UWT.Libs.Normals");
}

```