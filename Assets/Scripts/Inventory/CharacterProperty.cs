using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterProperty : MonoBehaviour {

	private GameObject PropertyList;
	private GameObject FiveElementsList;
	private GameObject propertyContent;

	public List<GameObject> PropertyItems = new List<GameObject>();
	public List<GameObject> FiveElementsItems = new List<GameObject>();

	public GameObject PropertyItem; 
	public GameObject FiveElementsItem; 

	void Start ()
	{
		PropertyList = GameObject.Find("Property Panel").transform.FindChild("Viewport").gameObject;
		propertyContent = PropertyList.transform.GetChild(0).gameObject;
		InitProperty();
		
		FiveElementsList = GameObject.Find("Five Elements Panel").gameObject;
		InitFiveElements();
	}
	private void InitProperty()
	{
		for(int i = 0; i < PlayerStatus.characterAttributes.Count; i++)
		{
			PropertyItems.Add(Instantiate(PropertyItem));
			PropertyItems[i].transform.SetParent(propertyContent.transform);
		}
	}

	private void InitFiveElements()
	{
		for(int i = 0; i < PlayerStatus.fiveElementsProperty.Count; i++)
		{
			FiveElementsItems.Add(Instantiate(FiveElementsItem));
			FiveElementsItems[i].transform.SetParent(FiveElementsList.transform);

		}
	}

	private void updateProperty()
	{
		int index = 0;
		Debug.Log(PlayerStatus.getItemRelatedAttribute().Count);
		foreach ( KeyValuePair<string,double > item in PlayerStatus.getItemRelatedAttribute())
		{
		 	PropertyItems[index].transform.GetChild(0).GetComponent<Text>().text = item.Key + ":  " + item.Value;
		 	index++;
		}
		
	}

	private void updateFiveElementsProperty()
	{
		int index = 0;
		foreach ( KeyValuePair<string,float > item in PlayerStatus.fiveElementsProperty)
		{
			FiveElementsItems[index].transform.GetChild(0).GetComponent<Text>().text = item.Key.ToString();
			FiveElementsItems[index].transform.GetChild(1).GetComponent<Text>().text = item.Value.ToString();
		 	index++;
		}		
	}
	
	// Update is called once per frame
	void Update () {
		updateProperty();
		updateFiveElementsProperty();
		inputH();
		
	}

	private void inputH()
	{
		if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerStatus.characterAttributes["Health"] -= 1;
        }
	}
}
