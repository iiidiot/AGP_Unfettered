using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTransparentDisappear : MonoBehaviour {

public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool IsLoop;
    public float timeSpeed = 1;
    public float valSize = 1;

    [HideInInspector] public bool canUpdate;
    private float startTime;
	private Text text;
    

    // Use this for initialization
    void Start () {
		text = GetComponent<Text>();
	}
    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
    }


    // Update is called once per frame
    void Update () {

        var time = (Time.time - startTime)*timeSpeed;

        if (text.color.a >= 0.001)
        {

            var eval = LightCurve.Evaluate(time)*valSize;
			text.color = new Color( text.color.r, text.color.g, text.color.b, 1 - eval);


        }
    }
}
