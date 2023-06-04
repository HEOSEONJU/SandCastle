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
        public Enemy_Status EnemyStatus
        {
            get { return enemyStatus; }
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
        public void BaseAttack()
        {
            if (enemyStatus.Hp <= 0)
            {
                return;
            }
            
            RaycastHit2D[] hitlist = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, Vector2.zero);
            
            foreach (var hit in hitlist)
            {
                hit.collider.transform.TryGetComponent<BaseHP>(out BaseHP basehp);
                if (basehp != null)
                {
                    Debug.Log("베이스적중");
                }
            }
            gameObject.SetActive(false);
        }


    }
}