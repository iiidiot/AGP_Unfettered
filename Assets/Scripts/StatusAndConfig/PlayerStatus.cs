using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatus {

    public static double MaxHealth = 10;
    public static double MaxMana = 10;
    public static double Health = 10;
    public static double Mana = 10;
    public static double Power = 1;
    public static double Defense = 0;
    public static double Speed = 0;
    public static double Intelligence = 0;
    public static double CriticalChance = 0;
    public static double Luck = 0;
    public static double Vitality = 0;
    public static double Experience = 0;
    public static double Level = 0;

    // five elements attributes
    public static double Metal = 0;
    public static double Wood = 0;
    public static double Water = 0;
    public static double Fire = 0;
    public static double Earth = 0;


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
        ["CriticalChance"] = 0,
        ["Luck"] = 0,
        ["Vitality"] = 0,
        ["Experience"] = 0,
        ["Level"] = 0,
    };
    public static Dictionary<string, double> getfiveElementsAttribute()
    {
        Dictionary<string, double> attribute = new Dictionary<string, double>()
		{
			["Metal"] = Metal,
			["Wood"] = Wood,
			["Water"] = Water,	
			["Fire"] = Fire,	
			["Earth"] = Earth,	
    	};

		return attribute;
    }
    public static Dictionary<string, double> getItemRelatedAttribute()
    {
        Dictionary<string, double> attribute = new Dictionary<string, double>()
		{
			["Max_health"] = Health,
			["Max_Mana"] = Mana,
			["Power"] = Power,
			["Defense"] = Defense,
			["Speed"] = Speed,
			["Intelligence"] = Intelligence,
			["CriticalChance"] = CriticalChance,
			["Luck"] = Luck,
			["Metal"] = Metal,
			["Wood"] = Wood,
			["Water"] = Water,	
			["Fire"] = Fire,	
			["Earth"] = Earth,	
    	};

		return attribute;
    }
}
