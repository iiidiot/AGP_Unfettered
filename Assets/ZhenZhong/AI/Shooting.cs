using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Shooting : MonoBehaviour
    {
        public GameObject spawnPoint;

        public float fireTime = 0.05f;

        void Start()
        {
            //InvokeRepeating("Fire", fireTime, fireTime);
        }

        public void Fire()
        {
            GameObject obj = ObjectPoolerScript.current.GetPooledObject();

            if(!obj)
            {
                return;
            }

            obj.transform.position = spawnPoint.transform.position;
            obj.transform.rotation = spawnPoint.transform.rotation;
            obj.SetActive(true);
        }
    }

}
