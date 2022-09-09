using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Common.Utils;

public static class ProcessUtil
{
    /// <summary>
    /// 打开指定链接或程序
    /// </summary>
    /// <param name="link"></param>
    public static void OpenLink(string link)
    {
        Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
    }

    /// <summary>
    /// 打开指定链接或程序
    /// </summary>
    /// <param name="link"></param>
    public static void OpenLink(string link, string path)
    {
        Process.Start(new ProcessStartInfo(link, path) { UseShellExecute = true });
    }

    /// <summary>
    /// 判断程序是否运行
    /// </summary>
    /// <param name="appName">程序名称</param>
    /// <returns>正在运行返回true，未运行返回false</returns>
    public static bool IsAppRun(string appName)
    {
        return Process.GetProcessesByName(appName).ToList().Count > 0;
    }

    /// <summary>
    /// 以管理员权限打开指定程序，不需要后缀.exe
    /// </summary>
    /// <param name="processName">程序名字，要带后缀名</param>
    /// <param name="isKiddion">是否在Kiddion目录下</param>
    public static void OpenProcess(string processName, bool isKiddion)
    {
        try
        {
            if (IsAppRun(processName))
            {
                MsgBoxUtil.Warning($"请不要重复打开，{processName} 已经在运行了");
            }
            else
            {
                string path = string.Empty;
                if (isKiddion)
                    path = FileUtil.Kiddion_Path;
                else
                    path = FileUtil.Cache_Path;

                Directory.SetCurrentDirectory(path);
                path = Path.Combine(path, processName + ".exe");
                Process.Start(new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                    Verb = "runas"
                });
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 是否置顶指定窗口
    /// </summary>
    public static void TopMostProcess(string processName, bool isTopMost)
    {
        try
        {
            if (!IsAppRun(processName))
            {
                MsgBoxUtil.Warning($"未发现 {processName} 进程");
                return;
            }

            var process = Process.GetProcessesByName(processName)[0];
            var windowHandle = process.MainWindowHandle;

            if (isTopMost)
                WinAPI.SetWindowPos(windowHandle, -1, 0, 0, 0, 0, 1 | 2);
            else
                WinAPI.SetWindowPos(windowHandle, -2, 0, 0, 0, 0, 1 | 2);
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 根据进程名字关闭指定程序
    /// </summary>
    /// <param name="processName">程序名字，不需要加.exe</param>
    public static void CloseProcess(string processName)
    {
        var appProcess = Process.GetProcesses();
        foreach (var targetPro in appProcess)
        {
            if (targetPro.ProcessName.Equals(processName))
                targetPro.Kill();
        }
    }

    /// <summary>
    /// 关闭全部第三方exe进程
    /// </summary> 
    public static void CloseTheseProcess()
    {
        CloseProcess("Kiddion");
        CloseProcess("Kiddion_Chs");
        CloseProcess("GTAHax");
        CloseProcess("BincoHax");
        CloseProcess("LSCHax");
        CloseProcess("dControl");
    }
}
