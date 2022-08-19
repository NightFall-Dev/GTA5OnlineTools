using GTA5OnlineTools.Common.Utils;

namespace GTA5OnlineTools.Modules.Pages;

/// <summary>
/// BincoHaxPage.xaml 的交互逻辑
/// </summary>
public partial class BincoHaxPage : Page
{
    public BincoHaxPage()
    {
        InitializeComponent();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }
}
