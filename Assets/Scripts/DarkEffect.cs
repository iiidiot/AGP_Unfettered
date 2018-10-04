using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DarkEffect : MonoBehaviour
{
    public Transform item; // item has light

    private Material caveMaterial;
    private Renderer caveRender;
   
    private void OnEnable()
    {
        caveRender = this.GetComponent<Renderer>();
        caveMaterial = caveRender.sharedMaterial;
        
    }

    private void Update()
    {
        caveMaterial.SetVector("_ItemPos", item.position);
    }

    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{

    //    caveMaterial.SetVector("_ItemPos", item.position);

    //    Graphics.Blit(source, destination, caveMaterial);
    //}
}
