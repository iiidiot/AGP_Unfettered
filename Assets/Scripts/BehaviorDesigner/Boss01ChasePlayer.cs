using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
    public class Boss01ChasePlayer : Action
    {

        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;

        //[SharedRequired]
        [Tooltip("The NavMeshAgent destination")]
        public SharedGameObject playerObject;

        private GameObject prevGameObject;
        private GameObject playerGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                prevGameObject = currentGameObject;
            }

            playerGameObject = GetDefaultGameObject(playerObject.Value);
        }

        public override TaskStatus OnUpdate()
        {
            
            //if(playerGameObject.transform.position.x < prevGameObject.transform.position.x)
            //{
               // Vector3 pos = prevGameObject.transform.position;
               // prevGameObject.transform.position = new Vector3(pos.x - 0.1f, pos.y, pos.z);

            //prevGameObject.GetComponent<Rigidbody>().velocity = new Vector3(-5, 0, 0);

            prevGameObject.GetComponent<Boss1ChaseController>().x_Velocity = -5;
            //}

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            playerObject = null;
        }
    }
}
