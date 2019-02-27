using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {


    private Quaternion fixedRotation;
	// Use this for initialization
	void Start () {
        fixedRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = fixedRotation;
	}
}
