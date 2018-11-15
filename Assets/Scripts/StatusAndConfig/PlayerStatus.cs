﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatus {

    public static double MaxHealth = 10;
    public static double HealthRegeneration = 10;
    public static double Health = 10;
    public static double Attack = 10;
     public static double Defense = 0;
    public static double CriticalDamage = 1;
    public static double MovingSpeed = 0;
    public static double CriticalChance = 0;
    public static double Shield = 0;
    public static double FuCritical = 0;
    public static double Spirit = 0;
    public static double Physique = 0;
    public static double Power = 0;
    public static double InternalPower = 0;
    public static double Agility = 0;
    public static double Calm = 0;
    public static double Strength = 0;
    public static double Stamina = 0;


    public static double Experience = 0;
    public static double Level = 0;

    // five elements attributes
    public static double MetalAttack = 0;
	public static double WoodAttack = 0;
	public static double WaterAttack = 0;
	public static double FireAttack = 0;
	public static double EarthAttack = 0;
	public static double MetalDefense = 0;
    public static double WoodDefense = 0;
	public static double WaterDefense = 0;
	public static double FireDefense = 0;
	public static double EarthDefense = 0;


    public static Dictionary<string, string> item = new Dictionary<string, string>();
    public static string[] blockStatement = {"isBlockMeleeAttack", "isBlockFuAttack", "isBlockMovement", "isBlockItemUsage"};

    public static Dictionary<string, double> GetPanelAttributes()
    {
        Dictionary<string, double> attribute = new Dictionary<string, double>()
        {
            ["Physique"] = Physique,
            ["Strength"] = Strength,
            ["Stamina"] = Stamina,
            ["InternalPower"] = InternalPower,
            ["Clam"] = Calm,
            ["Agility"] = Agility,
        };
        return attribute;
    } 
    
    public static Dictionary<string, double> GetFiveElementsAttribute()
    {
        Dictionary<string, double> attribute = new Dictionary<string, double>()
		{
			["MetalAttack"] = MetalAttack,
			["WoodAttack"] = WoodAttack,
			["WaterAttack"] = WaterAttack,	
			["FireAttack"] = FireAttack,	
			["EarthAttack"] = EarthAttack,	
            ["MetalDefense"] = MetalDefense,
			["WoodDefense"] = WoodDefense,
			["WaterDefense"] = WaterDefense,	
			["FireDefense"] = FireDefense,	
			["EarthDefense"] = EarthDefense,	
    	};
		return attribute;
    }
    // public static Dictionary<string, double> getItemRelatedAttribute()
    // {
    //     Dictionary<string, double> attribute = new Dictionary<string, double>()
	// 	{
	// 		["Max_health"] = Health,
	// 		["Max_Mana"] = Mana,
	// 		["Power"] = Power,
	// 		["Defense"] = Defense,
	// 		["Speed"] = Speed,
	// 		["Intelligence"] = Intelligence,
	// 		["CriticalChance"] = CriticalChance,
	// 		["Luck"] = Luck,
	// 		["Metal"] = Metal,
	// 		["Wood"] = Wood,
	// 		["Water"] = Water,	
	// 		["Fire"] = Fire,	
	// 		["Earth"] = Earth,	
    // 	};

	// 	return attribute;
    // }
}
