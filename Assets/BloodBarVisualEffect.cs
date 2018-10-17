using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode]
public class BloodBarVisualEffect : MonoBehaviour
{

    public Color color1;
    public Color color2;
    private Image image;
    
    public bool displayColor2;

    // Use this for initialization
    void Start()
    {
        image = this.GetComponent<Image>();
        image.color = color1;
        image.DOColor(color2, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR

        image.color = displayColor2 ? color2 : color1;
#endif

    }

}
