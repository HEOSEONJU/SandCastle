using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class ResetBoolState : StateMachineBehaviour
{


    [SerializeField]
    string resetBoolName;
    [SerializeField]
    bool state;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(resetBoolName, state);
    }
}
