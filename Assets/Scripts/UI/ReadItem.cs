using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ItemSample item = Resources.Load<ItemSample>("ScriptableObjects/Item1");
        this.transform.Find("Viewport/Content/Button/Text").GetComponent<Text>().text = item.name;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
