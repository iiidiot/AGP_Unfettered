using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class BubbleEffectController : MonoBehaviour {

    private Transform m_bg;
    private Transform m_content;
    private Transform m_nameTag;
    private Transform m_head;

    // Use this for initialization
    void Start () {

        m_content = this.transform.Find("bg/content");
        m_nameTag = this.transform.Find("bg/name");
        m_head = this.transform.Find("head");
        m_bg = this.transform.Find("bg");

        this.transform.DOScale(Vector3.zero, 0);
        
        this.m_content.DOScale(Vector3.zero, 0); // hide content
                                               
        this.m_head.GetComponent<Image>().DOFade(0, 0);  // hide head
       
        Sequence mySequence = DOTween.Sequence();                            //创建空序列  
        Vector3 max = new Vector3(1, 1, 1);

        Tweener move1 = this.transform.DOScale(max, 1f);       // scale up bubble
        Tweener move2 = m_content.DOScale(max, 0);               // show content
        Tweener show1 = m_head.GetComponent<Image>().DOFade(1, 1f);   // show head
        mySequence.Append(move1);
        mySequence.Append(move2);
        mySequence.Append(show1);
    }

    

    public void setSaySomething(string saySomething)
    {

        this.transform.Find("bg/content/text").GetComponent<TextMeshProUGUI>().text = saySomething;
        if (saySomething.Length > 80)
        {
            this.transform.Find("bg").GetComponent<RectTransform>().DOAnchorMax(new Vector2(0.95f, 0.85f), 1f);
        }
        else
        {
            this.transform.Find("bg").GetComponent<RectTransform>().DOAnchorMax(new Vector2(0.8f, 0.75f), 1f);
        }
    }

    public void setName(string name)
    {

        this.transform.Find("bg/name/text").GetComponent<TextMeshProUGUI>().text = name;

    }

    public void setHead(string path)
    {
        this.transform.Find("head").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        this.transform.Find("head").GetComponent<Image>().SetNativeSize();
    }




    // Update is called once per frame
    void Update () {
        m_bg.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        m_bg.GetComponent<RectTransform>().offsetMin = Vector2.zero;
    }
}
