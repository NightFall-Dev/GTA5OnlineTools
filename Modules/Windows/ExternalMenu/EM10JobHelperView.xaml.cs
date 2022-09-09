using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM10JobHelperView.xaml 的交互逻辑
/// </summary>
public partial class EM10JobHelperView : UserControl
{
    public EM10JobHelperView()
    {
        InitializeComponent();

        ExternalMenuWindow.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void CheckBox_RemoveBunkerSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveBunkerSupplyDelay(CheckBox_RemoveBunkerSupplyDelay.IsChecked == true);
    }

    private void CheckBox_UnlockBunkerResearch_Click(object sender, RoutedEventArgs e)
    {
        Online.UnlockBunkerResearch(CheckBox_UnlockBunkerResearch.IsChecked == true);
    }

    private void CheckBox_RemoveBuyingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOBuyingCratesCooldown(CheckBox_CooldownForBuyingCrates.IsChecked == true);
    }

    private void CheckBox_RemoveSellingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOSellingCratesCooldown(CheckBox_CooldownForSellingCrates.IsChecked == true);
    }

    private void CheckBox_PricePerCrateAtCrates_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOPricePerCrateAtCrates(CheckBox_PricePerCrateAtCrates.IsChecked == true);
    }

    private void CheckBox_RemoveMCSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveMCSupplyDelay(CheckBox_RemoveMCSupplyDelay.IsChecked == true);
    }

    private void Button_CEOCargos_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.CEOCargos.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            // They are in gb_contraband_buy at func_915, for future updates.
            Online.CEOSpecialCargo(false);
            Thread.Sleep(100);
            Online.CEOSpecialCargo(true);
            Thread.Sleep(100);
            Online.CEOCargoType(MiscData.CEOCargos[index].ID);
        }
    }

    private void CheckBox_RemoveExportVehicleDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveExportVehicleDelay(CheckBox_RemoveExportVehicleDelay.IsChecked == true);
    }

    private void CheckBox_RemoveSmugglerRunInDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveSmugglerRunInDelay(CheckBox_RemoveSmugglerRunInDelay.IsChecked == true);
    }

    private void CheckBox_RemoveSmugglerRunOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveSmugglerRunOutDelay(CheckBox_RemoveSmugglerRunOutDelay.IsChecked == true);
    }

    private void CheckBox_RemoveNightclubOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.RemoveNightclubOutDelay(CheckBox_RemoveNightclubOutDelay.IsChecked == true);
    }
}
