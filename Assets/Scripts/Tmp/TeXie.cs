using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeXie : MonoBehaviour {
	public GameObject player, sister;
	public GameObject bossTeXie;

	public GameObject boss;

 void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && Input.anyKey)
        {
			boss.SetActive(false);
			sister.SetActive(false);
			player.SetActive(false);
			bossTeXie.SetActive(true);
        }
    }
}
