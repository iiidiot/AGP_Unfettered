using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFragBurst : MonoBehaviour {

    public GameObject iceFragBustParticle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private ParticleSystem ps;
    private ParticleSystemRenderer psr;

    public void Do()
    {
    
        iceFragBustParticle.SetActive(true);
        this.gameObject.SetActive(false);
    }

   
  
}
