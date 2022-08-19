using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;
using static GTA5OnlineTools.Features.SDK.Hacks;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM03OnlineOptionView.xaml 的交互逻辑
/// </summary>
public partial class EM03OnlineOptionView : UserControl
{
    private struct PVInfo
    {
        public int Index;
        public string Name;
        public long hash;
        public string plate;
    }

    private List<PVInfo> pVInfos = new List<PVInfo>();

    public EM03OnlineOptionView()
    {
        InitializeComponent();

        ExternalMenuWindow.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void CheckBox_RemovePassiveModeCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.RemovePassiveModeCooldown(CheckBox_RemovePassiveModeCooldown.IsChecked == true);
    }

    private void CheckBox_RemoveSuicideCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveSuicideCooldown(CheckBox_RemoveSuicideCooldown.IsChecked == true);
    }

    private void CheckBox_DisableOrbitalCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.DisableOrbitalCooldown(CheckBox_DisableOrbitalCooldown.IsChecked == true);
    }

    private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e)
    {
        Online.OffRadar(CheckBox_OffRadar.IsChecked == true);
    }

    private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e)
    {
        Online.GhostOrganization(CheckBox_GhostOrganization.IsChecked == true);
    }

    private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e)
    {
        Online.BribeOrBlindCops(CheckBox_BribeOrBlindCops.IsChecked == true);
    }

    private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e)
    {
        Online.BribeAuthorities(CheckBox_BribeAuthorities.IsChecked == true);
    }

    private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e)
    {
        Online.RevealPlayers(CheckBox_RevealPlayers.IsChecked == true);
    }

    private void CheckBox_AllowSellOnNonPublic_Click(object sender, RoutedEventArgs e)
    {
        Online.AllowSellOnNonPublic(CheckBox_AllowSellOnNonPublic.IsChecked == true);
    }

    private void CheckBox_OnlineSnow_Click(object sender, RoutedEventArgs e)
    {
        Online.SessionSnow(CheckBox_OnlineSnow.IsChecked == true);
    }

    private void Button_Blips_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.Blips.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Teleport.ToBlips(MiscData.Blips[index].ID);
        }
    }

    private void Button_MerryweatherServices_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.MerryWeatherServices.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            Online.MerryWeatherServices(MiscData.MerryWeatherServices[index].ID);
        }
    }

    private void CheckBox_InstantBullShark_Click(object sender, RoutedEventArgs e)
    {
        Online.InstantBullShark(CheckBox_InstantBullShark.IsChecked == true);
    }

    private void CheckBox_BackupHeli_Click(object sender, RoutedEventArgs e)
    {
        Online.CallBackupHeli(CheckBox_BackupHeli.IsChecked == true);
    }

    private void CheckBox_Airstrike_Click(object sender, RoutedEventArgs e)
    {
        Online.CallAirstrike(CheckBox_Airstrike.IsChecked == true);
    }

    private void Button_RefushPersonalVehicleList_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        ListBox_PersonalVehicle.Items.Clear();
        pVInfos.Clear();

        Task.Run(() =>
        {
            int max_slots = ReadGA<int>(1585857);
            for (int i = 0; i < max_slots; i++)
            {
                long hash = ReadGA<long>(1585857 + 1 + (i * 142) + 66);
                if (hash == 0)
                    continue;

                string plate = ReadGAString(1585857 + 1 + (i * 142) + 1);

                pVInfos.Add(new PVInfo()
                {
                    Index = i,
                    Name = Vehicle.FindVehicleDisplayName(hash, true),
                    hash = hash,
                    plate = plate
                });
            }

            foreach (var item in pVInfos)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListBox_PersonalVehicle.Items.Add($"[{item.plate}]\t{item.Name}");
                });
            }
        });
    }

    private void Button_SpawnPersonalVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_PersonalVehicle.SelectedIndex;
        if (index != -1)
        {
            Task.Run(() =>
            {
                Vehicle.SpawnPersonalVehicle(pVInfos[index].Index);
            });
        }
    }
}
