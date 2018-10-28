using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public enum MoveType
    {
        PATROL,
        CHASE,

        NONE
    };

    public class BehaviorController : MonoBehaviour
    {       
        public AI.EnemyStats enemyStats;

        [HideInInspector]
        public bool canJump;

        [HideInInspector]
        public bool isMoving;

        [HideInInspector]
        public bool facingRight;

        [HideInInspector]
        public bool targetReached;

        [HideInInspector]
        public bool canRotate;

        [HideInInspector]
        public MoveType moveType { get; set; }

        private Rigidbody m_rigidbody;

        // Use this for initialization
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            isMoving = false;
            facingRight = true;
            canRotate = false;

            targetReached = false;
        }

        // Update is called once per frame
        void Update()
        {    
            OnRotate();
        }

        private void FixedUpdate()
        {
            OnMove();
            OnJump();
        }

        private void OnJump()
        {
            if(canJump)
            {
                Vector3 jumpDirection = Vector3.up * enemyStats.jumpSpeed;

                m_rigidbody.AddForce(jumpDirection, ForceMode.VelocityChange);
            }
            else
            {
                Vector3 jumpDirection = Vector3.down * enemyStats.jumpSpeed;

                m_rigidbody.AddForce(jumpDirection, ForceMode.VelocityChange);
            }
        }

        private void OnMove()
        {
            if (isMoving)
            {
                if (facingRight)
                {
                    if(moveType == MoveType.PATROL)
                    {
                        m_rigidbody.AddForce(new Vector3(enemyStats.moveSpeed, 0, 0), ForceMode.Impulse);
                    }
                    else if(moveType == MoveType.CHASE)
                    {
                        m_rigidbody.AddForce(new Vector3(enemyStats.chaseSpeed, 0, 0), ForceMode.Impulse);
                    }
                }
                else
                {
                    if (moveType == MoveType.PATROL)
                    {
                        m_rigidbody.AddForce(new Vector3(-enemyStats.moveSpeed, 0, 0), ForceMode.Impulse);
                    }
                    else if (moveType == MoveType.CHASE)
                    {
                        m_rigidbody.AddForce(new Vector3(-enemyStats.chaseSpeed, 0, 0), ForceMode.Impulse);
                    }            
                }
            }
        }

        private void OnRotate()
        {
            if(canRotate)
            {
                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * enemyStats.rotateSpeed, 180);

                if(!targetReached)
                {
                    facingRight = !facingRight;
                }
            }

            else if (targetReached)
            {
                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);
                facingRight = !facingRight;

                targetReached = false;
            }
        }
    }
}
