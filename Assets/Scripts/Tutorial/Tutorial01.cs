using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial01 : MonoBehaviour {
    
    public GameObject blackRect;
    bool isInTutorial = true;

	void Update()
    {
			if(PlayerStatus.TutorialStatus == 0){
				GameRunTimeStatus.MovieMode = true;
                PlayerStatus.TutorialStatus = 0;
                List<int> blockstate = new List<int>();
                blockstate.Add(0);
                blockstate.Add(3);
                PlayerTestController.instance.BlockPlayerInput(blockstate);
                blackRect.SetActive(true);
			}
            
            if(Input.GetAxisRaw("Horizontal")!=0 && PlayerStatus.TutorialStatus == 1){
				GameRunTimeStatus.MovieMode = false;
                PlayerStatus.TutorialStatus = 2;
                List<int> blockstate = new List<int>();
                blockstate.Add(0);
                PlayerTestController.instance.UnblockPlayerInput(blockstate);
			}
    }
}
