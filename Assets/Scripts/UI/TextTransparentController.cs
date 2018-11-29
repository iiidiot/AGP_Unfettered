using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTransparentController : MonoBehaviour {

	public TextTransprentAppear appear;
	public TextTransparentDisappear disappear;

	public int tutorialNum = 0;

	void Update() {
		if(GameRunTimeStatus.MovieMode && PlayerStatus.TutorialStatus == tutorialNum){
			appear.enabled = true;
			disappear.enabled = false;

			PlayerStatus.TutorialStatus++;
		}
		if(!GameRunTimeStatus.MovieMode){
			appear.enabled = false;
			disappear.enabled = true;
		}
	}
}
