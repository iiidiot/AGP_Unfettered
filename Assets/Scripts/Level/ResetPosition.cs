using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

	[SerializeField]
	private GameObject resetPosition;
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.transform.position = new Vector3(resetPosition.transform.position.x, resetPosition.transform.position.y+2f, 0);
			PlayerTestController.instance.GetDamage(2);
			//other.transform.localPosition = Vector3.Lerp(other.transform.position, resetPosition.transform.localPosition, moveDuration);
			//other.gameObject.transform.position = Vector3.MoveTowards(other.transform.position, resetPositionp, moveDuration);
		}

	}



       
 
}
