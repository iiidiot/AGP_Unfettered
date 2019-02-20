//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FieldObjectPlacer : MonoBehaviour
//{
//   // public GameObject prefab;

//    private GridManager m_gridManager;

//    private Vector3 m_mousePos;

//    public FarmItemsSO farmItems;
    
//    void Start ()
//    {
//        m_gridManager = GetComponent<GridManager>();
//	}

//	void Update ()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            RaycastHit hitInfo;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hitInfo))
//            {
//                m_mousePos = hitInfo.point;

//                if(m_gridManager.IsValidPosition(m_mousePos))
//                {
//                    PlaceObject();
//                }              
//            }
//        }
//    }

//    private void PlaceObject()
//    {
//        Vector3 pos = m_gridManager.GetCenterPosition(m_mousePos);

//        Instantiate(farmItems.AppleItems[0], pos, Quaternion.identity);

//        m_gridManager.SetVisited(m_mousePos);
//    }
//}
