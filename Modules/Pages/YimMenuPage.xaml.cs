using GTA5OnlineTools.Common.Utils;

namespace GTA5OnlineTools.Modules.Pages;

/// <summary>
/// YimMenuPage.xaml 的交互逻辑
/// </summary>
public partial class YimMenuPage : Page
{
    public YimMenuPage()
    {
        InitializeComponent();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }
}
