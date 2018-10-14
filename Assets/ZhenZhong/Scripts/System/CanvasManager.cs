//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//namespace CanvasManager
//{
//    public class CanvasManager : MonoBehaviour
//    {
//        public static CanvasManager instance = null;

//        void Awake()
//        {
//            if (!instance)
//            {
//                instance = this;
//            }
//        }

//        // Use this for initialization
//        void Start()
//        {

//        }

//        // Update is called once per frame
//        void Update()
//        {
//            FindTarget();
//        }

//        public static bool FindTarget()
//        {
//            PointerEventData pointer = new PointerEventData(EventSystem.current);
//            pointer.position = Input.mousePosition;

//            List<RaycastResult> raycastResults = new List<RaycastResult>();
//            EventSystem.current.RaycastAll(pointer, raycastResults);

//            if (raycastResults.Count > 0)
//            {
//                foreach (var go in raycastResults)
//                {
//                    Debug.Log(go.gameObject.tag);

//                    if(go.gameObject.tag == "Canvas")
//                    {
//                        GameObject obj = go.gameObject;
//                        obj = GameObject.FindWithTag("SwordInventory");

//                        Debug.Log(obj.name);
//                    }

//                    if (go.gameObject.name == "SwordSlot")
//                    {
//                        Debug.Log("Sword Inventory is found!!");
                        
//                    }

//                    if (go.gameObject.tag == "SwordInventory")
//                    {
//                        Debug.Log("Sword Inventory is found!!");
 
//                    }
//                }
//            }

//            return false;
//        }
//    }
//}
