using Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGame
{
    public class PatrolPoint : MonoBehaviour
    {
        [SerializeField]
        List<Transform> patrolPoint;
        [SerializeField]
        

        public float hp = 10;


        public List<Transform> PatrolPoints
        {
            get { return patrolPoint; }
        }

        public int count;


        public bool Enable;

        public void Init(float defaluthp,float multiply)
        {
            
            hp = defaluthp*multiply;
            Enable = true;
            count = 0;
            patrolPoint = transform.GetComponentsInChildren<Transform>().ToList();
            patrolPoint.Remove(this.transform);
        }


        public bool Alive()
        {
            return Enable;
        }

        public Transform ReturnPosition()
        {
            if (Enable)
            {
                if (count == patrolPoint.Count)
                {
                    count = 0;
                }

                return patrolPoint[count++];
            }
            else
                return null;
        }
    }
}