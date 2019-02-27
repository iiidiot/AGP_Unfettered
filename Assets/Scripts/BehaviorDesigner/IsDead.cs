using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class IsDead : Conditional
    {
        public SharedTransform monsterTransform;
        private MonsterController m_Controller;

        public override void OnAwake()
        {
            m_Controller = monsterTransform.Value.GetComponent<MonsterController>();
        }

        public override TaskStatus OnUpdate()
        {
            if (m_Controller.m_hp < 0)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}
