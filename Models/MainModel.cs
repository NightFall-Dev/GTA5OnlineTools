using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public class MainModel : ObservableObject
{
    /// <summary>
    /// 窗口标题
    /// </summary>
    public string WindowTitle { get; set; }

    //////////////////////////////////////////////////////////////

    private string _gTA5IsRun;
    /// <summary>
    /// GTA5是否运行
    /// </summary>
    public string GTA5IsRun
    {
        get => _gTA5IsRun;
        set => SetProperty(ref _gTA5IsRun, value);
    }

    private string _appRunTime;
    /// <summary>
    /// 程序运行时间
    /// </summary>
    public string AppRunTime
    {
        get => _appRunTime;
        set => SetProperty(ref _appRunTime, value);
    }
}
