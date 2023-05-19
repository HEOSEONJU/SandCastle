using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Manager : MonoBehaviour
    {
        

        [SerializeField]
        Enemy_Move enemyMove;
        [SerializeField]
        Enemy_Status enemyStatus;
        
        public void Start()
        {
            
        }

        public void StartMove(Transform point)
        {
            enemyMove.Move_Next_Point(point.position - transform.position);
        }
        public void Hit(float value)
        {
            enemyStatus.Hp -= value;
            if(enemyStatus.Hp <= 0)
            {
                enemyMove.StopMove();
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
         if(collision.CompareTag("Patrol"))
            {
                Debug.Log("충돌");
                collision.TryGetComponent<PatrolPoint>(out PatrolPoint patrolpoint);
                if(!(patrolpoint is null))
                {
                    Debug.Log("명령");
                    
                    patrolpoint.NextOrder(enemyMove);
                }
                
            }
        }

    }
}