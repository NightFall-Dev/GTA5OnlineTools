using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public class UC1HacksModel : ObservableObject
{
    private bool _kiddionIsRun = false;
    /// <summary>
    /// Kiddion运行状态
    /// </summary>
    public bool KiddionIsRun
    {
        get => _kiddionIsRun;
        set => SetProperty(ref _kiddionIsRun, value);
    }

    private bool _gTAHaxIsRun = false;
    /// <summary>
    /// GTAHax运行状态
    /// </summary>
    public bool GTAHaxIsRun
    {
        get => _gTAHaxIsRun;
        set => SetProperty(ref _gTAHaxIsRun, value);
    }

    private bool _bincoHaxIsRun = false;
    /// <summary>
    /// BincoHax运行状态
    /// </summary>
    public bool BincoHaxIsRun
    {
        get => _bincoHaxIsRun;
        set => SetProperty(ref _bincoHaxIsRun, value);
    }

    private bool _lSCHaxIsRun = false;
    /// <summary>
    /// LSCHax运行状态
    /// </summary>
    public bool LSCHaxIsRun
    {
        get => _lSCHaxIsRun;
        set => SetProperty(ref _lSCHaxIsRun, value);
    }

    private bool _yimMenuIsRun = false;
    /// <summary>
    /// YimMenu运行状态
    /// </summary>
    public bool YimMenuIsRun
    {
        get => _yimMenuIsRun;
        set => SetProperty(ref _yimMenuIsRun, value);
    }

    private object _frameContent;
    /// <summary>
    /// Frame的呈现内容
    /// </summary>
    public object FrameContent
    {
        get => _frameContent;
        set => SetProperty(ref _frameContent, value);
    }

    private Visibility _frameVisibilityState = Visibility.Collapsed;
    /// <summary>
    /// Frame的显示状态
    /// </summary>
    public Visibility FrameVisibilityState
    {
        get => _frameVisibilityState;
        set => SetProperty(ref _frameVisibilityState, value);
    }
}
