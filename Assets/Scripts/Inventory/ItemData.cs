using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
	public ItemObject item;
	public int amount;
	public int slot; 
	private Vector2 offset; 
	private InventoryObject inventory;
	private Tooltip tooltip;
	public int generalType = 4;
	void Start () 
	{
		inventory = GameObject.Find("Inventory").GetComponent<InventoryObject>();
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
}
