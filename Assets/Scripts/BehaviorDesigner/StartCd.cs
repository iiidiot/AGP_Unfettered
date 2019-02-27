using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class StartCd : Action
    {
        public SharedTransform monsterTransform;
        private MonsterController m_Controller;

        public override void OnAwake()
        {
            m_Controller = monsterTransform.Value.GetComponent<MonsterController>();
        }

        public override TaskStatus OnUpdate()
        {
            m_Controller.m_attackCDTimeCounter = m_Controller.attackCDTime;
            return (m_Controller.m_attackCDTimeCounter == m_Controller.attackCDTime) ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}
