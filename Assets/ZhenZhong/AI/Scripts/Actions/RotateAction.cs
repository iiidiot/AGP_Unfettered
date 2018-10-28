using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Rotate")]
    public class RotateAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            Rotate(controller);
        }

        private void Rotate(AI.StateController controller)
        {
            if(!IsInfrontOfMe(controller.chaseTarget, controller.transform))
            {
                controller.behaviorController.canRotate = true;
            }
            else
            {
                controller.behaviorController.canRotate = false;
            }
        }

        private bool IsInfrontOfMe(Transform target, Transform myself)
        {
            Vector3 myPosition = myself.position;
            Vector3 targetPosition = target.position;

            Vector3 dir = targetPosition - myPosition;

            Vector3 forwardVector = myself.forward;

            // in front of me
            if (Vector3.Dot(dir, forwardVector) >= 0.5f)
            {
                return true;
            }
            else if (Vector3.Dot(dir, forwardVector) <= -0.5f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
