using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolSetting : MonoBehaviour
{
    [SerializeField]
    Transform nexus;

    public Transform Nexus
    {
        get { return nexus; }
    }

    [SerializeField]
    List<Transform> patrolPoint;
    public void Init()
    {
        patrolPoint = transform.GetComponentsInChildren<Transform>().ToList();
        patrolPoint.Remove(this.transform);
    }
    

    public Transform SwpanPoint()
    {

        return patrolPoint[Random.Range(0, patrolPoint.Count)];
    }
}
