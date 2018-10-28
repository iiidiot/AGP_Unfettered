using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Idea: 
        Object Pooling: prevent destroying and creating objects all the time
                        because that will slow down the system.
                        Storing all the bullets together and just keep using
                        the same bullets all the time
                            
*/
namespace AI
{
    public class BulletDestroy : MonoBehaviour
    {
        private void OnEnable()
        {
            Invoke("Destroy", 2.0f);
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            // Prevent double disable, or enable immediately following disable
            CancelInvoke();
        }
    }

}