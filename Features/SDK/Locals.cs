﻿using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Locals
{
    public const string Casino_Script_Name = "fm_mission_controller";
    public const int Take_Casino_Script_Index = 19652 + 2685;
    public const int Casino_Mission_Life_Script_Index = 26077 + 1322 + 1;
    public const int Vault_Door_Script_Index = 10068 + 7;
    public const int Vault_Door_Total_Script_Index = 10068 + 37;
    public const int Casino_Fingerprint_Script_Index = (0x68CA0 - 0x8) / 8;
    public const int Casino_Keypad_Script_Index = (0x6ADD0 - 0x8) / 8;

    ////////////////////////////////////////////////////////////////////////

    public const string Cayo_Script_Name = "fm_mission_controller_2020";
    public const int Take_Cayo_Script_Index = 40004 + 1392 + 53;
    public const int Cayo_Mission_Life_Script_Index = 43059 + 865 + 1;
    public const int Plasma_Cutter_Progress_Script_Index = 28269 + 3;
    public const int Plasma_Cutter_Heat_Script_Index = 28269 + 4;
    public const int Cayo_Fingerprint_Script_Index = (0x2EBD0 - 0x8) / 8;
    public const int Cayo_Sewer_Cuts_Script_Index = 0x34CE0 / 8;

    public static long LocalAddress(string name)
    {
        for (int i = 0; i < 54; i++)
        {
            var pointer = Memory.Read<long>(Globals.LocalScriptsPTR);
            pointer = Memory.Read<long>(pointer + i * 0x8);

            var str = Memory.ReadString(pointer + 0xD4, null, name.Length + 1);
            if (str.ToLower() == name.ToLower())
                return pointer + 0xB0;
        }

        return 0;
    }

    public static long LocalAddress(string name, int index)
    {
        for (int i = 0; i < 54; i++)
        {
            var pointer = Memory.Read<long>(Globals.LocalScriptsPTR);
            pointer = Memory.Read<long>(pointer + i * 0x8);

            var address = Memory.Read<long>(pointer + 0xB0);
            var str = Memory.ReadString(pointer + 0xD0, null, name.Length + 1);
            if (str == name && pointer != 0)
                return address + index * 8;
        }

        return 0;
    }

    public static T ReadLocalAddress<T>(string name, int index) where T : struct
    {
        return Memory.Read<T>(LocalAddress(name, index));
    }

    public static void WriteLocalAddress<T>(string name, int index, T value) where T : struct
    {
        Memory.Write<T>(LocalAddress(name, index), value);
    }
}
