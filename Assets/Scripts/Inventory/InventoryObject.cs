using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour {
	private GameObject inventoryPanel;
	private GameObject slotPanel;
	private ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
	public int slotAmount = 40;
	public int accessoryAmount = 4;
	public int slot;
	public List<ItemObject> items= new List<ItemObject>();
	public List<GameObject> slots = new List<GameObject>();

	// 4 means this slot can allow any type of items to put; 0 means weapen , 1 means ......
	public int generalType = 4;
	void Start()
	{
		database = GetComponent<ItemDatabase>();
		//SaveAndLoadUtil.LoadPlayerStatus();
		InitAccessoryPanel();
		InitInventoryPanel();
		AddAccessoryItem(0);
		AddAccessoryItem(1);
		AddItem(0);
		AddItem(0);
		AddItem(1);
		AddItem(1);
		AddItem(1);

		
	}

	public void InitAccessoryPanel () 
	{
		inventoryPanel = GameObject.Find("Character Panel");
		slotPanel = inventoryPanel.transform.FindChild("Accessory Panel").gameObject;
		for(int i = 0; i < generalType; i++)
		{
			items.Add(new ItemObject());
			slots.Add(Instantiate(inventorySlot));
			slots[i].GetComponent<SlotObject>().id = i;
			slots[i].GetComponent<SlotObject>().itemType = i;
			slots[i].transform.SetParent(slotPanel.transform);
		}
		
	}
	public void InitInventoryPanel () 
	{
		inventoryPanel = GameObject.Find("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
		for(int i = generalType; i < slotAmount+generalType; i++)
		{
			items.Add(new ItemObject());
			slots.Add(Instantiate(inventorySlot));
			slots[i].GetComponent<SlotObject>().id = i;
			slots[i].GetComponent<SlotObject>().itemType = generalType;
			slots[i].transform.SetParent(slotPanel.transform);
		}
		
	}
	public void AddItem(int id)
	{
		ItemObject itemToAdd = database.FetchItemByID(id);
		if(itemToAdd.Stackable && checkDuplicate(itemToAdd))
		{
			for(int i = generalType; i < items.Count; i++)
			{
				if(items[i].ID == id)
				{
					ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
					data.amount++;
					data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
					break;
				}
			}
		}
		else
		{
			for(int i = generalType; i < items.Count; i++)
			{
				if(items[i].ID == -1)
				{
					items[i] = itemToAdd;
					GameObject itemObj = Instantiate(inventoryItem);
					itemObj.GetComponent<ItemData>().item = itemToAdd;
					itemObj.GetComponent<ItemData>().amount = 1;
					itemObj.GetComponent<ItemData>().slot = i;  
					itemObj.transform.SetParent(slots[i].transform);
					itemObj.transform.position = Vector2.zero;
					itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
					itemObj.name = itemToAdd.Title;
					break;
				}
			}
		}
	}

	public void AddAccessoryItem(int id)
	{
		ItemObject itemToAdd = database.FetchItemByID(id);
		for(int i = 0; i < generalType; i++)
		{
			if(items[i].ID == -1)
			{
				items[i] = itemToAdd;
				GameObject itemObj = Instantiate(inventoryItem);
				itemObj.GetComponent<ItemData>().item = itemToAdd;
				itemObj.GetComponent<ItemData>().amount = 1;
				itemObj.GetComponent<ItemData>().slot = i;  
				itemObj.transform.SetParent(slots[i].transform);
				itemObj.transform.position = Vector2.zero;
				itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
				itemObj.name = itemToAdd.Title;
				break;
			}
		}
	}

	private bool checkDuplicate(ItemObject item)
	{
		for(int i = generalType; i < items.Count; i++)
		{
			if(items[i].ID == item.ID)
				return true;
		}
		return false;
	}
}
