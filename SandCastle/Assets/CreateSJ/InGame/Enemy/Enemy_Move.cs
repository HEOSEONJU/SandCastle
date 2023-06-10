using System.Collections;

using UnityEngine;

namespace Enemy
{
    public class Enemy_Move : MonoBehaviour
    {
        [SerializeField]
        Enemy_Manager enemymanager;
        Vector3 movePoint;
        Vector3 direction;

        public Vector3 targetVector;
        
        
        [SerializeField]
        bool active;//�۵���

        

        public bool Active
        {
            get { return active;}
        }


        public void Move_Next_Point(Vector3 point,Vector3 t)
        {
            direction = point.normalized;
            targetVector= t;
        }

        public void StopMove()
        {
            StopCoroutine(MovePoint());
            active = false;


            enemymanager.BaseAttack();


        }
        public IEnumerator MovePoint()
        {
            active= true;
            while(active) {

                //transform.position += direction*moveSpeed*Time.deltaTime;
                transform.position = Vector3.MoveTowards(gameObject.transform.position, targetVector, Time.deltaTime* enemymanager.EnemyStatus.MoveSpeed);
                yield return null;
            }
            
        }
        
    }
}