using GTA5OnlineTools.Common.Utils;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC5AboutView.xaml 的交互逻辑
/// </summary>
public partial class UC5AboutView : UserControl
{
    public RelayCommand<string> HyperlinkClickCommand { get; private set; }

    public UC5AboutView()
    {
        InitializeComponent();
        this.DataContext = this;

        HyperlinkClickCommand = new(HyperlinkClick);
    }

    private void HyperlinkClick(string url)
    {
        ProcessUtil.OpenLink(url);
    }
}
