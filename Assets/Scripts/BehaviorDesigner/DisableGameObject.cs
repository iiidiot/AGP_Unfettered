using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class DisableGameObject : Action
    {
        public SharedGameObject targetObject;

        public override TaskStatus OnUpdate()
        {

            targetObject.Value.SetActive(false);

            return !targetObject.Value.active ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}
