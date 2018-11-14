﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotObject : MonoBehaviour, IDropHandler {
	public int id;
	private InventoryObject inventory;
	public int itemType;
	public int generalType = 4;

	public int amount;

	void Start () 
	{
		inventory = GameObject.Find("Inventory").GetComponent<InventoryObject>();
	}

	void Update ()
	{
		// TODO: this text update should put in more resonable place, we don't need to check each second.
		if(this.transform.childCount > 2)
		{

			this.transform.GetChild(0).GetComponent<Text>().text = (transform.childCount - 1).ToString();
		}
		else
		{
			this.transform.GetChild(0).GetComponent<Text>().text = "";
		}
	}
	
    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
		int currentStackedItem = transform.childCount - 1;
		Debug.Log("id:"+id);
		//Debug.Log("inventory.items[id].ID:"+inventory.items[id].ID);
		if((droppedItem.item.Type == id || id >= generalType ))
		{
			if(inventory.items[id].ID == droppedItem.item.ID && currentStackedItem < droppedItem.item.StackableQuantity)
			{
				inventory.items[droppedItem.slot] = new ItemObject();
				inventory.items[id] = droppedItem.item;
				droppedItem.slot = id;
			}
			else if(inventory.items[id].ID == -1)
			{
				inventory.items[droppedItem.slot] = new ItemObject();
				inventory.items[id] = droppedItem.item;
				droppedItem.slot = id;
			}
			else if(droppedItem.slot != id)
			{
				Transform item = this.transform.GetChild(1);
				item.GetComponent<ItemData>().slot = droppedItem.slot;
				item.transform.SetParent(inventory.slots[droppedItem.slot].transform);
				item.transform.position = inventory.slots[droppedItem.slot].transform.position;

				droppedItem.slot = id;
				droppedItem.transform.SetParent(this.transform);
				droppedItem.transform.position = this.transform.position;

				inventory.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
				inventory.items[id] = droppedItem.item;
			}
		}
	}
}
