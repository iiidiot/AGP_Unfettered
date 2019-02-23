using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LayerSetting : MonoBehaviour {

	// Use this for initialization
	void Awake () {
#if UNITY_EDITOR
        LayerUtil.CreateLayer("Units");
        LayerUtil.CreateLayer("Terrain");
        LayerUtil.CreateLayer("CameraDragLayer");
#endif
        Camera c = gameObject.GetComponent<Camera>();
        c.cullingMask = ~(1 << LayerMask.NameToLayer("Units"));
    }
}
