using System.Collections;

using UnityEngine;

namespace Enemy
{
    public class Enemy_Move : MonoBehaviour
    {
        [SerializeField]
        Enemy_Manager enemymanager;


        [SerializeField]
        Transform target;
        
        
        
        [SerializeField]
        bool active;//¿€µø¡ﬂ

        
        public IEnumerator moveCoroutine;

        public bool Active
        {
            get { return active;}
        }


        public void Move_Next_Point(Transform point)
        {
            target = point;   
        }

        public void StopMove()
        {
            StopCoroutine(moveCoroutine);
            active = false;


            


        }
        public void MovePoint()
        {
            moveCoroutine = MoveCoroutine();
            StartCoroutine(moveCoroutine);
        }
        
        public IEnumerator MoveCoroutine()
        {
            
            active = true;
            while(active) {

                if(Vector3.Distance(transform.position, target.position)<=0.1f)
                {
                    active = false;
                }

                
                transform.position = Vector3.MoveTowards(gameObject.transform.position,target.position, Time.deltaTime* enemymanager.EnemyStatus.MoveSpeed);
                yield return null;
            }

            enemymanager.BaseAttack();

        }
        
    }
}