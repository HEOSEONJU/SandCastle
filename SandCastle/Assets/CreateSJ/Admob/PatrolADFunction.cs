using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MainUI;
public class PatrolADFunction : ADFunction
{
    [SerializeField]
    GainPatrol gainPatrol;


    public override void Reward()
    {
        gainPatrol.OpenGain();
    }
}
