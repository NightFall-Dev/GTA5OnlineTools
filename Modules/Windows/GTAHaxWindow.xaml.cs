using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// GTAHaxWindow.xaml 的交互逻辑
/// </summary>
public partial class GTAHaxWindow : Window
{
    private const string GTAHax_MP = "$MPx";

    public GTAHaxWindow()
    {
        InitializeComponent();
    }

    private void Window_GTAHax_Loaded(object sender, RoutedEventArgs e)
    {
        TextBox_GTAHaxCodePreview.Text = "INT32\r\n";

        // STAT列表
        foreach (var item in StatData.StatDataClass)
        {
            ListBox_GTAHaxCode_ClassList.Items.Add(item.ClassName);
        }
        ListBox_GTAHaxCode_ClassList.SelectedIndex = 0;
    }

    private void Window_GTAHax_Closing(object sender, CancelEventArgs e)
    {

    }

    private void Button_WriteStat_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            File.WriteAllText(FileUtil.GTAHaxStat_Path, string.Empty);

            using (var sw = new StreamWriter(FileUtil.GTAHaxStat_Path, true))
            {
                sw.Write(TextBox_GTAHaxCodePreview.Text);

                MsgBoxUtil.Information("写入到stat.txt文件成功，现在可以打开GTAHax导入代码执行了");
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    private void Button_ImportStat_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (!ProcessUtil.IsAppRun("GTAHax"))
            ProcessUtil.OpenProcess("GTAHax", false);

        Task.Run(() =>
        {
            bool isRun = false;
            do
            {
                if (ProcessUtil.IsAppRun("GTAHax"))
                {
                    isRun = true;

                    var pGTAHax = Process.GetProcessesByName("GTAHax").ToList()[0];

                    bool isShow = false;
                    do
                    {
                        IntPtr Menu_handle = pGTAHax.MainWindowHandle;
                        IntPtr child_handle = WinAPI.FindWindowEx(Menu_handle, IntPtr.Zero, "Static", null);
                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Static", null);
                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Static", null);
                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Static", null);

                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Edit", null);
                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Edit", null);

                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Button", null);
                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Button", null);

                        child_handle = WinAPI.FindWindowEx(Menu_handle, child_handle, "Button", null);

                        if (child_handle != IntPtr.Zero)
                        {
                            isShow = true;

                            WinAPI.SendMessage(child_handle, WinAPI.WM_LBUTTONDOWN, IntPtr.Zero, null);
                            WinAPI.SendMessage(child_handle, WinAPI.WM_LBUTTONUP, IntPtr.Zero, null);

                            MsgBoxUtil.Information("导入到GTAHax成功！代码正在执行，请返回GTAHax和GTA5游戏查看\n\n如果未执行，请重新点击\"导入GTAHax\"\n如果执行成功游戏内会出现大受好评奖章");
                        }
                        else
                        {
                            isShow = false;
                        }

                        Task.Delay(100).Wait();
                    } while (!isShow);
                }
                else
                {
                    isRun = false;
                }

                Task.Delay(100).Wait();
            } while (!isRun);
        });
    }

    private void TextBox_AppendText_MP(string str, string value)
    {
        TextBox_GTAHaxCodePreview.AppendText($"\r\n{GTAHax_MP}{str}");
        TextBox_GTAHaxCodePreview.AppendText($"\r\n{value}");
    }

    private void TextBox_AppendText_NoMP(string str, string value)
    {
        TextBox_GTAHaxCodePreview.AppendText($"\r\n${str}");
        TextBox_GTAHaxCodePreview.AppendText($"\r\n{value}");
    }

    private string SelectedItemContent(ListBox listBox)
    {
        return (listBox.SelectedItem as ListBoxItem).Content.ToString();
    }

    private void ListBox_GTAHaxCode_ClassList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var statClassName = ListBox_GTAHaxCode_ClassList.SelectedItem.ToString();
        int index = StatData.StatDataClass.FindIndex(t => t.ClassName == statClassName);
        if (index != -1)
        {
            TextBox_GTAHaxCodePreview.Clear();
            TextBox_GTAHaxCodePreview.AppendText("INT32");

            for (int i = 0; i < StatData.StatDataClass[index].StatInfo.Count; i++)
            {
                var hash = StatData.StatDataClass[index].StatInfo[i].Hash;
                var value = StatData.StatDataClass[index].StatInfo[i].Value;

                if (hash.IndexOf("_") == 0)
                {
                    TextBox_AppendText_MP(hash, value.ToString());
                }
                else
                {
                    TextBox_AppendText_NoMP(hash, value.ToString());
                }
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////

    private void Button_CreateHaxCode_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        TextBox_GTAHaxCodePreview.Clear();
        TextBox_GTAHaxCodePreview.AppendText("INT32");

        if (TabControl_Main.SelectedIndex == 1)
        {
            if (CheckBox_H3_COMPLETEDPOSIX.IsChecked == true)
            {
                TextBox_AppendText_MP("_H3_COMPLETEDPOSIX", "-1");
            }

            if (CheckBox_Reset_P1P2.IsChecked == true)
            {
                TextBox_AppendText_MP("_H3OPT_BITSET1", "0");
                TextBox_AppendText_MP("_H3OPT_BITSET0", "0");
                TextBox_AppendText_MP("_H3OPT_POI", "0");
                TextBox_AppendText_MP("_H3OPT_ACCESSPOINTS", "0");
                TextBox_AppendText_MP("_CAS_HEIST_FLOW", "0");
            }

            if (RadioButton_H3_P1.IsChecked == true)
            {
                if (CheckBox_H3OPT_BITSET1.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_BITSET1", "0");
                }

                if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "不选择抢劫方式")
                {

                }
                else if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "隐迹潜踪")
                {
                    TextBox_AppendText_MP("_H3OPT_APPROACH", "1");
                }
                else if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "兵不厌诈")
                {
                    TextBox_AppendText_MP("_H3OPT_APPROACH", "2");
                }
                else if (SelectedItemContent(ListBox_H3OPT_APPROACH) == "气势汹汹")
                {
                    TextBox_AppendText_MP("_H3OPT_APPROACH", "3");
                }

                if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "不选择抢劫物品")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "现金")
                {
                    TextBox_AppendText_MP("_H3OPT_TARGET", "0");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "黄金")
                {
                    TextBox_AppendText_MP("_H3OPT_TARGET", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "艺术品")
                {
                    TextBox_AppendText_MP("_H3OPT_TARGET", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_TARGET) == "钻石")
                {
                    TextBox_AppendText_MP("_H3OPT_TARGET", "3");
                }

                if (CheckBox_H3OPT_ACCESSPOINTS.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_ACCESSPOINTS", "-1");
                }

                if (CheckBox_H3OPT_POI.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_POI", "-1");
                }

                if (CheckBox_H3OPT_BITSET1_1.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_BITSET1", "-1");
                }
            }
            else if (RadioButton_H3_P2.IsChecked == true)
            {
                if (CheckBox_H3OPT_BITSET0.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_BITSET0", "0");
                }

                //////////////////////////////////////

                if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "不选择逃亡载具")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型一")
                {
                    TextBox_AppendText_MP("_H3OPT_VEHS", "0");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型二")
                {
                    TextBox_AppendText_MP("_H3OPT_VEHS", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型三")
                {
                    TextBox_AppendText_MP("_H3OPT_VEHS", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_VEH) == "载具类型四")
                {
                    TextBox_AppendText_MP("_H3OPT_VEHS", "3");
                }

                //////////////////////////////////////

                if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "不选择武器类型")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型一")
                {
                    TextBox_AppendText_MP("_H3OPT_WEAPS", "0");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型二")
                {
                    TextBox_AppendText_MP("_H3OPT_WEAPS", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型三")
                {
                    TextBox_AppendText_MP("_H3OPT_WEAPS", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型四")
                {
                    TextBox_AppendText_MP("_H3OPT_WEAPS", "3");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_WEAPS) == "武器类型五")
                {
                    TextBox_AppendText_MP("_H3OPT_WEAPS", "4");
                }

                //////////////////////////////////////

                if (CheckBox_H3OPT_DISRUPTSHIP.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_DISRUPTSHIP", "3");
                }

                if (CheckBox_H3OPT_KEYLEVELS.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_KEYLEVELS", "2");
                }

                //////////////////////////////////////

                if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "不选择枪手")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "卡尔·阿不拉季（5％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWWEAP", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "古斯塔沃·莫塔（9％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWWEAP", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "查理·里德（7％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWWEAP", "3");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "切斯特·麦考伊（10％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWWEAP", "4");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "帕里克·麦克瑞利（8％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWWEAP", "5");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWWEAP) == "枪手零分红")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWWEAP", "6");
                }

                //////////////////////////////////////

                if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "不选择车手")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "卡里姆·登茨（5％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "塔丽娜·马丁内斯（7％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "淘艾迪（9％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "3");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "扎克·尼尔森（6％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "4");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "切斯特·麦考伊（10％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "5");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWDRIVER) == "车手零分红")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWDRIVER", "6");
                }

                //////////////////////////////////////

                if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "不选择黑客")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "里奇·卢肯斯（3％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWHACKER", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "克里斯汀·费尔兹（7％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWHACKER", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "尤汗·布莱尔（5％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWHACKER", "3");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "阿维·施瓦茨曼（10％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWHACKER", "4");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "佩奇·哈里斯（9％分红）")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWHACKER", "5");
                }
                else if (SelectedItemContent(ListBoxItem_H3OPT_CREWHACKER) == "黑客零分红")
                {
                    TextBox_AppendText_MP("_H3OPT_CREWHACKER", "6");
                }

                //////////////////////////////////////

                if (CheckBox_H3OPT_BITSET0_0.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H3OPT_BITSET0", "-1");
                }
            }

            TextBox_GTAHaxCodePreview.AppendText("\n");
            //MessageBox.Show("生成Hax代码成功", " 提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else if (TabControl_Main.SelectedIndex == 2)
        {
            if (RadioButton_H4CNF_P1.IsChecked == true)
            {
                if (CheckBox_H4CNF_BS_GEN.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_BS_GEN", "131071");
                }
                if (CheckBox_H4CNF_BS_ENTR.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_BS_ENTR", "63");
                }
                if (CheckBox_H4CNF_BS_ABIL.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_BS_ABIL", "63");
                }
                if (CheckBox_H4CNF_APPROACH.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_APPROACH", "-1");
                }

                //////////////////////////

                if (SelectedItemContent(ListBoxItem_H4_PROGRESS) == "未选择")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H4_PROGRESS) == "普通模式")
                {
                    TextBox_AppendText_MP("_H4_PROGRESS", "126823");
                }
                else if (SelectedItemContent(ListBoxItem_H4_PROGRESS) == "困难模式")
                {
                    TextBox_AppendText_MP("_H4_PROGRESS", "131055");
                }

                //////////////////////////

                if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "未选择")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "西西米托龙舌兰")
                {
                    TextBox_AppendText_MP("_H4CNF_TARGET", "0");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "红宝石项链")
                {
                    TextBox_AppendText_MP("_H4CNF_TARGET", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "不记名债券")
                {
                    TextBox_AppendText_MP("_H4CNF_TARGET", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "粉钻")
                {
                    TextBox_AppendText_MP("_H4CNF_TARGET", "3");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "玛德拉索文件")
                {
                    TextBox_AppendText_MP("_H4CNF_TARGET", "4");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TARGET) == "猎豹雕像")
                {
                    TextBox_AppendText_MP("_H4CNF_TARGET", "5");
                }

                //////////////////////////

                if (SelectedItemContent(ListBoxItem_H4LOOT) == "未选择")
                {

                }

                /**************************** 现金 ****************************/
                if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 现金（室内/室外）")
                {
                    TextBox_AppendText_MP("_H4LOOT_CASH_I", "-1");
                    TextBox_AppendText_MP("_H4LOOT_CASH_C", "-1");
                    TextBox_AppendText_MP("_H4LOOT_CASH_I_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_CASH_C_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_CASH_V", "90000");
                }
                else
                {
                    if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                    {
                        TextBox_AppendText_MP("_H4LOOT_CASH_I", "0");
                        TextBox_AppendText_MP("_H4LOOT_CASH_C", "0");
                        TextBox_AppendText_MP("_H4LOOT_CASH_I_SCOPED", "0");
                        TextBox_AppendText_MP("_H4LOOT_CASH_C_SCOPED", "0");
                        //TextBox_AppendText_MP("_H4LOOT_CASH_V", "0");
                    }
                }

                /**************************** 大麻 ****************************/
                if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 大麻（室内/室外）")
                {
                    TextBox_AppendText_MP("_H4LOOT_WEED_I", "-1");
                    TextBox_AppendText_MP("_H4LOOT_WEED_C", "-1");
                    TextBox_AppendText_MP("_H4LOOT_WEED_I_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_WEED_C_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_WEED_V", "145000");
                }
                else
                {
                    if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                    {
                        TextBox_AppendText_MP("_H4LOOT_WEED_I", "0");
                        TextBox_AppendText_MP("_H4LOOT_WEED_C", "0");
                        TextBox_AppendText_MP("_H4LOOT_WEED_I_SCOPED", "0");
                        TextBox_AppendText_MP("_H4LOOT_WEED_C_SCOPED", "0");
                        //TextBox_AppendText_MP("_H4LOOT_WEED_V", "0");
                    }
                }

                /**************************** 可可 ****************************/
                if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 可可（室内/室外）")
                {
                    TextBox_AppendText_MP("_H4LOOT_COKE_I", "-1");
                    TextBox_AppendText_MP("_H4LOOT_COKE_C", "-1");
                    TextBox_AppendText_MP("_H4LOOT_COKE_I_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_COKE_C_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_COKE_V", "220000");
                }
                else
                {
                    if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                    {
                        TextBox_AppendText_MP("_H4LOOT_COKE_I", "0");
                        TextBox_AppendText_MP("_H4LOOT_COKE_C", "0");
                        TextBox_AppendText_MP("_H4LOOT_COKE_I_SCOPED", "0");
                        TextBox_AppendText_MP("_H4LOOT_COKE_C_SCOPED", "0");
                        //TextBox_AppendText_MP("_H4LOOT_COKE_V", "0");
                    }
                }

                /**************************** 黄金 ****************************/
                if (SelectedItemContent(ListBoxItem_H4LOOT) == "已发现/侦察 黄金（室内/室外）")
                {
                    TextBox_AppendText_MP("_H4LOOT_GOLD_I", "-1");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_C", "-1");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_I_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_C_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_V", "320000");
                }
                else
                {
                    if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                    {
                        TextBox_AppendText_MP("_H4LOOT_GOLD_I", "0");
                        TextBox_AppendText_MP("_H4LOOT_GOLD_C", "0");
                        TextBox_AppendText_MP("_H4LOOT_GOLD_I_SCOPED", "0");
                        TextBox_AppendText_MP("_H4LOOT_GOLD_C_SCOPED", "0");
                        //TextBox_AppendText_MP("_H4LOOT_GOLD_V", "0");
                    }
                }

                //////////////////////////

                /**************************** 画作 ****************************/
                if (SelectedItemContent(ListBoxItem_H4LOOT_PAINT) == "已发现/侦察 画作（室内/室外）")
                {
                    TextBox_AppendText_MP("_H4LOOT_PAINT", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "-1");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_V", "180000");
                }
                else
                {
                    if (CheckBox_H4LOOT_Random.IsChecked == false && Expander_H4LOOT.IsExpanded == true)
                    {
                        TextBox_AppendText_MP("_H4LOOT_PAINT", "0");
                        TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "0");
                        //TextBox_AppendText_MP("_H4LOOT_PAINT_V", "0");
                    }
                }

                //////////////////////////

                if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "未选择")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "潜水艇：虎鲸")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "65283");
                }
                else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "飞机：阿尔科诺斯特")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "65413");
                }
                else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "飞机：梅杜莎")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "65289");
                }
                else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "直升机：隐形歼灭者")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "65425");
                }
                else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "船只：巡逻艇")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "65313");
                }
                else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "船只：长鳍")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "65345");
                }
                else if (SelectedItemContent(ListBoxItem_H4_MISSIONS) == "全部载具可用")
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "65535");
                }

                //////////////////////////

                if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "未选择")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "侵略者（连发散弹，连发手枪，手雷，砍刀）")
                {
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "阴谋者（军用步枪，单发手枪，粘弹，指虎）")
                {
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "神枪手（轻狙，连发手枪，燃烧瓶，小刀）")
                {
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "3");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "破坏者（MK2冲锋枪，单发手枪，土质炸弹，小刀）")
                {
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "4");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_WEAPONS) == "神射手（MK2突击步枪，单发手枪，土质炸弹，砍刀）")
                {
                    TextBox_AppendText_MP("_H4CNF_WEAPONS", "5");
                }

                //////////////////////////

                if (CheckBox_H4CNF_WEP_DISRP.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_WEP_DISRP", "3");
                }
                if (CheckBox_H4CNF_ARM_DISRP.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_ARM_DISRP", "3");
                }
                if (CheckBox_H4CNF_HEL_DISRP.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_HEL_DISRP", "3");
                }

                //////////////////////////

                if (CheckBox_H4CNF_GRAPPEL.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_GRAPPEL", "-1");
                }
                if (CheckBox_H4CNF_UNIFORM.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_UNIFORM", "-1");
                }
                if (CheckBox_H4CNF_BOLTCUT.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4CNF_BOLTCUT", "-1");
                }

                //////////////////////////

                if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "未选择")
                {

                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "机场")
                {
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "1");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "北船坞")
                {
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "2");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "主码头-东")
                {
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "3");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "主码头-西")
                {
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "4");
                }
                else if (SelectedItemContent(ListBoxItem_H4CNF_TROJAN) == "混合粉")
                {
                    TextBox_AppendText_MP("_H4CNF_TROJAN", "5");
                }

                //////////////////////////

                if (CheckBox_H4_PLAYTHROUGH_STATUS.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "10");
                }
            }
            else if (RadioButton_H4CNF_P2.IsChecked == true)
            {
                if (CheckBox_ResetEverything.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4_MISSIONS", "0");
                    TextBox_AppendText_MP("_H4_PROGRESS", "0");
                    TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "0");
                    TextBox_AppendText_MP("_H4CNF_APPROACH", "0");
                    TextBox_AppendText_MP("_H4CNF_BS_ENTR", "0");
                    TextBox_AppendText_MP("_H4CNF_BS_GEN", "0");
                }
                if (CheckBox_H4_COOLDOWN.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4_COOLDOWN", "0");
                }
                if (CheckBox_H4_COOLDOWN_HARD.IsChecked == true)
                {
                    TextBox_AppendText_MP("_H4_COOLDOWN_HARD", "0");
                }
            }

            TextBox_GTAHaxCodePreview.AppendText("\n");
        }
    }

    private void TabControl_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Button_CreateHaxCode == null)
            return;

        if (TabControl_Main.SelectedIndex == 0)
        {
            Button_CreateHaxCode.IsEnabled = false;
        }
        else
        {
            Button_CreateHaxCode.IsEnabled = true;
        }
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }

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

    private void CheckBox_H4CNF_P1_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_H4CNF_P1.IsChecked == true)
        {
            RadioButton_H4CNF_P2.IsChecked = false;
        }
        else
        {
            RadioButton_H4CNF_P2.IsChecked = true;
        }
    }

    private void CheckBox_H4CNF_P2_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_H4CNF_P2.IsChecked == true)
        {
            RadioButton_H4CNF_P1.IsChecked = false;
        }
        else
        {
            RadioButton_H4CNF_P1.IsChecked = true;
        }
    }

    private void RadioButton_H3_P1_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_H3_P1.IsChecked == true)
        {
            RadioButton_H3_P2.IsChecked = false;
        }
        else
        {
            RadioButton_H3_P2.IsChecked = true;
        }
    }

    private void RadioButton_H3_P2_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_H3_P2.IsChecked == true)
        {
            RadioButton_H3_P1.IsChecked = false;
        }
        else
        {
            RadioButton_H3_P1.IsChecked = true;
        }
    }
}
