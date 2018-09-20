using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HEALTH,
    ENERGY
}

/*
    This is the Item base class for every kind of items 
    in our game. 
    Every other new Items should be inherited from this class.

    TODO: Needs to modify it for inheritance in the future.
    For now it's used for the Sprite usage.
*/
public class Item : MonoBehaviour
{
    public ItemType itemType;

    public Sprite normalSprite;

    public Sprite hightLightedSprite;

    // This is to tell how many same items can be stacked together.
    // If maxSize = 0, then even the same items will be placed to different slots.
    public int maxSize;

	public void Use()
    {
        switch(itemType)
        {
            case ItemType.HEALTH:
                Debug.Log("I just used a Health Potion.");
                break;
            case ItemType.ENERGY:
                Debug.Log("I just used an Energy Potion.");
                break;
            default:
                break;
        }
    }
}
