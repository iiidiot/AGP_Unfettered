using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager {

    public enum ItemType
    {
        ImportantItem,
        None,
        Health,
        Energy,
    }

    public enum WeaponType
    {
        NONE,
        FU,
        SWORD,
    }

    //public static string GetItemName(string itemId)
    //{
    //    ItemUnfettered item = Resources.Load<ItemUnfettered>("ScriptableObjects/Item/" + itemId);
    //    return item.name;
    //}

    public static ItemUnfettered GetItem(string itemId)
    {
        ItemUnfettered item = Resources.Load<ItemUnfettered>("ScriptableObjects/Item/" + itemId);
        return item;
    }

}
