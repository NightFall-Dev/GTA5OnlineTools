using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Modules.Windows.SpeedMeter;

/// <summary>
/// DrawWindow.xaml 的交互逻辑
/// </summary>
public partial class DrawWindow : Window
{
    private const float MPH = 2.23694f;
    private const float KPH = 3.6f;

    private float SpeedUnit = MPH;

    /////////////////////////////////////////////////

    /// <summary>
    /// 用于关闭窗口的时候销毁线程
    /// </summary>
    private bool isRunning = true;

    public DrawWindowModel DrawWindowModel { get; set; } = new();

    [StructLayout(LayoutKind.Sequential)]
    private struct STYLESTRUCT
    {
        public int styleOld;
        public int styleNew;
    }

    public DrawWindow()
    {
        InitializeComponent();

        this.SourceInitialized += delegate
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = WinAPI.GetWindowLong(hwnd, WinAPI.GWL_EXSTYLE);
            WinAPI.SetWindowLong(hwnd, WinAPI.GWL_EXSTYLE, extendedStyle | WinAPI.WS_EX_TRANSPARENT);
        };
    }

    private void Window_Draw_Loaded(object sender, RoutedEventArgs e)
    {
        ((HwndSource)PresentationSource.FromVisual(this)).AddHook((IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
        {
            // 想要让窗口透明穿透鼠标和触摸等，需要同时设置 WS_EX_LAYERED 和 WS_EX_TRANSPARENT 样式
            // 确保窗口始终有 WS_EX_LAYERED 这个样式，并在开启穿透时设置 WS_EX_TRANSPARENT 样式
            // 但是WPF窗口在未设置 AllowsTransparency = true 时，会自动去掉 WS_EX_LAYERED 样式 (在 HwndTarget 类中)
            // 如果设置了 AllowsTransparency = true 将使用WPF内置的低性能的透明实现
            // 所以这里通过 Hook 的方式，在不使用WPF内置的透明实现的情况下，强行保证这个样式存在
            if (msg == (int)WinAPI.WM_STYLECHANGING && (long)wParam == (long)WinAPI.GWL_EXSTYLE)
            {
                var styleStruct = (STYLESTRUCT)Marshal.PtrToStructure(lParam, typeof(STYLESTRUCT));
                styleStruct.styleNew |= (int)WinAPI.WS_EX_LAYERED;
                Marshal.StructureToPtr(styleStruct, lParam, false);
                handled = true;
            }

            return IntPtr.Zero;
        });

        this.DataContext = this;

        var thread0 = new Thread(DrawThread);
        thread0.IsBackground = true;
        thread0.Start();

        var thread1 = new Thread(MainThread);
        thread1.IsBackground = true;
        thread1.Start();

        Console.Beep(600, 75);
    }

    private void Window_Draw_Closing(object sender, CancelEventArgs e)
    {
        isRunning = false;
    }

    private void DrawThread()
    {
        bool isShow = true;
        bool isChange = false;

        while (isRunning)
        {
            var windowData = Memory.GetGameWindowData();

            this.Dispatcher.Invoke(() =>
            {
                var width = Window_Draw.ActualWidth * ScreenHelper.GetScalingRatio();

                if (DrawData.IsDrawCenter)
                {
                    this.Left = windowData.Left + windowData.Width / 2 - width / 2;
                    this.Top = windowData.Top + windowData.Height - width;
                }
                else
                {
                    this.Left = windowData.Left + windowData.Width - width * 1.05;
                    this.Top = windowData.Top + windowData.Height - width;
                }

                this.Left /= ScreenHelper.GetScalingRatio();
                this.Top /= ScreenHelper.GetScalingRatio();

                if (IsPlayerInCar() && Memory.IsForegroundWindow())
                {
                    if (!isShow)
                    {
                        this.Show();
                        isShow = true;
                    }
                }
                else
                {
                    if (isShow)
                    {
                        this.Hide();
                        isShow = false;
                    }
                }

                if (DrawData.IsShowMPH)
                {
                    if (!isChange)
                    {
                        MeterPlate_Main.Unit = "MPH";
                        MeterPlate_Main.Maximum = 200;
                        SpeedUnit = MPH;
                        isChange = true;
                    }
                }
                else
                {
                    if (isChange)
                    {
                        MeterPlate_Main.Unit = "KPH";
                        MeterPlate_Main.Maximum = 400;
                        SpeedUnit = KPH;
                        isChange = false;
                    }
                }
            });

            Thread.Sleep(200);
        }
    }

    private void MainThread()
    {
        while (isRunning)
        {
            DrawWindowModel.VehicleSpeed = GetVehicleSpeed();
            DrawWindowModel.VehicleGear = GetVehicleGear();

            Thread.Sleep(20);
        }
    }

    /// <summary>
    /// 判断玩家是否在载具中
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerInCar()
    {
        return Memory.Read<int>(Globals.WorldPTR, Offsets.InVehicle) == 1;
    }

    /// <summary>
    /// 获取载具速度
    /// </summary>
    /// <returns></returns>
    private int GetVehicleSpeed()
    {
        if (IsPlayerInCar())
        {
            var v3_1 = Memory.Read<Vector3>(Globals.WorldPTR, new int[] { 0x8, 0xD30, 0x7F0 });
            var VehicleSpeed1 = Math.Sqrt(Math.Pow(v3_1.X, 2) + Math.Pow(v3_1.Y, 2) + Math.Pow(v3_1.Z, 2));

            var v3_2 = Memory.Read<Vector3>(Globals.WorldPTR, new int[] { 0x8, 0xD30, 0x7F0 });
            var VehicleSpeed2 = Math.Sqrt(Math.Pow(v3_2.X, 2) + Math.Pow(v3_2.Y, 2) + Math.Pow(v3_2.Z, 2));

            var VehicleSpeed = VehicleSpeed1 + (VehicleSpeed2 - VehicleSpeed1) * 0.5;
            VehicleSpeed *= SpeedUnit;

            return VehicleSpeed > 0 ? (int)VehicleSpeed : 0;
        }
        else
        {
            //double PlayerSpeed = Memory.Read<double>(Globals.WorldPTR, new int[] { 0x8, 0x850 });
            //return PlayerSpeed * SpeedUnit;

            return 0;
        }
    }

    /// <summary>
    /// 获取载具最大速度
    /// </summary>
    /// <returns></returns>
    private double GetVehicleMaxSpeed()
    {
        return Memory.Read<double>(Globals.UnkPTR, Offsets.VehicleMaxSpeed) * SpeedUnit;
    }

    /// <summary>
    /// 获取载具挡位
    /// </summary>
    /// <returns></returns>
    private string GetVehicleGear()
    {
        var gear = Memory.Read<int>(Globals.UnkPTR, Offsets.VehicleGear);
        return gear == 0 ? "R" : gear.ToString();
    }

    /// <summary>
    /// 获取载具加速度
    /// </summary>
    /// <returns></returns>
    private float GetVehicleRPM()
    {
        return Memory.Read<float>(Globals.UnkPTR, Offsets.VehicleRPM);
    }
}

/// <summary>
/// DrawWindow 数据模型
/// </summary>
public class DrawWindowModel : ObservableObject
{
    private int _vehicleSpeed;
    /// <summary>
    /// 载具速度
    /// </summary>
    public int VehicleSpeed
    {
        get => _vehicleSpeed;
        set => SetProperty(ref _vehicleSpeed, value);
    }

    private string _vehicleGear;
    /// <summary>
    /// 载具档位
    /// </summary>
    public string VehicleGear
    {
        get => _vehicleGear;
        set => SetProperty(ref _vehicleGear, value);
    }
}
