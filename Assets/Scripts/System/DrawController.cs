using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour {

    public float time_scale = 0;
    public bool isDrawing = false;
    public GameObject FuDrawPanel;

	// Use this for initialization
	void Start () {
        Camera.main.GetComponent<Painting>().enabled = false;
        isDrawing = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isDrawing)
            {
                DisableDraw();
            }
            else
            {
                EnableDraw();
            }
        }
	}

    void EnableDraw()
    {
        Camera.main.GetComponent<Painting>().enabled = true;
        Camera.main.GetComponent<Painting>().Clear();

        FuDrawPanel.SetActive(true);

        Time.timeScale = time_scale;

        isDrawing = true;
    }

    void DisableDraw()
    {
        Camera.main.GetComponent<Painting>().enabled = false;
        Camera.main.GetComponent<Painting>().Clear();

        FuDrawPanel.SetActive(false);

        Time.timeScale = 1;

        isDrawing = false;
    }
}
