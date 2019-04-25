﻿using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part04Restart : MonoBehaviour {

    public Transform boss;
    public Transform bossBornPlace;
    public GameObject part04Camera;
    public GameObject fireWall;
    public GameObject bgm;


    public void DoPart04Restart()
    {
        ProCamera2DTransitionsFX.Instance.TransitionExit();
        

        fireWall.GetComponent<FireWallDestruct>().Restore();

        Invoke("AfterTransitionExitComplete", 0.5f);  // 0.5f is the transition exit duration
    }

    public void AfterTransitionExitComplete()
    {
        RestartBGM();
        ResetGameElements();
        RestoreFragileStones();
        ProCamera2DTransitionsFX.Instance.TransitionEnter();
    }

    private void RestartBGM()
    {
        bgm.GetComponent<AudioSource>().Stop();
        bgm.GetComponent<AudioSource>().Play();
    }

    private void ResetGameElements()
    {
        ProCamera2D.Instance.Reset();
        boss.Find("boss01Anime01").GetComponent<Rigidbody>().position = bossBornPlace.position;
        boss.Find("boss01Anime01").GetComponent<Boss1ChaseController>().Initializations();
        PlayerTestController.instance.Restart();
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
