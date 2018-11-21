using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingProfileManager : MonoBehaviour {

    public PostProcessingProfile profileOrdinary;
    public PostProcessingProfile profileDrawMode;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeToDrawMode()
    {
        this.GetComponent<PostProcessingBehaviour>().profile = profileDrawMode;
    }

    public void ChangeToOrdinaryMode()
    {
        this.GetComponent<PostProcessingBehaviour>().profile = profileOrdinary;
    }
}
