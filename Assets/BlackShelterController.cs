using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackShelterController : MonoBehaviour {

	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool IsLoop;
    public float degree;

	private GameObject topShelter;

	private GameObject bottomShelter;
    [HideInInspector] public bool canUpdate;
    private float startTime;
    

    // Use this for initialization
    void Start () {
		topShelter = transform.GetChild(0).gameObject;
		bottomShelter = transform.GetChild(1).gameObject;
	}
    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
    }


    // Update is called once per frame
    void Update () {

        var time = Time.time - startTime;

        if (canUpdate)
        {

            var eval = LightCurve.Evaluate(time);
			float topHeight = topShelter.GetComponent<RectTransform>().rect.height;
			float bottomHeight = bottomShelter.GetComponent<RectTransform>().rect.height;

			topShelter.GetComponent<RectTransform>().sizeDelta = new Vector2(0, topHeight - eval * degree) ;
			bottomShelter.GetComponent<RectTransform>().sizeDelta = new Vector2(0, bottomHeight - eval * degree) ;
			canUpdate = topHeight <= 0 ? false : true; 


            //transform.Rotate(0, eval * degree * factor * Time.deltaTime, 0, Space.World);


        }
        // if ( topHeight < = 0)
        // {
        //     //if (IsLoop) startTime = Time.time;
        //     else canUpdate = false;
        // }
    }
}
