using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Kits;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC3ToolsView.xaml 的交互逻辑
/// </summary>
public partial class UC3ToolsView : UserControl
{
    private InjectorWindow InjectorWindow = null;

    public RelayCommand<string> ToolsButtonClickCommand { get; private set; }
    public RelayCommand<string> HyperlinkClickCommand { get; private set; }

    public UC3ToolsView()
    {
        InitializeComponent();
        this.DataContext = this;

        ToolsButtonClickCommand = new(ToolsButtonClick);
        HyperlinkClickCommand = new(HyperlinkClick);
    }

    private void ToolsButtonClick(string obj)
    {
        AudioUtil.ClickSound();

        switch (obj)
        {
            case "RestartApp":
                RestartAppClick();
                break;
            case "InitCPDPath":
                InitCPDPathClick();
                break;
            case "KiddionChsON":
                KiddionChsONClick();
                break;
            case "KiddionChsOFF":
                KiddionChsOFFClick();
                break;
            case "KiddionKey87":
                KiddionKey87Click();
                break;
            case "KiddionKey104":
                KiddionKey104Click();
                break;
            case "CurrentDirectory":
                CurrentDirectoryClick();
                break;
            case "ReleaseDirectory":
                ReleaseDirectoryClick();
                break;
            case "BigBaseV2Directory":
                BigBaseV2DirectoryClick();
                break;
            case "StoryModeArchive":
                StoryModeArchiveClick();
                break;
            case "EditKiddionConfig":
                EditKiddionConfigClick();
                break;
            case "EditKiddionTP":
                EditKiddionTPClick();
                break;
            case "EditKiddionVC":
                EditKiddionVCClick();
                break;
            case "EditGTAHaxStat":
                EditGTAHaxStatClick();
                break;
            case "ReNameAppCN":
                ReNameAppCNClick();
                break;
            case "ReNameAppEN":
                ReNameAppENClick();
                break;
            case "RefreshDNSCache":
                RefreshDNSCacheClick();
                break;
            case "EditHosts":
                EditHostsClick();
                break;
            case "OpenUpdateWindow":
                OpenUpdateWindowClick();
                break;
            case "GTA5Win2TopMost":
                GTA5Win2TopMostClick();
                break;
            case "GTA5Win2NoTopMost":
                GTA5Win2NoTopMostClick();
                break;
            case "DefenderControl":
                DefenderControlClick();
                break;
            case "BaseInjector":
                BaseInjectorClick();
                break;
            case "MinimizeToTray":
                MinimizeToTrayClick();
                break;
            case "ManualGC":
                ManualGCClick();
                break;
            case "SwitchAudio":
                SwitchAudioClick();
                break;
        }
    }

    /// <summary>
    /// 打开Web链接
    /// </summary>
    /// <param name="url"></param>
    private void HyperlinkClick(string url)
    {
        ProcessUtil.OpenLink(url);
    }

    /// <summary>
    /// GC垃圾回收
    /// </summary>
    private void ManualGCClick()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        MainWindow.ShowNoticeInfo("执行GC垃圾回收成功");
    }

    /// <summary>
    /// 基础DLL注入器
    /// </summary>
    private void BaseInjectorClick()
    {
        if (InjectorWindow == null)
        {
            InjectorWindow = new InjectorWindow();
            InjectorWindow.Show();
        }
        else
        {
            if (InjectorWindow.IsVisible)
            {
                InjectorWindow.Topmost = true;
                InjectorWindow.Topmost = false;
                InjectorWindow.WindowState = WindowState.Normal;
            }
            else
            {
                InjectorWindow = null;
                InjectorWindow = new InjectorWindow();
                InjectorWindow.Show();
            }
        }
    }

    /// <summary>
    /// Win10安全中心设置
    /// </summary>
    private void DefenderControlClick()
    {
        ProcessUtil.OpenProcess("dControl", false);
    }

    /// <summary>
    /// GTA5窗口置顶
    /// </summary>
    private void GTA5Win2TopMostClick()
    {
        ProcessUtil.TopMostProcess(CoreUtil.TargetAppName, true);
    }

    /// <summary>
    /// GTA5窗口取消置顶
    /// </summary>
    private void GTA5Win2NoTopMostClick()
    {
        ProcessUtil.TopMostProcess(CoreUtil.TargetAppName, false);
    }

    /// <summary>
    /// 打开更新窗口
    /// </summary>
    private void OpenUpdateWindowClick()
    {
        // 更新窗口要以对话框形式处理
        var UpdateWindow = new UpdateWindow
        {
            Owner = MainWindow.MainWindowIns
        };
        UpdateWindow.ShowDialog();
    }

    /// <summary>
    /// 编辑Hosts文件
    /// </summary>
    private void EditHostsClick()
    {
        ProcessUtil.OpenLink("notepad.exe", @"C:\windows\system32\drivers\etc\hosts");
    }

    /// <summary>
    /// 刷新DNS缓存
    /// </summary>
    private void RefreshDNSCacheClick()
    {
        CoreUtil.CMD_Code("ipconfig /flushdns");
        MainWindow.ShowNoticeInfo("成功刷新DNS解析程序缓存");
    }

    /// <summary>
    /// 重命名小助手为英文
    /// </summary>
    private void ReNameAppENClick()
    {
        try
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
            if (fileNameWithoutExtension != "GTA5OnlineTools")
            {
                FileUtil.FileReName(FileUtil.Current_Path, "GTA5OnlineTools.exe");

                ProcessUtil.CloseTheseProcess();
                App.AppMainMutex.Dispose();
                ProcessUtil.OpenLink(FileUtil.GetCurrFullPath("GTA5OnlineTools.exe"));
                Application.Current.Shutdown();
            }
            else
            {
                MsgBoxUtil.Information("程序文件名已经符合英文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 重命名小助手为中文
    /// </summary>
    private void ReNameAppCNClick()
    {
        try
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
            if (fileNameWithoutExtension != (CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo))
            {
                FileUtil.FileReName(FileUtil.Current_Path, FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo + ".exe"));

                ProcessUtil.CloseTheseProcess();
                App.AppMainMutex.Dispose();
                ProcessUtil.OpenLink(FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo + ".exe"));
                Application.Current.Shutdown();
            }
            else
            {
                MsgBoxUtil.Information("程序文件名已经符合中文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 编辑GTAHax导入文件
    /// </summary>
    private void EditGTAHaxStatClick()
    {
        ProcessUtil.OpenLink("notepad.exe", FileUtil.GTAHaxStat_Path);
    }

    /// <summary>
    /// 编辑Kiddion自定义载具
    /// </summary>
    private void EditKiddionVCClick()
    {
        ProcessUtil.OpenLink("notepad.exe", FileUtil.Kiddion_Path + @"vehicles.json");
    }

    /// <summary>
    /// 编辑Kiddion自定义传送
    /// </summary>
    private void EditKiddionTPClick()
    {
        ProcessUtil.OpenLink("notepad.exe", FileUtil.Kiddion_Path + @"teleports.json");
    }

    /// <summary>
    /// 编辑Kiddion配置文件
    /// </summary>
    private void EditKiddionConfigClick()
    {
        ProcessUtil.OpenLink("notepad.exe", FileUtil.Kiddion_Path + @"config.json");
    }

    /// <summary>
    /// 程序当前目录
    /// </summary>
    private void CurrentDirectoryClick()
    {
        ProcessUtil.OpenLink(FileUtil.CurrentDirectory_Path);
    }

    /// <summary>
    /// 程序释放目录
    /// </summary>
    private void ReleaseDirectoryClick()
    {
        ProcessUtil.OpenLink(FileUtil.Default_Path);
    }

    /// <summary>
    /// BigBaseV2配置目录
    /// </summary>
    private void BigBaseV2DirectoryClick()
    {
        try
        {
            ProcessUtil.OpenLink(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/BigBaseV2/");
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 故事模式完美存档
    /// </summary>
    private void StoryModeArchiveClick()
    {
        var path = Path.Combine(FileUtil.MyDocuments_Path, @"Rockstar Games\GTA V\Profiles");
        if (!Directory.Exists(path))
        {
            MsgBoxUtil.Error("GTA5故事模式存档路径不存在");
            return;
        }

        if (MessageBox.Show("你确定替换GTA5故事模式存档吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        {
            try
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    var dirIf = new DirectoryInfo(dir);
                    string fullName = Path.Combine(dirIf.FullName, "SGTA50000");
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "Other.SGTA50000", fullName);
                }

                MsgBoxUtil.Information($"GTA5故事模式存档替换成功，请前往我的文档查看\n\n{path}");
            }
            catch (Exception ex)
            {
                MsgBoxUtil.Exception(ex);
            }
        }
    }

    /// <summary>
    /// 启用Kiddion[87键]
    /// </summary>
    private void KiddionKey87Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        ProcessUtil.CloseProcess("Kiddion_Chs");
        FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "key87.config.json", FileUtil.Kiddion_Path + @"config.json");
    }

    /// <summary>
    /// 启用Kiddion[104键]
    /// </summary>
    private void KiddionKey104Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        ProcessUtil.CloseProcess("Kiddion_Chs");
        FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "config.json", FileUtil.Kiddion_Path + @"config.json");
    }

    /// <summary>
    /// 关闭Kiddion汉化
    /// </summary>
    private void KiddionChsOFFClick()
    {
        ProcessUtil.CloseProcess("Kiddion_Chs");
    }

    /// <summary>
    /// 启用Kiddion汉化
    /// </summary>
    private void KiddionChsONClick()
    {
        if (ProcessUtil.IsAppRun("Kiddion"))
        {
            ProcessUtil.CloseProcess("Kiddion_Chs");
            ProcessUtil.OpenProcess("Kiddion_Chs", true);
        }
        else
        {
            MsgBoxUtil.Information("请先启动 Kiddion 程序");
        }
    }

    /// <summary>
    /// 重启程序
    /// </summary>
    private void RestartAppClick()
    {
        ProcessUtil.CloseTheseProcess();
        App.AppMainMutex.Dispose();
        ProcessUtil.OpenLink(FileUtil.Current_Path);
        Application.Current.Shutdown();
    }

    /// <summary>
    /// 初始化配置文件夹
    /// </summary>
    private void InitCPDPathClick()
    {
        try
        {
            if (MessageBox.Show("你确定要初始化配置文件吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Directory.SetCurrentDirectory(FileUtil.CurrentDirectory_Path);
                ProcessUtil.CloseTheseProcess();
                Thread.Sleep(20);
                FileUtil.DelectDir(FileUtil.Default_Path);

                App.AppMainMutex.Dispose();
                ProcessUtil.OpenLink(FileUtil.Current_Path);
                Application.Current.Shutdown();
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 最小化程序到系统托盘
    /// </summary>
    private void MinimizeToTrayClick()
    {
        MainWindow.MainWindowIns.WindowState = WindowState.Minimized;
        MainWindow.MainWindowIns.ShowInTaskbar = false;
        MainWindow.ShowNoticeInfo("程序已最小化到托盘");
    }

    /// <summary>
    /// 切换按键音效
    /// </summary>
    private void SwitchAudioClick()
    {
        if (AudioUtil.ClickSoundIndex < 5)
            AudioUtil.ClickSoundIndex++;
        else
            AudioUtil.ClickSoundIndex = 0;

        AudioUtil.ClickSound();
    }
}
