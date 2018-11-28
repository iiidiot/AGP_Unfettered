using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {

    // Use this for initialization
    public Vector3 tarPos = Config.UnValidPos;
	void Start () {
		
	}
    public float speed = 1f;
    public void setTarPos(Vector3 pos)
    {
        pos.y = transform.position.y;
        tarPos = pos;
    }

	// Update is called once per frame
	void Update () {
		if(tarPos == Config.UnValidPos)
        {
            return;
        }

        Vector3 diff = tarPos - transform.position;
        float dissqr = diff.sqrMagnitude;
        float s = speed * Time.deltaTime;
        float sqr = s * s;
        if(dissqr <= sqr)
        {
            transform.position = tarPos;
            tarPos = Config.UnValidPos;
            return;
        }

        transform.position = diff.normalized * s +transform.position;


	}
}
