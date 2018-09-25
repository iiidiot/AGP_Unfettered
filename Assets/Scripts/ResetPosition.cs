using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

	[SerializeField]
	private Vector3 resetPosition;

	[SerializeField]
	private float moveDuration;
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.transform.localPosition = Vector3.Lerp(other.transform.position, resetPosition, moveDuration);
		}
		Vector3 vec= transform.InverseTransformPoint(transform.position);
		Debug.Log("relative:"+transform.position);
		Debug.Log("absolute:"+vec);
	}



       
 
}
