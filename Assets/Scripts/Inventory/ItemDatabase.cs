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
							(int)itemData[i]["price"], (int)itemData[i]["stats"]["property"]["health"], (int)itemData[i]["stats"]["property"]["mana"], 
							(int)itemData[i]["stats"]["property"]["power"], (int)itemData[i]["stats"]["property"]["defence"], (int)itemData[i]["stats"]["property"]["critical_chance"],
							(int)itemData[i]["stats"]["property"]["speed"], (int)itemData[i]["stats"]["property"]["luck"], (int)itemData[i]["stats"]["property"]["intelligence"],
							(int)itemData[i]["stats"]["property"]["metal"], (int)itemData[i]["stats"]["property"]["wood"], (int)itemData[i]["stats"]["property"]["water"],
							(int)itemData[i]["stats"]["property"]["fire"], (int)itemData[i]["stats"]["property"]["earth"], (int)itemData[i]["stats"]["durability"], 
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
	public int Health { get; set;}
	public int Mana { get; set;}
	public int Power { get; set;}
	public int Defense { get; set;}
	public int CriticalChance { get; set;}
	public int Speed { get; set;}
	public int Luck { get; set;}
	public int Intelligence { get; set;}
	public int Metal { get; set;}
	public int Wood { get; set;}
	public int Water { get; set;}
	public int Fire { get; set;}
	public int Earth { get; set;}
	public int Durablility { get; set;}
	public string Description { get; set;}
	public int StackableQuantity { get; set;}
	public int Rarity { get; set;}
	public Sprite Sprite { get; set;}
	public string ModelPath { get; set;}

	public ItemObject( int id, string title, int type, int price, int health, int mana, int power, int defence, int criticalChance,
						int speed, int luck, int intellignece, int metal, int wood, int water, int fire,
						int earth, int durability, string description, int stackable_quantity, int rarity, string sprite_path,
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

	public Dictionary<string, float> getAllCharacterRelatedAttribute(){
		Dictionary<string, float> attribute = new Dictionary<string, float>()
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