using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnOnClick : MonoBehaviour {

	public void LoadScene(string name){
		GameRunTimeStatus.MovieMode = false;
		PlayerStatus.TutorialStatus = 0;
		PlayerStatus.Health = 10;
		SceneManager.LoadScene(name);
	}

	public void Quit(){
		Application.Quit();
	}

	public void ReTry(string name){
		GameRunTimeStatus.MovieMode = false;
		PlayerStatus.TutorialStatus = 10;
		PlayerStatus.Health = 10;
		SceneManager.LoadScene(name);
	}
}
