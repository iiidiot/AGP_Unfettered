using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 15.0f;

        void Update()
        {
            MovePosition();
        }

        private void MovePosition()
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

}
