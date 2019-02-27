using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IceWallCG : MonoBehaviour {

	public PlayableDirector pd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision co)
    {
        if (co.collider.tag == "Player")
        {
            pd.Play();
        }
    }
}
