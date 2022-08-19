using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Player
{
    /// <summary>
    /// 无敌模式
    /// </summary>
    public static void GodMode(bool isEnable)
    {
        if (isEnable)
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.GodMode, 0x01);
        else
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.GodMode, 0x00);
    }

    /// <summary>
    /// 玩家通缉等级，0x00,0x01,0x02,0x03,0x04,0x05
    /// </summary>
    public static void WantedLevel(byte level)
    {
        Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Wanted, level);
    }

    /// <summary>
    /// 挂机防踢
    /// </summary>
    /// <param name="isEnable"></param>
    public static void AntiAFK(bool isEnable)
    {
        /*
         idleKick: {('262145', '87')}    GLOBAL IDLEKICK_WARNING1 
         idleKick: {('262145', '88')}    GLOBAL IDLEKICK_WARNING2 
         idleKick: {('262145', '89')}    GLOBAL IDLEKICK_WARNING3 
         idleKick: {('262145', '90')}    GLOBAL IDLEKICK_KICK 
         idleKick: {('262145', '8248')}    GLOBAL ConstrainedKick_Warning1 
         idleKick: {('262145', '8249')}    GLOBAL ConstrainedKick_Warning2 
         idleKick: {('262145', '8250')}    GLOBAL ConstrainedKick_Warning3 
         idleKick: {('262145', '8251')}    GLOBAL ConstrainedKick_Kick 
         timeStoodIdle: {('1648034', '1156')}    GLOBAL time in ms  
         idleKick: {('1648034', '1172')}    GLOBAL
         */

        // joaat("weapon_minigun");
        Hacks.WriteGA<int>(262145 + 87, isEnable ? 99999999 : 120000);        // 120000     GLOBAL IDLEKICK_WARNING1 
        Hacks.WriteGA<int>(262145 + 88, isEnable ? 99999999 : 300000);        // 300000     GLOBAL IDLEKICK_WARNING2 
        Hacks.WriteGA<int>(262145 + 89, isEnable ? 99999999 : 600000);        // 600000     GLOBAL IDLEKICK_WARNING3
        Hacks.WriteGA<int>(262145 + 90, isEnable ? 99999999 : 900000);        // 900000     GLOBAL IDLEKICK_KICK 
        // 742014
        Hacks.WriteGA<int>(262145 + 8248, isEnable ? 2000000000 : 30000);     // 30000      GLOBAL ConstrainedKick_Warning1
        Hacks.WriteGA<int>(262145 + 8249, isEnable ? 2000000000 : 60000);     // 60000      GLOBAL ConstrainedKick_Warning2
        Hacks.WriteGA<int>(262145 + 8250, isEnable ? 2000000000 : 90000);     // 90000      GLOBAL ConstrainedKick_Warning3
        Hacks.WriteGA<int>(262145 + 8251, isEnable ? 2000000000 : 120000);    // 120000     GLOBAL ConstrainedKick_Kick
    }

    /// <summary>
    /// 无布娃娃
    /// </summary>
    public static void NoRagdoll(bool isEnable)
    {
        if (isEnable)
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.NoRagdoll, 0x01);
        else
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.NoRagdoll, 0x20);
    }

    /// <summary>
    /// 无碰撞体积
    /// </summary>
    public static void NoCollision(bool isEnable)
    {
        if (isEnable)
            Memory.Write(Globals.WorldPTR, Offsets.Player.NoCollision, -1.0f);
        else
            Memory.Write(Globals.WorldPTR, Offsets.Player.NoCollision, 0.25f);
    }

    /// <summary>
    /// 角色防子弹
    /// </summary>
    public static void ProofBullet(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 4)) : (uint)(proof & ~(1 << 4));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防火烧
    /// </summary>
    public static void ProofFire(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 5)) : (uint)(proof & ~(1 << 5));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防撞击
    /// </summary>
    public static void ProofCollision(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 6)) : (uint)(proof & ~(1 << 6));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防近战
    /// </summary>
    public static void ProofMelee(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 7)) : (uint)(proof & ~(1 << 7));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防无敌
    /// </summary>
    public static void ProofGod(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 8)) : (uint)(proof & ~(1 << 8));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防爆炸
    /// </summary>
    public static void ProofExplosion(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 11)) : (uint)(proof & ~(1 << 11));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防蒸汽
    /// </summary>
    public static void ProofSteam(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 15)) : (uint)(proof & ~(1 << 15));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防溺水
    /// </summary>
    public static void ProofDrown(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 16)) : (uint)(proof & ~(1 << 16));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色防海水
    /// </summary>
    public static void ProofWater(bool isEnable)
    {
        var proof = Memory.Read<uint>(Globals.WorldPTR, Offsets.PlayerProof);
        proof = isEnable ? (uint)(proof | (1 << 24)) : (uint)(proof & ~(1 << 24));
        Memory.Write<uint>(Globals.WorldPTR, Offsets.PlayerProof, proof);
    }

    /// <summary>
    /// 角色隐形（虚假）
    /// </summary>
    public static void Invisibility(bool isEnable)
    {
        if (isEnable)
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Invisibility, 0x01);
        else
            Memory.Write<byte>(Globals.WorldPTR, Offsets.Player.Invisibility, 0x27);
    }

    /// <summary>
    /// 补满血量和护甲
    /// </summary>
    public static void FillHealthArmor()
    {
        Memory.Write(Globals.WorldPTR, Offsets.Player.Health, 328.0f);
        Memory.Write(Globals.WorldPTR, Offsets.Player.MaxHealth, 328.0f);
        Memory.Write(Globals.WorldPTR, Offsets.Player.Armor, 50.0f);
    }

    /// <summary>
    /// 玩家自杀
    /// </summary>
    public static void Suicide()
    {
        Memory.Write(Globals.WorldPTR, Offsets.Player.Health, 1.0f);
    }

    /// <summary>
    /// 雷达影踪（最大生命值为0）
    /// </summary>
    public static void UndeadOffRadar(bool isEnable)
    {
        if (isEnable)
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.MaxHealth, 0.0f);
        else
            Memory.Write<float>(Globals.WorldPTR, Offsets.Player.MaxHealth, 328.0f);
    }

    /// <summary>
    /// 永不通缉
    /// </summary>
    public static void WantedCanChange(bool isEnable)
    {
        if (isEnable)
            Memory.Write<float>(Globals.WorldPTR, Offsets.WantedCanChange, 1.0f);
        else
            Memory.Write<float>(Globals.WorldPTR, Offsets.WantedCanChange, 0.0f);
    }
}
