using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGame
{
    public class PatrolSetting : MonoBehaviour
    {
        [SerializeField]
        Transform nexus;

        public Transform Nexus
        {
            get { return nexus; }
        }

        [SerializeField]
        List<PatrolPoint> patrolPoint;
        public void Init()
        {
            patrolPoint = transform.GetComponentsInChildren<PatrolPoint>().ToList();
            
            foreach(var p in patrolPoint)
            {
                p.Init();
            }
        }


        public PatrolPoint SwpanPoint(int n)
        {

            return patrolPoint[n];
        }
    }
}