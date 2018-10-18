//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//namespace EnemyEditor
//{
//    public enum AttackType
//    {
//        Tackle,
//        Projectile,
//        Shoot
//    }

//    public class MyScript : MonoBehaviour
//    {
//        public bool flag;
//        public AttackType attackType;
//        public Transform gunEnd;
//        public GameObject bullet;
//    }



//    [CustomEditor(typeof(MyScript))]
//    public class EnemyCustomEditor : Editor
//    {
//        public override void OnInspectorGUI()
//        {
//            var myScript = target as MyScript;

//            myScript.flag = GUILayout.Toggle(myScript.flag, "Flag");

//            if (myScript.flag)
//            {
//                myScript.attackType = (AttackType)EditorGUILayout.EnumFlagsField(myScript.attackType);
//            }
//        }
//    }
//}



