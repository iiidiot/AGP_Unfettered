﻿using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class DisableGameObjectList : Action
    {
        public SharedGameObjectList targetObjectList;

        public override TaskStatus OnUpdate()
        {
            bool status = false;
            foreach (var go in targetObjectList.Value)
            {
                go.SetActive(false);
                status |= go.active;
            }

            return !status ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}