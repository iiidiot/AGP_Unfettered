using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class EnableGameObject : Action
    {
        public SharedGameObject targetObject;

        public override TaskStatus OnUpdate()
        {

            targetObject.Value.SetActive(true);

            return targetObject.Value.active ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}
