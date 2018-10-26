using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemUnfettered : ScriptableObject {

    public string id;

    public ItemManager.ItemType itemType;

    // public ItemManager.WeaponType weaponType;  not understand

    public string normalSpritePath;

    public string hightLightedSpritePath;

    public int maxSize;

    public int price;

}