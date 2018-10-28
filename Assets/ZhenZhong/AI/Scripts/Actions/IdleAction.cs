using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Idle")]
    public class IdleAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            Idle(controller);
        }

        private void Idle(AI.StateController controller)
        {
            controller.behaviorController.isMoving = false;
        }
    }

}
