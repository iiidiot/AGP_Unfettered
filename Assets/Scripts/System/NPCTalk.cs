using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCTalk : MonoBehaviour
{
    public float Y_offset = 200;

    private GameObject dialog;
    private GameObject image;
    private string[] allLines;
    private int count = 0;
    private float tempTimer;
    char previousTalker = 'a';


    // Use this for initialization
    void Start()
    {
        dialog = GameObject.Find("Dialog");
        image = GameObject.Find("Image");
        string path = "Text/Talk/" + gameObject.name;
        string myTxt = ((TextAsset)Resources.Load(path)).text;
        allLines = myTxt.Split('\n');
        dialog.SetActive(false);
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.Mouse0)))
        {

            dialog.SetActive(true);
            image.SetActive(true);

            if (count < allLines.Length)
            {
                string content = allLines[count++];
                if (content[0] != previousTalker) {
                    image.GetComponent<ImageControl>().flag = true;
                }
                previousTalker = content[0];
                if (content[0] == 'p')
                {        // player talk
                    dialog.GetComponent<Text>().color = Color.red;
                    image.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position) + new Vector3(0, Y_offset, 0);

                }
                else
                {      // npc talk
                    dialog.GetComponent<Text>().color = Color.gray;
                    image.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, Y_offset, 0);

                }
                content = content.Substring(2);
                dialog.GetComponent<Text>().text = content;
            }
        }
    }

}
