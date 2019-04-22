using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part04Restart : MonoBehaviour {

    public Transform boss;
    public Transform bossBornPlace;
    public GameObject part04Camera;
    public void DoPart04Restart()
    {
        RestoreFragileStones();

        ProCamera2D.Instance.Reset();

        boss.Find("boss01Anime01").GetComponent<Rigidbody>().position = bossBornPlace.position;
        CharactersConfigManager.GetPlayerGameObject().transform.position = GameRunTimeStatus.RebornPlace;
    }

    private void RestoreFragileStones()
    {
        Transform platformGroup = this.transform.Find("EnvRoot/PlatformGroup");
        for (int i = 0; i < platformGroup.childCount; i++)
        {
            GameObject o = platformGroup.GetChild(i).gameObject;
            o.SetActive(true);
            if (o.GetComponent<FragileStone>())
            {
                o.GetComponent<FragileStone>().Restore();
            }
          
               
            
        }
    }
}
