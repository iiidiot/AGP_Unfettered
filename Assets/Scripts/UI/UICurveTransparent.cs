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
        text = GetComponent<Text>();
    }
    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameRunTimeStatus.UIBlackShelterMoveIn)
        {
            DisappearText();
        }
        else
        {
           DisappearText();
        }
    }

    void AppearText()
    {
        if (canUpdate)
        {
            var time = Time.time - startTime;
            var eval = LightCurve.Evaluate(time);
            text.color = new Color( text.color.r, text.color.g, text.color.b, eval * degree);
        }

         canUpdate = text.color.a >= 1 ? false : true;
    }

    void DisappearText()
    {
        if (canUpdate)
        {
            var time = Time.time - startTime;
            var eval = LightCurve.Evaluate(time);
            text.color = new Color( text.color.r, text.color.g, text.color.b, 1 - eval * degree);
        }
        canUpdate = text.color.a <= 0 ? false : true;
    }



}
