using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TriggerInteractionItem : MonoBehaviour {
	public PlayableDirector pd;
	public Text hintUI;
	public bool canPressF = false;

	public bool isPlay = false;
	public GameObject fft, words, plane;

	// Use this for initialization
	void Start () {
		pd = GameObject.Find("CG06").GetComponent<PlayableDirector>();
		hintUI = GameObject.Find("Caption Canvas").transform.Find("Fhint").GetComponent<Text>();
		fft = GameObject.Find("Tutorial05").transform.Find("fire fu tutorial").gameObject;
		words = GameObject.Find("Tutorial05").transform.Find("Words").gameObject;
		plane = GameObject.Find("Tutorial05").transform.Find("Plane").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(canPressF && Input.GetKeyDown(KeyCode.F))
		{
			hintUI.enabled = false;
			pd.Play();
			isPlay = true;
		}

		if (isPlay && pd.state != PlayState.Playing)
		{
			//显示tutorialUI
			//删除自己
			fft.SetActive(true);
			words.SetActive(true);
			plane.SetActive(true);
			hintUI.enabled = true;
			hintUI.gameObject.SetActive(false);
			Destroy(this);
			Destroy(this.gameObject);
		}	
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
        	hintUI.gameObject.SetActive(true);
        	hintUI.enabled = true;
            canPressF = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
        	hintUI.gameObject.SetActive(true);
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
