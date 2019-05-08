using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneTriggerController : MonoBehaviour {

    public GameObject boss;
    public GameObject text;

    public GameObject bgm;


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
            List<int> blockstate = new List<int>();
            blockstate.Add(0);
            PlayerTestController.instance.BlockPlayerInput(blockstate);
            PlayerTestController.instance.SetXZVelocityZero();
            boss.GetComponent<Boss1ChaseController>().isIdle = true;
            text.SetActive(true);
            bgm.SetActive(false);
        }
    }
}
