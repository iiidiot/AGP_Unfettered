using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextClickSwitch : MonoBehaviour {
    public GameObject boss;
    public GameObject BGM;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            PlayerTestController.instance.BlockStateClearAll();
            boss.GetComponent<Boss1ChaseController>().isIdle = false;
            BGM.SetActive(true);
            this.gameObject.SetActive(false);
        }
	}
}
