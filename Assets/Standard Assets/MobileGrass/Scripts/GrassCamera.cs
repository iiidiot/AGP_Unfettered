using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCamera : MonoBehaviour {
    public Material grassMat;
    public int textureSize = 512;
    RenderTexture texture;
    // Use this for initialization
    Matrix4x4 matVP;
    Camera c;
	void Start () {
        texture = new RenderTexture(textureSize, textureSize, 0);
        c = GetComponent<Camera>();
        c.targetTexture = texture;
        matVP = GL.GetGPUProjectionMatrix(c.projectionMatrix, true) * c.worldToCameraMatrix;
        grassMat.SetMatrix("GrassMatrix", matVP);
        c.cullingMask = (1 << LayerMask.NameToLayer("Units"));
    }
	
	// Update is called once per frame
	void Update () {
        grassMat.SetTexture("_GrassTex", texture);
        matVP = GL.GetGPUProjectionMatrix(c.projectionMatrix, true) * c.worldToCameraMatrix;
        grassMat.SetMatrix("GrassMatrix", matVP);
    }
}
