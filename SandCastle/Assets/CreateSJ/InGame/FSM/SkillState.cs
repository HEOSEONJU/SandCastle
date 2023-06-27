using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : BaseState
{


    public SkillState(InGame_Char igc) : base(igc)
    {

    }
    // Start is called before the first frame update
    public override void OnStateEnter()
    {
        IGC.Animator.SetBool("Infinity", true);
        IGC.InGameStatus.CurrentMana = 0;
        IGC.Animator.CrossFade("CharSkill", 0.01f);
        
    }
    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        IGC.Animator.SetBool("Infinity", false);
    }

    
}
