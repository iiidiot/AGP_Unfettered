using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

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
				Dictionary<string, double> currentAttribute = currentAccessoryItems[i].getAllCharacterRelatedAttribute();
				foreach ( KeyValuePair<string,double > item in currentAttribute)
				{
					Type playerstats = typeof(PlayerStatus);
					FieldInfo[] properties = playerstats.GetFields(BindingFlags.Public | BindingFlags.Static );
					foreach (FieldInfo property in properties)
					{
						if(item.Key.Equals(property.Name))
						{
							property.SetValue(null, item.Value + (double)property.GetValue(null));
						}
						
					}
				}
			}
			else
			{
				currentAccessoryItems[i] = null;
			}

			// substract Previous item data;
			if(previousAccessoryItems[i] != null )
			{
				Dictionary<string, double> previousAttribute = previousAccessoryItems[i].getAllCharacterRelatedAttribute();
				foreach ( KeyValuePair<string,double > item in previousAttribute)
				{
					Type playerstats = typeof(PlayerStatus);
					FieldInfo[] properties = playerstats.GetFields(BindingFlags.Public | BindingFlags.Static );
					foreach (FieldInfo property in properties)
					{
						if(item.Key.Equals(property.Name))
						{
							property.SetValue(null, (double)property.GetValue(null) - item.Value);
						}
					}
				}
			}

			// update Previous item
			previousAccessoryItems[i] = currentAccessoryItems[i];
			
		}
		


	}
}