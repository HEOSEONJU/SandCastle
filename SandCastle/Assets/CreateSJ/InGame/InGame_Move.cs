using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace InGame
{
    public class InGame_Move : MonoBehaviour
    {


        public void MoveChar(Animator animator,Vector2 inputvector)
        {
            animator.SetInteger("Amount", Convert.ToInt32((MathF.Abs(inputvector.x) +MathF.Abs(inputvector.y))));
            animator.SetFloat("Amount_X", inputvector.x);
            animator.SetFloat("Amount_Y", inputvector.y);


        }
        public void StopChar(Animator animator)
        {
            
            animator.SetFloat("Amount_X", 0f);
            animator.SetFloat("Amount_Y", 0f);

        }

        
    }

}