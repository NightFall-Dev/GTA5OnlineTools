﻿using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM08ExternalOverlayView.xaml 的交互逻辑
/// </summary>
public partial class EM08ExternalOverlayView : UserControl
{
    private Overlay overlay;

    private InjectInfo InjectInfo { get; set; }

    public EM08ExternalOverlayView()
    {
        InitializeComponent();

        ExternalMenuWindow.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {
        overlay?.Dispose();
    }

    private void Button_Overaly_Run_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (overlay == null)
        {
            GameOverlay.TimerService.EnableHighPrecisionTimers();

            Task.Run(() =>
            {
                overlay = new Overlay();
                overlay.Run();
            });
        }
        else
        {
            MessageBox.Show("ESP程序已经运行了，请勿重复启动",
                "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void CloseESP()
    {
        if (overlay != null)
        {
            overlay.Dispose();
            overlay = null;
        }
    }

    private void Button_Overaly_Exit_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        CloseESP();
    }

    private void CheckBox_ESP_2DBox_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_2DBox.IsChecked == true)
        {
            Settings.Overlay.ESP_2DBox = true;

            Settings.Overlay.ESP_3DBox = false;
            CheckBox_ESP_3DBox.IsChecked = false;
        }
        else
        {
            Settings.Overlay.ESP_2DBox = false;
        }
    }

    private void CheckBox_ESP_3DBox_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_3DBox.IsChecked == true)
        {
            Settings.Overlay.ESP_3DBox = true;

            Settings.Overlay.ESP_2DBox = false;
            CheckBox_ESP_2DBox.IsChecked = false;
        }
        else
        {
            Settings.Overlay.ESP_3DBox = false;
        }
    }

    private void CheckBox_ESP_2DLine_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_2DLine.IsChecked == true)
        {
            Settings.Overlay.ESP_2DLine = true;
        }
        else
        {
            Settings.Overlay.ESP_2DLine = false;
        }
    }

    private void CheckBox_ESP_Bone_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_Bone.IsChecked == true)
        {
            Settings.Overlay.ESP_Bone = true;
        }
        else
        {
            Settings.Overlay.ESP_Bone = false;
        }
    }

    private void CheckBox_ESP_2DHealthBar_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_2DHealthBar.IsChecked == true)
        {
            Settings.Overlay.ESP_2DHealthBar = true;
        }
        else
        {
            Settings.Overlay.ESP_2DHealthBar = false;
        }
    }

    private void CheckBox_ESP_HealthText_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_HealthText.IsChecked == true)
        {
            Settings.Overlay.ESP_HealthText = true;
        }
        else
        {
            Settings.Overlay.ESP_HealthText = false;
        }
    }

    private void CheckBox_ESP_NameText_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_NameText.IsChecked == true)
        {
            Settings.Overlay.ESP_NameText = true;
        }
        else
        {
            Settings.Overlay.ESP_NameText = false;
        }
    }

    private void CheckBox_AimBot_Enabled_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_AimBot_Enabled.IsChecked == true)
        {
            Settings.Overlay.AimBot_Enabled = true;
        }
        else
        {
            Settings.Overlay.AimBot_Enabled = false;
        }
    }

    private void CheckBox_ESP_Player_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_Player.IsChecked == true)
        {
            Settings.Overlay.ESP_Player = true;
        }
        else
        {
            Settings.Overlay.ESP_Player = false;
        }
    }

    private void CheckBox_ESP_NPC_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_NPC.IsChecked == true)
        {
            Settings.Overlay.ESP_NPC = true;
        }
        else
        {
            Settings.Overlay.ESP_NPC = false;
        }
    }

    private void RadioButton_Overlay_RunMode0_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_Overlay_RunMode0.IsChecked == true)
        {
            Settings.Overlay.VSync = true;
            Settings.Overlay.FPS = 300;

            CloseESP();
        }
        else if (RadioButton_Overlay_RunMode1.IsChecked == true)
        {
            Settings.Overlay.VSync = false;
            Settings.Overlay.FPS = 300;

            CloseESP();
        }
        else if (RadioButton_Overlay_RunMode2.IsChecked == true)
        {
            Settings.Overlay.VSync = false;
            Settings.Overlay.FPS = 144;

            CloseESP();
        }
        else if (RadioButton_Overlay_RunMode3.IsChecked == true)
        {
            Settings.Overlay.VSync = false;
            Settings.Overlay.FPS = 90;

            CloseESP();
        }
        else if (RadioButton_Overlay_RunMode4.IsChecked == true)
        {
            Settings.Overlay.VSync = false;
            Settings.Overlay.FPS = 60;

            CloseESP();
        }
    }

    private void CheckBox_ESP_Crosshair_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_ESP_Crosshair.IsChecked == true)
        {
            Settings.Overlay.ESP_Crosshair = true;
        }
        else
        {
            Settings.Overlay.ESP_Crosshair = false;
        }
    }

    private void RadioButton_AimbotKey_CONTROL_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_AimbotKey_CONTROL.IsChecked == true)
        {
            Settings.Overlay.AimBot_Key = WinVK.CONTROL;
        }
        else if (RadioButton_AimbotKey_SHIFT.IsChecked == true)
        {
            Settings.Overlay.AimBot_Key = WinVK.SHIFT;
        }
        else if (RadioButton_AimbotKey_LBUTTON.IsChecked == true)
        {
            Settings.Overlay.AimBot_Key = WinVK.LBUTTON;
        }
        else if (RadioButton_AimbotKey_RBUTTON.IsChecked == true)
        {
            Settings.Overlay.AimBot_Key = WinVK.RBUTTON;
        }
        else if (RadioButton_AimbotKey_XBUTTON1.IsChecked == true)
        {
            Settings.Overlay.AimBot_Key = WinVK.XBUTTON1;
        }
        else if (RadioButton_AimbotKey_XBUTTON2.IsChecked == true)
        {
            Settings.Overlay.AimBot_Key = WinVK.XBUTTON2;
        }
    }

    private void RadioButton_AimBot_BoneIndex_0_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_AimBot_BoneIndex_0.IsChecked == true)
        {
            Settings.Overlay.AimBot_BoneIndex = 0;
        }
        else if (RadioButton_AimBot_BoneIndex_7.IsChecked == true)
        {
            Settings.Overlay.AimBot_BoneIndex = 7;
        }
        else if (RadioButton_AimBot_BoneIndex_8.IsChecked == true)
        {
            Settings.Overlay.AimBot_BoneIndex = 8;
        }
    }

    private void RadioButton_AimbotFov_Height_Click(object sender, RoutedEventArgs e)
    {
        var windowData = Memory.GetGameWindowData();

        if (RadioButton_Crosshair_NearBy.IsChecked == true)
        {
            Settings.Overlay.AimBot_Fov = 250.0f;
        }
        else if (RadioButton_AimbotFov_14Height.IsChecked == true)
        {
            Settings.Overlay.AimBot_Fov = windowData.Height / 4.0f;
        }
        else if (RadioButton_AimbotFov_12Height.IsChecked == true)
        {
            Settings.Overlay.AimBot_Fov = windowData.Height / 2.0f;
        }
        else if (RadioButton_AimbotFov_Height.IsChecked == true)
        {
            Settings.Overlay.AimBot_Fov = windowData.Height;
        }
        else if (RadioButton_AimbotFov_Width.IsChecked == true)
        {
            Settings.Overlay.AimBot_Fov = windowData.Width;
        }
        else if (RadioButton_AimbotFov_All.IsChecked == true)
        {
            Settings.Overlay.AimBot_Fov = 8848.0f;
        }
    }

    private void CheckBox_NoTOPMostHide_Click(object sender, RoutedEventArgs e)
    {
        Settings.Overlay.IsNoTOPMostHide = CheckBox_NoTOPMostHide.IsChecked == true;
    }
}
