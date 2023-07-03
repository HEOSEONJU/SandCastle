using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallState : PlayerBaseState
{


    public RecallState(InGame_Char igc) : base(igc)
    {

    }
    // Start is called before the first frame update
    public override void OnStateEnter()
    {
        
        IGC.Animator.SetBool("RecallEnd", false);
        IGC.Animator.SetBool("Infinity", true);
    }
    public override void OnStateUpdate()
    {
        IGC.InGameMove.MoveChar(IGC.Animator, IGC.InGameStatus.MoveSpeed*2);
    }

    public override void OnStateExit()
    {
        Debug.Log("¸®ÄÝÅ»Ãâ");
        IGC.Animator.SetBool("Infinity", false);
        IGC.Animator.SetBool("RecallEnd", true);
    }
}
