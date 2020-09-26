using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [Controller]
    public class ThemeController
    {
        static Dictionary<string, byte[]> ThemeMapCache = new Dictionary<string, byte[]>();
        static Dictionary<string, Color> UwtColorMap = new Dictionary<string, Color>()
        {
            ["NAVY"] = FromWebColorText("#001f3f"),
            ["BLUE"] = FromWebColorText("#0074D9"),
            ["AQUA"] = FromWebColorText("#7FDBFF"),
            ["TEAL"] = FromWebColorText("#39CCCC"),
            ["OLIVE"] = FromWebColorText("#3D9970"),
            ["GREEN"] = FromWebColorText("#2ECC40"),
            ["LIME"] = FromWebColorText("#01FF70"),
            ["YELLOW"] = FromWebColorText("#FFDC00"),
            ["ORANGE"] = FromWebColorText("#FF851B"),
            ["RED"] = FromWebColorText("#FF4136"),
            ["MAROON"] = FromWebColorText("#85144b"),
            ["FUCHSIA"] = FromWebColorText("#F012BE"),
            ["PURPLE"] = FromWebColorText("#B10DC9"),
            ["BLACK"] = FromWebColorText("#111111"),
            ["GRAY"] = FromWebColorText("#AAAAAA"),
            ["SILVER"] = FromWebColorText("#DDDDDD"),
            ["RED2"] = FromWebColorText("#D24D57"),
            ["GREEN2"] = FromWebColorText("#26A65B"),
            ["ORANGE2"] = FromWebColorText("#EB7347")
        };

        private static int _FromHex1(char c)
        {
            if (c >= 'a' && c <= 'f')
            {
                return 10 + (c - 'a');
            }
            else if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            else
            {
                //  出错
                return 0;
            }
        }

        private static int FromHex(char c1, char c2)
        {
            int h = _FromHex1(c1);
            int l = _FromHex1(c2);
            return h * 0x10 + l;
        }

        private static Color FromWebColorText(string color)
        {
            int a = 0xff, r = 0, g = 0, b = 0;
            color = color.ToLower();
            if (color.StartsWith("#"))
            {
                switch (color.Length - 1)
                {
                    case 1:     //  #A      == #FFAAAAAA
                        a = 0xff;
                        r = g = b = FromHex(color[1], color[1]);
                        break;
                    case 2:     //  #AB     == #FFABABAB
                        a = 0xff;
                        r = g = b = FromHex(color[1], color[2]);
                        break;
                    case 3:     //  #ABC    == #FFAABBCC
                        a = 0xff;
                        r = FromHex(color[1], color[1]);
                        g = FromHex(color[2], color[2]);
                        b = FromHex(color[3], color[3]);
                        break;
                    case 4:     //  #ABCD   == #AABBCCDD
                        a = FromHex(color[1], color[1]);
                        r = FromHex(color[2], color[2]);
                        g = FromHex(color[3], color[3]);
                        b = FromHex(color[4], color[4]);
                        break;
                    case 6:     //  #ABCDEF ==  #FFABCDEF
                        a = 0xff;
                        r = FromHex(color[1], color[2]);
                        g = FromHex(color[3], color[4]);
                        b = FromHex(color[5], color[6]);
                        break;
                    case 8:
                        a = FromHex(color[1], color[2]);
                        r = FromHex(color[3], color[4]);
                        g = FromHex(color[5], color[6]);
                        b = FromHex(color[7], color[8]);
                        break;
                    default:
                        break;
                }
            }
            else if ((color.ToLower().StartsWith("rgb(") || color.ToLower().StartsWith("argb(")) && color.EndsWith(")"))
            {
                var cs = color.Substring(3, color.Length - 3 - 1);
                if (cs.StartsWith("("))
                {
                    cs = cs.Substring(1);
                }
                var rr = cs.Split(',');
                try
                {
                    if (rr.Length == 3)
                    {
                        r = int.Parse(rr[0]);
                        g = int.Parse(rr[1]);
                        b = int.Parse(rr[2]);
                    }
                    else
                    {
                        a = int.Parse(rr[0]);
                        r = int.Parse(rr[1]);
                        g = int.Parse(rr[2]);
                        b = int.Parse(rr[3]);
                    }
                }
                catch (Exception)
                {
                    0.LogError(color + " not a color");
                }
            }
            else
            {
                try
                {
                    return Color.FromName(color);
                }
                catch (Exception)
                {
                    0.LogError(color + " not a color");
                    return Color.Empty;
                }
            }
            return Color.FromArgb(a, r, g, b);
        }
        [Route("/bbs/themes/{theme}")]
        public IActionResult ThemeFile(string theme)
        {
            theme = theme.ToUpper();
            if (!ThemeMapCache.ContainsKey(theme))
            {
                lock (ThemeMapCache)
                {
                    Color color = Color.Transparent;
                    if (!UwtColorMap.ContainsKey(theme))
                    {
                        if (BBSEx.BbsConfigModel.Themes != null)
                        {
                            foreach (var item in BBSEx.BbsConfigModel.Themes)
                            {
                                UwtColorMap[item.Key] = FromWebColorText(item.Value);
                            }
                        }
                    }
                    if (!UwtColorMap.ContainsKey(theme))
                    {
                        return new NotFoundResult();
                    }
                    color = UwtColorMap[theme];
                    var txt = string.Format("header{{background-color:rgba({1},{2},{3},{0})}}.page-selector button.current,.page-selector button.current:hover{{background-color:rgba({1},{2},{3},{0});border-color:rgba({4},{5},{6},{0})}}.page-selector button:hover{{background-color:rgba({4},{5},{6},{0})}}.page-selector button.unhandle:hover{{background-color:#efefef}}",
                        color.A, color.R, color.G, color.B, (color.R - 3) >= 0 ? (color.R - 3) : 0, (color.G - 3) >= 0 ? (color.G - 3) : 0, (color.B - 3) >= 0 ? (color.B - 3) : 0);
                    ThemeMapCache[theme] = Encoding.UTF8.GetBytes(txt);
                }
            }
            return new FileContentResult(ThemeMapCache[theme], "text/css; charset=utf-8");
        }
    }
}
