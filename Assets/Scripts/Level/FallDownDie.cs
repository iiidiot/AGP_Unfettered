﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject part04 = GameObject.Find("SceneRoot/Part04");
            if (part04.activeSelf) // if in part04
            {
                part04.GetComponent<Part04Restart>().DoPart04Restart();
            }
            else
            {
                other.gameObject.transform.position = GameRunTimeStatus.RebornPlace;

            }
        }
    }

}
