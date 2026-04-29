using System.Collections.Generic;

public static class BrewingData
{
    // ingredient slots
    public static string Slot1 = "";
    public static string Slot2 = "";
    public static string Slot3 = "";

    public static float QualityPercent = 0;

    public static HashSet<string> DiscoveredPotions = new HashSet<string>();
    public static bool returning = false;
}
