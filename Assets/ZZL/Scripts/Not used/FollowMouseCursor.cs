//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FollowMouseCursor : MonoBehaviour
//{
//    private float moveSpeed = 0.01f;
//    private Vector3 mousePosition;

//    // Use this for initialization
//    void Start ()
//    {
		
//	}
	
//	// Update is called once per frame
//	void Update ()
//    {
//        mousePosition = Input.mousePosition;
//        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
//        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
//    }
//}
