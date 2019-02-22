using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[System.Serializable]
public class Item
{
    public int index;
    public string name;
    public GameObject itemPrefab;
    public Sprite sprite;
    public bool isFarmItem;
    public List<GameObject> growthList;
    
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FarmItemsOS", order = 1)]
public class FarmItemsSO : ScriptableObject
{
    public List<Item> InventoryItems;
}

