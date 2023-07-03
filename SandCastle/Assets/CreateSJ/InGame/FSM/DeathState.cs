using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : PlayerBaseState
{


    public DeathState(InGame_Char igc) : base(igc)
    {
        
    }
    // Start is called before the first frame update
    public override void OnStateEnter()
    {
        IGC.InGameMove.StopChar(IGC.Animator);
    }
    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
