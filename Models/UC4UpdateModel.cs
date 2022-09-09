using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public class UC4UpdateModel : ObservableObject
{
    private string _changeInfo;
    /// <summary>
    /// 更新日志
    /// </summary>
    public string ChangeInfo
    {
        get => _changeInfo;
        set => SetProperty(ref _changeInfo, value);
    }
}
