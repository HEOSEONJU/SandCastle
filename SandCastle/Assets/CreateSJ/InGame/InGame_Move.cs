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
        }
        [SerializeField]
        float distacne;

        public void MoveChar(Animator animator,float speed)
        {
            if(Fix)
            { return; }

            agent.enabled = true;
            if (Distance() >= value && agent.enabled)
            {
                
                
                distacne = Distance();
                
                Vector3 direction = transform.position - defaultPosition.position;
                animator.SetFloat("Amount", MathF.Abs(direction.x) + MathF.Abs(direction.y));
                animator.SetFloat("Amount_X", direction.x);
                animator.SetFloat("Amount_Y", direction.y);

                agent.speed = speed;
                Vector3 dir = defaultPosition.position;
                dir.z = transform.position.z;


                agent.SetDestination(dir);

                //transform.position = Vector3.MoveTowards(transform.position, defaultPosition.position, step);

            }

            


        }
        public void StopChar(Animator animator)
        {
            
            agent.enabled = false;
            
            animator.SetFloat("Amount_X", 0f);
            animator.SetFloat("Amount_Y", 0f);
            animator.SetFloat("Amount", 0f);

        }

        public void SettingPosi(Transform T=null)
        {

            agent.updateRotation = false;
            agent.updateUpAxis = false;



            if (T is null)
            {
                defaultPosition = transform.parent;
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

        public bool NeedMove()
        {
            if (Distance() >= value && !Fix)
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