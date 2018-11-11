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
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Inventory/Items.json"));
		ConstructItemDatabase();
	}

	void ConstructItemDatabase()
	{
		for(int i = 0; i < itemData.Count; i++)
		{
			database.Add(new ItemObject((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["type"],
							(int)itemData[i]["price"], (double)(int)itemData[i]["stats"]["property"]["health"], (double)(int)itemData[i]["stats"]["property"]["mana"], 
							(double)(int)itemData[i]["stats"]["property"]["power"], (double)(int)itemData[i]["stats"]["property"]["defence"], (double)(int)itemData[i]["stats"]["property"]["critical_chance"],
							(double)(int)itemData[i]["stats"]["property"]["speed"], (double)(int)itemData[i]["stats"]["property"]["luck"], (double)(int)itemData[i]["stats"]["property"]["intelligence"],
							(double)(int)itemData[i]["stats"]["property"]["metal"], (double)(int)itemData[i]["stats"]["property"]["wood"], (double)(int)itemData[i]["stats"]["property"]["water"],
							(double)(int)itemData[i]["stats"]["property"]["fire"], (double)(int)itemData[i]["stats"]["property"]["earth"], (double)(int)itemData[i]["stats"]["durability"], 
							itemData[i]["description"].ToString(), (int)itemData[i]["stackable_quantity"], (int)itemData[i]["rarity"], itemData[i]["sprite_path"].ToString(),
							itemData[i]["model_path"].ToString()));
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
	public int Price { get; set;}
	public double Health { get; set;}
	public double Mana { get; set;}
	public double Power { get; set;}
	public double Defense { get; set;}
	public double CriticalChance { get; set;}
	public double Speed { get; set;}
	public double Luck { get; set;}
	public double Intelligence { get; set;}
	public double Metal { get; set;}
	public double Wood { get; set;}
	public double Water { get; set;}
	public double Fire { get; set;}
	public double Earth { get; set;}
	public double Durablility { get; set;}
	public string Description { get; set;}
	public int StackableQuantity { get; set;}
	public int Rarity { get; set;}
	public Sprite Sprite { get; set;}
	public string ModelPath { get; set;}

	public ItemObject( int id, string title, int type, int price, double health, double mana, double power, double defence, double criticalChance,
						double speed, double luck, double intellignece, double metal, double wood, double water, double fire,
						double earth, double durability, string description, int stackable_quantity, int rarity, string sprite_path,
						string model_path)
	{
		this.ID = id;
		this.Title = title;
		this.Type = type;
		this.Price = price;
		this.Health = health;
		this.Mana = mana;
		this.Power = power;
		this.Defense = defence;
		this.CriticalChance = criticalChance;
		this.Speed = speed;
		this.Luck = luck;
		this.Intelligence = intellignece;
		this.Metal = metal;
		this.Wood = wood;
		this.Water = water;
		this.Fire = fire;
		this.Earth = earth;
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

	public Dictionary<string, double> getAllCharacterRelatedAttribute(){
		Dictionary<string, double> attribute = new Dictionary<string, double>()
		{
			["Max_health"] = this.Health,
			["Max_Mana"] = this.Mana,
			["Power"] = this.Power,
			["Defense"] = this.Defense,
			["Speed"] = this.Speed,
			["Intelligence"] = this.Intelligence,
			["CriticalChance"] = this.CriticalChance,
			["Luck"] = this.Luck,
			["Metal"] = this.Metal,
			["Wood"] = this.Metal,
			["Water"] = this.Water,	
			["Fire"] = this.Fire,	
			["Earth"] = this.Earth,	
    	};
	
		return attribute;
	}
}