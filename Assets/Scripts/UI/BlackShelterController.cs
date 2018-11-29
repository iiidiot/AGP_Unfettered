using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackShelterController : MonoBehaviour {
    public BlackShelterMoveOut moveOut;
    public BlackShelterMoveIn movein;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameRunTimeStatus.UIBlackShelterMoveIn)
        {
            moveOut.enabled = false;
            movein.enabled = true;
        }
        else
        {
            moveOut.enabled = true;
            movein.enabled = false;
        }
	}
}
