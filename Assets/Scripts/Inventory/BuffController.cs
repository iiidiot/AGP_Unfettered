using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public IEnumerator AddFireAttribute( double data, double time){
		PlayerStatus.FireAttack += data;
		yield return new WaitForSeconds((float)time);
		PlayerStatus.FireAttack -= data;
	}


	
}
