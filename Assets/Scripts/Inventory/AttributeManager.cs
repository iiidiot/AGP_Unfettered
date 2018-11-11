using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManager : MonoBehaviour {

	private int generalType = 4;
	private List<ItemObject> previousAccessoryItems = new List<ItemObject>();
	private List<ItemObject> currentAccessoryItems = new List<ItemObject>();

	// Use this for initialization
	void Start () {
		for(int i =0; i < generalType; i++)
		{
			previousAccessoryItems.Add(null);
			currentAccessoryItems.Add(null);
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateAttribute();
	}


	private void updateAttribute(){
		for(int i =0; i < generalType; i++)
		{
			GameObject slot = GetComponent<InventoryObject>().slots[i];
			//updateCurrent accessory item and add Current item data;;
			if(slot.transform.childCount > 0)
			{
				currentAccessoryItems[i] = slot.transform.GetChild(0).GetComponent<ItemData>().item;
				PlayerStatus.characterAttributes["Power"] += currentAccessoryItems[i].Power;
				PlayerStatus.characterAttributes["Defense"] += currentAccessoryItems[i].Defense;

				Debug.Log(PlayerStatus.getItemRelatedAttribute()["Power"]); 
			}
			else
			{
				currentAccessoryItems[i] = null;
			}

			// substract Previous item data;
			if(previousAccessoryItems[i] != null )
			{
				PlayerStatus.characterAttributes["Power"] -= previousAccessoryItems[i].Power;
				PlayerStatus.characterAttributes["Defense"] -= previousAccessoryItems[i].Defense;
			}

			// update Previous item
			previousAccessoryItems[i] = currentAccessoryItems[i];
			
		}
		


	}
}