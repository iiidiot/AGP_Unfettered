using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Part03BossWakeUp : MonoBehaviour {

    public PlayableDirector pd;
    bool hasPlayed;
    // Use this for initialization
    void Start () {
        hasPlayed = false;

    }

    public float transitionTime = 0.5f;
    public GameObject oldPart, newPart;
    public GameObject oldCam, newCam;
    public Transform player;
    public Transform playerBornPlace;

    void PartSwitch()
    {
        oldPart.SetActive(false);
        oldCam.SetActive(false);

        newPart.SetActive(true);
        newCam.SetActive(true);

        player.position = playerBornPlace.position;
        PlayerTestController.instance.grounds.Clear();


        pd.gameObject.SetActive(false);
        //ProCamera2DTransitionsFX.Instance.TransitionEnter();
        //newCam.GetComponent<ProCamera2DTransitionsFX>().TransitionEnter();
    }


    // Update is called once per frame
    void Update()
    {

        if (hasPlayed && pd.state != PlayState.Playing)
        {
            //oldCam.GetComponent<ProCamera2DTransitionsFX>().TransitionExit();
            Invoke("PartSwitch", 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" || other.tag == "Fu")
        {
            pd.gameObject.SetActive(true);
            pd.Play();
            hasPlayed = true;
        }
    }
  

}
