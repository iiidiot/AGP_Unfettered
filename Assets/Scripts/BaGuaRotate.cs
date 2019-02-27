using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaGuaRotate : MonoBehaviour {

    public float rotateSpeed = 20;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(this.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
