using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    Transform nextToPoint;

    public Transform NextToPoint
    {
        get {
          
            return nextToPoint; 
        }
        set { nextToPoint = value; }
    }


    public void NextOrder(Enemy_Move enemymove)
    {
        if (NextToPoint.position == enemymove.targetVector)
        {
            
            enemymove.StopMove(); 


        }
        else
        {
            StartCoroutine(Delay_Move(enemymove));
            
        }

    }
   IEnumerator Delay_Move(Enemy_Move enemymove)
    {
        yield return null;
        yield return null;
        
        enemymove.Move_Next_Point(nextToPoint.position - transform.position, nextToPoint.position);
    }

}
