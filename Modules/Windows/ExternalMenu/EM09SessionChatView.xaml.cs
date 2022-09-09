using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Common.Http;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

using Chinese;
using Forms = System.Windows.Forms;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM09SessionChatView.xaml 的交互逻辑
/// </summary>
public partial class EM09SessionChatView : UserControl
{
    private readonly string youdaoAPI = "http://fanyi.youdao.com/translate?&doctype=json&type=AUTO&i=";

    public EM09SessionChatView()
    {
        InitializeComponent();

        TextBox_InputMessage.Text = "测试文本: 请把游戏中聊天输入法调成英文,否则会漏掉文字.Hello1234,漏掉文字了吗?";

        ReadPlayerName();

        ExternalMenuWindow.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {
        
    }

    private void Button_Translate_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            if (TextBox_InputMessage.Text != "")
            {
                var str = (e.OriginalSource as Button).Content.ToString();

                switch (str)
                {
                    case "中英互译":
                        Translation();
                        break;
                    case "简转繁":
                        TextBox_InputMessage.Text = ChineseConverter.ToTraditional(TextBox_InputMessage.Text);
                        break;
                    case "繁转简":
                        TextBox_InputMessage.Text = ChineseConverter.ToSimplified(TextBox_InputMessage.Text);
                        break;
                    case "转拼音":
                        TextBox_InputMessage.Text = Pinyin.GetString(TextBox_InputMessage.Text, PinyinFormat.WithoutTone);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void Button_SendTextToGTA5_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            if (TextBox_InputMessage.Text != "")
            {
                TextBox_InputMessage.Text = ToDBC(TextBox_InputMessage.Text);

                Memory.SetForegroundWindow();

                SendMessageToGTA5(TextBox_InputMessage.Text);
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void KeyPress(WinVK winVK)
    {
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 0, 0);
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 2, 0);
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
    }

    private void SendMessageToGTA5(string str)
    {
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));

        KeyPress(WinVK.RETURN);

        if (RadioButton_PressKeyT.IsChecked == true)
            KeyPress(WinVK.T);
        else
            KeyPress(WinVK.Y);

        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));
        Forms.SendKeys.Flush();
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        Forms.SendKeys.SendWait(str);
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        Forms.SendKeys.Flush();
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));

        KeyPress(WinVK.RETURN);
        KeyPress(WinVK.RETURN);
    }

    private async void Translation()
    {
        try
        {
            var stringBuilder = new StringBuilder();

            string str = await HttpHelper.HttpClientGET(youdaoAPI + TextBox_InputMessage.Text);
            var rb = JsonUtil.JsonDese<ReceiveObj>(str);

            foreach (var item in rb.translateResult)
            {
                foreach (var t in item)
                {
                    stringBuilder.Append(t.tgt);
                }
            }

            TextBox_InputMessage.Text = stringBuilder.ToString();
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void TextBox_InputMessage_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;
            Button_SendTextToGTA5_Click(null, null);
        }
    }

    private string ToDBC(string input)
    {
        char[] c = input.ToCharArray();

        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }

            if (c[i] > 65280 && c[i] < 65375)
            {
                c[i] = (char)(c[i] - 65248);
            }
        }

        return new string(c);
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }

    private void Button_ReadPlayerName_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        ReadPlayerName();
    }

    private void Button_WritePlayerName_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (TextBox_OnlineList.Text != "" &&
            TextBox_ChatName.Text != "" &&
            TextBox_ExternalDisplay.Text != "")
        {
            Memory.WriteString(Globals.WorldPTR, Offsets.OnlineListPlayerName, TextBox_OnlineList.Text + "\0");
            Memory.WriteString(Globals.PlayerChatterNamePTR + 0xBC, null, TextBox_ChatName.Text + "\0");
            Memory.WriteString(Globals.PlayerExternalDisplayNamePTR + 0x84, null, TextBox_ExternalDisplay.Text + "\0");

            MsgBoxUtil.Information("写入成功，请切换战局生效");
        }
        else
        {
            MsgBoxUtil.Warning("内容不能为空");
        }
    }

    private void ReadPlayerName()
    {
        TextBox_OnlineList.Text = Memory.ReadString(Globals.WorldPTR, Offsets.OnlineListPlayerName, 20);
        TextBox_ChatName.Text = Memory.ReadString(Globals.PlayerChatterNamePTR + 0xBC, null, 20);
        TextBox_ExternalDisplay.Text = Memory.ReadString(Globals.PlayerExternalDisplayNamePTR + 0x84, null, 20);
    }

    private void TextBox_OnlineList_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox_ChatName.Text = TextBox_OnlineList.Text;
        TextBox_ExternalDisplay.Text = TextBox_OnlineList.Text;
    }
}

public class ReceiveObj
{
    public string type { get; set; }
    public int errorCode { get; set; }
    public int elapsedTime { get; set; }
    public List<List<TranslateResultItemItem>> translateResult { get; set; }
}

public class TranslateResultItemItem
{
    public string src { get; set; }
    public string tgt { get; set; }
}
