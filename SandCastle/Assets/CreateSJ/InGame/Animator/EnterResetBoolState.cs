using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterResetBoolState : StateMachineBehaviour
{
    [SerializeField]
    string resetBoolName;
    [SerializeField]
    bool state;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(resetBoolName, state);
    }
    
}
