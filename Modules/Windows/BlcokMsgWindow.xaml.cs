using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// BlcokMsgWindow.xaml 的交互逻辑
/// </summary>
public partial class BlcokMsgWindow : Window
{
    public BlcokMsgWindow()
    {
        InitializeComponent();
    }

    private void Window_BlcokMsg_Loaded(object sender, RoutedEventArgs e)
    {
        if (File.Exists(FileUtil.BlockWords_Path))
        {
            try
            {
                var file = new StreamReader(FileUtil.BlockWords_Path, Encoding.Default);
                string content = string.Empty;
                while (content != null)
                {
                    content = file.ReadLine();
                    if (!string.IsNullOrEmpty(content))
                        ListBox_BlcokWords.Items.Add(content);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                MsgBoxUtil.Exception(ex);
            }
        }
        else
        {
            DefaultBlcokWords();
        }
    }

    private void Window_BlcokMsg_Closing(object sender, CancelEventArgs e)
    {
        SaveBlcokWords();
    }

    private void Button_AddBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var txt = TextBox_InputBlcokWord.Text;
        if (!string.IsNullOrEmpty(txt))
        {
            foreach (string item in ListBox_BlcokWords.Items)
            {
                if (item.Equals(txt))
                {
                    MsgBoxUtil.Information($"The keyword {txt} has already been added, please do not add it again");
                    return;
                }
            }

            ListBox_BlcokWords.Items.Add(txt);
            ListBox_BlcokWords.SelectedIndex = ListBox_BlcokWords.Items.Count - 1;
            TextBox_InputBlcokWord.Clear();
        }
    }

    private void Button_RemoveBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_BlcokWords.SelectedIndex;
        if (index != -1)
        {
            ListBox_BlcokWords.Items.RemoveAt(index);
            ListBox_BlcokWords.SelectedIndex = ListBox_BlcokWords.Items.Count - 1;
        }
    }

    private void Button_InjectGameProcess_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        SaveBlcokWords();

        var InjectInfo = new InjectInfo();
        InjectInfo.DLLPath = FileUtil.Inject_Path + "BlcokMsg.dll";

        if (string.IsNullOrEmpty(InjectInfo.DLLPath))
        {
            MsgBoxUtil.Warning("An exception occurred, DLL path is empty");
            return;
        }

        var process = Process.GetProcessesByName("GTA5")[0];
        InjectInfo.PID = process.Id;
        InjectInfo.PName = process.ProcessName;
        InjectInfo.MWindowHandle = process.MainWindowHandle;

        foreach (ProcessModule module in Process.GetProcessById(InjectInfo.PID).Modules)
        {
            if (module.FileName == InjectInfo.DLLPath)
            {
                MsgBoxUtil.Warning("The DLL has already been injected, please do not re-inject");
                return;
            }
        }

        try
        {
            BaseInjector.SetForegroundWindow(InjectInfo.MWindowHandle);
            BaseInjector.DLLInjector(InjectInfo.PID, InjectInfo.DLLPath);
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void Button_SaveBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        SaveBlcokWords();
    }

    private void SaveBlcokWords()
    {
        try
        {
            using var fs = new FileStream(FileUtil.BlockWords_Path, FileMode.Create);
            using var sw = new StreamWriter(fs, Encoding.Default);
            for (int i = 0; i < ListBox_BlcokWords.Items.Count; i++)
            {
                sw.WriteLine(ListBox_BlcokWords.Items[i].ToString());
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void Button_DefaultBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        DefaultBlcokWords();
    }

    private void DefaultBlcokWords()
    {
        ListBox_BlcokWords.Items.Clear();

        ListBox_BlcokWords.Items.Add("网站");
        ListBox_BlcokWords.Items.Add("网址");
        ListBox_BlcokWords.Items.Add("www");
        ListBox_BlcokWords.Items.Add("com");
        ListBox_BlcokWords.Items.Add("top");
        ListBox_BlcokWords.Items.Add("xyz");
        ListBox_BlcokWords.Items.Add("cn");

        ListBox_BlcokWords.Items.Add("微信");
        ListBox_BlcokWords.Items.Add("vx");
        ListBox_BlcokWords.Items.Add("扣扣");
        ListBox_BlcokWords.Items.Add("qq");
        ListBox_BlcokWords.Items.Add("q群");
        ListBox_BlcokWords.Items.Add("加群");

        ListBox_BlcokWords.Items.Add("外挂");
        ListBox_BlcokWords.Items.Add("价格");

        ListBox_BlcokWords.Items.Add("毛");
        ListBox_BlcokWords.Items.Add("元");
        ListBox_BlcokWords.Items.Add("块");
        ListBox_BlcokWords.Items.Add("一亿");

        ListBox_BlcokWords.Items.Add("刷金");
        ListBox_BlcokWords.Items.Add("淘宝");
        ListBox_BlcokWords.Items.Add("代刷");
        ListBox_BlcokWords.Items.Add("辅助");
        ListBox_BlcokWords.Items.Add("一亿");
        ListBox_BlcokWords.Items.Add("手工");
        ListBox_BlcokWords.Items.Add("解封");
        ListBox_BlcokWords.Items.Add("下单");
        ListBox_BlcokWords.Items.Add("充值");
        ListBox_BlcokWords.Items.Add("科技");
        ListBox_BlcokWords.Items.Add("不要挂");
        ListBox_BlcokWords.Items.Add("恶搞");
        ListBox_BlcokWords.Items.Add("全网最低");
        ListBox_BlcokWords.Items.Add("地堡解锁");
        ListBox_BlcokWords.Items.Add("恶搞");
        ListBox_BlcokWords.Items.Add("自瞄");
        ListBox_BlcokWords.Items.Add("福利");
        ListBox_BlcokWords.Items.Add("保底");
        ListBox_BlcokWords.Items.Add("妹妹");
        ListBox_BlcokWords.Items.Add("妹子");

        ListBox_BlcokWords.Items.Add("上岛");
        ListBox_BlcokWords.Items.Add("免费带岛");
        ListBox_BlcokWords.Items.Add("萌新");
        ListBox_BlcokWords.Items.Add("加我送");
        ListBox_BlcokWords.Items.Add("挂狗");
        ListBox_BlcokWords.Items.Add("百分百");
        ListBox_BlcokWords.Items.Add("拍照保留");
        ListBox_BlcokWords.Items.Add("截图");
        ListBox_BlcokWords.Items.Add("开挂勿进");
        ListBox_BlcokWords.Items.Add("限时限量");

        ListBox_BlcokWords.Items.Add("gta");
        ListBox_BlcokWords.Items.Add("vip");

        ListBox_BlcokWords.Items.Add("单场百万");
        ListBox_BlcokWords.Items.Add("另售");
        ListBox_BlcokWords.Items.Add("有抽奖");

        ListBox_BlcokWords.Items.Add("麻豆");
        ListBox_BlcokWords.Items.Add("传媒");
        ListBox_BlcokWords.Items.Add("蜜桃星空");
        ListBox_BlcokWords.Items.Add("av");
        ListBox_BlcokWords.Items.Add("欧美");
        ListBox_BlcokWords.Items.Add("荷官");
        ListBox_BlcokWords.Items.Add("在线观看");
        ListBox_BlcokWords.Items.Add("处男");
        ListBox_BlcokWords.Items.Add("幼女");
        ListBox_BlcokWords.Items.Add("自慰");
        ListBox_BlcokWords.Items.Add("挂逼");
        ListBox_BlcokWords.Items.Add("强奸");
        ListBox_BlcokWords.Items.Add("人妻");
        ListBox_BlcokWords.Items.Add("日本");
        ListBox_BlcokWords.Items.Add("性爱");
        ListBox_BlcokWords.Items.Add("处女");
        ListBox_BlcokWords.Items.Add("乱伦");
        ListBox_BlcokWords.Items.Add("小电影");

        ListBox_BlcokWords.Items.Add("不过百");
        ListBox_BlcokWords.Items.Add("加入我们");
        ListBox_BlcokWords.Items.Add("修仙");
        ListBox_BlcokWords.Items.Add("加我免费");

    }
}
