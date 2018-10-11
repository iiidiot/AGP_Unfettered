using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	private string playerTag = "Player";

    public GameObject bubble;

	[SerializeField]
	private GameObject dialogBox;

	[SerializeField]
	private Vector3 offsetPosition;
	
	[SerializeField]
	private bool willDisappear;

	[SerializeField]
	private bool existenceStatus = true; // change to false will disappear itself
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkExistence();
		HanldeInput();
	}

    private void HanldeInput()
    {
        //TODO figure out a better to solve the jump problem
		if(Input.GetKey(KeyCode.F)){
			existenceStatus = false;
		}

    }

    private void checkExistence()
    {
        if(!existenceStatus){
			dialogBox.SetActive(false);
			Destroy(gameObject);
		}
    }

    void OnTriggerEnter( Collider collider) {
		if(collider.tag == playerTag){
			dialogBox.transform.position = offsetPosition + new Vector3(transform.position.x, transform.position.y, transform.position.z);
			dialogBox.SetActive(true);
            bubble.SetActive(true);
            bubble.GetComponent<DialogController>().nextDialog = true;

        }
	}

	void OnTriggerExit( Collider collider) {
		if(collider.tag == playerTag){
			dialogBox.SetActive(false);
		}
	}
}
