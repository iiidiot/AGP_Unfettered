using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial02 : MonoBehaviour
{
    bool inTutorial = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (PlayerStatus.TutorialStatus == 2)
            {
                GameRunTimeStatus.MovieMode = true;
                PlayerStatus.TutorialStatus = 2;
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
            if (Input.GetKeyDown(KeyCode.Space) && PlayerStatus.TutorialStatus == 3)
            {
                GameRunTimeStatus.MovieMode = false;
                PlayerStatus.TutorialStatus = 4;
                List<int> blockstate = new List<int>();
                blockstate.Add(0);
                blockstate.Add(3);
                PlayerTestController.instance.UnblockPlayerInput(blockstate);
                PlayerTestController.instance.playerJump = true;
            }
        }
    }
}
