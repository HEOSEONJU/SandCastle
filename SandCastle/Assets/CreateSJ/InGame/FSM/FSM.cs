using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM 
{
    BaseState current;
    

    public FSM(BaseState state)
    {
        current = state;
    }




    public void ChangeState(BaseState next)
    {
        
        if(next==current)
        {
            return;
        }
        if(current!=null)
        {
            current.OnStateExit();
        }

        current = next;
        current.OnStateEnter();




    }

    public void UpdateState()
    {
        if (current != null)
        {
            current.OnStateUpdate();
        }
    }

}
