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
        InGame_Status status;
        
        [SerializeField]
        Input_Joystikc inputJoystick;

        
        public float Speed
        {
            get { return status.MoveSpeed; }
            
        }


        [SerializeField]
        Rigidbody2D rigid;


        public void MoveChar(Animator animator)
        {
            animator.SetInteger("Amount", Convert.ToInt32((MathF.Abs( inputJoystick.inputVector.x) +MathF.Abs( inputJoystick.inputVector.y))));
            animator.SetFloat("Amount_X", inputJoystick.inputVector.x);
            animator.SetFloat("Amount_Y", inputJoystick.inputVector.y);
            rigid.velocity = inputJoystick.inputVector.normalized * Speed;

        }
        public void StopChar(Animator animator)
        {
            
            animator.SetFloat("Amount_X", 0f);
            animator.SetFloat("Amount_Y", 0f);
            rigid.velocity = Vector2.zero;
        }

        
    }

}