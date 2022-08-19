using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public class UC0IndexModel : ObservableObject
{
    private string _noticeInfo;
    /// <summary>
    /// 通知公告
    /// </summary>
    public string NoticeInfo
    {
        get => _noticeInfo;
        set => SetProperty(ref _noticeInfo, value);
    }
}
