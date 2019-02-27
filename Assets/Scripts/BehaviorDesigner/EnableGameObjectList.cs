using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class EnableGameObjectList : Action
    {
        public SharedGameObjectList targetObjectList;

        public override TaskStatus OnUpdate()
        {
            bool status = true;
            foreach (var go in targetObjectList.Value)
            {
                go.SetActive(true);
                status &= go.active;
            }

            return status ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}
