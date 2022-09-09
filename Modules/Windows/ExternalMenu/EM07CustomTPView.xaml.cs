using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM07CustomTPView.xaml 的交互逻辑
/// </summary>
public partial class EM07CustomTPView : UserControl
{
    public EM07CustomTPView()
    {
        InitializeComponent();

        // 读取自定义传送坐标文件
        try
        {
            using (var streamReader = new StreamReader(FileUtil.CustomTPList_Path))
            {
                List<TeleportData.TeleportInfo> teleportPreviews = JsonUtil.JsonDese<List<TeleportData.TeleportInfo>>(streamReader.ReadToEnd());

                TeleportData.CustomTeleport.Clear();

                foreach (var item in teleportPreviews)
                {
                    TeleportData.CustomTeleport.Add(item);
                }

                TextBox_Result.Text = $"读取自定义传送坐标文件成功 {FileUtil.CustomTPList_Path}";
            }
        }
        catch (Exception ex)
        {
            TextBox_Result.Text = $"读取自定义传送坐标文件失败 {ex.Message}";
        }

        UpdateTpList();

        ListBox_TeleportClass.SelectedIndex = 0;
        ListBox_TeleportInfo.SelectedIndex = 0;
    }

    private void UpdateTpList()
    {
        ListBox_TeleportClass.Items.Clear();

        // 传送列表
        foreach (var item in TeleportData.TeleportDataClass)
        {
            ListBox_TeleportClass.Items.Add(item.ClassName);
        }

        ListBox_TeleportClass.Items.Refresh();
        ListBox_TeleportInfo.Items.Refresh();
    }

    private void ListBox_TeleportClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ListBox_TeleportClass.SelectedIndex;

        if (index != -1)
        {
            ListBox_TeleportInfo.Items.Clear();

            foreach (var item in TeleportData.TeleportDataClass[index].TeleportInfo)
            {
                ListBox_TeleportInfo.Items.Add(item.Name);
            }

            ListBox_TeleportInfo.SelectedIndex = 0;
        }
    }

    private void ListBox_TeleportInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index1 = ListBox_TeleportClass.SelectedIndex;
        int index2 = ListBox_TeleportInfo.SelectedIndex;

        if (index1 != -1 && index2 != -1)
        {
            TempData.TCode = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position;

            if (index1 == 0)
            {
                TextBox_Position_X.IsEnabled = true;
                TextBox_Position_Y.IsEnabled = true;
                TextBox_Position_Z.IsEnabled = true;

                TextBox_Position_Name.IsEnabled = true;

                TextBox_Position_Name.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Name;
                TextBox_Position_X.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position.X.ToString();
                TextBox_Position_Y.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position.Y.ToString();
                TextBox_Position_Z.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position.Z.ToString();
            }
            else
            {
                TextBox_Position_X.IsEnabled = false;
                TextBox_Position_Y.IsEnabled = false;
                TextBox_Position_Z.IsEnabled = false;

                TextBox_Position_Name.IsEnabled = false;

                TextBox_Position_Name.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Name;
                TextBox_Position_X.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position.X.ToString();
                TextBox_Position_Y.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position.Y.ToString();
                TextBox_Position_Z.Text = TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position.Z.ToString();
            }
        }
    }

    private void Button_Teleport_AddCustom_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Vector3 vector3 = Memory.Read<Vector3>(Globals.WorldPTR, Offsets.PlayerPositionX);

        TeleportData.CustomTeleport.Add(new TeleportData.TeleportInfo()
        {
            Name = $"保存点 : {DateTime.Now:yyyyMMdd_HH-mm-ss_ffff}",
            Position = vector3
        });

        UpdateTpList();

        ListBox_TeleportClass.SelectedIndex = 0;
        ListBox_TeleportInfo.SelectedIndex = ListBox_TeleportInfo.Items.Count - 1;

        TextBox_Result.Text = $"增加自定义传送坐标成功";
    }

    private void Button_Teleport_EditCustom_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index1 = ListBox_TeleportClass.SelectedIndex;
        int index2 = ListBox_TeleportInfo.SelectedIndex;

        if (index1 == 0 && index2 != -1)
        {
            TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Position = new Vector3()
            {
                X = Convert.ToSingle(TextBox_Position_X.Text),
                Y = Convert.ToSingle(TextBox_Position_Y.Text),
                Z = Convert.ToSingle(TextBox_Position_Z.Text)
            };

            TeleportData.TeleportDataClass[index1].TeleportInfo[index2].Name = TextBox_Position_Name.Text;

            UpdateTpList();

            ListBox_TeleportClass.SelectedIndex = 0;
            ListBox_TeleportInfo.SelectedIndex = index2; ;

            TextBox_Result.Text = $"修改自定义传送坐标成功";
        }
        else
        {
            TextBox_Result.Text = $"当前选中项为空";
        }
    }

    private void Button_Teleport_DeleteCustom_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index1 = ListBox_TeleportClass.SelectedIndex;
        int index2 = ListBox_TeleportInfo.SelectedIndex;

        if (index1 == 0 && index2 != -1)
        {
            TeleportData.TeleportDataClass[index1].TeleportInfo.Remove(TeleportData.TeleportDataClass[index1].TeleportInfo[index2]);

            UpdateTpList();

            ListBox_TeleportClass.SelectedIndex = 0;
            ListBox_TeleportInfo.SelectedIndex = ListBox_TeleportInfo.Items.Count - 1;

            TextBox_Result.Text = $"删除自定义传送坐标成功";
        }
        else
        {
            TextBox_Result.Text = $"当前选中项为空";
        }
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

    private void ListBox_TeleportInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Button_Teleport_Click(null, null);
    }

    private void Button_Teleport_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Teleport.SetTeleportV3Pos(TempData.TCode);

        TextBox_Result.Text = $"传送到自定义坐标成功";
    }

    private void Button_Teleport_SaveCustom_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            File.WriteAllText(FileUtil.CustomTPList_Path, JsonUtil.JsonSeri(TeleportData.CustomTeleport));

            TextBox_Result.Text = $"保存到自定义传送坐标文件成功 {FileUtil.CustomTPList_Path}";
        }
        catch (Exception ex)
        {
            MsgBoxUtil.Exception(ex);
        }
    }
}
