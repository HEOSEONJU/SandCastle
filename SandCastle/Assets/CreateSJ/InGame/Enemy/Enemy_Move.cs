using Google.GData.Extensions;
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy_Move : MonoBehaviour
    {
        [SerializeField]
        Enemy_Manager enemymanager;


        [SerializeField]
        Transform target;

        [SerializeField]
        NavMeshAgent agent;


        [SerializeField]
        bool active;//¿€µø¡ﬂ


        public IEnumerator moveCoroutine;

        public bool Active
        {
            get { return active; }
        }


        public void SettingPoint(Transform point)
        {
            
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            target = point;
        }

        public void StopMove()
        {
            agent.enabled = false;
            active = false;





        }

        public void MoveEnemy()
        {
            agent.enabled = true;



            agent.speed = enemymanager.EnemyStatus.MoveSpeed;
            Vector3 dir = target.position;
            dir.z = transform.position.z;

            agent.SetDestination(dir);


        }

        

        public float Distance()
        {
            
            return Vector3.Distance(transform.position, target.position);
        }
    }
}