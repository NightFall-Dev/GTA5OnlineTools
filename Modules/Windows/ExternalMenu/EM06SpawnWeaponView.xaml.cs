using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM06SpawnWeaponView.xaml 的交互逻辑
/// </summary>
public partial class EM06SpawnWeaponView : UserControl
{
    public EM06SpawnWeaponView()
    {
        InitializeComponent();

        // 武器列表
        for (int i = 0; i < WeaponData.WeaponDataClass.Count; i++)
        {
            ListBox_WeaponList.Items.Add(WeaponData.WeaponDataClass[i].ClassName);
        }
        ListBox_WeaponList.SelectedIndex = 0;

        // 子弹类型
        for (int i = 0; i < MiscData.ImpactExplosions.Count; i++)
        {
            ComboBox_ImpactExplosion.Items.Add(MiscData.ImpactExplosions[i].Name);
        }
        ComboBox_ImpactExplosion.SelectedIndex = 0;

        ExternalMenuWindow.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {

    }

    private void ListBox_WeaponList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ListBox_WeaponList.SelectedIndex;

        if (index != -1)
        {
            ListBox_WeaponInfo.Items.Clear();

            for (int i = 0; i < WeaponData.WeaponDataClass[index].WeaponInfo.Count; i++)
            {
                ListBox_WeaponInfo.Items.Add(WeaponData.WeaponDataClass[index].WeaponInfo[i].DisplayName);
            }

            ListBox_WeaponInfo.SelectedIndex = 0;
        }
    }

    private void ListBox_WeaponInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index1 = ListBox_WeaponList.SelectedIndex;
        int index2 = ListBox_WeaponInfo.SelectedIndex;

        if (index1 != -1 && index2 != -1)
        {
            TempData.WPickup = "pickup_" + WeaponData.WeaponDataClass[index1].WeaponInfo[index2].Name;
        }
    }

    private void Button_SpawnWeapon_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.CreateAmbientPickup(TempData.WPickup);
    }

    ////////////////////////////////////////////////////////////////////////////////////////

    private void RadioButton_AmmoModifier_None_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_AmmoModifier_None.IsChecked == true)
        {
            Weapon.AmmoModifier(0);
        }
        else if (RadioButton_AmmoModifier_AMMO.IsChecked == true)
        {
            Weapon.AmmoModifier(1);
        }
        else if (RadioButton_AmmoModifier_CLIP.IsChecked == true)
        {
            Weapon.AmmoModifier(2);
        }
        else if (RadioButton_AmmoModifier_Both.IsChecked == true)
        {
            Weapon.AmmoModifier(3);
        }
    }

    private void CheckBox_ReloadMult_Click(object sender, RoutedEventArgs e)
    {
        Weapon.ReloadMult(CheckBox_ReloadMult.IsChecked == true);
    }

    private void Button_NoRecoil_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Weapon.NoRecoil();
    }

    private void CheckBox_NoSpread_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Weapon.NoSpread();
    }

    private void CheckBox_Range_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Weapon.Range();
    }

    private void CheckBox_ImpactType_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ImpactType.IsChecked == true)
        {
            Weapon.ImpactType(5);
        }
        else
        {
            Weapon.ImpactType(3);
        }
    }

    private void ComboBox_ImpactExplosion_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ComboBox_ImpactExplosion.SelectedIndex;

        if (index != -1)
        {
            Weapon.ImpactExplosion(MiscData.ImpactExplosions[index].ID);
        }
    }

    private void Button_FillCurrentAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Weapon.FillCurrentAmmo();
    }

    private void Button_FillAllAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Weapon.FillAllAmmo();
    }
}
