﻿using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class DisableGameObject : Action
    {
        public GameObject targetObject;

        public override TaskStatus OnUpdate()
        {

            targetObject.SetActive(false);

            return !targetObject.active ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}