using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This class handles the resolution of the Inventory when
    the screen size is changed. It will prevent loss of the pixel
    and set a more accurate result no matter what screen size is.
*/
public class HandleCanvas : MonoBehaviour
{
    private CanvasScaler m_scaler;

	// Use this for initialization
	void Start ()
    {
        m_scaler = GetComponent<CanvasScaler>();

        // At the beginning, the Canvas is set to be "Constant Pixel Size".
        // After the game runs, set it to "Scale With Screen Size" will fix the issue.
        m_scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
	}
}
