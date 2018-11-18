using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MeltController : MonoBehaviour {

    private Material material;
    

    // Use this for initialization
    void OnEnable () {
        material = this.transform.GetComponent<ParticleSystemRenderer>().sharedMaterial;
        Debug.Log(material);
	}
	
	// Update is called once per frame
	void Update () {
        float random = Random.Range(0.1f, 0.2f);
        material.SetFloat("_Threshold", random);
	}
}
