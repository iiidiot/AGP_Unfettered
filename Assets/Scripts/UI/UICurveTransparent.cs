using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurveTransparent : MonoBehaviour {
    public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool IsLoop;
    public float degree;

    [HideInInspector] public bool canUpdate;
    private float startTime;
    private Text text;


    // Use this for initialization
    void Start()
    {

    }
    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
    }


    // Update is called once per frame
    void Update()
    {
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
