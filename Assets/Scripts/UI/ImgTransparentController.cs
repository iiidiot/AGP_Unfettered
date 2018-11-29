using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgTransparentController : MonoBehaviour {

	public ImgTransparentAppear appear;
	public ImgTransparentDisappear disappear;

	void Update() {
		if(GameRunTimeStatus.MovieMode){
			appear.enabled = true;
			disappear.enabled = false;
		}
		if(!GameRunTimeStatus.MovieMode){
			appear.enabled = false;
			disappear.enabled = true;
		}
	}
}
