using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Move : MonoBehaviour
    {
        Vector3 movePoint;
        Vector3 direction;
        [SerializeField]
        float moveSpeed=1f;

        


        public void Move_Next_Point(Vector3 point)
        {

            StopMove();
            direction = point.normalized;



            StartCoroutine(MovePoint());

        }

        public void StopMove()
        {
            StopCoroutine(MovePoint());
        }
        IEnumerator MovePoint()
        {
            while(true) {
                
                transform.position += direction*moveSpeed*Time.deltaTime;

                yield return null;
            }
            
        }
    }
}