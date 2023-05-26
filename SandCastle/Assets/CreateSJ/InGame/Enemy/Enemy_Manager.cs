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
        public Enemy_Move EnemyMove
        { get { return enemyMove; } 
        }
        
        public void Reseting(float speed,float hp)
        {
            enemyMove.MoveSpeed = speed;
            enemyStatus.Hp = hp;
        }

        public void StartMove(Transform point)
        {
            enemyMove.Move_Next_Point(point.position - transform.position, point.position);
            enemyMove.StartCoroutine(enemyMove.MovePoint());
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


    }
}