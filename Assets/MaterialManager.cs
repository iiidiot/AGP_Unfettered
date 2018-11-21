using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {

    
    public MaterialTable[] materialTable;

   

    [System.Serializable]
    public struct MaterialTable
    {
        public GameObject gameObject;
        public Material ordinary;
        public Material black;
    }

   

    public void DrawingMode()
    {
        foreach(MaterialTable m in materialTable)
        {
            m.gameObject.GetComponent<SkinnedMeshRenderer>().material = m.black;
        }

    }

    public void OrdinaryMode()
    {
        foreach (MaterialTable m in materialTable)
        {
            m.gameObject.GetComponent<SkinnedMeshRenderer>().material = m.ordinary;
        }
    }


}
