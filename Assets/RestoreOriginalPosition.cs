using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreOriginalPosition : MonoBehaviour {

    private Vector3 originalPosition;
	// Use this for initialization
	void Start () {
        originalPosition = this.transform.position;
	}
	
    public void DoRestoreOriginalPosition()
    {
        this.transform.position = originalPosition;
    }
}
