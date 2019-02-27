using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class DamageCheckout : Action
    {
        public SharedTransform monsterTransform;
        private MonsterController m_Controller;

        public override void OnAwake()
        {
            m_Controller = monsterTransform.Value.GetComponent<MonsterController>();
        }

        public override TaskStatus OnUpdate()
        {
            m_Controller.DamageCheckout();

            // make sure the damage counter is cleared 
            return (m_Controller.damageCounter.Count == 0) ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}
