using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UWT.Templates.Attributes.Details;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Details;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Models.Templates.Lists;
using UWT.Templates.Services.Extends;

namespace UWT.Templates.Services.Caches
{
    static class ModelCache
    {
        /// <summary>
        /// 列表模型
        /// </summary>
        public static Dictionary<Type, ListViewModel> ListViewModel { get; private set; } = new Dictionary<Type, ListViewModel>();
        /// <summary>
        /// 表单模型
        /// </summary>
        public static Dictionary<Type, FormModel> FormModel { get; private set; } = new Dictionary<Type, FormModel>();
        /// <summary>
        /// 详情模型
        /// </summary>
        public static Dictionary<Type, DetailModel> DetailModel { get; private set; } = new Dictionary<Type, DetailModel>();
        /// <summary>
        /// 以表选择Id时key内部映射
        /// </summary>
        public static Dictionary<string, IChoosenIdFromTableEx> TableRKeyMap { get; private set; } = new Dictionary<string, IChoosenIdFromTableEx>();
        /// <summary>
        /// 错误码信息表
        /// </summary>
        public static Dictionary<int, Controllers.ErrorCodeListModel> ErrorCodeMap { get; private set; } = new Dictionary<int, Controllers.ErrorCodeListModel>();
        /// <summary>
        /// 获得模型
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="froms">字典</param>
        /// <returns></returns>
        public static TModel GetModelFromType<TModel>(Type type, Dictionary<Type, TModel> froms)
            where TModel : class, new()
        {
            if (froms.ContainsKey(type))
            {
                return froms[type];
            }
            else
            {
                foreach (var item in froms)
                {
                    if (item.Key.Namespace == type.Namespace && item.Key.Name == type.Name)
                    {
                        if (typeof(TModel) == typeof(ListViewModel))
                        {
                            var s = item.Value as ListViewModel;
                            var lm = new ListViewModel()
                            {
                                Title = s.Title,
                                BatchKey = s.BatchKey,
                                Color = s.Color,
                                Columns = new List<IListColumnModel>(),
                                BatchProperty = s.BatchProperty == null ? null : (type.GetProperty(s.BatchProperty.Name))
                            };
                            foreach (var c in s.Columns)
                            {
                                s.Columns.Add(new ListColumnModel()
                                {
                                    Title = c.Title,
                                    IsIgnore = c.IsIgnore,
                                    Index = c.Index,
                                    ColumnType = c.ColumnType,
                                    Styles = c.Styles,
                                    Class = c.Class,
                                    Property = type.GetProperty(c.Property.Name)
                                });
                            }
                            return froms[type] = lm as TModel;
                        }
                        else if (typeof(TModel) == typeof(FormModel))
                        {
                            var s = item.Value as FormModel;
                            var fm = new FormModel()
                            {
                                Title = s.Title,
                                FormHandlers = new List<IFormHandlerModel>(),
                                FormItems = new List<IFormItemModel>(),
                                HandleBtnsInTitleBar = s.HandleBtnsInTitleBar,
                                Method = s.Method,
                                Type = s.Type,
                                Url = s.Url,
                                BackUrl = s.BackUrl,
                                CshtmlPartList = new Dictionary<FormCshtmlPosition, List<string>>()
                            };
                            foreach (var h in s.FormHandlers)
                            {
                                fm.FormHandlers.Add(h);
                            }
                            foreach (var it in s.FormItems)
                            {
                                fm.FormItems.Add(new FormItemModel()
                                {
                                    Index = it.Index,
                                    Name = it.Name,
                                    IsFullWidth = it.IsFullWidth,
                                    IsInline = it.IsInline,
                                    IsRequired= it.IsRequired,
                                    Title = it.Title,
                                    Tooltip = it.Tooltip,
                                    ItemType = it.ItemType,
                                    ModelEx = it.ModelEx,
                                    PropertyInfo = type.GetProperty(it.Name)
                                });
                            }
                            foreach (var it in s.CshtmlPartList)
                            {
                                fm.CshtmlPartList.Add(it.Key, it.Value);
                            }
                            return froms[type] = fm as TModel;
                        }
                        else if (typeof(TModel) == typeof(DetailModel))
                        {
                            var s = item.Value as DetailModel;
                            var dm = new DetailModel()
                            {
                                ItemModels = new List<IDetailItemModel>()
                            };
                            foreach (var d in s.ItemModels)
                            {
                                dm.ItemModels.Add(new DetailItemModel()
                                {
                                    Title = d.Title,
                                    Cate = d.Cate,
                                    PropertyInfo = type.GetProperty(d.PropertyInfo.Name)
                                });
                            }
                            return froms[type] = dm as TModel;
                        }
                        return item.Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获得错误信息
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">附加错误信息</param>
        /// <returns>错误信息，null为未定义错误信息</returns>
        public static string GetErrorMsgFromCode(int code, string msg = null)
        {
            if (ErrorCodeMap.ContainsKey(code))
            {
                return msg;
            }
            if (string.IsNullOrEmpty(msg))
            {
                return ErrorCodeMap[code].Desc;
            }
            else
            {
                return $"{ErrorCodeMap[code].Desc}:{msg}";
            }
        }
        /// <summary>
        /// 转换URL
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="url">原URL</param>
        /// <returns></returns>
        public static string RechangeUrl(Type type, string url)
        {
            if (url.StartsWith("."))
            {
                var sp = type.Namespace.Split('.');
                if (type.IsSubclassOf(typeof(Controller)))
                {
                    url = $"/{type.Name.Substring(0, type.Name.Length - "Controller".Length)}/{url.Substring(1)}";
                    if (sp.Contains("Areas"))
                    {
                        url = "/" + sp[sp.Length - 2] + url;
                    }
                }
                else
                {
                    url = $"/{sp[sp.Length - 1]}/{url.Substring(1)}";
                    if (sp.Contains("Areas"))
                    {
                        url = "/" + sp[sp.Length - 3] + url;
                    }
                }
            }
            var assname = type.Assembly.GetName();
            return url.RCalcText(assname.Name);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="assemblies">可以初始化的程序集</param>
        public static void Init(List<Assembly> assemblies = null)
        {
            System.Threading.Thread thread = new System.Threading.Thread(InitModelCache)
            {
                IsBackground = true
            };
            thread.Start(assemblies);
        }
        static List<Assembly> UsedAssembles = null;
        private static void InitModelCache(object _assemblies)
        {
            UsedAssembles = (List<Assembly>)_assemblies;
            if (UsedAssembles == null)
            {
                UsedAssembles = new List<Assembly>() { Assembly.GetEntryAssembly() };
            }
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                string assName = item.GetName().Name;
                if (assName.StartsWith("UWT.Libs") && !assName.EndsWith(".Views"))
                {
                    UsedAssembles.Add(item);
                }
            }
            UsedAssembles.Add(Assembly.GetExecutingAssembly());
            LoggerEx.ConfigAssembilies(UsedAssembles);
            0.LogInformation("正在初始化模型缓存");
            List<string> errorModels = new List<string>();
            Dictionary<string, List<DescNameIdModel>> errCodeMaps = new Dictionary<string, List<DescNameIdModel>>();
            foreach (var item in UsedAssembles)
            {
                foreach (var type in item.GetTypes())
                {
                    try
                    {
                        if ((!type.IsInterface) && typeof(IErrorCodeMap).IsAssignableFrom(type))
                        {
                            var errMap = item.CreateInstance(type.FullName) as IErrorCodeMap;
                            var list = errMap.EnumErrorCodeMsgList();
                            if (list == null || list.Count == 0)
                            {
                                continue;
                            }
                            var assname = item.GetName().Name;
                            if (errCodeMaps.ContainsKey(assname))
                            {
                                errCodeMaps[assname].AddRange(list);
                            }
                            else
                            {
                                errCodeMaps[assname] = list;
                            }
                        }
                        foreach (Attribute att in type.GetCustomAttributes(false))
                        {
                            //  列表项缓存
                            if (att is ListViewModelAttribute)
                            {
                                var listViewAttribute = att as ListViewModelAttribute;
                                ListViewModel listViewModel = new ListViewModel()
                                {
                                    Class = listViewAttribute.Class,
                                    Columns = new List<IListColumnModel>(),
                                    BatchKey = listViewAttribute.BatchKey,
                                    BatchProperty = listViewAttribute.BatchKey == null ? null : type.GetProperty(listViewAttribute.BatchKey),
                                    Title = listViewAttribute.Title,
                                    Color = listViewAttribute.Color
                                };
                                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                                {
                                    ListColumnModel listColumn = null;
                                    var meatt = prop.GetCustomAttribute<ListColumnAttribute>();
                                    if (meatt != null)
                                    {
                                        IListItemExBasicModel mex = null;
                                        switch (meatt.ColumnType)
                                        {
                                            case ColumnType.Text:
                                                break;
                                            case ColumnType.Summary:
                                                break;
                                            case ColumnType.Image:
                                                break;
                                            case ColumnType.Handle:
                                                break;
                                            case ColumnType.Link:
                                                break;
                                            case ColumnType.Index:
                                                break;
                                            case ColumnType.MIndex:
                                                break;
                                            case ColumnType.Cshtml:
                                                mex = (IListItemExBasicModel)BuildFromAttribute(prop.GetCustomAttribute<ListItems.PartCshtmlAttribute>());
                                                break;
                                            default:
                                                break;
                                        }
                                        CellWidth width = new CellWidth()
                                        {
                                            MinWidth = meatt.MinWidth,
                                            MaxWidth = meatt.MaxWidth,
                                        };
                                        FromString(meatt.Width, width);
                                        listColumn = new ListColumnModel()
                                        {
                                            Property = prop,
                                            Index = meatt.Index,
                                            IsIgnore = meatt.Ignore,
                                            Title = meatt.Title,
                                            Styles = meatt.Styles,
                                            Class = meatt.Class,
                                            ColumnType = meatt.ColumnType,
                                            ModelEx = mex,
                                            Width = width
                                        };
                                    }
                                    else
                                    {
                                        listColumn = new ListColumnModel()
                                        {
                                            Property = prop,
                                            Title = prop.Name,
                                            IsIgnore = true,
                                            Width = new CellWidth()
                                            {
                                                MinWidth = 80,
                                                UnitType = CellWidthUnitType.Star,
                                                Value = 1
                                            }
                                        };
                                    }
                                    listViewModel.Columns.Add(listColumn);
                                }
                                listViewModel.Columns.Sort((l, r) => l.Index.CompareTo(r.Index));
                                ListViewModel.Add(type, listViewModel);
                            }
                            //  Form项缓存
                            else if (att is FormModelAttribute)
                            {
                                var formAtt = att as FormModelAttribute;

                                string url = RechangeUrl(type, formAtt.Url);
                                FormModel formModel = new FormModel()
                                {
                                    Title = formAtt.Title,
                                    Method = formAtt.Method,
                                    Type = formAtt.Type.ToLower(),
                                    HandleBtnsInTitleBar = formAtt.HandleBtnsInTitleBar,
                                    Url = url,
                                    FormHandlers = new List<IFormHandlerModel>(),
                                    FormItems = new List<IFormItemModel>(),
                                    BackUrl = formAtt.BackUrl,
                                    CshtmlPartList = new Dictionary<FormCshtmlPosition, List<string>>()
                                };
                                foreach (var handler in type.GetCustomAttributes<FormHandlerAttribute>())
                                {
                                    formModel.FormHandlers.Add(new FormHandlerModel()
                                    {
                                        Class = handler.Class,
                                        Handler = handler.Handler,
                                        Title = handler.Title,
                                        Styles = handler.Styles,
                                        JSCallback = handler.JSCallback
                                    });
                                }
                                foreach (var cpart in type.GetCustomAttributes<FormCshtmlAttribute>())
                                {
                                    if (formModel.CshtmlPartList.ContainsKey(cpart.Position))
                                    {
                                        formModel.CshtmlPartList[cpart.Position].Add(cpart.CshtmlPath);
                                    }
                                    else
                                    {
                                        formModel.CshtmlPartList[cpart.Position] = new List<string>() { cpart.CshtmlPath };
                                    }
                                }
                                if (formModel.FormHandlers.Count == 0)
                                {
                                    formModel.FormHandlers.Add(new FormHandlerModel()
                                    {
                                        Title = "保存"
                                    });
                                }
                                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                                {
                                    FormItemModel formItemModel = new FormItemModel();
                                    var formItem = prop.GetCustomAttribute<FormItemAttribute>();
                                    if (formItem != null)
                                    {
                                        formItemModel.Title = formItem.Title;
                                        formItemModel.Name = prop.Name;
                                        formItemModel.PropertyInfo = prop;
                                        formItemModel.IsRequired = formItem.IsRequire;
                                        formItemModel.IsFullWidth = formItem.TitleIsFullScreen;
                                        formItemModel.IsInline = formItem.IsInline;
                                        formItemModel.Tooltip = formItem.Tooltip;
                                        formItemModel.Index = formItem.Index;
                                        formItemModel.ItemType = formItem.ItemType;
                                        var displayGroup = prop.GetCustomAttribute<FormItemGroupAttribute>();
                                        if (displayGroup != null)
                                        {
                                            formItemModel.GroupName = displayGroup.GroupName;
                                            formItemModel.GroupItemName = displayGroup.GroupItemName;
                                        }
                                        switch (formItem.ItemType)
                                        {
                                            case FormItemType.Text:
                                                var text = prop.GetCustomAttribute<FormItems.TextAttribute>();
                                                if (text == null)
                                                {
                                                    formItemModel.ModelEx = new FormItemsFormText()
                                                    {
                                                        MaxLength = 255,
                                                        MinLength = 0,
                                                        TextCate = FormItems.TextAttribute.Cate.SimpleText,
                                                        Regex = null
                                                    };
                                                }
                                                else
                                                {
                                                    if (text.MaxLength == -1)
                                                    {
                                                        switch (text.TextCate)
                                                        {
                                                            case FormItems.TextAttribute.Cate.SimpleText:
                                                                text.MaxLength = 255;
                                                                break;
                                                            case FormItems.TextAttribute.Cate.AreaText:
                                                                text.MaxLength = short.MaxValue;
                                                                break;
                                                            case FormItems.TextAttribute.Cate.RichText:
                                                            case FormItems.TextAttribute.Cate.RichText_Wang:
                                                            case FormItems.TextAttribute.Cate.RichText_UEditor:
                                                                text.MaxLength = int.MaxValue;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    formItemModel.ModelEx = new FormItemsFormText()
                                                    {
                                                        MaxLength = text.MaxLength,
                                                        MinLength = text.MinLength,
                                                        TextCate = text.TextCate,
                                                        Regex = text.Regex
                                                    };
                                                }
                                                break;
                                            case FormItemType.Integer:
                                                formItemModel.ModelEx = Fill(prop.GetCustomAttribute<FormItems.IntegerAttribute>() ?? new FormItems.IntegerAttribute(), prop);
                                                break;
                                            case FormItemType.Float:
                                                formItemModel.ModelEx = Fill(prop.GetCustomAttribute<FormItems.FloatAttribute>() ?? new FormItems.FloatAttribute(), prop);
                                                break;
                                            case FormItemType.Money:
                                                formItemModel.ModelEx = Fill(prop.GetCustomAttribute<FormItems.MoneyAttribute>() ?? new FormItems.MoneyAttribute(), prop, m =>
                                                {
                                                    var ex = new FormItemMoneyEx();
                                                    ex.DigitCnt = ((FormItems.MoneyAttribute)m).DigitCnt;
                                                    return ex;
                                                });
                                                break;
                                            case FormItemType.Date:
                                            case FormItemType.DateTime:
                                                formItemModel.ModelEx = Fill(prop.GetCustomAttribute<FormItems.DateTimeAttribute>() ?? new FormItems.DateTimeAttribute(), prop);
                                                break;
                                            case FormItemType.TimeSpan:
                                                formItemModel.ModelEx = Fill(prop.GetCustomAttribute<FormItems.TimeSpanAttribute>() ?? new FormItems.TimeSpanAttribute(), prop);
                                                break;
                                            case FormItemType.Password:
                                                var pwd = prop.GetCustomAttribute<FormItems.PasswordAttribute>();
                                                if (pwd == null)
                                                {
                                                    formItemModel.ModelEx = new FormItemsPwd()
                                                    {
                                                        MinLength = 6,
                                                        MaxLength = 20
                                                    };
                                                }
                                                else
                                                {
                                                    formItemModel.ModelEx = new FormItemsPwd()
                                                    {
                                                        MaxLength = pwd.MaxLength,
                                                        MinLength = pwd.MinLength,
                                                        HasConfirm = pwd.HasCofirm,
                                                        Regex = pwd.Regex
                                                    };
                                                }
                                                break;
                                            case FormItemType.File:
                                                var file = prop.GetCustomAttribute<FormItems.FileAttribute>();
                                                if (file == null)
                                                {
                                                    formItemModel.ModelEx = new FormItemsFile()
                                                    {
                                                        FileType = null,
                                                        CanFilter = false,
                                                        CanSelectReadyAll = false,
                                                        MaxSize = -1
                                                    };
                                                }
                                                else
                                                {
                                                    if (file.MaxSize == 0)
                                                    {
                                                        switch (file.FileType)
                                                        {
                                                            case FileTypeConst.Audio:
                                                                file.MaxSize = FileSizeConst._10MB;
                                                                break;
                                                            case FileTypeConst.Video:
                                                                file.MaxSize = FileSizeConst.GB;
                                                                break;
                                                            case FileTypeConst.PowerPoint:
                                                            case FileTypeConst.Word:
                                                            case FileTypeConst.Excel:
                                                            case FileTypeConst.OfficeFiles:
                                                            case FileTypeConst.Pdf:
                                                            case FileTypeConst.Html:
                                                                file.MaxSize = FileSizeConst._2MB;
                                                                break;
                                                            case FileTypeConst.Zip:
                                                                file.MaxSize = FileSizeConst.MB * 20;
                                                                break;
                                                            case FileTypeConst.Image:
                                                            case FileTypeConst.JavaScript:
                                                                file.MaxSize = FileSizeConst.MB;
                                                                break;
                                                            default:
                                                                file.MaxSize = -1;
                                                                break;
                                                        }
                                                    }
                                                    formItemModel.ModelEx = new FormItemsFile()
                                                    {
                                                        FileType = file.FileType,
                                                        CanFilter = file.CanFilter,
                                                        CanSelectReadyAll = file.CanSelectReadyAll,
                                                        CanLinkOther = file.CanLinkOther,
                                                        MaxSize = file.MaxSize,
                                                        CustomIcon = file.CustomIcon
                                                    };
                                                }
                                                break;
                                            case FormItemType.ChooseId:
                                                var choose = prop.GetCustomAttribute<FormItems.ChooseIdAttribute>();
                                                if (choose == null)
                                                {
                                                    var chooseFromTable = prop.GetCustomAttribute<FormItems.ChooseIdFromTableAttribute>();
                                                    if (chooseFromTable == null)
                                                    {
                                                        errorModels.Add("FormModel:" + type.FullName);
                                                        //  出错
                                                    }
                                                    else
                                                    {
                                                        string rKey = Guid.NewGuid().ToString();
                                                        var assemblyName = type.Assembly.GetName().Name;
                                                        var r = new ChoosenIdFromTable()
                                                        {
                                                            IdColumnName = chooseFromTable.IdColumnName.RCalcText(assemblyName),
                                                            ParentIdColumnName = chooseFromTable.ParentIdColumnName.RCalcText(assemblyName),
                                                            NameColumnName = chooseFromTable.NameColumnName.RCalcText(assemblyName),
                                                            RKey = rKey,
                                                            Where = chooseFromTable.Where.RCalcText(assemblyName),
                                                            TableName = chooseFromTable.TableName.RCalcText(assemblyName),
                                                            MultiSelect = prop.PropertyType == typeof(List<int>)
                                                        };
                                                        formItemModel.ModelEx = r;
                                                        TableRKeyMap[rKey] = r;
                                                    }
                                                }
                                                else
                                                {
                                                    formItemModel.ModelEx = new FormItemsChooseId()
                                                    {
                                                        ApiUrl = choose.ApiUrl,
                                                        ChooseKey = choose.ChooseKey,
                                                        MultiSelect = prop.PropertyType == typeof(List<int>)
                                                    };
                                                }
                                                break;
                                            case FormItemType.Color:
                                                break;
                                            case FormItemType.Time:
                                                var time = prop.GetCustomAttribute<FormItems.TimeAttribute>();
                                                if (time == null)
                                                {
                                                    formItemModel.ModelEx = new FormItemsCanRangeMaxMinEx<string>()
                                                    {
                                                        Max = "23:59:59",
                                                        Min = "0:0:0",
                                                        IsRange = PropertyIsRange(prop)
                                                    };
                                                }
                                                else
                                                {
                                                    formItemModel.ModelEx = new FormItemsCanRangeMaxMinEx<string>()
                                                    {
                                                        Max = time.Max,
                                                        Min = time.Min,
                                                        IsRange = PropertyIsRange(prop)
                                                    };
                                                }
                                                break;
                                            case FormItemType.SimpleSelect:
                                                var simple = prop.GetCustomAttribute<FormItems.SimpleSelectAttribute>();
                                                if (simple == null)
                                                {
                                                    //  出错
                                                    errorModels.Add("FormModel:" + type.FullName);
                                                }
                                                else
                                                {
                                                    formItemModel.ModelEx = new SimpleSelect()
                                                    {
                                                        SimpleSelectItemsBuilder = simple.Builder,
                                                        GenericTypePramterIndex = simple.BuilderTemplateTypeIndex,
                                                        DefaultSelected = simple.DefaultSelected
                                                    };
                                                }
                                                break;
                                            case FormItemType.MultiSelect:
                                                var multi = prop.GetCustomAttribute<FormItems.MultiSelectAttribute>();
                                                if (multi == null)
                                                {
                                                    //  出错
                                                    errorModels.Add("FormModel:" + type.FullName);
                                                }
                                                else
                                                {
                                                    HashSet<string> defaultvalues = new HashSet<string>();
                                                    if (!string.IsNullOrEmpty(multi.DefaultSelected))
                                                    {
                                                        defaultvalues = multi.DefaultSelected.SplitFiles(";,|").ToHashSet();
                                                    }
                                                    formItemModel.ModelEx = new MultiSelect()
                                                    {
                                                        MaxSelectCount = multi.MaxSelectCount,
                                                        StyleType = multi.StyleType,
                                                        SimpleSelectItemsBuilder = multi.Builder,
                                                        GenericTypePramterIndex = multi.BuilderTemplateTypeIndex,
                                                        DefaultSelected = defaultvalues
                                                    };
                                                }
                                                break;
                                            case FormItemType.Switch:
                                                var @switch = prop.GetCustomAttribute<FormItems.SwitchAttribute>();
                                                if (@switch == null)
                                                {
                                                    @switch = new FormItems.SwitchAttribute();
                                                }
                                                formItemModel.ModelEx = new Switch()
                                                {
                                                    SwitchText = @switch.OnText + "|" + @switch.OffText,
                                                    DefaultValue = @switch.DefaultValue
                                                };
                                                break;
                                            case FormItemType.Slider:
                                                var slider = prop.GetCustomAttribute<FormItems.SliderAttribute>();
                                                if (slider == null)
                                                {
                                                    slider = new FormItems.SliderAttribute();
                                                }
                                                formItemModel.ModelEx = new SliderModel()
                                                {
                                                    Block = slider.Block,
                                                    Class = slider.Class,
                                                    Max = slider.Max,
                                                    Min = slider.Min,
                                                    IsRange = PropertyIsRange(prop)
                                                };
                                                break;
                                            case FormItemType.List:
                                                if (!prop.PropertyType.IsGenericType)
                                                {
                                                    //  出错
                                                    errorModels.Add("FormModel:" + type.FullName);
                                                }
                                                var list = prop.GetCustomAttribute<FormItems.ListAttribute>();
                                                if (list == null)
                                                {
                                                    list = new FormItems.ListAttribute()
                                                    {
                                                        Flags = FormItems.ListAttribute.FormListFlag.CanAddNew
                                                    };
                                                    if (prop.PropertyType.GenericTypeArguments[0] == typeof(int) || prop.PropertyType.GenericTypeArguments[0] == typeof(string))
                                                    {
                                                        list.Flags |= FormItems.ListAttribute.FormListFlag.ShowAsTag;
                                                    }
                                                }
                                                formItemModel.ModelEx = new FormListModel() { Flags = list.Flags };
                                                break;
                                            case FormItemType.ShowImage:
                                            case FormItemType.ShowText:
                                                var show = prop.GetCustomAttribute<FormItems.ShowAttribute>();
                                                formItemModel.ModelEx = new ShowModel()
                                                {
                                                    ShowDefault = show?.Default
                                                };
                                                break;
                                            case FormItemType.DisplayGroup:
                                                var display = prop.GetCustomAttribute<FormItems.DisplayGroupAttribute>();
                                                if (display == null)
                                                {
                                                    //  出错
                                                    errorModels.Add("FormModel:" + type.FullName);
                                                }
                                                else
                                                {
                                                    formItemModel.ModelEx = new DislplayGroupEx()
                                                    {
                                                        GroupName = display.GroupName,
                                                        SimpleSelectItemsBuilder = display.Builder,
                                                        GenericTypePramterIndex = display.BuilderTemplateTypeIndex,
                                                        DefaultSelected = display.DefaultSelected
                                                    };
                                                }
                                                break;
                                            case FormItemType.PartCshtml:
                                                var cshtml = prop.GetCustomAttribute<FormItems.PartCshtmlAttribute>();
                                                if (cshtml == null)
                                                {
                                                    //  出错
                                                    errorModels.Add("FormModel:" + type.FullName);
                                                }
                                                else
                                                {
                                                    formItemModel.ModelEx = (IFormItemExBasicModel)BuildFromAttribute(prop.GetCustomAttribute<FormItems.PartCshtmlAttribute>(),
                                                    (r, a) =>
                                                    {
                                                        r.CallBackFunc = a.CommitCallback;
                                                    });
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                        formModel.FormItems.Add(formItemModel);
                                    }
                                    else
                                    {
                                        //  还没想好怎么处理
                                    }
                                }
                                //重新排序
                                formModel.FormItems.Sort((l, r) => l.Index.CompareTo(r.Index));
                                FormModel.Add(type, formModel);
                            }
                            //  详情项缓存
                            else if (att is DetailModelAttribute)
                            {
                                var detailModel = att as DetailModelAttribute;
                                DetailModel detailViewModel = new DetailModel()
                                {
                                    ItemModels = new List<IDetailItemModel>()
                                };
                                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                                {
                                    DetailItemModel detailItem = new DetailItemModel();
                                    var meatt = prop.GetCustomAttribute<DetailItemAttribute>();
                                    if (meatt == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        detailItem.Cate = meatt.Cate;
                                        detailItem.Title = meatt.Title;
                                        detailItem.PropertyInfo = prop;
                                        if (meatt.Cate == DetailItemCategory.Cshtml)
                                        {
                                            detailItem.ModelEx = (IDetailItemExBasicModel)BuildFromAttribute(prop.GetCustomAttribute<DetailItems.PartCshtmlAttribute>());
                                        }
                                    }
                                    detailViewModel.ItemModels.Add(detailItem);
                                }
                                DetailModel.Add(type, detailViewModel);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        0.LogError("出错：" + type.FullName + ":\n" + ex.ToString());
                    }
                }
            }
            foreach (var item in errCodeMaps)
            {
                foreach (var err in item.Value)
                {
                    if (ErrorCodeMap.ContainsKey(err.Id))
                    {
                        //  错误
                        errorModels.Add($"错误码{err.Id}二义性 {item.Key}, {ErrorCodeMap[err.Id].AssemblyName}");
                    }
                    else
                    {
                        ErrorCodeMap.Add(err.Id, new Controllers.ErrorCodeListModel()
                        {
                            Code = err.Id,
                            Name = err.Name,
                            Desc = err.Desc,
                            AssemblyName = item.Key
                        });
                    }
                }
            }
            if (errorModels.Count != 0)
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.Append("加载失败 ");
                errMsg.Append(errorModels.Count);
                errMsg.Append("个, 细则如下：\n");
                foreach (var item in errorModels)
                {
                    errMsg.Append("\t");
                    errMsg.Append(item);
                    errMsg.Append("\n");
                }
                0.LogError(errMsg.ToString());
            }
            0.LogInformation($"成功加载 ListViewModel: {ListViewModel.Count}个，FormModel: {FormModel.Count}个，DetailModel: {DetailModel.Count}个，ErrorCode: {ErrorCodeMap.Count}");
        }

        public static Stream GetManifestResourceStream(string name)
        {
            bool isContent = name.StartsWith("/_content/");
            if (isContent)
            {
                name = name.Substring(name.IndexOf('/', 10));
            }
            name = ".wwwroot" + name.Replace("/", ".");
            foreach (var item in UsedAssembles)
            {
#if DEBUG
                string[] rs = item.GetManifestResourceNames();
#endif
                string newName = item.GetName().Name + name;
                var stream = item.GetManifestResourceStream(newName);
                if (stream != null)
                {
                    return stream;
                }
            }
            return null;
        }

        private static bool PropertyIsRange(PropertyInfo prop)
        {
            bool isRange = false;
            if (prop.PropertyType.Namespace + '.' + prop.PropertyType.Name == typeof(Range<>).FullName)
            {
                isRange = true;
            }
            return isRange;
        }

        private static FormItemsCanRangeMaxMinEx<T> Fill<T>(ICanMaxMinEx<T> model, PropertyInfo prop, Func<ICanMaxMinEx<T>, FormItemsCanRangeMaxMinEx<T>> buildnew = null)
        {
            FormItemsCanRangeMaxMinEx<T> ex = null;
            if (buildnew != null)
            {
                ex = buildnew(model);
                ex.IsRange = PropertyIsRange(prop);
                ex.Max = model.Max;
                ex.Min = model.Min;
            }
            else
            {
                ex = new FormItemsCanRangeMaxMinEx<T>()
                {
                    Max = model.Max,
                    Min = model.Min,
                    IsRange = PropertyIsRange(prop)
                };
            }
            return ex;
        }

        private static ICshtmlEx BuildFromAttribute<TAttribute>(TAttribute attr, Action<CshtmlModel, TAttribute> callback = null)
            where TAttribute: IPartCshtmlAttribute
        {
            var r = new CshtmlModel()
            {
                CshtmlPath = attr.PartPath,
                CssList = attr.AppendCSS.SplitFiles(),
                JsList = attr.AppendJS.SplitFiles(),
            };
            callback?.Invoke(r, attr);
            return r;
        }

        private static List<string> SplitFiles(this string files, string splits = ",")
        {
            if (string.IsNullOrEmpty(files))
            {
                return new List<string>();
            }
            return files.Split(splits.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        private static string[] UnitStrings = new string[3]
        {
            "auto",
            "px",
            "*"
        };
        /// <summary>
        /// 从WPF中的GridLength抄过来的
        /// </summary>
        static void FromString(string s, CellWidth w)
        {
            if (string.IsNullOrEmpty(s))
            {
                w.Value = 1;
                w.UnitType = CellWidthUnitType.Star;
                return;
            }
            string text = s.Trim().ToLowerInvariant();
            w.Value = 0.0;
            w.UnitType = CellWidthUnitType.Pixel;
            int length = text.Length;
            int num = 0;
            int i = 0;
            if (text == UnitStrings[i])
            {
                num = UnitStrings[i].Length;
                w.UnitType = (CellWidthUnitType)i;
            }
            else
            {
                for (i = 1; i < UnitStrings.Length; i++)
                {
                    if (text.EndsWith(UnitStrings[i], StringComparison.Ordinal))
                    {
                        num = UnitStrings[i].Length;
                        w.UnitType = (CellWidthUnitType)i;
                        break;
                    }
                }
            }
            if (length == num && (w.UnitType == CellWidthUnitType.Auto || w.UnitType == CellWidthUnitType.Star))
            {
                w.Value = 1.0;
                return;
            }
            string value2 = text.Substring(0, length - num);
            w.Value = Convert.ToDouble(value2);
        }
    }
}
