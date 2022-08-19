using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Common.Utils;
using static GTA5OnlineTools.Features.Data.TeleportData;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// HeistPrepsWindow.xaml 的交互逻辑
/// </summary>
public partial class HeistPrepsWindow : Window
{
    public HeistPrepsWindow()
    {
        InitializeComponent();
    }

    private void Window_HeistPreps_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.WorldPTR = Memory.FindPattern(Offsets.Mask.WorldMask);
            Globals.WorldPTR = Memory.Rip_37(Globals.WorldPTR);

            Globals.GlobalPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
            Globals.GlobalPTR = Memory.Rip_37(Globals.GlobalPTR);

            this.Dispatcher.Invoke(() =>
            {

            });
        });
    }

    private void AppendTextBox(string str)
    {
        TextBox_Result.AppendText($"[{DateTime.Now:T}] {str}\r\n");
        TextBox_Result.ScrollToEnd();
    }

    private void Window_HeistPreps_Closing(object sender, CancelEventArgs e)
    {

    }

    ////////////////////////////////////////////////////

    private void WriteStatWithDelay(string hash, int value)
    {
        Hacks.WriteStat(hash, value);
        Thread.Sleep(1000);
    }

    private void Button_H3OPT_BITSET1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        AppendTextBox($"Reset the first panel successfully");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_APPROACH_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_APPROACH", 1);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Set the robbery method to be invisible");
    }

    private void Button_H3OPT_APPROACH_2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_APPROACH", 2);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Set the robbery method to 'Successful'");
    }

    private void Button_H3OPT_APPROACH_3_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_APPROACH", 3);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Set robbery method to menacing success");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_TARGET_0_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_TARGET", 0);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Set loot item to cash success");
    }

    private void Button_H3OPT_TARGET_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_TARGET", 1);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Set loot item to gold success");
    }

    private void Button_H3OPT_TARGET_2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_TARGET", 2);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Set the looted item as art success");
    }

    private void Button_H3OPT_TARGET_3_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_TARGET", 3);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Set the loot item to diamond success");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_ACCESSPOINTS_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_ACCESSPOINTS", -1);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Successfully unlocked all scout points");
    }

    private void Button_H3OPT_ACCESSPOINTS_0_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_ACCESSPOINTS", 0);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Unlock all scout points successfully");
    }

    private void Button_H3OPT_H3OPT_POI_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_POI", -1);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Unlock all POIs successfully");
    }

    private void Button_H3OPT_H3OPT_POI_0_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET1", 0);
        WriteStatWithDelay("_H3OPT_POI", 0);
        WriteStatWithDelay("_H3OPT_BITSET1", -1);
        AppendTextBox($"Unlock all POIs successfully");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_BITSET0_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        AppendTextBox($"Reset the second panel successfully");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_DISRUPTSHIP_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_DISRUPTSHIP", 3);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Successful removal of heavily armed guards");
    }

    private void Button_H3OPT_KEYLEVELS_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_KEYLEVELS", 2);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Obtained secondary access control card successfully");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_CREWWEAP_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWWEAP", 1);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Gunslinger level to Carl Abrad 5% success");
    }

    private void Button_H3OPT_CREWWEAP_2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWWEAP", 2);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Gunner Level to Gustavo Mota 9% success");
    }

    private void Button_H3OPT_CREWWEAP_3_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWWEAP", 3);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Gunner Rating to Charlie Reid 7% success");
    }

    private void Button_H3OPT_CREWWEAP_4_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWWEAP", 4);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Gunner Rating to Chester McCoy 10% Success");
    }

    private void Button_H3OPT_CREWWEAP_5_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWWEAP", 5);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Gunner Rating to Patrick McRaeley 8% success");
    }

    private void Button_H3OPT_CREWWEAP_6_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWWEAP", 6);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the gunner's level to zero bonus for gunner success");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_CREWDRIVER_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWDRIVER", 1);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Driver Rating to Karim Denz 5% Success");
    }

    private void Button_H3OPT_CREWDRIVER_2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWDRIVER", 2);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Driver Rating to Tarina Martinez 7% Success");
    }

    private void Button_H3OPT_CREWDRIVER_3_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWDRIVER", 3);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the driver level to Tao Eddie 9% success");
    }

    private void Button_H3OPT_CREWDRIVER_4_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWDRIVER", 4);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Driver Rating to Zach Nelson 6% Success");
    }

    private void Button_H3OPT_CREWDRIVER_5_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWDRIVER", 5);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Driver Rating to Chester McCoy 10% Success");
    }

    private void Button_H3OPT_CREWDRIVER_6_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWDRIVER", 6);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the driver level to zero bonus for driver success");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_CREWHACKER_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWHACKER", 1);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set hacking level to Ricky Lukens 3% success");
    }

    private void Button_H3OPT_CREWHACKER_2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWHACKER", 2);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Hacking Rating to Kristen Fields 7% Success");
    }

    private void Button_H3OPT_CREWHACKER_3_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWHACKER", 3);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set hacking level to Yohan Blair 5% success");
    }

    private void Button_H3OPT_CREWHACKER_4_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWHACKER", 4);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the hacking level to Avi Schwarzman 10% success");
    }

    private void Button_H3OPT_CREWHACKER_5_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWHACKER", 5);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set Hacking Rating to Paige Harris 9% Success");
    }

    private void Button_H3OPT_CREWHACKER_6_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_CREWHACKER", 6);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the hacker level to hacker zero bonus success");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_WEAPS_0_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_WEAPS", 0);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"set weapon type to type one success");
    }

    private void Button_H3OPT_WEAPS_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_WEAPS", 1);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the weapon type to type 2 success");
    }

    private void Button_H3OPT_WEAPS_2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_WEAPS", 2);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the weapon type to type three success");
    }

    private void Button_H3OPT_WEAPS_3_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_WEAPS", 3);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the weapon type to type 4 success");
    }

    private void Button_H3OPT_WEAPS_4_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_WEAPS", 4);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the weapon type to type five success");
    }

    ////////////////////////////////////////////////////

    private void Button_H3OPT_VEH_0_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_VEHS", 0);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set escape vehicle to type one success");
    }

    private void Button_H3OPT_VEH_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_VEHS", 1);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set escape vehicle to type 2 success");
    }

    private void Button_H3OPT_VEH_2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_VEHS", 2);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set the escape vehicle to type 3 success");
    }

    private void Button_H3OPT_VEH_3_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_H3OPT_BITSET0", 0);
        WriteStatWithDelay("_H3OPT_VEHS", 3);
        WriteStatWithDelay("_H3OPT_BITSET0", -1);
        AppendTextBox($"Set escape vehicle to type 4 success");
    }

    ////////////////////////////////////////////////////

    private void Button_FastTeleport_Click(object sender, RoutedEventArgs e)
    {
        var str = (e.OriginalSource as Button).Content.ToString();

        int index = HeistPrepsConfig.FastTeleport.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Teleport.SetTeleportV3Pos(HeistPrepsConfig.FastTeleport[index].Position);
        }

        AppendTextBox($"send to {str} success");
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Teleport.ToBlips(740);

        AppendTextBox($"Teleport to the arcade icon Successfully");
    }

    ////////////////////////////////////////////////////

    private void Button_GANGOPS_FLOW_MISSION_PROG_503_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 503);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 229383);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
        AppendTextBox($"Entering the doomsday dividend level success");
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_240_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 240);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 229378);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
        AppendTextBox($"Enter the doomsday two dividend level success");
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_16368_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 16368);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 229380);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
        AppendTextBox($"Enter the doomsday three-point bonus level success");
    }

    ////////////////////////////////////////////////////

    private void Button_HEISTCOOLDOWNTIMER0_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_HEISTCOOLDOWNTIMER0", -1);
        AppendTextBox($"Reset Doomsday Cooldown Success");
    }

    private void Button_HEISTCOOLDOWNTIMER1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_HEISTCOOLDOWNTIMER1", -1);
        AppendTextBox($"Reset Doom II cooldown successful");
    }

    private void Button_HEISTCOOLDOWNTIMER2_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_HEISTCOOLDOWNTIMER2", -1);
        AppendTextBox($"Doomsday 3 cooldown reset successful");
    }

    private void Button_GANGOPS_HEIST_STATUS_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", -1);
        AppendTextBox($"Reset Doomsday 123 Mission Success");
    }

    private void Button_GANGOPS_FLOW_NOTIFICATIONS_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 9999);

        //WriteWithStat("_GANGOPS_HEIST_STATUS", -1);
        //CoreUtils.Delay(500);

        //WriteWithStat("_GANGOPS_FLOW_NOTIFICATIONS", -1);
        //CoreUtils.Delay(500);

        AppendTextBox($"Unlock and replay the Doomsday Heist");
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", -1);
        AppendTextBox($"Skip doomsday pre- and preparation tasks");
    }

    private void Button_HEIST_PLANNING_STAGE_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_HEIST_PLANNING_STAGE", -1);
        AppendTextBox($"Go directly to the dividend level success");
    }
}

public class HeistPrepsConfig
{
    public static List<TeleportInfo> FastTeleport = new List<TeleportInfo>()
    {
        new TeleportInfo(){ Name="Casino gate", Position=new Vector3 { X=911.072f, Y=53.321f, Z=80.893f } },
        new TeleportInfo(){ Name="Surveillance and Security Personnel", Position=new Vector3 { X=1089.614f, Y=215.696f, Z=-49.200f } },
        new TeleportInfo(){ Name="access control system", Position=new Vector3 { X=1117.732f, Y=214.123f, Z=-49.440f } },
        new TeleportInfo(){ Name="Casino backdoor", Position=new Vector3 { X=993.162f, Y=86.234f, Z=80.990f } },
    };
}
