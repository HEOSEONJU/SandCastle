using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using InGame;
public abstract class BaseState
{
    protected InGame_Char IGC;

    protected BaseState(InGame_Char igc) { this.IGC = igc; }


    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();






}
