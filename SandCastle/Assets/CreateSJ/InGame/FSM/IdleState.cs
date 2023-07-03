using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{


    public IdleState(InGame_Char igc) : base(igc)
    {

    }
    // Start is called before the first frame update
    public override void OnStateEnter()
    {
        
        IGC.InGameMove.StopChar(IGC.Animator);
    }
    public override void OnStateUpdate()
    {
        IGC.InGameAttack.PlayAttack();
        
    }

    public override void OnStateExit()
    {
        IGC.InGameMove.StopChar(IGC.Animator);
    }
}
