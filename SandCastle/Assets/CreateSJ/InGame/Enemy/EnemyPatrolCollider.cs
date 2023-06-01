using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyPatrolCollider : MonoBehaviour
    {
        [SerializeField]
        Enemy_Manager enemyManager;
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Patrol"))
            {
                
                collision.TryGetComponent<PatrolPoint>(out PatrolPoint patrolpoint);
                if (!(patrolpoint is null))
                {
                    

                    patrolpoint.NextOrder(enemyManager.EnemyMove);
                }

            }
        }
    }
}