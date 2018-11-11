using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodSliderController : MonoBehaviour {

   
    public Slider slider;


    private float targetValue;
    public float slideSpeed = 0.5f;
    public Text number;

    // Use this for initialization
    void Start () {
        slider = this.transform.Find("Slider").GetComponent<Slider>();
        slider.value = (float)(PlayerStatus.Health / PlayerStatus.MaxHealth);
    }

    void Update()
    {

        number.text = PlayerStatus.Health.ToString();


        targetValue = (float) (PlayerStatus.Health / PlayerStatus.MaxHealth);

        if (targetValue != slider.value)
        {
            //插值运算  
            slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * slideSpeed);
            if (Mathf.Abs(slider.value - targetValue) < 0.01f)
            {
                slider.value = targetValue;
            }
        }

        //((int)(slider.value * 100)).ToString() + "%";
    }

    public void updateHealthDisplay()
    {

    }

}
