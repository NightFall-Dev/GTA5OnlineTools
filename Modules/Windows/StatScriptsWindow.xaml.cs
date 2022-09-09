using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// StatScriptsWindow.xaml 的交互逻辑
/// </summary>
public partial class StatScriptsWindow : Window
{
    public StatScriptsWindow()
    {
        InitializeComponent();
    }

    private void Window_StatAutoScripts_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.GlobalPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
            Globals.GlobalPTR = Memory.Rip_37(Globals.GlobalPTR);

            this.Dispatcher.Invoke(() =>
            {

            });
        });

        // STAT列表
        foreach (var item in StatData.StatDataClass)
        {
            ListBox_STATList.Items.Add(item.ClassName);
        }
        ListBox_STATList.SelectedIndex = 0;
    }

    private void Window_StatAutoScripts_Closing(object sender, CancelEventArgs e)
    {

    }

    private void Button_LoadSession_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Online.LoadSession(11);
    }

    private void AppendTextBox(string str)
    {
        this.Dispatcher.Invoke(() =>
        {
            TextBox_Result.AppendText($"[{DateTime.Now:T}] {str}\r\n");
            TextBox_Result.ScrollToEnd();
        });
    }

    private void Button_ExecuteAutoScript_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_STATList.SelectedIndex;
        if (index != -1)
        {
            AutoScript(ListBox_STATList.SelectedItem.ToString());
        }
    }

    private void AutoScript(string statClassName)
    {
        TextBox_Result.Clear();

        Task.Run(() =>
        {
            try
            {
                int index = StatData.StatDataClass.FindIndex(t => t.ClassName == statClassName);
                if (index != -1)
                {
                    AppendTextBox($"is executing {StatData.StatDataClass[index].ClassName} script code");

                    for (int i = 0; i < StatData.StatDataClass[index].StatInfo.Count; i++)
                    {
                        AppendTextBox($"is executing 第 {i + 1}/{StatData.StatDataClass[index].StatInfo.Count} bar code");

                        Hacks.WriteStat(StatData.StatDataClass[index].StatInfo[i].Hash, StatData.StatDataClass[index].StatInfo[i].Value);
                        Task.Delay(500).Wait();
                    }

                    AppendTextBox($"{StatData.StatDataClass[index].ClassName} The script code is executed");
                }
            }
            catch (Exception ex)
            {
                AppendTextBox($"Error：{ex.Message}");
            }
        });
    }
}
