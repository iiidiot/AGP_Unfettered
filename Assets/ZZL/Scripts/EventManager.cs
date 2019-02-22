using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlaceObject()
    {
        int index = UIFarmItemManager.s_index;
        GameObject farmItem = UIFarmItemManager.s_targetFarmItem;

        if(index != -1)
        {
            GridManager.PlaceObject(farmItem, index);

            UIFarmItemManager.RemoveItem();
        }
    }
}
