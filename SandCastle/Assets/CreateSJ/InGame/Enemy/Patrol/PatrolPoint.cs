using Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGame
{
    public class PatrolPoint : MonoBehaviour,IHit
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

        public void Init()
        {
            hp = 10;
            Enable = true;
            count = 0;
            patrolPoint = transform.GetComponentsInChildren<Transform>().ToList();
            patrolPoint.Remove(this.transform);
        }

        public void Hit(float value)
        {
            hp -= value;   
            if(hp<=0)
            {
                Enable = false;
                gameObject.SetActive(false);
            }
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