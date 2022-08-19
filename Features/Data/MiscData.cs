namespace GTA5OnlineTools.Features.Data;

public static class MiscData
{
    public struct Blip
    {
        public string Name;
        public int ID;
    }

    public static List<Blip> Blips = new()
    {
        new Blip(){ Name="Yacht", ID=455 },
        new Blip(){ Name="CEO Office", ID=475 },
        new Blip(){ Name="Motorcycle Club House", ID=492 },
        new Blip(){ Name="Weed Farm", ID=496 },
        new Blip(){ Name="Cocaine Lockup", ID=497 },
        new Blip(){ Name="Document Forgery Office", ID=498 },
        new Blip(){ Name="Methamphetamine Lab", ID=499 },
        new Blip(){ Name="Counterfeit Cash Factory", ID=500 },
        new Blip(){ Name="Bunker", ID=557 },
        new Blip(){ Name="Hangar", ID=569 },
        new Blip(){ Name="Facility", ID=590 },
        new Blip(){ Name="Nightclub", ID=614 },
        new Blip(){ Name="Arcade", ID=740 },
        new Blip(){ Name="Kosatka", ID=760 },
        new Blip(){ Name="Vehicle warehouse", ID=777 },
    };

    public struct Session
    {
        public string Name;
        public int ID;
    }

    public static List<Session> Sessions = new()
    {
        new Session(){ Name="Leave online mode", ID=-1 },
        new Session(){ Name="Join Public", ID=0 },
        new Session(){ Name="New Public", ID=1 },
        new Session(){ Name="Crew", ID=2 },
        new Session(){ Name="帮会战局", ID=3 },
        new Session(){ Name="加入好友", ID=9 },
        new Session(){ Name="私人好友战局", ID=6 },
        new Session(){ Name="Solo", ID=10 },
        new Session(){ Name="Invite", ID=11 },
        new Session(){ Name="加入帮会伙伴", ID=12 },
    };

    public struct RPxN
    {
        public string Name;
        public float ID;
    }

    public static List<RPxN> RPxNs = new()
    {
        new RPxN(){ Name="RPx1", ID=1.0f },
        new RPxN(){ Name="RPx2", ID=2.0f },
        new RPxN(){ Name="RPx5", ID=5.0f },
        new RPxN(){ Name="RPx10", ID=10.0f },
        new RPxN(){ Name="RPx20", ID=20.0f },
        new RPxN(){ Name="RPx50", ID=50.0f },
        new RPxN(){ Name="RPx100", ID=100.0f },
        new RPxN(){ Name="RPx500", ID=500.0f },
        new RPxN(){ Name="RPx1000", ID=1000.0f },
    };

    public struct REPxN
    {
        public string Name;
        public float ID;
    }

    public static List<REPxN> REPxNs = new()
    {
        new REPxN(){ Name="REPx1", ID=1.0f },
        new REPxN(){ Name="REPx2", ID=2.0f },
        new REPxN(){ Name="REPx5", ID=5.0f },
        new REPxN(){ Name="REPx10", ID=10.0f },
        new REPxN(){ Name="REPx20", ID=20.0f },
        new REPxN(){ Name="REPx50", ID=50.0f },
        new REPxN(){ Name="REPx100", ID=100.0f },
        new REPxN(){ Name="REPx500", ID=500.0f },
        new REPxN(){ Name="REPx1000", ID=1000.0f },
    };

    public struct CEOCargo
    {
        public string Name;
        public int ID;
    }

    // Set Global_1946791 to 1, otherwise you'll see regular crates.
    // Set Global_1946637 to one of these: 2, 4, 6, 7, 8, 9.
    // Now you'll see the unique cargo in your terrorbyte.

    public static List<CEOCargo> CEOCargos = new()
    {
        new CEOCargo(){ Name="Medical supply", ID=0 },
        new CEOCargo(){ Name="Tobacco and alcohol", ID=1 },
        new CEOCargo(){ Name="Antique Artwork (Gorgeous Easter Eggs)", ID=2 },
        new CEOCargo(){ Name="electronic product", ID=3 },
        new CEOCargo(){ Name="Weapons and Ammunition (Gold Vulcan Machine Gun)", ID=4 },
        new CEOCargo(){ Name="LSD", ID=5 },
        new CEOCargo(){ Name="Gem (a large diamond)", ID=6 },
        new CEOCargo(){ Name="animal material (rare fur)", ID=7 },
        new CEOCargo(){ Name="Imitations (movie rolls)", ID=8 },
        new CEOCargo(){ Name="Jewelry (rare pocket watch)", ID=9 },
        new CEOCargo(){ Name="silver nugget", ID=10 },
    };

    public struct MerryWeatherService
    {
        public string Name;
        public int ID;
    }

    public static List<MerryWeatherService> MerryWeatherServices = new()
    {
        new MerryWeatherService(){ Name="Request Ammo Drop", ID=874 },
        new MerryWeatherService(){ Name="Request Ballistic Equipment", ID=884 },
        new MerryWeatherService(){ Name="request bull shark testosterone", ID=882 },
        new MerryWeatherService(){ Name="Request Boat Pickup", ID=875 },
        new MerryWeatherService(){ Name="Request Helicopter Pickup", ID=876 },
    };

    public struct LocalWeather
    {
        public string Name;
        public int ID;
    }

    public static List<LocalWeather> LocalWeathers = new()
    {
        new LocalWeather(){ Name="Default", ID=-1 },
        new LocalWeather(){ Name="Extra Sunny", ID=0 },
        new LocalWeather(){ Name="Clear", ID=1 },
        new LocalWeather(){ Name="Clouds", ID=2 },
        new LocalWeather(){ Name="Smog", ID=3 },
        new LocalWeather(){ Name="Foggy", ID=4 },
        new LocalWeather(){ Name="Overcast", ID=5 },
        new LocalWeather(){ Name="Rain", ID=6 },
        new LocalWeather(){ Name="Thunder", ID=7 },
        new LocalWeather(){ Name="Light Rain", ID=8 },
        new LocalWeather(){ Name="Smoggy Light Rain", ID=9 },
        new LocalWeather(){ Name="Very Light Snow", ID=10 },
        new LocalWeather(){ Name="Windy Snow", ID=11 },
        new LocalWeather(){ Name="Light Snow", ID=12 },
        new LocalWeather(){ Name="XMAS", ID=13 },
        new LocalWeather(){ Name="Halloween", ID=14 },
		new LocalWeather(){ Name="Black screen", ID=15 },
    };

    public struct ImpactExplosion
    {
        public string Name;
        public int ID;
    }

    public static List<ImpactExplosion> ImpactExplosions = new()
    {
        new ImpactExplosion(){ Name="DEFAULT BULLETS", ID=-1 },
        new ImpactExplosion(){ Name="GRENADE", ID=0 },
        new ImpactExplosion(){ Name="GRENADELAUNCHER", ID=1 },
        new ImpactExplosion(){ Name="STICKYBOMB", ID=2 },
        new ImpactExplosion(){ Name="MOLOTOV", ID=3 },
        new ImpactExplosion(){ Name="ROCKET", ID=4 },
        new ImpactExplosion(){ Name="TANKSHELL", ID=5 },
        new ImpactExplosion(){ Name="HI_OCTANE", ID=6 },
        new ImpactExplosion(){ Name="CAR", ID=7 },
        new ImpactExplosion(){ Name="PLANE", ID=8 },
        new ImpactExplosion(){ Name="PETROL_PUMP", ID=9 },
        new ImpactExplosion(){ Name="BIKE", ID=10 },
        new ImpactExplosion(){ Name="DIR_STEAM", ID=11 },
        new ImpactExplosion(){ Name="DIR_FLAME", ID=12 },
        new ImpactExplosion(){ Name="DIR_WATER_HYDRANT", ID=13 },
        new ImpactExplosion(){ Name="DIR_GAS_CANISTER", ID=14 },
        new ImpactExplosion(){ Name="BOAT", ID=15 },
        new ImpactExplosion(){ Name="SHIP_DESTROY", ID=16 },
        new ImpactExplosion(){ Name="TRUCK", ID=17 },
        new ImpactExplosion(){ Name="MK2_EXPLOSIVE_BULLETS", ID=18 },
        new ImpactExplosion(){ Name="SMOKEGRENADELAUNCHER", ID=19 },
        new ImpactExplosion(){ Name="SMOKEGRENADE", ID=20 },
        new ImpactExplosion(){ Name="BZGAS", ID=21 },
        new ImpactExplosion(){ Name="FLARE", ID=22 },
        new ImpactExplosion(){ Name="GAS_CANISTER", ID=23 },
        new ImpactExplosion(){ Name="EXTINGUISHER", ID=24 },
        new ImpactExplosion(){ Name="PROGRAMMABLEAR", ID=25 },
        new ImpactExplosion(){ Name="TRAIN", ID=26 },
        new ImpactExplosion(){ Name="BARREL", ID=27 },
        new ImpactExplosion(){ Name="PROPANE", ID=28 },
        new ImpactExplosion(){ Name="BLIMP", ID=29 },
        new ImpactExplosion(){ Name="DIR_FLAME_EXPLODE ", ID=30 },
        new ImpactExplosion(){ Name="TANKER", ID=31 },
        new ImpactExplosion(){ Name="PLANE_ROCKET", ID=32 },
        new ImpactExplosion(){ Name="VEHICLE_BULLET", ID=33 },
        new ImpactExplosion(){ Name="GAS_TANK", ID=34 },
        new ImpactExplosion(){ Name="BIRD_CRAP", ID=35 },
        new ImpactExplosion(){ Name="RAILGUN", ID=36 },
        new ImpactExplosion(){ Name="BLIMP2", ID=37 },
        new ImpactExplosion(){ Name="FIREWORK", ID=38 },
        new ImpactExplosion(){ Name="SNOWBALL", ID=39 },
        new ImpactExplosion(){ Name="PROXMINE", ID=40 },
        new ImpactExplosion(){ Name="VALKYRIE_CANNON", ID=41 },
        new ImpactExplosion(){ Name="AIR_DEFENCE", ID=42 },
        new ImpactExplosion(){ Name="PIPEBOMB", ID=43 },
        new ImpactExplosion(){ Name="VEHICLEMINE", ID=44 },
        new ImpactExplosion(){ Name="EXPLOSIVEAMMO", ID=45 },
        new ImpactExplosion(){ Name="APCSHELL", ID=46 },
        new ImpactExplosion(){ Name="BOMB_CLUSTER", ID=47 },
        new ImpactExplosion(){ Name="BOMB_GAS", ID=48 },
        new ImpactExplosion(){ Name="BOMB_INCENDIARY", ID=49 },
        new ImpactExplosion(){ Name="BOMB_STANDARD", ID=50 },
        new ImpactExplosion(){ Name="TORPEDO", ID=51 },
        new ImpactExplosion(){ Name="TORPEDO_UNDERWATER", ID=52 },
        new ImpactExplosion(){ Name="BOMBUSHKA_CANNON", ID=53 },
        new ImpactExplosion(){ Name="BOMB_CLUSTER_SECONDARY", ID=54 },
        new ImpactExplosion(){ Name="HUNTER_BARRAGE", ID=55 },
        new ImpactExplosion(){ Name="HUNTER_CANNON", ID=56 },
        new ImpactExplosion(){ Name="ROGUE_CANNON", ID=57 },
        new ImpactExplosion(){ Name="MINE_UNDERWATER", ID=58 },
        new ImpactExplosion(){ Name="ORBITAL_CANNON", ID=59 },
        new ImpactExplosion(){ Name="BOMB_STANDARD_WIDE", ID=60 },
        new ImpactExplosion(){ Name="EXPLOSIVEAMMO_SHOTGUN", ID=61 },
        new ImpactExplosion(){ Name="OPPRESSOR2_CANNON", ID=62 },
        new ImpactExplosion(){ Name="MORTAR_KINETIC", ID=63 },
        new ImpactExplosion(){ Name="VEHICLEMINE_KINETIC", ID=64 },
        new ImpactExplosion(){ Name="VEHICLEMINE_EMP", ID=65 },
        new ImpactExplosion(){ Name="VEHICLEMINE_SPIKE", ID=66 },
        new ImpactExplosion(){ Name="VEHICLEMINE_SLICK", ID=67 },
        new ImpactExplosion(){ Name="VEHICLEMINE_TAR", ID=68 },
        new ImpactExplosion(){ Name="SCRIPT_DRONE", ID=69 },
        new ImpactExplosion(){ Name="RAYGUN", ID=70 },
        new ImpactExplosion(){ Name="BURIEDMINE", ID=71 },
        new ImpactExplosion(){ Name="SCRIPT_MISSILE", ID=72 },
		// Add RC Tank
		new ImpactExplosion(){ Name="RCTANK_ROCKET", ID=73 },
		new ImpactExplosion(){ Name="BOMB_WATER", ID=74 },
		new ImpactExplosion(){ Name="BOMB_WATER_SECONDARY", ID=75 },
		new ImpactExplosion(){ Name="EXTINGUISHER", ID=76 },
		new ImpactExplosion(){ Name="EXTINGUISHER", ID=77 },
		new ImpactExplosion(){ Name="EXTINGUISHER", ID=78 },
		new ImpactExplosion(){ Name="EXTINGUISHER", ID=79 },
		new ImpactExplosion(){ Name="EXTINGUISHER", ID=80 },
		new ImpactExplosion(){ Name="SCRIPT_MISSILE_LARGE", ID=81 },
        new ImpactExplosion(){ Name="SUBMARINE_BIG", ID=82 },
		// Add EMP Launcher
		new ImpactExplosion(){ Name="EMPLAUNCHER_EMP", ID=83 },
        new ImpactExplosion(){ Name="SPOOF EXPLOSION (NO DAMAGE)", ID=99 },
    };
}
