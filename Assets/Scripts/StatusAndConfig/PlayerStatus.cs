using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatus {

    public const double MAX_HEALTH = 10;
    public static double health = 10;
    public static double attack = 1;
    public static double defense = 0;

    public static Dictionary<string, string> item = new Dictionary<string, string>();
    public static string[] blockStatement = {"isBlockMeleeAttack", "isBlockFuAttack", "isBlockMovement", "isBlockItemUsage"};

    public static Dictionary<string, float> characterAttributes = new Dictionary<string, float>()
    {
        ["Max_health"] = 10,
        ["Health"] = 10,
        ["Mana"] = 10,
        ["Power"] = 1,
        ["Defense"] = 0,
        ["Speed"] = 0,
        ["Intelligence"] = 0,
        ["Magic"] = 0,
        ["CriticalChance"] = 0,
        ["Luck"] = 0,
        ["Vitality"] = 0,
        ["Experience"] = 0,
        ["Level"] = 0,
    };

    public static Dictionary<string, float> fiveElementsProperty = new Dictionary<string, float>()
    {
        ["METAL"] = 0,
        ["WOOD"] = 0,
        ["WATER"] = 0,
        ["FIRE"] = 0,
        ["EARTH"] = 0,
    };
}
