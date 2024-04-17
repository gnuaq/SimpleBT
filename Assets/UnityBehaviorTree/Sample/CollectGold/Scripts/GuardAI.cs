using System.Collections;
using System.Collections.Generic;
using UnityBehaviorTree.Core;
using UnityEngine;

public class GuardAI : BehaviorTreeComponent
{
    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (!CollectGoldManager.Instance.IsEndGame)
        {
            base.Update();
        }
    }
}
