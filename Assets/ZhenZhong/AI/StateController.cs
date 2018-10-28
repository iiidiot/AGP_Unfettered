using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class StateController : MonoBehaviour
    {
        //public Transform target;
        public List<Transform> wayPointList;

        public Transform jumpWaypoint;

        public Transform eyes;

        public AI.State currentState;
        public AI.State remainState;

        [HideInInspector]
        public int wayPointIndex;

        [HideInInspector]
        public AI.BehaviorController behaviorController;

        [HideInInspector]
        public AI.Shooting shooting;

        [HideInInspector]
        public Transform chaseTarget;

        [HideInInspector]
        public float stateTimeElapsed;

        private bool m_aiActive;

        private float m_lookSphereCastRadius;

        void Awake()
        {
            m_aiActive = true;
        }

        // Use this for initialization
        void Start()
        {
            behaviorController = GetComponent<AI.BehaviorController>();

            shooting = GetComponent<AI.Shooting>();

            wayPointIndex = 0;

            m_lookSphereCastRadius = behaviorController.enemyStats.lookSphereCastRadius;
        }

        // Update is called once per frame
        void Update()
        {
            if(!m_aiActive)
            {
                return;
            }

            currentState.UpdateState(this);
        }

        // For Debug: allow us to draw things on the scene view 
        // but not in the game view
        void OnDrawGizmos()
        {
            if (currentState && eyes)
            {
                Gizmos.color = currentState.sceneGizmoColor;

                Gizmos.DrawWireSphere(eyes.position, m_lookSphereCastRadius);
            }
        }

        public void TransitionToState(AI.State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;

            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0.0f;
        }
    }

}
