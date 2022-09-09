namespace GTA5OnlineTools.Common.Utils;

public static class CoreUtil
{
    /// <summary>
    /// 主窗口标题
    /// </summary>
    public const string MainAppWindowName = "GTA5线上小助手 支持1.61 完全免费 v";

    /// <summary>
    /// 目标进程，默认为GTA5
    /// </summary>
    public const string TargetAppName = "GTA5";

    // 小助手配置文件
    public const string ConfigAddress = "https://api.crazyzhang.cn/update/config.json";

    public static string ChangeAddress = "https://api.crazyzhang.cn/update/server/change.txt";
    public static string NoticeAddress = "https://api.crazyzhang.cn/update/server/notice.txt";

    public static string UpdateAddress = "https://github.com/CrazyZhang666/GTA5OnlineTools/releases/download/update/GTA5OnlineTools.exe";

    // 手动下载更新链接
    public const string ManuallyDownloadAddress = "https://github.com/CrazyZhang666/GTA5OnlineTools/releases";

    public const string GTA5OnlineToolsWebsite = "https://crazyzhang.cn";

    public const string GTA5OnlineToolsHelp = "https://crazyzhang.cn/categories/gta5/";
    public const string GTA5OnlineToolsDiscord = "https://crazyzhang.cn/gta5/20210725032256.html";
    public const string GTA5OnlineToolsIssues = "https://github.com/CrazyZhang666/GTA5OnlineTools/issues/new";

    /// <summary>
    /// 公告内容文本
    /// </summary>
    public static string NoticeText = string.Empty;
    /// <summary>
    /// 更新日志文本
    /// </summary>
    public static string ChangeText = string.Empty;

    /// <summary>
    /// 正在更新时的文件名
    /// </summary>
    public const string HalfwayAppName = "未下载完的小助手更新文件.exe";

    /// <summary>
    /// 程序服务端版本号，如：1.2.3.4
    /// </summary>
    public static Version ServerVersionInfo = Version.Parse("0.0.0.0");

    /// <summary>
    /// 程序客户端版本号，如：1.2.3.4
    /// </summary>
    public static Version ClientVersionInfo = Application.ResourceAssembly.GetName().Version;

    /// <summary>
    /// 计算时间差，即软件运行时间
    /// </summary>
    public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
    {
        var ts1 = new TimeSpan(dateBegin.Ticks);
        var ts2 = new TimeSpan(dateEnd.Ticks);

        return ts1.Subtract(ts2).Duration().ToString("c").Substring(0, 8);
    }

    /// <summary>
    /// 更新完成后的文件名
    /// </summary>
    /// <returns></returns>
    public static string FinalAppName()
    {
        return MainAppWindowName + ServerVersionInfo + ".exe";
    }

    /// <summary>
    /// 执行CMD指令
    /// </summary>
    public static void CMD_Code(string cmd)
    {
        var CmdProcess = new Process();
        CmdProcess.StartInfo.FileName = "cmd.exe";
        CmdProcess.StartInfo.CreateNoWindow = true;                     // 不创建新窗口
        CmdProcess.StartInfo.UseShellExecute = false;                   // 不启用shell启动进程  
        CmdProcess.StartInfo.RedirectStandardInput = true;              // 重定向输入    
        CmdProcess.StartInfo.RedirectStandardOutput = true;             // 重定向标准输出    
        CmdProcess.StartInfo.RedirectStandardError = true;              // 重定向错误输出  
        CmdProcess.StartInfo.Arguments = "/c " + cmd;                   // "/C" 表示执行完命令后马上退出  
        CmdProcess.Start();                                             // 执行   
        CmdProcess.WaitForExit();                                       // 等待程序执行完退出进程  
        CmdProcess.Close();                                             // 结束  
    }
}
