using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour {
	private GameObject inventoryPanel;
	private GameObject slotPanel;
	private ItemDatabase database;

	private int slotIndex = 0;
	public GameObject inventoryGeneralSlot;
	public GameObject inventoryFuSlot;
	public GameObject inventoryItem;
	public int slotAmount = 35;
	public int accessoryAmount = 4;
	public int ShortcutSlotsCount = 5;
	public int slot;
	public List<ItemObject> items= new List<ItemObject>();
	public List<GameObject> slots = new List<GameObject>();

	private List<string> inventoryTypes = new List<string>(new string[] { "Equipment Inventory Panel", "Consumable Inventory Panel", "Quest Inventory Panel", "Others Inventory Panel", "Overall Inventory Panel" });



	// 4 means this slot can allow any type of items to put; 0 means weapen , 1 means ......
	public int generalType = 4;
	void Start()
	{
		database = GetComponent<ItemDatabase>();
		//SaveAndLoadUtil.LoadPlayerStatus();
		InitAccessoryPanel();
		AddAccessoryItem(0);
		InitInvPanel();
		
		
		
		//InitEquipmentInvPanel();
		//InitConsumableInvPanel();
		//InitQuestItemInvPanel();
		//InitOthersInvPanel();
		
		InitShortcutInvPanel();

		AddAccessoryInvItem(0);
		AddAccessoryInvItem(0);
		AddAccessoryInvItem(1);
		AddAccessoryInvItem(1);

		AddFuInvItem(2);
		AddFuInvItem(2);
		AddFuInvItem(2);
		AddFuInvItem(2);
		AddFuInvItem(2);
		AddFuInvItem(2);
		AddFuInvItem(2);

		
		//AddAccessoryItem(1);
		// AddItem(0);
		// AddItem(0);
		// AddItem(1);
		// AddItem(1);
		// AddItem(1);
		// AddItem(2);
		// AddItem(2);
		// AddItem(2);
		
		
	}

	public void InitAccessoryPanel () 
	{
		inventoryPanel = GameObject.Find("Character Panel");
		slotPanel = inventoryPanel.transform.Find("Accessory Panel").gameObject;
		for(; slotIndex < generalType; slotIndex++)
		{
			items.Add(new ItemObject());
			slots.Add(Instantiate(inventoryGeneralSlot));
			slots[slotIndex].GetComponent<SlotObject>().id = slotIndex;
			slots[slotIndex].GetComponent<SlotObject>().itemType = slotIndex;
			slots[slotIndex].transform.SetParent(slotPanel.transform);
		}
	}

	public void InitInvPanel ()
	{
		foreach(string inventoryType in inventoryTypes)
		{
			slotPanel = GameObject.Find(inventoryType).transform.GetChild(1).gameObject;
			int startIndex = slotIndex;
			for(; slotIndex < slotAmount+startIndex; slotIndex++)
			{
				items.Add(new ItemObject());
				slots.Add(Instantiate(inventoryGeneralSlot));
				slots[slotIndex].GetComponent<SlotObject>().id = slotIndex;
				slots[slotIndex].GetComponent<SlotObject>().itemType = inventoryTypes.IndexOf(inventoryType);
				slots[slotIndex].transform.SetParent(slotPanel.transform);
			}
		}
	}
	public void InitShortcutInvPanel () 
	{
		slotPanel = GameObject.Find("Shortcut Panel");
		int startIndex = slotIndex;
		for(; slotIndex < ShortcutSlotsCount+startIndex; slotIndex++)
		{
			items.Add(new ItemObject());
			slots.Add(Instantiate(inventoryFuSlot));
			slots[slotIndex].GetComponent<SlotObject>().id = slotIndex;
			slots[slotIndex].GetComponent<SlotObject>().itemType = generalType;
			slots[slotIndex].transform.SetParent(slotPanel.transform);
		}
	}

	//TODO delete those init function when all slot is inited perfectly

	// public void InitEquipmentInvPanel () 
	// {
	// 	inventoryPanel = GameObject.Find("Inventory Panel");
	// 	slotPanel = inventoryPanel.transform.GetChild(0).GetChild(1).gameObject;
	// 	int startIndex = slotIndex;
	// 	for(; slotIndex < slotAmount+startIndex; slotIndex++)
	// 	{
	// 		items.Add(new ItemObject());
	// 		slots.Add(Instantiate(inventoryGeneralSlot));
	// 		slots[slotIndex].GetComponent<SlotObject>().id = slotIndex;
	// 		slots[slotIndex].GetComponent<SlotObject>().itemType = generalType;
	// 		slots[slotIndex].transform.SetParent(slotPanel.transform);
	// 	}
		
	// }

	// public void InitConsumableInvPanel () 
	// {
	// 	inventoryPanel = GameObject.Find("Inventory Panel");
	// 	slotPanel = inventoryPanel.transform.GetChild(1).GetChild(1).gameObject;
	// 	int startIndex = slotIndex;
	// 	for(; slotIndex < slotAmount+startIndex; slotIndex++)
	// 	{
	// 		items.Add(new ItemObject());
	// 		slots.Add(Instantiate(inventoryGeneralSlot));
	// 		slots[slotIndex].GetComponent<SlotObject>().id = slotIndex;
	// 		slots[slotIndex].GetComponent<SlotObject>().itemType = generalType;
	// 		slots[slotIndex].transform.SetParent(slotPanel.transform);
	// 	}
	// }

	// public void InitQuestItemInvPanel () 
	// {
	// 	inventoryPanel = GameObject.Find("Inventory Panel");
	// 	slotPanel = inventoryPanel.transform.GetChild(2).GetChild(1).gameObject;
	// 	int startIndex = slotIndex;
	// 	for(; slotIndex < slotAmount+startIndex; slotIndex++)
	// 	{
	// 		items.Add(new ItemObject());
	// 		slots.Add(Instantiate(inventoryGeneralSlot));
	// 		slots[slotIndex].GetComponent<SlotObject>().id = slotIndex;
	// 		slots[slotIndex].GetComponent<SlotObject>().itemType = generalType;
	// 		slots[slotIndex].transform.SetParent(slotPanel.transform);
	// 	}
		
	// }
	// public void InitOthersInvPanel () 
	// {
	// 	inventoryPanel = GameObject.Find("Inventory Panel");
	// 	slotPanel = inventoryPanel.transform.GetChild(3).GetChild(1).gameObject;
	// 	int startIndex = slotIndex;
	// 	for(; slotIndex < slotAmount+startIndex; slotIndex++)
	// 	{
	// 		items.Add(new ItemObject());
	// 		slots.Add(Instantiate(inventoryGeneralSlot));
	// 		slots[slotIndex].GetComponent<SlotObject>().id = slotIndex;
	// 		slots[slotIndex].GetComponent<SlotObject>().itemType = generalType;
	// 		slots[slotIndex].transform.SetParent(slotPanel.transform);
	// 	}
		
	// }
	public void AddAccessoryInvItem(int id)
	{
		ItemObject itemToAdd = database.FetchItemByID(id);
		if(itemToAdd.StackableQuantity > 1 && checkDuplicate(itemToAdd))
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

	public void AddFuInvItem(int id)
	{
		ItemObject itemToAdd = database.FetchItemByID(id);
		int startIndex = generalType + slotAmount;
		bool isRightSlot = false;
		bool shouldStack = (itemToAdd.StackableQuantity > 1 && checkDuplicate(itemToAdd));
		for(int i = startIndex ; i < startIndex + slotAmount; i++)
		{
			// find the stackable slot or empty slot 
			if( shouldStack && (items[i].ID == id) && (slots[i].GetComponent<SlotObject>().amount < itemToAdd.StackableQuantity) )
			{
				slots[i].GetComponent<SlotObject>().amount += 1;
				isRightSlot = true;
			}
			else if(items[i].ID == -1)
			{
				slots[i].GetComponent<SlotObject>().amount = 1;
				isRightSlot = true;
			}
			
			// add item to right slot 
			if(isRightSlot)
			{
				items[i] = itemToAdd;
				GameObject itemObj = Instantiate(inventoryItem);
				itemObj.GetComponent<ItemData>().item = itemToAdd;
				itemObj.GetComponent<ItemData>().slot = i;  
				itemObj.transform.SetParent(slots[i].transform);
				itemObj.transform.position = Vector2.zero;
				itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
				itemObj.name = itemToAdd.Title;
				break;
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
