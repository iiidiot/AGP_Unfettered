using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial01 : MonoBehaviour {

	void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
			if(Input.anyKeyDown){
                Debug.Log("ishere");
				GameRunTimeStatus.UIBlackShelterMoveIn = true;
			}
        }
    }
}
