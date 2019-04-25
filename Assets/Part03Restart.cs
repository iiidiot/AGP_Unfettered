using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Part03Restart : MonoBehaviour {

    public GameObject part03Camera;
    public GameObject fireWall;

    public void DoPart03Restart()
    {
        ProCamera2DTransitionsFX.Instance.TransitionExit();

        fireWall.GetComponent<FireWallDestruct>().Restore();

        Invoke("AfterTransitionExitComplete", 0.5f);  // 0.5f is the transition exit duration
    }

    public void AfterTransitionExitComplete()
    {
        ResetGameElements();
        RestoreFragileStones();
        ProCamera2DTransitionsFX.Instance.TransitionEnter();
    }

    private void ResetGameElements()
    {
        ProCamera2D.Instance.Reset();

        PlayerTestController.instance.Restart();
        CharactersConfigManager.GetPlayerGameObject().transform.position = GameRunTimeStatus.RebornPlace;
    }



    private void RestoreFragileStones()
    {

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
