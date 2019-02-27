using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    public class IsHurt : Conditional
    {
        public SharedTransform monsterTransform;
        private MonsterController m_Controller;
        private double m_lastCheckHp;

        public override void OnAwake()
        {
            m_Controller = monsterTransform.Value.GetComponent<MonsterController>();
            m_lastCheckHp = m_Controller.maxHP;
        }

        public override TaskStatus OnUpdate()
        {
            //Debug.Log(m_Controller.m_hp + ", check: " + m_lastCheckHp);
            if (m_Controller.m_hp < m_lastCheckHp)
            {
                m_lastCheckHp = m_Controller.m_hp;
                
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}
