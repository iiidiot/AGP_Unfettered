using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

	[SerializeField]
	private GameObject resetPosition;
<<<<<<< HEAD
	[SerializeField]
	private Vector3 resetPositionp;
	[SerializeField]
=======
>>>>>>> 59fa10d7d1a3b089e4d2f6e1de2b6d6b9ebcdb3d
	private float moveDuration;
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.transform.position = new Vector3(resetPosition.transform.position.x, resetPosition.transform.position.y+2f, 0);
			//other.transform.localPosition = Vector3.Lerp(other.transform.position, resetPosition.transform.localPosition, moveDuration);
			//other.gameObject.transform.position = Vector3.MoveTowards(other.transform.position, resetPositionp, moveDuration);
		}

	}



       
 
}
