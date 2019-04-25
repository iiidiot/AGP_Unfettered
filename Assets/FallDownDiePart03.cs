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
            CastDamage();

        }
    }

    private void CastDamage()
    {
        //PlayerTestController.instance.GetDamage(100);

        Invoke("DoPart03Restart", 0f);

    }

    private void DoPart03Restart()
    {
        GameObject part03 = GameObject.Find("SceneRoot/Part03");
        part03.GetComponent<Part03Restart>().DoPart03Restart();
    }

}
