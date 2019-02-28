using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TriggerInteractionItem : MonoBehaviour {
	public PlayableDirector pd;
	public Text hintUI;
	public bool canPressF = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(canPressF && Input.GetKeyDown(KeyCode.F))
		{
			hintUI.enabled = false;
			pd.Play();
		}		
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
        	hintUI.enabled = true;
            canPressF = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
        	hintUI.enabled = false;
            canPressF = false;
        }
    }
}
