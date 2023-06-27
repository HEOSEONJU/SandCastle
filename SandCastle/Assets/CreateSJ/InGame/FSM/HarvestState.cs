using InGame;
using InGameResourceEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestState : BaseState
{


    public HarvestState(InGame_Char igc) : base(igc)
    {

    }
    public override void OnStateEnter()
    {

        Debug.Log("수확시작");
        IGC.IsAction = true;
        
        


        switch (IGC.Harvest.Search.Target.resourceType)
        {
            case ResourceEnum.sand:
                IGC.Animator.CrossFade("CharSand", 0.01f);
                break;
            case ResourceEnum.water:
                IGC.Animator.CrossFade("CharWater", 0.01f);
                break;
            case ResourceEnum.mud:
                IGC.Animator.CrossFade("CharMud", 0.01f);
                break;
        }

    }
    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        Debug.Log("수확끝");
        
        IGC.Animator.SetTrigger("HarvestExit");
    }
}
