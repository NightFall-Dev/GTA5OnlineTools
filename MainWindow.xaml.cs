using GTA5OnlineTools.Views;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Modules.Kits;
using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Common.Http;
using GTA5OnlineTools.Common.Utils;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hardcodet.Wpf.TaskbarNotification;

namespace GTA5OnlineTools;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow : Window
{
    // 主窗口数据模型
    public MainModel MainModel { get; set; } = new();
    // 主菜单数据模型
    public List<MenuBar> MenuBars { get; set; } = new();

    // 任务栏图标
    private static TaskbarIcon TaskbarIcon_Main = null;

    // 页面导航命令
    public RelayCommand<MenuBar> NavigateCommand { get; private set; }

    // 用户控件，用于视图切换
    private UC0IndexView UC0IndexView { get; set; } = new();
    private UC1HacksView UC1HacksView { get; set; } = new();
    private UC2ModulesView UC2ModulesView { get; set; } = new();
    private UC3ToolsView UC3ToolsView { get; set; } = new();
    private UC4UpdateView UC4UpdateView { get; set; } = new();
    private UC5AboutView UC5AboutView { get; set; } = new();

    /// <summary>
    /// 用于向外暴露主窗口实例
    /// </summary>
    public static Window MainWindowIns = null;

    // 声明一个变量，用于存储软件开始运行的时间
    private DateTime Origin_DateTime;

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 窗口加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Main_Loaded(object sender, RoutedEventArgs e)
    {
        // 设置当前上下文数据
        this.DataContext = this;
        // 向外暴露主窗口实例
        MainWindowIns = this;

        // 创建主菜单
        CreateMenuBar();
        // 初始化主数据模型
        NavigateCommand = new(Navigate);
        // 首页导航
        ContentControl_Main.Content = UC0IndexView;

        // 获取当前时间，存储到对于变量中
        Origin_DateTime = DateTime.Now;

        //////////////////////////////////////////////////////////////////////////////

        // 设置主窗口标题
        MainModel.WindowTitle = CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo;

        MainModel.GTA5IsRun = "GTA5 : OFF";
        MainModel.AppRunTime = "Runtime : Loading...";

        // 定时器线程
        var timer = new System.Timers.Timer();
        timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
        timer.Interval = 1000;
        timer.AutoReset = true;
        timer.Start();
        // 初始化后台线程
        var thread0 = new Thread(InitThread);
        thread0.IsBackground = true;
        thread0.Start();

        //////////////////////////////////////////////////////////////////////////////

        #region 状态栏图标
        TaskbarIcon_Main = new TaskbarIcon();
        TaskbarIcon_Main.IconSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Favicon.ico", UriKind.RelativeOrAbsolute));
        TaskbarIcon_Main.MenuActivation = PopupActivationMode.RightClick;
        TaskbarIcon_Main.ToolTipText = "GTA5线上小助手";
        TaskbarIcon_Main.Visibility = Visibility.Visible;

        ContextMenu contextMenu = new ContextMenu();
        MenuItem MenuItem_Show = new MenuItem();
        MenuItem_Show.Header = "显示";
        MenuItem_Show.Click += TaskbarIcon_MenuItem_Show_Click;
        MenuItem MenuItem_Exit = new MenuItem();
        MenuItem_Exit.Header = "退出";
        MenuItem_Exit.Click += TaskbarIcon_MenuItem_Exit_Click;
        contextMenu.Items.Add(MenuItem_Show);
        contextMenu.Items.Add(MenuItem_Exit);
        TaskbarIcon_Main.ContextMenu = contextMenu;

        TaskbarIcon_Main.TrayMouseDoubleClick += TaskbarIcon_Main_TrayMouseDoubleClick;
        #endregion
    }

    /// <summary>
    /// 窗口关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Main_Closing(object sender, CancelEventArgs e)
    {
        // 关闭第三方进程
        ProcessUtil.CloseTheseProcess();
        // 释放状态栏图标
        TaskbarIcon_Main.IconSource = null;
        TaskbarIcon_Main.ContextMenu = null;
        TaskbarIcon_Main.Dispose();
        // 结束程序
        Application.Current.Shutdown();
    }

    #region 状态栏图标相关事件
    /// <summary>
    /// 托盘菜单显示点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TaskbarIcon_MenuItem_Show_Click(object sender, RoutedEventArgs e)
    {
        Topmost = true;
        Topmost = false;
        ShowInTaskbar = true;
        WindowState = WindowState.Normal;
    }

    /// <summary>
    /// 托盘菜单退出点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TaskbarIcon_MenuItem_Exit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// 鼠标双击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TaskbarIcon_Main_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
        Topmost = true;
        Topmost = false;
        ShowInTaskbar = true;
        WindowState = WindowState.Normal;
    }

    /// <summary>
    /// 发送通知栏提示信息
    /// </summary>
    /// <param name="msg"></param>
    public static void ShowNoticeInfo(string msg)
    {
        TaskbarIcon_Main?.ShowBalloonTip("提示", msg, BalloonIcon.Info);
    }
    #endregion

    //////////////////////////////////////////////////////////////////////

    private void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar() { Icon = "\xe734", Title = "Index", ColorHex = "#F45221", NameSpace = "UC0IndexView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe630", Title = "Hacks", ColorHex = "#00B2F2", NameSpace = "UC1HacksView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe609", Title = "Modules", ColorHex = "#88C600", NameSpace = "UC2ModulesView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe644", Title = "Tools", ColorHex = "#673AB7", NameSpace = "UC3ToolsView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe652", Title = "Updates", ColorHex = "#FFC501", NameSpace = "UC4UpdateView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe684", Title = "About", ColorHex = "#66CCCC", NameSpace = "UC5AboutView" });
    }

    /// <summary>
    /// 页面导航服务
    /// </summary>
    /// <param name="obj"></param>
    private void Navigate(MenuBar obj)
    {
        if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
            return;

        switch (obj.NameSpace)
        {
            case "UC0IndexView":
                ContentControl_Main.Content = UC0IndexView;
                break;
            case "UC1HacksView":
                ContentControl_Main.Content = UC1HacksView;
                break;
            case "UC2ModulesView":
                ContentControl_Main.Content = UC2ModulesView;
                break;
            case "UC3ToolsView":
                ContentControl_Main.Content = UC3ToolsView;
                break;
            case "UC4UpdateView":
                ContentControl_Main.Content = UC4UpdateView;
                break;
            case "UC5AboutView":
                ContentControl_Main.Content = UC5AboutView;
                break;
        }
    }

    /// <summary>
    /// 计时器独立线程
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Timer_Tick(object sender, ElapsedEventArgs e)
    {
        // 获取软件运行时间
        MainModel.AppRunTime = "Runtime : " + CoreUtil.ExecDateDiff(Origin_DateTime, DateTime.Now);
        // 判断 GTA5 是否运行
        MainModel.GTA5IsRun = ProcessUtil.IsAppRun("GTA5") ? "GTA5 : ON" : "GTA5 : OFF";
    }

    /// <summary>
    /// 初始化线程
    /// </summary>
    private async void InitThread()
    {
        try
        {
            // 刷新DNS缓存
            CoreUtil.CMD_Code("ipconfig /flushdns");
            // 检查更新
            string webConfig = await HttpHelper.HttpClientGET(CoreUtil.ConfigAddress);
            // 解析web返回的数据
            GlobalData.ServerData = JsonUtil.JsonDese<ServerData>(webConfig);
            // 获取对应数据
            CoreUtil.ServerVersionInfo = Version.Parse(GlobalData.ServerData.Version);
            CoreUtil.NoticeAddress = GlobalData.ServerData.Address.Notice;
            CoreUtil.ChangeAddress = GlobalData.ServerData.Address.Change;
            // 获取最新公告
            await HttpHelper.HttpClientGET(CoreUtil.NoticeAddress).ContinueWith((t) =>
            {
                if (t != null)
                    WeakReferenceMessenger.Default.Send(t.Result, "Notice");
                else
                    WeakReferenceMessenger.Default.Send("Failed to get latest announcement content！", "Notice");
            });
            // 获取更新日志
            await HttpHelper.HttpClientGET(CoreUtil.ChangeAddress).ContinueWith((t) =>
            {
                if (t != null)
                    WeakReferenceMessenger.Default.Send(t.Result, "Change");
                else
                    WeakReferenceMessenger.Default.Send("Failed to get changelog information！", "Change");
            });

            // 如果线上版本号大于本地版本号，则提示更新
            if (CoreUtil.ServerVersionInfo > CoreUtil.ClientVersionInfo)
            {
                AudioUtil.SP_GTA5_Email.Play();
                // 打开更新对话框
                OpenUpateWindow();
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 打开更新窗口
    /// </summary>
    private static void OpenUpateWindow()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (MessageBox.Show($"Detect that a new version has been released, whether to go to update immediately？\n\n{GlobalData.ServerData.Latest.Date}\n{GlobalData.ServerData.Latest.Change}\n\nIt is strongly recommended that you use the latest version！",
                "new version found", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                var UpdateWindow = new UpdateWindow
                {
                    // 设置父窗口
                    Owner = MainWindowIns
                };
                // 以对话框形式显示更新窗口
                UpdateWindow.ShowDialog();
            }
        });
    }
}
