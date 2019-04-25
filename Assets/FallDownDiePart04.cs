﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownDiePart04 : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CastDamage();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Player")
    //    {
    //        CastDamage();
    //    }
    //}

    private void CastDamage()
    {
        PlayerTestController.instance.GetDamage(100);
       
        Invoke("DoPart04Restart", 0f);

    }

    private void DoPart04Restart()
    {
        GameObject part04 = GameObject.Find("SceneRoot/Part04");
        part04.GetComponent<Part04Restart>().DoPart04Restart();
    }
}
