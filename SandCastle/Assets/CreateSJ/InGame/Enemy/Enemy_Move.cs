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
        Rigidbody2D rigid;


        [SerializeField]
        bool active;//¿€µø¡ﬂ


        public IEnumerator moveCoroutine;

        public bool Active
        {
            get { return active; }
        }

        public Transform Target
        {
            get { return target; }
            set { target = value; }
        }



        public void StopMove()
        {
            rigid.velocity= Vector3.zero;
            active = false;





        }

        public void MoveEnemy()
        {
            Vector3 dir = target.position-transform.position;
            dir.z = transform.position.z;
            rigid.velocity = dir.normalized * enemymanager.EnemyStatus.MoveSpeed;
            //rigid.AddForce(dir*Time.deltaTime);
            //rigid.velocity = rigid.velocity.normalized * enemymanager.EnemyStatus.MoveSpeed;





        }

        

        public float Distance()
        {
            
            return Vector3.Distance(transform.position, target.position);
        }
    }
}