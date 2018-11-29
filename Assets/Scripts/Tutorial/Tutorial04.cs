using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial04 : MonoBehaviour
{
	public TextTransparentController t1;
	public TextTransparentController t2;
    bool inTutorial = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (PlayerStatus.TutorialStatus == 6)
            {
                GameRunTimeStatus.MovieMode = true;
                PlayerStatus.TutorialStatus = 6;
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
            if (Input.GetKeyDown(KeyCode.C) && PlayerStatus.TutorialStatus == 7)
            {
                GameRunTimeStatus.MovieMode = true;
                PlayerStatus.TutorialStatus = 8;
				inTutorial = true;
				t1.appear.enabled = false;
				t1.disappear.enabled = true;
            }
			if (Input.GetKeyUp(KeyCode.Mouse0) && PlayerStatus.TutorialStatus == 9)
            {
                GameRunTimeStatus.MovieMode = true;
                PlayerStatus.TutorialStatus = 10;
				inTutorial = true;
				t2.appear.enabled = false;
				t2.disappear.enabled = true;
            }
			if (Input.GetKeyDown(KeyCode.C) && PlayerStatus.TutorialStatus == 11)
            {
                GameRunTimeStatus.MovieMode = false;
                PlayerStatus.TutorialStatus = 12;
				inTutorial = false;
				List<int> blockstate = new List<int>();
                blockstate.Add(0);
                PlayerTestController.instance.UnblockPlayerInput(blockstate);
            }
        }
    }
}
