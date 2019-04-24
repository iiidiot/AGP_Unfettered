using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallDiePart3 : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CastDamage();

        }
    }

    private static void DoPart3Restart()
    {


        CharactersConfigManager.GetPlayerGameObject().transform.position = GameRunTimeStatus.RebornPlace;


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

    private void CastDamage()
    {
        PlayerTestController.instance.GetDamage(100);
        Invoke("DoPart3Restart", 1f);

    }

  
}
