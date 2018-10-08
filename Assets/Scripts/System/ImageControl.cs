using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageControl : MonoBehaviour
{
    // Use this for initialization
    Vector3 previousScale;
    Transform tranf;
    public AnimationCurve curve;
    public float duration = 1;
    public bool flag = false;
    float n;
    //public bool fadingOut = false;
    public Image image;
    float counter = 0;
    Color currentColor;
    Color visibleColor;

    void Start()
    {
        var tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
        currentColor = image.color;
        visibleColor = image.color;
        visibleColor.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == true) {
            Reset();
            flag = false;
        }
        //if (fadingOut == true)       // fade out
        //{
        //    //Fully fade in Image (1) with the duration of 2
        //    image.CrossFadeAlpha(0f, duration, false);
        //    fadingOut = false;
        //    resetColor();
        //}
        n = Time.deltaTime * 10 / duration;
        tranf.localScale = Vector3.Lerp(this.transform.localScale, previousScale, curve.Evaluate(n));   // zoom in
     
        if (counter < duration) {       //fade in
            counter += Time.deltaTime;
            image.color = Color.Lerp(currentColor, visibleColor, counter / duration);
        }
    }

    void Reset()
    {
        previousScale = this.transform.localScale;
        tranf = GetComponent<RectTransform>();
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

    }
}

