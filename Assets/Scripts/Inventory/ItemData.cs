using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public ItemObject item;
	public int amount;
	public int slot; 
	private Vector2 offset; 
	private InventoryObject inventory;
	private Tooltip tooltip;
	public int generalType = 4;

	public BuffController buffController;
	void Start () 
	{
		inventory = GameObject.Find("Inventory").GetComponent<InventoryObject>();
		buffController = GameObject.Find("BuffManager").GetComponent<BuffController>();
		tooltip = inventory.GetComponent<Tooltip>();
	}
	public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
		{
			this.transform.SetParent(this.transform.parent.parent);
			this.transform.position = eventData.position - offset;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
    }

    public void OnDrag(PointerEventData eventData)
    {
        if( item != null )
		{
			this.transform.position = eventData.position - offset;
		}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		if( item.Type == slot || slot >= generalType){
			this.transform.SetParent(inventory.slots[slot].transform);
			this.transform.position = inventory.slots[slot].transform.position;
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(item != null)
		{
			offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
		}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactive();
    }

	public void changeProperty(){

	}

    public void OnPointerClick(PointerEventData eventData)
    {
		if(eventData.clickCount == 2)
		{
			int targetSlot = eventData.pointerPress.GetComponent<ItemData>().item.Type;
			if( targetSlot >= generalType ){
				 StartCoroutine(buffController.AddFireAttribute(3,10));
				if(inventory.slots[slot].transform.childCount <= 2){
					 //inventory.slots[slot].GetComponent<SlotObject>().id = -1;
					 inventory.items[slot].ID = -1;
					 Debug.Log("slot:"+ slot);
				 }
				 Destroy(gameObject);

			}else{
				if(inventory.items[targetSlot].ID == -1)
				{
					inventory.items[slot].ID = -1;
					this.slot = targetSlot;
					this.transform.SetParent(inventory.slots[targetSlot].transform);
					this.transform.position = this.transform.parent.position;
					
				}
				else
				{
					GameObject targetItem = inventory.slots[targetSlot].transform.GetChild(1).gameObject;
					targetItem.transform.SetParent(this.transform.parent);
					targetItem.transform.position = this.transform.parent.position;
					targetItem.GetComponent<ItemData>().slot = slot;
					inventory.items[slot].ID = targetItem.GetComponent<ItemData>().item.ID;

					this.slot = targetSlot;
					this.transform.SetParent(inventory.slots[targetSlot].transform);
					this.transform.position = this.transform.parent.position;
					inventory.items[slot].ID = this.item.ID;
				}

			}
		}
        
    }
}
