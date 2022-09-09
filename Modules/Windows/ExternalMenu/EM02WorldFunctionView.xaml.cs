using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM02WorldFunctionView.xaml 的交互逻辑
/// </summary>
public partial class EM02WorldFunctionView : UserControl
{
    public EM02WorldFunctionView()
    {
        InitializeComponent();

        // Ped列表
        foreach (var item in PedData.PedDataClass)
        {
            ListBox_PedModel.Items.Add(item.DisplayName);
        }
        ListBox_PedModel.SelectedIndex = 0;

        ExternalMenuWindow.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void Button_Sessions_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.Sessions.FindIndex(t => t.Name == str);
        if (index != -1)
            Online.LoadSession(MiscData.Sessions[index].ID);
    }

    private void Button_EmptySession_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Online.EmptySession();
    }

    private void Button_LocalWeather_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.LocalWeathers.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            World.Set_Local_Weather(MiscData.LocalWeathers[index].ID);
        }
    }

    private void Button_KillNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.KillNPC(false);
    }
    private void Button_KillAllHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.KillNPC(true);
    }

    private void Button_KillAllPolice_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.KillPolice();
    }

    private void Button_DestroyAllVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.DestroyAllVehicles();
    }

    private void Button_DestroyAllNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.DestroyNPCVehicles(false);
    }

    private void Button_DestroyAllHostilityNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.DestroyNPCVehicles(true);
    }

    private void Button_TPAllNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.TeleportNPCToMe(false);
    }

    private void Button_TPHostilityNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.TeleportNPCToMe(true);
    }

    private void Button_RPxN_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.RPxNs.FindIndex(t => t.Name == str);
        if (index != -1)
            Online.RPMultiplier(MiscData.RPxNs[index].ID);
    }

    private void Button_REPxN_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.REPxNs.FindIndex(t => t.Name == str);
        if (index != -1)
            Online.REPMultiplier(MiscData.REPxNs[index].ID);
    }

    private void Button_ModelChange_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var index = ListBox_PedModel.SelectedIndex;
        if (index != -1)
            Online.ModelChange(PedData.PedDataClass[index].Hash);
    }
}
