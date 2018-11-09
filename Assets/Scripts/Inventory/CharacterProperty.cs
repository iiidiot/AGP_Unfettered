using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterProperty : MonoBehaviour {

	private GameObject PropertyList;
	private GameObject FiveElementsList;
	private GameObject propertyContent;

	public List<GameObject> PropertyItems = new List<GameObject>();

	public GameObject PropertyItem; 

	void Start ()
	{
		PropertyList = GameObject.Find("Property Panel").transform.FindChild("Viewport").gameObject;
		propertyContent = PropertyList.transform.GetChild(0).gameObject;
		InitProperty();
		
	}
	private void InitProperty()
	{
		for(int i = 0; i < PlayerStatus.characterProperty.Count; i++)
		{
			PropertyItems.Add(Instantiate(PropertyItem));
			PropertyItems[i].transform.SetParent(propertyContent.transform);
		}
	}

	private void LoadFiveElement()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		LoadFiveElement();
		
	}
}
