using GTA5OnlineTools.Models;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Pages;
using GTA5OnlineTools.Features.Core;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC1HacksView.xaml 的交互逻辑
/// </summary>
public partial class UC1HacksView : UserControl
{
    public UC1HacksModel UC1HacksModel { get; set; } = new();

    public RelayCommand<string> HacksClickCommand { get; private set; }
    public RelayCommand<string> ReadMeClickCommand { get; private set; }

    public RelayCommand FrameHideClickCommand { get; private set; }

    private KiddionPage KiddionPage = new();
    private GTAHaxPage GTAHaxPage = new();
    private BincoHaxPage BincoHaxPage = new();
    private LSCHaxPage LSCHaxPage = new();
    private YimMenuPage YimMenuPage = new();

    public UC1HacksView()
    {
        InitializeComponent();
        this.DataContext = this;

        HacksClickCommand = new(HacksClick);
        ReadMeClickCommand = new(ReadMeClick);

        FrameHideClickCommand = new(FrameHideClick);

        var thread = new Thread(MainThread)
        {
            IsBackground = true
        };
        thread.Start();
    }

    /// <summary>
    /// 主线程
    /// </summary>
    private void MainThread()
    {
        while (true)
        {
            // 判断 Kiddion 是否运行
            UC1HacksModel.KiddionIsRun = ProcessUtil.IsAppRun("Kiddion");
            // 判断 GTAHax 是否运行
            UC1HacksModel.GTAHaxIsRun = ProcessUtil.IsAppRun("GTAHax");
            // 判断 BincoHax 是否运行
            UC1HacksModel.BincoHaxIsRun = ProcessUtil.IsAppRun("BincoHax");
            // 判断 LSCHax 是否运行
            UC1HacksModel.LSCHaxIsRun = ProcessUtil.IsAppRun("LSCHax");

            Thread.Sleep(1000);
        }
    }

    private void HacksClick(string hackName)
    {
        AudioUtil.ClickSound();

        switch (hackName)
        {
            case "Kiddion":
                KiddionClick();
                break;
            case "GTAHax":
                GTAHaxClick();
                break;
            case "BincoHax":
                BincoHaxClick();
                break;
            case "LSCHax":
                LSCHaxClick();
                break;
            case "YimMenu":
                YimMenuClick();
                break;
        }
    }

    private void ReadMeClick(string pageName)
    {
        switch (pageName)
        {
            case "KiddionPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = KiddionPage;
                break;
            case "GTAHaxPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = GTAHaxPage;
                break;
            case "BincoHaxPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = BincoHaxPage;
                break;
            case "LSCHaxPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = LSCHaxPage;
                break;
            case "YimMenuPage":
                UC1HacksModel.FrameVisibilityState = Visibility.Visible;
                UC1HacksModel.FrameContent = YimMenuPage;
                break;
        }
    }

    private void KiddionClick()
    {
        bool isRun = false;

        Task.Run(() =>
        {
            if (UC1HacksModel.KiddionIsRun)
            {
                // 先关闭Kiddion汉化程序
                ProcessUtil.CloseProcess("Kiddion_Chs");
                // 如果Kiddion没有运行则打开Kiddion
                if (!ProcessUtil.IsAppRun("Kiddion"))
                    ProcessUtil.OpenProcess("Kiddion", true);

                do
                {
                    // 等待Kiddion启动
                    if (ProcessUtil.IsAppRun("Kiddion"))
                    {
                        // Kiddion进程启动标志
                        isRun = true;
                        // Kiddion菜单界面显示标志
                        bool isShow = false;
                        do
                        {
                            // 拿到Kiddion进程
                            var pKiddion = Process.GetProcessesByName("Kiddion").ToList()[0];
                            // 获取Kiddion窗口句柄
                            var caption_handle = WinAPI.FindWindowEx(pKiddion.MainWindowHandle, IntPtr.Zero, "Static", null);

                            var length = WinAPI.GetWindowTextLength(caption_handle);
                            var windowName = new StringBuilder(length + 1);
                            WinAPI.GetWindowText(caption_handle, windowName, windowName.Capacity);

                            if (windowName.ToString() == "Kiddion's Modest Menu v0.9.4")
                            {
                                isShow = true;
                                ProcessUtil.OpenProcess("Kiddion_Chs", true);
                            }
                            else
                            {
                                isShow = false;
                            }

                            Task.Delay(100).Wait();
                        } while (!isShow);
                    }
                    else
                    {
                        isRun = false;
                    }

                    Task.Delay(250).Wait();
                } while (!isRun);
            }
            else
            {
                ProcessUtil.CloseProcess("Kiddion");
                ProcessUtil.CloseProcess("Kiddion_Chs");
            }
        });

        Task.Run(() =>
        {
            // 模拟任务超时
            Task.Delay(5000);
            isRun = true;
        });
    }

    private void GTAHaxClick()
    {
        if (UC1HacksModel.GTAHaxIsRun)
            ProcessUtil.OpenProcess("GTAHax", false);
        else
            ProcessUtil.CloseProcess("GTAHax");
    }

    private void BincoHaxClick()
    {
        if (UC1HacksModel.BincoHaxIsRun)
            ProcessUtil.OpenProcess("BincoHax", false);
        else
            ProcessUtil.CloseProcess("BincoHax");
    }

    private void LSCHaxClick()
    {
        if (UC1HacksModel.LSCHaxIsRun)
            ProcessUtil.OpenProcess("LSCHax", false);
        else
            ProcessUtil.CloseProcess("LSCHax");
    }

    private void YimMenuClick()
    {
        if (!ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            MsgBoxUtil.Warning("未发现GTA5进程，请先运行GTA5游戏");
            return;
        }

        var InjectInfo = new InjectInfo();
        InjectInfo.DLLPath = FileUtil.Inject_Path + "YimMenu.dll";

        if (string.IsNullOrEmpty(InjectInfo.DLLPath))
        {
            MsgBoxUtil.Warning("发生异常，DLL路径为空");
            return;
        }

        var process = Process.GetProcessesByName("GTA5")[0];
        InjectInfo.PID = process.Id;
        InjectInfo.PName = process.ProcessName;
        InjectInfo.MWindowHandle = process.MainWindowHandle;

        foreach (ProcessModule module in Process.GetProcessById(InjectInfo.PID).Modules)
        {
            if (module.FileName == InjectInfo.DLLPath)
            {
                MsgBoxUtil.Warning("该DLL已经被注入过了，请勿重复注入，游戏中按 Ins 键显示菜单");
                return;
            }
        }

        try
        {
            BaseInjector.SetForegroundWindow(InjectInfo.MWindowHandle);
            BaseInjector.DLLInjector(InjectInfo.PID, InjectInfo.DLLPath);
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void FrameHideClick()
    {
        UC1HacksModel.FrameVisibilityState = Visibility.Collapsed;
        UC1HacksModel.FrameContent = null;
    }
}