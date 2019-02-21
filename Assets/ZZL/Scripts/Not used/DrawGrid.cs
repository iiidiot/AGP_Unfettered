//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DrawGrid : MonoBehaviour
//{

//    //private Material lineMat = new Material("Shader \"Lines/Colored Blended\" {" + "SubShader { Pass { " + "    Blend SrcAlpha OneMinusSrcAlpha " + "    ZWrite Off Cull Off Fog { Mode Off } " + "    BindChannels {" + "      Bind \"vertex\", vertex Bind \"color\", color }" + "} } }");
//    public Color c1 = Color.yellow;
//    public Color c2 = Color.red;
//    public int lengthOfLineRenderer = 20;

//    // Use this for initialization
//    void Start ()
//    {
//        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
//        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
//        lineRenderer.widthMultiplier = 0.2f;
//        lineRenderer.positionCount = lengthOfLineRenderer;

//        // A simple 2 color gradient with a fixed alpha of 1.0f.
//        float alpha = 1.0f;
//        Gradient gradient = new Gradient();
//        gradient.SetKeys(
//            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
//            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
//        );
//        lineRenderer.colorGradient = gradient;
//    }
	
//	// Update is called once per frame
//	void Update () {
		
//	}

//    void OnPostRender()
//    {

//        GL.Begin(GL.LINES);
//        //lineMat.SetPass(0);
//        GL.Color(new Color(0f, 0f, 0f, 1f));
//        GL.Vertex3(0f, 0f, 0f);
//        GL.Vertex3(1f, 1f, 1f);
//        GL.End();
//    }
//}
