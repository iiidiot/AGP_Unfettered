using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class LavaAnimationController : MonoBehaviour {

    public float timeOffset;

    // Use this for initialization
    void Start()
    {
        Invoke("StartPlayingAnimation", timeOffset);
    }

    void StartPlayingAnimation()
    {
        transform.DOMoveY(-40, 2f).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
