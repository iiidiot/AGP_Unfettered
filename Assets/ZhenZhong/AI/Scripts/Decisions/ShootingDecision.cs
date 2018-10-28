using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Decisions/Shooting")]
    public class ShootingDecision : AI.Decision
    {
        public override bool Decide(AI.StateController controller)
        {
            return ShouldFire(controller);
        }

        private bool ShouldFire(AI.StateController controller)
        {
            Transform target = controller.chaseTarget;

            Vector3 targetPosition = target.transform.position;
            Vector3 myPosition = controller.transform.position;

            Vector3 result = targetPosition - myPosition;
            float currentDist = result.magnitude;

            // Not calculating the direction of two points for now, 
            // only calculating the x coordinate because the player can be on top of the enemy
            //float currentDist = Mathf.Abs(targetPosition.x - myPosition.x);

            if (currentDist <= controller.behaviorController.enemyStats.shootingRange)
            {
                Debug.Log("Shooting!!!!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

