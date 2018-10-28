using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Shoot")]
    public class ShootAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            Shoot(controller);
        }

        private void Shoot(AI.StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.eyes.position,
                            controller.eyes.forward.normalized *
                            controller.behaviorController.enemyStats.attackRange,
                            Color.red);

            if (Physics.SphereCast(controller.eyes.position,
                                  controller.behaviorController.enemyStats.lookSphereCastRadius,
                                  controller.eyes.forward,
                                  out hit,
                                  controller.behaviorController.enemyStats.shootingRange) &&
                                  hit.collider.CompareTag("Player"))
            {
                //controller.shooting.Fire();

                if (controller.CheckIfCountDownElapsed(controller.behaviorController.enemyStats.attackRate))
                {
                    controller.shooting.Fire();
                }
            }
        }
    }

}
