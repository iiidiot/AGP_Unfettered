using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallDiePart3 : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            CastDamage();
        }
    }

    private void DoPart03Restart()
    {
        GameObject part03 = GameObject.Find("SceneRoot/Part03");
        part03.GetComponent<Part03Restart>().DoPart03Restart();
    }

    private void CastDamage()
    {
        PlayerTestController.instance.GetDamage(100);
        Invoke("DoPart03Restart", 1f);

    }

  
}
