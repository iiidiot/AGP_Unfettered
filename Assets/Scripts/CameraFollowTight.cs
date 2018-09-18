using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTight : MonoBehaviour {

    public Transform followTarget;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - followTarget.position;
	}

    // Update is called once per frame
    void LateUpdate () {
        //x, y 跟随
        transform.position = new Vector3((followTarget.position + offset).x, (followTarget.position + offset).y, transform.position.z);
    }
}
