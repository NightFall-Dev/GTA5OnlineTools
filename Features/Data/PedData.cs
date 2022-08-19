namespace GTA5OnlineTools.Features.Data;

public static class PedData
{
    public struct PedInfo
    {
        public string Name;
        public string DisplayName;
        public long Hash;
    }

    /// <summary>
    /// Ped模型列表
    /// </summary>
    public static List<PedInfo> PedDataClass = new()
    {
        new PedInfo(){ Name="MP_M_Freemode_01", DisplayName="默认线上角色", Hash=0x705E61F2 },
        new PedInfo(){ Name="CSB_MP_Agent14", DisplayName="14号探员", Hash=0x6DBBFC8B },
        new PedInfo(){ Name="A_C_Chop", DisplayName="动物 小查", Hash=0x14EC17EA },
        new PedInfo(){ Name="Player_Zero", DisplayName="麦克", Hash=0xD7114C9 },
        new PedInfo(){ Name="Player_One", DisplayName="富兰克林", Hash=0x9B22DBAF },
        new PedInfo(){ Name="P_Franklin_02", DisplayName="富兰克林2", Hash=0xAF10BD56 },
        new PedInfo(){ Name="Player_Two", DisplayName="崔佛", Hash=0x9B810FA2 },
        new PedInfo(){ Name="CS_LesterCrest", DisplayName="莱斯特", Hash=0xB594F5C3 },
        new PedInfo(){ Name="CS_LesterCrest_2", DisplayName="莱斯特2", Hash=0xB63911D },
        new PedInfo(){ Name="CS_LesterCrest_3", DisplayName="莱斯特3", Hash=0x1D953580 },
        new PedInfo(){ Name="CSB_Agatha", DisplayName="贝克女士", Hash=0x2D145A18 },
        new PedInfo(){ Name="CSB_Rashcosvki", DisplayName="越狱任务光头", Hash=0x188099A9 },
        new PedInfo(){ Name="IG_LamarDavis_02", DisplayName="拉玛2", Hash=0x1924A05E },
        new PedInfo(){ Name="IG_NervousRon", DisplayName="小罗", Hash=0xBD006AF1 },
        new PedInfo(){ Name="IG_TaoCheng", DisplayName="陈陶", Hash=0xDC5C5EA5 },
        new PedInfo(){ Name="IG_TaoCheng2", DisplayName="陈陶2", Hash=0x59C62B90 },
        new PedInfo(){ Name="CS_MartinMadrazo", DisplayName="马丁", Hash=0x43595670 },
        new PedInfo(){ Name="CSB_Mimi", DisplayName="米米", Hash=0x86C1FFE8 },
        new PedInfo(){ Name="IG_JimmyDiSanto", DisplayName="吉米", Hash=0x570462B9 },
        new PedInfo(){ Name="IG_JimmyDiSanto2", DisplayName="吉米2", Hash=0x842DC2D6 },
        new PedInfo(){ Name="IG_SiemonYetarian", DisplayName="西门", Hash=0x4C7B2F05 },
    };
}
