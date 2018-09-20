using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCTalk : MonoBehaviour
{
    public float Y_offset = 80;

    private GameObject dialog;
    private string[] allLines;
    private int count = 0;
    private int timeDelay = 0;
    private const int gap = 300;

    // Use this for initialization
    void Start()
    {
        dialog = GameObject.Find("Dialog");
        string path = "Text/Talk/" + gameObject.name;
        string myTxt = ((TextAsset)Resources.Load(path)).text;
        allLines = myTxt.Split('\n');
        //dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    dialog.SetActive(true);
        //}
        if (timeDelay++ % gap == 0 && count < allLines.Length)
        {
            string content = allLines[count++];
            if (content[0] == 'p') {        // player talk
                dialog.GetComponent<Text>().color = Color.red;
                dialog.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position) + new Vector3(0, Y_offset, 0);

            }
            else {      // npc talk
                dialog.GetComponent<Text>().color = Color.gray;
                dialog.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, Y_offset, 0);

            }
            content = content.Substring(2);
            dialog.GetComponent<Text>().text = content;
        }

    }

}
