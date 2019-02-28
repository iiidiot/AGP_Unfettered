using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Basic/GameObject")]
    [TaskDescription("Instantiates a new GameObject. Returns Success.")]
    public class DropItem : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The position of the new GameObject")]
        public SharedGameObject positionObject;


        public override TaskStatus OnUpdate()
        {
            if (targetGameObject.Value)
            {
                GameObject.Instantiate(targetGameObject.Value, positionObject.Value.transform.position, Quaternion.identity);
            }


            return TaskStatus.Success;
        }


    }
}