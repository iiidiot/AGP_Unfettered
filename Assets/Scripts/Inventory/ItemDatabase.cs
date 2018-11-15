using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;


public class ItemDatabase : MonoBehaviour {
	private List<ItemObject> database = new List<ItemObject>();
	private JsonData itemData;

	void Start() 
	{
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Inventory/ItemsTemplate.json"));
		ConstructItemDatabase();
	}

	void ConstructItemDatabase()
	{
		for(int i = 0; i < itemData.Count; i++)
		{
			// database.Add(new ItemObject((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["type"],
			// 				(int)itemData[i]["price"], (double)(int)itemData[i]["stats"]["property"]["health"], (double)(int)itemData[i]["stats"]["property"]["mana"], 
			// 				(double)(int)itemData[i]["stats"]["property"]["power"], (double)(int)itemData[i]["stats"]["property"]["defence"], (double)(int)itemData[i]["stats"]["property"]["critical_chance"],
			// 				(double)(int)itemData[i]["stats"]["property"]["speed"], (double)(int)itemData[i]["stats"]["property"]["luck"], (double)(int)itemData[i]["stats"]["property"]["intelligence"],
			// 				(double)(int)itemData[i]["stats"]["property"]["metal"], (double)(int)itemData[i]["stats"]["property"]["wood"], (double)(int)itemData[i]["stats"]["property"]["water"],
			// 				(double)(int)itemData[i]["stats"]["property"]["fire"], (double)(int)itemData[i]["stats"]["property"]["earth"], (double)(int)itemData[i]["stats"]["durability"], 
			// 				itemData[i]["description"].ToString(), (int)itemData[i]["stackable_quantity"], (int)itemData[i]["rarity"], itemData[i]["sprite_path"].ToString(),
			// 				itemData[i]["model_path"].ToString()));
			database.Add(new ItemObject((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["type"],
				(double)(int)itemData[i]["price"], (double)(int)itemData[i]["health"], (double)(int)itemData[i]["health_regeneration"], 
				(double)(int)itemData[i]["attack"], (double)(int)itemData[i]["defense"], (double)(int)itemData[i]["critical_chance"],
				(double)(int)itemData[i]["critical_damage"],(double)(int)itemData[i]["shield"],(double)(int)itemData[i]["moving_speed"],
				(double)(int)itemData[i]["metal_attack"], (double)(int)itemData[i]["wood_attack"], (double)(int)itemData[i]["water_attack"],
				(double)(int)itemData[i]["fire_attack"], (double)(int)itemData[i]["earth_attack"], (double)(int)itemData[i]["metal_defense"],
				(double)(int)itemData[i]["wood_defense"], (double)(int)itemData[i]["water_defense"], (double)(int)itemData[i]["fire_defense"], 
				(double)(int)itemData[i]["earth_defense"], (double)(int)itemData[i]["fu_critical"], (double)(int)itemData[i]["spirit"], 
				(double)(int)itemData[i]["physique"], (double)(int)itemData[i]["strength"], (double)(int)itemData[i]["stamina"], 
				(double)(int)itemData[i]["internal_power"], (double)(int)itemData[i]["calm"], (double)(int)itemData[i]["agility"], 
				(double)(int)itemData[i]["durability"], (double)(int)itemData[i]["speical_effect_1"], (double)(int)itemData[i]["speical_effect_2"], 
				(double)(int)itemData[i]["speical_effect_3"], (double)(int)itemData[i]["speical_effect_4"], (double)(int)itemData[i]["speical_effect_5"], 
				(double)(int)itemData[i]["speical_effect_6"], itemData[i]["description"].ToString(), (int)itemData[i]["stackable_quantity"], 
				(int)itemData[i]["rarity"], itemData[i]["sprite_path"].ToString(), itemData[i]["model_path"].ToString()));
		}
	}

	public ItemObject FetchItemByID(int id) 
	{
		for( int i = 0; i < database.Count; i++)
			if(database[i].ID == id)
				return database[i];
		return null;
	}
}

public class ItemObject
{
	public int ID { get; set; }
	public string Title { get; set; }
	public int Type { get; set;}
	public double Price { get; set;}
	public double Health { get; set;}
	public double HealthRegeneration { get; set;}
	public double Attack { get; set;}
	public double Defense { get; set;}
	public double CriticalChance { get; set;}
	public double CriticalDamage { get; set;}
	public double Shield { get; set;}
	public double MovingSpeed { get; set;}
	public double MetalAttack { get; set;}
	public double WoodAttack { get; set;}
	public double WaterAttack { get; set;}
	public double FireAttack { get; set;}
	public double EarthAttack { get; set;}
	public double MetalDefense { get; set;}
	public double WoodDefense { get; set;}
	public double WaterDefense { get; set;}
	public double FireDefense { get; set;}
	public double EarthDefense { get; set;}
	public double FuCritical { get; set;}
	public double Spirit { get; set;}
	public double Physique { get; set;}
	public double Strength { get; set;}
	public double Stamina { get; set;}
	public double InternalPower { get; set;}
	public double Calm { get; set;}
	public double Agility { get; set;}
	public double Durability { get; set;}
	public double SpeicalEffect1 { get; set;}
	public double SpeicalEffect2 { get; set;}
	public double SpeicalEffect3 { get; set;}
	public double SpeicalEffect4 { get; set;}
	public double SpeicalEffect5 { get; set;}
	public double SpeicalEffect6 { get; set;}
	public double Durablility { get; set;}
	public string Description { get; set;}
	public int StackableQuantity { get; set;}
	public int Rarity { get; set;}
	public Sprite Sprite { get; set;}
	public string ModelPath { get; set;}

	public ItemObject( int id, string title, int type, double price, double health, double health_regeneration, double attack, 
						double defence, double critical_chance, double critical_damage, double shield, double moving_speed, 
						double metal_attack, double wood_attack, double water_attack, double fire_attack, double earth_attack, 
						double metal_defense, double wood_defense, double water_defense, double fire_defense, double earth_defense,
						double fu_critical, double spirit, double physique, double strength, double stamina, double internal_Power, 
						double calm, double agility, double durability, double speical_effect_1, double speical_effect_2, 
						double speical_effect_3, double speical_effect_4, double speical_effect_5, double speical_effect_6,
						string description, int stackable_quantity, int rarity, string sprite_path,string model_path)
	{
		this.ID = id;
		this.Title = title;
		this.Type = type;
		this.Price = price;
		this.Health = health;
		this.HealthRegeneration = health_regeneration;
		this.Attack = attack;
		this.Defense = defence;
		this.CriticalChance = critical_chance;
		this.CriticalDamage = critical_damage;
		this.Shield = shield;
		this.MovingSpeed = moving_speed;
		this.MetalAttack = metal_attack;
		this.WoodAttack = wood_attack;
		this.WaterAttack = water_attack;
		this.FireAttack = fire_attack;
		this.EarthAttack = earth_attack;
		this.MetalDefense = metal_defense;
		this.WoodDefense = wood_defense;
		this.WaterDefense = water_defense;
		this.FireDefense = fire_defense;
		this.EarthDefense = earth_defense;
		this.FuCritical = fu_critical;
		this.Spirit = spirit;
		this.Physique = physique;
		this.Strength = strength;
		this.Stamina = stamina;
		this.InternalPower = internal_Power;
		this.Calm = calm;
		this.Agility = agility;
		this.Durablility = durability;
		this.Description = description;
		this.StackableQuantity = stackable_quantity;
		this.Rarity = rarity;
		this.Sprite = Resources.Load<Sprite>("Sprites/Inventory/Items/" + sprite_path);
	}

		public ItemObject( int id, string title, int price)
	{
		this.ID = id;
		this.Title = title;
		this.Price = price;
	}


	public ItemObject()
	{
		this.ID = -1;
	}


	public Dictionary<string, double> GetCharacterRelatedAttribute(){
		Dictionary<string, double> attribute = new Dictionary<string, double>()
		{
			["MaxHealth"] = this.Health,
			["HealthRegeneration"] = this.HealthRegeneration,
			["Attack"] = this.Attack,
			["Defense"] = this.Defense,
			["CriticalChance"] = this.CriticalChance,
			["CriticalDamage"] = this.CriticalDamage,
			["Shield"] = this.Shield,
			["MovingSpeed"] = this.MovingSpeed,
			["FuCritical"] = this.FuCritical,
			["Calm"] = this.Calm,
			["Physique"] = this.Physique,
			["Strength"] = this.Strength,
			["Stamina"] = this.Stamina,
			["InternalPower"] = this.InternalPower,
			["Spirit"] = this.Spirit,
			["Agility"] = this.Agility,
			["MetalAttack"] = this.MetalAttack,
			["WoodAttack"] = this.WoodAttack,
			["WaterAttack"] = this.WaterAttack,
			["FireAttack"] = this.FireAttack,
			["EarthAttack"] = this.EarthAttack,
			["MetalDefense"] = this.MetalDefense,
			["WoodDefense"] = this.WoodDefense,
			["WaterDefense"] = this.WaterDefense,
			["FireDefense"] = this.FireDefense,
			["EarthDefense"] = this.EarthDefense,
    	};
		return attribute;
	}

	public Dictionary<string, double> GetFiveElementsAttribute(){
		Dictionary<string, double> attribute = new Dictionary<string, double>()
		{
			["MetalAttack"] = this.MetalAttack,
			["WoodAttack"] = this.WoodAttack,
			["WaterAttack"] = this.WaterAttack,
			["FireAttack"] = this.FireAttack,
			["EarthAttack"] = this.EarthAttack,
			["MetalDefense"] = this.MetalDefense,
			["WoodDefense"] = this.WoodDefense,
			["WaterDefense"] = this.WaterDefense,
			["FireDefense"] = this.FireDefense,
			["EarthDefense"] = this.EarthDefense,
    	};
		return attribute;
	}

	public Dictionary<string, double> getBuffsAttribute(){
		Dictionary<string, double> attribute = new Dictionary<string, double>()
		{
			["Health"] = this.Health,
			["HealthRegeneration"] = this.HealthRegeneration,
			["Strength"] = this.Strength,
			["Calm"] = this.Calm,
    	};
		return attribute;
	}
}