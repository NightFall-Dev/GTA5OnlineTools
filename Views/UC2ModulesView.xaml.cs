using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Modules.Windows;
using GTA5OnlineTools.Modules.Windows.ExternalMenu;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// UC2ModulesView.xaml 的交互逻辑
/// </summary>
public partial class UC2ModulesView : UserControl
{
    private ExternalMenuWindow ExternalMenuWindow = null;

    private GTAHaxWindow GTAHaxWindow = null;
    private OutfitsWindow OutfitsWindow = null;
    private HeistCutWindow HeistCutWindow = null;
    private StatScriptsWindow StatAutoScriptsWindow = null;
    private HeistPrepsWindow HeistPrepsWindow = null;
    private CasinoHackWindow CasinoHackWindow = null;
    private BlcokMsgWindow BlcokMsgWindow = null;

    private BigBaseV2Window BigBaseV2Window = null;

    public RelayCommand<string> ModelsClickCommand { get; private set; }

    private const string HintMsg = "未发现GTA5进程，请先运行GTA5游戏";

    public UC2ModulesView()
    {
        InitializeComponent();
        this.DataContext = this;

        ModelsClickCommand = new(ModelsClick);
    }

    private void ModelsClick(string obj)
    {
        AudioUtil.ClickSound();

        if (ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
        {
            switch (obj)
            {
                case "ExternalMenu":
                    ExternalMenuClick();
                    break;
                case "GTAHax":
                    GTAHaxClick();
                    break;
                case "Outfits":
                    OutfitsClick();
                    break;
                case "HeistCut":
                    HeistCutClick();
                    break;
                case "StatAutoScripts":
                    StatAutoScriptsClick();
                    break;
                case "HeistPreps":
                    HeistPrepsClick();
                    break;
                case "CasinoHack":
                    CasinoHackClick();
                    break;
                case "BlcokMsg":
                    BlcokMsgClick();
                    break;
                case "BigBaseV2":
                    BigBaseV2Click();
                    break;
            }
        }
        else
        {
            MsgBoxUtil.Error(HintMsg);
        }
    }

    private void ExternalMenuClick()
    {
        if (ExternalMenuWindow == null)
        {
            ExternalMenuWindow = new ExternalMenuWindow();
            ExternalMenuWindow.Show();
        }
        else
        {
            if (ExternalMenuWindow.IsVisible)
            {
                if (!ExternalMenuWindow.Topmost)
                {
                    ExternalMenuWindow.Topmost = true;
                    ExternalMenuWindow.Topmost = false;
                }

                ExternalMenuWindow.WindowState = WindowState.Normal;
            }
            else
            {
                ExternalMenuWindow = null;
                ExternalMenuWindow = new ExternalMenuWindow();
                ExternalMenuWindow.Show();
            }
        }
    }

    private void HeistPrepsClick()
    {
        if (HeistPrepsWindow == null)
        {
            HeistPrepsWindow = new HeistPrepsWindow();
            HeistPrepsWindow.Show();
        }
        else
        {
            if (HeistPrepsWindow.IsVisible)
            {
                HeistPrepsWindow.Topmost = true;
                HeistPrepsWindow.Topmost = false;
                HeistPrepsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                HeistPrepsWindow = null;
                HeistPrepsWindow = new HeistPrepsWindow();
                HeistPrepsWindow.Show();
            }
        }
    }

    private void StatAutoScriptsClick()
    {
        if (StatAutoScriptsWindow == null)
        {
            StatAutoScriptsWindow = new StatScriptsWindow();
            StatAutoScriptsWindow.Show();
        }
        else
        {
            if (StatAutoScriptsWindow.IsVisible)
            {
                StatAutoScriptsWindow.Topmost = true;
                StatAutoScriptsWindow.Topmost = false;
                StatAutoScriptsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                StatAutoScriptsWindow = null;
                StatAutoScriptsWindow = new StatScriptsWindow();
                StatAutoScriptsWindow.Show();
            }
        }
    }

    private void HeistCutClick()
    {
        if (HeistCutWindow == null)
        {
            HeistCutWindow = new HeistCutWindow();
            HeistCutWindow.Show();
        }
        else
        {
            if (HeistCutWindow.IsVisible)
            {
                HeistCutWindow.Topmost = true;
                HeistCutWindow.Topmost = false;
                HeistCutWindow.WindowState = WindowState.Normal;
            }
            else
            {
                HeistCutWindow = null;
                HeistCutWindow = new HeistCutWindow();
                HeistCutWindow.Show();
            }
        }
    }

    private void OutfitsClick()
    {
        if (OutfitsWindow == null)
        {
            OutfitsWindow = new OutfitsWindow();
            OutfitsWindow.Show();
        }
        else
        {
            if (OutfitsWindow.IsVisible)
            {
                OutfitsWindow.Topmost = true;
                OutfitsWindow.Topmost = false;
                OutfitsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                OutfitsWindow = null;
                OutfitsWindow = new OutfitsWindow();
                OutfitsWindow.Show();
            }
        }
    }

    private void GTAHaxClick()
    {
        if (GTAHaxWindow == null)
        {
            GTAHaxWindow = new GTAHaxWindow();
            GTAHaxWindow.Show();
        }
        else
        {
            if (GTAHaxWindow.IsVisible)
            {
                GTAHaxWindow.Topmost = true;
                GTAHaxWindow.Topmost = false;
                GTAHaxWindow.WindowState = WindowState.Normal;
            }
            else
            {
                GTAHaxWindow = null;
                GTAHaxWindow = new GTAHaxWindow();
                GTAHaxWindow.Show();
            }
        }
    }

    private void CasinoHackClick()
    {
        if (CasinoHackWindow == null)
        {
            CasinoHackWindow = new CasinoHackWindow();
            CasinoHackWindow.Show();
        }
        else
        {
            if (CasinoHackWindow.IsVisible)
            {
                CasinoHackWindow.Topmost = true;
                CasinoHackWindow.Topmost = false;
                CasinoHackWindow.WindowState = WindowState.Normal;
            }
            else
            {
                CasinoHackWindow = null;
                CasinoHackWindow = new CasinoHackWindow();
                CasinoHackWindow.Show();
            }
        }
    }

    private void BlcokMsgClick()
    {
        if (BlcokMsgWindow == null)
        {
            BlcokMsgWindow = new BlcokMsgWindow();
            BlcokMsgWindow.Show();
        }
        else
        {
            if (BlcokMsgWindow.IsVisible)
            {
                BlcokMsgWindow.Topmost = true;
                BlcokMsgWindow.Topmost = false;
                BlcokMsgWindow.WindowState = WindowState.Normal;
            }
            else
            {
                BlcokMsgWindow = null;
                BlcokMsgWindow = new BlcokMsgWindow();
                BlcokMsgWindow.Show();
            }
        }
    }

    private void BigBaseV2Click()
    {
        if (BigBaseV2Window == null)
        {
            BigBaseV2Window = new BigBaseV2Window();
            BigBaseV2Window.Show();
        }
        else
        {
            if (BigBaseV2Window.IsVisible)
            {
                BigBaseV2Window.Topmost = true;
                BigBaseV2Window.Topmost = false;
                BigBaseV2Window.WindowState = WindowState.Normal;
            }
            else
            {
                BigBaseV2Window = null;
                BigBaseV2Window = new BigBaseV2Window();
                BigBaseV2Window.Show();
            }
        }
    }
}
