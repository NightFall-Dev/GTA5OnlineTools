using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// BigBaseV2Window.xaml 的交互逻辑
/// </summary>
public partial class BigBaseV2Window : Window
{
    private InjectInfo InjectInfo { get; set; } = new();

    public BigBaseV2Window()
    {
        InitializeComponent();
    }

    private void Window_BigBaseV2_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            InjectInfo.DLLPath = FileUtil.Cache_Path + "PackedStatEditor.dll";

            var process = Process.GetProcessesByName("GTA5")[0];
            InjectInfo.PID = process.Id;
            InjectInfo.PName = process.ProcessName;
            InjectInfo.MWindowHandle = process.MainWindowHandle;
        });

        AppendTextBox("waiting for user action...");
    }

    private void Window_BigBaseV2_Closing(object sender, CancelEventArgs e)
    {

    }

    private void AppendTextBox(string str)
    {
        TextBox_Log.AppendText($"[{DateTime.Now:T}] {str}\r\n");
        TextBox_Log.ScrollToEnd();
    }

    private void RadioButton_InjectDLL_Bread_Click(object sender, RoutedEventArgs e)
    {
        //if (RadioButton_InjectDLL_Bread.IsChecked == true)
        //{
        //    InjectInfo.DLLPath = FileUtil.Cache_Path + "Bread.dll";
        //    Button_BigBaseV2_Inject.IsEnabled = false;
        //}
    }

    private void Button_BigBaseV2_Inject_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (InjectInfo.PID == 0)
        {
            AppendTextBox("GTA5 process not found");
            return;
        }
        else if (string.IsNullOrEmpty(InjectInfo.DLLPath))
        {
            AppendTextBox("DLL path is empty");
            return;
        }

        foreach (ProcessModule module in Process.GetProcessById(InjectInfo.PID).Modules)
        {
            if (module.FileName == InjectInfo.DLLPath)
            {
                AppendTextBox("The DLL has been injected");
                return;
            }
        }

        try
        {
            BaseInjector.SetForegroundWindow(InjectInfo.MWindowHandle);

            BaseInjector.DLLInjector(InjectInfo.PID, InjectInfo.DLLPath);
            AppendTextBox($"DLL injection into process {InjectInfo.PName} success");
        }
        catch (Exception ex)
        {
            AppendTextBox($"发生错误：{ex.Message}");
        }
    }

    private void Button_Located_Stats_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            ProcessUtil.OpenLink(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/BigBaseV2/");
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }

    //////////////////////////////////////////////////////

    private bool IsInputNumber(KeyEventArgs e)
    {
        if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
           e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Tab)
        {
            // 按下了Alt、ctrl、shift等修饰键
            if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
            {
                e.Handled = true;
            }
            else
            {
                return true;
            }
        }
        else
        {
            // 按下了字符等其它功能键
            e.Handled = true;
        }
        return false;
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        IsInputNumber(e);
    }

    private void Button_Calculate_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            if (RadioButton_ToMS.IsChecked == true)
            {
                if (TextBox_d.Text != "" && TextBox_h.Text != "" && TextBox_m.Text != "" && TextBox_s.Text != "")
                {
                    TextBox_ms.Text = (
                    (Convert.ToInt64(TextBox_d.Text) * 86400 +
                    Convert.ToInt64(TextBox_h.Text) * 3600 +
                    Convert.ToInt64(TextBox_m.Text) * 60 +
                    Convert.ToInt64(TextBox_s.Text)) * 1000
                    ).ToString();
                }
            }
            else if (RadioButton_ToTIME.IsChecked == true)
            {
                if (TextBox_ms.Text != "")
                {
                    if (Convert.ToInt64(TextBox_ms.Text) >= 1000)
                    {
                        long t = Convert.ToInt64(TextBox_ms.Text) / 1000;

                        long day = 0, hour = 0, minute = 0, second = 0;
                        if (t >= 86400)         // 天
                        {
                            day = Convert.ToInt64(t / 86400);
                            hour = Convert.ToInt64((t % 86400) / 3600);
                            minute = Convert.ToInt64((t % 86400 % 3600) / 60);
                            second = Convert.ToInt64(t % 86400 % 3600 % 60);
                        }
                        else if (t >= 3600)     // 时
                        {
                            hour = Convert.ToInt64(t / 3600);
                            minute = Convert.ToInt64((t % 3600) / 60);
                            second = Convert.ToInt64(t % 3600 % 60);
                        }
                        else if (t >= 60)       // 分
                        {
                            minute = Convert.ToInt64(t / 60);
                            second = Convert.ToInt64(t % 60);
                        }
                        else
                        {
                            second = Convert.ToInt64(t);
                        }

                        TextBox_d.Text = day.ToString();
                        TextBox_h.Text = hour.ToString();
                        TextBox_m.Text = minute.ToString();
                        TextBox_s.Text = second.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }
}
