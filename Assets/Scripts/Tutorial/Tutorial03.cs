using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial03 : MonoBehaviour
{

    bool inTutorial = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (PlayerStatus.TutorialStatus == 4)
            {
                GameRunTimeStatus.MovieMode = true;
                PlayerStatus.TutorialStatus = 4;
                List<int> blockstate = new List<int>();
                blockstate.Add(0);
                PlayerTestController.instance.BlockPlayerInput(blockstate);
                inTutorial = true;
            }
        }
    }
    void Update()
    {
        if (inTutorial)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerStatus.TutorialStatus == 5)
            {
                GameRunTimeStatus.MovieMode = false;
                PlayerStatus.TutorialStatus = 6;
                List<int> blockstate = new List<int>();
                blockstate.Add(0);
                PlayerTestController.instance.UnblockPlayerInput(blockstate);
                PlayerTestController.instance.playerAttack = true;
				PlayerTestController.instance.m_animator.SetTrigger("attack");
            }
        }
    }
}
