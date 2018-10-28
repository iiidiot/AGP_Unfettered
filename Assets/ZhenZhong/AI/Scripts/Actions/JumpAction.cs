using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Jump")]
    public class JumpAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            Jump(controller);
        }

        private void Jump(AI.StateController controller)
        {
            Transform target = controller.jumpWaypoint;

            Vector3 targetPosition = target.position;
            Vector3 myPosition = controller.transform.position;

            float targetY = targetPosition.y;
            float myY = myPosition.y;

            float targetX = targetPosition.x;
            float myX = myPosition.x;

            float distance = 5.0f;

            float currentDist = Mathf.Abs(targetX - myX);

            // X coordinate
            if(currentDist <= distance)
            {
                // Y coordinate
                if (myY < targetY)
                {
                    controller.behaviorController.canJump = true;
                }
                else
                {
                    controller.behaviorController.canJump = false;
                }
            }

            
        }
    }

}