using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{


    public MoveState(InGame_Char igc) : base(igc)
    {

    }
    // Start is called before the first frame update
    public override void OnStateEnter()
    {

    }
    public override void OnStateUpdate()
    {
        IGC.InGameMove.MoveChar(IGC.Animator, IGC.InGameStatus.MoveSpeed);
        IGC.InGameAttack.PlayAttack();
    }

    public override void OnStateExit()
    {
        IGC.InGameMove.StopChar(IGC.Animator);
    }
}
