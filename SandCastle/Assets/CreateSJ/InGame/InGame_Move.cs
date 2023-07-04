using Google.GData.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

namespace InGame
{
    public class InGame_Move : MonoBehaviour
    {
        [SerializeField]
        bool fix;

        [SerializeField]
        NavMeshAgent agent;
            

        




        [SerializeField]
        Transform defaultPosition;
        
        public float value=0.1f;//거리보정값

        
        public NavMeshAgent Agent
        {
            get { return agent; }
        }

        public bool Fix
        {
            get { return fix; }
            set { fix = value;  }
        }
        [SerializeField]
        float distacne;
        [SerializeField]
        Rigidbody2D rigid;


        public Vector3 dir;


        public void MoveChar(Animator animator,float speed)
        {
            if(Fix)
            { return; }

            
            

            rigid.velocity = dir * speed;
            

            animator.SetFloat("Amount", MathF.Abs(dir.x) + MathF.Abs(dir.y));
            animator.SetFloat("Amount_X", dir.x);
            animator.SetFloat("Amount_Y", dir.y);
            

            return;
            if (Distance() >= value )
            {
                
                
                distacne = Distance();
                
                Vector3 direction = transform.position - defaultPosition.position;
                animator.SetFloat("Amount", MathF.Abs(direction.x) + MathF.Abs(direction.y));
                animator.SetFloat("Amount_X", direction.x);
                animator.SetFloat("Amount_Y", direction.y);

                agent.speed = speed;
                Vector3 dir = defaultPosition.position;
                dir.z = transform.position.z;

                
                //agent.SetDestination(dir);

                //transform.position = Vector3.MoveTowards(transform.position, defaultPosition.position, step);

            }

            


        }
        public void StopChar(Animator animator)
        {
            
            agent.enabled = false;
            rigid.velocity = Vector3.zero;
            animator.SetFloat("Amount_X", 0f);
            animator.SetFloat("Amount_Y", 0f);
            animator.SetFloat("Amount", 0f);

        }

        public void SettingPosi(Transform T,bool fix)
        {

            agent.updateRotation = false;
            agent.updateUpAxis = false;


            defaultPosition = T;
            this.fix = fix;

            
        }

        public  float Distance()
        {
           return Vector3.Distance(transform.position , defaultPosition.position);
        }

        public bool NeedMove()
        {
            if(dir.magnitude>0.01f)
            {
                return true;
            }

            
                return false;
        }

        public float Angle()
        {
            
            


            return Vector3.Cross(transform.up, defaultPosition.position - transform.position).z;
        }
    }

}