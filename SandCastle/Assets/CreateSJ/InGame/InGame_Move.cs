using System;
using UnityEngine;


namespace InGame
{
    public class InGame_Move : MonoBehaviour
    {
        

        [SerializeField]
        Transform defaultPosition;
        
        public float value=0.1f;//거리보정값

        
        


        [SerializeField]
        float distacne;
        [SerializeField]
        Rigidbody2D rigid;


        public Vector3 dir;


        public void MoveChar(Animator animator,float speed)
        {
            

            
            

            rigid.velocity = dir * speed;
            

            animator.SetFloat("Amount", MathF.Abs(dir.x) + MathF.Abs(dir.y));
            animator.SetFloat("Amount_X", dir.x);
            animator.SetFloat("Amount_Y", dir.y);
            


            


        }
        public void StopChar(Animator animator)
        {
            
            
            rigid.velocity = Vector3.zero;
            animator.SetFloat("Amount_X", 0f);
            animator.SetFloat("Amount_Y", 0f);
            animator.SetFloat("Amount", 0f);

        }

        public void SettingPosi(Transform T)
        {

            


            defaultPosition = T;
            

            
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