using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Decisions/FindGround")]
    public class FindGroundDecision : Decision
    {
        public override bool Decide(AI.StateController controller)
        {
            return FindGround(controller);
        }

        private bool FindGround(AI.StateController controller)
        {
            RaycastHit hit;
            
            Ray landingRay = new Ray(controller.eyes.transform.position, Vector3.down);

            Debug.DrawRay(controller.eyes.transform.position, Vector3.down * 1.0f);

            if (Physics.Raycast(landingRay, out hit, 1.0f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


