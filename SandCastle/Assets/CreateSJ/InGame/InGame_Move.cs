using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace InGame
{
    public class InGame_Move : MonoBehaviour
    {
        [SerializeField]
        bool fix;




        [SerializeField]
        Transform defaultPosition;
        
        public float value=0.1f;//거리보정값


        public bool Fix
        {
            get { return fix; }
        }
        public void MoveChar(Animator animator,float speed)
        {
            if(Fix)
            { return; }

            Debug.Log(Distance());
            if (Distance() >= value)
            {
                Vector3 direction = transform.position - defaultPosition.position;
                animator.SetFloat("Amount", MathF.Abs(direction.x) + MathF.Abs(direction.y));
                animator.SetFloat("Amount_X", direction.x);
                animator.SetFloat("Amount_Y", direction.y);
                float step = Time.deltaTime * speed;

                transform.position = Vector3.MoveTowards(transform.position, defaultPosition.position, step);
                
            }
            


        }
        public void StopChar(Animator animator)
        {
            
            animator.SetFloat("Amount_X", 0f);
            animator.SetFloat("Amount_Y", 0f);
            animator.SetFloat("Amount", 0f);

        }

        public void SettingPosi(Transform T=null)
        {
            if(T is null)
            {
                fix = true;
                return;
            }
            defaultPosition = T;
            fix = false;
        }

        public  float Distance()
        {
           return Vector3.Distance(transform.position , defaultPosition.position);
        }

        public float Angle()
        {
            
            


            return Vector3.Cross(transform.up, defaultPosition.position - transform.position).z;
        }
    }

}