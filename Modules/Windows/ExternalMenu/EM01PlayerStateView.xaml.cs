using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM01PlayerStateView.xaml 的交互逻辑
/// </summary>
public partial class EM01PlayerStateView : UserControl
{
    // 快捷键
    private HotKeys MainHotKeys;

    private bool NoCollisionToggle = false;

    public EM01PlayerStateView()
    {
        InitializeComponent();

        var thread0 = new Thread(MainThread);
        thread0.IsBackground = true;
        thread0.Start();

        var thread1 = new Thread(CommonThread);
        thread1.IsBackground = true;
        thread1.Start();

        MainHotKeys = new HotKeys();
        MainHotKeys.AddKey(WinVK.F3);
        MainHotKeys.AddKey(WinVK.F4);
        MainHotKeys.AddKey(WinVK.F5);
        MainHotKeys.AddKey(WinVK.F6);
        MainHotKeys.AddKey(WinVK.F7);
        MainHotKeys.AddKey(WinVK.F8);
        MainHotKeys.AddKey(WinVK.Oem0);
        MainHotKeys.AddKey(WinVK.DELETE);
        MainHotKeys.KeyDownEvent += new HotKeys.KeyHandler(MyKeyDownEvent);

        ExternalMenuWindow.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {
        MainHotKeys.Dispose();
    }

    private void MyKeyDownEvent(int keyId, string keyName)
    {
        Dispatcher.BeginInvoke(new Action(delegate
        {
            switch (keyId)
            {
                case (int)WinVK.DELETE:
                    ExternalMenuWindow.IsShowWindowDelegate();
                    break;
                case (int)WinVK.F3:
                    if (CheckBox_FillAllAmmo.IsChecked == true)
                    {
                        Weapon.FillAllAmmo();
                    }
                    break;
                case (int)WinVK.F4:
                    if (CheckBox_MovingFoward.IsChecked == true)
                    {
                        Teleport.MovingFoward();
                    }
                    break;
                case (int)WinVK.F5:
                    if (CheckBox_ToWaypoint.IsChecked == true)
                    {
                        Teleport.ToWaypoint();
                    }
                    break;
                case (int)WinVK.F6:
                    if (CheckBox_ToObjective.IsChecked == true)
                    {
                        Teleport.ToObjective();
                    }
                    break;
                case (int)WinVK.F7:
                    if (CheckBox_FillHealthArmor.IsChecked == true)
                    {
                        Player.FillHealthArmor();
                    }
                    break;
                case (int)WinVK.F8:
                    if (CheckBox_ClearWanted.IsChecked == true)
                    {
                        Player.WantedLevel(0x00);
                    }
                    break;
                case (int)WinVK.Oem0:
                    if (CheckBox_NoCollision.IsChecked == true)
                    {
                        NoCollisionToggle = !NoCollisionToggle;

                        Player.NoCollision(NoCollisionToggle);
                        Settings.Player.NoCollision = NoCollisionToggle;

                        if (NoCollisionToggle)
                            Console.Beep(600, 75);
                        else
                            Console.Beep(500, 75);
                    }
                    break;
            }
        }));
    }

    private void MainThread()
    {
        while (true)
        {
            float oHealth = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.Health);
            float oMaxHealth = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.MaxHealth);
            float oArmor = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.Armor);

            byte oWanted = Memory.Read<byte>(Globals.WorldPTR, Offsets.Player.Wanted);
            float oRunSpeed = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.RunSpeed);
            float oSwimSpeed = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.SwimSpeed);
            float oStealthSpeed = Memory.Read<float>(Globals.WorldPTR, Offsets.Player.StealthSpeed);

            byte oInVehicle = Memory.Read<byte>(Globals.WorldPTR, Offsets.InVehicle);
            byte oCurPassenger = Memory.Read<byte>(Globals.WorldPTR, Offsets.Vehicle.CurPassenger);

            ////////////////////////////////

            if (Settings.Player.GodMode)
                Player.GodMode(true);

            if (Settings.Player.AntiAFK)
                Player.AntiAFK(true);

            if (Settings.Player.NoRagdoll)
                Player.NoRagdoll(true);

            if (Settings.Player.NoCollision)
                Memory.Write(Globals.WorldPTR, Offsets.Player.NoCollision, -1.0f);

            if (Settings.Vehicle.VehicleGodMode)
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Vehicle.GodMode, 0x01);

            if (Settings.Vehicle.VehicleSeatbelt)
                Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Seatbelt, 0xC9);

            ////////////////////////////////

            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (Slider_Health.Value != oHealth)
                    Slider_Health.Value = oHealth;

                if (Slider_MaxHealth.Value != oMaxHealth)
                    Slider_MaxHealth.Value = oMaxHealth;

                if (Slider_Armor.Value != oArmor)
                    Slider_Armor.Value = oArmor;

                if (Slider_Wanted.Value != oWanted)
                    Slider_Wanted.Value = oWanted;

                if (Slider_RunSpeed.Value != oRunSpeed)
                    Slider_RunSpeed.Value = oRunSpeed;

                if (Slider_SwimSpeed.Value != oSwimSpeed)
                    Slider_SwimSpeed.Value = oSwimSpeed;

                if (Slider_StealthSpeed.Value != oStealthSpeed)
                    Slider_StealthSpeed.Value = oStealthSpeed;
            }));

            Thread.Sleep(1000);
        }
    }

    private void CommonThread()
    {
        while (true)
        {
            if (Settings.Common.AutoClearWanted)
                Player.WantedLevel(0x00);

            if (Settings.Common.AutoKillNPC)
                World.KillNPC(false);

            if (Settings.Common.AutoKillHostilityNPC)
                World.KillNPC(true);

            if (Settings.Common.AutoKillPolice)
                World.KillPolice();

            Thread.Sleep(200);
        }
    }

    private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Memory.Write<float>(Globals.WorldPTR, Offsets.Player.Health, (float)Slider_Health.Value);
    }

    private void Slider_MaxHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Memory.Write<float>(Globals.WorldPTR, Offsets.Player.MaxHealth, (float)Slider_MaxHealth.Value);
    }

    private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Memory.Write<float>(Globals.WorldPTR, Offsets.Player.Armor, (float)Slider_Armor.Value);
    }

    private void Slider_Wanted_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Wanted, (byte)Slider_Wanted.Value);
    }

    private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Memory.Write<float>(Globals.WorldPTR, Offsets.Player.RunSpeed, (float)Slider_RunSpeed.Value);
    }

    private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Memory.Write<float>(Globals.WorldPTR, Offsets.Player.SwimSpeed, (float)Slider_SwimSpeed.Value);
    }

    private void Slider_StealthSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Memory.Write<float>(Globals.WorldPTR, Offsets.Player.StealthSpeed, (float)Slider_StealthSpeed.Value);
    }

    private void CheckBox_PlayerGodMode_Click(object sender, RoutedEventArgs e)
    {
        Player.GodMode(CheckBox_PlayerGodMode.IsChecked == true);
        Settings.Player.GodMode = CheckBox_PlayerGodMode.IsChecked == true;
    }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e)
    {
        Player.AntiAFK(CheckBox_AntiAFK.IsChecked == true);
        Settings.Player.AntiAFK = CheckBox_AntiAFK.IsChecked == true;
    }

    private void CheckBox_Invisibility_Click(object sender, RoutedEventArgs e)
    {
        Player.Invisibility(CheckBox_Invisibility.IsChecked == true);
    }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e)
    {
        Player.UndeadOffRadar(CheckBox_UndeadOffRadar.IsChecked == true);
    }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e)
    {
        Player.NoRagdoll(CheckBox_NoRagdoll.IsChecked == true);
        Settings.Player.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true;
    }

    private void CheckBox_NPCIgnore_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_NPCIgnore.IsChecked == true && CheckBox_PoliceIgnore.IsChecked == false)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0x04);
        }
        else if (CheckBox_NPCIgnore.IsChecked == false && CheckBox_PoliceIgnore.IsChecked == true)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0xC3);
        }
        else if (CheckBox_NPCIgnore.IsChecked == true && CheckBox_PoliceIgnore.IsChecked == true)
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0xC7);
        }
        else
        {
            Memory.Write<byte>(Globals.WorldPTR, Offsets.NPCIgnore, 0x00);
        }
    }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e)
    {
        Player.WantedLevel(0x00);
        Settings.Common.AutoClearWanted = CheckBox_AutoClearWanted.IsChecked == true;
    }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e)
    {
        World.KillNPC(false);
        Settings.Common.AutoKillNPC = CheckBox_AutoKillNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        World.KillNPC(true);
        Settings.Common.AutoKillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e)
    {
        World.KillPolice();
        Settings.Common.AutoKillPolice = CheckBox_AutoKillPolice.IsChecked == true;
    }

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Teleport.ToWaypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Teleport.ToObjective();
    }

    private void Button_FillHealthArmor_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Player.FillHealthArmor();
    }

    private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Player.WantedLevel(0x00);
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Player.Suicide();
    }

    private void Slider_MovingFoward_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Settings.Forward = (float)Slider_MovingFoward.Value;
    }

    private void CheckBox_ProofBullet_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofBullet(CheckBox_ProofBullet.IsChecked == true);
    }

    private void CheckBox_ProofFire_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofFire(CheckBox_ProofFire.IsChecked == true);
    }

    private void CheckBox_ProofCollision_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofCollision(CheckBox_ProofCollision.IsChecked == true);
    }

    private void CheckBox_ProofMelee_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofMelee(CheckBox_ProofMelee.IsChecked == true);
    }

    private void CheckBox_ProofExplosion_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofExplosion(CheckBox_ProofExplosion.IsChecked == true);
    }

    private void CheckBox_ProofSteam_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofSteam(CheckBox_ProofSteam.IsChecked == true);
    }

    private void CheckBox_ProofDrown_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofDrown(CheckBox_ProofDrown.IsChecked == true);
    }

    private void CheckBox_ProofWater_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofWater(CheckBox_ProofWater.IsChecked == true);
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e)
    {
        NoCollisionToggle = false;

        Player.NoCollision(NoCollisionToggle);
        Settings.Player.NoCollision = NoCollisionToggle;
    }
}
