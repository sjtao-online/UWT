using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace UWT.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
            if (File.Exists(filename))
            {
                WriteLine("发现配置文件");
                PublishModel publish = null;
                using (StreamReader sr = new StreamReader(filename))
                {
                    try
                    {
                        publish = JsonSerializer.Deserialize<PublishModel>(sr.ReadToEnd());
                    }
                    catch (Exception ex)
                    {
                        WriteError("读取配置文件异常：\n" + ex.Message);
                        return;
                    }
                }
                WriteLine("解析配置文件");
                string msbuild = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe";
                if (!string.IsNullOrEmpty(publish.MSBuild))
                {
                    msbuild = publish.MSBuild;
                }
                List<string> Errors = new List<string>();
                for (int i=0;i<publish.ProjList.Count;i++)
                {
                    WriteLine($"处理第{i+1}个工程");
                    var item = publish.ProjList[i];
                    string csprojname = item.ProjFilename;
                    if (string.IsNullOrEmpty(csprojname))
                    {
                        Errors.Add($"工程名不存在，第{i+1}个工程");
                        continue;
                    }
                    if (csprojname[0] == '\\')
                    {
                        csprojname = publish.SolutionPath + csprojname;
                    }
                    var argx = $"\"{csprojname}\" /t:Build /p:Configuration={item.Configuration ?? "Release"} /p:Platform={item.Platform ?? "AnyCpu"} /m:3 /v:m /p:Inputs=true /p:Outputs=true";
                    var proc = new Process();
                    proc.StartInfo.FileName = msbuild;
                    proc.StartInfo.Arguments = argx;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.Start();
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        Console.WriteLine(proc.StandardOutput.ReadLine());
                    }
                    proc.WaitForExit();
                    Console.WriteLine("复制文件");
                    string copyPath = Path.GetDirectoryName(csprojname) + @"\Bin\" + (item.Configuration ?? "Release");
                    List<string> filters = item.CopyFilter ?? new List<string>();
                    if (publish.CopyFilter != null)
                    {
                        filters.AddRange(publish.CopyFilter);
                    }
                    filters = filters.Distinct().ToList();
                    string copyTo = item.OutputPath;
                    if (string.IsNullOrEmpty(copyTo))
                    {
                        copyTo = publish.OutputPath;
                    }
                    if (!Directory.Exists(copyTo))
                    {
                        try
                        {
                            Directory.CreateDirectory(copyTo);
                        }
                        catch (Exception ex)
                        {
                            Errors.Add($"创建输出目录失败，第{i+1}个工程:\n" + ex.Message);
                            continue;
                        }
                    }
                    foreach (var fileter in filters)
                    {
                        foreach (var file in Directory.EnumerateFiles(copyPath, fileter, SearchOption.AllDirectories))
                        {
                            if (!string.IsNullOrEmpty(publish.IgnoreRegex))
                            {
                                if (Regex.IsMatch(file, publish.IgnoreRegex))
                                {
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(item.IgnoreRegex))
                            {
                                if (Regex.IsMatch(file, item.IgnoreRegex))
                                {
                                    continue;
                                }
                            }
                            try
                            {
                                string shortName = Path.GetFileName(file);
                                File.Copy(file, Path.Combine(copyTo, shortName), true);
                                Console.WriteLine("成功复制：" + shortName);
                            }
                            catch (Exception ex)
                            {
                                Errors.Add("复制文件失败:\n" + file + "\n" + ex.Message);
                            }
                        }
                    }
                }
                if (Errors.Count == 0)
                {
                    WriteLine("成功");
                }
                else
                {
                    WriteError(string.Join('\n', Errors));
                }
            }
            else
            {
                WriteError("未发现配置文件，请放置配置文件到当前目录[config.json]。");
            }
            WriteLine("按任意键继续...");
            Console.ReadLine();
        }

        static void WriteWarning(string text) => ChangeColor(() => WriteLine(text), ConsoleColor.DarkYellow);

        static void WriteError(string text) => ChangeColor(() => WriteLine(text), ConsoleColor.Red);

        static void WriteLine(string text) => Console.WriteLine(text);

        static void ChangeColor(Action writeline, ConsoleColor color)
        {
            var tmp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writeline();
            Console.ForegroundColor = tmp;
        }
    }
    class PublishModel : BuildBasicModel
    {
        /// <summary>
        /// 解决方案路径
        /// </summary>
        public string SolutionPath { get; set; }
        /// <summary>
        /// 工程列表
        /// </summary>
        public List<BuildModel> ProjList { get; set; }
        /// <summary>
        /// MSBuild.exe路径
        /// </summary>
        public string MSBuild { get; set; }
    }
    class BuildBasicModel
    {
        /// <summary>
        /// 输出目录
        /// </summary>
        public string OutputPath { get; set; }
        /// <summary>
        /// 工程配置 Debug,Release
        /// </summary>
        public string Configuration { get; set; }
        /// <summary>
        /// 平台 AnyCpu,x86,x64
        /// </summary>
        public string Platform { get; set; }
        /// <summary>
        /// 拷贝过滤器
        /// </summary>
        public List<string> CopyFilter { get; set; }
        /// <summary>
        /// 忽略正则
        /// </summary>
        public string IgnoreRegex { get; set; }
    }
    class BuildModel : BuildBasicModel
    {
        /// <summary>
        /// 工程文件名
        /// </summary>
        public string ProjFilename { get; set; }
    }
}
