using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// HeistCutWindow.xaml 的交互逻辑
/// </summary>
public partial class HeistCutWindow : Window
{
    public HeistCutWindow()
    {
        InitializeComponent();
    }

    private void Window_HeistCut_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.GlobalPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
            Globals.GlobalPTR = Memory.Rip_37(Globals.GlobalPTR);

            this.Dispatcher.Invoke(() =>
            {
                ReadHeistCutData();
            });
        });
    }

    private void Window_HeistCut_Closing(object sender, CancelEventArgs e)
    {

    }

    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        ReadHeistCutData();
    }

    /// <summary>
    /// 读取抢劫任务相关数据
    /// </summary>
    private void ReadHeistCutData()
    {
        try
        {
            // 佩里克岛抢劫玩家分红比例
            TextBox_Cayo_Player1.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 1).ToString();
            TextBox_Cayo_Player2.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 2).ToString();
            TextBox_Cayo_Player3.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 3).ToString();
            TextBox_Cayo_Player4.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 4).ToString();

            TextBox_Cayo_Tequila.Text = Hacks.ReadGA<int>(262145 + 29970).ToString();
            TextBox_Cayo_RubyNecklace.Text = Hacks.ReadGA<int>(262145 + 29971).ToString();
            TextBox_Cayo_BearerBonds.Text = Hacks.ReadGA<int>(262145 + 29972).ToString();
            TextBox_Cayo_PinkDiamond.Text = Hacks.ReadGA<int>(262145 + 29973).ToString();
            TextBox_Cayo_MadrazoFiles.Text = Hacks.ReadGA<int>(262145 + 29974).ToString();
            TextBox_Cayo_BlackPanther.Text = Hacks.ReadGA<int>(262145 + 29975).ToString();

            TextBox_Cayo_LocalBagSize.Text = Hacks.ReadGA<int>(262145 + 29720).ToString();

            TextBox_Cayo_FencingFee.Text = Hacks.ReadGA<float>(262145 + 29979).ToString();
            TextBox_Cayo_PavelCut.Text = Hacks.ReadGA<float>(262145 + 29980).ToString();

            //////////////////////////////////////////////////////////////////////////////////

            // 赌场抢劫玩家分红比例
            TextBox_Casino_Player1.Text = Hacks.ReadGA<int>(1966534 + 1497 + 736 + 92 + 1).ToString();
            TextBox_Casino_Player2.Text = Hacks.ReadGA<int>(1966534 + 1497 + 736 + 92 + 2).ToString();
            TextBox_Casino_Player3.Text = Hacks.ReadGA<int>(1966534 + 1497 + 736 + 92 + 3).ToString();
            TextBox_Casino_Player4.Text = Hacks.ReadGA<int>(1966534 + 1497 + 736 + 92 + 4).ToString();

            TextBox_Casino_Lester.Text = Hacks.ReadGA<int>(262145 + 28779).ToString();

            TextBox_CasinoPotential_Money.Text = Hacks.ReadGA<int>(262145 + 28793).ToString();
            TextBox_CasinoPotential_Artwork.Text = Hacks.ReadGA<int>(262145 + 28794).ToString();
            TextBox_CasinoPotential_Gold.Text = Hacks.ReadGA<int>(262145 + 28795).ToString();
            TextBox_CasinoPotential_Diamonds.Text = Hacks.ReadGA<int>(262145 + 28796).ToString();

            TextBox_CasinoAI_1.Text = Hacks.ReadGA<int>(262145 + 28804 + 1).ToString();
            TextBox_CasinoAI_2.Text = Hacks.ReadGA<int>(262145 + 28804 + 2).ToString();
            TextBox_CasinoAI_3.Text = Hacks.ReadGA<int>(262145 + 28804 + 3).ToString();
            TextBox_CasinoAI_4.Text = Hacks.ReadGA<int>(262145 + 28804 + 4).ToString();
            TextBox_CasinoAI_5.Text = Hacks.ReadGA<int>(262145 + 28804 + 5).ToString();

            TextBox_CasinoAI_6.Text = Hacks.ReadGA<int>(262145 + 28804 + 6).ToString();
            TextBox_CasinoAI_7.Text = Hacks.ReadGA<int>(262145 + 28804 + 7).ToString();
            TextBox_CasinoAI_8.Text = Hacks.ReadGA<int>(262145 + 28804 + 8).ToString();
            TextBox_CasinoAI_9.Text = Hacks.ReadGA<int>(262145 + 28804 + 9).ToString();
            TextBox_CasinoAI_10.Text = Hacks.ReadGA<int>(262145 + 28804 + 10).ToString();

            TextBox_CasinoAI_11.Text = Hacks.ReadGA<int>(262145 + 28804 + 11).ToString();
            TextBox_CasinoAI_12.Text = Hacks.ReadGA<int>(262145 + 28804 + 12).ToString();
            TextBox_CasinoAI_13.Text = Hacks.ReadGA<int>(262145 + 28804 + 13).ToString();
            TextBox_CasinoAI_14.Text = Hacks.ReadGA<int>(262145 + 28804 + 14).ToString();
            TextBox_CasinoAI_15.Text = Hacks.ReadGA<int>(262145 + 28804 + 15).ToString();

            //////////////////////////////////////////////////////////////////////////////////

            // 末日抢劫玩家分红比例
            TextBox_Doomsday_Player1.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 1).ToString();
            TextBox_Doomsday_Player2.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 2).ToString();
            TextBox_Doomsday_Player3.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 3).ToString();
            TextBox_Doomsday_Player4.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 4).ToString();

            TextBox_Doomsday_ActI.Text = Hacks.ReadGA<int>(262145 + 9132).ToString();
            TextBox_Doomsday_ActII.Text = Hacks.ReadGA<int>(262145 + 9133).ToString();
            TextBox_Doomsday_ActIII.Text = Hacks.ReadGA<int>(262145 + 9134).ToString();

            //////////////////////////////////////////////////////////////////////////////////

            // 公寓抢劫玩家分红比例
            TextBox_Apart_Player1.Text = Hacks.ReadGA<int>(1933908 + 3008 + 1).ToString();
            TextBox_Apart_Player2.Text = Hacks.ReadGA<int>(1933908 + 3008 + 2).ToString();
            TextBox_Apart_Player3.Text = Hacks.ReadGA<int>(1933908 + 3008 + 3).ToString();
            TextBox_Apart_Player4.Text = Hacks.ReadGA<int>(1933908 + 3008 + 4).ToString();

            TextBox_Apart_Fleeca.Text = Hacks.ReadGA<int>(262145 + 9127).ToString();
            TextBox_Apart_PrisonBreak.Text = Hacks.ReadGA<int>(262145 + 9128).ToString();
            TextBox_Apart_HumaneLabs.Text = Hacks.ReadGA<int>(262145 + 9129).ToString();
            TextBox_Apart_SeriesA.Text = Hacks.ReadGA<int>(262145 + 9130).ToString();
            TextBox_Apart_PacificStandard.Text = Hacks.ReadGA<int>(262145 + 9131).ToString();

            TextBox_Result.Text = $"data read successfully";
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }

    /// <summary>
    /// 写入抢劫任务相关数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            if (TextBox_Cayo_Player1.Text != "" &&
                TextBox_Cayo_Player2.Text != "" &&
                TextBox_Cayo_Player3.Text != "" &&
                TextBox_Cayo_Player4.Text != "" &&

                TextBox_Cayo_Tequila.Text != "" &&
                TextBox_Cayo_RubyNecklace.Text != "" &&
                TextBox_Cayo_BearerBonds.Text != "" &&
                TextBox_Cayo_PinkDiamond.Text != "" &&
                TextBox_Cayo_MadrazoFiles.Text != "" &&
                TextBox_Cayo_BlackPanther.Text != "" &&

                TextBox_Cayo_LocalBagSize.Text != "" &&

                TextBox_Cayo_FencingFee.Text != "" &&
                TextBox_Cayo_PavelCut.Text != "" &&

                TextBox_Casino_Player1.Text != "" &&
                TextBox_Casino_Player2.Text != "" &&
                TextBox_Casino_Player3.Text != "" &&
                TextBox_Casino_Player4.Text != "" &&

                TextBox_Casino_Lester.Text != "" &&

                TextBox_CasinoPotential_Money.Text != "" &&
                TextBox_CasinoPotential_Artwork.Text != "" &&
                TextBox_CasinoPotential_Gold.Text != "" &&
                TextBox_CasinoPotential_Diamonds.Text != "" &&

                TextBox_CasinoAI_1.Text != "" &&
                TextBox_CasinoAI_2.Text != "" &&
                TextBox_CasinoAI_3.Text != "" &&
                TextBox_CasinoAI_4.Text != "" &&
                TextBox_CasinoAI_5.Text != "" &&

                TextBox_CasinoAI_6.Text != "" &&
                TextBox_CasinoAI_7.Text != "" &&
                TextBox_CasinoAI_8.Text != "" &&
                TextBox_CasinoAI_9.Text != "" &&
                TextBox_CasinoAI_10.Text != "" &&

                TextBox_CasinoAI_11.Text != "" &&
                TextBox_CasinoAI_12.Text != "" &&
                TextBox_CasinoAI_13.Text != "" &&
                TextBox_CasinoAI_14.Text != "" &&
                TextBox_CasinoAI_15.Text != "" &&

                TextBox_Doomsday_Player1.Text != "" &&
                TextBox_Doomsday_Player2.Text != "" &&
                TextBox_Doomsday_Player3.Text != "" &&
                TextBox_Doomsday_Player4.Text != "" &&

                TextBox_Doomsday_ActI.Text != "" &&
                TextBox_Doomsday_ActII.Text != "" &&
                TextBox_Doomsday_ActIII.Text != "" &&

                TextBox_Apart_Player1.Text != "" &&
                TextBox_Apart_Player2.Text != "" &&
                TextBox_Apart_Player3.Text != "" &&
                TextBox_Apart_Player4.Text != "" &&

                TextBox_Apart_Fleeca.Text != "" &&
                TextBox_Apart_PrisonBreak.Text != "" &&
                TextBox_Apart_HumaneLabs.Text != "" &&
                TextBox_Apart_SeriesA.Text != "" &&
                TextBox_Apart_PacificStandard.Text != "")
            {
                // 佩里克岛抢劫玩家分红比例
                Hacks.WriteGA<int>(1973321 + 823 + 56 + 1, Convert.ToInt32(TextBox_Cayo_Player1.Text));
                Hacks.WriteGA<int>(1973321 + 823 + 56 + 2, Convert.ToInt32(TextBox_Cayo_Player2.Text));
                Hacks.WriteGA<int>(1973321 + 823 + 56 + 3, Convert.ToInt32(TextBox_Cayo_Player3.Text));
                Hacks.WriteGA<int>(1973321 + 823 + 56 + 4, Convert.ToInt32(TextBox_Cayo_Player4.Text));

                Hacks.WriteGA<int>(262145 + 29970, Convert.ToInt32(TextBox_Cayo_Tequila.Text));
                Hacks.WriteGA<int>(262145 + 29971, Convert.ToInt32(TextBox_Cayo_RubyNecklace.Text));
                Hacks.WriteGA<int>(262145 + 29972, Convert.ToInt32(TextBox_Cayo_BearerBonds.Text));
                Hacks.WriteGA<int>(262145 + 29973, Convert.ToInt32(TextBox_Cayo_PinkDiamond.Text));
                Hacks.WriteGA<int>(262145 + 29974, Convert.ToInt32(TextBox_Cayo_MadrazoFiles.Text));
                Hacks.WriteGA<int>(262145 + 29975, Convert.ToInt32(TextBox_Cayo_BlackPanther.Text));

                Hacks.WriteGA<int>(262145 + 29720, Convert.ToInt32(TextBox_Cayo_LocalBagSize.Text));

                Hacks.WriteGA<float>(262145 + 29979, Convert.ToSingle(TextBox_Cayo_FencingFee.Text));
                Hacks.WriteGA<float>(262145 + 29980, Convert.ToSingle(TextBox_Cayo_PavelCut.Text));

                //////////////////////////////////////////////////////////////////////////////////

                // 赌场抢劫玩家分红比例
                Hacks.WriteGA<int>(1966534 + 1497 + 736 + 92 + 1, Convert.ToInt32(TextBox_Casino_Player1.Text));
                Hacks.WriteGA<int>(1966534 + 1497 + 736 + 92 + 2, Convert.ToInt32(TextBox_Casino_Player2.Text));
                Hacks.WriteGA<int>(1966534 + 1497 + 736 + 92 + 3, Convert.ToInt32(TextBox_Casino_Player3.Text));
                Hacks.WriteGA<int>(1966534 + 1497 + 736 + 92 + 4, Convert.ToInt32(TextBox_Casino_Player4.Text));

                Hacks.WriteGA<int>(262145 + 28779, Convert.ToInt32(TextBox_Casino_Lester.Text));

                Hacks.WriteGA<int>(262145 + 28793, Convert.ToInt32(TextBox_CasinoPotential_Money.Text));
                Hacks.WriteGA<int>(262145 + 28794, Convert.ToInt32(TextBox_CasinoPotential_Artwork.Text));
                Hacks.WriteGA<int>(262145 + 28795, Convert.ToInt32(TextBox_CasinoPotential_Gold.Text));
                Hacks.WriteGA<int>(262145 + 28796, Convert.ToInt32(TextBox_CasinoPotential_Diamonds.Text));

                Hacks.WriteGA<int>(262145 + 28804 + 1, Convert.ToInt32(TextBox_CasinoAI_1.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 2, Convert.ToInt32(TextBox_CasinoAI_2.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 3, Convert.ToInt32(TextBox_CasinoAI_3.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 4, Convert.ToInt32(TextBox_CasinoAI_4.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 5, Convert.ToInt32(TextBox_CasinoAI_5.Text));

                Hacks.WriteGA<int>(262145 + 28804 + 6, Convert.ToInt32(TextBox_CasinoAI_6.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 7, Convert.ToInt32(TextBox_CasinoAI_7.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 8, Convert.ToInt32(TextBox_CasinoAI_8.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 9, Convert.ToInt32(TextBox_CasinoAI_9.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 10, Convert.ToInt32(TextBox_CasinoAI_10.Text));

                Hacks.WriteGA<int>(262145 + 28804 + 11, Convert.ToInt32(TextBox_CasinoAI_11.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 12, Convert.ToInt32(TextBox_CasinoAI_12.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 13, Convert.ToInt32(TextBox_CasinoAI_13.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 14, Convert.ToInt32(TextBox_CasinoAI_14.Text));
                Hacks.WriteGA<int>(262145 + 28804 + 15, Convert.ToInt32(TextBox_CasinoAI_15.Text));

                //////////////////////////////////////////////////////////////////////////////////

                // 末日抢劫玩家分红比例
                Hacks.WriteGA<int>(1962546 + 812 + 50 + 1, Convert.ToInt32(TextBox_Doomsday_Player1.Text));
                Hacks.WriteGA<int>(1962546 + 812 + 50 + 2, Convert.ToInt32(TextBox_Doomsday_Player2.Text));
                Hacks.WriteGA<int>(1962546 + 812 + 50 + 3, Convert.ToInt32(TextBox_Doomsday_Player3.Text));
                Hacks.WriteGA<int>(1962546 + 812 + 50 + 4, Convert.ToInt32(TextBox_Doomsday_Player4.Text));

                Hacks.WriteGA<int>(262145 + 9132, Convert.ToInt32(TextBox_Doomsday_ActI.Text));
                Hacks.WriteGA<int>(262145 + 9133, Convert.ToInt32(TextBox_Doomsday_ActII.Text));
                Hacks.WriteGA<int>(262145 + 9134, Convert.ToInt32(TextBox_Doomsday_ActIII.Text));

                //////////////////////////////////////////////////////////////////////////////////

                // 公寓抢劫玩家分红比例
                Hacks.WriteGA<int>(1933908 + 3008 + 1, Convert.ToInt32(TextBox_Apart_Player1.Text));
                Hacks.WriteGA<int>(1933908 + 3008 + 2, Convert.ToInt32(TextBox_Apart_Player2.Text));
                Hacks.WriteGA<int>(1933908 + 3008 + 3, Convert.ToInt32(TextBox_Apart_Player3.Text));
                Hacks.WriteGA<int>(1933908 + 3008 + 4, Convert.ToInt32(TextBox_Apart_Player4.Text));

                Hacks.WriteGA<int>(262145 + 9127, Convert.ToInt32(TextBox_Apart_Fleeca.Text));
                Hacks.WriteGA<int>(262145 + 9128, Convert.ToInt32(TextBox_Apart_PrisonBreak.Text));
                Hacks.WriteGA<int>(262145 + 9129, Convert.ToInt32(TextBox_Apart_HumaneLabs.Text));
                Hacks.WriteGA<int>(262145 + 9130, Convert.ToInt32(TextBox_Apart_SeriesA.Text));
                Hacks.WriteGA<int>(262145 + 9131, Convert.ToInt32(TextBox_Apart_PacificStandard.Text));

                TextBox_Result.Text = $"data written successfully";
            }
            else
            {
                MessageBox.Show("Part of the data is empty when written, please check and rewrite", "warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }
}
