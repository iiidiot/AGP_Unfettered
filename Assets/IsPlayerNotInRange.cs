using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class IsPlayerNotInRange : BehaviorDesigner.Runtime.Tasks.Conditional
{

    public Transform rangeTransform;
    private PlayerInTriggerController_BT m_TriggerController;

    public override void OnAwake()
    {
        m_TriggerController = rangeTransform.GetComponent<PlayerInTriggerController_BT>();
    }

    public override TaskStatus OnUpdate()
    {
        if (m_TriggerController.GetIsPlayerInAlertRange())
        {
            return TaskStatus.Running; // 找到player 下一帧继续执行此任务
        }
        else
        {
            return TaskStatus.Success;  // 没找到player -> ok
        }
    }
    


}
