using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotate : MonoBehaviour {

    public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool IsLoop;
    public float degree;

    [HideInInspector] public bool canUpdate;
    private float startTime;
    

    // Use this for initialization
    void Start () {
		
	}
    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
    }


    // Update is called once per frame
    void Update () {
        var factor = 1.0f;
        if(Mathf.Abs(transform.localEulerAngles.y) > 50)
        {
            factor = 3.2f;
        }
        var time = Time.time - startTime;
        if (canUpdate)
        {
            var eval = LightCurve.Evaluate(time);
            transform.Rotate(0, eval * degree * factor * Time.deltaTime, 0, Space.World);


        }
        if (time >= 1)
        {
            if (IsLoop) startTime = Time.time;
            else canUpdate = false;
        }
    }
}
