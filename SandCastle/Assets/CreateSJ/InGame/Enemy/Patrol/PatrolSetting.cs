using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSetting : MonoBehaviour
{
    private void Start()
    {
        int count = transform.childCount;
        PatrolPoint patrolpoint;

        for(int i=0;i<count-1; i++) 
        {
            transform.GetChild(i).TryGetComponent<PatrolPoint>(out patrolpoint);
            
            patrolpoint.NextToPoint = transform.GetChild(i+1).transform;
            
        }
        transform.GetChild(count-1).TryGetComponent<PatrolPoint>(out patrolpoint);
        patrolpoint.NextToPoint = transform.GetChild(count-1).transform;


    }
}
