using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class EnableGameObject : Action
    {
        public GameObject targetObject;

        public override TaskStatus OnUpdate()
        {

            targetObject.SetActive(true);

            return targetObject.active ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}
