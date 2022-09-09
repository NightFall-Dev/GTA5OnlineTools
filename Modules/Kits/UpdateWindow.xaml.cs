using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Common.Utils;

using Downloader;

namespace GTA5OnlineTools.Modules.Kits;

/// <summary>
/// UpdateWindow.xaml 的交互逻辑
/// </summary>
public partial class UpdateWindow : Window
{
    private readonly DownloadService downloader = new();

    public UpdateWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 更新窗口加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Update_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (CoreUtil.ServerVersionInfo != CoreUtil.ClientVersionInfo)
                AudioUtil.SP_GTA5_Job.Play();

            if (GlobalData.ServerData != null)
            {
                foreach (var item in GlobalData.ServerData.Download)
                {
                    ListBox_DownloadAddress.Items.Add(item.Name);
                }
                ListBox_DownloadAddress.SelectedIndex = 0;
            }

            File.Delete(FileUtil.GetCurrFullPath("未下载完的更新.exe"));
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 更新窗口关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Update_Closing(object sender, CancelEventArgs e)
    {
        downloader.CancelAsync();
        downloader.Clear();
        downloader.Dispose();
    }

    /// <summary>
    /// 超链接请求导航事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }

    /// <summary>
    /// 开始更新按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Update_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Button_Update.IsEnabled = false;
        Button_CancelUpdate.IsEnabled = true;

        TextBlock_Info.Text = "Download starts";
        TextBlock_Percentage.Text = "0KB / 0MB";

        int index = ListBox_DownloadAddress.SelectedIndex;
        if (index != -1)
        {
            CoreUtil.UpdateAddress = GlobalData.ServerData.Download[index].Url;
        }
        else
        {
            CoreUtil.UpdateAddress = "https://github.com/CrazyZhang666/GTA5OnlineTools/releases/download/update/GTA5onlineTools.exe";
        }

        // 下载临时文件完整路径
        string OldPath = FileUtil.GetCurrFullPath(CoreUtil.HalfwayAppName);

        downloader.DownloadProgressChanged += DownloadProgressChanged;
        downloader.DownloadFileCompleted += DownloadFileCompleted;

        downloader.DownloadFileTaskAsync(CoreUtil.UpdateAddress, OldPath);
    }

    /// <summary>
    /// 取消更新按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_CancelUpdate_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        downloader.CancelAsync();
        downloader.Clear();

        Button_Update.IsEnabled = true;
        Button_CancelUpdate.IsEnabled = false;

        ProgressBar_Update.Minimum = 0;
        ProgressBar_Update.Maximum = 1024;
        ProgressBar_Update.Value = 0;

        TaskbarItemInfo.ProgressValue = 0;

        TextBlock_Info.Text = "Download canceled";
        TextBlock_Percentage.Text = "0KB / 0MB";
    }

    /// <summary>
    /// 下载进度变更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DownloadProgressChanged(object sender, Downloader.DownloadProgressChangedEventArgs e)
    {
        this.Dispatcher.BeginInvoke(new Action(delegate
        {
            ProgressBar_Update.Minimum = 0;
            ProgressBar_Update.Maximum = e.TotalBytesToReceive;
            ProgressBar_Update.Value = e.ReceivedBytesSize;

            TextBlock_Info.Text = $"Download File Size {e.TotalBytesToReceive / 1024.0f / 1024:0.0}MB";

            TextBlock_Percentage.Text = $"{LongToString(e.ReceivedBytesSize)}/{LongToString(e.TotalBytesToReceive)}";

            TaskbarItemInfo.ProgressValue = ProgressBar_Update.Value / ProgressBar_Update.Maximum;
        }));
    }

    /// <summary>
    /// 下载文件完成事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        Dispatcher.BeginInvoke(new Action(delegate
        {
            if (e.Error != null)
            {
                ProgressBar_Update.Minimum = 0;
                ProgressBar_Update.Maximum = 1024;
                ProgressBar_Update.Value = 0;

                TaskbarItemInfo.ProgressValue = 0;

                TextBlock_Info.Text = $"Download Failed {e.Error.Message}";
                TextBlock_Percentage.Text = "0KB / 0MB";
            }
            else
            {
                try
                {
                    AudioUtil.SP_DownloadOK.Play();

                    // 下载临时文件完整路径
                    string OldPath = FileUtil.GetCurrFullPath(CoreUtil.HalfwayAppName);
                    // 下载完成后文件真正路径
                    string NewPath = FileUtil.GetCurrFullPath(CoreUtil.FinalAppName());
                    // 下载完成后新文件重命名
                    FileUtil.FileReName(OldPath, NewPath);

                    Thread.Sleep(100);

                    // 下载完成后旧文件重命名
                    string oldFileName = $"[Please delete the old version of the assistant manually] {Guid.NewGuid()}.exe";
                    // 旧版本小助手重命名
                    FileUtil.FileReName(FileUtil.Current_Path, FileUtil.GetCurrFullPath(oldFileName));

                    TextBlock_Info.Text = "Update download complete，The program will restart in 3 seconds";

                    App.AppMainMutex.Dispose();
                    Thread.Sleep(1000);
                    ProcessUtil.OpenLink(NewPath);
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MsgBoxUtil.Exception(ex);
                }
            }
        }));
    }

    /// <summary>
    /// 文件大小转换
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private string LongToString(long num)
    {
        float kb = num / 1024.0f;

        if (kb > 1024)
            return $"{kb / 1024:0.0}MB";
        else
            return $"{kb:0.0}KB";
    }
}
