using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part04BornTrigger : MonoBehaviour {
    public GameObject boss;
    public GameObject haveToRunText;
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //List<int> blockstate = new List<int>();
            //blockstate.Add(0);
            //PlayerTestController.instance.BlockPlayerInput(blockstate);
            PlayerTestController.instance.blockStatements[0] = 1;
            boss.GetComponent<Boss1ChaseController>().isIdle = true;
            haveToRunText.SetActive(true);
        }
    }
}
