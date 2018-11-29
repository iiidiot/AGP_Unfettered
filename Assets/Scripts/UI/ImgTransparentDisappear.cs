using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgTransparentDisappear : MonoBehaviour {
public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool IsLoop;
    public float timeSpeed = 1;
    public float valSize = 1;

    [HideInInspector] public bool canUpdate;
    private float startTime;
	private Image img;
    

    // Use this for initialization
    void Start () {
		img = GetComponent<Image>();
	}
    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
    }


    // Update is called once per frame
    void Update () {

        var time = (Time.time - startTime)*timeSpeed;

        if (img.color.a <= 1)
        {
            var eval = LightCurve.Evaluate(time)*valSize;
			img.color = new Color( img.color.r, img.color.g, img.color.b, 1 - eval);
        }
    }
}
