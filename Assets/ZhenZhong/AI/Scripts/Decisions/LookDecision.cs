using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Decisions/Look")]
    public class LookDecision : AI.Decision
    {
        public override bool Decide(AI.StateController controller)
        {
            return Look(controller);
        }

        private bool Look(AI.StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.eyes.position,
                          controller.eyes.forward.normalized *
                          controller.behaviorController.enemyStats.lookRange,
                          Color.green);

            if(Physics.SphereCast(controller.eyes.position,
                                  controller.behaviorController.enemyStats.lookSphereCastRadius,
                                  controller.eyes.forward,
                                  out hit,
                                  controller.behaviorController.enemyStats.lookRange) &&
                                  hit.collider.CompareTag("Player"))
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
