using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FarmItemsOS", order = 1)]
public class FarmItemsSO : ScriptableObject
{
    // Add different lists for different items' growths
    public List<GameObject> AppleItems;

	
}
