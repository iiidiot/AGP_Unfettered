using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class IsPlayerNotInRange : BehaviorDesigner.Runtime.Tasks.Conditional
{

    public SharedTransform rangeTransform;
    private PlayerInTriggerController_BT m_TriggerController;

    public override void OnAwake()
    {
        m_TriggerController = rangeTransform.Value.GetComponent<PlayerInTriggerController_BT>();
    }

    public override TaskStatus OnUpdate()
    {
        if (m_TriggerController.GetIsPlayerInAlertRange())
        {
            return TaskStatus.Failure; // 找到player 下一帧继续执行此任务
        }
        else
        {
            return TaskStatus.Success;  // 没找到player -> ok
        }
    }
    


}
