using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Move : MonoBehaviour
    {
        Vector3 movePoint;
        Vector3 direction;

        public Vector3 targetVector;
        [SerializeField]
        float moveSpeed=1f;
        float speedValue=5f;//속도보정값
        bool active;//작동중

        public float MoveSpeed
        {
            set { moveSpeed = value; }
        }

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

        }
        public IEnumerator MovePoint()
        {
            active= true;
            while(active) {

                //transform.position += direction*moveSpeed*Time.deltaTime;
                transform.position = Vector3.MoveTowards(gameObject.transform.position, targetVector, Time.deltaTime*moveSpeed*speedValue);
                yield return null;
            }
            
        }
    }
}