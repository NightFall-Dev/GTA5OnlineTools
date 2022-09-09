using GTA5OnlineTools.Models;

using CommunityToolkit.Mvvm.Messaging;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC0IndexView.xaml 的交互逻辑
/// </summary>
public partial class UC0IndexView : UserControl
{
    public UC0IndexModel UC0IndexModel { get; set; } = new();

    public UC0IndexView()
    {
        InitializeComponent();
        this.DataContext = this;

        UC0IndexModel.NoticeInfo = "正在获取最新公告内容...";

        WeakReferenceMessenger.Default.Register<string, string>(this, "Notice", (s, e) =>
        {
            UC0IndexModel.NoticeInfo = e;
        });
    }
}
