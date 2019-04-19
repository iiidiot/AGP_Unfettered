using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownDiePart03 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            other.gameObject.transform.position = GameRunTimeStatus.RebornPlace;


            GameObject part03Platforms = GameObject.Find("SceneRoot/Part03/EnvRoot/PlatformGroup");

            for (int i = 0; i < part03Platforms.transform.childCount; i++)
            {
                GameObject o = part03Platforms.transform.GetChild(i).gameObject;
                o.SetActive(true);
                if (o.GetComponent<FragileStone>())
                {
                    o.GetComponent<FragileStone>().Restore();
                }
            }

        }
    }

}
