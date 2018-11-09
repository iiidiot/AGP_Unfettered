using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    private Canvas m_canvas;

    void Start()
    {
        m_canvas = GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        HandleInput();
	}

    private void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            m_canvas.enabled = !m_canvas.enabled;
        }
    }
}
