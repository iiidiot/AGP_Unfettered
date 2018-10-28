using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/State")]
    public class State : ScriptableObject
    {
        public AI.Action[] actions;
        public AI.Transition[] transitions;

        public Color sceneGizmoColor = Color.grey;

        public void UpdateState(AI.StateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }
        
        private void DoActions(AI.StateController controller)
        {
            for(int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }

        private void CheckTransitions(AI.StateController controller)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceeded = transitions[i].decision.Decide(controller);

                if (decisionSucceeded)
                {
                    controller.TransitionToState(transitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
    }
}

