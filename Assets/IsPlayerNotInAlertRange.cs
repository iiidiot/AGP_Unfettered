using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner;
using BehaviorDesigner.Runtime.Tasks;

public class IsPlayerNotInAlertRange : BehaviorDesigner.Runtime.Tasks.Conditional
{
 
    private PlayerInTriggerController_BT m_TriggerController;

    public override void OnAwake()
    {
        m_TriggerController = this.transform.Find("Positions/AlertRange").GetComponent<PlayerInTriggerController_BT>();
    }

    public override TaskStatus OnUpdate()
    {
        if (m_TriggerController.GetIsPlayerInAlertRange())
        {
            return TaskStatus.Running; 

        }
        else
        {
            return TaskStatus.Success;  // 没找到player 下一帧继续执行此任务
        }
    }
    


}
